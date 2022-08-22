using Microsoft.AspNetCore.Mvc;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Administrator")]
    public class StatusPrijaveController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusPrijaveController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Status_prijave> objStatusPrijaveList = _unitOfWork.StatusPrijave.GetAll();

            return View(objStatusPrijaveList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(Status_prijave obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StatusPrijave.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Status prijave uspješno kreiran!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }




        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var statusPrijaveFromDb = _unitOfWork.StatusPrijave.GetFirstOrDefault(u => u.Id == id);
            if (statusPrijaveFromDb == null)
            {
                return NotFound();
            }
            return View(statusPrijaveFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(Status_prijave obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StatusPrijave.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Status prijave uspješno uređen!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var statusPrijaveFromDbFirst = _unitOfWork.StatusPrijave.GetFirstOrDefault(u=>u.Id==id);
            if (statusPrijaveFromDbFirst == null)
            {
                return NotFound();
            }
            return View(statusPrijaveFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.StatusPrijave.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.StatusPrijave.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Status prijave uspješno obrisan!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisStatusaPrijava = _unitOfWork.StatusPrijave.GetAll();
            return Json(new { data = popisStatusaPrijava });
        }
        #endregion
    }

}
