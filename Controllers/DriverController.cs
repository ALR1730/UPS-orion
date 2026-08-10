using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrionMVP.Data;
using OrionMVP.Models;
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
    }
}
