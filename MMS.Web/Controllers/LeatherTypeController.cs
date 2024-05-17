using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.LeatherSize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class LeatherTypeController : Controller
    {
        #region LeatherType View

        [HttpGet]
        public ActionResult LeatherType()
        {
            LeatherTypeModel vm = new LeatherTypeModel();
            return View(vm);
        }
        public ActionResult LeatherTypeGrid()
        {
            LeatherTypeModel vm = new LeatherTypeModel();
            LeatherTypeManager leatherTypeManager = new LeatherTypeManager();
            vm.leatherTypeList = leatherTypeManager.Get();

            return PartialView("Partial/LeatherTypeGrid", vm);
        }
        [HttpPost]
        public ActionResult LeatherTypeDetails(int LeatherTypeID)
        {
            LeatherTypeManager leatherTypeManager = new LeatherTypeManager();
            LeatherType leatherType = new LeatherType();
            LeatherTypeModel model = new LeatherTypeModel();
            leatherType = leatherTypeManager.GetLeatherTypeID(LeatherTypeID);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getLeatherTypeCodeId();
            if(LeatherTypeID==0)
            {
                ID++;
            }
            
            if (leatherType != null)
            {
                model.LeatherTypeID = leatherType.LeatherTypeID;
                model.LeatherTypeCode ="LT"+ ID;
                model.LeatherTypeDescription = leatherType.LeatherTypeDescription;
                model.CreatedDate = leatherType.CreatedDate;
                model.UpdatedDate = leatherType.UpdatedDate;
                model.CreatedBy = leatherType.CreatedBy;
                model.UpdatedBy = leatherType.UpdatedBy;
            }
            return PartialView("Partial/LeatherTypeDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult LeatherType(LeatherTypeModel model)
        {
            LeatherType LeatherType_ = new LeatherType();
            if (ModelState.IsValid)
            {
                LeatherType leatherType = new LeatherType();
                LeatherTypeManager leatherTypeManager = new LeatherTypeManager();
                leatherType = leatherTypeManager.GetLeatherTypeDescription(model.LeatherTypeDescription);
                if (leatherType == null&&model.LeatherTypeID==0)
                {
                    LeatherType_.LeatherTypeCode = model.LeatherTypeCode;
                    LeatherType_.LeatherTypeDescription = model.LeatherTypeDescription;
                    LeatherType_.CreatedDate = DateTime.Now;
                    leatherTypeManager.Post(LeatherType_);
                }
                else if(model.LeatherTypeID!=0)
                {
                    LeatherType_.LeatherTypeID = model.LeatherTypeID;
                    LeatherType_.LeatherTypeCode = model.LeatherTypeCode;
                    LeatherType_.LeatherTypeDescription = model.LeatherTypeDescription;                   
                    LeatherType_.UpdatedDate = DateTime.Now;                   
                    leatherTypeManager.Put(LeatherType_);
                }
                else
                {
                    return Json(LeatherType_, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(LeatherType_, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
     
        public ActionResult Delete(int LeatherTypeID)
        {
            LeatherType leatherType = new LeatherType();
            string status = "";
            LeatherTypeManager leatherTypeManager = new LeatherTypeManager();
            leatherType = leatherTypeManager.GetLeatherTypeID(LeatherTypeID);
            if (leatherType != null)
            {
                status = "Success";
                leatherTypeManager.Delete(leatherType.LeatherTypeID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<LeatherType> countryMasterList = new List<LeatherType>();
            LeatherTypeManager leatherTypeManager = new LeatherTypeManager();
            countryMasterList = leatherTypeManager.Get();
            countryMasterList = countryMasterList.Where(x => x.LeatherTypeDescription.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            LeatherTypeModel vm = new LeatherTypeModel();
            vm.leatherTypeList = countryMasterList;
            return PartialView("Partial/LeatherTypeGrid", vm);
        }
        #endregion


    }
}
