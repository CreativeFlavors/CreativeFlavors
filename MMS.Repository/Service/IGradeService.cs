using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities;

namespace MMS.Repository.Service
{
   public interface IGradeService
    {
        bool Post(GradeMaster arg);
        bool Put(GradeMaster arg);
        bool Delete(int id);
        GradeMaster Get(int id);
        List<GradeMaster> Get();

    }
}
