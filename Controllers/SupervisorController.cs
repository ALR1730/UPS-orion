using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrionMVP.Data;
using OrionMVP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Route = OrionMVP.Models.Route;

namespace OrionMVP.Controllers
{
    public class SupervisorReportViewModel
    {
        public List<Route> Routes { get; set; } = new();
        public double TotalHistoricalKm { get; set; }
        public double TotalRealKm { get; set; }
        public double TotalSavedKm { get; set; }
        public double TotalFuelSavingsPercentage { get; set; }
        public double EstimatedFuelSavedLiters { get; set; }
    }

    public class SupervisorController : Controller
    {
        private readonly OrionDbContext _db;

        public SupervisorController(OrionDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var routes = await _db.Routes
                .Include(r => r.Driver)
                .Include(r => r.Stops)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            // Ensure baseline distance is populated for demo if 0
            foreach (var r in routes)
            {
                if (r.BaselineDistanceKm <= 0 && r.Stops.Count > 0)
                {
                    r.BaselineDistanceKm = Math.Round(r.Stops.Count * 4.5, 1);
                }
            }

            var vm = new SupervisorReportViewModel
            {
                Routes = routes
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
            vm.EstimatedFuelSavedLiters = Math.Round(vm.TotalSavedKm * 0.12, 1); // 12 liters / 100km

            return View(vm);
        }
    }
}
