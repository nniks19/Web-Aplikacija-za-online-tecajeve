using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Kreator_Tecaja")]
    public class PretpregledTecaja : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PretpregledTecaja(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Preview(string? id)
        {
            Tecaj oTecaj = _unitOfWork.Tecaj.GetFirstOrDefault(u => u.Id == Convert.ToInt32(id));
            IEnumerable<Videozapis> videozapisList = _unitOfWork.Videozapis.GetAll().Where(x => x.TecajId == oTecaj.Id);
            IEnumerable<Ocjena_tecaja> ocjenetecajaList = _unitOfWork.OcjenaTecaja.GetAll(includeProperties: "Osoba").Where(x => x.TecajId == oTecaj.Id);
            TecajPreviewVM tecajPreviewVM = new TecajPreviewVM()
            {
                Tecaj = oTecaj,
                VideozapisList = videozapisList,
                OcjenaTecajaList = ocjenetecajaList,
                CjelinaTecajaList = _unitOfWork.CjelinaTecaja.GetAll().Where(x => x.TecajId == oTecaj.Id),
                OsobaList = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == oTecaj.OsobaOib).Select(i => new SelectListItem
                {
                    Text = i.ime + " " + i.prezime,
                    Value = i.Oib
                }),
                KategorijaList = _unitOfWork.Kategorija.GetAll().Where(x => x.Id == oTecaj.KategorijaId).Select(i => new SelectListItem
                {
                    Text = i.Naziv,
                    Value = i.Id.ToString()
                })
            };
            if (tecajPreviewVM.Tecaj == null)
            {
                return RedirectToAction("Index");
            }
            return View(tecajPreviewVM);
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisTecaja = _unitOfWork.Tecaj.GetAll(includeProperties: "Osoba,Kategorija");
            popisTecaja = popisTecaja.Where(x => x.OsobaOib == HttpContext.Session.GetString("oib"));
            return Json(new { data = popisTecaja });
        }
        #endregion
    }
}
