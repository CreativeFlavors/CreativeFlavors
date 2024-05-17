using System;

namespace MMS.Core.Entities.GateEntry
{
    public class GateEntryOutwardDocumentMaster : BaseEntity
    {
        public int GateEntryOutwardDocumentId { get; set; }
        public int OutwardDocTypeId { get; set; }
        public string GateEntryNo { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public string Mode { get; set; }
        public string Company { get; set; }
        public string CourierNo { get; set; }
        public string Unit { get; set; }
        public string PersonName { get; set; }
        public string ModeofTransport { get; set; }
        public string VehicleNo { get; set; }
        public string AddressToWhom { get; set; }
        public string HandOverTo { get; set; }
        public string ReceivedBy { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}