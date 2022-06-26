using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class NacinPlacanjaRepository : Repository<Nacin_placanja>, INacinPlacanjaRepository
    {
        private ApplicationDbContext _db;
        public NacinPlacanjaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Nacin_placanja obj)
        {
            _db.NacinPlacanja.Update(obj);
        }
    }
}
