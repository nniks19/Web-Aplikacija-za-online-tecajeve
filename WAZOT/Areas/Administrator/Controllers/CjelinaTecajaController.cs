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
    public class CjelinaTecaja : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CjelinaTecaja(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Create(int? id)
        {
            CjelinaTecajaVM cjelinaTecajaVM = new CjelinaTecajaVM()
            {
                Cjelina_tecaja = new Cjelina_tecaja(),
                TecajList = null,
            };
            cjelinaTecajaVM.Cjelina_tecaja.TecajId = id;
            return View(cjelinaTecajaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(CjelinaTecajaVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CjelinaTecaja.Add(obj.Cjelina_tecaja);
                _unitOfWork.Save();
                TempData["success"] = "Cjelina tečaja uspješno dodana!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Cjelina(int? id)
        {
            return View();
        }

        //GET
        public IActionResult Edit(string? id)
        {
            Cjelina_tecaja oCjelinaTecaja = _unitOfWork.CjelinaTecaja.GetFirstOrDefault(u => u.Id == Convert.ToInt32(id));
            CjelinaTecajaVM cjelinaTecajaVM = new CjelinaTecajaVM()
            {
                Cjelina_tecaja = oCjelinaTecaja,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString()
                })
            };
            if (cjelinaTecajaVM.Cjelina_tecaja == null)
            {
                return NotFound();
            }
            return View(cjelinaTecajaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(CjelinaTecajaVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CjelinaTecaja.Update(obj.Cjelina_tecaja);
                _unitOfWork.Save();
                TempData["success"] = "Podaci o cjelini tečaja su uspješno uređeni!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString()
            });
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            CjelinaTecajaVM cjelinaTecajaVM = new CjelinaTecajaVM()
            {
                Cjelina_tecaja = _unitOfWork.CjelinaTecaja.GetFirstOrDefault(u => u.Id == id),
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled = true,
                })
            };
            if (cjelinaTecajaVM.Cjelina_tecaja == null)
            {
                return NotFound();
            }
            return View(cjelinaTecajaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(CjelinaTecajaVM? cjelinaTecajaVM)
        {
            var obj = _unitOfWork.CjelinaTecaja.GetFirstOrDefault(u => u.Id == cjelinaTecajaVM.Cjelina_tecaja.Id);
            int brCjelina = _unitOfWork.CjelinaTecaja.GetAll().Where(u => u.TecajId == cjelinaTecajaVM.Cjelina_tecaja.TecajId).Count();
            int brVideozapisa = _unitOfWork.Videozapis.GetAll().Where(u => u.Cjelina_TecajaId == cjelinaTecajaVM.Cjelina_tecaja.Id).Count();

            if (brVideozapisa == 0)
            {
                _unitOfWork.CjelinaTecaja.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cjelina tečaja uspješno obrisana";
            }
            else
            {
                TempData["error"] = "Cjelina tečaja ne može biti obrisana jer sadržava bar 1 videozapis!";
            }

            if (obj == null)
            {
                return NotFound();
            }
            
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties:"Osoba,Kategorija");
            return Json(new { data = popisTecaja });
        }
        public IActionResult GetCjeline(int? id)
        {
            var popisCjelina = _unitOfWork.CjelinaTecaja.GetAll(includeProperties: "Tecaj").Where(x=>x.TecajId == id);
            return Json(new { data = popisCjelina });
        }
        #endregion
    }
}
