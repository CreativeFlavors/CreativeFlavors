using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
  public  class SPMultipleIssueCount
    {
        public string MaterialName { get; set; }
        public int IssueSlipFK { get; set; }
        public int IssueSlipId { get; set; }
        public string InternalOderID { get; set; }
        public string IssueSlipNo { get; set; }
    }
    public class ApprovedPriceMail
    {
        public decimal? PriceRs { get; set; }
        public string MaterialDescription { get; set; }
        public string UpdatedBY { get; set; }
        public string createdby { get; set; }
        public Int64 RankOrder { get; set; }
        public int? materialid { get; set; }
        public string SupplierName { get; set; }
        public string approvedby { get; set; }
        public string StoreName { get; set; }
    }
}
