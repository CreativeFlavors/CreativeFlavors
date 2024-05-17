using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Models.GateEntryModel
{
    public class GateEntryVehicle
    {
        public int VehicleEntryId { get; set; }
        public string GateEntryNo { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public string EntryDate { get; set; }
        public string ReturnDate { get; set; }
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public IEnumerable<string> selectedid { get; set; }
        public IEnumerable<string> selectedempid { get; set; }
        public IEnumerable<string> selectedInwardDcid { get; set; }

        public List<GateEntryVehicleMaster> GateEntryVehiclelist { get; set; }

        public List<GateEntryOutwardList> GateEntryOutwardList { get; set; }
      //  public List<int> GateEntryOutwardId { get; set; }

    }

    public class GateEntryOutwardList
    {
        public int OutwardDcNo { get; set; }
        public int? MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int GateEntryOutwardId { get; set; }
    }
    public class GateEntryInwardList
    {
        public int InwardDcNo { get; set; }
        public int? MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int GateEntryInwardId { get; set; }
        public string DcDate { get; set; }
    }

}