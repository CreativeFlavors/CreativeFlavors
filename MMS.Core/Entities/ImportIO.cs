using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class ImportIO : BaseEntity
    {
        public int IoNumberID { get; set; }
        public int SimpleMRPID { get; set; }
        public int Sno { get; set; }
        public string IoNO { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
       // public DateTime? CreatedDate { get; set; }
       // public DateTime? UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? Deletedon { get; set; }
        public bool IsDeleted { get; set; }

    }
}
