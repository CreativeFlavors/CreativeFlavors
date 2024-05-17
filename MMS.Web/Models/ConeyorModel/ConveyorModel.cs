using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.ConeyorModel
{
    public class ConveyorModel
    {
        public int ConveyorMasterId { get; set; }
        public string ConveyorCode { get; set; }
        public string ConveyorName { get; set; }
        public int CapacityPerDay { get; set; }
        public string CapacityUnits { get; set; }
        public string ConveyorType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string UOMMasterID { get; set; }
        public List<ConveyorMaster> ConveyorMasterList { get; set; }
    }
}