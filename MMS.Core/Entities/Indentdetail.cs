using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("indentdetail", Schema = "public")]
    public class Indentdetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("indentdetailid")]
        public int IndentDetailId { get; set; }

        [Column("indentheaderid")]
        public int IndentHeaderId { get; set; }

        [Column("indentdate")]
        public DateTime? IndentDate { get; set; }

        [Column("storecode")]
        public int StoreCode { get; set; }

        [Column("suppliercode")]
        public int SupplierCode { get; set; }

        [Column("materialid")]
        public int MaterialId { get; set; }

        [Column("uomid")]
        public int UomId { get; set; }

        [Column("taxid")]
        public int TaxId { get; set; }

        [Column("unitprice")]
        public decimal UnitPrice { get; set; }

        [Column("quantity")]
        public decimal Quantity { get; set; }

        [Column("extendedprice")]
        public decimal ExtendedPrice { get; set; }

        [Column("discountvalue")]
        public decimal DiscountValue { get; set; }

        [Column("discountperc")]
        public decimal DiscountPerc { get; set; }

        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [Column("taxvalue")]
        public decimal TaxValue { get; set; }

        [Column("total")]
        public decimal Total { get; set; }

        [Column("weight")]
        public decimal Weight { get; set; }

        [Column("for_unitprice")]
        public decimal ForUnitPrice { get; set; }

        [Column("for_extendedprice")]
        public decimal ForExtendedPrice { get; set; }

        [Column("for_discountvalue")]
        public decimal ForDiscountValue { get; set; }

        [Column("for_subtotal")]
        public decimal ForSubtotal { get; set; }

        [Column("for_taxvalue")]
        public decimal ForTaxValue { get; set; }

        [Column("for_total")]
        public decimal ForTotal { get; set; }

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
        public decimal IndentQty { get; set; }
    }
}
