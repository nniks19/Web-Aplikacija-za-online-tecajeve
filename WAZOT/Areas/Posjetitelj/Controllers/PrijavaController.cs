using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Services;

namespace WAZOT.Controllers
{
    [Area("Posjetitelj")]

    public class PrijavaController : Controller
    {
        private OsobaService osobaService;
        private readonly IUnitOfWork _unitOfWork;
        public PrijavaController(IUnitOfWork unitOfWork, OsobaService _osobaService)
        {
            _unitOfWork = unitOfWork;
            osobaService = _osobaService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ResetPassword()
        {
            Osoba osoba = new Osoba();
            return View(osoba);
        }


        public IActionResult Login()
        {
            Osoba osoba = new Osoba();
            return View(osoba);
        }


        [HttpPost]
        public IActionResult ResetPassword(string email, string pin)
        {
            var account = _unitOfWork.Osoba.GetAll().Where(x => x.email == email && x.pin == pin).FirstOrDefault();
                
            if (account == null)
            {
                TempData["error"] = "Upisali ste pogrešan PIN ili Email!";
                return View("Index");
            }
            if (account != null)
            {
                account.lozinka = null;
                return RedirectToAction("ResetConfirm", new {oib= account.Oib});
            }
            return View("Login");
        }

        public IActionResult ResetConfirm(string oib)
        {
            var account = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == oib).FirstOrDefault();
            return View(account);
        }
        [HttpPost]
        public IActionResult ResetConfirm(Osoba oOsoba)
        {
            if(oOsoba.lozinka != null)
            {
                var account = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == oOsoba.Oib).FirstOrDefault();
                account.lozinka = oOsoba.lozinka;
                _unitOfWork.Osoba.Update(account);
                _unitOfWork.Save();
                TempData["success"] = "Uspješno ste resetirali lozinku!";
                return RedirectToAction("index");
            }
            else
            {
                TempData["error"] = "Polje za unos lozinke je obavezno!";
                return RedirectToAction("index");
            }
            return View(oOsoba);
        }
        [HttpPost]
        public IActionResult Login(string email, string lozinka, string pin)
        {
            var account = osobaService.Login(email, lozinka, pin);
            if (account != null && account.odobreno == 1)
            {
                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetString("oib", account.Oib);
                HttpContext.Session.SetString("razina_prava", account.Razina_PravaId.ToString());
                HttpContext.Session.SetString("ime", account.ime);
                HttpContext.Session.SetString("prezime", account.prezime);
                if (account.Razina_PravaId == 1)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Administrator" });
                }
                if (account.Razina_PravaId == 2)
                {
                    return RedirectToAction("Index", "HomeKorisnik", new { area = "Korisnik" });

                }
                if (account.Razina_PravaId == 3)
                {
                    return RedirectToAction("Index", "HomeKreatorTecaja", new { area = "Kreator_Tecaja" });
                }
            }
            if (account != null && account.odobreno == 0)
            {
                ViewBag.PrijavaMsg = "Vaš korisnički račun još nije odobren od strane administratora!";
                return View("Login");
            }
            ViewBag.PrijavaMsg = "Neispravan pin, email ili lozinka!";
            return View("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("razina_prava");
            HttpContext.Session.Remove("ime");
            HttpContext.Session.Remove("prezime");
            HttpContext.Session.Remove("oib");
            return RedirectToAction("Index", "", new { area = "Posjetitelj" });
        }
    }
}