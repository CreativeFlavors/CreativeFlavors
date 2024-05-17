using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GradeMasterModel
{
    public class GradeMasterModel
    {
        public int GradeMasterID { get; set; }
        public string CodeNo { get; set; }
        public string Category { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Efficiency { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<GradeMaster> GradeMasterList { get; set; }
    }
}