using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class OcjenaTecajaRepository : Repository<Ocjena_tecaja>, IOcjenaTecajaRepository
    {
        private ApplicationDbContext _db;
        public OcjenaTecajaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Ocjena_tecaja obj)
        {
            var objFromDb = _db.OcjeneTecaja.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.komentar = obj.komentar;
                objFromDb.ocjena = obj.ocjena;
                objFromDb.OsobaOib = obj.OsobaOib;
                objFromDb.TecajId = obj.TecajId;
                objFromDb.CjelinaTecajaId = obj.CjelinaTecajaId;
            }
        }
    }
}
