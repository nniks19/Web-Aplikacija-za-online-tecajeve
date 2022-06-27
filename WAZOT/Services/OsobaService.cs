using WAZOT.Models;

namespace WAZOT.Services
{
    public interface OsobaService
    {
        public Osoba Login(string username, string password);
        public Osoba CheckPermissionAndLogin(string? email, string? razinaprava);
    }
}
