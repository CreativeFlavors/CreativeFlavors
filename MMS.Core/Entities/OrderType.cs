using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("ordertype", Schema = "public")]
    public class OrderType : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("ordertypeid")]
        public int OrderTypeID { get; set; }

        [Column("ordertypecode")]
        public string OrderTypeCode { get; set; }

        [Column("ordertypename")]
        public string OrderTypeName { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool? IsDeleted { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
    }
}
