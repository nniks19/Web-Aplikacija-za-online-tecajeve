using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IStatusNarudzbeRepository: IRepository<Status_narudzbe>
    {
        void Update(Status_narudzbe obj);
    }
}
