using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Services;

namespace WAZOT.Controllers
{
    [Area("Administrator")]

    public class HomeAdminController : Controller
    {
        private readonly ILogger<HomeAdminController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private OsobaService _osobaService;


        public HomeAdminController(ILogger<HomeAdminController> logger, IUnitOfWork unitOfWork, OsobaService osobaService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _osobaService = osobaService;
        }

        public IActionResult Index(StatistikaVM StatistikaVM)
        {
            IEnumerable<Prijava_Na_Tecaj> objPrijavaNaTecajList = _unitOfWork.PrijavaNaTecaj.GetAll();
            IEnumerable<Ocjena_tecaja> objOcjenaTecajaList = _unitOfWork.OcjenaTecaja.GetAll();
            IEnumerable<Osoba> objOsobaList = _unitOfWork.Osoba.GetAll();
            IEnumerable<Razina_Prava> objRazinaPravaList = _unitOfWork.RazinaPrava.GetAll();
            IEnumerable<Status_prijave> objStatusPrijaveList = _unitOfWork.StatusPrijave.GetAll();
            IEnumerable<Tecaj> objTecajlist = _unitOfWork.Tecaj.GetAll();
            IEnumerable<Videozapis> objVideozapisList = _unitOfWork.Videozapis.GetAll();
            IEnumerable<Kategorija> objKategorijaList = _unitOfWork.Kategorija.GetAll();
            StatistikaVM.brPrijava = objPrijavaNaTecajList.Count();
            StatistikaVM.brOcjenaTecaja = objOcjenaTecajaList.Count();
            StatistikaVM.brStatusaPrijava = objStatusPrijaveList.Count();
            StatistikaVM.brOsoba = objOsobaList.Count();
            StatistikaVM.brRazinePrava = objRazinaPravaList.Count();
            StatistikaVM.brVideozapisa = objVideozapisList.Count();
            StatistikaVM.brTecaja = objTecajlist.Count();
            StatistikaVM.brKategorija = objKategorijaList.Count();
            ViewBag.ime = HttpContext.Session.GetString("ime");
            ViewBag.prezime = HttpContext.Session.GetString("prezime");
            return View(StatistikaVM);
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