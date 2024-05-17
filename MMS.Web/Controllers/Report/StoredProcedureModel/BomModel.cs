using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Controllers.Report.StoredProcedureModel
{
    public class BomModel
    {
        public int BomId { get; set; }
        public string BomNo { get; set; }
        public string Description { get; set; }
        public string BuyerFullName { get; set; }
        public string MeanSize { get; set; }
        public string CategoryName { get; set; }
        public string GroupName { get; set; }
        public string StoreName { get; set; }
        public string MaterialDescription  { get; set; }
        public string ShortUnitName { get; set; } 
        public string TotalNorms { get; set; }
        public decimal NoOfSets { get; set; }
        public string SizeRangeName { get; set; }
        public int MaterialCategoryMasterId { get; set; }
        public string LotNo { get; set; }
        public string SeasonName { get; set; }

    }
}