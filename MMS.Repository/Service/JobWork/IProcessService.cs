using MMS.Core.Entities.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service.JobWork
{
    public interface IProcessService
    {
        bool Post(ProcessMaster arg);
        bool Put(ProcessMaster arg);
        bool Delete(int id);
        ProcessMaster Get(int id);
        List<ProcessMaster> Get();
    }
}
