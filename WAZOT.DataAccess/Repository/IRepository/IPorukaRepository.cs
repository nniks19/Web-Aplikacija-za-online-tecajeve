using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IPorukaRepository: IRepository<Poruka>
    {
        void Update(Poruka obj);
    }
}
