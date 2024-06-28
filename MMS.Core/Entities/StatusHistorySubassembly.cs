using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class StatusHistorySubassembly : BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("productcode")]
        public string ProductCode { get; set; }

        [Column("startedcode")]
        public int? StartedCode { get; set; }

        [Column("starteddate")]
        public DateTime? StartedDate { get; set; }

        [Column("startedby")]
        public string StartedBy { get; set; }

        [Column("inprogresscode")]
        public int? InProgressCode { get; set; }

        [Column("inprogressdate")]
        public DateTime? InProgressDate { get; set; }

        [Column("inprogressby")]
        public string InProgressBy { get; set; }

        [Column("qualityinspectioncode")]
        public int? QualityInspectionCode { get; set; }

        [Column("qualityinspectiondate")]
        public DateTime? QualityInspectionDate { get; set; }

        [Column("qualityinspectionby")]
        public string QualityInspectionBy { get; set; }

        [Column("completedcode")]
        public int? CompletedCode { get; set; }

        [Column("completeddate")]
        public DateTime? CompletedDate { get; set; }

        [Column("completedby")]
        public string CompletedBy { get; set; }

        [Column("pendingapprovalcode")]
        public int? PendingApprovalCode { get; set; }

        [Column("pendingapprovaldate")]
        public DateTime? PendingApprovalDate { get; set; }

        [Column("pendingapprovalby")]
        public string PendingApprovalBy { get; set; }

        [Column("releasedforassemblycode")]
        public int? ReleasedForAssemblyCode { get; set; }

        [Column("releasedforassemblydate")]
        public DateTime? ReleasedForAssemblyDate { get; set; }

        [Column("releasedforassemblyby")]
        public string ReleasedForAssemblyBy { get; set; }

        [NotMapped]
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
