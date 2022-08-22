using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class CjelinaTecajaRepository : Repository<Cjelina_tecaja>, ICjelinaTecajaRepository
    {
        private ApplicationDbContext _db;
        public CjelinaTecajaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Cjelina_tecaja obj)
        {
            var objFromDb = _db.CjelinaTecaja.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.TecajId = obj.TecajId;
                objFromDb.naziv_cjeline = obj.naziv_cjeline;
                objFromDb.opis_cjeline = obj.opis_cjeline;
            }
        }
    }
}
