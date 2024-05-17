using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.OrginMasterModel
{
    public class OrginMasterModel
    {
        public int OriginMasterId { get; set; }
        public string Code { get; set; }
        public string OriginName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<OrginMaster> OrginMasterlist { get; set; }
    }
}