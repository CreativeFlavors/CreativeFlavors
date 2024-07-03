using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StatusHistorySubassemblyModel
{
    public class StatusHistorySubassemblyModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int? StartedCode { get; set; }
        public DateTime? StartedDate { get; set; }
        public string StartedBy { get; set; }
        public int? InProgressCode { get; set; }
        public DateTime? InProgressDate { get; set; }
        public string InProgressBy { get; set; }
        public int? QualityInspectionCode { get; set; }
        public DateTime? QualityInspectionDate { get; set; }
        public string QualityInspectionBy { get; set; }
        public int? CompletedCode { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string CompletedBy { get; set; }
        public int? PendingApprovalCode { get; set; }
        public DateTime? PendingApprovalDate { get; set; }
        public string PendingApprovalBy { get; set; }
        public int? ReleasedForAssemblyCode { get; set; }
        public DateTime? ReleasedForAssemblyDate { get; set; }
        public string ReleasedForAssemblyBy { get; set; }
    }
}