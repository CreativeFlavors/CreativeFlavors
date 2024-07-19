using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_salesorders", Schema = "public")]
    public class GRNCarlist
    {
        [Column("grncartid")]
        public int grncartid { get; set; }
        [Column("productname")]
        public string productname { get; set; }
        [Column("quantity")]
        public decimal? quantity { get; set; } 
        [Column("poquantity")]
        public decimal? POquantity { get; set; }
        [Column("unitprice")]
        public decimal? unitprice { get; set; }
        [Column("shortunitname")]
        public string shortunitname { get; set; }
        [Column("discountvalue")]
        public decimal? discountvalue { get; set; }
        [Column("subtotal")]
        public decimal? subtotal { get; set; }
        [Column("taxvalue")]
        public decimal? taxvalue { get; set; }
        [Column("grandtotal")]
        public decimal? grandtotal { get; set; }
    }
}
