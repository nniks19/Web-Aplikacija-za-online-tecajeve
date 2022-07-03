using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class TecajRepository : Repository<Tecaj>, ITecajRepository
    {
        private ApplicationDbContext _db;
        public TecajRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Tecaj obj)
        {
            var objFromDb = _db.Tecaj.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.naziv = obj.naziv;
                objFromDb.opis = obj.opis;
                objFromDb.OsobaOib = obj.OsobaOib;
                objFromDb.cijena = obj.cijena;
                objFromDb.KategorijaId = obj.KategorijaId;
            }
        }
        public int Max()
        {
            var idFromDb = _db.Tecaj.Max(item => item.Id);
            return idFromDb;
        }
    }
}
