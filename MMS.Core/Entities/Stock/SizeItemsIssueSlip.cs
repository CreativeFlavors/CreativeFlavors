using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class SizeItemsIssueSlip : BaseEntity
    {
        public int SizeMaterialD { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }
        public string SizeRange { get; set; }
        public int? MultipleIssueslipFk { get; set; }
        public string CreatedBY { get; set; }
        public string UpdatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
   
}
