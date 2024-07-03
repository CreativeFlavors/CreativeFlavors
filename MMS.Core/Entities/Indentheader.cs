using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("indentheader", Schema = "public")]
    public class Indentheader :BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("indentheaderid")]
        public int IndentHeaderId { get; set; }

        [Column("indentdate")]
        public DateTime? IndentDate { get; set; }

        [Column("storecode")]
        public int? StoreCode { get; set; }

        [Column("suppliercode")]
        public int? SupplierCode { get; set; }

        [Column("porefnumber")]
        public string PoRefNumber { get; set; }

        [Column("items")]
        public int? Items { get; set; }

        [Column("quantity")]
        public decimal? Quantity { get; set; }

        [Column("extendedvalue")]
        public decimal? ExtendedValue { get; set; }

        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }

        [Column("subtotalvalue")]
        public decimal? SubtotalValue { get; set; }

        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }

        [Column("totalvalue")]
        public decimal? TotalValue { get; set; }

        [Column("expdeliverydate")]
        public DateTime? ExpDeliveryDate { get; set; }

        [Column("paybydate")]
        public DateTime? PayByDate { get; set; }

        [Column("fulfilldate")]
        public DateTime? FulfillDate { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("shipmentdetails")]
        public string ShipmentDetails { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("overallweight")]
        public decimal? OverallWeight { get; set; }

        [Column("for_extendedvalue")]
        public decimal? ForExtendedValue { get; set; }

        [Column("for_discountvalue")]
        public decimal? ForDiscountValue { get; set; }

        [Column("for_subtotalvalue")]
        public decimal? ForSubtotalValue { get; set; }

        [Column("for_taxvalue")]
        public decimal? ForTaxValue { get; set; }

        [Column("for_totalvalue")]
        public decimal? ForTotalValue { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("deleteddate")]
        public DateTime? DeletedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("deletedby")]
        public string DeletedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("indentqty")]
        public decimal? IndentQty { get; set; }

        [Column("indentno")]
        public int IndentNo { get; set; }
    }
}
