using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace MMS.Web.Models.BOEShipmentModel
{
    public class BOEShipmentModel
    {
        public int BOEId { get; set; }
        public string CountryStamp { get; set; }
        public string ShipmentFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public string ShipmentTo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public string OtherInstructionFromBuyer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<BOEShipmentMaster> BOEShipmentMastetList { get; set; }
    }
}