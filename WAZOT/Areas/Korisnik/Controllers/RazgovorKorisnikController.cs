using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;
using WAZOT.Models.ViewModels;
using WAZOT.Repository.IRepository;

namespace WAZOT.Controllers
{
    [Area("Korisnik")]
    public class RazgovorKreatorTecajaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RazgovorKreatorTecajaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Pregled(int? id)
        {
            var razgovor = _unitOfWork.Razgovor.GetAll().Where(x => x.Id == id).First();
            var porukaList = _unitOfWork.Poruka.GetAll().Where(x=>x.RazgovorId == razgovor.Id).ToList();
            RazgovorVM razgovorVM = new RazgovorVM();
            razgovorVM.Razgovor = razgovor;
            razgovorVM.porukaList = porukaList;
            var osoba = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == razgovorVM.Razgovor.PrimateljOsobaOib).First();
            razgovorVM.imeprezime = osoba.ime + " " + osoba.prezime;
            return View(razgovorVM);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Zastita od Cross Site Forgery
        public IActionResult Pregled(int? id , string poruka)
        {
            var razgovor = _unitOfWork.Razgovor.GetAll().Where(x => x.Id == id).First();
            Poruka oPoruka = new Poruka();
            oPoruka.Tekst = poruka;
            oPoruka.PosiljateljOsobaOib = HttpContext.Session.GetString("oib");
            oPoruka.Datum_slanja = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            oPoruka.RazgovorId = razgovor.Id;
            _unitOfWork.Poruka.Add(oPoruka);
            _unitOfWork.Save();
            var porukaList = _unitOfWork.Poruka.GetAll().Where(x => x.RazgovorId == razgovor.Id).ToList();
            RazgovorVM razgovorVM = new RazgovorVM();
            razgovorVM.Razgovor = razgovor;
            razgovorVM.porukaList = porukaList;
            var osoba = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == razgovorVM.Razgovor.PrimateljOsobaOib).First();
            razgovorVM.imeprezime = osoba.ime + " " + osoba.prezime;
            return View(razgovorVM);
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisRazgovora = _unitOfWork.Razgovor.GetAll(includeProperties: "PrimateljOsoba,PosiljateljOsoba").Where(x=>x.PosiljateljOsobaOib == HttpContext.Session.GetString("oib") || x.PrimateljOsobaOib == HttpContext.Session.GetString("oib"));
            return Json(new { data = popisRazgovora });
        }
        #endregion
    }
}
