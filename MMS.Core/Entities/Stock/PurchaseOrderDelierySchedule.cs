using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("purchaseorderdelieryschedule", Schema = "public")]
    public class PurchaseOrderDelierySchedule : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]

        [Column("io")]
        public int IO { get; set; }

        [Column("poorderid")]
        public int PoOrderID { get; set; }

        [Column("material")]
        public string Material { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }
    }
}
