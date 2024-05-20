using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("colormaster", Schema = "public")]
    public class ColorMaster : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("colormasterid")]
        public int ColorMasterId { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("buyercolorcode")]
        public string BuyerColorCode { get; set; }
        [Column("colorimages")]
        public string ColorImages { get; set; }
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
