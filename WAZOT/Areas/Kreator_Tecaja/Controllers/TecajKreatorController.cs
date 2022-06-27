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
    public class TecajKreatorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TecajKreatorController(IUnitOfWork unitOfWork)
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
            TecajVM tecajVM = new TecajVM()
            {
                Tecaj = new(),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib
                })
            };

            //ViewData["RazinaPravaList"] = RazinaPravaList;
            return View(tecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(TecajVM obj)
        {
            ModelState.Remove("OsobaList");
            if (ModelState.IsValid)
            {
                _unitOfWork.Tecaj.Add(obj.Tecaj);
                _unitOfWork.Save();
                TempData["success"] = "Tečaj uspješno dodan!";
                return RedirectToAction("Index");
            }
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib
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
                })
            };
            if (tecajVM.Tecaj == null)
            {
                return NotFound();
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
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            TecajVM tecajVM = new TecajVM()
            {
                Tecaj = _unitOfWork.Tecaj.GetFirstOrDefault(u => u.Id == id),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                    Disabled =true,
                })
            };
            if (tecajVM.Tecaj == null)
            {
                return NotFound();
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
                return NotFound();
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
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties:"Osoba");
            return Json(new { data = popisTecaja });
        }
        #endregion
    }
}
