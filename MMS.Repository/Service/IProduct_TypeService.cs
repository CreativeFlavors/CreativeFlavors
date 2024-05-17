using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IProduct_TypeService
    {
        Product_Type Post(Product_Type arg);
        bool Put(Product_Type arg);
        bool Delete(int id);
        Product_Type Get(int id);
        List<Product_Type> Get();
    }
}
