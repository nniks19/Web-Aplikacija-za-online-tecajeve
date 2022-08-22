using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IPrijavaNaTecajRepository: IRepository<Prijava_Na_Tecaj>
    {
        void Update(Prijava_Na_Tecaj obj);
    }
}
