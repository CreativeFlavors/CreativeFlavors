using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class StockConversionModel
    {
        public int StockConversionId { get; set; }
        public string DocNo { get; set; }
        public string FromStore { get; set; }
        public string ToStore { get; set; }
        public string Date { get; set; }
        public string Remarks { get; set; }
        public bool StockValueChange { get; set; }
        public int MaterialGroupId { get; set; }
        public int UomId { get; set; }
        public int MaterialMasterId { get; set; }
        public int ColourMasterId { get; set; }
        public int IoNo { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public string ReservedStock { get; set; }
        public string FreeStock { get; set; }
        public string StockInAllStores { get; set; }
        public string StockInCurrentStores { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<StockConversionForm> StockConversionFormList { get; set; }
    }
}