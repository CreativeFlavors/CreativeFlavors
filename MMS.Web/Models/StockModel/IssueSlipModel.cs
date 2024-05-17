using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StockModel
{
    public class IssueSlipModel
    {
        public int IssueSlipId { get; set; }
        public int IssueSlipFK { get; set; }
        public string OrderNo { get; set; }
        public string Style { get; set; }
        public string IssueType { get; set; }
        public int NoOfSets { get; set; }
        public string StoreName { get; set; }
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
        public List<tbl_issueslipdetails> IssueSlipList { get; set; }
    }
}