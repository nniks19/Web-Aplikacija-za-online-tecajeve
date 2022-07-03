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
    [Area("Korisnik")]
    public class PregledController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PregledController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var popisNarudzbi = _unitOfWork.Narudzba.GetAll(includeProperties:"Tecaj,Osoba,Status_narudzbe,Nacin_placanja").Where(x=>x.OsobaOib == HttpContext.Session.GetString("oib"));
            return Json(new { data = popisNarudzbi });
        }
        #endregion
    }
}
