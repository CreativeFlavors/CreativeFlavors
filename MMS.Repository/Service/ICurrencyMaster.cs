using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface ICurrencyService
    {
        bool Post(CurrencyMaster arg);
        bool Put(CurrencyMaster arg);
        bool Delete(int id);
        CurrencyMaster Get(int id);
        List<CurrencyMaster> Get();
    }
}
