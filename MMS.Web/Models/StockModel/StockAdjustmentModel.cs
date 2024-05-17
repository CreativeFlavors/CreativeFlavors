using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class StockAdjustmentModel
    {
        public int StockAdjustmentId { get; set; }
        public string AsOnDate { get; set; }
        public string StockNo { get; set; }
        public int StoreId { get; set; }
        public int CategoryId { get; set; }
        public int GroupId { get; set; }
        public int SubGroupId { get; set; }
        public int? MaterialType { get; set; }
        public int MaterialMasterId { get; set; }
        public int UomId { get; set; }
        public decimal Rate { get; set; }
        public string Size { get; set; }
        public string QTY { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string SizeQuantityRate { get; set; }
        public bool IsApproved { get; set; }
        public List<StockAdjustmentForm> StockAdjustmentFormList { get; set; }

        public List<SizeItemsStockAdjustment> SizeRangeQtyRateList { get; set; }

    }
}