﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    [Table("pocart", Schema = "public")]
    public class PoCart : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pocartid")]
        public int PocartId { get; set; }

        [Column("supplierid")]
        public int SupplierId { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("suppliermaterialid")]
        public string SupplierMaterialId { get; set; }

        [Column("storecode")]
        public int StoreCode { get; set; }

        [Column("unitprice")]
        public decimal? UnitPrice { get; set; }

        [Column("quantity")]
        public decimal? Quantity { get; set; }

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

        [Column("status")]
        public string Status { get; set; }

        [Column("withindentreference")]
        public bool WithIndentReference { get; set; }

        [Column("uomid")]
        public int UomId { get; set; }

        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }

        [Column("subtotal")]
        public decimal? Subtotal { get; set; }

        [Column("totalvalue")]
        public decimal? TotalValue { get; set; }

        [Column("discountpercentage")]
        public decimal? DiscountPercentage { get; set; }

        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }

        [Column("taxpercentage")]
        public decimal? TaxPercentage { get; set; }

        [Column("taxinclusive")]
        public bool TaxInclusive { get; set; }

        [Column("ponumber")]
        public int PoNumber { get; set; }

        [Column("indentnumber")]
        public int IndentNumber { get; set; }

        [Column("podate")]
        public DateTime? PoDate { get; set; }
    }
}
