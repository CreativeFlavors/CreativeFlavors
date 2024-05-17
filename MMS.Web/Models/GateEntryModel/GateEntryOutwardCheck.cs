using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GateEntryModel
{
    public class GateEntryOutwardCheck
    {
        public string IssueSlipNo { get; set; }
        public int? TotalQty { get; set; }
        public string GroupId { get; set; }
        public string CategoryId { get; set; }
        public string StoreName { get; set; }
        public string MaterialName { get; set; }
        public string ColourId { get; set; }
        public decimal? RequiredQty { get; set; }
        public decimal? AlredayIssued { get; set; }
        public decimal? CurrentIssue { get; set; }
        public string BalanceInThisListlot { get; set; }
        public int? sheet { get; set; }
        public decimal? Rate { get; set; }

        //public List<GateEntryOutwardCheck> GateEntryOutwardCheckList { get; set; }

    }
}



