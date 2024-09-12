using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Repository.Service;
using MMS.Web.Models.CustomerTransaction;
using MMS.Web.Models.Product;
using MMS.Web.Models.Production;
using MMS.Web.Models.StockModel;
using MMS.Web.Models.SupplierTransaction;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class SupplierTransactionController : Controller
    {
        [HttpGet]
        public ActionResult SupplierTransactionGrid(int page = 1, int pageSize = 15)
        {
            SupplierTransaction model = new SupplierTransaction();

            GRNHeaderManager manager = new GRNHeaderManager();
            var suppliertransactionlist = manager.Get();

            Supplier_masterManager supplierMasterManager = new Supplier_masterManager();
            var data1 = supplierMasterManager.Get();

            SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();
            var data2 = supplierTransactionManager.Get();

            paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
            var data3 = paymentmethodmanager.Get();

            List<SupplierTransaction> totalList = new List<SupplierTransaction>();

            foreach (var item in suppliertransactionlist)
            {
                SupplierTransaction supplierTransaction = new SupplierTransaction();
                decimal? paid = 0;
                supplierTransaction.GrnDate = item.GrnDate;
                supplierTransaction.GrnAmount = item.total_unitprice;
                supplierTransaction.GrnRefNumber = item.GRNNumber;
                supplierTransaction.Grnqty = item.Quantity;
                supplierTransaction.Id = item.GrnHeaderId;
                supplierTransaction.SupplierMaster = data1.Where(W => W.SupplierId == item.SupplierId).ToList().FirstOrDefault();
                List<supplierTransaction> suppliertransactions = data2.Where(W => W.GrnRefNumber == item.GRNNumber).ToList();
                foreach (var i in suppliertransactions)
                {
                    paid += i.GrnPaidAmount;
                }
                supplierTransaction.GrnPaidAmount = paid;
                supplierTransaction.GrnBalanceAmount = item.total_unitprice - paid;
                totalList.Add(supplierTransaction);

            }
            var totalCount = totalList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList();
            int startSerialNumber = startIndex + 1;
            int endSerialNumber = startSerialNumber + totalList.Count - 1;
            ViewBag.TotalList = totalList;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return View(totalList);
        }

        [HttpGet]
        public ActionResult SupplierTransaction(int id)
        {
            SupplierTransaction model = new SupplierTransaction();

            GRNHeaderManager manager = new GRNHeaderManager();
            Supplier_masterManager suppliermastermanager = new Supplier_masterManager();
            AccounttypeManager accounttypeManager = new AccounttypeManager();
            SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();

            var data = manager.Get(id);
            var supid = data.SupplierId;
            var data1 = suppliermastermanager.getsupplierId(supid);
            var supplier = data.SupplierId;
            var acctype = data1.AccountTypeId;
            var daylist = accounttypeManager.GettypeId(acctype);
            var balance = supplierTransactionManager.GettypeId(id);
            var Termdays = daylist.TermDays;

            model.GrnRefNumber = data.GRNNumber;
            model.Supplier = data1.Suppliername;
            model.SupplierCode = data1.suppliercode;
            model.GrnDate = data.GrnDate;
            model.Grnqty = data.Quantity;
            model.GrnAmount = data.total_unitprice;
            model.PoDate = data.PoDate ?? DateTime.MinValue;
            model.PoNo = data.PoNumber;
            model.GrnDueDate = data.GrnDate;
            model.SupplierId = supid;
            decimal? bal = 0;
            if (balance.Count() != 0)
            {
                foreach (var item in balance)
                {
                    bal += item.GrnPaidAmount;
                }
                model.GrnBalanceAmount = data.total_unitprice - bal;
                model.GrnPaidAmount = bal;

            }
            else
            {
                model.GrnBalanceAmount = data.total_unitprice;

            }

            var PaymentDates = data.GrnDate;
            DateTime startDate = (PaymentDates);
            model.PaymentDate = startDate.AddDays(Termdays);
            return PartialView("SupplierTransaction", model);
        }


        [HttpPost]
        public ActionResult SupplierTransactionDataInsert(SupplierTransaction model)
        {
            string AlertMessage = "";

            supplierTransaction supplierTransactionEntity = new supplierTransaction();

            SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();

            Supplier_masterManager supplierMasterManager = new Supplier_masterManager();
            var balance = supplierTransactionManager.GettypeId(model.GrnRefNumber);
            decimal? bal = 0;
            if (balance.Count() != 0)
            {
                foreach (var item in balance)
                {
                    bal += item.GrnPaidAmount;
                }
            }
            if (bal == model.GrnAmount)
            {
                AlertMessage = "Done";
                return Json(AlertMessage, JsonRequestBehavior.AllowGet);
            }

            if (model.Cash != null)
            {
                var money = model.Cash + bal;
                var totalamount = model.GrnAmount;
                var balanceamount = totalamount - money;
                supplierTransactionEntity.GrnBalanceAmount = balanceamount;
                supplierTransactionEntity.GrnPaidAmount = model.Cash;
                if (money > totalamount)
                {
                    AlertMessage = "Incorrect";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else if (model.PaymentAmount != null && model.PaymentAmount != 0)
            {
                var money = model.PaymentAmount + bal;
                var totalamount = model.GrnAmount;
                var balanceamount = totalamount - money;
                supplierTransactionEntity.GrnBalanceAmount = balanceamount;
                supplierTransactionEntity.GrnPaidAmount = model.PaymentAmount;
                if (money > totalamount)
                {
                    AlertMessage = "Incorrect";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
            }
            else if (model.CreditNoteValue != null)
            {
                var money = model.CreditNoteValue + bal;
                var totalamount = model.GrnAmount;
                var balanceamount = totalamount - money;
                supplierTransactionEntity.GrnBalanceAmount = balanceamount;
                supplierTransactionEntity.GrnPaidAmount = model.CreditNoteValue;
                if (money > totalamount)
                {
                    AlertMessage = "Incorrect";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
            }
            supplierTransactionEntity.SupplierId = model.SupplierId;
            supplierTransactionEntity.PaymentDate = model.PaymentDate;
            supplierTransactionEntity.PaymentAmount = model.PaymentAmount;
            supplierTransactionEntity.GrnRefNumber = model.GrnRefNumber;
            supplierTransactionEntity.GrnDate = model.GrnDate;
            supplierTransactionEntity.GrnDueDate = model.GrnDueDate;
            supplierTransactionEntity.GrnAmount = model.GrnAmount;
            supplierTransactionEntity.IsTransactionOnHold = model.IsTransactionOnHold;
            supplierTransactionEntity.PaymentRefNo = model.PaymentRefNo;
            supplierTransactionEntity.CreditNoteRef = model.CreditNoteRefNo;
            supplierTransactionEntity.CreditNoteValue = model.CreditNoteValue;
            supplierTransactionEntity.CreditNoteDate = model.CreditNoteDate;
            supplierTransactionEntity.CreatedDate = DateTime.Now;
            supplierTransactionEntity.paymentid = model.paymentid;
            supplierTransactionEntity.Cash = model.Cash;
            supplierTransactionEntity.GrnDate = model.GrnDate;
            supplierTransactionManager.Post(supplierTransactionEntity);
            AlertMessage = "success";
            return Json(AlertMessage, JsonRequestBehavior.AllowGet);
        }
        #region filterion

        [HttpGet]
        public ActionResult SupplierTransactionSearch(int Supplierid)
        {
            try
            {
                SupplierTransaction model = new SupplierTransaction();

                GRNHeaderManager manager = new GRNHeaderManager();
                var suppliertransactionlist = manager.Get();

                Supplier_masterManager supplierMasterManager = new Supplier_masterManager();
                var data1 = supplierMasterManager.Get();

                SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();
                var data2 = supplierTransactionManager.Get();

                paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
                var data3 = paymentmethodmanager.Get();

                List<SupplierTransaction> totalList = new List<SupplierTransaction>();


                foreach (var item in suppliertransactionlist)
                {
                    if (item.SupplierId == Supplierid)
                    {
                        SupplierTransaction supplierTransaction = new SupplierTransaction();
                        decimal? paid = 0;
                        supplierTransaction.GrnDate = item.GrnDate;
                        supplierTransaction.GrnAmount = item.total_unitprice;
                        supplierTransaction.GrnRefNumber = item.GRNNumber;
                        supplierTransaction.Grnqty = item.Quantity;
                        supplierTransaction.Id = item.GrnHeaderId;
                        supplierTransaction.SupplierMaster = data1.Where(W => W.SupplierId == item.SupplierId).ToList().FirstOrDefault();
                        List<supplierTransaction> suppliertransactions = data2.Where(W => W.GrnRefNumber == item.GRNNumber).ToList();
                        foreach (var i in suppliertransactions)
                        {
                            paid += i.GrnPaidAmount;
                        }
                        supplierTransaction.GrnPaidAmount = paid;
                        supplierTransaction.GrnBalanceAmount = item.total_unitprice - paid;
                        totalList.Add(supplierTransaction);
                    }
                }
                return Json(totalList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Getsupplierno(int id)
        {
            GRNHeaderManager GRNHeaderManager = new GRNHeaderManager();
            var data = GRNHeaderManager.GetsupplierId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}

