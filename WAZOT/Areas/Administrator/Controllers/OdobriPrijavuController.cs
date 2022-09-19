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
    public class OdobriPrijavuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OdobriPrijavuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
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
                return RedirectToAction("Index");
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
                if(obj.Osoba.Razina_PravaId == 1)
                {
                    TempData["error"] = "Administratoru nije moguće zaključati račun!";
                    return RedirectToAction("Index");
                }
                _unitOfWork.Osoba.Update(obj.Osoba);
                _unitOfWork.Save();
                if (obj.Osoba.email == HttpContext.Session.GetString("email") && obj.Osoba.Razina_PravaId != 1)
                {
                    HttpContext.Session.Remove("email");
                    HttpContext.Session.Remove("razina_prava");
                    HttpContext.Session.Remove("ime");
                    HttpContext.Session.Remove("prezime");
                }
                TempData["success"] = "Stanje računa uređeno!";
                return RedirectToAction("Index");
            }
            obj.RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
            {
                Text = i.Naziv,
                Value = i.Id.ToString()
            });
            return View(obj);
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
