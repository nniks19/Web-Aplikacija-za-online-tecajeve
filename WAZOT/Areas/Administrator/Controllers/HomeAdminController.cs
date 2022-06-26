using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;

namespace WAZOT.Controllers
{
    [Area("Administrator")]

    public class HomeAdminController : Controller
    {
        private readonly ILogger<HomeAdminController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeAdminController(ILogger<HomeAdminController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(StatistikaVM StatistikaVM)
        {
            IEnumerable<Nacin_placanja> objNacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll();
            IEnumerable<Narudzba> objNarudzbaList = _unitOfWork.Narudzba.GetAll();
            IEnumerable<Ocjena_tecaja> objOcjenaTecajaList = _unitOfWork.OcjenaTecaja.GetAll();
            IEnumerable<Osoba> objOsobaList = _unitOfWork.Osoba.GetAll();
            IEnumerable<Razina_Prava> objRazinaPravaList = _unitOfWork.RazinaPrava.GetAll();
            IEnumerable<Status_narudzbe> objStatusNarudzbeList = _unitOfWork.StatusNarudzbe.GetAll();
            IEnumerable<Tecaj> objTecajlist = _unitOfWork.Tecaj.GetAll();
            IEnumerable<Videozapis> objVideozapisList = _unitOfWork.Videozapis.GetAll();
            StatistikaVM.brNacinaPlacanja = objNacinPlacanjaList.Count();
            StatistikaVM.brNarudzbi = objNarudzbaList.Count();
            StatistikaVM.brOcjenaTecaja = objOcjenaTecajaList.Count();
            StatistikaVM.brStatusaNarudzbi = objStatusNarudzbeList.Count();
            StatistikaVM.brOsoba = objOsobaList.Count();
            StatistikaVM.brRazinePrava = objRazinaPravaList.Count();
            StatistikaVM.brVideozapisa = objVideozapisList.Count();
            StatistikaVM.brTecaja = objTecajlist.Count();
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