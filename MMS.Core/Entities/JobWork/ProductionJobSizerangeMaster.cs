using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class ProductionJobSizerangeMaster:BaseEntity
    {
        public int Job_Production_Size_id { get; set; }
        public int Job_Production_pair_id { get; set; }
        public decimal Sizerange { get; set; }
        public decimal Qty { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
