using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IUOMService
    {
        UomMaster Post(UomMaster arg);
        bool Put(UomMaster arg);
        bool Delete(int id);
        UomMaster Get(int id);
        List<UomMaster> Get();
    }
}
