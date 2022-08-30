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
    public class ProfilKreatorTecajaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfilKreatorTecajaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(ProfilVM profilVM)
        {
            Osoba oOsoba = new Osoba();
            profilVM.Osoba = _unitOfWork.Osoba.GetAll().Where(x => x.Oib == HttpContext.Session.GetString("oib")).FirstOrDefault();
            profilVM.Osoba.Razina_Prava = _unitOfWork.RazinaPrava.GetAll().Where(x => x.Id == profilVM.Osoba.Razina_PravaId).FirstOrDefault();
            return View(profilVM);
        }
       
    }
}
