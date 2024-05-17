using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IconveyorService
    {
        bool Post(ConveyorMaster arg);
        bool Put(ConveyorMaster arg);
        bool Delete(int id);
        ConveyorMaster Get(int id);
        List<ConveyorMaster> Get();
    }
}
