using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    [Table("get_mrp_details", Schema = "public")]
    public class MRP_Details
    {
        [Column("product_id")]
        public int Product_id { get; set; }
        [Column("productname")]
        public string ProductName { get; set; }
        [Column("subassemblyproid")]
        public int? Subassemblyproid { get; set; }
        [Column("sub_quantity")]
        public decimal? Sub_quantity { get; set; }
        [Column("salesorderquantity")]
        public decimal? salesorderquantity { get; set; }
        [Column("availablestock")]
        public decimal? AvailableStock { get; set; }
        [Column("productionduedate")]
        public DateTime? ProductionDueDate { get; set; }
        [Column("productionqty")]
        public decimal? ProductionQty { get; set; }
        [Column("status")]
        public string Status { get; set; }
    }
}
