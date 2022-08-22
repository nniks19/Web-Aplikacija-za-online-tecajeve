using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IPracenjeKorisnikaRepository: IRepository<Pracenje_Korisnika>
    {
        void Update(Pracenje_Korisnika obj);
    }
}
