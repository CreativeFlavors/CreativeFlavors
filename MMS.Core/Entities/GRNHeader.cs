using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("grnheader", Schema = "public")]
    public class GRNHeader :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("grnheaderid")]
        public int GrnHeaderId { get; set; }
        [Column("supplierid")]
        public int SupplierId { get; set; }
        [Column("grndate")]
        public DateTime GrnDate { get; set; }
        [Column("refinvoicenumber")]
        public string RefInvoiceNumber { get; set; }
        [Column("refinvoicedate")]
        public DateTime? RefInvoiceDate { get; set; }
        [Column("ponumber")]
        public int PoNumber { get; set; } 
        [Column("currencyid")]
        public int currencyid { get; set; }
        [Column("podate")]
        public DateTime? PoDate { get; set; }
        [Column("storecode")]
        public int StoreCode { get; set; }
        [Column("items")]
        public int Items { get; set; }
        [Column("quantity")]
        public decimal? Quantity { get; set; }
        [Column("total_unitprice")]
        public decimal? total_unitprice { get; set; }
        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }
        [Column("subtotalvalue")]
        public decimal? SubtotalValue { get; set; }
        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }
        [Column("totalvalue")]
        public decimal? TotalValue { get; set; }
        [Column("Isfulfilled")]
        public DateTime? IsFulfilled { get; set; }
        [Column("Status")]
        public string Status { get; set; }
        [Column("shipmentdetails")]
        public string ShipmentDetails { get; set; }
        [Column("notes")]
        public string Notes { get; set; }
        [Column("overallweight")]
        public decimal? OverallWeight { get; set; }
        [Column("for_totalunitprice")]
        public decimal? for_totalunitprice { get; set; }
        [Column("for_discountvalue")]
        public decimal? ForDiscountValue { get; set; }
        [Column("for_subtotalvalue")]
        public decimal? ForSubtotalValue { get; set; }
        [Column("for_taxvalue")]
        public decimal? ForTaxValue { get; set; }
        [Column("for_totalvalue")]
        public decimal? ForTotalValue { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("grnnumber")]
        public int GRNNumber { get; set; }
    }
}
