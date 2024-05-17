using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class MRPSizeRange:BaseEntity
    {
        public int MRPSizeRangeID { get; set; }
        public string Size { get; set; }
        public decimal? Qty { get; set; }
        public int? BOMMaterialID { get; set; }     
        public int? MaterialName { get; set; }
        public string OrderNo { get; set; }
        public int SimpleMRPID { get; set; }

    }
}
