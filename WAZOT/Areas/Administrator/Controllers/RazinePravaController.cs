using Microsoft.AspNetCore.Mvc;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Administrator")]
    public class RazinePravaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RazinePravaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Razina_Prava> objRazinaPravaList = _unitOfWork.RazinaPrava.GetAll();

            return View(objRazinaPravaList);
        }

        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(Razina_Prava obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RazinaPrava.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Razina prava uspješno kreirana!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var razinaPravaFromDb = _unitOfWork.RazinaPrava.GetFirstOrDefault(u => u.Id == id);
            if (razinaPravaFromDb == null)
            {
                return RedirectToAction("Index");
            }
            return View(razinaPravaFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(Razina_Prava obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RazinaPrava.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Razina prava uspješno uređena!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var razinaPravaFromDbFirst = _unitOfWork.RazinaPrava.GetFirstOrDefault(u=>u.Id==id);
            if (razinaPravaFromDbFirst == null)
            {
                return RedirectToAction("Index");
            }
            return View(razinaPravaFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.RazinaPrava.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            var _osobe = _unitOfWork.Osoba.GetAll().Where(x => x.Razina_PravaId == id);
            if(_osobe.Count() > 0)
            {
                TempData["error"] = "Postoje osobe sa ovom razinom prava!";
                return RedirectToAction("Index");
            }
            _unitOfWork.RazinaPrava.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Razina prava uspješno obrisana!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisRazinaPrava = _unitOfWork.RazinaPrava.GetAll();
            return Json(new { data = popisRazinaPrava });
        }
        #endregion
    }

}
