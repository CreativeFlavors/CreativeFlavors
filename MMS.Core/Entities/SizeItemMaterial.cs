using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("sizeItemmaterial", Schema = "public")]
    public class SizeItemMaterial : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("sizemateriald")]
        public int SizeMaterialD { get; set; }
        [Column("qty")]
        public int Qty { get; set; }
        [Column("rate")]
        public decimal? Rate { get; set; }
        [Column("sizerange")]
        public string SizeRange { get; set; }
        [Column("materialmasterid")]
        public int? MaterialMasterID { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }
        [Column("deletedby")]
        public string DeletedBy { get; set; }
    }
}
