using Microsoft.AspNetCore.Mvc;
using ObligatorioProgramacion3.Models;
using System.Diagnostics;

namespace ObligatorioProgramacion3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor del controlador que inyecta el logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción para la página principal
        public IActionResult Index()
        {
            return View();
        }

        // Acción para la página de privacidad
        public IActionResult Privacy()
        {
            return View();
        }

        // Acción para manejar y mostrar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
