using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface StageMasterService
    {
        bool Post(StageMaster arg);
        bool Put(StageMaster arg);
        bool Delete(int id);
        StageMaster Get(int id);
        List<StageMaster> Get();
    }
}
