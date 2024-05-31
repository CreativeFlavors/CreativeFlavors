using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IsubassemblyServices
    {
        subassembly Post(subassembly arg);
        bool Put(subassembly arg);
        bool Delete(int id);
    }
}
