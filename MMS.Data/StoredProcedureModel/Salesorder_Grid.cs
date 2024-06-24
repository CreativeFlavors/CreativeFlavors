using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("salesorder_grid", Schema = "public")]
    public class Salesorder_Grid
    {
        [Column("salesorderid")]
        public int salesorderid { get; set; }
        [Column("salesorderid_dt")]
        public int salesorderid_dt { get; set; }
        [Column("salesorderdate")]
        public DateTime salesorderdate { get; set; }
        [Column("productname")]
        public string productname { get; set; }
        [Column("productcode")]
        public string productcode { get; set; }
        [Column("quantity")]
        public decimal? quantity { get; set; }
        [Column("discount_value")]
        public decimal? discount_value { get; set; }   
        [Column("subtotal")]
        public decimal? subtotal { get; set; }   
        [Column("taxvalue")]
        public decimal? taxvalue { get; set; }  
        [Column("totalprice")]
        public decimal? totalprice { get; set; }   
        [Column("long_unit_name")]
        public string long_unit_name { get; set; }
        [Column("buyer_full_name")]
        public string buyer_full_name { get; set; }
        [Column("buyerid")]
        public int Buyerid { get; set; }
        [Column("bomno")]
        public string bomno { get; set; }
    }
}
