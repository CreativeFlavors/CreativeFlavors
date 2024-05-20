using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMS.Core.Entities.Stock
{
    [Table("materialopeningsizeqtyrate", Schema = "public")]
    public class MaterialOpeningSizeQtyRate : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("materialopeningsizeqtyrateid")]
        public int MaterialOpeningSizeQtyRateId { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("quantity")]
        public string Quantity { get; set; }

        [Column("rate")]
        public decimal Rate { get; set; }

        [Column("materialopeningid")]
        public int MaterialOpeningId { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}
