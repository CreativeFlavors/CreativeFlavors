using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Web.Models.BuyerMaserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{

    public class BuyerMaterController : Controller
    {

       // #region Accounts View

      
        public ActionResult BuyerMater()
        {
            BuyerMasterModel vm = new BuyerMasterModel();
            return View(vm);
        }

        [HttpGet]
        public ActionResult BuyerMasteDetails()
        {
            BuyerMasterModel vm = new BuyerMasterModel();
            return View("Partial/BuyerMasteDetails", vm);
        }

        //get data using sp
        [HttpGet]
        public ActionResult BuyerMasterGird(int? page, int pageSize = 15)
        {
            BuyerMasterManager buyerMasterManager = new BuyerMasterManager();
            var buyers = buyerMasterManager.GetBuyerModels();

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

            if (!page.HasValue)
            {
                page = 1;
            }
            var totalCount = totaldata.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (int)((page - 1) * pageSize);
            //int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            int endIndex = startIndex + pageSize;


            totaldata = totaldata.OrderByDescending(b => b.BuyerMasterId)
                                   .Skip(startIndex)
                                   .Take(endIndex)
                                    .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;


            return PartialView("Partial/BuyerMasterGird", totaldata);

        }


        //post the data 
        [HttpPost]
        public ActionResult BuyerModel(BuyerMasterModel model)
        {
            BuyerMasterManager _buyerMasterManager = new BuyerMasterManager();

            List<BuyerModel_SP> existingbuyers = new List<BuyerModel_SP>();           

             existingbuyers = _buyerMasterManager.GetBuyerModels();

            if(existingbuyers != null)
            {
                for (int i = 0; i < existingbuyers.Count; i++)
                {
                    if (existingbuyers[i].BuyerCode.ToLower() == model.BuyerCode.ToLower() && existingbuyers[i].CustomerName.ToLower() == model.CustomerName.ToLower())
                    {
                        return Json(new { message = "already exist" });
                    }
                }
            }
            BuyerModel_SP buyerMaster = new BuyerModel_SP();


            buyerMaster.CustomerName = model.CustomerName;           
            buyerMaster.Account = model.Account;
            buyerMaster.AccountName = model.AccountName;
            buyerMaster.AccountDescription = model.AccountDescription;
            buyerMaster.SwiftCode = model.SwiftCode;
            buyerMaster.Physical1 = model.Physical1;         
            buyerMaster.PhysicalCode = model.PhysicalCode;           
            buyerMaster.CurrencyId = model.CurrencyId;
            buyerMaster.Telephone1 = model.Telephone1;
            buyerMaster.Telephone2 = model.Telephone2;
            buyerMaster.EmailContact = model.EmailContact;
            buyerMaster.EmailAccounts = model.EmailAccounts;
            buyerMaster.EmailEmergency = model.EmailEmergency;
            buyerMaster.AccountTypeId = model.AccountTypeId;
            buyerMaster.VatNumber = model.VatNumber;
            buyerMaster.RegNumber = model.RegNumber;
            buyerMaster.CreditLimit = model.CreditLimit;
            buyerMaster.ChargeInterest = true;
            buyerMaster.Interest = model.Interest;       
            buyerMaster.TaxTypeId = model.TaxTypeId;
            buyerMaster.ForeignCurrency = model.ForeignCurrency;
            buyerMaster.OnHold = true;
            buyerMaster.Active = true;
            buyerMaster.UpdatedDate = null;
            buyerMaster.UpdatedBy = null;
            buyerMaster.IsDeleted = false;
            buyerMaster.BuyerCode = model.BuyerCode;
            buyerMaster.BuyerShortName = model.BuyerShortName;
            buyerMaster.DeletedBy = null;
            buyerMaster.DeletedDate = null;
            buyerMaster.DcBalance = model.DcBalance;
            buyerMaster.ForeignDcBalance = model.ForeignDcBalance;
            buyerMaster.Website = model.Website;

            var result = _buyerMasterManager.post(buyerMaster);

            if (result == true)
            {
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Save process Failed" }, JsonRequestBehavior.AllowGet);
            }
            return null;

        }  

       
        [HttpPost]
        public ActionResult GetbyIdBuyermasterDetails(int id)
        {
            BuyerMasterManager _buyerMasterManager = new BuyerMasterManager();
            try
            {
               
                BuyerMasterModel dtomodel = new BuyerMasterModel();
                BuyerModel_SP buyerModel_SP = new BuyerModel_SP();
               // dtomodel.BuyerMasterList = _buyerMasterManager.Get();

                var  buyermanager= _buyerMasterManager.GetSingleBuyerModel(id);

                if( buyermanager != null ) 
                {
                    dtomodel.BuyerMasterId = buyermanager.BuyerMasterId;
                    dtomodel.BuyerCode = buyermanager.BuyerCode;
                    dtomodel.BuyerShortName = buyermanager.BuyerShortName;
                    dtomodel.CustomerName = buyermanager.CustomerName;
                    dtomodel.Account = buyermanager.Account;
                    dtomodel.AccountName = buyermanager.AccountName;
                    dtomodel.AccountDescription = buyermanager.AccountDescription;
                    dtomodel.SwiftCode = buyermanager.SwiftCode;
                    dtomodel.Physical1 = buyermanager.Physical1;
                    dtomodel.PhysicalCode = buyermanager.PhysicalCode;
                    dtomodel.CurrencyId = buyermanager.CurrencyId;
                    dtomodel.Telephone1 = buyermanager.Telephone1;
                    dtomodel.Telephone2 = buyermanager.Telephone2;
                    dtomodel.EmailContact = buyermanager.EmailContact;
                    dtomodel.EmailAccounts = buyermanager.EmailAccounts;
                    dtomodel.EmailEmergency = buyermanager.EmailEmergency;
                    dtomodel.AccountTypeId = buyermanager.AccountTypeId;
                    dtomodel.VatNumber = buyermanager.VatNumber;
                    dtomodel.RegNumber = buyermanager.RegNumber;
                    dtomodel.CreditLimit = buyermanager.CreditLimit;
                    dtomodel.ChargeInterest = buyermanager.ChargeInterest;
                    dtomodel.Interest = buyermanager.Interest;
                    dtomodel.DateAdded = buyermanager.DateAdded;
                    dtomodel.TaxTypeId = buyermanager.TaxTypeId;
                    dtomodel.ForeignCurrency = buyermanager.ForeignCurrency;
                    dtomodel.DcBalance = buyermanager.DcBalance;
                    dtomodel.ForeignDcBalance = buyermanager.ForeignDcBalance;
                    dtomodel.Website = buyermanager.Website;               

                }
                return PartialView("Partial/BuyerMasteDetails", dtomodel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }

        //update the employee
        [HttpPost]
        public ActionResult Update(BuyerMasterModel model)
        {
            BuyerMasterManager _buyerMasterManager = new BuyerMasterManager();
            try
            {     
                var existingBuyer = _buyerMasterManager.GetSingleBuyerModel(model.BuyerMasterId);

                if (model != null)
                {

                    existingBuyer.BuyerMasterId = model.BuyerMasterId;        
                    existingBuyer.CustomerName = model.CustomerName;
                    existingBuyer.Account = model.Account;
                    existingBuyer.AccountName = model.AccountName;
                    existingBuyer.AccountDescription = model.AccountDescription;
                    existingBuyer.SwiftCode = model.SwiftCode;
                    existingBuyer.Physical1 = model.Physical1;
                  
                    existingBuyer.PhysicalCode = model.PhysicalCode;
                   
                    existingBuyer.CurrencyId = model.CurrencyId;
                    existingBuyer.Telephone1 = model.Telephone1;
                    existingBuyer.Telephone2 = model.Telephone2;
                    existingBuyer.EmailContact = model.EmailContact;
                    existingBuyer.EmailAccounts = model.EmailAccounts;
                    existingBuyer.EmailEmergency = model.EmailEmergency;
                    existingBuyer.AccountTypeId = model.AccountTypeId;
                    existingBuyer.VatNumber = model.VatNumber;
                    existingBuyer.RegNumber = model.RegNumber;
                    existingBuyer.CreditLimit = model.CreditLimit;
                    existingBuyer.ChargeInterest = true;
                    existingBuyer.Interest = model.Interest;
                    existingBuyer.TaxTypeId = model.TaxTypeId;
                    existingBuyer.ForeignCurrency = model.ForeignCurrency;
                    existingBuyer.OnHold = true;
                    existingBuyer.Active = true;
                    existingBuyer.BuyerCode = model.BuyerCode;
                    existingBuyer.BuyerShortName = model.BuyerShortName;
                    existingBuyer.DcBalance = model.DcBalance;
                    existingBuyer.Website = model.Website;
                    var result = _buyerMasterManager.Putbuyer(existingBuyer);
                   

                    if (result == true)
                    {                  
                        return Json(new { AlertMessage = "Updated", JsonRequestBehavior.AllowGet });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        //delete the employee
        public ActionResult Delete(int id)
        {
            BuyerMasterManager _buyerMasterManager = new BuyerMasterManager();
            try
            {
                string status = "";
                //var result = _buyerMasterManager.GetSingleBuyerModel(id);
                if(id != 0)
                {                    
                    _buyerMasterManager.Deletebuyer(id);
                    status = "Success";
                }
                return Json(status, JsonRequestBehavior.AllowGet);
             
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        //search
        public ActionResult Search(string filter)
        {
            BuyerMasterManager _buyerMasterManager = new BuyerMasterManager();

            List<BuyerMasterModel> filterbuyerMasterModels = new List<BuyerMasterModel>();

            var buyermasterlist = _buyerMasterManager.GetBuyerModels();
            var filterlist = buyermasterlist.Where(x => x.CustomerName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            foreach (var item in filterlist)
            {
                BuyerMasterModel model = new BuyerMasterModel();
                model.BuyerMasterId = item.BuyerMasterId;
                model.BuyerCode = item.BuyerCode;
                model.CustomerName = item.CustomerName;
                model.Account = item.Account;
                model.Physical1 = item.Physical1;
                model.Telephone1 = item.Telephone1;
                model.EmailContact = item.EmailContact;
                filterbuyerMasterModels.Add(model);
            }
            return Json(filterbuyerMasterModels, JsonRequestBehavior.AllowGet);           
        }

    }
}
