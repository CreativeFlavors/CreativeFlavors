using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
   public interface ISkillMasterService
    {
        bool Post(SkillMaster arg);
        bool Put(SkillMaster arg);
        bool Delete(int id);
        SkillMaster Get(int id);
        List<SkillMaster> Get();
    }
}
