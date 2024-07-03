using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IProductionSubassemblyServices
    {
        bool Post(Productionsubassembly arg);
        bool Put(Productionsubassembly arg);
    }
}
