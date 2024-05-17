using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public  interface CommisionCritiriaService
    {
        bool Post(CommisionCriteria arg);
        bool Put(CommisionCriteria arg);
        bool Delete(int id);
        CommisionCriteria Get(int id);
        List<CommisionCriteria> Get();
    }
}
