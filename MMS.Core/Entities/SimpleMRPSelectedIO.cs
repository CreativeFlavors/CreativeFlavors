using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class SimpleMRPSelectedIO
    {
        public int SelectedIO { get; set; }
        public int SimpleMRPID { get; set; }
        public string IO { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBY { get; set; }
        public DateTime? DeletedON { get; set; }
        public string DeletedBy { get; set; }
        public string IsDeleted { get; set; }


    }
}
