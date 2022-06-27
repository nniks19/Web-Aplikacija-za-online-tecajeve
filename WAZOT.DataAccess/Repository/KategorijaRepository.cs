using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class KategorijaRepository : Repository<Kategorija>, IKategorijaRepository
    {
        private ApplicationDbContext _db;
        public KategorijaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Kategorija obj)
        {
            _db.Kategorija.Update(obj);
        }
    }
}
