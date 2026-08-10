using Microsoft.AspNetCore.Mvc;
using OrionMVP.Data;

namespace OrionMVP.Controllers
{
    public class DriverController : Controller
    {
        private readonly OrionDbContext _db;

        public DriverController(OrionDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
