using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IOrginMasterService
    {
       OrginMaster Post(OrginMaster arg);
        bool Put(OrginMaster arg);
        bool Delete(int id);
        OrginMaster Get(int id);
        List<OrginMaster> Get();
    }
}
