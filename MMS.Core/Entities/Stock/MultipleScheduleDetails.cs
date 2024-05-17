using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
    public class MultipleScheduleDetails : BaseEntity
    {
        public int MultipleScheduleDetailsId { get; set; }
        public string Io { get; set; }
        public string Size { get; set; }
        public string Qty { get; set; }
        public string ExFDt { get; set; }
        public int OrderEntryId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
