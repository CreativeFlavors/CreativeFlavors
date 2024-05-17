using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class LeatherType:BaseEntity
    {
        public int LeatherTypeID { get; set; }
        public string LeatherTypeCode { get; set; }
        public string LeatherTypeDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedON { get; set; }

    }
}
