using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public interface IMachineryMasterService
    {
      MachineryMaster Post(MachineryMaster arg);
        bool Put(MachineryMaster arg);
        bool Delete(int id);
        MachineryMaster Get(int id);
        List<MachineryMaster> Get();
    }
}
