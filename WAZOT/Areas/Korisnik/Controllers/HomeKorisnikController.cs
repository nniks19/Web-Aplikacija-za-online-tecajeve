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
            IEnumerable<Prijava_Na_Tecaj> objPrijavaList = _unitOfWork.PrijavaNaTecaj.GetAll().Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            IEnumerable<Tecaj> objTecajlist = _unitOfWork.Tecaj.GetAll().Where(x => objPrijavaList.Any(y => y.TecajId == x.Id && y.Status_PrijaveId == 1));
            IEnumerable<Ocjena_tecaja> objOcjenaTecajaList = _unitOfWork.OcjenaTecaja.GetAll().Where(x=>x.OsobaOib == HttpContext.Session.GetString("oib"));
            statistikaKorisnikaVM.brPrijava = objPrijavaList.Count();
            statistikaKorisnikaVM.brOcjenaTecaja = objOcjenaTecajaList.Count();
            statistikaKorisnikaVM.brTecaja = objTecajlist.Count();
            ViewBag.ime = HttpContext.Session.GetString("ime");
            ViewBag.prezime = HttpContext.Session.GetString("prezime");
            return View(statistikaKorisnikaVM);
        }
    }
}