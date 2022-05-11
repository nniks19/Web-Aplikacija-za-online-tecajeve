using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IRazinaPravaRepository: IRepository<Razina_Prava>
    {
        void Update(Razina_Prava obj);
    }
}
