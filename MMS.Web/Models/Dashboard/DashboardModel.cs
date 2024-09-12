using MMS.Data.StoredProcedureModel;
using MMS.Web.Models.BuyerMaserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Web.Models.Dashboard
{
    public class DashboardModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
         public int usertype { get; set; }
        public int customercount { get; set; }
        public int suppliercount { get; set; }
        public int invoicegenrated { get; set; }
        public decimal? pendingpayment {  get; set; }
        public decimal? paymentdone { get; set; }
        public List<BuyerMasterModel> AddressdetailbuyerLists { get; set; }

    }
}