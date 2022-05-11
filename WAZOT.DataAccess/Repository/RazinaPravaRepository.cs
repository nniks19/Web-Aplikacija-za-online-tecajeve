using WAZOT.DataAccess;
using WAZOT.Models;
using WAZOT.Repository.IRepository;

namespace WAZOT.Repository
{
    public class RazinaPravaRepository : Repository<Razina_Prava>, IRazinaPravaRepository
    {
        private ApplicationDbContext _db;
        public RazinaPravaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Razina_Prava obj)
        {
            _db.Razina_Prava.Update(obj);
        }
    }
}
