﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Kreator_Tecaja")]
    public class PopisTecajaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PopisTecajaController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Create()
        {
            TecajVM tecajVM = new TecajVM()
            {
                Tecaj = new(),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib
                }),
                KategorijaList = _unitOfWork.Kategorija.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString()
                })
            };

            //ViewData["RazinaPravaList"] = RazinaPravaList;
            return View(tecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(TecajVM obj, IFormFile? file)
        {
            ModelState.Remove("OsobaList");
            ModelState.Remove("Tecaj.OsobaOib");
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                ModelState.Remove("Tecaj.slika");
                obj.Tecaj.slika = "Soon";
            }
            if (ModelState.IsValid)
            {
                obj.Tecaj.OsobaOib = HttpContext.Session.GetString("oib");
                _unitOfWork.Tecaj.Add(obj.Tecaj);
                _unitOfWork.Save();
                TempData["success"] = "Tečaj uspješno dodan!";

                //Spremanje slike
                int lastTecajId = _unitOfWork.Tecaj.Max();
                //Originalni naziv slike: file.FileName
                string filename = lastTecajId.ToString();
                var uploads = Path.Combine(wwwRootPath, @"slike\tecajevi\");
                var extension = Path.GetExtension(file.FileName);
                using (var FileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    file.CopyTo(FileStreams);
                }
                obj.Tecaj.slika = @"\slike\tecajevi\" + filename + extension;
                obj.Tecaj.Id = lastTecajId;
                Directory.CreateDirectory(Path.Combine(Path.Combine(wwwRootPath, @"videozapisi\tecajevi\"), filename));
                Cjelina_tecaja cjelina_Tecaja = new Cjelina_tecaja();
                cjelina_Tecaja.naziv_cjeline = "Nekategorizirano";
                cjelina_Tecaja.opis_cjeline = "Nekategorizirano";
                cjelina_Tecaja.TecajId = lastTecajId;
                _unitOfWork.CjelinaTecaja.Add(cjelina_Tecaja);
                _unitOfWork.Tecaj.Update(obj.Tecaj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib
            });
            obj.KategorijaList = _unitOfWork.Kategorija.GetAll().Select(i => new SelectListItem
            {
                Text = i.Naziv,
                Value = i.Id.ToString()
            });
            return View(obj);
        }


        //GET
        public IActionResult Edit(string? id)
        {
            Tecaj oTecaj = _unitOfWork.Tecaj.GetFirstOrDefault(u => u.Id == Convert.ToInt32(id));
            TecajVM tecajVM = new TecajVM()
            {
                Tecaj = oTecaj,
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib
                }),
                KategorijaList = _unitOfWork.Kategorija.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString()
                })
            };
            if (tecajVM.Tecaj == null)
            {
                return RedirectToAction("Index");
            }
            return View(tecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(TecajVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Tecaj.Update(obj.Tecaj);
                _unitOfWork.Save();
                TempData["success"] = "Podaci o tečaju su uspješno uređeni!";
                return RedirectToAction("Index");
            }
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib
            });
            obj.KategorijaList = _unitOfWork.Kategorija.GetAll().Select(i => new SelectListItem
            {
                Text = i.Naziv,
                Value = i.Id.ToString()
            });
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            TecajVM tecajVM = new TecajVM()
            {
                Tecaj = _unitOfWork.Tecaj.GetFirstOrDefault(u => u.Id == id),
                OsobaList = _unitOfWork.Osoba.GetAll().Where(x=>x.Oib == HttpContext.Session.GetString("oib")).Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                    Disabled = true,
                }),
                KategorijaList = _unitOfWork.Kategorija.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString(),
                    Disabled = true,
                })
            };
            if (tecajVM.Tecaj == null)
            {
                return RedirectToAction("Index");
            }
            return View(tecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(Tecaj? Tecaj)
        {
            var obj = _unitOfWork.Tecaj.GetFirstOrDefault(u => u.Id == Tecaj.Id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            var cjelineTecaja = _unitOfWork.CjelinaTecaja.GetAll().Where(x => x.TecajId == Tecaj.Id);
            if (cjelineTecaja.Count() > 0)
            {
                TempData["error"] = "Tečaj ne može biti obrisan jer ima barem jednu cjelinu!";
                return RedirectToAction("Index");
            }
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var putanjaDoSlike = wwwRootPath + obj.slika;
            FileInfo file = new FileInfo(putanjaDoSlike);
            if (file.Exists)//check file exist or not  
            {
                file.Delete();
            }
            int TecajId = Tecaj.Id;
            var putanja = Path.Combine(Path.Combine(wwwRootPath, @"videozapisi\tecajevi\"), TecajId.ToString());
            if (Directory.Exists(putanja))
            {
                Directory.Delete(putanja, true);
            }

            _unitOfWork.Tecaj.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Podaci o tečaju uspješno obrisani!";

            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties: "Osoba,Kategorija");
            popisTecaja = popisTecaja.Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            return Json(new { data = popisTecaja });
        }
        #endregion
    }
}
