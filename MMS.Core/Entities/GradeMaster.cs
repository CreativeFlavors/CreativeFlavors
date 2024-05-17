using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
   public class GradeMaster:BaseEntity2
    {
        public int GradeMasterID { get; set; }
        public string CodeNo { get; set; }
        public string Category { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Efficiency { get; set; }       
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
