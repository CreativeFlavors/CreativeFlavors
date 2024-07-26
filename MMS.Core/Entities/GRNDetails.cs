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
    public class GRNDetails :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("grndetailid")]
        public int GrnDetailId { get; set; }
        [Column("grnheaderid")]
        public int GrnHeaderId { get; set; }
        [Column("supplierid")]
        public int SupplierId { get; set; }
        [Column("productid")]
        public int productid { get; set; }
        [Column("podetailid")]
        public int? PoDetailId { get; set; }
        [Column("poquantity")]
        public decimal? PoQuantity { get; set; }  
        [Column("pounitprice")]
        public decimal? Pounitprice { get; set; }
        [Column("unitprice")]
        public decimal? UnitPrice { get; set; }
        [Column("quantity")]
        public decimal? Quantity { get; set; }
        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }
        [Column("subtotalvalue")]
        public decimal? SubtotalValue { get; set; }
        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }
        [Column("totalvalue")]
        public decimal? TotalValue { get; set; }
        [Column("expirydate")]
        public DateTime? ExpiryDate { get; set; }
        [Column("batchcode")]
        public string BatchCode { get; set; }
        [Column("storecode")]
        public int StoreCode { get; set; }
        [Column("weight")]
        public decimal? Weight { get; set; }
        [Column("isfulfilled")]
        public DateTime IsFulfilled { get; set; }
        [Column("status")]
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
        [Column("currencyid")]
        public int currencyid { get; set; }
        [Column("currencyconid")]
        public int currencyconid { get; set; }
        [Column("for_currencyconid")]
        public int? for_currencyconid { get; set; }

    }
}
