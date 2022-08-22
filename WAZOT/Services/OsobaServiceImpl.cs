using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Models;

namespace WAZOT.Services
{
    public class OsobaServiceImpl: OsobaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OsobaServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Osoba Login(string email, string password, string pin)
        {
            return _unitOfWork.Osoba.GetFirstOrDefault(u => u.email == email && u.lozinka == password && u.pin == pin);        
        }
        public Osoba CheckPermissionAndLogin(string? email, string? razinaprava)
        {
            return _unitOfWork.Osoba.GetFirstOrDefault(u => u.email == email && u.Razina_PravaId == Convert.ToInt32(razinaprava));
        }
    }
}
