using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Core.Entities.GateEntry
{
    public class GateEntryVehicleMaster:BaseEntity
    {
        public int VehicleEntryId { get; set; }
        public string GateEntryNo { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public int VehicleId { get; set; }
        //public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string Destination { get; set; }
        public string Purpose { get; set; }
        public string Passengers { get; set; }
        public string MaterialTaken { get; set; }
        public string DCNo { get; set; }
        public string StartingKm { get; set; }
        public string ClosingKm { get; set; }
        public string KmUsed { get; set; }     
        public DateTime? ReturnDateTime { get; set; }
        public string PurposeofTravel { get; set; }
        public string Remarks { get; set; }
        public string InwardDcNo { get; set; }
        public DateTime? InwardDcDate { get; set; }
        public string InwardMaterial { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}