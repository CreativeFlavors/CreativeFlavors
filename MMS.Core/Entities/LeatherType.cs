using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities
{
    [Table("leathertype", Schema = "public")]
    public class LeatherType : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("leathertypeid")]
        public int LeatherTypeID { get; set; }

        [Column("leathertypecode")]
        public string LeatherTypeCode { get; set; }

        [Column("leathertypedescription")]
        public string LeatherTypeDescription { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("deletedon")]
        public DateTime? DeletedON { get; set; }
    }
}
