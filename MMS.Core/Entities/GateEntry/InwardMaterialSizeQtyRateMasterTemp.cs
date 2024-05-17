using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
    public class InwardMaterialSizeQtyRateMasterTemp : BaseEntity
    {
        public int InwardMaterialSizeQtyRateId { get; set; }
        public int? GateEntryInwardId { get; set; }
        public int? MaterialId { get; set; }
        public string Size { get; set; }
        public string Quantity { get; set; }
        public decimal Rate { get; set; }
        public string PoQty { get; set; }
        public string DcQty { get; set; }
        public string ArrivedQty { get; set; }
        public string PendingQty { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
