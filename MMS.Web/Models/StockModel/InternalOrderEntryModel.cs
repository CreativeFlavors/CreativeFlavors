using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.Stock;

namespace MMS.Web.Models.StockModel
{
    public class InternalOrderEntryModel
    {

        public int InternalEntryOrderId { get; set; }
        public string BuyerOrderSlNo { get; set; }
        public string LotNo { get; set; }
        public string Count { get; set; }
        public string WeekNo { get; set; }
        public DateTime? Date { get; set; }
        public string BuyerSeason { get; set; }
        public string BuyerName { get; set; }
        public string OrderProjectionNo { get; set; }
        public string BuyerPoNo { get; set; }
        public string OurStyle { get; set; }
        public string LeatherDescription { get; set; }
        public string DiscountPer { get; set; }
        public string QuoteNo { get; set; }
        public string CountryStamp { get; set; }
        public string CustomerPlan { get; set; }
        public DateTime? CustomerDate { get; set; }
        public int AgentMasterId { get; set; }
        public string CommPer { get; set; }
        public DateTime? ExFactoryDate { get; set; }
        public string ShipmentMode { get; set; }
        public string SampleReqNo { get; set; }
        public string Brand { get; set; }
        public string BuyerStyleNo { get; set; }
        public string BarCodeNo { get; set; }
        public string BomNo { get; set; }
        public string Last { get; set; }
        public int ColorMasterId { get; set; }
        public string FinishedProdType { get; set; }
        public string ProductType_BuyerModel { get; set; }
        public string AmendmentNoWithDate { get; set; }
        public string TotalOrderForWeek { get; set; }
        public int OrderType { get; set; }
        public int Currency { get; set; }
        public string Parties { get; set; }
        public int GradeMasterId { get; set; }
        public int SizeRangeMasterId { get; set; }
        public string Remarks { get; set; }
        public string OtherSpecifications { get; set; }
        public string LineNo_1 { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsInternal { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Edit { get; set; }

        public List<InternalOrderEntryForm> InternalOrderEntryList { get; set; }

    }
}