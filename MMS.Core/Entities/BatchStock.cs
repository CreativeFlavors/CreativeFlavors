using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("grndetails", Schema = "public")]
    public class BatchStock : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("batchstockid")]
        public int BatchStockId { get; set; }
        [Column("supplierid")]
        public int SupplierId { get; set; }
        [Column("storecode")]
        public int StoreCode { get; set; }
        [Column("productid")]
        public int productid { get; set; }
        [Column("batchcode")]
        public string BatchCode { get; set; }
        [Column("altbatchcode")]
        public string AltBatchCode { get; set; }
        [Column("expirydate")]
        public DateTime? ExpiryDate { get; set; }
        [Column("quantity")]
        public decimal? Quantity { get; set; }
        [Column("grnnumber")]
        public int GrnNumber { get; set; }
        [Column("grndate")]
        public DateTime? GrnDate { get; set; }
        [Column("grndetailid")]
        public int GrnDetailId { get; set; }
        [Column("price")]
        public decimal? Price { get; set; }
        [Column("cost")]
        public decimal? Cost { get; set; }
        [Column("taxcode")]
        public int TaxCode { get; set; }
        [Column("uomid")]
        public int UomId { get; set; }
        [Column("reservedqty")]
        public decimal? ReservedQty { get; set; }
        [Column("lasttransmode")]
        public string LastTransMode { get; set; }
        [Column("lasttransnumber")]
        public string LastTransNumber { get; set; }
        [Column("lasttransdate")]
        public DateTime? LastTransDate { get; set; }
        [Column("lasttransqty")]
        public decimal? LastTransQty { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("status")]
        public int status { get; set; }
        [Column("producttype")]
        public int producttype { get; set; }
    }
}
