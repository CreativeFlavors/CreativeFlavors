using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("displaysizematerial", Schema = "public")]
    public class DisplaySizeMaterial:BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("displaysizemateriald")]
        public int DisplaySizeMaterialD { get; set; }
        [Column("sizeischecked")]
        public bool SizeIsChecked { get; set; }
        [Column("sizerange")]
        public string SizeRange { get; set; }
        [Column("bommaterialid")]
        public int? BOMmaterialID { get; set; }
        [Column("createdby")]
        public string CreatedBY { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
    }
}
