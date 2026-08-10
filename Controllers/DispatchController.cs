using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrionMVP.Data;
using OrionMVP.Models;
using OrionMVP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route = OrionMVP.Models.Route;

namespace OrionMVP.Controllers
{
    public class DispatchController : Controller
    {
        private readonly OrionDbContext _db;
        private readonly IAddressImportService _importService;
        private readonly IGeocodingService _geocodingService;

        public DispatchController(OrionDbContext db, IAddressImportService importService, IGeocodingService geocodingService)
        {
            _db = db;
            _importService = importService;
            _geocodingService = geocodingService;
        }

        public async Task<IActionResult> Index()
        {
            var routes = await _db.Routes
                .Include(r => r.Driver)
                .Include(r => r.Stops)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            ViewBag.Drivers = await _db.Drivers.ToListAsync();
            return View(routes);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int? driverId)
        {
            var result = await _importService.ProcessFileAsync(file);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                if (result.MissingColumns.Any())
                {
                    TempData["MissingColumns"] = string.Join(", ", result.MissingColumns);
                }
                return RedirectToAction("Index");
            }

            var driver = await _db.Drivers.FirstOrDefaultAsync(d => d.Id == (driverId ?? 1));
            var newRoute = new Route
            {
                Name = $"Ruta {DateTime.Now:yyyy-MM-dd HH:mm} - {result.TotalRead} paradas",
                Date = DateTime.UtcNow,
                DriverId = driver?.Id ?? 1,
                Status = "No iniciado",
                BaselineDistanceKm = Math.Round(result.TotalRead * 4.2, 1),
                CreatedAt = DateTime.UtcNow
            };

            int order = 1;
            foreach (var rec in result.Records)
            {
                newRoute.Stops.Add(new RouteStop
                {
                    CustomerName = rec.CustomerName,
                    Street = rec.Street,
                    Number = rec.Number,
                    City = rec.City,
                    SequenceOrder = order++,
                    Status = "Pendiente",
                    IsGeocoded = false
                });
            }

            _db.Routes.Add(newRoute);
            await _db.SaveChangesAsync();

            // Auto-trigger initial geocoding
            await InternalGeocodeRouteStops(newRoute.Id);

            TempData["SuccessMessage"] = $"¡Éxito! Se cargaron {result.TotalRead} direcciones y se procesó la geocodificación inicial.";
            return RedirectToAction("RouteDetails", new { id = newRoute.Id });
        }

        public async Task<IActionResult> RouteDetails(int id)
        {
            var route = await _db.Routes
                .Include(r => r.Driver)
                .Include(r => r.Stops)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null)
            {
                return NotFound();
            }

            route.Stops = route.Stops.OrderBy(s => s.SequenceOrder).ToList();
            return View(route);
        }

        [HttpPost]
        public async Task<IActionResult> GeocodeRoute(int routeId)
        {
            int updatedCount = await InternalGeocodeRouteStops(routeId);
            TempData["SuccessMessage"] = $"Se geocodificaron las paradas de la ruta #{routeId} mediante API externa.";
            return RedirectToAction("RouteDetails", new { id = routeId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStopAddress(int stopId, string street, string number, string city)
        {
            var stop = await _db.RouteStops.FindAsync(stopId);
            if (stop == null)
            {
                return NotFound();
            }

            stop.Street = street;
            stop.Number = number;
            stop.City = city;

            // Re-geocode updated address
            var geoResult = await _geocodingService.GeocodeAddressAsync(street, number, city);
            if (geoResult.IsSuccess)
            {
                stop.Latitude = geoResult.Latitude;
                stop.Longitude = geoResult.Longitude;
                stop.IsGeocoded = true;
                stop.HasGeocodingError = false;
                stop.ExternalNavUrl = $"https://www.google.com/maps/search/?api=1&query={geoResult.Latitude},{geoResult.Longitude}";
            }
            else
            {
                stop.IsGeocoded = false;
                stop.HasGeocodingError = true;
            }

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Dirección de la parada #{stop.SequenceOrder} actualizada y re-geocodificada correctamente.";
            return RedirectToAction("RouteDetails", new { id = stop.RouteId });
        }

        [HttpGet]
        public IActionResult DownloadSampleCsv()
        {
            var csv = "Calle,Altura,Ciudad,Cliente\n" +
                      "Av. Corrientes,1234,Buenos Aires,Juan Perez\n" +
                      "Calle Erronea Invalida,999,Desconocido,Cliente Con Error\n" +
                      "Av. Santa Fe,2500,Buenos Aires,Maria Gomez\n" +
                      "Calle Florida,450,Buenos Aires,Tech Corp\n" +
                      "Av. Cabildo,3000,Buenos Aires,Distribuidora Sur\n";

            var bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "Plantilla_Direcciones_UPS.csv");
        }

        private async Task<int> InternalGeocodeRouteStops(int routeId)
        {
            var stops = await _db.RouteStops.Where(s => s.RouteId == routeId).ToListAsync();
            int count = 0;

            foreach (var stop in stops)
            {
                var geoResult = await _geocodingService.GeocodeAddressAsync(stop.Street, stop.Number, stop.City);
                if (geoResult.IsSuccess)
                {
                    stop.Latitude = geoResult.Latitude;
                    stop.Longitude = geoResult.Longitude;
                    stop.IsGeocoded = true;
                    stop.HasGeocodingError = false;
                    stop.ExternalNavUrl = $"https://www.google.com/maps/search/?api=1&query={geoResult.Latitude},{geoResult.Longitude}";
                }
                else
                {
                    stop.IsGeocoded = false;
                    stop.HasGeocodingError = true;
                }
                count++;
            }

            await _db.SaveChangesAsync();
            return count;
        }
    }
}
