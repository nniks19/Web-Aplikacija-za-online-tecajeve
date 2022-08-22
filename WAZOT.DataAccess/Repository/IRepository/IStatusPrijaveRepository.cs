using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IStatusPrijaveRepository: IRepository<Status_prijave>
    {
        void Update(Status_prijave obj);
    }
}
