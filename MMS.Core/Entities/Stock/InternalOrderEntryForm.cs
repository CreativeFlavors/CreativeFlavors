using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    [Table("internalorderform", Schema = "public")]
    public class InternalOrderForm : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("orderentryid")]
        public int OrderEntryId { get; set; }
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
        public decimal? DiscountPer { get; set; }
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
        [Column("rs")]
        public int? Rs { get; set; }
        [Column("parties")]
        public int? Parties { get; set; }
        [Column("grademasterid")]
        public int? GradeMasterId { get; set; }
        [Column("sizerangemasterid")]
        public int? SizeRangeMasterId { get; set; }
        [Column("remarks1")]
        public string Remarks1 { get; set; }
        [Column("remarks2")]
        public string Remarks2 { get; set; }
        [Column("lineno_1")]
        public string LineNo_1 { get; set; }
        [Column("isbuyer")]
        public bool IsBuyer { get; set; }
        [Column("isinternal")]
        public bool IsInternal { get; set; }
        [Column("partiesamount1")]
        public int? PartiesAmount1 { get; set; }
        [Column("partiesamount2")]
        public int? PartiesAmount2 { get; set; }
        [Column("shortunitid")]
        public int? ShortUnitID { get; set; }
        [Column("longunitid")]
        public int? LongUnitID { get; set; }
        [Column("createdby")]
        public string CreatedBy { get; set; }
        [Column("updatedby")]
        public string UpdatedBy { get; set; }
        [Column("totalamount")]
        public decimal? TotalAmount { get; set; }
        [Column("isdeleted")]
        public bool IsDeleted { get; set; }
       
        [Column("season")]
        public string SEASON { get; set; }
        
        [Column("purchase")]
        public string Purchase { get; set; }
     
        [Column("stage1")]
        public bool Stage1 { get; set; }
       
        [Column("stage2")]
        public bool Stage2 { get; set; }
       
        [Column("stage3")]
        public bool Stage3 { get; set; }
       
        [Column("stage4")]
        public bool Stage4 { get; set; }
       
        [Column("stage5")]
        public bool Stage5 { get; set; }
       
        [Column("stage6")]
        public bool Stage6 { get; set; }
        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
