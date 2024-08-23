using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_product_details", Schema = "public")]
    public class ProductGrid
    {
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("categoryname")]
        public string CategoryName { get; set; }
        [Column("product_code")]
        public string product_code { get; set; }
        [Column("product_name")]
        public string product_name { get; set; }
        [Column("product_desc")]
        public string product_desc { get; set; }
        [Column("taxvalue")]
        public string TaxValue { get; set; }
        [Column("uom")]
        public string uom { get; set; }
        [Column("price")]
        public decimal? Price { get; set; }
        [Column("bom_no")]  
        public string bom_no { get; set; }   
        [Column("productype")]
        public int Productype { get; set; }
    }
}
