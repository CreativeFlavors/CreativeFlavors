using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class PurchaseOrderDelierySchedule:BaseEntity
    {
        public int IO { get; set; }
        public int PoOrderID { get; set; }
        public string Material { get; set; }
        public int quantity { get; set; }
        public DateTime? Date { get; set; }
       // public DateTime? CreatedDate { get; set; }
       // public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
