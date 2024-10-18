using Microsoft.Ajax.Utilities;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SupplierMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MMS.Web.Controllers.Report.GrnGstReportController;

namespace MMS.Web.Controllers
{
    public class SupplierMasterController : Controller
    {
        [HttpGet]
        public ActionResult SupplierMaster()
        {
            SupplierModel model = new SupplierModel();
            return View(model);
        }
        [HttpGet]
        public ActionResult SupplierMasterDetails()
        {
            SupplierModel supplierMasterModel1 = new SupplierModel();
            return View("Partial/SupplierMasterDetails", supplierMasterModel1);
        }
        [HttpGet]
        public ActionResult SupplierMasterGrid(int page = 1, int pageSize = 15)
        {
            Supplier_masterManager supplier_MasterManager = new Supplier_masterManager();
            var supplierlist = supplier_MasterManager.Get();
            List<SupplierModel> totalist = new List<SupplierModel>();
            foreach (var item in supplierlist)
            {
                SupplierModel supplierModel = new SupplierModel();
                supplierModel.Suppliername = item.Suppliername;
                supplierModel.suppliercode = item.suppliercode;
                supplierModel.Telephone1 = item.Telephone1;
                supplierModel.EmailContact = item.EmailContact;
                supplierModel.AccountName = item.AccountName;
                supplierModel.PhysicalCode = item.PhysicalCode;
                supplierModel.SupplierId = item.SupplierId;
                totalist.Add(supplierModel);
            }
            var totalCount = totalist.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalist = totalist.OrderByDescending(s => s.SupplierId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return PartialView("/Views/SupplierMaster/Partial/SupplierMasterGrid.cshtml", totalist);
        }

        [HttpPost]
        public ActionResult SupplierMaster(SupplierModel model)
        {
            string AlertMessage = "";
            Supplier_masterManager _supplyMasterManager = new Supplier_masterManager();
            Supplier_master supplierMaster1 = new Supplier_master();
            if (model.SupplierId == 0)
            {
                var totallist = _supplyMasterManager.Get();
                var suppliercode = totallist.Where(x => x.suppliercode.ToLower().Contains(model.suppliercode.ToLower())).ToList();
                if (suppliercode.Count() != 0)
                {
                    AlertMessage = "Suppliercode";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
                supplierMaster1.Suppliername = model.Suppliername;
                supplierMaster1.suppliercode = model.suppliercode;
                supplierMaster1.Account = model.Account;
                supplierMaster1.AccountName = model.AccountName;
                supplierMaster1.AccountDescription = model.AccountDescription;
                supplierMaster1.Physical1 = model.Physical1;
                supplierMaster1.PhysicalCode = model.PhysicalCode;
                supplierMaster1.Telephone1 = model.Telephone1;
                supplierMaster1.Telephone2 = model.Telephone2;
                supplierMaster1.EmailContact = model.EmailContact;
                supplierMaster1.EmailAccounts = model.EmailAccounts;
                supplierMaster1.EmailEmergency = model.EmailEmergency;
                supplierMaster1.AccountTypeId = model.AccountTypeId;
                supplierMaster1.VatNumber = model.VatNumber;
                supplierMaster1.RegNumber = model.RegNumber;
                supplierMaster1.CreditLimit = model.CreditLimit;
                supplierMaster1.DcBalance = model.DcBalance;
                supplierMaster1.Interest = model.Interest;
                supplierMaster1.TaxTypeId = model.TaxTypeId;
                supplierMaster1.ForeignCurrency = model.ForeignCurrency;
                supplierMaster1.CurrencyID = model.CurrencyID;
                AlertMessage = "Saved";
                var result = _supplyMasterManager.Post(supplierMaster1);
            }
            else
            {
                var totallist = _supplyMasterManager.Get();
                var suppliercode = totallist.Where(x => x.suppliercode.ToLower().Contains(model.suppliercode.ToLower())).ToList();
                if (suppliercode.Count() >=2)
                {
                    AlertMessage = "Suppliercode";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
                supplierMaster1.Suppliername = model.Suppliername;
                supplierMaster1.suppliercode = model.suppliercode;
                supplierMaster1.Account = model.Account;
                supplierMaster1.AccountName = model.AccountName;
                supplierMaster1.AccountDescription = model.AccountDescription;
                supplierMaster1.Physical1 = model.Physical1;
                supplierMaster1.DcBalance = model.DcBalance;
                supplierMaster1.PhysicalCode = model.PhysicalCode;
                supplierMaster1.Telephone1 = model.Telephone1;
                supplierMaster1.Telephone2 = model.Telephone2;
                supplierMaster1.EmailContact = model.EmailContact;
                supplierMaster1.EmailAccounts = model.EmailAccounts;
                supplierMaster1.EmailEmergency = model.EmailEmergency;
                supplierMaster1.AccountTypeId = model.AccountTypeId;
                supplierMaster1.VatNumber = model.VatNumber;
                supplierMaster1.RegNumber = model.RegNumber;
                supplierMaster1.CreditLimit = model.CreditLimit;
                supplierMaster1.Interest = model.Interest;
                supplierMaster1.SupplierId = model.SupplierId;
                supplierMaster1.TaxTypeId = model.TaxTypeId;
                supplierMaster1.ForeignCurrency = model.ForeignCurrency;
                supplierMaster1.CurrencyID = model.CurrencyID;
                AlertMessage = "Updated";
                var results = _supplyMasterManager.Put(supplierMaster1);
            }
                return Json(AlertMessage, JsonRequestBehavior.AllowGet);
            }
            [HttpPost]
            public ActionResult GetSupplierById(int id)
            {
                Supplier_masterManager Supplier_masterManager = new Supplier_masterManager();
                SupplierModel supplierMasterModel1 = new SupplierModel();

                var supplierman_value = Supplier_masterManager.getsupplierId(id);

                if (supplierman_value != null)
                {
                    supplierMasterModel1.SupplierId = supplierman_value.SupplierId;
                    supplierMasterModel1.suppliercode = supplierman_value.suppliercode;
                    supplierMasterModel1.Suppliername = supplierman_value.Suppliername;
                    supplierMasterModel1.Account = supplierman_value.Account;
                    supplierMasterModel1.AccountName = supplierman_value.AccountName;
                    supplierMasterModel1.AccountDescription = supplierman_value.AccountDescription;
                    supplierMasterModel1.Physical1 = supplierman_value.Physical1;
                    supplierMasterModel1.PhysicalCode = supplierman_value.PhysicalCode;
                    supplierMasterModel1.DcBalance = supplierman_value.DcBalance;
                    supplierMasterModel1.Telephone1 = supplierman_value.Telephone1;
                    supplierMasterModel1.Telephone2 = supplierman_value.Telephone2;
                    supplierMasterModel1.EmailContact = supplierman_value.EmailContact;
                    supplierMasterModel1.EmailAccounts = supplierman_value.EmailAccounts;
                    supplierMasterModel1.EmailEmergency = supplierman_value.EmailEmergency;
                    supplierMasterModel1.AccountTypeId = supplierman_value.AccountTypeId;
                    supplierMasterModel1.VatNumber = supplierman_value.VatNumber;
                    supplierMasterModel1.RegNumber = supplierman_value.RegNumber;
                    supplierMasterModel1.CreditLimit = supplierman_value.CreditLimit;
                    supplierMasterModel1.Interest = supplierman_value.Interest;
                    supplierMasterModel1.TaxTypeId = supplierman_value.TaxTypeId;
                    supplierMasterModel1.ForeignCurrency = supplierman_value.ForeignCurrency;
                    supplierMasterModel1.CurrencyID = supplierman_value.CurrencyID;
                }
                return PartialView("Partial/SupplierMasterDetails", supplierMasterModel1);

            }
        //delete the employee
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Supplier_masterManager _supplyMasterManager = new Supplier_masterManager();
                string status = "";
                var result = _supplyMasterManager.getsupplierId(id);
                if (id != 0)
                {
                    status = "Success";
                    _supplyMasterManager.Delete(id);
                }
                return Json(status, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
            public ActionResult Search(string filter)
            {
                List<SupplierModel> supplierMasterModels = new List<SupplierModel>();
                Supplier_masterManager _supplyMasterManager = new Supplier_masterManager();

                var supplierlist = _supplyMasterManager.Get();
                var filteredsupplierlist = supplierlist.Where(x => x.Suppliername.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();

                foreach (var item in filteredsupplierlist)
                {
                    SupplierModel supplierMasterModel1 = new SupplierModel();
                    supplierMasterModel1.SupplierId = item.SupplierId;
                    supplierMasterModel1.Suppliername = item.Suppliername;
                    supplierMasterModel1.suppliercode = item.suppliercode;
                supplierMasterModel1.PhysicalCode = item.PhysicalCode;
                  supplierMasterModel1.AccountName = item.AccountName;
                    supplierMasterModel1.Telephone1 = item.Telephone1;
                    supplierMasterModel1.EmailContact = item.EmailContact;
                    supplierMasterModels.Add(supplierMasterModel1);
                }

                return Json(supplierMasterModels, JsonRequestBehavior.AllowGet);
            }

        }
    }
