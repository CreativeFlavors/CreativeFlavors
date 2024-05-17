using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
   public class Jobsheet_pairSizerangeMaster: BaseEntity
    {
        public int Job_sheet_pair_Size_id { get; set; }
        public int jobsheet_pair_id { get; set; }
        public int jobsheet_pair_Code_id { get; set; }
        public decimal Sizerange { get; set; }
        public decimal Qty { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
