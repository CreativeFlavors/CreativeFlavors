using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace MMS.Core.Entities
{
    public class BOEShipmentMaster:BaseEntity
    {
       public int BOEId { get; set; }
       public string CountryStamp { get; set; }
       public DateTime? ShipmentFrom { get; set; }
       public DateTime? ShipmentTo { get; set; }
       public string OtherInstructionFromBuyer { get; set; }
       public string CreatedBy { get; set; }
       public string UpdatedBy { get; set; }
       public bool? IsDeleted { get; set; }
       public string DeletedBy { get; set; }
       public DateTime? DeletedDate { get; set; }
    }
}
