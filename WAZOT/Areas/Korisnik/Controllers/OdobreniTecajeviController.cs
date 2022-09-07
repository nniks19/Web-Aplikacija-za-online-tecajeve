using System.Collections.Generic;
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
            IEnumerable<Pracenje_Korisnika> pracenjeKorisnikaList = _unitOfWork.PracenjeKorisnika.GetAll().Where(x=> x.OsobaOib == HttpContext.Session.GetString("oib") && Convert.ToInt32(id) == x.TecajId);
            if(pracenjeKorisnikaList.Count() == 0)
            {
                Pracenje_Korisnika oPracenjeKorisnika = new Pracenje_Korisnika();
                oPracenjeKorisnika.OsobaOib = HttpContext.Session.GetString("oib");
                oPracenjeKorisnika.TecajId = Convert.ToInt32(id);
                oPracenjeKorisnika.Datum_posjete = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                oPracenjeKorisnika.brPosjeta = 1;
                oPracenjeKorisnika.brPokretanjaVideozapisa = 0;
                _unitOfWork.PracenjeKorisnika.Add(oPracenjeKorisnika);
                _unitOfWork.Save();
            }
            else
            {
                Pracenje_Korisnika oPracenjeKorisnika = new Pracenje_Korisnika();
                oPracenjeKorisnika = pracenjeKorisnikaList.First();
                oPracenjeKorisnika.brPosjeta = oPracenjeKorisnika.brPosjeta + 1;
                oPracenjeKorisnika.Datum_posjete = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                _unitOfWork.PracenjeKorisnika.Update(oPracenjeKorisnika);
                _unitOfWork.Save();
            }
            TecajPreviewVM tecajPreviewVM = new TecajPreviewVM()
            {
                Tecaj = oTecaj,
                VideozapisList = videozapisList,
                OcjenaTecajaList = ocjenetecajaList,
                CjelinaTecajaList = _unitOfWork.CjelinaTecaja.GetAll().Where(x => x.TecajId == oTecaj.Id),
                oibosobe= HttpContext.Session.GetString("oib"),
                OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == oTecaj.OsobaOib).Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib
                }),
                CjelinaList = _unitOfWork.CjelinaTecaja.GetAll().Where(x => x.TecajId == oTecaj.Id).Select(i => new SelectListItem
                {
                    Text = i.naziv_cjeline,
                    Value = i.Id.ToString(),
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
                        ocjenaTecaja.Cjelina_tecajaId = Convert.ToInt32(form["Ocjena_Tecaja.Cjelina_tecajaId"].ToString());
                        _unitOfWork.OcjenaTecaja.Add(ocjenaTecaja);
                        _unitOfWork.Save();
                        var tecaj = _unitOfWork.Tecaj.GetFirstOrDefault(x => x.Id == Convert.ToInt32(form["tecajid"].ToString()));
                        var ocjene = _unitOfWork.OcjenaTecaja.GetAll().Where(x => x.TecajId == tecaj.Id);
                        float ukupno_ocjena = ocjene.Count();
                        float zbroj_ocjena = ocjene.Sum(x => x.ocjena);
                        if (zbroj_ocjena != null)
                        {
                            tecaj.prosjecna_ocjena = zbroj_ocjena / ukupno_ocjena;
                            tecaj.prosjecna_ocjena = (float)Math.Round(tecaj.prosjecna_ocjena * 100f) / 100f;
                            _unitOfWork.Tecaj.Update(tecaj);
                            _unitOfWork.Save();
                        }

                        TempData["success"] = "Uspješno ste ocjenili tečaj!";
                        return RedirectToAction("Index");
                    }
                }
            }


            return RedirectToAction("Index");
        }
        //POST
        [HttpPost]
        public IActionResult PrijavaKomentara(int idocjene)
        {
            string message = "";
            var prijavljenosobaoib = _unitOfWork.OcjenaTecaja.GetAll().Where(x => x.Id == idocjene).First().OsobaOib;
            Neprikladni_komentar oNeprikladni_komentar = new Neprikladni_komentar();
            oNeprikladni_komentar.Ocjena_tecajaId = idocjene;
            oNeprikladni_komentar.PrijavaOsobaOib = HttpContext.Session.GetString("oib");
            oNeprikladni_komentar.PrijavljenOsobaOib = prijavljenosobaoib;
            if(oNeprikladni_komentar.PrijavaOsobaOib == oNeprikladni_komentar.PrijavljenOsobaOib)
            {
                message = "Ne možete prijaviti sami svoj komentar.";
            }
            else
            {
                if (_unitOfWork.NeprikladniKomentar.GetAll().Where(x => x.Ocjena_tecajaId == idocjene).Count() < 1)
                {
                    _unitOfWork.NeprikladniKomentar.Add(oNeprikladni_komentar);
                    _unitOfWork.Save();
                    message = "Uspješno ste označili komentar kao neprikladan!";
                }
                else
                {
                    message = "Ovaj komentar je već označen kao neprikladan. Administrator ga provjerava.";
                }
            }
            return Json(new { message=message});
        }
        //POST
        [HttpPost]
        public IActionResult PokretanjeVideozapisa(int idtecaja)
        {
            string message = "";
            var prijavljenosobaoib = HttpContext.Session.GetString("oib");
            var pracenjekorisnika = _unitOfWork.PracenjeKorisnika.GetAll().Where(x=>x.OsobaOib == prijavljenosobaoib && x.TecajId == idtecaja).First();
            
            pracenjekorisnika.brPokretanjaVideozapisa = pracenjekorisnika.brPokretanjaVideozapisa + 1;
            _unitOfWork.PracenjeKorisnika.Update(pracenjekorisnika);
            _unitOfWork.Save();
            return Json(new { message = message });
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
