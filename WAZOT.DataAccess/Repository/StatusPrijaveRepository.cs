using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class StatusPrijaveRepository : Repository<Status_prijave>, IStatusPrijaveRepository
    {
        private ApplicationDbContext _db;
        public StatusPrijaveRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Status_prijave obj)
        {
            _db.Status_Prijave.Update(obj);
        }
    }
}
