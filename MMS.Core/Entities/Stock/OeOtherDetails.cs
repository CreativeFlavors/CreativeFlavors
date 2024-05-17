using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
  public  class OeOtherDetails:BaseEntity
    {
      public int OeOtherDetailsId { get; set; }
      public string PaymentTerms { get; set; }
      public string DeliveryTerms { get; set; }
      public string Insurance { get; set; }
      public string DeliverTo { get; set; }
      public string SpecialInstructions { get; set; }
      public string PackingOrLabelling { get; set; }
      public int OrderEntryId { get; set; }
    }
}
