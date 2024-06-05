using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StatusHistory
{
    public class StatusHistoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int? InitiatedCode { get; set; }
        public DateTime? InitiatedDate { get; set; }
        public string InitiatedBy { get; set; }
        public int? InProgressCode { get; set; }
        public DateTime? InProgressDate { get; set; }
        public string InProgressBy { get; set; }
        public int? PendingCode { get; set; }
        public DateTime? PendingDate { get; set; }
        public string PendingBy { get; set; }
        public int? QualityCheckingCode { get; set; }
        public DateTime? QualityCheckingDate { get; set; }
        public string QualityCheckingBy { get; set; }
        public int? SequenceCode { get; set; }
        public DateTime? SequenceDate { get; set; }
        public string SequenceBy { get; set; }
        public int? PackingCode { get; set; }
        public DateTime? PackingDate { get; set; }
        public string PackingBy { get; set; }

    }
}