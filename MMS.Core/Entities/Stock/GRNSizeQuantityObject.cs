using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class GRNSizeQuantityObject_:BaseEntity
    {
        public int GRNeSizeRangeID { get; set; }
        public string Size { get; set; }
        public int? quantity { get; set; }
        public decimal? Rate { get; set; }
        public int PoOrderID { get; set; }
    }
}
