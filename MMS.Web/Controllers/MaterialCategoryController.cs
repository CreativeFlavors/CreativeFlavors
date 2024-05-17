using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.MaterailCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class MaterialCategoryController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult MaterialCategory()
        {
            MaterialCategoryModel vm = new MaterialCategoryModel();
            return View(vm);
        }
        public ActionResult MaterialCategoryGrid()
        {
            MaterialCategoryModel vm = new MaterialCategoryModel();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            vm.MaterialCatgoryList = materialCategoryManager.Get();

            return PartialView("Partial/MaterialCategoryGrid", vm);
        }
        [HttpPost]
        public ActionResult MaterialCategoryDetails(int MaterialCategoryMasterId)
        {
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            MaterialCategoryModel model = new MaterialCategoryModel();
            materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(MaterialCategoryMasterId);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoMaterialCategoryMasterD();
            ID++;
            if (materialCategoryMaster != null)
            {
                model.CategoryCode = materialCategoryMaster.CategoryCode;
                model.CategoryName = materialCategoryMaster.CategoryName;
                model.MaterialCategoryMasterId = materialCategoryMaster.MaterialCategoryMasterId;
                model.CreatedDate = materialCategoryMaster.CreatedDate;
                model.UpdatedDate = materialCategoryMaster.UpdatedDate;
                model.CreatedBy = materialCategoryMaster.CreatedBy;
                model.UpdatedBy = materialCategoryMaster.UpdatedBy;
            }
            if (MaterialCategoryMasterId != 0)
            {
                model.CategoryCode = model.CategoryCode;
            }
            else
            {
                model.CategoryCode = "MAC" + ID;
            }
            return PartialView("Partial/MaterialCategoryDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult MaterialCategory(MaterialCategoryModel model)
        {
            MaterialCategoryMaster materialCategoryMasters = new MaterialCategoryMaster();
            if (ModelState.IsValid)
            {
                MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                materialCategoryMaster = materialCategoryManager.GetCategoryName(model.CategoryName);
                if (materialCategoryMaster == null)
                {
                    materialCategoryMasters.CategoryCode = model.CategoryCode;
                    materialCategoryMasters.CategoryName = model.CategoryName;
                    materialCategoryMasters.CreatedDate = DateTime.Now;                  
                    materialCategoryManager.Post(materialCategoryMasters);
                }
                else if (materialCategoryMaster != null && materialCategoryMaster.CategoryName == model.CategoryName && model.MaterialCategoryMasterId == 0)
                {
                    materialCategoryMasters.MaterialCategoryMasterId = 0;
                    return Json(materialCategoryMasters, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(materialCategoryMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(materialCategoryMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(MaterialCategoryModel model)
        {
            MaterialCategoryMaster ComponentMasters = new MaterialCategoryMaster();
            if (ModelState.IsValid)
            {
                MaterialCategoryMaster uomMaster = new MaterialCategoryMaster();
                MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                uomMaster = materialCategoryManager.GetMaterialCategoryMaster(model.MaterialCategoryMasterId);
                if (uomMaster != null)
                {
                    ComponentMasters.CategoryCode = model.CategoryCode;
                    ComponentMasters.CategoryName = model.CategoryName;
                    ComponentMasters.MaterialCategoryMasterId = model.MaterialCategoryMasterId; ;
                    ComponentMasters.CreatedDate = uomMaster.CreatedDate;
                    ComponentMasters.UpdatedDate = DateTime.Now;
                    ComponentMasters.CreatedBy = model.CreatedBy;
                    ComponentMasters.UpdatedBy = model.UpdatedBy;
                    materialCategoryManager.Put(ComponentMasters);
                }
                else
                {
                    return Json(ComponentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(ComponentMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int MaterialCategoryMasterId)
        {
            MaterialCategoryMaster uomMaster = new MaterialCategoryMaster();
            string status = "";
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            uomMaster = materialCategoryManager.GetMaterialCategoryMaster(MaterialCategoryMasterId);
            if (uomMaster != null)
            {
                status = "Success";
                materialCategoryManager.Delete(uomMaster.MaterialCategoryMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<MaterialCategoryMaster> UOMMasterlist = new List<MaterialCategoryMaster>();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            UOMMasterlist = materialCategoryManager.Get();
            UOMMasterlist = UOMMasterlist.Where(x => x.CategoryCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.CategoryName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            MaterialCategoryModel model = new MaterialCategoryModel();
            model.MaterialCatgoryList = UOMMasterlist;
            return PartialView("Partial/MaterialCategoryGrid", model);
        }
        #endregion

    }
}
