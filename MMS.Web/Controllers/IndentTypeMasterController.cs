using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.IndentTypeMasterModel;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Common;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class IndentTypeMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult IndentTypeMaster()
        {
            IndentTypeMasterModel vm = new IndentTypeMasterModel();
            return View(vm);
        }
        public ActionResult IndentTypeMasterGrid()
        {
            IndentTypeMasterModel vm = new IndentTypeMasterModel();
            IndentTypeManager indentManager = new IndentTypeManager();
            vm.IndentMasterList = indentManager.Get();
            return PartialView("Partial/IndentTypeMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult IndentTypeMasterDetails(int IndentMasterId)
        {
            IndentTypeManager indentManager = new IndentTypeManager();
            IndentMaster indentMaster = new IndentMaster();
            IndentTypeMasterModel model = new IndentTypeMasterModel();
            indentMaster = indentManager.GetIndentMasterID(IndentMasterId);
            int ID = Web.ExtensionMethod.HtmlHelper.getIndentTypeMasterId();
            if (indentMaster != null)
            {
                model.IndentMasterID = indentMaster.IndentMasterID;
                model.IndentTypeName = indentMaster.IndentTypeName;                
                model.CreatedBy = indentMaster.CreatedBy;
                model.UpdatedBy = indentMaster.UpdatedBy;
                model.CreatedDate = indentMaster.CreatedDate;
                model.UpdatedDate = indentMaster.UpdatedDate;
            }
            if (model.IndentMasterID != 0)
            {
                model.IndentTypeCode = indentMaster.IndentTypeCode;
            }
            else
            {
                ID++;
                model.IndentTypeCode = "IND" + ID;
            }
            return PartialView("Partial/IndentTypeMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult IndentTypeMaster(IndentTypeMasterModel model)
        {
            IndentMaster IndentMasters = new IndentMaster();
            IndentMaster indentMaster = new IndentMaster();
            if (ModelState.IsValid)
            {
             
                IndentTypeManager indentManager = new IndentTypeManager();
                indentMaster = indentManager.GetIndentTypeName(model.IndentTypeName);
                int ID = Web.ExtensionMethod.HtmlHelper.getIndentTypeMasterId();
                ID++;
                if (indentMaster == null)
                {
                    IndentMasters.IndentMasterID = model.IndentMasterID;
                    IndentMasters.IndentTypeCode = "IND" + ID;
                    IndentMasters.IndentTypeName = model.IndentTypeName;   
                    IndentMasters.CreatedDate = DateTime.Now;
                    IndentMasters.IsDeleted = false;
                    indentManager.Post(IndentMasters);
                }
                else if (indentMaster != null && indentMaster.IndentTypeName == model.IndentTypeName && model.IndentMasterID == 0)
                {
                    indentMaster.IndentMasterID = 0;
                    return Json(indentMaster, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(IndentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(IndentMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(IndentTypeMasterModel model)
        {
            IndentMaster IndentMasters = new IndentMaster();
            IndentMaster indentMaster = new IndentMaster();
            if (ModelState.IsValid)
            {
                IndentTypeManager indentManager = new IndentTypeManager();
                indentMaster = indentManager.GetIndentMasterID(model.IndentMasterID);
                if (indentMaster != null)
                {
                    IndentMasters.IndentMasterID = model.IndentMasterID;
                    IndentMasters.IndentTypeCode = model.IndentTypeCode;
                    IndentMasters.IndentTypeName = model.IndentTypeName;  
                    indentManager.Put(IndentMasters);
                }
                else
                {
                    return Json(IndentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(IndentMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int IndentMasterID)
        {
            IndentMaster indentMaster = new IndentMaster();
            string status = "";
            IndentTypeManager indentManager = new IndentTypeManager();
            indentMaster = indentManager.GetIndentMasterID(IndentMasterID);
            if (indentMaster != null)
            {
                status = "Success";
                indentManager.Delete(indentMaster.IndentMasterID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<IndentMaster> indentMasterList = new List<IndentMaster>();
            IndentTypeManager indentManager = new IndentTypeManager();
            indentMasterList = indentManager.Get();
            indentMasterList = indentMasterList.Where(x => x.IndentTypeCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.IndentTypeName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            IndentTypeMasterModel im = new IndentTypeMasterModel();
            im.IndentMasterList = indentMasterList;
            return PartialView("Partial/IndentTypeMasterGrid", im);
        }
        #endregion


    }
}
