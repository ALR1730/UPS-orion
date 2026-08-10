using Microsoft.AspNetCore.Mvc;
using OrionMVP.Data;

namespace OrionMVP.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly OrionDbContext _db;

        public SupervisorController(OrionDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
