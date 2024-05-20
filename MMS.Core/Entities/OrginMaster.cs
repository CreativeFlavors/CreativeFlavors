using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("originmaster", Schema = "public")]
    public class OriginMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("originmasterid")]
        public int OriginMasterId { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("originname")]
        public string OriginName { get; set; }
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
