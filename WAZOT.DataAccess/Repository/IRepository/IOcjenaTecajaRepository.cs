using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IOcjenaTecajaRepository: IRepository<Ocjena_tecaja>
    {
        void Update(Ocjena_tecaja obj);
    }
}
