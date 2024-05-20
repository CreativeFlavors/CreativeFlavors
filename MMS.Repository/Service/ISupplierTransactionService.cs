using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    internal interface ISuppliertransactionServices
    {
        bool Post(supplierTransaction arg);

        bool Put(supplierTransaction arg);

        List<supplierTransaction> Get();
    }
}
