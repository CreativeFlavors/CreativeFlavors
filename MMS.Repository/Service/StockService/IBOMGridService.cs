using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service.StockService
{
    public interface IBOMGridService
    {
        bool Post(Bom arg);
        bool Put(Bom arg);
        bool BOMMaterialListPost(BOMMaterialList arg);
        
        bool Delete(int id);
        Bom Get(int id);
    }
}
