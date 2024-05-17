using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities.Stock
{
    [Table("multipleissueslip", Schema = "public")]
    public class MultipleIssueSlip : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("multipleissueslipid")]
        public int MultipleIssueSlipID { get; set; }

        [Column("issueslipno")]
        public string IssueSlipNo { get; set; }

        [Column("issuetype")]
        public int IssueType { get; set; }

        [Column("internaloderid")]
        public string InternalOderID { get; set; }

        [Column("styleno")]
        public string StyleNo { get; set; }

        [Column("lotno")]
        public string LotNo { get; set; }

        [Column("cuttingissuetype")]
        public int CuttingIssueType { get; set; }

        [Column("conveyorid")]
        public int ConveyorID { get; set; }

        [Column("machinename")]
        public int MachineName { get; set; }

        [Column("subtanceid")]
        public int SubtanceID { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("isjobwork")]
        public bool IsJobWork { get; set; }

        [Column("jobworktype_id")]
        public int? Jobworktype_Id { get; set; }

        [Column("gateout_no")]
        public string GateOut_No { get; set; }

        [Column("gateout_no_datetime")]
        public DateTime? GateOut_No_Datetime { get; set; }
    }
}
