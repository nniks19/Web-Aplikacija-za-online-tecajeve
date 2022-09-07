using System.Runtime.CompilerServices;
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
    public class StatistikaTecajaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatistikaTecajaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index(StatistikaKreatoraVM statistikaKreatoraVM)
        {
            statistikaKreatoraVM = findStatistikaKreatoraVM(statistikaKreatoraVM);
            return View(statistikaKreatoraVM);
        }
        public StatistikaKreatoraVM findStatistikaKreatoraVM(StatistikaKreatoraVM statistikaKreatoraVM)
        {
            IEnumerable<Tecaj> objTecajlist = _unitOfWork.Tecaj.GetAll();
            objTecajlist = objTecajlist.Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            IEnumerable<Prijava_Na_Tecaj> objPrijavaList = _unitOfWork.PrijavaNaTecaj.GetAll();
            objPrijavaList = objPrijavaList.Where(x => objTecajlist.Any(y => y.Id == x.TecajId));
            IEnumerable<Ocjena_tecaja> objOcjenaTecajaList = _unitOfWork.OcjenaTecaja.GetAll();
            objOcjenaTecajaList = objOcjenaTecajaList.Where(x => objTecajlist.Any(y => y.Id == x.TecajId));
            IEnumerable<Videozapis> objVideozapisList = _unitOfWork.Videozapis.GetAll();
            objVideozapisList = objVideozapisList.Where(x => objTecajlist.Any(y => y.Id == x.TecajId));
            statistikaKreatoraVM.brPrijava = objPrijavaList.Count();
            statistikaKreatoraVM.brOcjenaTecaja = objOcjenaTecajaList.Count();
            statistikaKreatoraVM.brTecaja = objTecajlist.Count();
            statistikaKreatoraVM.brVideozapisa = objVideozapisList.Count();
            var listOsoba = _unitOfWork.Osoba.GetAll().Where(x => objPrijavaList.Any(y => x.Oib == y.OsobaOib && y.Status_PrijaveId == 1));
            statistikaKreatoraVM.OsobaList = listOsoba.Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib,
            });
            statistikaKreatoraVM.TecajList = objTecajlist.Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            return statistikaKreatoraVM;
        }
        public IActionResult StatistikaKorisnik(StatistikaKreatoraVM statistikaKreatoraVM)
        {
            statistikaKreatoraVM = findStatistikaKreatoraVM(statistikaKreatoraVM);
            if (statistikaKreatoraVM.OsobaOib != null)
            {
                var objTecajlist = _unitOfWork.Tecaj.GetAll().Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
                statistikaKreatoraVM = findStatistikaKreatoraVM(statistikaKreatoraVM);
                statistikaKreatoraVM.brKlikovaNaTecaju = _unitOfWork.PracenjeKorisnika.GetAll().Where(x => x.OsobaOib == statistikaKreatoraVM.OsobaOib && objTecajlist.Any(y=>y.Id == x.TecajId)).Sum(x => x.brPosjeta);
                statistikaKreatoraVM.brKlikovaNaVideozapise = _unitOfWork.PracenjeKorisnika.GetAll().Where(x => x.OsobaOib == statistikaKreatoraVM.OsobaOib && objTecajlist.Any(y => y.Id == x.TecajId)).Sum(x => x.brPokretanjaVideozapisa);
                statistikaKreatoraVM.brTecajeva = _unitOfWork.PrijavaNaTecaj.GetAll().Where(x => x.OsobaOib == statistikaKreatoraVM.OsobaOib && x.Status_PrijaveId == 1 && objTecajlist.Any(y => y.Id == x.TecajId)).Count();
                statistikaKreatoraVM.brPrijava = _unitOfWork.PrijavaNaTecaj.GetAll().Where(x => x.OsobaOib == statistikaKreatoraVM.OsobaOib && objTecajlist.Any(y => y.Id == x.TecajId)).Count();
                statistikaKreatoraVM.brOcjenaTecaja = _unitOfWork.OcjenaTecaja.GetAll().Where(x => x.OsobaOib == statistikaKreatoraVM.OsobaOib && objTecajlist.Any(y => y.Id == x.TecajId)).Count();
                var _aktivnosti = _unitOfWork.PracenjeKorisnika.GetAll().Where(x => objTecajlist.Any(y => y.Id == x.TecajId) && x.OsobaOib == statistikaKreatoraVM.OsobaOib);
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddMilliseconds(_aktivnosti.Max(x => x.Datum_posjete)*1000).ToLocalTime();
                statistikaKreatoraVM.posljednjaAktivnost = dtDateTime.ToString();
                var najvecibroj = _unitOfWork.PracenjeKorisnika.GetAll().Where(x => x.OsobaOib == statistikaKreatoraVM.OsobaOib && objTecajlist.Any(y => y.Id == x.TecajId)).Max(x=>x.brPosjeta);
                statistikaKreatoraVM.najvisePosjeta = najvecibroj;
                var najvecibr = _unitOfWork.PracenjeKorisnika.GetAll().Where(x => x.OsobaOib == statistikaKreatoraVM.OsobaOib && objTecajlist.Any(y => y.Id == x.TecajId)).Max(x => x.brPokretanjaVideozapisa);
                statistikaKreatoraVM.najvisePregleda = najvecibr;
                return View(statistikaKreatoraVM);
            }
            else
            { 
                TempData["error"] = "Korisnik tečaja mora biti odabran!";
                return View("Index",statistikaKreatoraVM);
            }
        }
        public IActionResult StatistikaTecaj(StatistikaKreatoraVM statistikaKreatoraVM)
        {
            statistikaKreatoraVM = findStatistikaKreatoraVM(statistikaKreatoraVM);
            if (statistikaKreatoraVM.TecajId != null)
            {
                statistikaKreatoraVM = findStatistikaKreatoraVM(statistikaKreatoraVM);
                statistikaKreatoraVM.brKlikovaNaTecaju = _unitOfWork.PracenjeKorisnika.GetAll().Where(x=>x.TecajId == statistikaKreatoraVM.TecajId).Sum(x=>x.brPosjeta);
                statistikaKreatoraVM.brKlikovaNaVideozapise = _unitOfWork.PracenjeKorisnika.GetAll().Where(x => x.TecajId == statistikaKreatoraVM.TecajId).Sum(x=>x.brPokretanjaVideozapisa);
                statistikaKreatoraVM.brKorisnika = _unitOfWork.PrijavaNaTecaj.GetAll().Where(x => x.TecajId == statistikaKreatoraVM.TecajId && x.Status_PrijaveId == 1).Count();
                statistikaKreatoraVM.brOdobrenihPrijava = _unitOfWork.PrijavaNaTecaj.GetAll().Where(x => x.TecajId == statistikaKreatoraVM.TecajId && x.Status_PrijaveId == 1).Count();
                statistikaKreatoraVM.prosjecnaOcjena = _unitOfWork.Tecaj.GetAll().Where(x => x.Id == statistikaKreatoraVM.TecajId).First().prosjecna_ocjena;
                statistikaKreatoraVM.brCjelina = _unitOfWork.CjelinaTecaja.GetAll().Where(x => x.TecajId == statistikaKreatoraVM.TecajId).Count();
                return View(statistikaKreatoraVM);
            }
            else
            {
                TempData["error"] = "Tečaj mora biti odabran!";
                return View("Index", statistikaKreatoraVM);
            }
        }
    }
}
