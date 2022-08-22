using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Administrator")]
    public class PrijavaNaTecajController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PrijavaNaTecajController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Create()
        {
            PrijavaNaTecajVM PrijavaNaTecaj = new PrijavaNaTecajVM()
            {
                PrijavaNaTecaj = new(),
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Razina_PravaId == 2).Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                }),
            };

            return View(PrijavaNaTecaj);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(PrijavaNaTecajVM obj)
        {
            if(obj.PrijavaNaTecaj == null)
            {
                ViewBag.notfilled = "Potrebno je odabrati ponuđene stavke.";
            }
            if (obj.PrijavaNaTecaj != null & ModelState.IsValid)
            {
                obj.PrijavaNaTecaj.datum_pocetak = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                _unitOfWork.PrijavaNaTecaj.Add(obj.PrijavaNaTecaj);
                _unitOfWork.Save();
                TempData["success"] = "Prijava na tečaj je suspješno kreirana!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Razina_PravaId == 2).Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib,
            });
            return View(obj);
        }




        //GET
        public IActionResult Edit(int? id)
        {
            Prijava_Na_Tecaj PrijavaNaTecaj = _unitOfWork.PrijavaNaTecaj.GetFirstOrDefault(u => u.Id == id);
            PrijavaNaTecajVM PrijavaNaTecajVM = new PrijavaNaTecajVM()
            {
                PrijavaNaTecaj = PrijavaNaTecaj,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Razina_PravaId == 2).Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                }),
            };
            if (PrijavaNaTecajVM.PrijavaNaTecaj == null)
            {
                return NotFound();
            }
            return View(PrijavaNaTecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(PrijavaNaTecajVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PrijavaNaTecaj.Update(obj.PrijavaNaTecaj);
                _unitOfWork.Save();
                TempData["success"] = "Podaci o prijavi uspješno uređeni!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Razina_PravaId == 2).Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib,
            });
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            Prijava_Na_Tecaj PrijavaNaTecaj = _unitOfWork.PrijavaNaTecaj.GetFirstOrDefault(u => u.Id == id);
            PrijavaNaTecajVM PrijavaNaTecajVM = new PrijavaNaTecajVM()
            {
                PrijavaNaTecaj = PrijavaNaTecaj,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled=true,
                }),
                StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled = true,
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Razina_PravaId == 2).Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                    Disabled = true,
                }),
            };
            if (PrijavaNaTecajVM.PrijavaNaTecaj == null)
            {
                return NotFound();
            }
            return View(PrijavaNaTecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(Prijava_Na_Tecaj? PrijavaNaTecaj)
        {
            var obj = _unitOfWork.PrijavaNaTecaj.GetFirstOrDefault(u => u.Id == PrijavaNaTecaj.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.PrijavaNaTecaj.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Prijava na tečaj uspješno uklonjena!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisPrijava = _unitOfWork.PrijavaNaTecaj.GetAll(includeProperties:"Tecaj,Osoba,Status_prijave");
            return Json(new { data = popisPrijava });
        }
        #endregion
    }
}
