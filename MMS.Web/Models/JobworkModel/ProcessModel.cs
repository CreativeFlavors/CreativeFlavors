using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities.JobWork;

namespace MMS.Web.Models.JobworkModel
{
    public class ProcessModel
    {
        public int ProcessId { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public bool Ioinvolved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProcessMaster> ProcesMasterList { get; set; }
    }
}