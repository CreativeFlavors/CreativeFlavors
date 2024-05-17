using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Core.Entities;
using System.Web.Mvc;

namespace MMS.Web.Models.StockModel
{
    public class OrderEntryModel
    {
        //Order Entry
        public int OrderEntryId { get; set; }
        [Remote("doesOrderExist", "BuyerOrderEntryForm", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string BuyerOrderSlNo { get; set; }
        public string LotNo { get; set; }
        public Pager Pager { get; set; }
        public string Count { get; set; }
        public string WeekNo { get; set; }
        public string Date { get; set; }
        public int? BuyerSeason { get; set; }
        public int? BuyerName { get; set; }
        public string OrderProjectionNo { get; set; }
        public string BuyerPoNo { get; set; }
        public string OurStyle { get; set; }
        public string LeatherDescription { get; set; }
        public decimal? DiscountPer { get; set; }
        public string QuoteNo { get; set; }
        public string CountryStamp { get; set; }
        public string CustomerPlan { get; set; }
        public string CustomerDate { get; set; }
        public int? AgentMasterId { get; set; }
        public string CommPer { get; set; }
        public string UploadError { get; set; }
        public string ExFactoryDate { get; set; }
        public string ShipmentMode { get; set; }
        public string SampleReqNo { get; set; }
        public string Brand { get; set; }
        public string BuyerStyleNo { get; set; }
        public string BarCodeNo { get; set; }
        public string BomNo { get; set; }
        public string Last { get; set; }
        public string ColorMasterId { get; set; }
        public string FinishedProdType { get; set; }
        public string ProductType_BuyerModel { get; set; }
        public string AmendmentNoWithDate { get; set; }
        public string TotalOrderForWeek { get; set; }
        public int? OrderType { get; set; }
        public int? Currency { get; set; }
        public int? Rs { get; set; }
        public int? GradeMasterId { get; set; }
        public int? SizeRangeMasterId { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsInternal { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int? ProductTypeId { get; set; }
        public bool Edit { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public int? Parties { get; set; }

        public decimal? TotalAmount { get; set; }
        public string TotalAmount_ { get; set; }

        public bool IsDeleted { get; set; }

        public string SEASON { get; set; }

        public string Purchase { get; set; }
        public List<InternalOrderEntryForm> OrderEntryList { get; set; }
        public List<OrderEntryForm> OrderFormList { get; set; }
        //SizeRangeQtyRate
        public string SizeQuantityRate { get; set; }
        public List<SizeRangeQtyRate> SizeRangeQtyRateList { get; set; }
        //MultipleScheduleDetails
        public string MultipleSchedule { get; set; }
        public List<MultipleScheduleDetails> MultipleScheduleDetailsList { get; set; }
        //CartonDetails
        public string packType { get; set; }
        public string noOfCarton { get; set; }
        public List<CartonDetails> CartonDetailsList { get; set; }

        //Packing Details        
        public int PackageSizeRangeMasterId { get; set; }
        public string PackingType { get; set; }
        public string PackingDetail { get; set; }
        public List<OePackingDetails> PackingDetailsList { get; set; }

        //Shipment Details
        public string ShipmentCountryStamp { get; set; }
        public string ShipmentFrom { get; set; }
        public string ShipmentTo { get; set; }
        public string OtherInstruction { get; set; }

        //Other Details
        public string PaymentTerms { get; set; }
        public string DeliveryTerms { get; set; }
        public string Insurance { get; set; }
        public string DeliveryTo { get; set; }
        public string SpecialInstruction { get; set; }        
        public string PackingLabeling { get; set; }
        public List<SelectListItem> OrderStyleList { get; set; }
        public string BOMErrorMessage { get; set; }
        public int? PartiesAmount1 { get; set; }
        public int? PartiesAmount2 { get; set; }
        public int? ShortUnitID { get; set; }
        public int? LongUnitID { get; set; }
        public string LineNo_1 { get; set; }
        public string Remarks { get; set; }

    }
     
    public class SizeQuantityRateObject 
    {
        public string Size { get; set; }
        public string quantity { get; set; }
        public string Rate { get; set; }
    }

    public class MultipleScheduleObject
    {
        public string Io { get; set; }
        public string Size { get; set; }
        public string quantity { get; set; } 
        public string Date { get; set; }
    }

    public class PackingDetailObject
    {
        public string PackingType { get; set; }
        public string SizeRangeMasterId { get; set; }
        public string Size { get; set; } 
    }
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

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}