using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.StoredProcedureModel
{
    public class SizeRangeIssueModelcs
    {
        public int? MaterialMasterId { get; set; }
        public string MaterialDescription { get; set; }
        public decimal? IssueSize1 { get; set; }
        public decimal? StockInHand { get; set; }

    }
}
