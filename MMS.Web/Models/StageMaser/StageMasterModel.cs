using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StageMaser
{
    public class StageMasterModel
    {
        public int StageMasterId { get; set; }
        public int ProcessCode { get; set; }
        public string OrderType { get; set; }
        public string StageCode { get; set; }
        public string StageName { get; set; }
        public string SubStage { get; set; }
         public int? Sequence { get; set; }
        public bool IsInspection { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<StageMaster> StageMasterList { get; set; }

       
    }
}