using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
   public  class Purpose : BaseEntity
    {
        public int? PurposeId { get; set; }
        public string PurposeDetails { get; set; }
       
    }
}
