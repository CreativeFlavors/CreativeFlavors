using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MMS.Core.Entities;    


namespace MMS.Web.Models.IndentTypeMasterModel
{
    public class IndentTypeMasterModel
    {
        public int IndentMasterID { get; set; }
        public string IndentTypeCode { get; set; }
        public string IndentTypeName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }

        public List<IndentMaster> IndentMasterList { get; set; }

    }
}