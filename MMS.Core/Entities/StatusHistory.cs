using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class StatusHistory : BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("productcode")]
        public string ProductCode { get; set; }

        [Column("initiatedcode")]
        public int? InitiatedCode { get; set; }

        [Column("initiateddate")]
        public DateTime? InitiatedDate { get; set; }

        [Column("initiatedby")]
        public string InitiatedBy { get; set; }

        [Column("inprogresscode")]
        public int? InProgressCode { get; set; }

        [Column("inprogressdate")]
        public DateTime? InProgressDate { get; set; }

        [Column("inprogressby")]
        public string InProgressBy { get; set; }

        [Column("pendingcode")]
        public int? PendingCode { get; set; }

        [Column("pendingdate")]
        public DateTime? PendingDate { get; set; }

        [Column("pendingby")]
        public string PendingBy { get; set; }

        [Column("qualitycheckingcode")]
        public int? QualityCheckingCode { get; set; }

        [Column("qualitycheckingdate")]
        public DateTime? QualityCheckingDate { get; set; }

        [Column("qualitycheckingby")]
        public string QualityCheckingBy { get; set; }

        [Column("sequencecode")]
        public int? SequenceCode { get; set; }

        [Column("sequencedate")]
        public DateTime? SequenceDate { get; set; }

        [Column("sequenceby")]
        public string SequenceBy { get; set; }

        [Column("packingcode")]
        public int? PackingCode { get; set; }

        [Column("packingdate")]
        public DateTime? PackingDate { get; set; }

        [Column("packingby")]
        public string PackingBy { get; set; }

        [NotMapped]
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [NotMapped]
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
