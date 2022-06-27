using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IKategorijaRepository: IRepository<Kategorija>
    {
        void Update(Kategorija obj);
    }
}
