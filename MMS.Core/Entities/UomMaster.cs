using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("uommaster", Schema = "public")]
    public class UomMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("uommasterid")]
        public int UomMasterId { get; set; }
        [Column("shortunitname")]
        public string ShortUnitName { get; set; }
        [Column("longunitname")]
        public string LongUnitName { get; set; }
        // public DateTime? CreatedDate { get; set; }
        //  public DateTime? UpdatedDate { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }
    }
}
