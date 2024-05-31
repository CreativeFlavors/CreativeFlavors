using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IFinishedGoodService
    {
        bool Post(FinishedGood arg);
        bool Put(FinishedGood arg);
        //Production Getproductionid(int? productionid);
    }
}
