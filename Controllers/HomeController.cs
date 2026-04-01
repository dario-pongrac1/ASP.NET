using Microsoft.AspNetCore.Mvc;

namespace lab_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Problem("Doslo je do neocekivane greske.");
        }
    }
}
