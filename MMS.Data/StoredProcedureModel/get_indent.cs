using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_indent", Schema = "public")]
    public class get_indent
    {
        [Key]
        public int ProductId { get; set; }

        [Column("indentdetailid")]
        public int indentdetailid { get; set; }

        [Column("productname")]
        public string ProductName { get; set; }

        [Column("price")]
        public decimal? Price { get; set; }

        [Column("storename")]
        public string StoreName { get; set; }

        [Column("uomname")]
        public string UomName { get; set; }

        [Column("taxname")]
        public string TaxName { get; set; }

        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }

        [Column("indentnumber")]
        public int IndentNumber { get; set; } 
        [Column("isactive")]
        public bool isactive { get; set; }
    }
}
