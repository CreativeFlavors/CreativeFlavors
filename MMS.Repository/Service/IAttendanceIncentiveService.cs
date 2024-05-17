using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Service
{
  public  interface IAttendanceIncentiveService
    {
        bool Post(AttendanceIncentiveCalculation arg);
        bool Put(AttendanceIncentiveCalculation arg);
        bool Delete(int id);
        AttendanceIncentiveCalculation Get(int id);
        List<AttendanceIncentiveCalculation> Get();
    }
}
