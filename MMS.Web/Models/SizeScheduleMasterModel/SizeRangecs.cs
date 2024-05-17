using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SizeScheduleMasterModel
{
    public class SizeRangecs
    {
        public int SizeScheduleID { get; set; }
        public int SizeScheduleMasterID { get; set; }
        public string Frame { get; set; }
        public decimal? Size { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }

    }
}