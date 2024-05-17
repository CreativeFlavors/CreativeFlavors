using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Substance
{
    public class SubstanceMasterModel
    {
        public int SubstanceMasterId { get; set; }
        public string SubstanceRange { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<SubstanceMaster> SubstanceMasterList { get; set; }
    }
}