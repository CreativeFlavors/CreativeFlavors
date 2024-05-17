using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;

namespace MMS.Repository.Service
{
   public interface ICurrencyExchangeService
    {
       //List<CurrencyExchangeMaster> Get();
       bool Post(CurrencyExchangeMaster arg);
       bool Put(CurrencyExchangeMaster arg);
       bool Delete(int id);
       CurrencyExchangeMaster Get(int id);
       List<CurrencyExchangeMaster> Get();
    }
}
