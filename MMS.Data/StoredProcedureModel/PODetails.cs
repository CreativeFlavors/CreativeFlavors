using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("podetails", Schema = "public")]
    public class PODetails
    {
        [Column("poheader")]
        public int poheader { get; set; } 
        [Column("grnqty")]
        public decimal? grnqty { get; set; }
        [Column("podetail")]
        public int podetail { get; set; }

        [Column("suppliername")]
        public string SupplierName { get; set; }

        [Column("productname")]
        public string ProductName { get; set; }
        [Column("productcode")]
        public string Productcode { get; set; }

        [Column("storename")]
        public string StoreName { get; set; }

        [Column("unitprice")]
        public decimal? UnitPrice { get; set; }

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
        public decimal? taxpercentage { get; set; }

        [Column("productid")]
        public int productid { get; set; }
        [Column("discountpercentage")]
        public decimal? discountpercentage { get; set; } 
        [Column("isactive")]
        public bool isactive { get; set; }
        [Column("indentqty")]
        public decimal? indentqty { get; set; }
    }
}
