using WAZOT.Models;

namespace WAZOT.Repository.IRepository
{
    public interface INacinPlacanjaRepository: IRepository<Nacin_placanja>
    {
        void Update(Nacin_placanja obj);
    }
}
