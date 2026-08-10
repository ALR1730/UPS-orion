using Microsoft.AspNetCore.Mvc;
using OrionMVP.Data;

namespace OrionMVP.Controllers
{
    public class DispatchController : Controller
    {
        private readonly OrionDbContext _db;

        public DispatchController(OrionDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
