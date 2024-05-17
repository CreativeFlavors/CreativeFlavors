using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class MRPSelectedValues : BaseEntity
    {
        public int MRPSelectedID { get; set; }
        public string IONumberID { get; set; }
        public int SimpleMRPID { get; set; }    
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }

}
