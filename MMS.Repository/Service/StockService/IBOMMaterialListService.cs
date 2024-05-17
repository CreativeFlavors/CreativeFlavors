using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service.StockService
{
   public interface IBOMMaterialListService
    {
        bool Post(BOMMaterialList arg);
        bool Put(BOMMaterialList arg);
        bool Delete(int id);
        BOMMaterialList Get(int id);
        List<BOMMaterialList> Get();
    }
}
