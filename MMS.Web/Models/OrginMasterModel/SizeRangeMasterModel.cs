using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.OrginMasterModel
{
    public class SizeRangeMasterModel
    {
        public int SizeRangeMasterId { get; set; }
        public string SizeRangeRef { get; set; }
        public string SizeRangeRefValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }   
    }
}