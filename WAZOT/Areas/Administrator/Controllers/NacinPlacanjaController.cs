using Microsoft.AspNetCore.Mvc;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Administrator")]
    public class NacinPlacanjaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NacinPlacanjaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Nacin_placanja> objNacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll();

            return View(objNacinPlacanjaList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(Nacin_placanja obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.NacinPlacanja.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Način plaćanja uspješno kreiran!";
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
            var nacinPlacanjaFromDb = _unitOfWork.NacinPlacanja.GetFirstOrDefault(u => u.Id == id);
            if (nacinPlacanjaFromDb == null)
            {
                return NotFound();
            }
            return View(nacinPlacanjaFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(Nacin_placanja obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.NacinPlacanja.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Način plaćanja uspješno uređen!";
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
            var nacinPlacanjaFromDbFirst = _unitOfWork.NacinPlacanja.GetFirstOrDefault(u=>u.Id==id);
            if (nacinPlacanjaFromDbFirst == null)
            {
                return NotFound();
            }
            return View(nacinPlacanjaFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.NacinPlacanja.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.NacinPlacanja.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Način plaćanja uspješno obrisan!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisNacinaPlacanja = _unitOfWork.NacinPlacanja.GetAll();
            return Json(new { data = popisNacinaPlacanja });
        }
        #endregion
    }

}
