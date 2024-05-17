using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Core.Entities;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using MMS.Data.Mapping;

namespace MMS.Web.Models.StockModel
{
    [Table("orderentrymodel", Schema = "public")]
    public class OrderEntryModel
    {
        //Order Entry
        [System.ComponentModel.DataAnnotations.Key]
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
        [Remote("doesOrderExist", "BuyerOrderEntryForm", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        [Column("buyerorderslno")]
        public string BuyerOrderSlNo { get; set; }
        [Column("lotno")]
        public string LotNo { get; set; }
        [Column("pager")]
        public Pager Pager { get; set; }
        [Column("count")]
        public string Count { get; set; }
        [Column("weekno")]
        public string WeekNo { get; set; }
        [Column("date")]
        public string Date { get; set; }
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
        public decimal? DiscountPer { get; set; }
        [Column("quoteno")]
        public string QuoteNo { get; set; }
        [Column("countrystamp")]
        public string CountryStamp { get; set; }
        [Column("customerplan")]
        public string CustomerPlan { get; set; }
        [Column("customerdate")]
        public string CustomerDate { get; set; }
        [Column("agentmasterid")]
        public int? AgentMasterId { get; set; }
        [Column("commper")]
        public string CommPer { get; set; }
        [Column("uploadError")]
        public string UploadError { get; set; }
        [Column("exfactorydate")]
        public string ExFactoryDate { get; set; }
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
        [Column("producttype_buyermodel")]
        public string ProductType_BuyerModel { get; set; }
        [Column("amendmentnowithdate")]
        public string AmendmentNoWithDate { get; set; }
        [Column("totalorderforweek")]
        public string TotalOrderForWeek { get; set; }
        [Column("ordertype")]
        public int? OrderType { get; set; }
        [Column("currency")]
        public int? Currency { get; set; }
        [Column("rs")]
        public int? Rs { get; set; }
        [Column("grademasterid")]
        public int? GradeMasterId { get; set; }
        [Column("sizerangemasterid")]
        public int? SizeRangeMasterId { get; set; }
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
        [Column("producttypeid")]
        public int? ProductTypeId { get; set; }
        [Column("edit")]
        public bool Edit { get; set; }
        [Column("remarks1")]
        public string Remarks1 { get; set; }
        [Column("remarks2")]
        public string Remarks2 { get; set; }
        [Column("parties")]
        public int? Parties { get; set; }
        [Column("totalamount")]
        public decimal? TotalAmount { get; set; }
        [Column("totalamount_")]
        public string TotalAmount_ { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
        [Column("season")]
        public string SEASON { get; set; }
        [Column("purchase")]
        public string Purchase { get; set; }
        public List<OrderEntry> OrderEntryList { get; set; }
        public OrderEntry ObjOrderEntry { get; set; }
        public List<OrderEntry> OrderFormList { get; set; }
        public List<order_details> OrderDetailsList { get; set; }
        public List<order_header> orderheaderList { get; set; }
        public order_header objorderheader { get; set; }
        //SizeRangeQtyRate
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
        public List<SelectListItem> OrderStyleList { get; set; }
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
        [Column("lineno_1")]
        public string LineNo_1 { get; set; }
        [Column("remarks")]
        public string Remarks { get; set; }
        [NotMapped]
        public string CustomerName { get; set; }
        [NotMapped]
        public string OrderSelectID { get; set; }
        [Column("processstatus")]
        public int ProcessStatus { get; set; }
    }

    [Table("sizequantityrateobject", Schema = "public")]
    public class SizeQuantityRateObject
    {
        [Column("size")]
        public string Size { get; set; }
        [Column("quantity")]
        public string quantity { get; set; }
        [Column("rate")]
        public string Rate { get; set; }
    }
    [Table("multiplescheduleobject", Schema = "public")]
    public class MultipleScheduleObject
    {
        [Column("io")]
        public string Io { get; set; }
        [Column("size")]
        public string Size { get; set; }
        [Column("quantity")]
        public string quantity { get; set; }
        [Column("date")]
        public string Date { get; set; }
    }
    [Table("packingdetailobject", Schema = "public")]
    public class PackingDetailObject
    {
        [Column("packingtype")]
        public string PackingType { get; set; }
        [Column("sizerangemasterid")]
        public string SizeRangeMasterId { get; set; }
        [Column("size")]
        public string Size { get; set; }
    }
    [Table("pager", Schema = "public")]
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
        [Column("totalitems")]
        public int TotalItems { get; private set; }
        [Column("currentpage")]
        public int CurrentPage { get; private set; }
        [Column("pagesize")]
        public int PageSize { get; private set; }
        [Column("totalpages")]
        public int TotalPages { get; private set; }
        [Column("startpage")]
        public int StartPage { get; private set; }
        [Column("endpage")]
        public int EndPage { get; private set; }
    }
}