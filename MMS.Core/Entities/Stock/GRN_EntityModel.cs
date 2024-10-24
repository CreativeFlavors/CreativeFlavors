﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class GRN_EntityModel : BaseEntity
    {
        public int GrnID { get; set; }
        public int GrnNo { get; set; }
        public string BarCodeNo { get; set; }
        public int GateEntryNo { get; set; }
        public decimal? FreightAmount { get; set; }
        public string GrnDate { get; set; }
        public string GEDate { get; set; }
        public int? TotalCount { get; set; }
        public string GETime { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public string GrnRefNo { get; set; }
        public string GateRefNo { get; set; }
        public string CustomsDuty { get; set; }
        public int SupplierNameId { get; set; }
        public int GrnType { get; set; }
        public string Pack_Forward { get; set; }
       
        public string DC_No { get; set; }
        public string ST_VATcredit { get; set; }
        public decimal? ServiceTax { get; set; }
        public string DCDate { get; set; }
        public string INVoiceNo { get; set; }
        public string INVoiceDate { get; set; }
        public string ImportClearance { get; set; }
        public string BENo { get; set; }
        public decimal? OtherCharges { get; set; }
        public decimal? ExchangeRate { get; set; }

        public string Transporter { get; set; }

        public string ShipmentMode { get; set; }
        public string GeneralRemarks1 { get; set; }
        public string GeneralRemarks { get; set; }
        public int PoNO { get; set; }
        public string LOTNo { get; set; }
        public int? MaterialId { get; set; }
        public int? IndentMaterialId { get; set; }
        public int ColourId { get; set; }
        public int UOMId { get; set; }
        public string DOM { get; set; }
        public int StoreLocation { get; set; }
        //public string Remarks { get; set; }
        public int IndentNo { get; set; }
        public string Grade { get; set; }
        public string Substance { get; set; }
        public decimal? Weight { get; set; }
        public int IONo { get; set; }
        public int GroupID { get; set; }
        public decimal? PoQty { get; set; }
        public decimal? QtyAsPerDc { get; set; }
        public int QtyAsPerSQFT { get; set; }
        public decimal? QtyAsPerPCS { get; set; }
        public int QTYType { get; set; }
        public decimal? ReceivedQty { get; set; }
        public int ReceivedSQFT { get; set; }
        public decimal? ReceivedPCS { get; set; }
        public int ReceivedType { get; set; }
        public decimal? RejectedQty { get; set; }
        public int RejectedSQFT { get; set; }
        public int RejectedType { get; set; }
        public decimal? RejectedPCS { get; set; }
        public decimal? AcceptedQty { get; set; }
       // public int RejectedType { get; set; }
        public decimal? AcceptedPCS { get; set; }
        //public int AcceptedQty { get; set; }
        public int AcceptedSQFT { get; set; }
       // public int AcceptedPCS { get; set; }
        public int AcceptedType { get; set; }
        public int Stores { get; set; }
        public int CategoryId { get; set; }
        public string IfGroupIsMaintenance { get; set; }
        public string QtyExcess { get; set; }
        public string ExcessApproval { get; set; }
        public string PendingQty { get; set; }
        public bool Disp_SelectedMatOfPO { get; set; }
        public bool Disp_AllPOBasedOnSelecMat { get; set; }
        public bool Disp_AllMatOfPO { get; set; }
        public string Rate { get; set; }
        public string Value { get; set; }
        public string MRPMargin { get; set; }
        public string MRPPrice { get; set; }
        public string AccessibleValue { get; set; }
        public string Discount_Per { get; set; }
        public string Discount_Val { get; set; }
        public string ExciseDuty_Per { get; set; }
        public string ExciseDuty_Val { get; set; }
        public string ST_VAT_CST_Per { get; set; }
        public string ST_VAT_CST_Val { get; set; }
        public string Ecess_Per { get; set; }
        public string Ecess_Val { get; set; }
        public string SH_Ecess_Per { get; set; }
        public string SH_Ecess_Val { get; set; }
        public string Surcharge_Per { get; set; }
        public string Surcharge_Val { get; set; }
        //public string Accessible_Val { get; set; }
        public string TagsPiecesDiscount_Per { get; set; }
        public string TagsPiecesDiscount_Val { get; set; }
        public decimal? AmountplusTax { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? BuyerSeasonID { get; set; }
        public int? BuyerMasterId { get; set; }
        public decimal? ShortageQty { get; set; }
        public int? ShortageSQFT { get; set; }
        public decimal? ShortagePCS { get; set; }
        public int? ShortageType { get; set; }
       public string AutomaticPONumber { get; set; }
        public decimal? ServiceTaxPer { get; set; }
        public int? MaterialType { get; set; }
        public int? Grn_MaterialID { get; set; }

    }
    public class GRNSizeQuantityObject : BaseEntity
    {
        public int GRNeSizeRangeID { get; set; }
        public string Size { get; set; }
        public decimal? quantity { get; set; }
        public decimal? Rate { get; set; }
        public int Grnid_FK { get; set; }
        public decimal? SizeRangeRefValue { get; set; }
        public decimal? Qty_Value { get; set; }
        public decimal? Received_value { get; set; }
        public decimal? Shortage_value { get; set; }
        public decimal? Rejectd_value { get; set; }
        public decimal? Accepted_value { get; set; }
    }
    public class GRNGrid
    {
        public int? GrnNo { get; set; }
        public int? GateEntryNo { get; set; }
        public string GrnDate { get; set; }
        public int? PoNO { get; set; }
        public string SupplierName { get; set; }
        public string DC_No { get; set; }
        public string INVoiceNo { get; set; }
        public string INVoiceDate { get; set; }
      
    }
}
