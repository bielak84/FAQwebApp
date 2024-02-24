using FAQwebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FAQwebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Konstruktor kontrolera, przyjmuj�cy interfejs do logowania
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /Home/Index
        // Akcja zwracaj�ca widok strony g��wnej
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Privacy
        // Akcja zwracaj�ca widok strony z informacjami o prywatno�ci
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/Error
        // Akcja zwracaj�ca widok strony b��du
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Tworzenie obiektu ErrorViewModel z informacjami o b��dzie
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
