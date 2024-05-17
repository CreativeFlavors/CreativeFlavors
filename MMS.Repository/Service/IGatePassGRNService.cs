using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
namespace MMS.Repository.Service
{
    public interface IGatePassGRNService
    {
        bool Post(GatePassGRNMaster arg);
        bool Put(GatePassGRNMaster arg);
        bool Delete(int id);
        GatePassGRNMaster Get(int id);
        List<GatePassGRNMaster> Get();
    }
}
