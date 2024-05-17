using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class ApprovedPriceList : BaseEntity
    {
        public int ApprovedPriceID { get; set; }
        public int SupplierId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? PriceRs { get; set; }
        public int Uom { get; set; }
        public int CategoryID { get; set; }
        public int TaxDetails { get; set; }
        public int GroupID { get; set; }
        public int CurrencyID { get; set; }
        public int MaterialID { get; set; }
        public int ColorID { get; set; }
        public string LeadTime { get; set; }
        public string MRPMargin { get; set; }
        public decimal? MRPPrice { get; set; }
        public decimal? AccessibleValue { get; set; }
        public string SupplierDescription { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBY { get; set; }
        public string POExcessPercentage { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }

    }
}
