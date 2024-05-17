using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.ProductionBOM;
using MMS.Core.Entities.Stock;
using MMS.Core.Entities;
using System.EnterpriseServices;
using MMS.Web.Models.Product;
using MMS.Core.Entities.GateEntry;

namespace MMS.Web.Controllers
{
    public class ProductionBOMController : Controller
    {
        //
        // GET: /ProductionBOM/
        [HttpGet]
        public ActionResult ProductionBOMGrid()
        {
            ProductionBOMModel objbom = new ProductionBOMModel();
            return View(objbom);
        }
        [HttpGet]
        public ActionResult ProductionBOMDetails()
        {
            ProductionBOMModel objbom = new ProductionBOMModel();
            return View("Partial/ProductionBOMDetails", objbom);
        }
        [HttpGet]
        public ActionResult GetBOMMaterialList(int BomId)
        {
            List<tbl_materialnamemaster> List = new List<tbl_materialnamemaster>();
            BillOfMaterialManager managerBOM = new BillOfMaterialManager();
            MaterialNameManager mangerMaterialName = new MaterialNameManager();
            //List = mang.GetBomMaterialBOMIDetails(BomId);
            //var query = from b in managerBOM.Get()
            //            join t in mangerMaterialName.Get() on b.MaterialMasterId equals t.MaterialMasterID into tempJoin
            //            from t in tempJoin.DefaultIfEmpty()
            //            where b.BomId == BomId
            //            select new { t.MaterialMasterID, t.MaterialDescription };
            var query = from b in managerBOM.Get()
                        join t in mangerMaterialName.Get() on b.MaterialMasterId equals t.MaterialMasterID into tempJoin
                        from t in tempJoin.DefaultIfEmpty()
                        where b.BomId == BomId
                        select new
                        {
                            MaterialMasterID = t != null ? t.MaterialMasterID : 0, // Default value if t is null
                            MaterialDescription = t != null ? t.MaterialDescription : "" // Default value if t is null
                        };


            query.Select(x => (x.MaterialMasterID, x.MaterialDescription));

            return Json(query, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSubassemblyMasterList()
        {
            List<SubAssemblyMaster> List = new List<SubAssemblyMaster>();
            SubAssemblyMasterManagers managerSub = new SubAssemblyMasterManagers();
            List = managerSub.GetSubAssemblyMaster();
            return Json(List, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ProductionBOMDetailsDataInsert(ProductionBOMModel model)
        {
            string[] materialsArray = null;
            ProductionBOM PBomEntity = new ProductionBOM();
            string status = "";

            ProductionBOMManager PBOMManager = new ProductionBOMManager();
            if (model.Id == 0 && model.Process == 1)
            {
                materialsArray = model.materiallist.Split(',');
            }
            else
            {
                materialsArray = model.consumelist.Split(',');
            }
            for (int i = 0; i < materialsArray.Length; i++)
            {
                if (model.Id == 0 && model.Process == 1)
                {
                    PBomEntity.MaterialId = Convert.ToInt32(materialsArray[i]);
                    PBomEntity.SubAssemblyId = 0;
                }
                else
                {
                    PBomEntity.SubAssemblyId = Convert.ToInt32(materialsArray[i]);
                    PBomEntity.MaterialId = 0;
                }
                PBomEntity.Id = model.Id;
                PBomEntity.BOMProductId = model.BOMProductId;
                PBomEntity.Bomid = model.Bomid;
                PBomEntity.BOMProductName = model.BOMProductName;
                PBomEntity.BOMProductCode = model.BOMProductCode;
                PBomEntity.MaterialCategoryMasterid = model.MaterialCategoryMasterid;
                PBomEntity.ProductType = model.ProductType;
                PBomEntity.UomIdMaster = model.UomIdMaster;
                PBomEntity.Description = model.Description;
                PBomEntity.Qty = model.Qty;
                PBomEntity.ConsumeunitId = model.UomIdMaster;
                PBomEntity.IsActive = true;
                PBomEntity.CreatedDate = DateTime.Now;
                PBomEntity.CreatedBy = model.CreatedBy;
                PBomEntity.Process = model.Process;
                PBOMManager.Post(PBomEntity);
                status = "Inserted";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
