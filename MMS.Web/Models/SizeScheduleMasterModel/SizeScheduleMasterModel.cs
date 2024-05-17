using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Web.Controllers.Report.StoredProcedureModel;
namespace MMS.Web.Models.SizeScheduleMasterModel
{
    public class SizeScheduleMasterModel
    {
        public int SizeScheduleMasterId { get; set; }
        public string SizeMatchingNo { get; set; }
        public string SizeRangeName { get; set; }
        public string FromValue { get; set; }
        public string ToValue { get; set; }
        public string Equals { get; set; }
        public int SketchNo { get; set; }
        public int MaterialName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string jsonOfLog { get; set; }
        public int? ShortUnitID { get; set; }
        public List<SizeScheduleRange> sizeScheduleRangeList { get; set; }
        public List<SizeScheduleMaster> SizeScheduleMasterList { get; set; }
    }
}