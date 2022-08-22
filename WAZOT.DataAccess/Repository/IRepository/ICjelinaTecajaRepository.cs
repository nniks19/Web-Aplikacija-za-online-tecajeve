using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface ICjelinaTecajaRepository: IRepository<Cjelina_tecaja>
    {
        void Update(Cjelina_tecaja obj);
    }
}
