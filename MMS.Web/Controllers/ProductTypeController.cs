using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.ProductTypeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class ProductTypeController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult ProductType()
        {
            ProductTypeModels vm = new ProductTypeModels();
            return View(vm);
        }
        public ActionResult ProductTypeGrid()
        {
            ProductTypeModels vm = new ProductTypeModels();
            ProductTypeManager productTypeManager = new ProductTypeManager();
            vm.ProductTypeList = productTypeManager.Get();

            return PartialView("Partial/ProductTypeGrid", vm);
        }
        [HttpPost]
        public ActionResult ProductTypeList(int ProductTypeID)
        {
            ProductTypeManager productTypeManager = new ProductTypeManager();
            ProductType colorMaster = new ProductType();
            ProductTypeModels model = new ProductTypeModels();
            colorMaster = productTypeManager.GetProductTypeID(ProductTypeID);
            string autoId = GetAutoid();
            if (colorMaster != null)
            {
                model.ProductTypeName = colorMaster.ProductTypeName;
                model.ProductTypeID = colorMaster.ProductTypeID;
               
                model.CreatedBy = colorMaster.CreatedBy;
                model.UpdatedBy = colorMaster.UpdatedBy;
            }
            if (ProductTypeID == 0)
            {
                model.ProductTypeCode = "Pro" + autoId;
            }
            else
            {
                model.ProductTypeCode = "Pro" + colorMaster.ProductTypeID;
            }
            return PartialView("Partial/ProductTypeList", model);
        }

        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SaveProductType(MMS.Web.Models.ProductTypeModel.ProductTypeModels productTypeModels)
        {
            ProductType productType = new ProductType();

            if (ModelState.IsValid)
            {
                ProductType ProductTypes = new ProductType();
                ProductTypeManager productTypeManager = new ProductTypeManager();
                ProductTypes = productTypeManager.GetProductName(productTypeModels.ProductTypeName);

                if (ProductTypes == null)
                {
                    productType.ProductTypeName = productTypeModels.ProductTypeName;
                    productType.ProductTypeID = productTypeModels.ProductTypeID;
                    productType.CreatedDate = DateTime.Now;
                    productTypeManager.Post(productType);
                }
                else if (ProductTypes != null && productTypeModels.ProductTypeName == ProductTypes.ProductTypeName && productTypeModels.ProductTypeID == 0)
                {
                    productType.ProductTypeID = 0;
                    return Json(productType, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(productType, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(productType, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateProductType(ProductTypeModels productTypeModels)
        {
            ProductType productTypes = new ProductType();
            if (ModelState.IsValid)
            {
                ProductType productType = new ProductType();
                ProductTypeManager productTypeManager = new ProductTypeManager();
                productType = productTypeManager.GetProductTypeID(productTypeModels.ProductTypeID);
                if (productType != null)
                {
                    productTypes.ProductTypeName = productTypeModels.ProductTypeName;
                    productTypes.ProductTypeID = productTypeModels.ProductTypeID;
                    productTypeManager.Put(productTypes);
                }
                else
                {
                    return Json(productTypes, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(productTypes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteProductType(int ProductTypeID)
        {
            ProductType productType = new ProductType();
            string status = "";
            ProductTypeManager productTypeManager = new ProductTypeManager();
            productType = productTypeManager.GetProductTypeID(ProductTypeID);
            if (productType != null)
            {
                status = "Success";
                productTypeManager.Delete(productType.ProductTypeID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public string GetAutoid()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoProductTypeMasterD();
            ID++;
            return ID.ToString();
        }
        public ActionResult Search(string filter)
        {
            List<ProductType> users = new List<ProductType>();
            ProductTypeManager productTypeManager = new ProductTypeManager();
            users = productTypeManager.Get();
            users = users.Where(x => x.ProductTypeName.ToLower().Contains(filter.ToLower())).ToList();
            Session["ColorList"] = users;
            ProductTypeModels model = new ProductTypeModels();
            model.ProductTypeList = users;
            return PartialView("Partial/ProductTypeGrid", model);
        }
        #endregion


    }
}
