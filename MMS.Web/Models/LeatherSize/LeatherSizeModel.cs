using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.LeatherSize
{
    public class LeatherSizeModel
    {
        public int LeatherSizeMasterId { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Average { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<LeatherSizeMaster> LeatherMasterList { get; set; }
    }
}