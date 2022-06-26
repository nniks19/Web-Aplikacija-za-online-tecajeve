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
    [Area("Administrator")]
    public class VideozapisController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public VideozapisController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Create()
        {
            VideozapisVM VideozapisVM = new VideozapisVM()
            {
                Videozapis = new(),
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
            };

            return View(VideozapisVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(VideozapisVM obj, IFormFile? file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"videozapisi\tecajevi");
                var extension = Path.GetExtension(file.FileName);
                using (var FileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    file.CopyTo(FileStreams);
                }
                obj.Videozapis.videozapis_putanja = @"\videozapisi\tecajevi\" + filename + extension;
                obj.Videozapis.videozapis_tip = extension;
                ModelState.Remove("Videozapis.videozapis_putanja");
                ModelState.Remove("Videozapis.videozapis_tip");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Videozapis.Add(obj.Videozapis);
                _unitOfWork.Save();
                TempData["success"] = "Videozapis uspješno dodan!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            return View(obj);
        }




        //GET
        public IActionResult Edit(int? id, IFormFile file)
        {
            Videozapis Videozapis = _unitOfWork.Videozapis.GetFirstOrDefault(u => u.Id == id);
            VideozapisVM VideozapisVM = new VideozapisVM()
            {
                Videozapis = Videozapis,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
            };
            if (VideozapisVM.Videozapis == null)
            {
                return NotFound();
            }
            return View(VideozapisVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(VideozapisVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Videozapis.Update(obj.Videozapis);
                _unitOfWork.Save();
                TempData["success"] = "Podaci o videozapisu uspješno uređeni!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            Videozapis Videozapis = _unitOfWork.Videozapis.GetFirstOrDefault(u => u.Id == id);
            VideozapisVM VideozapisVM = new VideozapisVM()
            {
                Videozapis = Videozapis,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled = true,
                }),
            };
            if (VideozapisVM.Videozapis == null)
            {
                return NotFound();
            }
            return View(VideozapisVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(Videozapis? Videozapis)
        {
            var obj = _unitOfWork.Videozapis.GetFirstOrDefault(u => u.Id == Videozapis.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Videozapis.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Videozapis uspješno obrisan!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisVideozapisa = _unitOfWork.Videozapis.GetAll(includeProperties:"Tecaj");
            return Json(new { data = popisVideozapisa });
        }
        #endregion
    }
}
