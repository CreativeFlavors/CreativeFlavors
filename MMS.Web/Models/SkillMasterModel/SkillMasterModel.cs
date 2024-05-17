using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SkillMasterModel
{
    public class SkillMasterModel
    {

        public int SkillMasterId { get; set; }
        public string SkillCode { get; set; }
        public string SkillName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<SkillMaster> SkillMasterList { get; set; }
    }
}