using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
    public class OutwardMaterialSizeQtyRateMaster : BaseEntity
    {
        public int OutwardMaterialSizeQtyRateId { get; set; }
        public int GateEntryOutwardId { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public int MaterialId { get; set; }
        public string Size { get; set; }
        public string Quantity { get; set; }
        public decimal Rate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
