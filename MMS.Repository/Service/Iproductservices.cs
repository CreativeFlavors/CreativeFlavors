using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    internal interface Iproductservices
    {
        bool Post(product arg);

        bool Delete(int productid);
        List<product> Get();
    }
}
