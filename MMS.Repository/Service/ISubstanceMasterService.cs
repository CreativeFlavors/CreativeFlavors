using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MMS.Repository.Service
{
    public interface ISubstanceMasterService
    {
        bool Post(LeatherSizeMaster arg);
        bool Put(LeatherSizeMaster arg);
        bool Delete(int id);
        SubstanceMaster Get(int id);
        List<SubstanceMaster> Get();
    }
}
