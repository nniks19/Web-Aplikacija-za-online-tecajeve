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
            StatusPrijave = new StatusPrijaveRepository(_db);
            Osoba = new OsobaRepository(_db);
            Tecaj = new TecajRepository(_db);
            OcjenaTecaja = new OcjenaTecajaRepository(_db);
            Videozapis = new VideozapisRepository(_db);
            PrijavaNaTecaj = new PrijavaNaTecajRepository(_db);
            Kategorija = new KategorijaRepository(_db);
            PracenjeKorisnika = new PracenjeKorisnikaRepository(_db);
            CjelinaTecaja = new CjelinaTecajaRepository(_db);
            Razgovor = new RazgovorRepository(_db);
            Poruka = new PorukaRepository(_db);
            NeprikladniKomentar = new NeprikladniKomentarRepository(_db);
        }
        public IRazinaPravaRepository RazinaPrava { get; private set; }
        public IStatusPrijaveRepository StatusPrijave { get; private set; }
        public IOsobaRepository Osoba { get; private set; }
        public ITecajRepository Tecaj { get; private set; }
        public IPracenjeKorisnikaRepository PracenjeKorisnika { get; private set; }
        public IOcjenaTecajaRepository OcjenaTecaja { get; private set; }
        public IVideozapisRepository Videozapis { get; private set; }
        public IPrijavaNaTecajRepository PrijavaNaTecaj { get; private set; }
        public IKategorijaRepository Kategorija { get; private set; }
        public ICjelinaTecajaRepository CjelinaTecaja { get; private set; }
        public IRazgovorRepository Razgovor { get; private set; }
        public IPorukaRepository Poruka { get; private set; }
        public INeprikladniKomentarRepository NeprikladniKomentar{ get; private set;}

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
