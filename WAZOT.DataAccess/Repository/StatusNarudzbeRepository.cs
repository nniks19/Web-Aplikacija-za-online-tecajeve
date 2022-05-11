using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class StatusNarudzbeRepository : Repository<Status_narudzbe>, IStatusNarudzbeRepository
    {
        private ApplicationDbContext _db;
        public StatusNarudzbeRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Status_narudzbe obj)
        {
            _db.Status_Narudzbe.Update(obj);
        }
    }
}
