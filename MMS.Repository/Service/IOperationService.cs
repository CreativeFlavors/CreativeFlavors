using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;

namespace MMS.Repository.Service
{
    public interface IOperationService
    {
        OperationMaster Post(OperationMaster arg);
        bool Put(OperationMaster arg);
        bool Delete(int id);
        OperationMaster Get(int id);
        List<OperationMaster> Get();
    }
}
