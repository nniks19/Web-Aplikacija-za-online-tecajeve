using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface IVideozapisRepository: IRepository<Videozapis>
    {
        void Update(Videozapis obj);
        int Max();
    }
}
