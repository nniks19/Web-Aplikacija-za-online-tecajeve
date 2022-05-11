using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Repository;
using WAZOT.Repository.IRepository;

namespace WAZOT.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            RazinaPrava = new RazinaPravaRepository(_db);
            StatusNarudzbe = new StatusNarudzbeRepository(_db);
        }
        public IRazinaPravaRepository RazinaPrava { get; private set; }
        public IStatusNarudzbeRepository StatusNarudzbe { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
