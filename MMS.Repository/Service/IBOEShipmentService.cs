using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;

namespace MMS.Repository.Service
{
   public interface IBOEShipmentService
    {
        bool Post(BOEShipmentMaster arg);
        bool Put(BOEShipmentMaster arg);
        bool Delete(int id);
        BOEShipmentMaster Get(int id);
        List<BOEShipmentMaster> Get();
    }
}
