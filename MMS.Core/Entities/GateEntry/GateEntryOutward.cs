using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Core.Entities.GateEntry
{
    public class GateEntryOutwardMaster : BaseEntity
    {
        public int GateEntryOutwardId { get; set; }
        public string GateEntryNo { get; set; }
        public DateTime? OutwardEntryDateTime { get; set; }
        public int OutwardMaterialType { get; set; }
        public bool IsNewOutward { get; set; }
        public bool IsDocuments { get; set; }
        public bool IsJobWork { get; set; }
        public bool IsSales { get; set; }
        public bool IsEmployee { get; set; }
        public string StoresRefNo { get; set; }
        public DateTime? StoresRefDate { get; set; }
        public int StoresName { get; set; }
        public string UnitDivision { get; set; }
        public string ReturnType { get; set; }
        public string MaterialType { get; set; }
        public string DcNo { get; set; }
        public DateTime? DcDate { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ModeofTransport { get; set; }
        public int InvoiceCurrency { get; set; }
        public string InvoiceValue { get; set; }
        public string VehicleNo { get; set; }
        public int PoNoId { get; set; }
        public int SupplierId { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int MaterialNameId { get; set; }
        public string MaterialName { get; set; }
        public string HSNCode { get; set; }
        public int UomId { get; set; }
        public string Uom { get; set; }
        public string PendingQuantity { get; set; }
        public string Quantity { get; set; }
        public string Piecies { get; set; }
        public string Pcs { get; set; }
        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }
        public int SizeRangeId { get; set; }
        public string SupplierAcknowledgedBy { get; set; }
        public decimal TotalQty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public int Total { get; set; }
        public string TransportCompany { get; set; }
        public string VehicleArrangedBy { get; set; }
        public string Purpose { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string  PlaceOfSupply { get; set; }
        public decimal? GST { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? GSTAmount { get; set; }

    }
}