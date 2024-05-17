using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SizeRangeMasterModel
{
    public class SizeRangeMasterModel
    {
        public int SizeRangeMasterId { get; set; }
        public string SizeRangeRef { get; set; }
        public string SizeRangeRefValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string SizeRangeHidden { get; set; }
       // public List<SizeRange> listSizeRange { get; set; }
        //public List<string[]> SizeRangeRef_ { get; set; }
      
        public List<SizeRangeMaster> SizeRangeMasterList { get; set; }
    }
    public class SizeRange
    {
        public string SizeRangeRef_ { get; set; }
        public string SizeRangeRefValue_ { get; set; }
    }
}