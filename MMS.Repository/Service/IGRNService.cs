using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IGRNService
    {
        bool Post(GrnTypeMaster arg);
        bool Put(GrnTypeMaster arg);
        bool Delete(int id);
        GrnTypeMaster Get(int id);
        List<GrnTypeMaster> Get();
    }
}
