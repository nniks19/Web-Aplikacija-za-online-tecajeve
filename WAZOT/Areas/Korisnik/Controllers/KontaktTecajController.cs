using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Korisnik")]
    public class KontaktTecajController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public KontaktTecajController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult SlanjePoruke(string oibtecaj, string oibkorisnik)
        {
            PorukaVM porukaVM = new PorukaVM();
            porukaVM.Razgovor = new Razgovor();
            porukaVM.Poruka = new Poruka();
            porukaVM.Razgovor.PosiljateljOsobaOib = oibkorisnik;
            porukaVM.Razgovor.PrimateljOsobaOib = oibtecaj;
            porukaVM.Poruka.PosiljateljOsobaOib = oibkorisnik;
            return View(porukaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult SlanjePoruke(PorukaVM obj)
        {
            var listaRazgovora = _unitOfWork.Razgovor.GetAll().Where(x=>x.PosiljateljOsobaOib == obj.Razgovor.PosiljateljOsobaOib || x.PrimateljOsobaOib == obj.Razgovor.PrimateljOsobaOib);
            if (listaRazgovora.Count() == 0)
            {
                _unitOfWork.Razgovor.Add(obj.Razgovor);
                _unitOfWork.Save();
                int lastRazgovorId = _unitOfWork.Razgovor.Max();
                obj.Poruka.RazgovorId = lastRazgovorId;
                obj.Poruka.Datum_slanja = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                obj.Poruka.PosiljateljOsobaOib = obj.Razgovor.PosiljateljOsobaOib;
                _unitOfWork.Poruka.Add(obj.Poruka);
                _unitOfWork.Save();
                TempData["success"] = "Uspješno ste poslali poruku!";
                return RedirectToAction("Index", "RazgovorKorisnik", new { area = "Korisnik" });
            }
            else
            {
                TempData["error"] = "Razgovor već postoji!";
                return RedirectToAction("Index", "RazgovorKorisnik", new { area = "Korisnik" });
            }
        }
    }
}
