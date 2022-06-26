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
    public class NarudzbaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NarudzbaController(IUnitOfWork unitOfWork)
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
            NarudzbaVM NarudzbaVM = new NarudzbaVM()
            {
                Narudzba = new(),
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                StatusNarudzbeList = _unitOfWork.StatusNarudzbe.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                }),
                NacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
            };

            return View(NarudzbaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Create(NarudzbaVM obj)
        {
            obj.Narudzba.datum_pocetak = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            if (ModelState.IsValid)
            {
                _unitOfWork.Narudzba.Add(obj.Narudzba);
                _unitOfWork.Save();
                TempData["success"] = "Narudzba uspješno dodana!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.StatusNarudzbeList = _unitOfWork.StatusNarudzbe.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib,
            });
            obj.NacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString()
            });
            return View(obj);
        }




        //GET
        public IActionResult Edit(int? id)
        {
            Narudzba Narudzba = _unitOfWork.Narudzba.GetFirstOrDefault(u => u.Id == id);
            NarudzbaVM NarudzbaVM = new NarudzbaVM()
            {
                Narudzba = Narudzba,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                StatusNarudzbeList = _unitOfWork.StatusNarudzbe.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
                OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib,
                }),
                NacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                }),
            };
            if (NarudzbaVM.Narudzba == null)
            {
                return NotFound();
            }
            return View(NarudzbaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Edit(NarudzbaVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Narudzba.Update(obj.Narudzba);
                _unitOfWork.Save();
                TempData["success"] = "Podaci o narudžbi uspješno uređeni!";
                return RedirectToAction("Index");
            }
            obj.TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.StatusNarudzbeList = _unitOfWork.StatusNarudzbe.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            obj.OsobaList = _unitOfWork.Osoba.GetAll().Select(i => new SelectListItem
            {
                Text = i.ime + " " + i.prezime,
                Value = i.Oib,
            });
            obj.NacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll().Select(i => new SelectListItem
            {
                Text = i.naziv,
                Value = i.Id.ToString(),
            });
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            Narudzba Narudzba = _unitOfWork.Narudzba.GetFirstOrDefault(u => u.Id == id);
            NarudzbaVM NarudzbaVM = new NarudzbaVM()
            {
                Narudzba = Narudzba,
                TecajList = _unitOfWork.Tecaj.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled=true,
                }),
                StatusNarudzbeList = _unitOfWork.StatusNarudzbe.GetAll().Select(i => new SelectListItem
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
                }),
                NacinPlacanjaList = _unitOfWork.NacinPlacanja.GetAll().Select(i => new SelectListItem
                {
                    Text = i.naziv,
                    Value = i.Id.ToString(),
                    Disabled=true,
                }),
            };
            if (NarudzbaVM.Narudzba == null)
            {
                return NotFound();
            }
            return View(NarudzbaVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult DeletePOST(Narudzba? Narudzba)
        {
            var obj = _unitOfWork.Narudzba.GetFirstOrDefault(u => u.Id == Narudzba.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Narudzba.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Narudžba uspješno obrisana!";
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisNarudzbi = _unitOfWork.Narudzba.GetAll(includeProperties:"Tecaj,Osoba,Status_narudzbe,Nacin_placanja");
            return Json(new { data = popisNarudzbi });
        }
        #endregion
    }
}
