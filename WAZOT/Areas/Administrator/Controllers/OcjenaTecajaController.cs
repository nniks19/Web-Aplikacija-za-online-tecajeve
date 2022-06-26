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
    public class OcjenaTecajaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OcjenaTecajaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Create()
        {
            OcjenaTecajaVM OcjenaTecajaVM = new OcjenaTecajaVM()
            {
                Ocjena_tecaja = new(),
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                })
            };

            return View(OcjenaTecajaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(OcjenaTecajaVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.OcjenaTecaja.Add(obj.Ocjena_tecaja);
                _unitOfWork.Save();
                TempData["success"] = "Ocjena tečaja uspješno dodana!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib
            });
            return View(obj);
        }




        //GET
        public IActionResult Edit(string? id)
        {
            Ocjena_tecaja Ocjena_tecaja = _unitOfWork.OcjenaTecaja.GetFirstOrDefault(u => u.Id == Convert.ToInt32(id));
            OcjenaTecajaVM ocjenaTecajaVM = new OcjenaTecajaVM()
            {
                Ocjena_tecaja = Ocjena_tecaja,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib
                })
            };
            if (ocjenaTecajaVM.Ocjena_tecaja == null)
            {
                return NotFound();
            }
            return View(ocjenaTecajaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(OcjenaTecajaVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.OcjenaTecaja.Update(obj.Ocjena_tecaja);
                _unitOfWork.Save();
                TempData["success"] = "Podaci o ocjeni tečaja su uspješno uređeni!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib
            });
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            Ocjena_tecaja Ocjena_tecaja = _unitOfWork.OcjenaTecaja.GetFirstOrDefault(u => u.Id == id);
            OcjenaTecajaVM ocjenaTecajaVM = new OcjenaTecajaVM()
            {
                Ocjena_tecaja = Ocjena_tecaja,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled = true,
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                    Disabled = true,
                })
            };
            if (ocjenaTecajaVM.Ocjena_tecaja == null)
            {
                return NotFound();
            }
            return View(ocjenaTecajaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(Ocjena_tecaja? Ocjena_tecaja)
        {
            var obj = _unitOfWork.OcjenaTecaja.GetFirstOrDefault(u => u.Id == Ocjena_tecaja.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.OcjenaTecaja.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Ocjena tečaja uspješno obrisana!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisOcjenaTecaja = _unitOfWork.OcjenaTecaja.GetAll(includeProperties:"Tecaj,Osoba");
            return Json(new { data = popisOcjenaTecaja });
        }
        #endregion
    }
}
