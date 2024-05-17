using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SubGroupModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SubGroupController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SubGroup()
        {
            SubGroupModel vm = new SubGroupModel();
            return View(vm);
        }
        public ActionResult SubGroupGrid()
        {
            SubGroupModel vm = new SubGroupModel();
            SubGroupManager countryManager = new SubGroupManager();
            vm.SubGroupList = countryManager.Get().Where(x => x.IsDeleted == false).ToList();

            return PartialView("Partial/SubGroupGrid", vm);
        }
        [HttpPost]
        public ActionResult SubGroupDetails(int SubGroupID)
        {
            SubGroupManager SubGroupManager = new SubGroupManager();
            SubGroup subGroup = new SubGroup();
            SubGroupModel model = new SubGroupModel();
            subGroup = SubGroupManager.GetSubGroupMasterId(SubGroupID);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateSubGroupID();
            ID++;

            if (subGroup != null)
            {
                model.SubGroupID = subGroup.SubGroupID;      
                model.SubGroupDescription = subGroup.SubGroupDescription;
                model.CreatedDate = subGroup.CreatedDate;
                model.UpdatedDate = subGroup.UpdatedDate;
                model.CreatedBy = subGroup.CreatedBy;
                model.IsDeleted = subGroup.IsDeleted;
                model.UpdatedBy = subGroup.UpdatedBy;
            }
            if (SubGroupID != 0)
            {
                model.SubGroupCode = "SGM" + SubGroupID;
            }
            else
            {
                model.SubGroupCode = "SGM" + ID;
            }
            return PartialView("Partial/SubGroupDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult Save(SubGroupModel model)
        {
            SubGroup subGroups = new SubGroup();
            if (ModelState.IsValid)
            {
                SubGroup subGroup = new SubGroup();
                SubGroupManager subGroupManager = new SubGroupManager();
                subGroup = subGroupManager.GetsubGroupDescriptionName(model.SubGroupDescription);
                if (subGroup == null)
                {
                    subGroups.SubGroupDescription = model.SubGroupDescription;
                    subGroups.IsDeleted = model.IsDeleted;
                    subGroups.CreatedDate = DateTime.Now;
                    subGroupManager.Post(subGroups);
                }
                else if (subGroup != null && subGroup.SubGroupDescription == model.SubGroupDescription && model.SubGroupID == 0)
                {
                    subGroups.SubGroupID = 0;
                    return Json(subGroups, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(subGroups, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(subGroups, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SubGroupModel model)
        {
            SubGroup subGroup = new SubGroup();
            if (ModelState.IsValid)
            {

                SubGroupManager subGroupManager = new SubGroupManager();
                subGroup = subGroupManager.GetSubGroupMasterId(model.SubGroupID);
                if (subGroup != null)
                {
                    subGroup.SubGroupDescription = model.SubGroupDescription;
                    subGroup.SubGroupID = model.SubGroupID;
                    subGroup.IsDeleted = model.IsDeleted;
                    subGroup.CreatedDate = model.CreatedDate;
                    subGroup.UpdatedDate = DateTime.Now;
                    subGroupManager.Put(subGroup);
                }
                else
                {
                    return Json(subGroup, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(subGroup, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SubGroupID)
        {
            SubGroup subGroup = new SubGroup();
            string status = "";
            SubGroupManager subGroupManager = new SubGroupManager();
            subGroup = subGroupManager.GetSubGroupMasterId(SubGroupID);
            if (subGroup != null)
            {
                status = "Success";
                subGroupManager.Delete(subGroup.SubGroupID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SubGroup> subGroupMasterList = new List<SubGroup>();
            SubGroupManager subGroupManager = new SubGroupManager();
            subGroupMasterList = subGroupManager.Get();
            subGroupMasterList = subGroupMasterList.Where(x => x.SubGroupDescription.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            SubGroupModel vm = new SubGroupModel();
            vm.SubGroupList = subGroupMasterList;
            return PartialView("Partial/SubGroupGrid", vm);
        }
        #endregion

    }
}
