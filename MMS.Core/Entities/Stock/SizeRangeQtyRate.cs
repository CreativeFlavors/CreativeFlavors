using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("sizerangeqtyrate", Schema = "public")]
    public class SizeRangeQtyRate : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("sizeqrid")]
        public int SizeQRId { get; set; }
        [Column("qty")]
        public decimal? Qty { get; set; }
        [Column("rate")]
        public decimal Rate { get; set; }
        [Column("sizerange")]
        public string SizeRange { get; set; }
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
