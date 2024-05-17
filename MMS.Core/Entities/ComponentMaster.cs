using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class ComponentMaster:BaseEntity
    {
       public int ComponentMasterId { get; set; }
       public string ComponentName { get; set; }
       public string UsedIn { get; set; }
       public string Image { get; set; }
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
