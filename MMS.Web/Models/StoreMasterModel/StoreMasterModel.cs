using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.StoreMasterModel
{
    public class StoreMasterModel
    {
        public int StoreMasterId { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string Location { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<StoreMaster> StoreMasterList { get; set; }
    }
}