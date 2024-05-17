using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.TaxTypeMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MMS.Common;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class TaxTypeMasterController : Controller
    {

      
         
        #region Accounts View

        [HttpGet]
        public ActionResult TaxTypeMaster()
        {
            TaxTypeMasterModel vm = new TaxTypeMasterModel();
            return View(vm);
        }
        public ActionResult TaxTypeMasterGrid()
        {
            TaxTypeMasterModel vm = new TaxTypeMasterModel();
            TaxTypeManager taxManager = new TaxTypeManager();
            vm.TaxTypeMasterList = taxManager.Get();

            return PartialView("Partial/TaxTypeMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult TaxTypeMasterDetails(int TaxMasterID)
        {
            TaxTypeManager taxManager = new TaxTypeManager();
            TaxTypeMaster taxMaster = new TaxTypeMaster();
            TaxTypeMasterModel model = new TaxTypeMasterModel();
            model.TaxTypeMasterList = taxManager.Get();
            taxMaster = taxManager.GetTaxMasterId(TaxMasterID);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getTaxTypeCodeId();
         

            if (taxMaster != null)
            {
                model.TaxMasterID      = taxMaster.TaxMasterID;
                model.TaxName          = taxMaster.TaxName;
                model.TaxValue         = taxMaster.TaxValue;
                model.TaxCode          = taxMaster.TaxCode;
                model.TaxRef           = taxMaster.TaxRef;
                model.Form             = taxMaster.Form;
                model.TaxOn            = taxMaster.TaxOn;
                model.TaxValueMode     = taxMaster.TaxValueMode;
                model.CreatedDate      = taxMaster.CreatedDate;
                model.UpdatedDate      = taxMaster.UpdatedDate;
                model.CreatedBy        = taxMaster.CreatedBy;
                model.UpdatedBy        = taxMaster.UpdatedBy;
            }
            if (model.TaxMasterID != 0)
            {
                model.TaxCode = taxMaster.TaxCode;
            }
            else
            {
                ID++;
                model.TaxCode = "TT" + ID;
            }
            return PartialView("Partial/TaxTypeMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult TaxTypeMaster(TaxTypeMasterModel model)
        {
            TaxTypeMaster taxMasters = new TaxTypeMaster();
            if (ModelState.IsValid)
            {
                TaxTypeMaster taxMaster = new TaxTypeMaster();
                TaxTypeManager taxManager = new TaxTypeManager();
                taxMaster = taxManager.GetTaxName(model.TaxName);
                if (taxMaster == null)
                {
                    int ID = MMS.Web.ExtensionMethod.HtmlHelper.getTaxTypeCodeId();
                    ID++;
                    taxMasters.TaxMasterID                =  model.TaxMasterID;    
                    taxMasters.TaxName            =  model.TaxName;          
                    taxMasters.TaxValue                 =  model.TaxValue;
                    taxMasters.TaxCode = "TT" + ID;          
                    taxMasters.TaxRef             =  model.TaxRef;          
                    taxMasters.Form             =  model.Form;            
                    taxMasters.TaxOn             =  model.TaxOn;           
                    taxMasters.TaxValueMode =  model.TaxValueMode;
                    taxMasters.CreatedDate = DateTime.Now;
                     
                    taxManager.Post(taxMasters);

                }
                else if (taxMaster != null && taxMaster.TaxName == model.TaxName)
                {
                    taxMasters.TaxMasterID = 0;
                    return Json(taxMasters, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(taxMasters, JsonRequestBehavior.AllowGet);
                }
               
            }
            return Json(taxMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(TaxTypeMasterModel model)
        {
            TaxTypeMaster taxMasters = new TaxTypeMaster();
            if (ModelState.IsValid)
            {
                TaxTypeMaster taxMaster = new TaxTypeMaster();
                TaxTypeManager taxManager = new TaxTypeManager();
                taxMaster = taxManager.GetTaxMasterId(model.TaxMasterID);
                if (taxMaster != null)
                {

                    taxMasters.TaxMasterID = model.TaxMasterID;
                    taxMasters.TaxName = model.TaxName;
                    taxMasters.TaxValue = model.TaxValue;
                    taxMasters.TaxCode = model.TaxCode;
                    taxMasters.TaxRef = model.TaxRef;
                    taxMasters.Form = model.Form;
                    taxMasters.TaxOn = model.TaxOn;
                    taxMasters.TaxValueMode = model.TaxValueMode;
                    taxManager.Put(taxMasters);
                }
                else
                {
                    return Json(taxMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(taxMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int TaxMasterID)
        {
            TaxTypeMaster taxMaster = new TaxTypeMaster();
            string status = "";
            TaxTypeManager taxManager = new TaxTypeManager();
            taxMaster = taxManager.GetTaxMasterId(TaxMasterID);
            if (taxMaster != null)
            {
                status = "Success";              
                taxManager.Delete(taxMaster.TaxMasterID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<TaxTypeMaster> taxmasterList = new List<TaxTypeMaster>();
            TaxTypeManager taxManager = new TaxTypeManager();
            taxmasterList = taxManager.Get();
            taxmasterList = taxmasterList.Where(x => x.TaxName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.TaxCode.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            TaxTypeMasterModel vm = new TaxTypeMasterModel();
            vm.TaxTypeMasterList = taxmasterList;
            return PartialView("Partial/TaxTypeMasterGrid", vm);
        }

        
        #endregion


    }
}
