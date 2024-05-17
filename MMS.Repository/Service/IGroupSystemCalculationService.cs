using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IGroupSystemCalculationService
    {
       bool Post(GroupSystemCalculation arg);
       bool Put(GroupSystemCalculation arg);
        bool Delete(int id);
        GroupSystemCalculation Get(int id);
        List<GroupSystemCalculation> Get();
    }
}
