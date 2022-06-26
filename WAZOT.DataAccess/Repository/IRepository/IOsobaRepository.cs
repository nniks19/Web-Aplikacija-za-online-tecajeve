using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IOsobaRepository: IRepository<Osoba>
    {
        void Update(Osoba obj);
    }
}
