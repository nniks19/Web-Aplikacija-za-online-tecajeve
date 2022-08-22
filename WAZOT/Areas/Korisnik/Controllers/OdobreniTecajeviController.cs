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
    public class OdobreniTecajeviController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OdobreniTecajeviController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
            IEnumerable<Ocjena_tecaja> ocjenaTecajaKorisnika = ocjenetecajaList.Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            bool vecKomentirao = false;
            if (ocjenaTecajaKorisnika.Count()>0)
            {
                vecKomentirao = true;
            }
            IEnumerable<Pracenje_Korisnika> pracenjeKorisnikaList = _unitOfWork.PracenjeKorisnika.GetAll().Where(x=> x.OsobaOib == HttpContext.Session.GetString("oib") && Convert.ToInt32(id) == x.TecajId);
            if(pracenjeKorisnikaList == null)
            {
                Pracenje_Korisnika oPracenjeKorisnika = new Pracenje_Korisnika();
                oPracenjeKorisnika.OsobaOib = HttpContext.Session.GetString("oib");
                oPracenjeKorisnika.TecajId = Convert.ToInt32(id);
                oPracenjeKorisnika.Datum_posjete = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                oPracenjeKorisnika.Vrijeme_videozapis = 0;
                _unitOfWork.PracenjeKorisnika.Add(oPracenjeKorisnika);
                _unitOfWork.Save();
            }
            TecajPreviewVM tecajPreviewVM = new TecajPreviewVM()
            {
                Tecaj = oTecaj,
                VideozapisList = videozapisList,
                OcjenaTecajaList = ocjenetecajaList,
                vecKomentirao = vecKomentirao,
                oibosobe= HttpContext.Session.GetString("oib"),
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
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult OcjeniTecaj(IFormCollection? form)
        {
            if (form["komentar"].ToString().Count()>0)
            {
                if (form["ocjena"].ToString().Count() > 0 && form["ocjena"].ToString() != 0.ToString())
                {
                    if (form["oibosobe"].ToString().Count() > 0)
                    {
                        Ocjena_tecaja ocjenaTecaja = new Ocjena_tecaja();
                        ocjenaTecaja.ocjena = Convert.ToInt32(form["ocjena"].ToString());
                        ocjenaTecaja.komentar = form["komentar"].ToString();
                        ocjenaTecaja.OsobaOib = form["oibosobe"].ToString();
                        ocjenaTecaja.TecajId = Convert.ToInt32(form["tecajid"].ToString());
                        _unitOfWork.OcjenaTecaja.Add(ocjenaTecaja);
                        _unitOfWork.Save();
                        TempData["success"] = "Uspješno ste ocjenili tečaj!";
                        return RedirectToAction("Index");
                    }
                }
            }


            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Prijava_Na_Tecaj> popisAktivnihPrijava = _unitOfWork.PrijavaNaTecaj.GetAll().Where(x=>x.OsobaOib == HttpContext.Session.GetString("oib") && x.Status_PrijaveId == 1);
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties: "Osoba,Kategorija").Where(x => popisAktivnihPrijava.Any(y => y.TecajId == x.Id));
            return Json(new { data = popisTecaja });
        }
        #endregion
    }
}
