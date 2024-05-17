using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IStoreMasterService
    {
        bool Post(StoreMaster arg);
        bool Put(StoreMaster arg);
        bool Delete(int id);
        StoreMaster Get(int id);
        List<StoreMaster> Get();
    }
}
