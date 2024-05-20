using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;

namespace MMS.Web.Models.StockModel
{
    [Table("internalorderentrymodel", Schema = "public")]
    public class InternalOrderEntryModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("internalentryorderid")]
        public int InternalEntryOrderId { get; set; }
        [Column("buyerorderslno")]
        public string BuyerOrderSlNo { get; set; }
        [Column("lotno")]
        public string LotNo { get; set; }
        [Column("count")]
        public string Count { get; set; }
        [Column("weekno")]
        public string WeekNo { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("buyerseason")]
        public int? BuyerSeason { get; set; }
        [Column("buyername")]
        public int? BuyerName { get; set; }
        [Column("orderprojectionno")]
        public string OrderProjectionNo { get; set; }
        [Column("buyerpono")]
        public string BuyerPoNo { get; set; }
        [Column("ourstyle")]
        public string OurStyle { get; set; }
        [Column("leatherdescription")]
        public string LeatherDescription { get; set; }
        [Column("discountper")]
        public string DiscountPer { get; set; }
        [Column("quoteno")]
        public string QuoteNo { get; set; }
        [Column("countrystamp")]
        public string CountryStamp { get; set; }
        [Column("customerplan")]
        public string CustomerPlan { get; set; }
        [Column("customerdate")]
        public DateTime? CustomerDate { get; set; }
        [Column("agentmasterid")]
        public int? AgentMasterId { get; set; }
        [Column("commper")]
        public string CommPer { get; set; }
        [Column("exfactorydate")]
        public DateTime? ExFactoryDate { get; set; }
        [Column("shipmentmode")]
        public string ShipmentMode { get; set; }
        [Column("samplereqno")]
        public string SampleReqNo { get; set; }
        [Column("brand")]
        public string Brand { get; set; }
        [Column("buyerstyleno")]
        public string BuyerStyleNo { get; set; }
        [Column("barcodeno")]
        public string BarCodeNo { get; set; }
        [Column("bomno")]
        public string BomNo { get; set; }
        [Column("last")]
        public string Last { get; set; }
        [Column("colormasterid")]
        public string ColorMasterId { get; set; }
        [Column("finishedprodtype")]
        public string FinishedProdType { get; set; }
        [Column("producttypeid")]
        public int? ProductTypeId { get; set; }
        [Column("amendmentnowithdate")]
        public string AmendmentNoWithDate { get; set; }
        [Column("totalorderforweek")]
        public string TotalOrderForWeek { get; set; }
        [Column("ordertype")]
        public int? OrderType { get; set; }
        [Column("currency")]
        public int? Currency { get; set; }
        [Column("parties")]
        public int? Parties { get; set; }
        [Column("grademasterid")]
        public int? GradeMasterId { get; set; }
        [Column("sizerangemasterid")]
        public int? SizeRangeMasterId { get; set; }
        [Column("remarks")]
        public string Remarks { get; set; }
        [Column("remarks1")]
        public string Remarks1 { get; set; }
        [Column("remarks2")]
        public string Remarks2 { get; set; }
        [Column("TotalAmount")]
        public decimal? TotalAmount { get; set; }
        [Column("otherspecifications")]
        public string OtherSpecifications { get; set; }
        [Column("lineno_1")]
        public string LineNo_1 { get; set; }
        [Column("isbuyer")]
        public bool IsBuyer { get; set; }
        [Column("isinternal")]
        public bool IsInternal { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("edit")]
        public bool Edit { get; set; }
        [Column("sizequantityrate")]
        public string SizeQuantityRate { get; set; }
        public List<SizeRangeQtyRate> SizeRangeQtyRateList { get; set; }
        //MultipleScheduleDetails
        [Column("multipleschedule")]
        public string MultipleSchedule { get; set; }
        public List<MultipleScheduleDetails> MultipleScheduleDetailsList { get; set; }
        //CartonDetails
        [Column("packtype")]
        public string packType { get; set; }
        [Column("noofcarton")]
        public string noOfCarton { get; set; }
        public List<CartonDetails> CartonDetailsList { get; set; }

        //Packing Details        
        [Column("packagesizerangemasterid")]
        public int PackageSizeRangeMasterId { get; set; }
        [Column("packingtype")]
        public string PackingType { get; set; }
        [Column("packingdetail")]
        public string PackingDetail { get; set; }
        public List<OePackingDetails> PackingDetailsList { get; set; }

        //Shipment Details
        [Column("shipmentcountrystamp")]
        public string ShipmentCountryStamp { get; set; }
        [Column("shipmentfrom")]
        public string ShipmentFrom { get; set; }
        [Column("shipmentto")]
        public string ShipmentTo { get; set; }
        [Column("otherinstruction")]
        public string OtherInstruction { get; set; }

        //Other Details
        [Column("paymentterms")]
        public string PaymentTerms { get; set; }
        [Column("deliveryterms")]
        public string DeliveryTerms { get; set; }
        [Column("insurance")]
        public string Insurance { get; set; }
        [Column("deliveryto")]
        public string DeliveryTo { get; set; }
        [Column("specialinstruction")]
        public string SpecialInstruction { get; set; }
        [Column("packinglabeling")]
        public string PackingLabeling { get; set; }

        [Column("bomerrormessage")]
        public string BOMErrorMessage { get; set; }
        [Column("partiesamount1")]
        public int? PartiesAmount1 { get; set; }
        [Column("partiesamount2")]
        public int? PartiesAmount2 { get; set; }
        [Column("shortunitid")]
        public int? ShortUnitID { get; set; }
        [Column("longunitid")]
        public int? LongUnitID { get; set; }
        //[Column("lineno_1")]
        //public string LineNo_1 { get; set; }
        //[Column("remarks")]
        //public string Remarks { get; set; }
        public List<InternalOrderForm> InternalOrderEntryList { get; set; }

    }
}
