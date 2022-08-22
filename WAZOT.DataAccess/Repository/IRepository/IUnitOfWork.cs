using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAZOT.Repository.IRepository;

namespace WAZOT.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRazinaPravaRepository RazinaPrava { get; }
        IStatusPrijaveRepository StatusPrijave { get; }
        IOsobaRepository Osoba { get; }
        ITecajRepository Tecaj { get; }
        IOcjenaTecajaRepository OcjenaTecaja { get; }
        IVideozapisRepository Videozapis { get; }
        IPrijavaNaTecajRepository PrijavaNaTecaj { get; }
        IKategorijaRepository Kategorija { get; }
        IPracenjeKorisnikaRepository PracenjeKorisnika { get; }
        ICjelinaTecajaRepository CjelinaTecaja { get; }
        IRazgovorRepository Razgovor { get; }
        IPorukaRepository Poruka { get; }
        INeprikladniKomentarRepository NeprikladniKomentar { get; }
        void Save();
    }
}
