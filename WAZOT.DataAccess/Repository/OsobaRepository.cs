using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class OsobaRepository : Repository<Osoba>, IOsobaRepository
    {
        private ApplicationDbContext _db;
        public OsobaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Osoba obj)
        {
            var objFromDb = _db.Osoba.FirstOrDefault(u =>u.Oib == obj.Oib);
            if (objFromDb != null)
            {
                objFromDb.Oib = obj.Oib;
                objFromDb.ime = obj.ime;
                objFromDb.prezime = obj.prezime;
                objFromDb.email = obj.email;
                objFromDb.lozinka = obj.lozinka;
                objFromDb.Razina_PravaId = obj.Razina_PravaId;
                objFromDb.odobreno = obj.odobreno;
                objFromDb.pin = obj.pin;
            }
        }
    }
}
