using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("grncart", Schema = "public")]
    public class GRNCart :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("grncartid")]
        public int grncartid { get; set; }
        [Column("podetailid")]
        public int podetailid { get; set; } 
        [Column("poheaderid")]
        public int poheaderid { get; set; }
        [Column("poquantity")]
        public decimal? poquantity { get; set; }
        [Column("for_currencyconid")]
        public int for_currencyconid { get; set; }
        [Column("currencyconid")]
        public int currencyconid { get; set; }
        [Column("productcode")]
        public string ProductCode { get; set; }
        [Column("productnameid")]
        public int ProductNameId { get; set; }
        [Column("uommasterid")]
        public int UomMasterId { get; set; }
        [Column("expirydate")]
        public DateTime? ExpiryDate { get; set; }
        [Column("batchcode")]
        public string BatchCode { get; set; }
        [Column("storecode")]
        public int StoreCode { get; set; }
        [Column("taxperid")]
        public int TaxPerId { get; set; }
        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }
        [Column("unitprice")]
        public decimal? unitprice { get; set; }
        [Column("quantity")]
        public decimal? Quantity { get; set; }
        [Column("subtotal")]
        public decimal? Subtotal { get; set; }
        [Column("grandtotal")]
        public decimal? Grandtotal { get; set; }
        [Column("discountperid")]
        public decimal? DiscountPerId { get; set; }
        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }
        [Column("for_discountvalue")]
        public decimal? ForDiscountValue { get; set; }
        [Column("for_subtotalvalue")]
        public decimal? ForSubtotalValue { get; set; }
        [Column("for_taxvalue")]
        public decimal? ForTaxValue { get; set; }
        [Column("for_totalvalue")]
        public decimal? ForTotalValue { get; set; } 
        [Column("for_totalunitprice")]
        public decimal? for_totalunitprice { get; set; }
        [Column("grndate")]
        public DateTime Grndate { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; } = true;
        [Column("deletedby")]
        public string DeletedBy { get; set; }
        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }
        [Column("grnnumber")]
        public int GRNNumber { get; set; }
    }
}
