using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.SizeMatchingModel
{
    public class SizeMatchingModel
    {
        public int SizeMatchingMasterID { get; set; }
        public string SizeMatchingCode { get; set; }
        public string SizeMatchingName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public List<SizeMatching> sizeMatchinglist { get; set; }

    }
}