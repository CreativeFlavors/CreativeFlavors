using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.Stock
{
   public class OePackingDetails:BaseEntity
    {
       public int OePackingDetailsId { get; set; }
       public string PackingType { get; set; }
       public int SizeRangeMasterId { get; set; }
       public string Size { get; set; }
       public int OrderEntryId { get; set; }
      // public date

    }
}
