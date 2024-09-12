using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.MaterailCategory;
using MMS.Web.Models.SupplierMaterialModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class CategoryMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult CategoryMaster()    
        {
            MaterialCategoryModel vm = new MaterialCategoryModel();
            return View(vm);
        }
        public ActionResult MaterialCategoryGrid(int page = 1, int pageSize = 15)
        {
            CategorymasterManager materialCategoryManager = new CategorymasterManager();
             var MaterialCatgoryList = materialCategoryManager.Get();
            List< MaterialCategoryModel> totalList = new List< MaterialCategoryModel>();
            foreach( var i in MaterialCatgoryList)
            {
                MaterialCategoryModel vm = new MaterialCategoryModel();
                vm.CategoryName = i.CategoryName;
                vm.CategoryCode = i.CategoryCode;
                vm.Categorytype = i.CategoryType;
                vm.MaterialCategoryMasterId = i.CategoryId;
                totalList.Add(vm);
            }
            var totalCount = totalList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(d => d.MaterialCategoryMasterId)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPage = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return PartialView("Partial/MaterialCategoryGrid", totalList);
        }
        [HttpPost]
        public ActionResult EditCategoryDetails(int CatRepository_id)
        {
            CategorymasterManager CategorymasterManager = new CategorymasterManager();
            MaterialCategoryModel model = new MaterialCategoryModel();
            var CategoryModel = CategorymasterManager.GetCategoryMasterid(CatRepository_id);
            if (CategoryModel != null)
            {
                model.MaterialCategoryMasterId = CategoryModel.CategoryId;
                model.CategoryName = CategoryModel.CategoryName;
                model.CategoryCode = CategoryModel.CategoryCode;
                model.Categorytype = CategoryModel.CategoryType;
            }
            return PartialView("Partial/MaterialCategoryDetails", model);
        }
        [HttpGet]
        public ActionResult MaterialCategoryDetails()
        {
            MaterialCategoryModel model = new MaterialCategoryModel();
            return View("Partial/MaterialCategoryDetails", model);
        }
        #endregion
        #region crud operation
        [HttpPost]
        public ActionResult MaterialCategoryDetailsInsert(MaterialCategoryModel model)
        {
            string Altermessage = "";
            CategoryMaster CategoryMaster = new CategoryMaster();
            CategorymasterManager CategorymasterManager = new CategorymasterManager();
            var MaterialCatgoryList = CategorymasterManager.Get();
            if(model.MaterialCategoryMasterId == 0)
            {
                var productList = MaterialCatgoryList.Where(x => x.CategoryName.ToLower().Contains(model.CategoryName.ToLower()) && x.CategoryCode.ToString().ToLower().Contains(model.CategoryCode.ToString().ToLower())).ToList();
                if (productList.Count() != 0)
                {
                    Altermessage = "alreadythereall";
                    return Json(Altermessage, JsonRequestBehavior.AllowGet);
                }
                var productListcode = MaterialCatgoryList.Where(x => x.CategoryName.ToLower().Contains(model.CategoryName.ToLower())).ToList();
                if (productList.Count() != 0)
                {
                    Altermessage = "CategoryCodeThere";
                    return Json(Altermessage, JsonRequestBehavior.AllowGet);
                }
            }

            CategoryMaster.CategoryName = model.CategoryName;
            CategoryMaster.CategoryType = model.Categorytype;
            CategoryMaster.CategoryCode = model.CategoryCode;
            if (model.MaterialCategoryMasterId == 0)
            {
                CategorymasterManager.Post(CategoryMaster);
                Altermessage = "Success";
            }
            else
            {
                CategoryMaster.CategoryId = model.MaterialCategoryMasterId;
                CategorymasterManager.Put(CategoryMaster);
                Altermessage = "Updated";
            }
            return Json(Altermessage, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int CategoryId)
        {
            CategoryMaster CategoryMaster = new CategoryMaster();
            string status = "";
            CategorymasterManager oCustAddressMangers = new CategorymasterManager();
            CategoryMaster = oCustAddressMangers.GetCategoryMasterid(CategoryId);
            if (CategoryId != 0)
            {
                status = "Success";
                oCustAddressMangers.Delete(CategoryMaster.CategoryId);
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            CategorymasterManager materialCategoryManager = new CategorymasterManager();
            var MaterialCatgoryList = materialCategoryManager.Get();
            List<MaterialCategoryModel> totalList = new List<MaterialCategoryModel>();
            foreach (var i in MaterialCatgoryList)
            {
                MaterialCategoryModel vm = new MaterialCategoryModel();
                vm.CategoryName = i.CategoryName;
                vm.CategoryCode = i.CategoryCode;
                vm.Categorytype = i.CategoryType;
                vm.MaterialCategoryMasterId = i.CategoryId;
                totalList.Add(vm);
            }
            var productList = totalList.Where(x => x.CategoryName.ToLower().Contains(filter.ToLower()) || x.CategoryCode.ToString().ToLower().Contains(filter.ToString().ToLower())).ToList().OrderByDescending(m=>m.MaterialCategoryMasterId).ToList();

            return Json(productList, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
