using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("purchaseordersizerangequantity", Schema = "public")]
    public class PurchaseOrderSizeRangeQuantity : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("purchasesizerangeid")]
        public int PurchaseSizeRangeID { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("rate")]
        public decimal? Rate { get; set; }

        [Column("poorderid")]
        public int PoOrderID { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}
