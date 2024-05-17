using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
    public interface ISeasonService
    {
        SeasonMaster Post(SeasonMaster arg);
        bool Put(SeasonMaster arg);
        bool Delete(int id);
        SeasonMaster Get(int id);
        List<SeasonMaster> Get();

    }
}
