using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class MaterialOpeningSizeQtyRate : BaseEntity
    {
        public int MaterialOpeningSizeQtyRateId { get; set; }
        public string Size { get; set; }
        public string Quantity { get; set; }
        public decimal Rate { get; set; }
        public int MaterialOpeningId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBY { get; set; }
    }
}
