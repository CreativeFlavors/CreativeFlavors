using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
   public class CuttingSlipModel
    {
            public int? MaterialMasterId { get; set; }
            public string BuyerFullName { get; set; }
            public string BuyerPoNo { get; set; }
            public string BuyerStyleNo       { get; set; }
            public string BomNo          { get; set; }
            public string LotNo          { get; set; }
            public string SketchNo       { get; set; }
            public string BuyerOrderSlNo { get; set; }
            public string CustomerPlan   { get; set; }
            public string JobcardNo      { get; set; }
            public string SizeHeader     { get; set; }
            public string QtyHeader      { get; set; }
            public string SeasonName     { get; set; }
            public string GroupName      { get; set; }
            public string CategoryName   { get; set; }
            public string SizeRangeName { get; set; }
            public string MaterialDescription { get; set; }
            public string ShortUnitName { get; set; }
            public decimal? TotalNorms { get; set; }
            public decimal? SizeRange { get; set; }
            public decimal? SizeQty { get; set; }
            public string SizeBuyerPoNumber { get; set; }
  }
}
