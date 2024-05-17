using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface IContryService
    {
        bool Post(CountryMaster arg);
        bool Put(CountryMaster arg);
        bool Delete(int id);
        CountryMaster Get(int id);
        List<CountryMaster> Get();
    }
}
