using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities.Stock
{
    [Table("sizeitemsissueslip", Schema = "public")]
    public class SizeItemsIssueSlip : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("sizemateriald")]
        public int SizeMaterialD { get; set; }

        [Column("qty")]
        public decimal? Qty { get; set; }

        [Column("rate")]
        public decimal? Rate { get; set; }

        [Column("sizerange")]
        public string SizeRange { get; set; }

        [Column("multipleissueslipfk")]
        public int? MultipleIssueslipFk { get; set; }

        [Column("createdby")]
        public string CreatedBY { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }
    }
   
}
