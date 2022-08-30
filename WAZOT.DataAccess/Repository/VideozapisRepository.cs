using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class VideozapisRepository : Repository<Videozapis>, IVideozapisRepository
    {
        private ApplicationDbContext _db;
        public VideozapisRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Videozapis obj)
        {
            var objFromDb = _db.Videozapis.FirstOrDefault(u =>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.videozapis_tip = obj.videozapis_tip;
                objFromDb.videozapis_putanja = obj.videozapis_putanja;
                objFromDb.videozapis_naziv = obj.videozapis_naziv;
                objFromDb.TecajId = obj.TecajId;
                objFromDb.Cjelina_TecajaId = obj.Cjelina_TecajaId;
            }
        }
        public int Max()
        {
            var idFromDb = _db.Videozapis.Max(item => item.Id);
            return idFromDb;
        }
    }
}
