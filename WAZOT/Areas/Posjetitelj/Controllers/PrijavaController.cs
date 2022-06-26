using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.Models;

namespace WAZOT.Controllers
{
    [Area("Posjetitelj")]

    public class PrijavaController : Controller
    {
        private readonly ILogger<PrijavaController> _logger;

        public PrijavaController(ILogger<PrijavaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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