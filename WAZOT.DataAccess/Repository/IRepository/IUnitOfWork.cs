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
        IStatusNarudzbeRepository StatusNarudzbe { get; }
        void Save();
    }
}
