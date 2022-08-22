using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class PorukaRepository : Repository<Poruka>, IPorukaRepository
    {
        private ApplicationDbContext _db;
        public PorukaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Poruka obj)
        {
            var objFromDb = _db.Poruka.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.Tekst = obj.Tekst;
                objFromDb.Datum_slanja = obj.Datum_slanja;
                objFromDb.RazgovorId = obj.RazgovorId;
            }
        }
    }
}
