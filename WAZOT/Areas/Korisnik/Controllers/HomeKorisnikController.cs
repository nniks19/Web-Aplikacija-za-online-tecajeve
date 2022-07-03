using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;

namespace WAZOT.Controllers
{
    [Area("Korisnik")]

    public class HomeKorisnikController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeKorisnikController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(StatistikaKorisnikaVM statistikaKorisnikaVM)
        {
            IEnumerable<Narudzba> objNarudzbaList = _unitOfWork.Narudzba.GetAll().Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            IEnumerable<Tecaj> objTecajlist = _unitOfWork.Tecaj.GetAll().Where(x => objNarudzbaList.Any(y => y.TecajId == x.Id && y.Status_NarudzbeId == 1));
            IEnumerable<Ocjena_tecaja> objOcjenaTecajaList = _unitOfWork.OcjenaTecaja.GetAll().Where(x=>x.OsobaOib == HttpContext.Session.GetString("oib"));
            statistikaKorisnikaVM.brNarudzbi = objNarudzbaList.Count();
            statistikaKorisnikaVM.brOcjenaTecaja = objOcjenaTecajaList.Count();
            statistikaKorisnikaVM.brTecaja = objTecajlist.Count();
            ViewBag.ime = HttpContext.Session.GetString("ime");
            ViewBag.prezime = HttpContext.Session.GetString("prezime");
            return View(statistikaKorisnikaVM);
        }
    }
}