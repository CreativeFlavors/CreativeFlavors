using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service.StockService
{
   public interface IApprovedPriceListService
    {
         
       bool Post(ApprovedPriceList arg);
       bool Put(ApprovedPriceList arg);
        
        bool Delete(int id);
        ApprovedPriceList Get(int id);
        List<ApprovedPriceList> Get();

       

    }
}
