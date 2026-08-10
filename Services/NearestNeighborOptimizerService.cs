using Microsoft.EntityFrameworkCore;
using OrionMVP.Data;
using OrionMVP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrionMVP.Services
{
    public class NearestNeighborOptimizerService : IRouteOptimizerService
    {
        private readonly OrionDbContext _db;

        public NearestNeighborOptimizerService(OrionDbContext db)
        {
            _db = db;
        }

        public async Task<OptimizationResultDto> OptimizeRouteAsync(int routeId, double startLat = -34.6037, double startLng = -58.3816)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = new OptimizationResultDto();

            var route = await _db.Routes
                .Include(r => r.Stops)
                .FirstOrDefaultAsync(r => r.Id == routeId);

            if (route == null || !route.Stops.Any())
            {
                stopwatch.Stop();
                result.IsSuccess = false;
                result.Message = "La ruta especificada no existe o no tiene paradas cargadas.";
                return result;
            }

            var unvisited = route.Stops.ToList();
            var optimizedSequence = new List<RouteStop>();

            double currentLat = startLat;
            double currentLng = startLng;
            double totalDistance = 0.0;

            int order = 1;
            while (unvisited.Count > 0)
            {
                // Find nearest unvisited stop using Haversine metric
                RouteStop nearest = unvisited[0];
                double minDistance = CalculateHaversineDistance(currentLat, currentLng, nearest.Latitude, nearest.Longitude);

                for (int i = 1; i < unvisited.Count; i++)
                {
                    double dist = CalculateHaversineDistance(currentLat, currentLng, unvisited[i].Latitude, unvisited[i].Longitude);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        nearest = unvisited[i];
                    }
                }

                totalDistance += minDistance;
                nearest.SequenceOrder = order++;
                optimizedSequence.Add(nearest);

                currentLat = nearest.Latitude;
                currentLng = nearest.Longitude;
                unvisited.Remove(nearest);
            }

            // Update route stats in DB
            route.TotalDistanceKm = Math.Round(totalDistance, 2);
            await _db.SaveChangesAsync();

            stopwatch.Stop();

            result.IsSuccess = true;
            result.TotalDistanceKm = route.TotalDistanceKm;
            result.ExecutionTimeMs = stopwatch.ElapsedMilliseconds;
            result.ProcessedStopsCount = optimizedSequence.Count;
            result.OptimizedStops = optimizedSequence;
            result.Message = $"Secuencia optimizada con éxito para {optimizedSequence.Count} paradas en {stopwatch.ElapsedMilliseconds} ms. Distancia total: {route.TotalDistanceKm} km.";

            return result;
        }

        public static double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371.0; // Earth radius in kilometers
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            return R * c;
        }

        private static double ToRadians(double angle)
        {
            return (Math.PI / 180.0) * angle;
        }
    }
}
