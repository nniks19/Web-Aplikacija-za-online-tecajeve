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
            NarudzbaVM NarudzbaVM = new NarudzbaVM()
            {
                Narudzba = new(),
                NacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
            };
            NarudzbaVM.Narudzba.Status_NarudzbeId = 2;
            NarudzbaVM.Narudzba.OsobaOib = HttpContext.Session.GetString("oib");
            NarudzbaVM.Narudzba.TecajId = id;
            return View(NarudzbaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(NarudzbaVM obj, int? id)
        {
            if (obj.Narudzba == null)
            {
                ViewBag.notfilled = "Potrebno je odabrati ponuđene stavke.";
            }
            if (obj.Narudzba != null & ModelState.IsValid)
            {
                obj.Narudzba.datum_pocetak = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                _unitOfWork.Narudzba.Add(obj.Narudzba);
                _unitOfWork.Save();
                TempData["success"] = "Uspješno ste se prijavili na tečaj!";
                return RedirectToAction("Index");
            }
            obj.NacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString()
            });
            obj.Narudzba.Status_NarudzbeId = 2;
            obj.Narudzba.OsobaOib = HttpContext.Session.GetString("oib");
            obj.Narudzba.TecajId = id;
            return View(obj);
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Narudzba> popisAktivnihNarudzbi = _unitOfWork.Narudzba.GetAll().Where(x=> x.OsobaOib == HttpContext.Session.GetString("oib"));
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties: "Osoba,Kategorija").Where(x => !popisAktivnihNarudzbi.Any(y => y.TecajId == x.Id));
            return Json(new { data = popisTecaja });
        }
        #endregion
    }
}
