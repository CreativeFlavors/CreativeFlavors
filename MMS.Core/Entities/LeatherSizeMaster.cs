using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("leathersizemaster", Schema = "public")]
    public class LeatherSizeMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("leathersizemasterid")]
        public int LeatherSizeMasterId { get; set; }

        [Column("length")]
        public string Length { get; set; }

        [Column("width")]
        public string Width { get; set; }

        [Column("average")]
        public string Average { get; set; }
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
