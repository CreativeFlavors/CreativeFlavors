using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
namespace MMS.Repository.Service
{
    public interface IGateService
    {
        bool Post(GatePassMaster arg);
        bool Put(GatePassMaster arg);
        bool Delete(int id);
        GatePassMaster Get(int id);
        List<GatePassMaster> Get();
    }
}
