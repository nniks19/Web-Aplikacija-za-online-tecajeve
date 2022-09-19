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
    [Area("Kreator_Tecaja")]
    public class UpravljanjePrijavamaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpravljanjePrijavamaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Edit(int? id)
        {
            Prijava_Na_Tecaj PrijavaNaTecaj = _unitOfWork.PrijavaNaTecaj.GetFirstOrDefault(u => u.Id == id);
            PrijavaNaTecajVM PrijavaNaTecajVM = new PrijavaNaTecajVM()
            {
                PrijavaNaTecaj = PrijavaNaTecaj,
                StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
            };
            if (PrijavaNaTecajVM.PrijavaNaTecaj == null)
            {
                return RedirectToAction("Index");
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
            obj.StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
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
                StatusPrijavaList = _unitOfWork.StatusPrijave.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled = true,
                }),
            };
            if (PrijavaNaTecajVM.PrijavaNaTecaj == null)
            {
                return RedirectToAction("Index");
            }
            return View(PrijavaNaTecajVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(Prijava_Na_Tecaj? PrijavaNaTecaj) // tu mozda bude error zbogi mena PrijavaNaTecaj
        {
            var obj = _unitOfWork.PrijavaNaTecaj.GetFirstOrDefault(u => u.Id == PrijavaNaTecaj.Id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            _unitOfWork.PrijavaNaTecaj.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Prijava uspješno obrisana!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties: "Osoba,Kategorija").Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            var popisPrijava = _unitOfWork.PrijavaNaTecaj.GetAll(includeProperties:"Tecaj,Osoba,Status_prijave").Where(x => popisTecaja.Any(y => x.TecajId == y.Id));
            return Json(new { data = popisPrijava });
        }
        #endregion
    }
}
