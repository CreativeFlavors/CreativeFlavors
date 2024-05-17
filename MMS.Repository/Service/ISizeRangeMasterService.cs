using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface ISizeRangeMasterService
    {
       SizeRangeMaster Post(SizeRangeMaster arg);
        bool Put(SizeRangeMaster arg);
        bool Delete(int id);
        SizeRangeMaster Get(int id);
        List<SizeRangeMaster> Get();
    }
}
