using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
   public class JobSheet_pairCodeMaster: BaseEntity
    {
        public int jobsheet_pair_code_id { get; set; }

        public string jobsheet_pair_Code { get; set; }
        
        public string CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
