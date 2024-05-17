using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class ImirGridDetails : BaseEntity
    {
        public int ImirGridId { get; set; }
        public string SlNo { get; set; }
        public string ParameterId { get; set; }
        public string Parameter { get; set; }
        public string InspectionFrequency { get; set; }
        public string RejectionQty { get; set; }
        public string GridRemarks { get; set; }
        public int ImirId { get; set; }
    }
}
