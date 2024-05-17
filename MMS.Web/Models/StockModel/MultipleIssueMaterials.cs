using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class MultipleIssueMaterialsModel
    {
        public int IssueSlipId { get; set; }
        public int? IssueSlipFK { get; set; }
        public string OrderNo { get; set; }
        public string Style { get; set; }
        public string IssueType { get; set; }
        public decimal? NoOfSets { get; set; }
        public string StoreName { get; set; }
        public string LotNo { get; set; }
        public string MaterialName { get; set; }
        public string ColourId { get; set; }
        public string CategoryId { get; set; }
        public string GroupId { get; set; }
        public decimal? RequiredQty { get; set; }
        public decimal? AlredayIssued { get; set; }
        public decimal? CurrentIssue { get; set; }
        public decimal? Rate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsChecked { get; set; }
        public decimal? Piecies { get; set; }
        public decimal? CurrentStock { get; set; }
        public string IssueSlipType { get; set; }
        public int? MaterialMasterId { get; set; }
        public int? StoreMasterId { get; set; }
        public int? BomStyle { get; set; }
        public int? Season { get; set; }  
        public int? BuyerMasterId { get; set; }
        public string MaterialTypes { get; set; }
    }
}