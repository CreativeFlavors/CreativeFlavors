using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.JobWork
{
   public class ProductionSizeQc_Issue:BaseEntity
    {

        public int QcSize_id { get; set; }

        public int Qc_id { get; set; }

        public string Size { get; set; }
        public bool Size_Bool { get; set; }
        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
        public static DatabaseGeneratedOption? Identity { get; set; }
    }
}
