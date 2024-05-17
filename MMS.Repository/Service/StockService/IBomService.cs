using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service.StockService
{
    public interface IBomService
    {
        bool Post(BillOfMaterial arg);
        bool Put(BillOfMaterial arg);
       // bool BOMMaterialListPost(BOMMaterialList arg);
        bool Delete(int id);
        BillOfMaterial Get(int id);

    }
}
