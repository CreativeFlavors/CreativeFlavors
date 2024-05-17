using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
    public class SizeProductionQc_IssueMaster :BaseEntity
    {
        public int QcSize_id { get; set; }

        public int Qc_id { get; set; }

        public string Size { get; set; }
        

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        
    }
}
