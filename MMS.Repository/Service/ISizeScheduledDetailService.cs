using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public  interface ISizeScheduledDetailService
    {
        bool Post(SizeScheduleDetails arg);
        bool Put(SizeScheduleDetails arg);
        bool Delete(int id);
        SizeScheduleDetails Get(int id);
        List<SizeScheduleDetails> Get();
    }
}
