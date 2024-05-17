using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class SupplierSubMaster:BaseEntity
    {
        public int SupplierSubMasterID { get; set; }
        public int SupplierMasterFKID { get; set; }
        public int MaterialGroupID { get; set; }
        public int MateriaSublMasterID { get; set; }
      
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
