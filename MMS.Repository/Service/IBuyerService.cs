using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface IBuyerService
    {
        bool Post(BuyerMaster arg);
        bool Put(BuyerMaster arg);
        bool Delete(int id);
        BuyerMaster Get(int id);
    }
}
