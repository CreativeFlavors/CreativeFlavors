using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.AttendanceIncentiveModel
{
    public class AttendanceIncentiveModel
    {
        public int AttendanceIncentiveCalcualtionId { get; set; }
        public int Leave { get; set; }
        public int Absent { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<AttendanceIncentiveCalculation> AttendanceIncentiveCalculationList { get; set; }
    }
}