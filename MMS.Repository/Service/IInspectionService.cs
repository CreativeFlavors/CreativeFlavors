using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;
namespace MMS.Repository.Service
{
    public interface IInspectionService
    {
        InspectionTypeMaster Post(InspectionTypeMaster arg);
        bool Put(InspectionTypeMaster arg);
        bool Delete(int id);
        InspectionTypeMaster Get(int id);
        List<InspectionTypeMaster> Get();
    }
}
