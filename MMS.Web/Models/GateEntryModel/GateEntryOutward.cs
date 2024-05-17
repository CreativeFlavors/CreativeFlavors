using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;
using MMS.Core.Entities.Stock;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GateEntryModel
{
    public class GateEntryOutward
    {
        public int GateEntryOutwardId { get; set; }
        public string GateEntryNo { get; set; }
        public string EntryDate { get; set; }
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
        public string DcDate { get; set; }
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
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public string SizeRange { get; set; }
        public string Size { get; set; }
        public string OutwardMaterialSizeQtyRateId { get; set; }
        public decimal Rate { get; set; }
        public int MaterialId { get; set; }
        public string SizeQuantityRate { get; set; }
        public decimal TotalQty { get; set; }
        public int Total { get; set; }
        public decimal Value { get; set; }
        public string TransportCompany { get; set; }
        public string VehicleArrangedBy { get; set; }
        public string Purpose { get; set; }
        public List<SizeItemMaterial> sizeItemMaterialList { get; set; }
        public List<GEOutwardGrid> GEOutwardGrid { get; set; }
        public List<GateEntryOutwardMaster> GateEntryOutwardList { get; set; }
        public List<OutwardMaterialSizeQtyRate> sizeRangeDetailslist { get; set; }
        public List<GeneratePdf> PdfList { get; set; }
        public List<MaterialMaster> MaterialMasterList { get; set; }
        public List<GateEntryOutwardMaterialData> GateEntryOutwardMaterialDataList { get; set; }
        public string PlaceOfSupply { get; set; }
        public decimal? GST { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? GSTAmount { get; set; }
        public class QuantityObject
        { 
            public string quantity { get; set; }
            public string Size { get; set; }
        }
    }
    public class MaterialMaster
    {
        public int MaterialNameId { get; set; }
        public string MaterialDescription { get; set; }
        public int MaterialMasterId { get; set; }
        public string MaterialName { get; set; }
    }
    public class GateEntryOutwardMaterialData
    {
        public string GroupName { get; set; }
        public string CategoryName { get; set; }
        public string SizeRangeName { get; set; }
        public string StoreName { get; set; }
        public string LongUnitName { get; set; }
        public string PriceRs { get; set; }
        public string PendingQuantity { get; set; }
        public bool IsSize { get; set; }
        public int SizeRangeDetailsId { get; set; }
        public int StoreMasterId { get; set; }
        public int UomMasterId { get; set; }

    }
}