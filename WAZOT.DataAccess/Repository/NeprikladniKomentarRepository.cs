using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class NeprikladniKomentarRepository : Repository<Neprikladni_komentar>, INeprikladniKomentarRepository
    {
        private ApplicationDbContext _db;
        public NeprikladniKomentarRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Neprikladni_komentar obj)
        {
            var objFromDb = _db.NeprikladniKomentar.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.Ocjena_tecajaId = obj.Ocjena_tecajaId;
                objFromDb.PrijavaOsobaOib = obj.PrijavaOsobaOib;
                objFromDb.PrijavljenOsobaOib = obj.PrijavljenOsobaOib;
            }
        }
    }
}
