using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class OrderType:BaseEntity
    {
       public int OrderTypeID { get; set; }
       public string OrderTypeCode { get; set; }
       public string OrderTypeName { get; set; }     
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
