using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrionMVP.Data;
using OrionMVP.Models;
using OrionMVP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
                int completed = activeRoute?.Stops.Count(s => s.Status == "Entregado" || s.Status == "No entregado") ?? 0;

                monitoring.Add(new DriverProgressDto
                {
                    DriverId = d.Id,
                    DriverName = d.Name,
                    VehicleId = d.VehicleId,
                    DriverStatus = d.Status,
                    RouteName = activeRoute?.Name ?? "Sin Ruta Asignada",
                    TotalStops = total,
                    CompletedStops = completed
                });
            }

            var routes = await _db.Routes
                .Include(r => r.Driver)
                .Include(r => r.Stops)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            foreach (var r in routes)
            {
                if (r.BaselineDistanceKm <= 0 && r.Stops.Count > 0)
                {
                    r.BaselineDistanceKm = Math.Round(r.Stops.Count * 4.5, 1);
                }
            }

            var vm = new SupervisorReportViewModel
            {
                Routes = routes,
                DriverMonitoring = monitoring
            };

            foreach (var r in routes)
            {
                double realKm = r.InitialKm.HasValue && r.FinalKm.HasValue 
                    ? (r.FinalKm.Value - r.InitialKm.Value) 
                    : (r.TotalDistanceKm > 0 ? r.TotalDistanceKm : r.Stops.Count * 3.2);

                vm.TotalHistoricalKm += r.BaselineDistanceKm;
                vm.TotalRealKm += realKm;
            }

            vm.TotalSavedKm = Math.Max(0, vm.TotalHistoricalKm - vm.TotalRealKm);
            vm.TotalFuelSavingsPercentage = vm.TotalHistoricalKm > 0 
                ? Math.Round((vm.TotalSavedKm / vm.TotalHistoricalKm) * 100.0, 1) 
                : 0.0;
            vm.EstimatedFuelSavedLiters = Math.Round(vm.TotalSavedKm * 0.12, 1);

            return View(vm);
        }
    }
}
