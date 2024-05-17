using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface ISizeScheduleMasterService
    {
        bool Post(SizeScheduleMaster arg);
        bool Put(SizeScheduleMaster arg);
        bool Delete(int id);
        SizeScheduleMaster Get(int id);
        List<SizeScheduleMaster> Get();
    }
}
