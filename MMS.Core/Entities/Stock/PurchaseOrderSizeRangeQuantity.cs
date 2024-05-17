using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class PurchaseOrderSizeRangeQuantity:BaseEntity
    {
        public int PurchaseSizeRangeID { get; set; }
        public string Size { get; set; }
        public int quantity { get; set; }
        public decimal? Rate { get; set; }
        public int PoOrderID { get; set; }
        //public DateTime? CreatedDate { get; set; }
       // public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
