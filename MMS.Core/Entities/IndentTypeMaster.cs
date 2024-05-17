using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class IndentTypeMaster:BaseEntity
    {
        public int IndentMasterID { get; set; }
        public string IndentTypeCode { get; set; }
        public string IndentTypeName { get; set; }    
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
