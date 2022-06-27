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
            NacinPlacanja = new NacinPlacanjaRepository(_db);
            Osoba = new OsobaRepository(_db);
            Tecaj = new TecajRepository(_db);
            OcjenaTecaja = new OcjenaTecajaRepository(_db);
            Videozapis = new VideozapisRepository(_db);
            Narudzba = new NarudzbaRepository(_db);
            Kategorija = new KategorijaRepository(_db);
        }
        public IRazinaPravaRepository RazinaPrava { get; private set; }
        public IStatusNarudzbeRepository StatusNarudzbe { get; private set; }
        public INacinPlacanjaRepository NacinPlacanja { get; private set; }
        public IOsobaRepository Osoba { get; private set; }
        public ITecajRepository Tecaj { get; private set; }
        public IOcjenaTecajaRepository OcjenaTecaja { get; private set; }
        public IVideozapisRepository Videozapis { get; private set; }
        public INarudzbaRepository Narudzba { get; private set; }
        public IKategorijaRepository Kategorija { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
