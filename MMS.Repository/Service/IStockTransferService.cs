using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;

namespace MMS.Repository.Service
{
   public interface IStockTransferService
    {
        bool Post(StockTransferMaster arg);
        bool Put(StockTransferMaster arg);
        bool Delete(int id);
        StockTransferMaster Get(int id);
        List<StockTransferMaster> Get();
    }
}
