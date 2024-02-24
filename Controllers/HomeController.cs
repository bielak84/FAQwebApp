using FAQwebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FAQwebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Konstruktor kontrolera, przyjmuj¹cy interfejs do logowania
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /Home/Index
        // Akcja zwracaj¹ca widok strony g³ównej
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Privacy
        // Akcja zwracaj¹ca widok strony z informacjami o prywatnoœci
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: /Home/Error
        // Akcja zwracaj¹ca widok strony b³êdu
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Tworzenie obiektu ErrorViewModel z informacjami o b³êdzie
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
