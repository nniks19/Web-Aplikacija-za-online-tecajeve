using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.Models;
using WAZOT.Services;

namespace WAZOT.Controllers
{
    [Area("Posjetitelj")]

    public class PrijavaController : Controller
    {
        private OsobaService osobaService;
        public PrijavaController(OsobaService _osobaService)
        {
            osobaService = _osobaService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            Osoba osoba = new Osoba();
            return View(osoba);
        }
        [HttpPost]
        public IActionResult Login(string email, string lozinka)
        {
            var account = osobaService.Login(email, lozinka);
            if (account != null)
            {
                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("razina_prava", account.Razina_PravaId.ToString());
                HttpContext.Session.SetString("ime", account.ime);
                HttpContext.Session.SetString("prezime", account.prezime);
                if (account.Razina_PravaId == 1)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Administrator" });
                }
                if (account.Razina_PravaId == 2)
                {
                    return RedirectToAction("Index", "Korisnik", new { area = "Korisnik" });

                }
                if (account.Razina_PravaId == 3)
                {
                    return RedirectToAction("Index", "Index", new { area = "Kreator_Tecaja" });
                }
            }
            ViewBag.msgaaa = "Neispravan email ili lozinka!";
            return View("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("razina_prava");
            HttpContext.Session.Remove("ime");
            HttpContext.Session.Remove("prezime");
            return RedirectToAction("Index", "", new { area = "Posjetitelj" });
        }
    }
}