using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IParentbomServices
    {
        parentbom Post(parentbom arg);
        bool Put(parentbom arg);
        bool Delete(int id);
    }
}
