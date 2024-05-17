using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.MaterialGroupMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class MaterialGroupController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult MaterialGroup()
        {
            SubGroupManager Manager = new SubGroupManager();
            MaterialGroupMasterModel model = new MaterialGroupMasterModel();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubGroupDescription).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.SubGroupDescription,
                                        Value = item.SubGroupID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            model.SubGroupList = items;
            return View(model);
        }

        public ActionResult MaterialGroupGrid()
        {
            MaterialGroupMasterModel vm = new MaterialGroupMasterModel();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            vm.MaterialGrouplist = materialGroupManager.Get();
            SubGroupManager Manager = new SubGroupManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubGroupDescription).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.SubGroupDescription,
                                        Value = item.SubGroupID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            vm.SubGroupList = items;
            return PartialView("Partial/MaterialGroupGrid", vm);
        }
        [HttpPost]
        public ActionResult MaterialGroupDetails(int MaterialGroupMasterId)
        {
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialGroupMaster_ materialGroupMaster_ = new MaterialGroupMaster_();
            MaterialGroupMasterModel model = new MaterialGroupMasterModel();
            MaterialCategoryManager CategoryManager = new MaterialCategoryManager();
            SubGroupManager Manager = new SubGroupManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.SubGroupDescription).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.SubGroupDescription,
                                        Value = item.SubGroupID.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            model.SubGroupList = items;
            materialGroupMaster_ = materialGroupManager.GetMaterialGroupMaster_Id(MaterialGroupMasterId);
            if (materialGroupMaster_.MaterialGroupMasterId != 0)
            {

                materialCategoryMaster = CategoryManager.GetMaterialCategoryMaster(materialGroupMaster_.MaterialCategoryMasterId);
                model.GroupName = materialGroupMaster_.GroupName;
                model.StoreName = materialGroupMaster_.StoreName;
                model.MaterialGroupMasterId = materialGroupMaster_.MaterialGroupMasterId;
                model.SubGroupName = materialGroupMaster_.SubGroup;
                model.MaterialCategoryMasterId = materialGroupMaster_.MaterialCategoryMasterId;
                model.IsSubstance = materialGroupMaster_.IsSubstance;
                model.IsSize = materialGroupMaster_.IsSize;
                model.IsColor = materialGroupMaster_.IsColor;
                model.CateogoryCode = materialCategoryMaster.CategoryCode;
                model.IsConsumption = materialGroupMaster_.IsConsumption;
                model.IsInspection = materialGroupMaster_.IsInspection;
                model.IsReservation = materialGroupMaster_.IsReservation;
                model.IsDisplay = materialGroupMaster_.IsDisplay;
                model.IsBatchcode = materialGroupMaster_.IsBatchcode;
                model.IsMultipleUnits = materialGroupMaster_.IsMultipleUnits;
                model.IsLeatherType = materialGroupMaster_.IsLeatherType;
                model.CreatedDate = materialGroupMaster_.CreatedDate;
                model.UpdatedDate = materialGroupMaster_.UpdatedDate;
                model.CreatedBy = string.Empty;
                model.UpdatedBy = string.Empty;
            }
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateMaterialGroupID();
            ID = (100 + ID);
           
            ID++;
            if (MaterialGroupMasterId != 0)
            {
                MaterialGroupMasterId = (MaterialGroupMasterId + 100);
                model.GroupCode = "MGM" + " " + MaterialGroupMasterId;
            }
            else
            {
                model.GroupCode = "MGM" + " " + ID;
            }
            return PartialView("Partial/MaterialGroupDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult MaterialGroup(MaterialGroupMasterModel model, FormCollection collection)
        {
            MaterialGroupMaster_ materialGroupMasters_ = new MaterialGroupMaster_();
            if (ModelState.IsValid)
            {
                MaterialGroupMaster_ materialCategoryMaster = new MaterialGroupMaster_();
                MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                materialCategoryMaster = materialGroupManager.GetGroupName(model.GroupName);
                if (materialCategoryMaster==null)
                {
                    materialGroupMasters_.GroupCode = model.GroupCode;
                    materialGroupMasters_.GroupName = model.GroupName;
                    materialGroupMasters_.StoreName = model.StoreName;
                    materialGroupMasters_.SubGroup = model.SubGroupName;
                    materialGroupMasters_.IsLeatherType = model.IsLeatherType;
                    materialGroupMasters_.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                    materialGroupMasters_.IsSubstance = model.IsSubstance;
                    materialGroupMasters_.IsSize = model.IsSize;
                    materialGroupMasters_.IsColor = model.IsColor;
                    materialGroupMasters_.IsConsumption = model.IsConsumption;
                    materialGroupMasters_.IsInspection = model.IsInspection;
                    materialGroupMasters_.IsReservation = model.IsReservation;
                    materialGroupMasters_.IsDisplay = model.IsDisplay;
                    materialGroupMasters_.IsBatchcode = model.IsBatchcode;
                    materialGroupMasters_.IsMultipleUnits = model.IsMultipleUnits;
                    materialGroupMasters_.CreatedDate = DateTime.Now;
                    materialGroupManager.Post(materialGroupMasters_);
                }
                if (materialCategoryMaster != null && model.GroupName == materialCategoryMaster.GroupName)
                {
                    materialGroupMasters_.MaterialGroupMasterId = 0;
                    return Json(materialGroupMasters_, JsonRequestBehavior.AllowGet);
                }
                else
                {
                   
                    return Json(materialGroupMasters_, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(materialGroupMasters_, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(MaterialGroupMasterModel model)
        {
            MaterialGroupMaster_ materialGroupMasters_ = new MaterialGroupMaster_();
            MaterialGroupMaster_ materialGroupMaster_ = new MaterialGroupMaster_();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialGroupMaster_ = materialGroupManager.GetMaterialGroupMaster_Id(model.MaterialGroupMasterId);
            if (materialGroupMaster_ != null)
            {
                materialGroupMasters_.GroupCode = model.GroupCode;
                materialGroupMasters_.GroupName = model.GroupName;
                materialGroupMasters_.MaterialGroupMasterId = model.MaterialGroupMasterId;
                materialGroupMasters_.SubGroup = model.SubGroupName;
                materialGroupMasters_.StoreName = model.StoreName;
                materialGroupMasters_.IsLeatherType = model.IsLeatherType;
                materialGroupMasters_.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                materialGroupMasters_.IsSubstance = model.IsSubstance;
                materialGroupMasters_.IsSize = model.IsSize;
                materialGroupMasters_.IsColor = model.IsColor;
                materialGroupMasters_.IsConsumption = model.IsConsumption;
                materialGroupMasters_.IsInspection = model.IsInspection;
                materialGroupMasters_.IsReservation = model.IsReservation;
                materialGroupMasters_.IsDisplay = model.IsDisplay;
                materialGroupMasters_.IsBatchcode = model.IsBatchcode;
                materialGroupMasters_.IsMultipleUnits = model.IsMultipleUnits;
                materialGroupMasters_.CreatedDate = model.CreatedDate;
                materialGroupMasters_.UpdatedDate = DateTime.Now;
                materialGroupManager.Put(materialGroupMasters_);
            }
            else
            {
                return Json(materialGroupMasters_, JsonRequestBehavior.AllowGet);
            }
            
            return Json(materialGroupMasters_, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int MaterialGroupMasterId)
        {
            MaterialGroupMaster_ materialGroupMaster_ = new MaterialGroupMaster_();
            string status = "";
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialGroupMaster_ = materialGroupManager.GetMaterialGroupMaster_Id(MaterialGroupMasterId);
            if (materialGroupMaster_ != null)
            {
                status = "Success";
                materialGroupManager.Delete(materialGroupMaster_.MaterialGroupMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<MaterialGroupMaster_> materialGroupMaster_ = new List<MaterialGroupMaster_>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialGroupMaster_ = materialGroupManager.Get();
            materialGroupMaster_ = materialGroupMaster_.Where(x => x.GroupName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.GroupCode.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            MaterialGroupMasterModel model = new MaterialGroupMasterModel();
            model.MaterialGrouplist = materialGroupMaster_;
            return PartialView("Partial/MaterialGroupGrid", model);
        }
        public JsonResult GetMaterialName(int MaterialGroupMasterId)
        {
            MaterialCategoryManager Manager = new MaterialCategoryManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            materialCategoryMaster = Manager.GetMaterialCategoryMaster(MaterialGroupMasterId);
            return Json(materialCategoryMaster.MaterialCategoryMasterId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategoryCodeDetail(int? CategoryId)
        {
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(CategoryId);
            return Json(new { CateogoryCode = materialCategoryMaster.CategoryCode }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
