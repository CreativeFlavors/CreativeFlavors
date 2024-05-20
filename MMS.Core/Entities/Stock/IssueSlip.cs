using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities.Stock
{
    [Table("singleissueslip", Schema = "public")]
    public class singleissueslip : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("issueslipid")]
        public int IssueSlipID { get; set; }

        [Column("issueslipno")]
        public string IssueSlipNo { get; set; }

        [Column("internaloderid")]
        public string InternalOderID { get; set; }

        [Column("styleno")]
        public string StyleNo { get; set; }

        [Column("conveyorid")]
        public int ConveyorID { get; set; }

        [Column("machinename")]
        public int MachineName { get; set; }

        [Column("subtanceid")]
        public int SubtanceID { get; set; }

        [Column("currentstock")]
        public decimal CurrentStock { get; set; }

        [Column("currentstocktype")]
        public int CurrentStockType { get; set; }

        [Column("freestock")]
        public decimal FreeStock { get; set; }

        [Column("freestocktype")]
        public int FreeStockType { get; set; }

        [Column("reserverqty")]
        public decimal ReserverQty { get; set; }

        [Column("reserverqtytype")]
        public int ReserverQtyType { get; set; }

        [Column("closingstockinallstores")]
        public decimal ClosingStockinAllStores { get; set; }

        [Column("closingstockinallstoredtype")]
        public int ClosingStockinAllStoredType { get; set; }

        [Column("intransitvalue")]
        public decimal InTransitValue { get; set; }

        [Column("intransittype")]
        public int InTransitType { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
