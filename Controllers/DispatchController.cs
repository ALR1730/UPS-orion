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
        private readonly IRouteOptimizerService _optimizerService;

        public DispatchController(
            OrionDbContext db, 
            IAddressImportService importService, 
            IRouteOptimizerService optimizerService)
        {
            _db = db;
            _importService = importService;
            _optimizerService = optimizerService;
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
                Name = $"Ruta Piloto - {result.TotalRead} artículos ({DateTime.Now:dd/MM/yyyy HH:mm})",
                Date = DateTime.UtcNow,
                DriverId = driver?.Id ?? 1,
                Status = "No iniciado",
                BaselineDistanceKm = Math.Round(result.TotalRead * 3.8, 1),
                CreatedAt = DateTime.UtcNow
            };

            int order = 1;
            foreach (var rec in result.Records)
            {
                newRoute.Stops.Add(new RouteStop
                {
                    ArticleName = rec.ArticleName,
                    CustomerName = rec.CustomerName,
                    Address = rec.Address,
                    Street = rec.Address,
                    Latitude = rec.Latitude,
                    Longitude = rec.Longitude,
                    SequenceOrder = order++,
                    Sequence = order - 1,
                    Status = "Pendiente",
                    IsGeocoded = true,
                    ExternalNavUrl = $"https://www.google.com/maps/search/?api=1&query={rec.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)},{rec.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}"
                });
            }

            _db.Routes.Add(newRoute);
            await _db.SaveChangesAsync();

            // Execute Linear Proximity Optimization (HU03)
            var optResult = await _optimizerService.OptimizeRouteAsync(newRoute.Id);

            TempData["SuccessMessage"] = $"¡Carga masiva exitosa! Se registraron {result.TotalRead} artículos. {optResult.Message}";
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
        public async Task<IActionResult> OptimizeSequence(int routeId)
        {
            var optResult = await _optimizerService.OptimizeRouteAsync(routeId);
            if (optResult.IsSuccess)
            {
                TempData["SuccessMessage"] = optResult.Message;
                TempData["BenchmarkTime"] = $"{optResult.ExecutionTimeMs} ms";
            }
            else
            {
                TempData["ErrorMessage"] = optResult.Message;
            }

            return RedirectToAction("RouteDetails", new { id = routeId });
        }

        [HttpGet]
        public IActionResult DownloadSampleCsv()
        {
            var csv = "Articulo,Cliente,Direccion,Latitud,Longitud\r\n" +
                      "Paquete Electrónica 3kg,Juan Pérez,Av. Winston Churchill 1099,18.4712,-69.9405\r\n" +
                      "Caja Documentos Legales,María Gómez,Av. 27 de Febrero 450,18.4764,-69.9281\r\n" +
                      "Monitor LED 27 pulg,Tech Solutions SRL,Av. John F. Kennedy 800,18.4893,-69.9356\r\n" +
                      "Suministros Médicos,Farmacia Central,Av. Abraham Lincoln 702,18.4735,-69.9324\r\n" +
                      "Ropa y Calzado Deportivo,Carlos Mendoza,Calle El Conde 310,18.4731,-69.8864\r\n" +
                      "Herramientas Eléctricas,Ferretería El Progreso,Av. San Vicente de Paúl 12,18.5023,-69.8512\r\n" +
                      "Componentes de Red,DataNet Dominicana,Av. Lope de Vega 29,18.4751,-69.9348\r\n" +
                      "Repuestos Automotrices,Taller Rodríguez,Av. Máximo Gómez 144,18.4811,-69.9123\r\n" +
                      "Libros Universitarios,Librería Cuesta,Av. 27 de Febrero esq. Lincoln,18.4758,-69.9311\r\n" +
                      "Insumos de Impresión,Imprenta Moderna,Calle Roberto Pastoriza 214,18.4729,-69.9362\r\n" +
                      "Electrodomésticos Pequeños,Distribuidora Corripio,Av. John F. Kennedy km 6.5,18.4912,-69.9451\r\n" +
                      "Cosméticos y Cuidado,Salón Belleza VIP,Av. Sarasota 45,18.4552,-69.9482\r\n" +
                      "Muestras de Laboratorio,Laboratorio Referencia,Av. Luperón 100,18.4612,-69.9721\r\n" +
                      "Alimentos No Perecederos,Supermercado Bravo,Av. Enriquillo 88,18.4489,-69.9615\r\n" +
                      "Artículos de Oficina,Oficinas Corporativas Torre Piantini,Av. Gustavo Mejía Ricart 102,18.4745,-69.9388\r\n";

            var preamble = Encoding.UTF8.GetPreamble();
            var contentBytes = Encoding.UTF8.GetBytes(csv);
            var bytes = new byte[preamble.Length + contentBytes.Length];
            Buffer.BlockCopy(preamble, 0, bytes, 0, preamble.Length);
            Buffer.BlockCopy(contentBytes, 0, bytes, preamble.Length, contentBytes.Length);

            return File(bytes, "text/csv; charset=utf-8", "Plantilla_Articulos_ORION_15_Paradas.csv");
        }
    }
}
