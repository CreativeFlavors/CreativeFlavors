using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.BuyerMaserModel
{
    public class BuyerModels
    {
        public int BuyerModelID { get; set; }
        public string BuyerModelName { get; set; }
        public string BuyerModelCode { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<BuyerModel> BuyerModelList { get; set; }
    }
}