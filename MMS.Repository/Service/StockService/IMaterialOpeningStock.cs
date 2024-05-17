using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IMaterialOpeningStock
    {
        bool Post(MaterialOpeningMaster arg);
        bool Put(MaterialOpeningMaster arg);
        bool Delete(int id);
        MaterialOpeningMaster Get(int id);
        List<MaterialOpeningMaster> Get();
    }
}
