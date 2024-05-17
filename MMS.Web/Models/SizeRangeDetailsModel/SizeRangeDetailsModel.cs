using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SizeRangeDetailsModel
{
    public class SizeRangeDetailsModel
    {
        public int SizeRangeDetailsId { get; set; }
        public string SizeNo { get; set; }
        public string SizenRangeName { get; set; }
     //   public int SizeRangeMasterId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<SizeRangeDetails> SizeRangeDetailslsit { get; set; }
    }
}