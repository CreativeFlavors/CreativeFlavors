using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
  public  class IndentSizeRangeQty:BaseEntity
    {
        public int IdentSizeRangeID { get; set; }
        public string Size { get; set; }
        public decimal? quantity { get; set; }
        public decimal? Rate { get; set; }
        public int IndentMaterialID { get; set; }       
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
