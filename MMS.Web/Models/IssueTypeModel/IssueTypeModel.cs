using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.IssueTypeModel
{
    public class IssueTypeModel
    {
        public int IssueTypeMasterId { get; set; }
        public string IssueType { get; set; }
        public string IssueTypeCode { get; set; }
        //public int IssueTypeNO { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<IssueTypeMaster> IssueTypeMasterList { get; set; }
    }
}