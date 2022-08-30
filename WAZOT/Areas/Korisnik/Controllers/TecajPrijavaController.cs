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
    public class TecajPrijavaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TecajPrijavaController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Preview(string? id)
        {
            Tecaj oTecaj = _unitOfWork.Tecaj.GetFirstOrDefault(u => u.Id == Convert.ToInt32(id));
            IEnumerable<Videozapis> videozapisList = _unitOfWork.Videozapis.GetAll().Where(x => x.TecajId == oTecaj.Id);
            IEnumerable<Ocjena_tecaja> ocjenetecajaList = _unitOfWork.OcjenaTecaja.GetAll(includeProperties: "Osoba").Where(x => x.TecajId == oTecaj.Id);
            TecajPreviewVM tecajPreviewVM = new TecajPreviewVM()
            {
                Tecaj = oTecaj,
                VideozapisList = videozapisList,
                OcjenaTecajaList = ocjenetecajaList,
                OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == oTecaj.OsobaOib).Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib
                }),
                KategorijaList = _unitOfWork.Kategorija.GetAll().Where(x => x.Id == oTecaj.KategorijaId).Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString()
                })
            };
            if (tecajPreviewVM.Tecaj == null)
            {
                return NotFound();
            }
            return View(tecajPreviewVM);
        }
        //GET
        public IActionResult Create(int? id)
        {
            PrijavaNaTecajVM prijavaNaTecajVM = new PrijavaNaTecajVM()
            {
                PrijavaNaTecaj = new(),
            };
            prijavaNaTecajVM.PrijavaNaTecaj.Status_PrijaveId = 2;
            prijavaNaTecajVM.PrijavaNaTecaj.OsobaOib = HttpContext.Session.GetString("oib");
            prijavaNaTecajVM.PrijavaNaTecaj.TecajId = id;
            return View(prijavaNaTecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(PrijavaNaTecajVM obj, int? id)
        {
            if (obj.PrijavaNaTecaj == null)
            {
                ViewBag.notfilled = "Nešto je pošlo po zlu.";
            }
            if (obj.PrijavaNaTecaj != null & ModelState.IsValid)
            {
                obj.PrijavaNaTecaj.datum_pocetak = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                _unitOfWork.PrijavaNaTecaj.Add(obj.PrijavaNaTecaj);
                _unitOfWork.Save();
                TempData["success"] = "Uspješno ste se prijavili na tečaj!";
                return RedirectToAction("Index");
            }
            obj.PrijavaNaTecaj.Status_PrijaveId = 2;
            obj.PrijavaNaTecaj.OsobaOib = HttpContext.Session.GetString("oib");
            obj.PrijavaNaTecaj.TecajId = id;
            return View(obj);
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Prijava_Na_Tecaj> popisAktivnihPrijava = _unitOfWork.PrijavaNaTecaj.GetAll().Where(x=> x.OsobaOib == HttpContext.Session.GetString("oib"));
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties: "Osoba,Kategorija").Where(x => !popisAktivnihPrijava.Any(y => y.TecajId == x.Id));
            return Json(new { data = popisTecaja });
        }
        #endregion
    }
}
