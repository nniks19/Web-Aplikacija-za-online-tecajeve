using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IRazgovorRepository: IRepository<Razgovor>
    {
        void Update(Razgovor obj);
    }
}
