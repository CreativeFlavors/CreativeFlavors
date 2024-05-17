using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class SizeMatching:BaseEntity
    {
       public int SizeMatchingMasterID { get; set; }
      // public string SizeMatchingCode { get; set; }
       public string SizeMatchingName { get; set; }
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
