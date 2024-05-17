using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Views.UnitMaster
{
    public class BasicUnitMasterModel
    {
        public int UomMasterId { get; set; }
        public string ShortUnitName { get; set; }
        public string LongUnitName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}