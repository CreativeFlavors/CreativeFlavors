using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface ILeatherSizeService
    {
        bool Post(LeatherSizeMaster arg);
        bool Put(LeatherSizeMaster arg);
        bool Delete(int id);
        LeatherSizeMaster Get(int id);
        List<LeatherSizeMaster> Get();
    }
}
