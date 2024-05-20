using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
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
            materialgroupmaster materialgroupmaster = new materialgroupmaster();
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
            materialgroupmaster = materialGroupManager.GetmaterialgroupmasterId(MaterialGroupMasterId);
            if (materialgroupmaster.MaterialGroupMasterId != 0)
            {

                materialCategoryMaster = CategoryManager.GetMaterialCategoryMaster(materialgroupmaster.MaterialCategoryMasterId);
                model.GroupName = materialgroupmaster.GroupName;
                model.StoreName = materialgroupmaster.StoreName;
                model.MaterialGroupMasterId = materialgroupmaster.MaterialGroupMasterId;
                model.SubGroupName = materialgroupmaster.SubGroup;
                model.MaterialCategoryMasterId = materialgroupmaster.MaterialCategoryMasterId;
                model.IsSubstance = materialgroupmaster.IsSubstance;
                model.IsSize = materialgroupmaster.IsSize;
                model.IsColor = materialgroupmaster.IsColor;
                model.CateogoryCode = materialCategoryMaster.CategoryCode;
                model.IsConsumption = materialgroupmaster.IsConsumption;
                model.IsInspection = materialgroupmaster.IsInspection;
                model.IsReservation = materialgroupmaster.IsReservation;
                model.IsDisplay = materialgroupmaster.IsDisplay;
                model.IsBatchcode = materialgroupmaster.IsBatchcode;
                model.IsMultipleUnits = materialgroupmaster.IsMultipleUnits;
                model.IsLeatherType = materialgroupmaster.IsLeatherType;
                model.CreatedDate = materialgroupmaster.CreatedDate;
                model.UpdatedDate = materialgroupmaster.UpdatedDate;
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
            materialgroupmaster materialGroupMasters_ = new materialgroupmaster();
            if (ModelState.IsValid)
            {
                materialgroupmaster materialCategoryMaster = new materialgroupmaster();
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
            materialgroupmaster materialGroupMasters_ = new materialgroupmaster();
            materialgroupmaster materialgroupmaster = new materialgroupmaster();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster = materialGroupManager.GetmaterialgroupmasterId(model.MaterialGroupMasterId);
            if (materialgroupmaster != null)
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
            materialgroupmaster materialgroupmaster = new materialgroupmaster();
            string status = "";
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster = materialGroupManager.GetmaterialgroupmasterId(MaterialGroupMasterId);
            if (materialgroupmaster != null)
            {
                status = "Success";
                materialGroupManager.Delete(materialgroupmaster.MaterialGroupMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<materialgroupmaster> materialgroupmaster = new List<materialgroupmaster>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster = materialGroupManager.Get();
            materialgroupmaster = materialgroupmaster.Where(x => x.GroupName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.GroupCode.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            MaterialGroupMasterModel model = new MaterialGroupMasterModel();
            model.MaterialGrouplist = materialgroupmaster;
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
