using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Repository.Service;
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
            BuyerMasterManager BuyerManager = new BuyerMasterManager();
            var data1 = BuyerManager.Get();
            CustomertransactionManager customertransactionmanager = new CustomertransactionManager();
            var data2 = customertransactionmanager.Get();
            paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
            var data3 = paymentmethodmanager.Get();
            List<Customertransaction> totalList = new List<Customertransaction>();
            foreach (var item in Customertransactionlist)
            {
                decimal? paid = 0;

                Customertransaction customertransaction = new Customertransaction();
                customertransaction.InvDate = item.invoicedate;
                customertransaction.InvAmount = item.GrandTotal;
                customertransaction.InvHDNumber = item.invoicehd_id;
                customertransaction.RefItems = item.invoice_items;
                customertransaction.RefQuantity = item.Quantity;
                customertransaction.BuyerMaster = data1.Where(W => W.BuyerMasterId == item.CustomerId).ToList().FirstOrDefault();
                List<customertransaction> customertransactions = data2.Where(W => W.InvRefNumber == item.invoicehd_id).ToList();
                foreach (var i in customertransactions)
                {
                    paid += i.InvPaidAmount;
                }
                customertransaction.InvPaidAmount = paid;
                customertransaction.InvBalanceAmount = item.GrandTotal - paid;
                totalList.Add(customertransaction);
            }


            var totalCount = totalList.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(ct => ct.InvHDNumber)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;


            return PartialView("Partial/CustomerTransactionGrid", totalList);
        }
        [HttpGet]
        public ActionResult CustomerTransaction(int id)
        {
            Customertransaction model = new Customertransaction();
            SalesorderDT_Manager SalesorderDT_Manager = new SalesorderDT_Manager();
            OrderDetailsManager orderDetailsManager = new OrderDetailsManager();
            OrderHeaderManager OrderHeaderManager = new OrderHeaderManager();
            BuyerMasterManager BuyerManager = new BuyerMasterManager();
            AccounttypeManager accounttypeManager = new AccounttypeManager();
            CustomertransactionManager Customertransactions = new CustomertransactionManager();
            var orderhd = OrderHeaderManager.Get(id);
            var orderDT = orderDetailsManager.Get(orderhd.invoicehd_id);
            var buyer = BuyerManager.GetBuyerMasterId(orderhd.CustomerId);
            var SODT = SalesorderDT_Manager.GetSO(orderDT.SalesOrderId_dt);
            var daylist = accounttypeManager.GettypeId(buyer.AccountTypeId);
            var balance = Customertransactions.GetiNId(id);

            var Termdays = daylist.TermDays;

            model.InvHDNumber = orderhd.invoicehd_id;
            model.customer = buyer.CustomerName;
            model.buyercode = buyer.BuyerCode;
            model.InvDate = orderhd.invoicedate;
            model.RefItems = orderhd.invoice_items;
            model.RefQuantity = orderhd.Quantity;
            model.InvAmount = orderhd.GrandTotal;
            model.Sonumber = SODT.Salesorderid_hd;
            model.InvDueDate = orderhd.invoicedate;
            model.SoDate = SODT.salesorderdate;
            model.buyerid = orderhd.CustomerId;
            decimal? bal = 0;
            if (balance.Count() != 0)
            {
                foreach (var item in balance)
                {
                    bal += item.InvPaidAmount;
                }
                model.InvBalanceAmount = orderhd.GrandTotal - bal;
                model.InvPaidAmount = bal;

            }
            else
            {
                model.InvBalanceAmount = orderhd.GrandTotal;
            }
            var PaymentDate = orderhd.invoicedate;
            DateTime startDate = PaymentDate;
            model.PaymentDate = startDate.AddDays(Termdays);

            return PartialView("Partial/CustomerTransaction", model);
        }
        public ActionResult Transactiondata(Customertransaction model)
        {
            string AlertMessage = "";
            customertransaction customertransaction = new customertransaction();
            CustomertransactionManager customertransactionManager = new CustomertransactionManager();
            var balance = customertransactionManager.GetiNId(model.InvHDNumber);
            decimal? bal = 0;
            if (balance.Count() != 0)
            {
                foreach (var item in balance)
                {
                    bal += item.InvPaidAmount;
                }
            }
            if (bal == model.InvAmount)
            {
                AlertMessage = "Done";
                return Json(AlertMessage, JsonRequestBehavior.AllowGet);
            }


            if (model.Cash != null)
            {
                var totalamount = model.InvAmount;
                var money = model.Cash + bal;
                var balanceamount = totalamount - money;
                customertransaction.InvBalanceAmount = balanceamount;
                customertransaction.InvPaidAmount = model.Cash;
                if (money > totalamount)
                {
                    AlertMessage = "Incorrect";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else if (model.PaymentAmount != null)
            {
                var totalamount = model.InvAmount;
                var money = model.PaymentAmount + bal;
                var balanceamount = totalamount - money;
                customertransaction.InvBalanceAmount = balanceamount;
                customertransaction.InvPaidAmount = model.PaymentAmount;
                if (money > totalamount)
                {
                    AlertMessage = "Incorrect";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else if (model.Debitnotevalue != null)
            {
                var totalamount = model.InvAmount;
                var money = model.Debitnotevalue + bal;
                var balanceamount = totalamount - money;
                customertransaction.InvBalanceAmount = balanceamount;
                customertransaction.InvPaidAmount = model.Debitnotevalue;
                if (money > totalamount)
                {
                    AlertMessage = "Incorrect";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
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
            AlertMessage = "success";
            return Json(AlertMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult CustomerTransactionsearch(int Buyerid, int INvRefNumber)
        {
            Customertransaction model1 = new Customertransaction();

            OrderHeaderManager manager = new OrderHeaderManager();
            var Customertransactionlist = manager.Get().OrderByDescending(m => m.invoicehd_id);
            BuyerMasterManager BuyerManager = new BuyerMasterManager();
            var data1 = BuyerManager.Get();
            CustomertransactionManager customertransactionmanager = new CustomertransactionManager();
            var data2 = customertransactionmanager.Get();
            paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
            var data3 = paymentmethodmanager.Get();

            List<Customertransaction> totalList = new List<Customertransaction>();

            foreach (var item in Customertransactionlist)
            {
                    decimal? paid = 0;
                    Customertransaction customertransaction = new Customertransaction();
                    customertransaction.InvDate = item.invoicedate;
                    customertransaction.InvAmount = item.GrandTotal;
                    customertransaction.InvHDNumber = item.invoicehd_id;
                    customertransaction.RefItems = item.invoice_items;
                    customertransaction.RefQuantity = item.Quantity;
                    customertransaction.buyerid = item.CustomerId;
                    customertransaction.BuyerMaster = data1.Where(W => W.BuyerMasterId == item.CustomerId).ToList().FirstOrDefault();
                    List<customertransaction> customertransactions = data2.Where(W => W.InvRefNumber == item.invoicehd_id).ToList();
                    foreach (var i in customertransactions)
                    {
                        paid += i.InvPaidAmount;
                    }
                    customertransaction.InvPaidAmount = paid;
                    customertransaction.InvBalanceAmount = item.GrandTotal - paid;
                    totalList.Add(customertransaction);
            }
            var roundtoatlist = (Buyerid == 0 && INvRefNumber == 0)
         ? totalList
         : totalList.Where(m => m.buyerid == Buyerid && m.InvHDNumber == INvRefNumber);

            return Json(roundtoatlist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Getbuyerno(int id)
        {
            OrderHeaderManager OrderHeaderManager = new OrderHeaderManager();
            var data = OrderHeaderManager.GettypeId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
