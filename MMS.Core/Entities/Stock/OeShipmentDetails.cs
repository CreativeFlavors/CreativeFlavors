using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
   public class OeShipmentDetails:BaseEntity
    {
       public int OeShipmentDetailsId { get; set; }
       public string CountryStamp { get; set; }
       public string ShipmentFrom { get; set; }
       public string ShipmentTo { get; set; }
       public string OtherInstruction { get; set; }
       public int OrderEntryId { get; set; }
     
    }
}
