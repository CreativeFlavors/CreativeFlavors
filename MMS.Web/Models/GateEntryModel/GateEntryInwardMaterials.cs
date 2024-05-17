using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GateEntryModel
{
    public class GateEntryInwardMaterials
    {
        public int GateEntryInwardId { get; set; }
        public string GateEntryNo { get; set; }
        public string EntryDate { get; set; }
        public DateTime? InwardEntryDateTime { get; set; }
        public int? InwardMaterialType { get; set; }
        public bool IsNewInward { get; set; }
        public bool IsReturned { get; set; }
        public bool IsJobWork { get; set; }
        public string DcRefNo { get; set; }
        public string MaterialName { get; set; }
        public string DcRefDate { get; set; }
        public string Company { get; set; }
        public string UnitDivision { get; set; }
        public string ReturnType { get; set; }
        public string MaterialType { get; set; }
        public string DcNo { get; set; }
        public string DcDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string ModeofTransport { get; set; }
        public int? InvoiceCurrency { get; set; }
        public string InvoiceValue { get; set; }
        public string VehicleNo { get; set; }
        public int? PoNoId { get; set; }
        public int? SupplierId { get; set; }
        public int? StoresId { get; set; }
     //   public int? Component_Id { get; set; }
        public int? MaterialNameId { get; set; }
        public string HSNCode { get; set; }
        public int? SizeRangeId { get; set; }
        public int? ComponentId { get; set; }
        public int? UomId { get; set; }
        public string Pcs { get; set; }
        public string ReceivedBy { get; set; }
        public string AcknowledgedBy { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string PendingQuantity { get; set; }
        public string Quantity { get; set; }
        public string Size { get; set; }
        public string PoQty { get; set; }
        public string DcQty { get; set; }
        public string ArrivedQty { get; set; }
        public string PendingQty { get; set; }
        public string InwardMaterialSizeQtyRateId { get; set; }
        public string PendingQuantityId { get; set; }
        public decimal TotalQty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public decimal? DcTotal { get; set; }
        public string DcTotal_Qty { get; set; }
        public int? ArrivedTotal { get; set; }
        public int? InwardPo { get; set; }
        public int? PoTotal { get; set; }
        public int? jobsheet_pair_Code_id { get; set; }
        public int? Process_Name { get; set; }
        public string Manual_DcNo { get; set; }
        public string IssueSlipId { get; set; }
        public string Type { get; set; }
        public List<GEInwardMaterialGrid> GEMaterialsGrid { get; set; }
        public List<GateEntryInwardMaterialsMaster> GateEntryInwardMaterialList { get; set; }
        public List<InwardMaterialSizeQtyRateMaster> sizeRangeDetailslist { get; set; }
        public List<GateEntryInwardMaterialData> GateEntryInwardMaterialDataList { get; set; }
        public List<InwardMaterialPendingQuantityMaster> GateEntryPendingQtyDataList { get; set; }
        public List<GateEntryInward> gateEntryInward { get; set; }

        public List<GateEntryInwardMaterialsUploadMaster> GateEntryInwardMaterialsUploadlist { get; set; }
    }
    public class GateEntryInwardMaterialData
    {
        public string GroupName { get; set; }
        public int GroupID { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string SizeRangeName { get; set; }
        public int SizeRangeDetailsId { get; set; }
        public string StoreName { get; set; }
        public int StoreMasterId { get; set; }
        public string LongUnitName { get; set; }
        public string HSNCode { get; set; }
        public int Uom { get; set; }
        public decimal PriceRs { get; set; }
        public bool IsSize { get; set; }

    }
    public class GateEntryInward
    {
        public int MaterialID { get; set; }
        public string MaterialDescription { get; set; }
        public int SupplierId { get; set; }
    }
    public class GateEntry_sheetlist
    {
        public string MaterialDescription { get; set; }
        public int Quantity { get; set; }
        public string Uc_Noms { get; set; }
        public int Req_Qty { get; set; }
        public int sheet { get; set; }
        public int jobsheet_pair_id { get; set; }
        public int Material { get; set; }
        public int Jw_Rate { get; set; }
        public int GateEntryInwardId { get; set; }

    }
}