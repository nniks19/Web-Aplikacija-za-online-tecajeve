using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;

using WAZOT.Models.ViewModels;

namespace WAZOT.Areas.Posjetitelj.Controllers
{
    [Area("Posjetitelj")]
    public class RegistracijaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistracijaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Korisnik()
        {
            OsobaVM osobaVM = new OsobaVM()
            {
                Osoba = new(),
                RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString(),
                    Disabled = true,
                    Selected = i.Id == 2,
                })
            };

            //ViewData["RazinaPravaList"] = RazinaPravaList;
            return View(osobaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Korisnik(OsobaVM obj)
        {
            ModelState.Remove("RazinaPravaList");
            if (ModelState.IsValid)
            {
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
        public IActionResult KreatorTecaja()
        {
            OsobaVM osobaVM = new OsobaVM()
            {
                Osoba = new(),
                RazinaPravaList = _unitOfWork.RazinaPrava.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString(),
                    Disabled=true,
                    Selected=i.Id == 3,
                })
            };
            //ViewData["RazinaPravaList"] = RazinaPravaList;
            return View(osobaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult KreatorTecaja(OsobaVM obj)
        {
            ModelState.Remove("RazinaPravaList");
            if (ModelState.IsValid)
            {
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
    }
}
