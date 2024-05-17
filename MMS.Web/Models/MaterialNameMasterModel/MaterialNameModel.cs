using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models
{
    public class MaterialNameModel
    {
        public int MaterialMasterID { get; set; }
        public int MaterialGroupMasterId { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
        public string LeatherMaterilFirstValue { get; set; }
        public int? LeatherMaterialName { get; set; }
        public string LeatherMaterilLastValue { get; set; }
       
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<tbl_materialnamemaster> MaterilNameList { get; set; }

        public string MaterialGroup { get; set; }
    }
}