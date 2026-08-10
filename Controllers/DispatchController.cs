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

        public DispatchController(OrionDbContext db, IAddressImportService importService)
        {
            _db = db;
            _importService = importService;
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

            // Create new Route in DB
            var driver = await _db.Drivers.FirstOrDefaultAsync(d => d.Id == (driverId ?? 1));
            var newRoute = new Route
            {
                Name = $"Ruta {DateTime.Now:yyyy-MM-dd HH:mm} - {result.TotalRead} paradas",
                Date = DateTime.UtcNow,
                DriverId = driver?.Id ?? 1,
                Status = "No iniciado",
                BaselineDistanceKm = Math.Round(result.TotalRead * 4.2, 1), // Estimate
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

            TempData["SuccessMessage"] = $"¡Éxito! Se leyeron y cargaron {result.TotalRead} direcciones correctamente en la base de datos.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DownloadSampleCsv()
        {
            var csv = "Calle,Altura,Ciudad,Cliente\n" +
                      "Av. Corrientes,1234,Buenos Aires,Juan Perez\n" +
                      "Av. Santa Fe,2500,Buenos Aires,Maria Gomez\n" +
                      "Calle Florida,450,Buenos Aires,Tech Corp\n" +
                      "Av. Cabildo,3000,Buenos Aires,Distribuidora Sur\n" +
                      "Av. Rivadavia,5400,Buenos Aires,Lucia Fernandez\n";

            var bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "Plantilla_Direcciones_UPS.csv");
        }
    }
}
