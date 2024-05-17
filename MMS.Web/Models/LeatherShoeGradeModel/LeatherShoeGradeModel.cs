using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.LeatherShoeGradeModel
{
    public class LeatherShoeGradeModel
    {
        public int LeatherShoesGradeMasterId { get; set; }
        public string GradeCode { get; set; }
        public string Grade { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<LeatherShoesGradeMaster> leatherShoesGradeMasterList { get; set; }
    }
}