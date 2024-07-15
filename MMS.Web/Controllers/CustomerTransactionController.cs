using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.CustomerTransaction;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class CustomerTransactionController : Controller
    {
        public ActionResult Customermaster()
        {
            Customertransaction model = new Customertransaction();
            return View(model);
        }
        [HttpGet]
        public ActionResult CustomerTransactionGrid(int page = 1, int pageSize = 15)
        {

            Customertransaction model = new Customertransaction();

            OrderHeaderManager manager = new OrderHeaderManager();
            var Customertransactionlist = manager.Get();
            BuyerManager BuyerManager = new BuyerManager();
            var data1 = BuyerManager.Get();
            CustomertransactionManager customertransactionmanager = new CustomertransactionManager();
            var data2 = customertransactionmanager.Get();
            paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
            var data3 = paymentmethodmanager.Get();

            List<Customertransaction> totalList = new List<Customertransaction>();

            foreach (var item in Customertransactionlist)
            {
                Customertransaction customertransaction = new Customertransaction();
                customertransaction.InvDate = item.invoicedate;
                customertransaction.InvAmount = item.TotalPrice;
                customertransaction.InvHDNumber = item.invoicehd_id;
                customertransaction.RefItems = item.invoice_items;
                customertransaction.RefQuantity = item.Quantity;
                customertransaction.BuyerMaster = data1.Where(W => W.BuyerMasterId == item.CustomerId).ToList().FirstOrDefault();
                customertransaction.customertransaction = data2.Where(W => W.InvRefNumber == item.invoicehd_id).ToList().FirstOrDefault();
                totalList.Add(customertransaction);
            }
       

            var totalCount = totalList.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(ct => ct.Invoicedt_Id)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;


            return PartialView("Partial/CustomerTransactionGrid",totalList);
        }
        [HttpGet]
        public ActionResult CustomerTransaction(int id)
        {
            Customertransaction model = new Customertransaction();
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            OrderDetailsManager orderDetailsManager = new OrderDetailsManager();
            OrderHeaderManager OrderHeaderManager = new OrderHeaderManager();
            BuyerManager BuyerManager = new BuyerManager();
            AccounttypeManager accounttypeManager = new AccounttypeManager();
            CustomertransactionManager Customertransactions = new CustomertransactionManager();
            var orderhd = OrderHeaderManager.Get(id);
            var orderDT = orderDetailsManager.Get(orderhd.invoicehd_id);
            var buyer = BuyerManager.GetBuyerMasterId(orderhd.CustomerId);
            var SODT = SalesorderDT_Manager.GetSO(orderDT.SalesOrderId_dt);
            var daylist = accounttypeManager.GettypeId(buyer.accountypeid);
            var balance = Customertransactions.GetiNId(id);

            var Termdays = daylist.TermDays;

            model.InvHDNumber = orderhd.invoicehd_id;
            model.customer = buyer.BuyerFullName;
            model.buyercode = buyer.BuyerCode;
            model.InvDate = orderhd.invoicedate;
            model.RefItems = orderhd.invoice_items;
            model.RefQuantity = orderhd.Quantity;
            model.InvAmount = orderhd.TotalPrice;
            model.Sonumber = SODT.Salesorderid_hd;
            model.InvDueDate = orderhd.invoicedate;
            model.SoDate = SODT.salesorderdate;
            model.buyerid = orderhd.CustomerId;
            if (balance != null)
            {
                model.InvBalanceAmount = balance.InvBalanceAmount;
                model.InvPaidAmount = balance.InvPaidAmount;
                model.Id = balance.Id;
            }
            else
            {
                model.InvBalanceAmount = orderhd.TotalPrice;
            }
            var PaymentDate = orderhd.invoicedate;
            DateTime startDate = PaymentDate;
            model.PaymentDate = startDate.AddDays(Termdays);

            return PartialView("Partial/CustomerTransaction", model);
        }
        public ActionResult Transactiondata(Customertransaction model)
        {
            customertransaction customertransaction = new customertransaction();
            CustomertransactionManager customertransactionManager = new CustomertransactionManager();
            if (model.Id == 0)
            {

                if (model.Cash != null)
                {
                    var totalamount = model.InvAmount;
                    var money = model.Cash;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.Cash;
                }
                else if (model.PaymentAmount != null)
                {
                    var totalamount = model.InvAmount;
                    var money = model.PaymentAmount;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.PaymentAmount;
                }
                else if (model.PaymentAmount != null)
                {
                    var totalamount = model.InvAmount;
                    var money = model.Debitnotevalue;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.Debitnotevalue;
                }
                customertransaction.CustomerId = model.buyerid;
                customertransaction.PaymentDate = model.PaymentDate;
                customertransaction.PaymentAmount = model.PaymentAmount;
                customertransaction.InvRefNumber = model.InvHDNumber;
                customertransaction.InvDate = model.InvDate;
                customertransaction.InvDueDate = model.InvDueDate;
                customertransaction.InvAmount = model.InvAmount;
                customertransaction.IsCustomerOnHold = model.IsCustomerOnHold;
                customertransaction.PaymentRefNo = model.PaymentRefNo;
                customertransaction.Debitnoteref = model.Debitnoteref;
                customertransaction.Debitnotevalue = model.Debitnotevalue;
                customertransaction.Debitnotedate = model.Debitnotedate;
                customertransaction.PaymentId = model.paymentid;
                customertransaction.Cash = model.Cash;
                customertransaction.CreatedDate = DateTime.Now;
                customertransactionManager.Post(customertransaction);
            }
            else
            {
               
                if (model.Cash != null)
                {
                    var totalamount = model.InvBalanceAmount;
                    var money = model.Cash;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.Cash;
                }
                else if (model.PaymentAmount != null && model.PaymentAmount != 0)
                {
                    var totalamount = model.InvBalanceAmount;
                    var money = model.PaymentAmount;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.PaymentAmount;
                }
                else if (model.Debitnotevalue != null)
                {
                    var totalamount = model.InvBalanceAmount;   
                    var money = model.Debitnotevalue;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.Debitnotevalue;
                }
                var cash = customertransactionManager.GetCTId(model.Id);
                var balance = cash.InvPaidAmount + customertransaction.InvPaidAmount;
                var AlertMessage = "";
                if (cash.InvAmount < balance)
                {
                    AlertMessage = "Payless";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);

                }
                customertransaction.Id = model.Id;
                customertransaction.CustomerId = model.buyerid;
                customertransaction.PaymentDate = model.PaymentDate;
                customertransaction.PaymentAmount = model.PaymentAmount;
                customertransaction.InvRefNumber = model.InvHDNumber;
                customertransaction.InvDate = model.InvDate;
                customertransaction.InvDueDate = model.InvDueDate;
                customertransaction.InvAmount = model.InvAmount;
                customertransaction.IsCustomerOnHold = model.IsCustomerOnHold;
                customertransaction.PaymentRefNo = model.PaymentRefNo;
                customertransaction.Debitnoteref = model.Debitnoteref;
                customertransaction.Debitnotevalue = model.Debitnotevalue;
                customertransaction.Debitnotedate = model.Debitnotedate;
                customertransaction.PaymentId = model.paymentid;
                customertransaction.Cash = model.Cash;
                customertransaction.CreatedDate = DateTime.Now;
                customertransactionManager.Put(customertransaction);
            }

            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CustomerTransactionsearch(int Buyerid, int INvRefNumber)
        {
            Customertransaction model1 = new Customertransaction();

            OrderHeaderManager manager = new OrderHeaderManager();
            var Customertransactionlist = manager.Get();
            BuyerManager BuyerManager = new BuyerManager();
            var data1 = BuyerManager.Get();
            CustomertransactionManager customertransactionmanager = new CustomertransactionManager();
            var data2 = customertransactionmanager.Get();
            paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
            var data3 = paymentmethodmanager.Get();

            List<Customertransaction> totalList = new List<Customertransaction>();

            foreach (var item in Customertransactionlist)
            {
                if (item.CustomerId == Buyerid && item.invoicehd_id == INvRefNumber)
                {
                    Customertransaction customertransaction = new Customertransaction();
                    customertransaction.InvDate = item.invoicedate;
                    customertransaction.InvAmount = item.TotalPrice;
                    customertransaction.InvHDNumber = item.invoicehd_id;
                    customertransaction.RefItems = item.invoice_items;
                    customertransaction.RefQuantity = item.Quantity;
                    customertransaction.BuyerMaster = data1.Where(W => W.BuyerMasterId == item.CustomerId).ToList().FirstOrDefault();
                    customertransaction.customertransaction = data2.Where(W => W.InvRefNumber == item.invoicehd_id).ToList().FirstOrDefault();

                    totalList.Add(customertransaction);
                }
            }


            return Json(totalList, JsonRequestBehavior.AllowGet);
        }
    }
}
