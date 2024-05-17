using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.OperationMasterModel
{
    public class OperationMasterModel
    {
        public int OperationMasterId { get; set; }
        public string OperationTypeCode { get; set; }
        public string OperationName { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<OperationMaster> OperationMasterList { get; set; }
  
    }
}