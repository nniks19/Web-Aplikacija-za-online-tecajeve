using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface INarudzbaRepository: IRepository<Narudzba>
    {
        void Update(Narudzba obj);
    }
}
