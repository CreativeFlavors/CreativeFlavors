using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface ISupplierService
    {
        bool Post(SupplierMaster arg);
        bool Put(SupplierMaster arg);
        bool Delete(int id);
        SupplierMaster Get(int id);
        List<SupplierMaster> Get();
    }
}
