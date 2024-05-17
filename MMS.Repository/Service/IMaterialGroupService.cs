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
        bool Post(MaterialGroupMaster_ arg);
        bool Put(MaterialGroupMaster_ arg);
        bool Delete(int id);
        MaterialGroupMaster_ Get(int id);
        List<MaterialGroupMaster_> Get();
    }
}
