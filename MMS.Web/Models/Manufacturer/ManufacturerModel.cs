using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Manufacturer
{
    public class ManufacturerModel
    {
        public int ManufacturerMasterId { get; set; }
        public string ManufacturerCode { get; set; }
        public string ManufacturerName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<ManufacturerMaster> ManufacturerMasterList { get; set; }
    }
}