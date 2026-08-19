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

        public async Task<OptimizationResultDto> OptimizeRouteAsync(int routeId, double startLat = 18.4861, double startLng = -69.9312)
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
                result.Message = "La ruta no contiene artículos o no existe.";
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
                nearest.Sequence = nearest.SequenceOrder;
                optimizedSequence.Add(nearest);

                currentLat = nearest.Latitude != 0 ? nearest.Latitude : currentLat;
                currentLng = nearest.Longitude != 0 ? nearest.Longitude : currentLng;
                unvisited.Remove(nearest);
            }

            route.TotalDistanceKm = Math.Round(totalDistance, 2);
            await _db.SaveChangesAsync();

            stopwatch.Stop();

            result.IsSuccess = true;
            result.TotalDistanceKm = route.TotalDistanceKm;
            result.ExecutionTimeMs = stopwatch.ElapsedMilliseconds;
            result.ProcessedStopsCount = optimizedSequence.Count;
            result.OptimizedStops = optimizedSequence;
            result.Message = $"Secuenciador ORION: {optimizedSequence.Count} paradas ordenadas por cercanía lineal en {stopwatch.ElapsedMilliseconds} ms (Distancia estimada: {route.TotalDistanceKm} km).";

            return result;
        }

        public static double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            if (lat1 == 0 && lon1 == 0 || lat2 == 0 && lon2 == 0) return 1.5; // Distancia estimada de fallback
            const double R = 6371.0; // Radio de la tierra en KM
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
