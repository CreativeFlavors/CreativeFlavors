using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public  interface AgentService
    {
        bool Post(AgentMaster arg);
        bool Put(AgentMaster arg);
        bool Delete(int id);
        AgentMaster Get(int id);
        List<AgentMaster> Get();
    }
}
