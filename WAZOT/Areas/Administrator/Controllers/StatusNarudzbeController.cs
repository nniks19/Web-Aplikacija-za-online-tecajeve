using Microsoft.AspNetCore.Mvc;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Administrator")]
    public class StatusNarudzbeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusNarudzbeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Status_narudzbe> objStatusNarudzbeList = _unitOfWork.StatusNarudzbe.GetAll();

            return View(objStatusNarudzbeList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(Status_narudzbe obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StatusNarudzbe.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Status narudžbe uspješno dodan!";
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
            var statusNarudzbeFromDb = _unitOfWork.StatusNarudzbe.GetFirstOrDefault(u => u.Id == id);
            if (statusNarudzbeFromDb == null)
            {
                return NotFound();
            }
            return View(statusNarudzbeFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(Status_narudzbe obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.StatusNarudzbe.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Status narudžbe uspješno uređen!";
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
            var statusNarudzbeFromDbFirst = _unitOfWork.StatusNarudzbe.GetFirstOrDefault(u=>u.Id==id);
            if (statusNarudzbeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(statusNarudzbeFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.StatusNarudzbe.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.StatusNarudzbe.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Status narudžbe uspješno obrisan!";
            return RedirectToAction("Index");
        }
    }

}
