using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.Models;

namespace WAZOT.Controllers
{
    [Area("Korisnik")]

    public class HomeKorisnikController : Controller
    {
        private readonly ILogger<HomeKorisnikController> _logger;

        public HomeKorisnikController(ILogger<HomeKorisnikController> logger)
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