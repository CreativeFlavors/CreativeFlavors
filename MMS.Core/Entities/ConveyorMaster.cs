using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.Entities
{
    public class ConveyorMaster : BaseEntity
    {
        public int ConveyorMasterId { get; set; }
        public string ConveyorCode { get; set; }
        public string ConveyorName { get; set; }
        public int CapacityPerDay { get; set; }
        public string CapacityUnits { get; set; }
        public string ConveyorType { get; set; }      
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
