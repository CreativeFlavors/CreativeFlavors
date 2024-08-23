using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.ViewModel
{
    public class ProductionViewModel
    {
        public int ProductionId { get; set; }
        public string ProductionCode { get; set; }
        public decimal? ProductionQty { get; set; }
        public decimal RequiredQty { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductionStatus { get; set; }
        public string ProductionType { get; set; } // Indicates whether it's "Production" or "Subassembly"
    }
}
