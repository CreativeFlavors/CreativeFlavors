using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class  ApprovedPriceListHistoryModel
    {
        public int ApprovePriceListHistoryID { get; set; }
        public int ApprovedPriceID { get; set; }
        public int SupplierId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? PriceRs { get; set; }
        public int Uom { get; set; }
        public int CategoryID { get; set; }
        public int TaxDetails { get; set; }
        public int GroupID { get; set; }
        public int MaterialID { get; set; }
        public int ColorID { get; set; }
        public string LeadTime { get; set; }
        public string MRPMargin { get; set; }
        public decimal? MRPPrice { get; set; }
        public decimal? AccessibleValue { get; set; }
        public string SupplierDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBY { get; set; }
       
    }
}