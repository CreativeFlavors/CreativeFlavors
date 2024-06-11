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
        [Column("productname")]
        public string ProductName { get; set; }
        [Column("salesquantity")]
        public decimal? SalesQuantity { get; set; }
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
