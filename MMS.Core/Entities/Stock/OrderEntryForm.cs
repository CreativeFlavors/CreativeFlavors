using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
   public class OrderEntryForm : BaseEntity
    {
        public int OrderEntryId { get; set; }
        public string BuyerOrderSlNo { get; set; }
        public string LotNo { get; set; }
        public string Count { get; set; }
        public string WeekNo { get; set; }
        public DateTime? Date { get; set; }
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
        public DateTime? CustomerDate { get; set; }
        public int? AgentMasterId { get; set; }
        public string CommPer { get; set; }
        public DateTime? ExFactoryDate { get; set; }
        public string ShipmentMode { get; set; }
        public string SampleReqNo { get; set; }
        public string Brand { get; set; }
        public string BuyerStyleNo { get; set; }
        public string BarCodeNo { get; set; }
        public string BomNo { get; set; }
        public string Last { get; set; }
        public string ColorMasterId { get; set; }
        public string FinishedProdType { get; set; }
        public int? ProductTypeId { get; set; }
        public string AmendmentNoWithDate { get; set; }
        public string TotalOrderForWeek { get; set; }
        public int? OrderType { get; set; }
        public int? Currency { get; set; }
        public int? Rs { get; set; }
        public int? Parties { get; set; }
        public Guid? GradeMasterId { get; set; }
        public int? SizeRangeMasterId { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string LineNo_1 { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsInternal { get; set; }
        public int? PartiesAmount1 { get; set; }
        public int? PartiesAmount2 { get; set; }
        public int? ShortUnitID { get; set; }
        public int? LongUnitID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public int TotalAmount { get; set; }

        public bool IsDeleted { get; set; }
    }
}
