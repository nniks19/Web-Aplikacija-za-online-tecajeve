
using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class RazgovorRepository : Repository<Razgovor>, IRazgovorRepository
    {
        private ApplicationDbContext _db;
        public RazgovorRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Razgovor obj)
        {
            var objFromDb = _db.Razgovor.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.PosiljateljOsobaOib = obj.PosiljateljOsobaOib;
                objFromDb.PrimateljOsobaOib = obj.PrimateljOsobaOib;
            }
        }
        public int Max()
        {
            var idFromDb = _db.Razgovor.Max(item => item.Id);
            return idFromDb;
        }
    }
}
