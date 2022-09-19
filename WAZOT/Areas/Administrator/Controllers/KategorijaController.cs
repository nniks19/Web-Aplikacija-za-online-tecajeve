using Microsoft.AspNetCore.Mvc;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Administrator")]
    public class KategorijaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public KategorijaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            IEnumerable<Kategorija> objKategorijaList = _unitOfWork.Kategorija.GetAll();

            return View(objKategorijaList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(Kategorija obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Kategorija.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Kategorija uspješno kreirana!";
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
            var kategorijaFromDb = _unitOfWork.Kategorija.GetFirstOrDefault(u => u.Id == id);
            if (kategorijaFromDb == null)
            {
                return RedirectToAction("Index");
            }
            return View(kategorijaFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(Kategorija obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Kategorija.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Kategorija uspješno uređena!";
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
            var kategorijaFromDbFirst = _unitOfWork.Kategorija.GetFirstOrDefault(u=>u.Id==id);
            if (kategorijaFromDbFirst == null)
            {
                return RedirectToAction("Index");
            }
            return View(kategorijaFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Kategorija.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            var listaTecajeva = _unitOfWork.Tecaj.GetAll().Where(x => x.KategorijaId == id);
            if (listaTecajeva.Count() > 0)
            {
                TempData["error"] = "Postoje tečajevi u odabranoj kategoriji!";
                return RedirectToAction("Index");
            }
            _unitOfWork.Kategorija.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Kategorija uspješno obrisana!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisKategorija = _unitOfWork.Kategorija.GetAll();
            return Json(new { data = popisKategorija });
        }
        #endregion
    }

}
