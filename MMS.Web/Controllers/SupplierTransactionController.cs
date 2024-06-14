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
        public ActionResult SupplierTransactionGrid(int page = 1, int pageSize = 8)
        {
            SupplierTransaction model = new SupplierTransaction();

            GrnManagerNew manager = new GrnManagerNew();
            var suppliertransactionlist = manager.Get();

            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            var data1=supplierMasterManager.Get();

            SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();
            var data2=supplierTransactionManager.Get();

            paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
            var data3 = paymentmethodmanager.Get();

            List<SupplierTransaction> totalList = new List<SupplierTransaction>();


            foreach (var item in suppliertransactionlist)
            {
                SupplierTransaction supplierTransaction = new SupplierTransaction();

                supplierTransaction.GrnDate = item.GrnDate;
                supplierTransaction.GrnAmount = item.TotalCost;
                supplierTransaction.GrnRefNumber = item.GrnNo;
                supplierTransaction.Grnqty = item.QtyAsPerDc;
                supplierTransaction.Id = item.GrnNo;
                supplierTransaction.SupplierMaster = data1.Where(W => W.SupplierMasterId == item.SupplierNameId).ToList().FirstOrDefault();
                supplierTransaction.suppliertransaction = data2.Where(W => W.GrnRefNumber == item.GrnNo).ToList().FirstOrDefault();


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

            GrnManagerNew manager = new GrnManagerNew();
            SupplierMasterManager suppliermastermanager = new SupplierMasterManager();
            AccounttypeManager accounttypeManager = new AccounttypeManager();
            SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();

            var data = manager.Get(id);
            var supid = data.SupplierNameId;
            var data1 = suppliermastermanager.GetSupplierMasterId(supid);
            var supplier = data.SupplierNameId;
            var acctype = data1.accounttypeid;
            var daylist = accounttypeManager.GettypeId(acctype);
            var balance= supplierTransactionManager.GettypeId(id);
            var Termdays = daylist.TermDays;

            model.GrnRefNumber = data.GrnNo;
            model.Supplier = data1.SupplierName;
            model.SupplierCode = data1.SupplierCode;
            model.GrnDate = data.GrnDate;
            model.Grnqty = data.QtyAsPerDc;
            model.GrnAmount = data.TotalCost;
            model.PoDate = data.PoDate;
            model.PoNo = data.PoNO;
            model.GrnDueDate = data.GrnDueDate;
            model.SupplierId= supid;

            if (balance != null)
            {
                model.GrnBalanceAmount = balance.GrnBalanceAmount;
                model.GrnPaidAmount = balance.GrnPaidAmount;
                model.Id = balance.Id;
            }
            else
            {
                model.GrnBalanceAmount = data.TotalCost;

            }

            var PaymentDates = data.GrnDate;
            string paymentDatesString = PaymentDates; // Assuming PaymentDates is the string representation of the date
            DateTime startDate = DateTime.Parse(paymentDatesString);
            model.PaymentDate = startDate.AddDays(Termdays);
            return PartialView("SupplierTransaction", model);
        }

       
        [HttpPost]
        public ActionResult SupplierTransactionDataInsert(SupplierTransaction model)
        {

            if (model.Id == 0)
            {
                supplierTransaction supplierTransactionEntity = new supplierTransaction();

                SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();

                SupplierMasterManager supplierMasterManager = new SupplierMasterManager();

                if (model.Cash != null)
                {
                    supplierTransactionEntity.GrnBalanceAmount = model.GrnAmount - model.Cash;
                    supplierTransactionEntity.GrnPaidAmount = model.Cash;
                }
                else if (model.PaymentAmount != null && model.PaymentAmount != 0)
                {
                    supplierTransactionEntity.GrnBalanceAmount = model.GrnAmount - model.PaymentAmount;
                    supplierTransactionEntity.GrnPaidAmount = model.PaymentAmount;
                }
                else if (model.CreditNoteValue != null)
                {
                    supplierTransactionEntity.GrnBalanceAmount = model.GrnAmount - model.CreditNoteValue;
                    supplierTransactionEntity.GrnPaidAmount = model.CreditNoteValue;
                }

                supplierTransactionEntity.SupplierId = model.SupplierId;
                supplierTransactionEntity.PaymentDate = model.PaymentDate;
                supplierTransactionEntity.PaymentAmount = model.PaymentAmount;
                supplierTransactionEntity.GrnRefNumber = model.GrnRefNumber;
                supplierTransactionEntity.GrnDate = model.GrnDate;
                supplierTransactionEntity.GrnDueDate = model.GrnDueDate;
                supplierTransactionEntity.GrnAmount = model.GrnAmount;
                //supplierTransactionEntity.GrnPaidAmount = model.GrnPaidAmount;
                //supplierTransactionEntity.GrnBalanceAmount = model.GrnBalanceAmount;
                supplierTransactionEntity.IsTransactionOnHold = model.IsTransactionOnHold;
                supplierTransactionEntity.PaymentRefNo = model.PaymentRefNo;
                supplierTransactionEntity.CreditNoteRef = model.CreditNoteRef;
                supplierTransactionEntity.CreditNoteValue = model.CreditNoteValue;
                supplierTransactionEntity.CreditNoteDate = model.CreditNoteDate;
                supplierTransactionEntity.CreatedDate = DateTime.Now;
                //supplierTransactionEntity.UpdatedDate = DateTime.Now;
                //supplierTransactionEntity.CreatedBy = model.CreatedBy;
                //supplierTransactionEntity.UpdatedBy = model.UpdatedBy;
                supplierTransactionEntity.paymentid = model.paymentid;
                supplierTransactionEntity.Cash = model.Cash;

                DateTime grnDate = DateTime.ParseExact(model.GrnDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string formattedGrnDate = grnDate.ToString("yyyy-MM-dd");
                supplierTransactionEntity.GrnDate = formattedGrnDate;

                supplierTransactionManager.Post(supplierTransactionEntity);

            }

            else
            {
                supplierTransaction supplierTransactionEntity = new supplierTransaction();

                SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();

                SupplierMasterManager supplierMasterManager = new SupplierMasterManager();

                if (model.Cash != null)
                {
                    supplierTransactionEntity.GrnBalanceAmount = model.GrnBalanceAmount - model.Cash;
                    supplierTransactionEntity.GrnPaidAmount = model.Cash;
                }
                else if (model.PaymentAmount != null && model.PaymentAmount != null)
                {
                    var totalamount = model.GrnBalanceAmount;
                    var money = model.PaymentAmount;
                    supplierTransactionEntity.GrnBalanceAmount = totalamount - money;
          
                    supplierTransactionEntity.GrnPaidAmount = model.PaymentAmount;
                }
                else if (model.CreditNoteValue != null)
                {
                    supplierTransactionEntity.GrnBalanceAmount = model.GrnBalanceAmount - model.CreditNoteValue;
                    supplierTransactionEntity.GrnPaidAmount = model.CreditNoteValue;
                }
                supplierTransactionEntity.Id= model.Id;
                supplierTransactionEntity.SupplierId = model.SupplierId;
                supplierTransactionEntity.PaymentDate = model.PaymentDate;
                supplierTransactionEntity.PaymentAmount = model.PaymentAmount;
                supplierTransactionEntity.GrnRefNumber = model.GrnRefNumber;
                supplierTransactionEntity.GrnDate = model.GrnDate;
                supplierTransactionEntity.GrnDueDate = model.GrnDueDate;
                supplierTransactionEntity.GrnAmount = model.GrnAmount;
                //supplierTransactionEntity.GrnPaidAmount = model.GrnPaidAmount;
                //supplierTransactionEntity.GrnBalanceAmount = model.GrnBalanceAmount;
                supplierTransactionEntity.IsTransactionOnHold = model.IsTransactionOnHold;
                supplierTransactionEntity.PaymentRefNo = model.PaymentRefNo;
                supplierTransactionEntity.CreditNoteRef = model.CreditNoteRef;
                supplierTransactionEntity.CreditNoteValue = model.CreditNoteValue;
                supplierTransactionEntity.CreditNoteDate = model.CreditNoteDate;
                supplierTransactionEntity.CreatedDate = DateTime.Now;
                //supplierTransactionEntity.UpdatedDate = DateTime.Now;
                //supplierTransactionEntity.CreatedBy = model.CreatedBy;
                //supplierTransactionEntity.UpdatedBy = model.UpdatedBy;
                supplierTransactionEntity.paymentid = model.paymentid;
                supplierTransactionEntity.Cash = model.Cash;

                DateTime grnDate = DateTime.ParseExact(model.GrnDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string formattedGrnDate = grnDate.ToString("yyyy-MM-dd");
                supplierTransactionEntity.GrnDate = formattedGrnDate;

                supplierTransactionManager.Put(supplierTransactionEntity);

            }




            return PartialView("SupplierTransactionGrid");
        }


        [HttpGet]
        public ActionResult SupplierTransactionSearch(string filter)
        {
            try
            {
                SupplierTransaction model = new SupplierTransaction();

                GrnManagerNew manager = new GrnManagerNew();
                var suppliertransactionlist = manager.Get();

                SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
                var data1 = supplierMasterManager.Get();

                SupplierTransactionManager supplierTransactionManager = new SupplierTransactionManager();
                var data2 = supplierTransactionManager.Get();

                paymentmethodmanager paymentmethodmanager = new paymentmethodmanager();
                var data3 = paymentmethodmanager.Get();

                List<SupplierTransaction> totalList = new List<SupplierTransaction>();


                foreach (var item in suppliertransactionlist)
                {
                    SupplierTransaction supplierTransaction = new SupplierTransaction();

                    supplierTransaction.GrnDate = item.GrnDate;
                    supplierTransaction.GrnAmount = item.TotalCost;
                    supplierTransaction.GrnRefNumber = item.GrnNo;
                    supplierTransaction.Grnqty = item.QtyAsPerDc;
                    supplierTransaction.Id = item.GrnNo;
                    supplierTransaction.SupplierMaster = data1.Where(W => W.SupplierMasterId == item.SupplierNameId).ToList().FirstOrDefault();
                    supplierTransaction.suppliertransaction = data2.Where(W => W.GrnRefNumber == item.GrnNo).ToList().FirstOrDefault();


                    totalList.Add(supplierTransaction);

                }
                // Filter productions based on the provided filter parameter
                totalList = totalList.Where(x => x.SupplierMaster.SupplierName.ToLower().Contains(filter.ToLower())).ToList();

                return Json(totalList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}

