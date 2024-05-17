using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("bomsizeMatching", Schema = "public")]
    public class BomSizeMatching : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("bomsizeMatchingid")]
        public int BomSizeMatchingID { get; set; }

        [Column("bommaterialid")]
        public int BomMaterialID { get; set; }

        [Column("frame")]
        public decimal? Frame { get; set; }

        [Column("size")]
        public decimal? Size { get; set; }

        [Column("rate")]
        public decimal? Rate { get; set; }

        [Column("sizeschedulemasterid")]
        public int SizeScheduleMasterID { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("isdeleted")]
        public bool IsDeleted { get; set; }

        [Column("deletedon")]
        public DateTime? DeletedOn { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("qty")]
        public decimal? Qty { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
