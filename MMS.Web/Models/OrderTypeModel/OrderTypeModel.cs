using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.OrderTypeModel
{
    public class OrderTypeModel
    {
        public int OrderTypeID { get; set; }
        public string OrderTypeCode { get; set; }
        public string OrderTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<OrderType> OrderTypeList { get; set; }
    }
}