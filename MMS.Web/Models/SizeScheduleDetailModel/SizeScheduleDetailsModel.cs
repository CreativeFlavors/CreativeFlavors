using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SizeScheduleDetailModel
{
    public class SizeScheduleDetailsModel
    {
        public int SizeScheduleDetailsId { get; set; }
        public int SizeNo { get; set; }
        public decimal Size { get; set; }
        public int SizeScheduleMasterId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<SizeScheduleDetails> SizeScheduleDetailsList { get; set; }
    }
}