using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;

namespace MMS.Web.Models.GateEntryModel
{
    public class GateEntryOutwardDocument
    {
        public int GateEntryOutwardDocumentId { get; set; }
        public int OutwardDocTypeId { get; set; }
        public string GateEntryNo { get; set; }
        public string EntryDate { get; set; }
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<GateEntryOutwardDocumentMaster> GateEntryDocumentlist { get; set; }
    }
}