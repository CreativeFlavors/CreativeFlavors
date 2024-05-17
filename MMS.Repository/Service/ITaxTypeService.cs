using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface ITaxTypeService
    {
        bool Post(TaxTypeMaster arg);
        bool Put(TaxTypeMaster arg);
        bool Delete(int id);
        TaxTypeMaster Get(int id);
        List<TaxTypeMaster> Get();
    }
}
