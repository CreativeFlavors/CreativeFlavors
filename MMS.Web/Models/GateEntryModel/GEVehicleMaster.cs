using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.GateEntryModel
{
    public class GEVehicleMaster
    {
        public int VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public List<VehicleMaster> vehicleList { get; set;}

    }
}