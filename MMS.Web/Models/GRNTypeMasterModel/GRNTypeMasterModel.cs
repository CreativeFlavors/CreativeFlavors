using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GRNTypeMasterModel
{
    public class GRNTypeMasterModel
    {
        public int GrnTypeMasterId { get; set; }
        public string IssueType { get; set; }
        public string GRNCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<GrnTypeMaster> GRNTypeList { get; set; }
    }
}