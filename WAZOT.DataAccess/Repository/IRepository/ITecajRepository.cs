using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface ITecajRepository: IRepository<Tecaj>
    {
        void Update(Tecaj obj);
        int Max();
    }
}
