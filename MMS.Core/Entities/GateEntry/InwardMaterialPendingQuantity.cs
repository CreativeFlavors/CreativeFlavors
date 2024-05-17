namespace MMS.Core.Entities.GateEntry
{
    public class InwardMaterialPendingQuantityMaster : BaseEntity
    {
        public int PendingQuantityId { get; set; }
        public int? GateEntryInwardId { get; set; }
        public int? MaterialId { get; set; }
        public string Size { get; set; }
        public string DcQty{ get; set; }
        public string ArrivedQty { get; set; }
        public int? PoOrderID { get; set; }
        public string PoQuantity { get; set; }
        public string PendingQty { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
