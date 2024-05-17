using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.LeatherSize
{
    public class LeatherTypeModel
    {
        public int LeatherTypeID { get; set; }
        public string LeatherTypeCode { get; set; }
        public string LeatherTypeDescription { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedON { get; set; }
        public List<LeatherType> leatherTypeList { get; set; }

    }
}