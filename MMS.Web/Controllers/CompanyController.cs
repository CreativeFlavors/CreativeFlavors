using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class CompanyController : Controller
    {

        #region View 
        public ActionResult Company()
        {
            CompanyModel model = new CompanyModel();
            return View(model);
        }
        public ActionResult CompanyGrid()
        {
            CompanyModel model = new CompanyModel();
            CompanyManager companyManager = new CompanyManager();
            model.companyList = companyManager.Get();

            return PartialView("Partial/CompanyGrid", model);
        }
        [HttpPost]
        public ActionResult CompanyDetails(int CompanyCodePK)
        {
            CompanyModel model = new CompanyModel();
            CompanyManager companyManager = new CompanyManager();
            Company company = new Company();
            company = companyManager.GetCompanyCode(CompanyCodePK);
            if (company != null && company.CompanyCodePK != 0)
            {
                model.CompanyCodePK = company.CompanyCodePK;
                model.SupplierName = company.SupplierName;
                model.City = company.City;
                model.StoreName = company.StoreID;
                model.Address = company.Address;
                model.DeliverTerms = company.DeliverTerms;
                model.TermsConditions = company.TermsConditions;
                model.CompanyName = company.CompanyName;
                model.Phone = company.Phone;
                model.PaymentTerms = company.PaymentTerms;
                model.OtherTerms = company.OtherTerms;
            }
            return PartialView("Partial/CompanyDetails", model);
        }
        #endregion
        #region Curd Operation
        [HttpPost]
        public ActionResult Company(CompanyModel model)
        {
            CompanyManager companyManager = new CompanyManager();
            Company company = new Company();
            Company companyDetails = new Company();
            CountryMaster countryMasters = new CountryMaster();
            if (ModelState.IsValid)
            {

                company = companyManager.IsExistCompany(model.CompanyName);
                Company company_ = new Company();
                company_.StoreID = model.StoreName;
                company_.CompanyCodePK = model.CompanyCodePK;
                company_.SupplierName = model.SupplierName;
                company_.City = model.City;
                company_.Address = model.Address;
                company_.DeliverTerms = model.DeliverTerms;
                company_.TermsConditions = model.TermsConditions;
                company_.CompanyName = model.CompanyName;
                company_.Phone = model.Phone;
                company_.PaymentTerms = model.PaymentTerms;
                company_.OtherTerms = model.OtherTerms;
                if (company == null)
                {
                    companyDetails = companyManager.Post(company_);
                }
                else
                {
                    companyDetails = companyManager.Post(company_);
                    return Json(companyDetails, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(companyDetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int CompanyCodePK)
        {
            CompanyManager companyManager = new CompanyManager();
            Company company = new Company();
            string status = "";
            ColorManager colorManager = new ColorManager();
            company = companyManager.GetCompanyCode(CompanyCodePK);
            if (company != null)
            {
                status = "Success";
                companyManager.Delete(CompanyCodePK);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
