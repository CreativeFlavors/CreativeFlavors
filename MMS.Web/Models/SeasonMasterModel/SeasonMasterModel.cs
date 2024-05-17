using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;
using System.ComponentModel.DataAnnotations;


namespace MMS.Web.Models.SeasonMasterModel
{
    public class SeasonMasterModel
    {
        public int SeasonMasterId { get; set; }
        public string SeasonName { get; set; }
        public string SpringDescription { get; set; }
        public string SpringSummerYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public string PeriodFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public string PeriodTo { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<SeasonMaster> SeasonMasterList { get; set; }
    }
}