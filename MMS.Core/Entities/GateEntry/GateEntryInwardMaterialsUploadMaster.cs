using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
   public class GateEntryInwardMaterialsUploadMaster : BaseEntity
    {
      public int  GateEntryInwardMaterialUploadId { get; set; }
      public int GateEntryInwardId { get; set; }
     public string UploadPath { get; set; }
     
    public string  CreatedBy { get; set; }
     public string UpdatedBy  { get; set; }
    public bool  IsDeleted { get; set; }
    }
}
