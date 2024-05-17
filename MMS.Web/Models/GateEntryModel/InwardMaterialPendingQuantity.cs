using MMS.Core;
using MMS.Core.Entities.GateEntry;
using System.Collections.Generic;

namespace MMS.Web.Models.GateEntryModel
{
    public class InwardMaterialPendingQuantity : BaseEntity
    {
        public int PendingQuantityId { get; set; }
        public int GateEntryInwardId { get; set; }
        public int MaterialId { get; set; }
        public string Size { get; set; }
        public string DcQty { get; set; }
        public string ArrivedQty { get; set; }
        public string PendingQty { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<InwardMaterialPendingQuantityMaster> sizeRangePendingQtylist { get; set; }
    }
}