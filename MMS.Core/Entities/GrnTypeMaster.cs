using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class GrnTypeMaster:BaseEntity
    {
       public int GrnTypeMasterId { get; set; }
       public string IssueType { get; set; }    
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
       public bool IsJobWork { get; set; }

    }
}
