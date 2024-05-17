using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class SizeRangeMaster:BaseEntity
    {
       public int SizeRangeMasterId { get; set; }
       public string SizeRangeRef { get; set; }
       public string SizeRangeRefValue { get; set; }      
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
