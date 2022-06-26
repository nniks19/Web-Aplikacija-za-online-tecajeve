using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class NarudzbaRepository : Repository<Narudzba>, INarudzbaRepository
    {
        private ApplicationDbContext _db;
        public NarudzbaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Narudzba obj)
        {
            var objFromDb = _db.Narudzba.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.OsobaOib = obj.OsobaOib;
                objFromDb.Status_NarudzbeId = obj.Status_NarudzbeId;
                objFromDb.Nacin_PlacanjaId = obj.Nacin_PlacanjaId;
                objFromDb.datum_pocetak = obj.datum_pocetak;
                objFromDb.datum_zavrsetak = obj.datum_zavrsetak;
                objFromDb.Id = obj.Id;
                objFromDb.TecajId = obj.TecajId;
            }
        }
    }
}
