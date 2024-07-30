using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("Fineshedgoods_report", Schema = "public")]
    public class FineshedgoodsReport
    {
        [Column("productname")]
        public string ProductName { get; set; }
        [Column("ProductCode")]
        public string ProductCode { get; set; }
        [Column("producttypename")]
        public string ProductTypeName { get; set; }
        [Column("price")]
        public decimal? Price { get; set; }
        [Column("costs")]
        public decimal? Costs { get; set; }
        [Column("storename")]
        public string StoreName { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("shortunitname")]
        public string ShortUnitName { get; set; }
    }
}
