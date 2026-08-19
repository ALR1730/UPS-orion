using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrionMVP.Data;
using OrionMVP.Models;
using OrionMVP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route = OrionMVP.Models.Route;

namespace OrionMVP.Controllers
{
    public class DriverProgressDto
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public string DriverStatus { get; set; } = "No iniciado";
        public string RouteName { get; set; } = string.Empty;
        public int TotalStops { get; set; }
        public int CompletedStops { get; set; }
        public int? InitialKm { get; set; }
        public int? FinalKm { get; set; }
        public double NetDistanceKm { get; set; }
        public double BaselineDistanceKm { get; set; }
        public double SavedKm { get; set; }
        public double SavingsPercentage { get; set; }
        public int ProgressPercentage => TotalStops > 0 ? (int)Math.Round((double)CompletedStops * 100 / TotalStops) : 0;
    }

    public class SupervisorReportViewModel
    {
        public List<Route> Routes { get; set; } = new();
        public List<DriverProgressDto> DriverMonitoring { get; set; } = new();
        public double TotalHistoricalKm { get; set; }
        public double TotalRealKm { get; set; }
        public double TotalSavedKm { get; set; }
        public double TotalFuelSavingsPercentage { get; set; }
        public double EstimatedFuelSavedLiters { get; set; }
    }

    public class SupervisorController : Controller
    {
        private readonly OrionDbContext _db;
        private readonly IDatabaseHealthService _dbHealthService;

        public SupervisorController(OrionDbContext db, IDatabaseHealthService dbHealthService)
        {
            _db = db;
            _dbHealthService = dbHealthService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.DbHealth = await _dbHealthService.GetHealthStatusAsync();

            var drivers = await _db.Drivers
                .Include(d => d.Routes)
                .ThenInclude(r => r.Stops)
                .ToListAsync();

            var monitoring = new List<DriverProgressDto>();
            foreach (var d in drivers)
            {
                var activeRoute = d.Routes.OrderByDescending(r => r.CreatedAt).FirstOrDefault();
                int total = activeRoute?.Stops.Count ?? 0;
                int completed = activeRoute?.Stops.Count(s => s.Status == "Entregado") ?? 0;

                int? initialKm = activeRoute?.InitialKm;
                int? finalKm = activeRoute?.FinalKm;
                double netDistance = (initialKm.HasValue && finalKm.HasValue) ? (finalKm.Value - initialKm.Value) : (activeRoute?.TotalDistanceKm ?? 0.0);
                double baselineKm = activeRoute?.BaselineDistanceKm ?? (total * 3.8);
                double savedKm = Math.Max(0, baselineKm - netDistance);
                double savingsPct = baselineKm > 0 ? Math.Round((savedKm / baselineKm) * 100.0, 1) : 0.0;

                monitoring.Add(new DriverProgressDto
                {
                    DriverId = d.Id,
                    DriverName = d.Name,
                    VehicleId = d.VehicleId,
                    DriverStatus = d.Status,
                    RouteName = activeRoute?.Name ?? "Sin Ruta Asignada",
                    TotalStops = total,
                    CompletedStops = completed,
                    InitialKm = initialKm,
                    FinalKm = finalKm,
                    NetDistanceKm = netDistance,
                    BaselineDistanceKm = baselineKm,
                    SavedKm = savedKm,
                    SavingsPercentage = savingsPct
                });
            }

            var routes = await _db.Routes
                .Include(r => r.Driver)
                .Include(r => r.Stops)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            var vm = new SupervisorReportViewModel
            {
                Routes = routes,
                DriverMonitoring = monitoring
            };

            foreach (var m in monitoring.Where(m => m.TotalStops > 0))
            {
                vm.TotalHistoricalKm += m.BaselineDistanceKm;
                vm.TotalRealKm += m.NetDistanceKm;
            }

            vm.TotalSavedKm = Math.Max(0, vm.TotalHistoricalKm - vm.TotalRealKm);
            vm.TotalFuelSavingsPercentage = vm.TotalHistoricalKm > 0 
                ? Math.Round((vm.TotalSavedKm / vm.TotalHistoricalKm) * 100.0, 1) 
                : 0.0;
            vm.EstimatedFuelSavedLiters = Math.Round(vm.TotalSavedKm * 0.12, 1);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ExportCsv()
        {
            var drivers = await _db.Drivers
                .Include(d => d.Routes)
                .ThenInclude(r => r.Stops)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Conductor,Vehiculo,EstadoJornada,RutaAsignada,ArticulosEntregados,TotalArticulos,OdometroInicialKm,OdometroFinalKm,DistanciaRealKm,DistanciaHistoricaKm,AhorroKm,AhorroPorcentaje");

            foreach (var d in drivers)
            {
                var r = d.Routes.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
                int total = r?.Stops.Count ?? 0;
                int completed = r?.Stops.Count(s => s.Status == "Entregado") ?? 0;
                int initial = r?.InitialKm ?? 0;
                int final = r?.FinalKm ?? 0;
                double realKm = (initial > 0 && final >= initial) ? (final - initial) : (r?.TotalDistanceKm ?? 0.0);
                double baseline = r?.BaselineDistanceKm ?? (total * 3.8);
                double saved = Math.Max(0, baseline - realKm);
                double pct = baseline > 0 ? Math.Round((saved / baseline) * 100.0, 1) : 0.0;

                sb.AppendLine($"\"{d.Name}\",\"{d.VehicleId}\",\"{d.Status}\",\"{r?.Name ?? "N/A"}\",{completed},{total},{initial},{final},{realKm},{baseline},{saved},{pct}%");
            }

            var preamble = Encoding.UTF8.GetPreamble();
            var contentBytes = Encoding.UTF8.GetBytes(sb.ToString());
            var bytes = new byte[preamble.Length + contentBytes.Length];
            Buffer.BlockCopy(preamble, 0, bytes, 0, preamble.Length);
            Buffer.BlockCopy(contentBytes, 0, bytes, preamble.Length, contentBytes.Length);

            return File(bytes, "text/csv; charset=utf-8", $"reporte_rendimiento_orion_{DateTime.Now:yyyyMMdd_HHmm}.csv");
        }
    }
}
