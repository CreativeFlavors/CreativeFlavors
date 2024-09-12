using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.BuyerMaserModel;
using MMS.Web.Models.CustomerTransaction;
using MMS.Web.Models.Dashboard;
using MMS.Web.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult dashboard()
        {
            Supplier_masterManager supplier_MasterManager = new Supplier_masterManager();
            DashboardModel model = new DashboardModel();
            UserManager userManager = new UserManager();
            var data = userManager.GetLogin();
            OrderHeaderManager manager = new OrderHeaderManager();
            var Customertransactionlist = manager.Get();
            CustomertransactionManager customertransactionmanager = new CustomertransactionManager();
            var data2 = customertransactionmanager.Get();
            BuyerMasterManager buyerMasterManager = new BuyerMasterManager();
            var buyers = buyerMasterManager.GetBuyerModels();
            var supplierlist = supplier_MasterManager.Get();

            var totaldata = buyers.Select(b => new BuyerMasterModel
            {
                BuyerMasterId = b.BuyerMasterId,
                BuyerCode = b.BuyerCode,
                CustomerName = b.CustomerName,
                Account = b.Account,
                Physical1 = b.Physical1,
                Telephone1 = b.Telephone1,
                EmailContact = b.EmailContact

            }).ToList();

            model.FirstName = data.FirstName;
            model.usertype = data.UserType;
            model.LastName = data.LastName;
            model.customercount = totaldata.Count();
            model.suppliercount = supplierlist.Count();
            model.invoicegenrated = Customertransactionlist.Count();
            model.AddressdetailbuyerLists = totaldata;
            decimal? paidamount1 = 0;
            decimal? balance = 0;

            foreach (var item in Customertransactionlist)
            {
                decimal? paidamount = 0;
                List<customertransaction> customertransactions = data2.Where(W => W.InvRefNumber == item.invoicehd_id).ToList();
                foreach (var i in customertransactions)
                {
                    paidamount += i.InvPaidAmount;
                }
                 paidamount1 = paidamount;
                var InvBalanceAmount = item.TotalPrice - paidamount;
                 balance += InvBalanceAmount;
            }
            model.pendingpayment = balance;
            model.paymentdone = paidamount1;

            return View(model);
        }

    }
}
