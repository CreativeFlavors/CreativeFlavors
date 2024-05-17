using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Core.Entities.GateEntry
{
    public class GateEntryInwardMaterialsMaster:BaseEntity
    {
        public int GateEntryInwardId { get;  set; }
        public string GateEntryNo { get; set; }
        public DateTime? InwardEntryDateTime { get; set; }
        public int? InwardMaterialType { get; set; }
        public bool IsNewInward { get; set; }
        public bool IsReturned { get; set; }
        public bool IsJobWork { get; set; }
        public string DcRefNo { get; set; }
        public string DcRefDate { get; set; }
        public string Company { get; set; }
        public string UnitDivision { get; set; }
        public string ReturnType { get; set; }
        public string MaterialName { get; set; }
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
        public int? MaterialNameId { get; set; }
        public string HSNCode { get; set; }
        public int? SizeRangeId { get; set; }
        public int? UomId { get; set; }
        public string Pcs { get; set; }
        public string ReceivedBy { get; set; }
        public string AcknowledgedBy { get; set; }
        public string Remarks { get; set; }
        public string PendingQuantity { get; set; }
        public string Quantity { get; set; }
        public string Piecies { get; set; }
        public decimal? DcTotal { get; set; }
        public int? ArrivedTotal { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public decimal TotalQty { get; set; }

        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public int? InwardPo { get; set; }
        public int? PoTotal { get; set; }

        public int? jobsheet_pair_Code_id { get; set; }
        public int? Process_Name { get; set; }
        public string Manual_DcNo { get; set; }
        public string IssueSlipId { get; set; }

        public int? ComponentId { get; set; }

        
    }
}