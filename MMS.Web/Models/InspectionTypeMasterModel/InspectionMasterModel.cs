using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;
namespace MMS.Web.Models.InspectionTypeMasterModel
{
    public class InspectionTypeMasterModel
    {
        public int InspectionTypeMasterId { get; set; }        
        public int Inspection { get; set; }
        public string InspectionReportName { get; set; }
        public int OperationId { get; set; }
        public string Code { get; set; }
        public string Parameter { get; set; }
        public string RadioButtonValue { get; set; }
        public string InspectionFrequency { get; set; }
        public string Type { get; set; }
        public string CommonCause { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<InspectionTypeMaster> InspectionTypeMasteList { get; set; }
    }
}