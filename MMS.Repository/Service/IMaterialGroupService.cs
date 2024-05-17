using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IMaterialGroupService
    {
        bool Post(materialgroupmaster arg);
        bool Put(materialgroupmaster arg);
        bool Delete(int id);
        materialgroupmaster Get(int id);
        List<materialgroupmaster> Get();
    }
}
