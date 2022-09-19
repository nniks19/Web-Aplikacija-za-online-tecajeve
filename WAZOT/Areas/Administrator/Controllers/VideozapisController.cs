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
                obj.Videozapis.videozapis_putanja = "soon";
                obj.Videozapis.videozapis_tip = ".mp4";
                ModelState.Remove("Videozapis.videozapis_putanja");
                ModelState.Remove("Videozapis.videozapis_tip");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Videozapis.Add(obj.Videozapis);
                _unitOfWork.Save();
                TempData["success"] = "Videozapis uspješno prenesen!";

                string filename = _unitOfWork.Videozapis.Max().ToString();
                var uploads_first = Path.Combine(wwwRootPath, @"videozapisi\tecajevi");
                var uploads = Path.Combine(uploads_first, obj.Videozapis.TecajId.ToString());
                var extension = Path.GetExtension(file.FileName);
                using (var FileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    file.CopyTo(FileStreams);
                }
                obj.Videozapis.videozapis_putanja = @"\videozapisi\tecajevi\" + obj.Videozapis.TecajId.ToString() + @"\" + filename + extension;
                obj.Videozapis.videozapis_tip = extension;
                IEnumerable<Cjelina_tecaja> _cjelineTecaja = _unitOfWork.CjelinaTecaja.GetAll().Where(x=>x.TecajId == obj.Videozapis.TecajId);
                if(_cjelineTecaja.Count() > 0)
                {
                    obj.Videozapis.Cjelina_TecajaId = _cjelineTecaja.First().Id;
                }
                else
                {
                    Cjelina_tecaja objCjelina = new Cjelina_tecaja();
                    objCjelina.TecajId = obj.Videozapis.TecajId;
                    objCjelina.naziv_cjeline = "Nekategorizirano";
                    objCjelina.opis_cjeline = "Nekategorizirano";
                    _unitOfWork.CjelinaTecaja.Add(objCjelina);
                    _unitOfWork.Save();
                }
                _cjelineTecaja = _unitOfWork.CjelinaTecaja.GetAll().Where(x => x.TecajId == obj.Videozapis.TecajId);
                obj.Videozapis.Cjelina_TecajaId = _cjelineTecaja.First().Id;
                _unitOfWork.Videozapis.Update(obj.Videozapis);
                _unitOfWork.Save();
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
                CjelinaList = _unitOfWork.CjelinaTecaja.GetAll().Where(x=>x.TecajId == Videozapis.TecajId).Select(i => new SelectListItem
                {
                    Text = i.naziv_cjeline,
                    Value = i.Id.ToString(),
                }),
            };
            if (VideozapisVM.Videozapis == null)
            {
                return RedirectToAction("Index");
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
            obj.CjelinaList = _unitOfWork.CjelinaTecaja.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv_cjeline,
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
                CjelinaList = _unitOfWork.CjelinaTecaja.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv_cjeline,
                    Value = i.Id.ToString(),
                    Disabled = true,
                }),
            };
            if (VideozapisVM.Videozapis == null)
            {
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var putanjaDoVideozapisa = wwwRootPath + obj.videozapis_putanja;
            FileInfo file = new FileInfo(putanjaDoVideozapisa);
            if (file.Exists)//check file exist or not  
            {
                file.Delete();
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
            var popisVideozapisa = _unitOfWork.Videozapis.GetAll(includeProperties:"Tecaj,CjelinaTecaja");
            return Json(new { data = popisVideozapisa });
        }
        #endregion
    }
}
