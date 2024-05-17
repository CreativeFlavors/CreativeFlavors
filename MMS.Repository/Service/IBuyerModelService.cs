using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IBuyerModelService
    {
        bool Post(BuyerModel arg);
        bool Put(BuyerModel arg);
        bool Delete(int id);
        BuyerModel Get(int id);
        List<BuyerModel> Get();
    }
}
