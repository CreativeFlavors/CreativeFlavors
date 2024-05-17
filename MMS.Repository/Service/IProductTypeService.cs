using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IProductTypeService
    {
       ProductType Post(ProductType arg);
        bool Put(ProductType arg);
        bool Delete(int id);
        ProductType Get(int id);
        List<ProductType> Get();
    }
}
