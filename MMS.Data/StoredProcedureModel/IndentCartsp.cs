using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_indentcart", Schema = "public")]
    public class IndentCartsp
    {
        [Key]
        public int ProductId { get; set; }

        [Column("indentcartid")]
        public int IndentCartId { get; set; }

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

        [Column("indentrequiredqty")]
        public decimal? IndentRequiredQty { get; set; }

        [Column("requiredqty")]
        public decimal? RequiredQty { get; set; }

        [Column("indentnumber")]
        public int IndentNumber { get; set; }
    }
}
