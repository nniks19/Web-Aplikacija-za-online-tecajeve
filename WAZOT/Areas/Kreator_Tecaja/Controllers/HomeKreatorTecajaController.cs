using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Kreator_Tecaja")]
    public class HomeKreatorTecajaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeKreatorTecajaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index(StatistikaKreatoraVM statistikaKreatoraVM)
        {
            IEnumerable<Tecaj> objTecajlist = _unitOfWork.Tecaj.GetAll();
            objTecajlist = objTecajlist.Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            IEnumerable<Narudzba> objNarudzbaList = _unitOfWork.Narudzba.GetAll();
            objNarudzbaList = objNarudzbaList.Where(x => objTecajlist.Any(y => y.Id == x.TecajId));
            IEnumerable<Ocjena_tecaja> objOcjenaTecajaList = _unitOfWork.OcjenaTecaja.GetAll();
            objOcjenaTecajaList = objOcjenaTecajaList.Where(x => objTecajlist.Any(y => y.Id == x.TecajId));
            IEnumerable<Videozapis> objVideozapisList = _unitOfWork.Videozapis.GetAll();
            objVideozapisList = objVideozapisList.Where(x => objTecajlist.Any(y => y.Id == x.TecajId));
            statistikaKreatoraVM.brNarudzbi = objNarudzbaList.Count();
            statistikaKreatoraVM.brOcjenaTecaja = objOcjenaTecajaList.Count();
            statistikaKreatoraVM.brTecaja = objTecajlist.Count();
            statistikaKreatoraVM.brVideozapisa = objVideozapisList.Count();
            ViewBag.ime = HttpContext.Session.GetString("ime");
            ViewBag.prezime = HttpContext.Session.GetString("prezime");
            return View(statistikaKreatoraVM);
        }

    }
}
