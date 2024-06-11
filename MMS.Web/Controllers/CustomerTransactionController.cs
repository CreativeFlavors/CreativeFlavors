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
        public ActionResult CustomerTransactionGrid(int page = 1, int pageSize = 8)
        {

            Customertransaction model = new Customertransaction();

            InvoiceManager manager = new InvoiceManager();
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
                customertransaction.InvDate = item.DocDate;
                customertransaction.InvAmount = item.DocNetValue;
                customertransaction.InvRefNumber = item.Id;
                customertransaction.RefItems = item.RefItems;
                customertransaction.RefQuantity = item.DocQuantity;
                customertransaction.Id = item.Id;
                customertransaction.BuyerMaster = data1.Where(W => W.BuyerMasterId == item.CustomerId).ToList().FirstOrDefault();
                customertransaction.customertransaction = data2.Where(W => W.InvRefNumber == item.Id).ToList().FirstOrDefault();
                totalList.Add(customertransaction);

            }
       

            var totalCount = totalList.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(ct => ct.Id)
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
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            InvoiceManager manager = new InvoiceManager();
            BuyerManager BuyerManager = new BuyerManager();
            AccounttypeManager accounttypeManager = new AccounttypeManager();
            CustomertransactionManager Customertransactions = new CustomertransactionManager();
            var data = manager.Get(id);
            var buyid = data.CustomerId;
            var buyersino = data.OrderId;
            var Cust = data.CustomerId;
            var data1 = BuyerManager.GetBuyerMasterId(buyid);
            var acctype = data1.accountypeid;
            var data2 = buyerOrderEntryManager.GetBuyersino(buyersino);
            var daylist = accounttypeManager.GettypeId(acctype);
            var balance = Customertransactions.GettypeId(id);

            var Termdays = daylist.TermDays;

            model.InvRefNumber = data.Id;
            model.customer = data1.BuyerFullName;
            model.buyercode = data1.BuyerCode;
            model.InvDate = data.DocDate;
            model.RefItems = data.RefItems;
            model.RefQuantity = data.DocQuantity;
            model.InvAmount = data.DocNetValue;
            model.Sonumber = data2.BuyerOrderSlNo;
            model.InvDueDate = data.DueDate;
            model.SoDate = data2.CreatedDate;
            model.buyerid = data.CustomerId;
            if (balance != null)
            {
                model.InvBalanceAmount = balance.InvBalanceAmount;
                model.InvPaidAmount = balance.InvPaidAmount;
                model.Id = balance.Id;
            }
            else
            {
                model.InvBalanceAmount = data.DocNetValue;

            }

            var PaymentDate = data.DocDate;
            DateTime startDate = PaymentDate;
            model.PaymentDate = startDate.AddDays(Termdays);



            return PartialView("Partial/CustomerTransaction", model);
        }
        public ActionResult Transactiondata(Customertransaction model)
        {
            if (model.Id == 0)
            {
                customertransaction customertransaction = new customertransaction();
                CustomertransactionManager customertransactionManager = new CustomertransactionManager();

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
                    var money = model.CreditNoteValue;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.CreditNoteValue;
                }
                customertransaction.CustomerId = model.buyerid;
                customertransaction.PaymentDate = model.PaymentDate;
                customertransaction.PaymentAmount = model.PaymentAmount;
                customertransaction.InvRefNumber = model.InvRefNumber;
                customertransaction.InvDate = model.InvDate;
                customertransaction.InvDueDate = model.InvDueDate;
                customertransaction.InvAmount = model.InvAmount;
                customertransaction.IsCustomerOnHold = model.IsCustomerOnHold;
                customertransaction.PaymentRefNo = model.PaymentRefNo;
                customertransaction.CreditNoteRef = model.CreditNoteRef;
                customertransaction.CreditNoteValue = model.CreditNoteValue;
                customertransaction.CreditNoteDate = model.CreditNoteDate;
                customertransaction.PaymentId = model.paymentid;
                customertransaction.Cash = model.Cash;
                customertransaction.CreatedDate = DateTime.Now;
                customertransactionManager.Post(customertransaction);
            }
            else
            {
                customertransaction customertransaction = new customertransaction();
                CustomertransactionManager customertransactionManager = new CustomertransactionManager();

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
                else if (model.CreditNoteValue != null)
                {
                    var totalamount = model.InvBalanceAmount;
                    var money = model.CreditNoteValue;
                    var balanceamount = totalamount - money;
                    customertransaction.InvBalanceAmount = balanceamount;
                    customertransaction.InvPaidAmount = model.CreditNoteValue;
                }
                customertransaction.Id = model.Id;
                customertransaction.CustomerId = model.buyerid;
                customertransaction.PaymentDate = model.PaymentDate;
                customertransaction.PaymentAmount = model.PaymentAmount;
                customertransaction.InvRefNumber = model.InvRefNumber;
                customertransaction.InvDate = model.InvDate;
                customertransaction.InvDueDate = model.InvDueDate;
                customertransaction.InvAmount = model.InvAmount;
                customertransaction.IsCustomerOnHold = model.IsCustomerOnHold;
                customertransaction.PaymentRefNo = model.PaymentRefNo;
                customertransaction.CreditNoteRef = model.CreditNoteRef;
                customertransaction.CreditNoteValue = model.CreditNoteValue;
                customertransaction.CreditNoteDate = model.CreditNoteDate;
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

            InvoiceManager manager = new InvoiceManager();
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
                if (item.CustomerId == Buyerid && item.Id == INvRefNumber)
                {
                    Customertransaction customertransaction = new Customertransaction();
                    customertransaction.InvDate = item.DocDate;
                    customertransaction.InvAmount = item.RefGrossValue;
                    customertransaction.InvRefNumber = item.Id;
                    customertransaction.RefItems = item.RefItems;
                    customertransaction.RefQuantity = item.RefQuantity;
                    customertransaction.Id = item.Id;
                    customertransaction.BuyerMaster = data1.Where(W => W.BuyerMasterId == item.CustomerId).ToList().FirstOrDefault();
                    customertransaction.customertransaction = data2.Where(W => W.InvRefNumber == item.Id).ToList().FirstOrDefault();

                    totalList.Add(customertransaction);
                }
            }


            return Json(totalList, JsonRequestBehavior.AllowGet);
        }
    }
}
