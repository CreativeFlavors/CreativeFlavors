using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
      [CustomFilter]
    public class MaterialReservationFormController : Controller
    {
        //
        // GET: /MaterialReservationForm/
        #region Helper Method
        [HttpGet]
        public ActionResult MaterialReservation()
        {
            MaterialReservationModel model = new MaterialReservationModel();
            return View("~/Views/Stock/MaterialReservation/MaterialReservation.cshtml", model);
        }

        [HttpPost]
        public ActionResult MaterialReservationDetails(int MaterialReservationId)
        {
            MaterialReservationModel model = new MaterialReservationModel();
            MaterialReservationManager materialReservationManager = new MaterialReservationManager();
            MaterialReservation arg = new MaterialReservation();
            arg = materialReservationManager.GetMaterialResvId(MaterialReservationId);
            if (arg.MaterialReservationId != 0)
            {
                arg = materialReservationManager.GetMaterialResvId(MaterialReservationId);
                model.MaterialReservationId = arg.MaterialReservationId;
                model.DocNo = arg.DocNo;
                model.InternalOrder = arg.InternalOrder;
                model.PlanWise = arg.PlanWise;
                model.MaterialCategoryId = arg.MaterialCategoryId;
                model.MaterialGroupId = arg.MaterialGroupId;
                model.MaterialMasterId = arg.MaterialMasterId;
                model.UomId = arg.UomId;
                model.ColourMasterId = arg.ColourMasterId;
                model.Quantity = arg.Quantity;
                model.FreeStock = arg.FreeStock;
                model.MaterialWise = arg.MaterialWise;
                model.Summary = arg.Summary;
                model.MultipleIO = arg.MultipleIO;
                model.DisplayIO = arg.DisplayIO;
                model.CreatedDate = arg.CreatedDate;
                model.UpdatedDate = arg.UpdatedDate;
                model.CreatedBy = arg.CreatedBy;
                model.UpdatedBy = arg.UpdatedBy;
            }
            return PartialView("~/Views/Stock/MaterialReservation/Partial/MaterialReservationDetails.cshtml", model);

        }

        public ActionResult MaterialReservationGrid()
        {
            MaterialReservationModel mr = new MaterialReservationModel();
            MaterialReservationManager materialReservationManager = new MaterialReservationManager();
            mr.MaterialReservationList = materialReservationManager.Get();

            return PartialView("~/Views/Stock/MaterialReservation/Partial/MaterialReservationGrid.cshtml", mr);
        }

        public ActionResult Search(string filter)
        {
            List<Core.Entities.Stock.MaterialReservation> materialReservationList = new List<Core.Entities.Stock.MaterialReservation>();
            MaterialReservationManager materialReservationManager = new MaterialReservationManager();
            List<materialgroupmaster> materialGroupMasterList = new List<materialgroupmaster>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialGroupMasterList = materialGroupManager.Get();
            materialReservationList = materialReservationManager.Get();
   var matReservation =         from X in materialGroupMasterList
            join Y in materialReservationList on X.MaterialGroupMasterId equals Y.MaterialGroupId
            where (X.GroupName.ToLower().Trim().Contains(filter.ToLower().Trim()) || Y.MaterialMasterId.ToString().Trim().Contains(filter.Trim()))
            select Y;
            materialReservationList = matReservation.ToList(); 
           
            MaterialReservationModel model = new MaterialReservationModel();
            model.MaterialReservationList = materialReservationList;
            return PartialView("~/Views/Stock/MaterialReservation/Partial/MaterialReservationGrid.cshtml", model); 
        }


        #endregion

        
        #region Crud operation
        [HttpPost]
        public ActionResult MaterialReservation(MaterialReservationModel model)
        {
            MaterialReservationManager materialReservationManager = new MaterialReservationManager();
            MaterialReservation materialReservation = new MaterialReservation();

            materialReservation.MaterialReservationId = model.MaterialReservationId;
            materialReservation.DocNo = model.DocNo;
            materialReservation.InternalOrder = model.InternalOrder;
            materialReservation.PlanWise = model.PlanWise;
            materialReservation.MaterialCategoryId = model.MaterialCategoryId;
            materialReservation.MaterialGroupId = model.MaterialGroupId;
            materialReservation.MaterialMasterId = model.MaterialMasterId;
            materialReservation.UomId = model.UomId;
            materialReservation.ColourMasterId = model.ColourMasterId;
            materialReservation.Quantity = model.Quantity;
            materialReservation.FreeStock = model.FreeStock;
            materialReservation.MaterialWise = model.MaterialWise;
            materialReservation.Summary = model.Summary;
            materialReservation.MultipleIO = "";
            materialReservation.DisplayIO = model.DisplayIO;
            materialReservation.CreatedDate = DateTime.Now;
            materialReservationManager.Post(materialReservation);

            return Json(materialReservation, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(MaterialReservationModel model)
        {
            MaterialReservationManager materialReservationManager = new MaterialReservationManager();
            MaterialReservation materialReservation = new MaterialReservation();
            materialReservation = materialReservationManager.GetMaterialResvId(model.MaterialReservationId);
            if (materialReservation != null)
            {
                materialReservation.MaterialReservationId = model.MaterialReservationId;
                materialReservation.DocNo = model.DocNo;
                materialReservation.InternalOrder = model.InternalOrder;
                materialReservation.PlanWise = model.PlanWise;
                materialReservation.MaterialCategoryId = model.MaterialCategoryId;
                materialReservation.MaterialGroupId = model.MaterialGroupId;
                materialReservation.MaterialMasterId = model.MaterialMasterId;
                materialReservation.UomId = model.UomId;
                materialReservation.ColourMasterId = model.ColourMasterId;
                materialReservation.Quantity = model.Quantity;
                materialReservation.FreeStock = model.FreeStock;
                materialReservation.MaterialWise = model.MaterialWise;
                materialReservation.Summary = model.Summary;
                materialReservation.MultipleIO = model.MultipleIO;
                materialReservation.DisplayIO = model.DisplayIO;
                materialReservation.CreatedDate = materialReservation.CreatedDate;
                materialReservation.UpdatedDate = DateTime.Now;
                materialReservationManager.Put(materialReservation);
            }
             else
                {
                    return Json(materialReservation, JsonRequestBehavior.AllowGet);
                }
            return Json(materialReservation, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int MaterialReservationId)
        {
            MaterialReservationManager materialReservationManager = new MaterialReservationManager();
            string status = "";
            MaterialReservation materialReservation = new MaterialReservation();
            materialReservation = materialReservationManager.GetMaterialResvId(MaterialReservationId);
            if (materialReservation != null)
            {
                status = "Success";
                materialReservationManager.Delete(materialReservation.MaterialReservationId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSelectedIOVal(int MaterialReservationId)
        {
            MaterialReservationManager materialReservationManager = new MaterialReservationManager();
            MaterialReservationModel model = new MaterialReservationModel();
            MMS.Core.Entities.Stock.MaterialReservation materialReservation = new Core.Entities.Stock.MaterialReservation();

            SelectList items = MMS.Web.ExtensionMethod.HtmlHelper.GetInternalOrderIONO();

            materialReservation = materialReservationManager.GetMaterialResvId(MaterialReservationId);
            model.DocNo = materialReservation.DocNo;
            model.DisplayIO = materialReservation.DisplayIO;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
