using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class PrijavaNaTecajRepository : Repository<Prijava_Na_Tecaj>, IPrijavaNaTecajRepository
    {
        private ApplicationDbContext _db;
        public PrijavaNaTecajRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Prijava_Na_Tecaj obj)
        {
            var objFromDb = _db.PrijavaNaTecaj.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.OsobaOib = obj.OsobaOib;
                objFromDb.Status_PrijaveId = obj.Status_PrijaveId;
                objFromDb.datum_pocetak = obj.datum_pocetak;
                objFromDb.datum_zavrsetak = obj.datum_zavrsetak;
                objFromDb.Id = obj.Id;
                objFromDb.TecajId = obj.TecajId;
            }
        }
    }
}
