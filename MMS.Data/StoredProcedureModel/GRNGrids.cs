using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_grn_Grid", Schema = "public")]
    public class GRNGrids
    {
        [Column("grndate")]
        public DateTime GrnDate { get; set; }
        [Column("refinvoicenumber")]
        public string RefInvoiceNumber { get; set; }
        [Column("items")]
        public int Items { get; set; }
        [Column("GrnDate")]
        public decimal? Quantity { get; set; }
        [Column("unitprice")]
        public decimal? UnitPrice { get; set; }
        [Column("poheaderid")]
        public int PoHeaderId { get; set; }
        [Column("subtotal")]
        public decimal? SubTotal { get; set; }
        [Column("taxvalue")]
        public decimal? TaxValue { get; set; }
        [Column("discountvalue")]
        public decimal? DiscountValue { get; set; }
        [Column("totalvalue")]
        public decimal? TotalValue { get; set; }
        [Column("suppliername")]
        public string SupplierName { get; set; }
    }
}
