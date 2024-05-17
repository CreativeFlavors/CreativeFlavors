using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
   public class GateEntryInwardDocumentUploadMaster:BaseEntity
    { 
        public int GateEntryDocumentuploadId { get; set; }
      public int GateEntryDocumentId { get; set; }
      public string UploadPath { get; set; }     
       public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
       public bool IsDeleted { get; set; }

      }
}
