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
        public async Task<IActionResult> SaveOdometer(int routeId, int initialKm, int finalKm)
        {
            if (initialKm <= 0 || finalKm <= 0)
            {
                TempData["ErrorMessage"] = "Los kilómetros inicial y final deben ser números enteros positivos mayores a cero.";
                return RedirectToAction("Index", new { routeId });
            }

            if (finalKm < initialKm)
            {
                TempData["ErrorMessage"] = "El KM Final debe ser mayor o igual al KM Inicial.";
                return RedirectToAction("Index", new { routeId });
            }

            var route = await _db.Routes.FirstOrDefaultAsync(r => r.Id == routeId);
            if (route == null)
            {
                return NotFound();
            }

            route.InitialKm = initialKm;
            route.FinalKm = finalKm;
            route.Status = "Finalizado";

            var log = new OdometerLog
            {
                RouteId = route.Id,
                DriverId = route.DriverId,
                InitialKm = initialKm,
                FinalKm = finalKm,
                Timestamp = DateTime.UtcNow
            };

            _db.OdometerLogs.Add(log);

            // Update driver status
            var driver = await _db.Drivers.FindAsync(route.DriverId);
            if (driver != null)
            {
                driver.Status = "Finalizado";
            }

            await _db.SaveChangesAsync();

            int realKm = finalKm - initialKm;
            TempData["SuccessMessage"] = $"Odómetro registrado con éxito. Distancia real recorrida: {realKm} KM.";
            return RedirectToAction("Index", new { routeId });
        }
    }
}
