using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.Models;

namespace WAZOT.Controllers
{
    [Area("Posjetitelj")]

    public class HomePosjetiteljController : Controller
    {
        private readonly ILogger<HomePosjetiteljController> _logger;

        public HomePosjetiteljController(ILogger<HomePosjetiteljController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ONama()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}