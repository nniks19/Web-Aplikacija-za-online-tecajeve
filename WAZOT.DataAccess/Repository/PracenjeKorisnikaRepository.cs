using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class PracenjeKorisnikaRepository : Repository<Pracenje_Korisnika>, IPracenjeKorisnikaRepository
    {
        private ApplicationDbContext _db;
        public PracenjeKorisnikaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Pracenje_Korisnika obj)
        {
            var objFromDb = _db.Pracenje_Korisnika.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.OsobaOib = obj.OsobaOib;
                objFromDb.TecajId = obj.TecajId;
                objFromDb.brPosjeta = obj.brPosjeta;
                objFromDb.Datum_posjete = obj.Datum_posjete;
                objFromDb.brPokretanjaVideozapisa = obj.brPokretanjaVideozapisa;
            }
        }
    }
}
