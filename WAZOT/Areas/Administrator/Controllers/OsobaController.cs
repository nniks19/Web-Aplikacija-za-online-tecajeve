using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Administrator")]
    public class OsobaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OsobaController(IUnitOfWork unitOfWork)
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
            OsobaVM osobaVM = new OsobaVM()
            {
                Osoba = new(),
                RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString()
                })
            };

            //ViewData["RazinaPravaList"] = RazinaPravaList;
            return View(osobaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(OsobaVM obj)
        {
            ModelState.Remove("RazinaPravaList");
            if (ModelState.IsValid)
            {
                if(_unitOfWork.Osoba.GetAll().Where(x=>x.Oib == obj.Osoba.Oib).Count() > 0)
                {
                    ViewBag.msgOsobaPostoji = "OIB već postoji!";
                    return View(obj);
                }
                if (_unitOfWork.Osoba.GetAll().Where(x => x.email == obj.Osoba.email).Count() > 0)
                {
                    ViewBag.msgEmailPostoji = "Email već postoji!";
                    return View(obj);
                }
                _unitOfWork.Osoba.Add(obj.Osoba);
                _unitOfWork.Save();
                TempData["success"] = "Osoba uspješno dodana!";
                return RedirectToAction("Index");
            }
            obj.RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
            {
                Text = i.Naziv,
                Value = i.Id.ToString()
            });
            return View(obj);
        }




        //GET
        public IActionResult Edit(string? oib)
        {
            Osoba oOsoba = _unitOfWork.Osoba.GetFirstOrDefault(u => u.Oib == oib);
            OsobaVM osobaVM = new OsobaVM()
            {
                Osoba = oOsoba,
                RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString(),
                })
            };
            if (osobaVM.Osoba == null)
            {
                return NotFound();
            }
            return View(osobaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(OsobaVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Osoba.Update(obj.Osoba);
                _unitOfWork.Save();
                if (obj.Osoba.email == HttpContext.Session.GetString("email") && obj.Osoba.Razina_PravaId != 1)
                {
                    HttpContext.Session.Remove("email");
                    HttpContext.Session.Remove("razina_prava");
                    HttpContext.Session.Remove("ime");
                    HttpContext.Session.Remove("prezime");
                }
                TempData["success"] = "Podaci o osobi uspješno uređeni!";
                return RedirectToAction("Index");
            }
            obj.RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
            {
                Text = i.Naziv,
                Value = i.Id.ToString()
            });
            return View(obj);
        }



        //GET
        public IActionResult Delete(string? oib)
        {
            Osoba oOsoba = _unitOfWork.Osoba.GetFirstOrDefault(u => u.Oib == oib);
            OsobaVM osobaVM = new OsobaVM()
            {
                Osoba = oOsoba,
                RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString(),
                    Disabled=true,
                })
            };
            if (osobaVM.Osoba == null)
            {
                return NotFound();
            }
            return View(osobaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(Osoba? Osoba)
        {
            var obj = _unitOfWork.Osoba.GetFirstOrDefault(u => u.Oib == Osoba.Oib);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Osoba.Remove(obj);
            _unitOfWork.Save();
            if (obj.email == HttpContext.Session.GetString("email"))
            {
                HttpContext.Session.Remove("email");
                HttpContext.Session.Remove("razina_prava");
                HttpContext.Session.Remove("ime");
                HttpContext.Session.Remove("prezime");
            }
            TempData["success"] = "Podaci o osobi uspješno obrisani!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisOsoba = _unitOfWork.Osoba.GetAll(includeProperties:"Razina_Prava");
            return Json(new { data = popisOsoba });
        }
        #endregion
    }
}
