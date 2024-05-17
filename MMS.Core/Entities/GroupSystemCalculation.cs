using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class GroupSystemCalculation:BaseEntity
    {
       public int GroupSystemCalculationId { get; set; }
       public string ProductionPercent { get; set; }
       public decimal Amount { get; set; }      
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
