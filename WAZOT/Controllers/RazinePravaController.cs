using Microsoft.AspNetCore.Mvc;
using WAZOT.Data;
using WAZOT.Models;


namespace WAZOT.Controllers
{
    public class RazinePravaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RazinePravaController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            IEnumerable<Razina_Prava> objRazinaPravaList = _db.Razina_Prava.ToList();

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
                _db.Razina_Prava.Add(obj);
                _db.SaveChanges();
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
                return NotFound();
            }
            var razinaPravaFromDb = _db.Razina_Prava.Find(id);
            if (razinaPravaFromDb == null)
            {
                return NotFound();
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
                _db.Razina_Prava.Update(obj);
                _db.SaveChanges();
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
                return NotFound();
            }
            var razinaPravaFromDb = _db.Razina_Prava.Find(id);
            if (razinaPravaFromDb == null)
            {
                return NotFound();
            }
            return View(razinaPravaFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Razina_Prava.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Razina_Prava.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Razina prava uspješno obrisana!";
            return RedirectToAction("Index");
        }
    }

}
