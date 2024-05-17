using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("grn_entitymodelnew", Schema = "public")]
    public class GRN_EntityModelNew : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("grnid")]
        public int GrnID { get; set; }

        [Column("grnno")]
        public int GrnNo { get; set; }

        [Column("barcodeno")]
        public string BarCodeNo { get; set; }

        [Column("gateentryno")]
        public int GateEntryNo { get; set; }

        [Column("freightamount")]
        public decimal? FreightAmount { get; set; }

        [Column("grndate")]
        public string GrnDate { get; set; }

        [Column("gedate")]
        public string GEDate { get; set; }

        [Column("totalcount")]
        public int? TotalCount { get; set; }

        [Column("getime")]
        public string GETime { get; set; }

        [Column("insuranceamount")]
        public decimal? InsuranceAmount { get; set; }

        [Column("grnrefno")]
        public string GrnRefNo { get; set; }

        [Column("gaterefno")]
        public string GateRefNo { get; set; }

        [Column("customsduty")]
        public string CustomsDuty { get; set; }

        [Column("suppliernameid")]
        public int SupplierNameId { get; set; }

        [Column("grntype")]
        public int GrnType { get; set; }

        [Column("pack_forward")]
        public string Pack_Forward { get; set; }

        [Column("dc_no")]
        public string DC_No { get; set; }

        [Column("st_vatcredit")]
        public string ST_VATcredit { get; set; }

        [Column("servicetax")]
        public decimal? ServiceTax { get; set; }

        [Column("dcdate")]
        public string DCDate { get; set; }

        [Column("invoiceno")]
        public string INVoiceNo { get; set; }

        [Column("invoicedate")]
        public string INVoiceDate { get; set; }

        [Column("importclearance")]
        public string ImportClearance { get; set; }

        [Column("beno")]
        public string BENo { get; set; }

        [Column("othercharges")]
        public decimal? OtherCharges { get; set; }

        [Column("exchangerate")]
        public decimal? ExchangeRate { get; set; }

        [Column("transporter")]
        public string Transporter { get; set; }

        [Column("shipmentmode")]
        public string ShipmentMode { get; set; }

        //[Column("generalremarks1")]
        //public string GeneralRemarks1 { get; set; }

        [Column("generalremarks")]
        public string GeneralRemarks { get; set; }

        [Column("pono")]
        public int PoNO { get; set; }

        [Column("lotno")]
        public string LOTNo { get; set; }

        [Column("materialid")]
        public int? MaterialId { get; set; }

        [Column("indentmaterialid")]
        public int? IndentMaterialId { get; set; }

        [Column("colourid")]
        public int ColourId { get; set; }

        [Column("uomid")]
        public int UOMId { get; set; }

        [Column("dom")]
        public string DOM { get; set; }

        [Column("storelocation")]
        public int StoreLocation { get; set; }

        [Column("indentno")]
        public int IndentNo { get; set; }

        [Column("grade")]
        public string Grade { get; set; }

        [Column("substance")]
        public string Substance { get; set; }

        [Column("weight")]
        public decimal? Weight { get; set; }

        [Column("iono")]
        public int IONo { get; set; }

        [Column("groupid")]
        public int GroupID { get; set; }

        [Column("poqty")]
        public decimal? PoQty { get; set; }

        [Column("qtyasperdc")]
        public decimal? QtyAsPerDc { get; set; }

        [Column("qtyaspersqft")]
        public int QtyAsPerSQFT { get; set; }

        [Column("qtyasperpcs")]
        public decimal? QtyAsPerPCS { get; set; }

        [Column("qtytype")]
        public int QTYType { get; set; }

        [Column("receivedqty")]
        public decimal? ReceivedQty { get; set; }

        [Column("receivedsqft")]
        public int ReceivedSQFT { get; set; }

        [Column("receivedpcs")]
        public decimal? ReceivedPCS { get; set; }

        [Column("receivedtype")]
        public int ReceivedType { get; set; }

        [Column("rejectedqty")]
        public decimal? RejectedQty { get; set; }

        [Column("rejectedsqft")]
        public int RejectedSQFT { get; set; }

        [Column("rejectedtype")]
        public int RejectedType { get; set; }

        [Column("rejectedpcs")]
        public decimal? RejectedPCS { get; set; }

        [Column("acceptedqty")]
        public decimal? AcceptedQty { get; set; }

        [Column("acceptedsqft")]
        public int AcceptedSQFT { get; set; }

        [Column("acceptedtype")]
        public int AcceptedType { get; set; }


        [Column("acceptedpcs")]
        public decimal? AcceptedPCS { get; set; }

        [Column("stores")]
        public int Stores { get; set; }

        [Column("categoryid")]
        public int CategoryId { get; set; }

        [Column("ifgroupismaintenance")]
        public string IfGroupIsMaintenance { get; set; }

        [Column("qtyexcess")]
        public string QtyExcess { get; set; }

        [Column("excessapproval")]
        public string ExcessApproval { get; set; }

        [Column("pendingqty")]
        public string PendingQty { get; set; }

        [Column("disp_selectedmatofpo")]
        public bool Disp_SelectedMatOfPO { get; set; }

        [Column("disp_allpobasedonselecmat")]
        public bool Disp_AllPOBasedOnSelecMat { get; set; }

        [Column("disp_allmatofpo")]
        public bool Disp_AllMatOfPO { get; set; }

        [Column("rate")]
        public string Rate { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("mrpmargin")]
        public string MRPMargin { get; set; }

        [Column("mrpprice")]
        public string MRPPrice { get; set; }

        [Column("accessiblevalue")]
        public string AccessibleValue { get; set; }

        [Column("discount_per")]
        public string Discount_Per { get; set; }

        [Column("discount_val")]
        public string Discount_Val { get; set; }

        [Column("exciseduty_per")]
        public string ExciseDuty_Per { get; set; }

        [Column("exciseduty_val")]
        public string ExciseDuty_Val { get; set; }

        [Column("st_vat_cst_per")]
        public string ST_VAT_CST_Per { get; set; }

        [Column("st_vat_cst_val")]
        public string ST_VAT_CST_Val { get; set; }

        [Column("ecess_per")]
        public string Ecess_Per { get; set; }

        [Column("ecess_val")]
        public string Ecess_Val { get; set; }

        [Column("sh_ecess_per")]
        public string SH_Ecess_Per { get; set; }

        [Column("sh_ecess_val")]
        public string SH_Ecess_Val { get; set; }

        [Column("surcharge_per")]
        public string Surcharge_Per { get; set; }

        [Column("surcharge_val")]
        public string Surcharge_Val { get; set; }

        [Column("tagspiecesdiscount_per")]
        public string TagsPiecesDiscount_Per { get; set; }

        [Column("tagspiecesdiscount_val")]
        public string TagsPiecesDiscount_Val { get; set; }

        [Column("amountplustax")]
        public decimal? AmountplusTax { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("buyerseasonid")]
        public int? BuyerSeasonID { get; set; }

        [Column("buyermasterid")]
        public int? BuyerMasterId { get; set; }

        [Column("shortageqty")]
        public decimal? ShortageQty { get; set; }

        [Column("shortagesqft")]
        public int? ShortageSQFT { get; set; }

        [Column("shortagepcs")]
        public decimal? ShortagePCS { get; set; }

        [Column("shortagetype")]
        public int? ShortageType { get; set; }

        [Column("automaticponumber")]
        public string AutomaticPONumber { get; set; }

        [Column("servicetaxper")]
        public decimal? ServiceTaxPer { get; set; }

        [Column("materialtype")]
        public int? MaterialType { get; set; }

        [Column("grn_materialid")]
        public int? Grn_MaterialID { get; set; }

        [Column("podate")]
        public DateTime? PoDate { get; set; }

        [Column("bedate")]
        public DateTime? BEDate { get; set; }

        [Column("modeoftransport")]
        public string ModeofTransport { get; set; }

        [Column("machineryname")]
        public string MachineryName { get; set; }

        [Column("poreceivedqty")]
        public decimal? PoReceivedQty { get; set; }

        [Column("gst")]
        public decimal? GST { get; set; }

        [Column("sgst")]
        public decimal? SGST { get; set; }

        [Column("cgst")]
        public decimal? CGST { get; set; }

        [Column("igst")]
        public decimal? IGST { get; set; }

        [Column("tcs")]
        public decimal? TCS { get; set; }

        [Column("tcsvalue")]
        public decimal? TCSValue { get; set; }

        [Column("importigst")]
        public decimal? ImportIGST { get; set; }

        [Column("surcharges")]
        public decimal? Surcharges { get; set; }

        [Column("gstvalue")]
        public decimal? GSTValue { get; set; }

        [Column("sgstvalue")]
        public decimal? SGSTValue { get; set; }

        [Column("cgstvalue")]
        public decimal? CGSTValue { get; set; }

        [Column("igstvalue")]
        public decimal? IGSTValue { get; set; }

        [Column("importigstvalue")]
        public decimal? ImportIGSTValue { get; set; }

        [Column("surchargesvalue")]
        public decimal? SurchargesValue { get; set; }

        [Column("freightamtpercent")]
        public decimal? FreightAmtPercent { get; set; }

        [Column("insuranceamtpercent")]
        public decimal? InsuranceAmtPercent { get; set; }

        [Column("customdutypercent")]
        public decimal? CustomDutyPercent { get; set; }

        [Column("clearingchargepercent")]
        public decimal? ClearingChargePercent { get; set; }

        [Column("packforwardpercent")]
        public decimal? PackforwardPercent { get; set; }

        [Column("freighttaxamt")]
        public decimal? FreightTaxAmt { get; set; }

        [Column("insurancetaxamt")]
        public decimal? InsuranceTaxAmt { get; set; }

        [Column("customdutytax")]
        public decimal? CustomDutyTax { get; set; }

        [Column("clearingchargetax")]
        public decimal? ClearingChargeTax { get; set; }

        [Column("packforwardtax")]
        public decimal? PackforwardTax { get; set; }

        [Column("servicechargetax")]
        public decimal? ServiceChargeTax { get; set; }

        [Column("valueinrps")]
        public decimal? ValueInRps { get; set; }

        [Column("currencykey")]
        public string Currencykey { get; set; }

        [Column("totalamount")]
        public decimal? TotalAmount { get; set; }

        [Column("totalcost")]
        public decimal? TotalCost { get; set; }

        [Column("tagspiecesdiscount")]
        public int TagsPiecesDiscount { get; set; }

        [Column("nettotal")]
        public decimal? NetTotal { get; set; }

        [Column("grn_reason")]
        public string Grn_Reason { get; set; }

        [Column("gateentryno_common")]
        public int? GateEntryNo_Common { get; set; }

        [Column("qtyasperdcsqf")]
        public decimal? QtyAsPerDcsqf { get; set; }

        [Column("qtyasperdcpsc")]
        public decimal? QtyAsPerDcPsc { get; set; }

        [Column("receivedqtysqf")]
        public decimal? ReceivedQtysqf { get; set; }

        [Column("receivedqtypsc")]
        public decimal? ReceivedQtyPsc { get; set; }

        [Column("shortageqtysqf")]
        public decimal? ShortageQtysqf { get; set; }

        [Column("shortageqtypsc")]
        public decimal? ShortageQtyPsc { get; set; }

        [Column("rejectedqtysqf")]
        public decimal? RejectedQtysqf { get; set; }

        [Column("rejectedqtypsc")]
        public decimal? RejectedQtyPsc { get; set; }

        [Column("acceptedqtysqf")]
        public decimal? AcceptedQtysqf { get; set; }

        [Column("acceptedqtypsc")]
        public decimal? AcceptedQtyPsc { get; set; }

        [Column("byprocuct_kg")]
        public decimal? byprocuct_kg { get; set; }

        [Column("byprocuct_sqf")]
        public decimal? byprocuct_sqf { get; set; }

        [Column("byprocuct_psc")]
        public decimal? byprocuct_psc { get; set; }

        [Column("cuttable_per")]
        public int? cuttable_per { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        [Column("grnduedate")]
        public DateTime? GrnDueDate { get; set; }
    }

    [Table("grnsizequantityobjectnew", Schema = "public")]
    public class GRNSizeQuantityObjectNew : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("grnesizerangeid")]
        public int GRNeSizeRangeID { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("quantity")]
        public decimal? quantity { get; set; }

        [Column("rate")]
        public decimal? Rate { get; set; }

        [Column("grnid_fk")]
        public int Grnid_FK { get; set; }

        [Column("sizerangerefvalue")]
        public decimal? SizeRangeRefValue { get; set; }

        [Column("qty_value")]
        public decimal? Qty_Value { get; set; }

        [Column("received_value")]
        public decimal? Received_value { get; set; }

        [Column("shortage_value")]
        public decimal? Shortage_value { get; set; }

        [Column("rejectd_value")]
        public decimal? Rejectd_value { get; set; }

        [Column("accepted_value")]
        public decimal? Accepted_value { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
