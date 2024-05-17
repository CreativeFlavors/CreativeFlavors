using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IMaterialMaster
    {
        bool Post(MaterialMaster arg);
        bool Put(MaterialMaster arg);
        bool Delete(int id);
        MaterialMaster Get(int id);
        List<MaterialMaster> Get();
    }
}
