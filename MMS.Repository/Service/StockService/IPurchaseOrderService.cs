using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service.StockService
{
  public  interface IPurchaseOrderService
    {
        bool Post(PurchaseOrder arg);
        bool Put(PurchaseOrder arg);
        bool Delete(int id);
        PurchaseOrder Get(int id);
        List<PurchaseOrder> Get();
    }
}
