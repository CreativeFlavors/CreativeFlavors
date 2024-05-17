using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class SizeScheduleDetails:BaseEntity
    {
       public int SizeScheduleDetailsId { get; set; }
       public int SizeNo { get; set; }
       public decimal Size { get; set; }
       public int SizeScheduleMasterId { get; set; }      
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
