using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrionMVP.Data;
using OrionMVP.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Route = OrionMVP.Models.Route;

namespace OrionMVP.Controllers
{
    public class DriverController : Controller
    {
        private readonly OrionDbContext _db;

        public DriverController(OrionDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int? driverId, int? routeId)
        {
            var selectedDriverId = driverId ?? 1;
            
            var driver = await _db.Drivers
                .Include(d => d.Routes)
                .ThenInclude(r => r.Stops)
                .FirstOrDefaultAsync(d => d.Id == selectedDriverId);

            ViewBag.Drivers = await _db.Drivers.ToListAsync();
            ViewBag.SelectedDriverId = selectedDriverId;
            ViewBag.CurrentDriver = driver;

            Route? activeRoute = null;
            if (routeId.HasValue)
            {
                activeRoute = await _db.Routes
                    .Include(r => r.Stops)
                    .FirstOrDefaultAsync(r => r.Id == routeId.Value);
            }
            else if (driver != null && driver.Routes.Any())
            {
                activeRoute = driver.Routes.OrderByDescending(r => r.CreatedAt).FirstOrDefault();
            }
            else
            {
                activeRoute = await _db.Routes
                    .Include(r => r.Stops)
                    .OrderByDescending(r => r.CreatedAt)
                    .FirstOrDefaultAsync();
            }

            if (activeRoute != null)
            {
                activeRoute.Stops = activeRoute.Stops.OrderBy(s => s.SequenceOrder).ToList();
            }

            return View(activeRoute);
        }

        [HttpPost]
        public async Task<IActionResult> StartRoute(int routeId, int initialKm)
        {
            if (initialKm <= 0)
            {
                TempData["ErrorMessage"] = "El kilometraje inicial debe ser un número positivo mayor a cero.";
                return RedirectToAction("Index", new { routeId });
            }

            var route = await _db.Routes.FirstOrDefaultAsync(r => r.Id == routeId);
            if (route == null) return NotFound();

            route.InitialKm = initialKm;
            route.Status = "En Ruta";

            var driver = await _db.Drivers.FindAsync(route.DriverId);
            if (driver != null)
            {
                driver.Status = "En Ruta";
            }

            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = $"¡Jornada iniciada! Odómetro inicial registrado: {initialKm} km. Hoja de ruta desbloqueada.";
            return RedirectToAction("Index", new { driverId = route.DriverId, routeId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkDelivered(int stopId)
        {
            var stop = await _db.RouteStops.FindAsync(stopId);
            if (stop == null) return NotFound();

            stop.Status = "Entregado";
            stop.CancellationReason = null;

            var route = await _db.Routes.FindAsync(stop.RouteId);
            if (route != null && route.Status == "No iniciado")
            {
                route.Status = "En Ruta";
                var driver = await _db.Drivers.FindAsync(route.DriverId);
                if (driver != null) driver.Status = "En Ruta";
            }

            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Parada #{stop.SequenceOrder} marcada como Entregada.";
            return RedirectToAction("Index", new { driverId = route?.DriverId, routeId = stop.RouteId });
        }

        [HttpPost]
        public async Task<IActionResult> CompleteRoute(int routeId, int finalKm)
        {
            var route = await _db.Routes.Include(r => r.Stops).FirstOrDefaultAsync(r => r.Id == routeId);
            if (route == null) return NotFound();

            int initialKm = route.InitialKm ?? 0;

            if (finalKm < initialKm)
            {
                TempData["ErrorMessage"] = $"El kilometraje final ({finalKm} km) debe ser mayor o igual al kilometraje inicial ({initialKm} km).";
                return RedirectToAction("Index", new { driverId = route.DriverId, routeId });
            }

            route.FinalKm = finalKm;
            route.TotalDistanceKm = finalKm - initialKm;
            route.Status = "Finalizado";

            var driver = await _db.Drivers.FindAsync(route.DriverId);
            if (driver != null)
            {
                driver.Status = "Finalizado";
            }

            var log = new OdometerLog
            {
                RouteId = route.Id,
                DriverId = route.DriverId,
                InitialKm = initialKm,
                FinalKm = finalKm,
                Timestamp = DateTime.UtcNow
            };
            _db.OdometerLogs.Add(log);

            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = $"¡Jornada completada con éxito! Distancia total real recorrida: {route.TotalDistanceKm} km.";
            return RedirectToAction("Index", new { driverId = route.DriverId, routeId });
        }
    }
}
