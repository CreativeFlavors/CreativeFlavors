using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities.GateEntry
{
   public  class VehicleMaster:BaseEntity
    {

        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
