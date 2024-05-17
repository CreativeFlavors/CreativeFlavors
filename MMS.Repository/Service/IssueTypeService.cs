using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IssueTypeService
    {
       IssueTypeMaster Post(IssueTypeMaster arg);
        bool Put(IssueTypeMaster arg);
        bool Delete(int id);
        IssueTypeMaster Get(int id);
        List<IssueTypeMaster> Get();
    }
}
