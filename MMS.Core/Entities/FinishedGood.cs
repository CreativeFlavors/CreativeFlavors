using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("finishedgood", Schema = "public")]
    public class FinishedGood : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Column("productcode")]
        public string ProductCode { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("cost")]
        public decimal Cost { get; set; }

        [Column("storecode")]
        public int StoreCode { get; set; }

        [Column("quantity")]
        public decimal Quantity { get; set; }

        [Column("quantitylock")]
        public string QuantityLock { get; set; }

        [Column("quantitylockreftime")]
        public DateTime? QuantityLockRefTime { get; set; }

        [Column("quantitylockreleaseat")]
        public string QuantityLockReleaseAt { get; set; }

        [Column("purchaseuom")]
        public string PurchaseUOM { get; set; }

        [Column("saleuom")]
        public string SaleUOM { get; set; }

        [Column("lasttransno")]
        public string LastTransNo { get; set; }

        [Column("lasttransqty")]
        public decimal LastTransQty { get; set; }

        [Column("lasttransdate")]
        public DateTime? LastTransDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("batchcode")]
        public string Batchcode { get; set; }

        [Column("producttype")]
        public int ProductType { get; set; }
    }
}
