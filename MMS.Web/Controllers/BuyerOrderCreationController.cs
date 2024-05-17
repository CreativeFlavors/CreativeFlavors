using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models;
using MMS.Web.Models.BuyerOrderCreationModel;
using MMS.Web.Models.Product_BuyerStyleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class BuyerOrderCreationController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult BuyerOrderCreation()
        {
            BuyerOrderCreationModel vm = new BuyerOrderCreationModel();
            return View(vm);
        }
        public ActionResult BuyerOrderCreationGrid()
        {
            BuyerOrderCreationModel vm = new BuyerOrderCreationModel();
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            vm.buyerOrderCreationList = buyerOrderCreationManager.Get();

            return PartialView("Partial/BuyerOrderCreationGrid", vm);
        }
        [HttpPost]
        public ActionResult BuyerOrderCreationDetails(int BuyerOrderCreationID)
        {
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            BuyerOrderCreation buyerOrderCreation = new BuyerOrderCreation();
            BuyerOrderCreationModel vm = new BuyerOrderCreationModel();
            string AutoCode;
            buyerOrderCreation = buyerOrderCreationManager.GetBuyerOrderCreationID(BuyerOrderCreationID);
            if (BuyerOrderCreationID == 0)
            {
                AutoCode = GetMasterID();
                AutoCode = "Con-" + AutoCode;
                vm.MaterialCode = AutoCode;
            }
            else
            {
                vm.MaterialCode = "Con-" + buyerOrderCreation.MaterialCode;
            }
            if (buyerOrderCreation != null)
            {
                vm.BuyerOrderCreationID = buyerOrderCreation.BuyerOrderCreationID;
                vm.BuyerId = buyerOrderCreation.BuyerId;
                vm.MaterialCode = buyerOrderCreation.MaterialCode;
                vm.MaterialName = buyerOrderCreation.MaterialName;
                vm.StockUnit = buyerOrderCreation.StockUnit;
                vm.SizeRange = buyerOrderCreation.SizeRange;
                vm.StandardPrice = buyerOrderCreation.StandardPrice;
                vm.ComplexitityFactor = buyerOrderCreation.ComplexitityFactor;
                vm.SketchNo = buyerOrderCreation.SketchNo;
                vm.LeatherMainRawMaterial = buyerOrderCreation.LeatherMainRawMaterial;
                vm.ProductColor = buyerOrderCreation.ProductColor;
                vm.BuyerStyleNo = buyerOrderCreation.BuyerStyleNo;
                vm.IsDeleted = buyerOrderCreation.IsDeleted;
            }
            return PartialView("Partial/BuyerOrderCreationDetails", vm);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult BuyerOrderCreation(BuyerOrderCreationModel model)
        {
            BuyerOrderCreation buyerOrderCreations = new BuyerOrderCreation();
            BuyerOrderCreation buyerOrderCreation = new BuyerOrderCreation();
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            Alert alert = new Alert();
            buyerOrderCreation = buyerOrderCreationManager.GetMaterialCode(model.MaterialCode);           
            buyerOrderCreations.MaterialCode = model.MaterialCode;
            buyerOrderCreations.BuyerOrderCreationID = model.BuyerOrderCreationID;
            buyerOrderCreations.MaterialCode = model.MaterialCode;
            buyerOrderCreations.MaterialName = model.MaterialName;
            buyerOrderCreations.StockUnit = model.StockUnit;
            buyerOrderCreations.SizeRange = model.SizeRange;
            buyerOrderCreations.BuyerId = model.BuyerId;
            buyerOrderCreations.StandardPrice = model.StandardPrice;
            buyerOrderCreations.ComplexitityFactor = model.ComplexitityFactor;
            buyerOrderCreations.SketchNo = model.SketchNo;
            buyerOrderCreations.LeatherMainRawMaterial = model.LeatherMainRawMaterial;
            buyerOrderCreations.ProductColor = model.ProductColor;
            buyerOrderCreations.BuyerStyleNo = model.BuyerStyleNo;
            buyerOrderCreations.IsDeleted = model.IsDeleted;
            if (model.BuyerOrderCreationID == 0 && buyerOrderCreation == null)
            {
                buyerOrderCreationManager.Post(buyerOrderCreations);
                alert.Status = "Saved";
            }
            else if(model.BuyerOrderCreationID!=0&&buyerOrderCreation!=null)
            {
                buyerOrderCreationManager.Post(buyerOrderCreations);
                alert.Status = "Updated";
            }
            else if (model.BuyerOrderCreationID == 0 && buyerOrderCreation != null)
            {
                alert.Status = "Already Existed";
            }
            return Json(alert, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int ConveyorMasterId)
        {
            BuyerOrderCreation buyerOrderCreations = new BuyerOrderCreation();
            string status = "";
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            buyerOrderCreations = buyerOrderCreationManager.GetBuyerOrderCreationID(ConveyorMasterId);
            if (buyerOrderCreations != null)
            {
                status = "Success";
                buyerOrderCreationManager.Delete(buyerOrderCreations.BuyerOrderCreationID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<BuyerOrderCreation> buyerOrderCreationList = new List<BuyerOrderCreation>();
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            buyerOrderCreationList = buyerOrderCreationManager.Get();
            buyerOrderCreationList = buyerOrderCreationList.Where(x => x.MaterialCode.ToLower().Contains(filter.ToLower()) || x.MaterialName.ToLower().Contains(filter.ToLower())).ToList();
            BuyerOrderCreationModel model = new BuyerOrderCreationModel();
            model.buyerOrderCreationList = buyerOrderCreationList;
            return PartialView("Partial/ConveyorGrid", model);
        }

        public ActionResult GetBuyerOrderCreationID(int BuyerOrderCreationID)
        {
            BuyerOrderCreation buyerOrderCreations = new BuyerOrderCreation();
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            MaterialMaster materialMaster = new MaterialMaster();
            Product_BuyerStyleManager Manager = new Product_BuyerStyleManager();
            Product_BuyerStyleMaster Master = new Product_BuyerStyleMaster();
            Product_BuyerStyleModel model = new Product_BuyerStyleModel();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
            Master = Manager.GetProductOrBuyerStyleId(BuyerOrderCreationID);
            if (Master != null)
            {
                  materialMaster = buyerOrderCreationManager.GetMaterialMasterID(Convert.ToInt32(Master.LeatherName_1));
                if (materialMaster != null)
                {
                    buyerOrderCreations.LeatherMainRawMaterial = materialMaster.MaterialMasterId;
                }
            }
            return Json(Master, JsonRequestBehavior.AllowGet);
        }
        public string GetMasterID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateConveyorID();
            ID++;
            return ID.ToString();
        }
        #endregion
    }
}
