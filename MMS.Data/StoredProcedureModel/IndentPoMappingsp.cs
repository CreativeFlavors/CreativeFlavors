using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_indentpomapping", Schema = "public")]
    public class IndentPoMappingsp
    {
        
        [Column("indentpomapid")]
        public int IndentPoMapId { get; set; }

        [Column("suppliername")] 
        public string SupplierName { get; set; }

        [Column("productname")]
        public string ProductName { get; set; }

        [Column("storename")]
        public string StoreName { get; set; }

        [Column("unitprice")]
        public decimal? UnitPrice { get; set; }

        [Column("indentqty")]
        public decimal? IndentQty { get; set; }

        [Column("poqty")]
        public decimal? PoQty { get; set; }

        [Column("uomname")]
        public string UomName { get; set; }

        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }

        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }

        [Column("subtotalvalue")]
        public decimal? SubtotalValue { get; set; }

        [Column("totalvalue")]
        public decimal? TotalValue { get; set; }

        [Column("ponumber")]
        public int PoNumber { get; set; }

        [Column("taxpercentage")]
        public decimal? TaxPercentage { get; set; }

        [Column("productid")]
        public int ProductId { get; set; }

        [Column("discountpercentage")]
        public decimal? DiscountPercentage { get; set; }
    }
}
