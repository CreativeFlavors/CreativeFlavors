using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.BuyerMaserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class BuyerMaterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult BuyerMater()
        {
            BuyerMasterModel vm = new BuyerMasterModel();
            return View(vm);
        }
        public ActionResult BuyerMasterGird()
        {
            BuyerMasterModel vm = new BuyerMasterModel();
            BuyerManager buyerManager = new BuyerManager();
            vm.BuyerMasterList = buyerManager.Get();

            return PartialView("Partial/BuyerMasterGird", vm);
        }

        [HttpPost]
        public ActionResult BuyerMasteDetails(int BuyerMasterId)
        {
            BuyerManager buyerManager = new BuyerManager();
            BuyerMaster buyerMaster = new BuyerMaster();
            BuyerMasterModel model = new BuyerMasterModel();
            buyerMaster = buyerManager.GetBuyerMasterId(BuyerMasterId);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateBuyerID();

            if (buyerMaster != null && buyerMaster.BuyerMasterId != 0)
            {
                model.BuyerMasterId = buyerMaster.BuyerMasterId;
                model.BuyerFullName = buyerMaster.BuyerFullName;
                model.BuyerShortName = buyerMaster.BuyerShortName;
                model.Currency = buyerMaster.Currency;
                model.BuyerAddress1 = buyerMaster.BuyerAddress1;
                model.BuyerAddress2 = buyerMaster.BuyerAddress2;
                model.BuyerPincode = buyerMaster.BuyerPincode;
                model.Country = buyerMaster.Country;
                model.ContactPersion = buyerMaster.ContactPersion;
                model.Designation = buyerMaster.Designation;
                model.ContactNoo = buyerMaster.ContactNoo;
                model.EmailID = buyerMaster.EmailID;
                model.STNoHead = buyerMaster.STNoHead;
                model.CGTNoHead = buyerMaster.CGTNoHead;
                model.DeliverAddress1 = buyerMaster.DeliverAddress1;
                model.DeliverAddress2 = buyerMaster.DeliverAddress2;
                model.Pincode = buyerMaster.Pincode;
                model.AgentName = buyerMaster.AgentName;
                model.AgentAddress1 = buyerMaster.AgentAddress1;
                model.AgentAddress2 = buyerMaster.AgentAddress2;
                model.AgentPincode = buyerMaster.AgentPincode;
                model.AgentCountry = buyerMaster.AgentCountry;
                model.AgentCurrency = buyerMaster.AgentCurrency;
                model.PaymentsTerms = buyerMaster.PaymentsTerms;
                model.DeliveryTerms = buyerMaster.DeliveryTerms;
                model.Pincode = buyerMaster.Pincode;
                model.Insurance = buyerMaster.Insurance;
                model.DelierTo = buyerMaster.DelierTo;
                model.Brand = buyerMaster.Brand;
                model.ShipmentTo = buyerMaster.ShipmentTo;
                model.ShimentMode = buyerMaster.ShimentMode;
                model.CountryStamp = buyerMaster.CountryStamp;
                model.CreatedDate = buyerMaster.CreatedDate;
                model.UpdatedDate = buyerMaster.UpdatedDate;
            }
            if (model.BuyerMasterId != 0)
            {
                model.BuyerCode = buyerMaster.BuyerCode;
            }
            else
            {
                ID++;
                model.BuyerCode = "BUY" + ID;
            }
            return PartialView("Partial/BuyerMasteDetails", model);
        }

        [HttpPost]
        public ActionResult AgentModelDetail(string agentName)
        {
            CurrencyManager currencyManager = new CurrencyManager();
            CurrencyMaster currencyMaster = new CurrencyMaster();
            AgentManager agentManager = new AgentManager();
            AgentMaster agentMaster = new AgentMaster();
            agentMaster = agentManager.GetAgent(agentName);
            currencyMaster = currencyManager.GetCurrentMasterId(agentMaster.Currency);
            string addrs1 = "";
            string addrs2 = "";
            string addrs3 = "";
            string address = string.Empty;

            if (agentMaster != null && agentMaster.AddressLine1 != null)
            {
                addrs1 = agentMaster.AddressLine1.Trim();
              address = addrs1 + ",";
                
            }
            if (agentMaster != null && agentMaster.AddressLine2 != null)
            {
                addrs2 = agentMaster.AddressLine2.Trim();
                 address  +=  addrs2 + ",";
            }
            if (agentMaster != null && agentMaster.AddressLine3 != null)
            {
                addrs3 = agentMaster.AddressLine3.Trim();
                address  += addrs3 + ",";
            }
            return Json(new { AddressLine1 = address, Currency = currencyMaster.LongForm, CountryMasterId = agentMaster.CountryMasterId }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region BuyderModel View

        [HttpGet]
        public ActionResult BuyerModel()
        {
            BuyerModels vm = new BuyerModels();
            return View(vm);
        }
        public ActionResult BuyerModelGrid()
        {
            BuyerModels vm = new BuyerModels();
            BuyerModelManager buyerManager = new BuyerModelManager();
            vm.BuyerModelList = buyerManager.Get();

            return PartialView("Partial/BuyerModelGrid", vm);
        }
        [HttpPost]
        public ActionResult BuyerModelDetails(int BuyerModelID)
        {
            BuyerModelManager buyerModelManager = new BuyerModelManager();
            BuyerModel buyerModel = new BuyerModel();
            BuyerModels model = new BuyerModels();
            buyerModel = buyerModelManager.GetBuyerModelID(BuyerModelID);
            int ID = Web.ExtensionMethod.HtmlHelper.getAutoGenerateBuyerModelID();
            if (buyerModel != null && buyerModel.BuyerModelID != 0)
            {
                model.BuyerModelID = buyerModel.BuyerModelID;
                model.BuyerModelCode = model.BuyerModelID.ToString();
                model.BuyerModelName = buyerModel.BuyerModelName;
                model.Remarks = buyerModel.Remarks;

            }
            if (model.BuyerModelID != 0)
            {
                model.BuyerModelCode = "BMC" + buyerModel.BuyerModelID.ToString();
            }
            else
            {
                ID++;
                model.BuyerModelCode = "BMC" + ID;
            }
            return PartialView("Partial/BuyerModelDetails", model);
        }

        #region Curd Opertion

        [HttpPost]
        public ActionResult BuyerModel(BuyerModels model)
        {
            BuyerModel buyerMasters = new BuyerModel();
            if (ModelState.IsValid)
            {
                BuyerModel buyerModel = new BuyerModel();
                BuyerModelManager buyerModelManager = new BuyerModelManager();
                buyerModel = buyerModelManager.GetBuyerModel(model.BuyerModelName);
                if (buyerModel == null)
                {
                    buyerMasters.BuyerModelID = model.BuyerModelID;
                    buyerMasters.BuyerModelName = model.BuyerModelName;
                    buyerMasters.Remarks = model.Remarks;
                    buyerMasters.CreatedDate = DateTime.Now;
                    buyerMasters.UpdatedDate = DateTime.Now;
                    buyerModelManager.Post(buyerMasters);
                }
                else if (buyerModel != null && buyerModel.BuyerModelName == model.BuyerModelName && buyerModel.BuyerModelID == 0)
                {
                    buyerModel.BuyerModelID = 0;
                    return Json(buyerModel, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(buyerMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(buyerMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BuyerModelUpdate(BuyerModels model)
        {
            BuyerModel buyerMasters = new BuyerModel();
            if (ModelState.IsValid)
            {
                BuyerModel buyerModel = new BuyerModel();
                BuyerModelManager buyerModelManager = new BuyerModelManager();
                buyerModel = buyerModelManager.GetBuyerModelID(model.BuyerModelID);
                if (buyerModel!= null)
                {
                    buyerMasters.BuyerModelID = model.BuyerModelID;
                    buyerMasters.BuyerModelName = model.BuyerModelName;
                    buyerMasters.Remarks = model.Remarks;
                    buyerMasters.CreatedDate = buyerModel.CreatedDate;
                    buyerMasters.UpdatedDate = DateTime.Now;
                    buyerModelManager.Put(buyerMasters);
                }
                else
                {
                    return Json(buyerMasters, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(buyerMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuyerModelDelete(int BuyerModelID)
        {
            BuyerModel buyerMasters = new BuyerModel();
            string status = "";
            BuyerModelManager buyerModelManager = new BuyerModelManager();
            buyerMasters = buyerModelManager.GetBuyerModelID(BuyerModelID);
            if (buyerMasters != null)
            {
                status = "Success";
                buyerModelManager.Delete(buyerMasters.BuyerModelID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuyerModelSearch(string filter)
        {
            List<BuyerModel> buyerModelList = new List<BuyerModel>();
            BuyerModelManager buyerModelManager = new BuyerModelManager();
            buyerModelList = buyerModelManager.Get();
            buyerModelList = buyerModelList.Where(x => x.BuyerModelName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            BuyerModels model_ = new BuyerModels();
            model_.BuyerModelList = buyerModelList;
            return PartialView("Partial/BuyerModelGrid", model_);
        }
        #endregion
        #endregion
        #region Curd Operation
        [HttpPost]
        public ActionResult BuyerMater(BuyerMasterModel model)
        {
            BuyerMaster buyerMasters = new BuyerMaster();
            if (ModelState.IsValid)
            {
                BuyerMaster buyerMaster = new BuyerMaster();
                BuyerManager buyerManager = new BuyerManager();
                buyerMaster = buyerManager.GetBuyerFullName(model.BuyerFullName);
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateBuyerID();
                ID++;

                if (buyerMaster == null)
                {
                    buyerMasters.BuyerMasterId = model.BuyerMasterId;
                    buyerMasters.BuyerCode = "BUY" + ID;
                    buyerMasters.BuyerFullName = model.BuyerFullName;
                    buyerMasters.BuyerShortName = model.BuyerShortName;
                    buyerMasters.Currency = model.Currency;
                    buyerMasters.BuyerAddress1 = model.BuyerAddress1;
                    buyerMasters.BuyerAddress2 = model.BuyerAddress2;
                    buyerMasters.BuyerPincode = model.BuyerPincode;
                    buyerMasters.Country = model.Country;
                    buyerMasters.ContactPersion = model.ContactPersion;
                    buyerMasters.Designation = model.Designation;
                    buyerMasters.ContactNoo = model.ContactNoo;
                    buyerMasters.EmailID = model.EmailID;
                    buyerMasters.STNoHead = model.STNoHead;
                    buyerMasters.CGTNoHead = model.CGTNoHead;
                    buyerMasters.DeliverAddress1 = model.DeliverAddress1;
                    buyerMasters.DeliverAddress2 = model.DeliverAddress2;
                    buyerMasters.Pincode = model.Pincode;
                    buyerMasters.AgentName = model.AgentName;
                    buyerMasters.AgentAddress1 = model.AgentAddress1;
                    buyerMasters.AgentAddress2 = model.AgentAddress2;
                    buyerMasters.AgentPincode = model.AgentPincode;
                    buyerMasters.AgentCountry = model.AgentCountry;
                    buyerMasters.AgentCurrency = model.AgentCurrency;
                    buyerMasters.PaymentsTerms = model.PaymentsTerms;
                    buyerMasters.DeliveryTerms = model.DeliveryTerms;
                    buyerMasters.Pincode = model.Pincode;
                    buyerMasters.Insurance = model.Insurance;
                    buyerMasters.DelierTo = model.DelierTo;
                    buyerMasters.Brand = model.Brand;
                    buyerMasters.ShipmentTo = model.ShipmentTo;
                    buyerMasters.ShimentMode = model.ShimentMode;
                    buyerMasters.CountryStamp = model.CountryStamp;
                    buyerMasters.CreatedDate = DateTime.Now;
                    buyerMasters.CreatedBy = model.CreatedBy;

                    buyerManager.Post(buyerMasters);
                }
                else if (buyerMaster != null && buyerMaster.BuyerFullName == model.BuyerFullName && model.BuyerMasterId == 0)
                {
                    buyerMaster.BuyerMasterId = 0;
                    return Json(buyerMaster, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(buyerMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(buyerMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(BuyerMasterModel model)
        {
            BuyerMaster BuyerMasters = new BuyerMaster();
            if (ModelState.IsValid)
            {
                BuyerMaster buyerMaster = new BuyerMaster();
                BuyerManager buyerManager = new BuyerManager();
                buyerMaster = buyerManager.GetBuyerMasterId(model.BuyerMasterId);
                if (buyerMaster != null)
                {
                    BuyerMasters.BuyerMasterId = model.BuyerMasterId;
                    BuyerMasters.BuyerCode = model.BuyerCode;
                    BuyerMasters.BuyerFullName = model.BuyerFullName;
                    BuyerMasters.BuyerShortName = model.BuyerShortName;
                    BuyerMasters.Currency = model.Currency;
                    BuyerMasters.BuyerAddress1 = model.BuyerAddress1;
                    BuyerMasters.BuyerAddress2 = model.BuyerAddress2;
                    BuyerMasters.BuyerPincode = model.BuyerPincode;
                    BuyerMasters.Country = model.Country;
                    BuyerMasters.ContactPersion = model.ContactPersion;
                    BuyerMasters.Designation = model.Designation;
                    BuyerMasters.ContactNoo = model.ContactNoo;
                    BuyerMasters.EmailID = model.EmailID;
                    BuyerMasters.STNoHead = model.STNoHead;
                    BuyerMasters.CGTNoHead = model.CGTNoHead;
                    BuyerMasters.DeliverAddress1 = model.DeliverAddress1;
                    BuyerMasters.DeliverAddress2 = model.DeliverAddress2;
                    BuyerMasters.ContactNoo = model.ContactNoo;
                    BuyerMasters.AgentName = model.AgentName;
                    BuyerMasters.AgentAddress1 = model.AgentAddress1;
                    BuyerMasters.AgentAddress2 = model.AgentAddress2;
                    BuyerMasters.AgentPincode = model.AgentPincode;
                    BuyerMasters.AgentCountry = model.AgentCountry;
                    BuyerMasters.AgentCurrency = model.AgentCurrency;
                    BuyerMasters.PaymentsTerms = model.PaymentsTerms;
                    BuyerMasters.DeliveryTerms = model.DeliveryTerms;
                    BuyerMasters.Pincode = model.Pincode;
                    BuyerMasters.Insurance = model.Insurance;
                    BuyerMasters.DelierTo = model.DelierTo;
                    BuyerMasters.Brand = model.Brand;
                    BuyerMasters.ShipmentTo = model.ShipmentTo;
                    BuyerMasters.ShimentMode = model.ShimentMode;
                    BuyerMasters.CountryStamp = model.CountryStamp;
                    BuyerMasters.CreatedDate = buyerMaster.CreatedDate;
                    BuyerMasters.UpdatedDate = DateTime.Now;
                    buyerManager.Put(BuyerMasters);
                }
                else
                {
                    return Json(BuyerMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(BuyerMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int BuyerMasterId)
        {
            BuyerMaster buyerMaster = new BuyerMaster();
            string status = "";
            BuyerManager buyerManager = new BuyerManager();
            buyerMaster = buyerManager.GetBuyerMasterId(BuyerMasterId);
            if (buyerMaster != null)
            {
                status = "Success";
                buyerManager.Delete(buyerMaster.BuyerMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<BuyerMaster> buyerMasterList = new List<BuyerMaster>();
            BuyerManager buyerManager = new BuyerManager();
            buyerMasterList = buyerManager.Get();
            buyerMasterList = buyerMasterList.Where(x => x.BuyerCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.BuyerFullName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.BuyerShortName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            BuyerMasterModel model = new BuyerMasterModel();
            model.BuyerMasterList = buyerMasterList;
            return PartialView("Partial/BuyerMasterGird", model);
        }
        
        #endregion

    }
}
