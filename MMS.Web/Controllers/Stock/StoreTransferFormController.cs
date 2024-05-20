using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Core.Entities;
using MMS.Repository.Managers;
namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class StoreTransferFormController : Controller
    {
        //
        // GET: /StoreTransfer/

        #region Store Transfer View

        [HttpGet]
        public ActionResult StoreTransfer()
        {
            StoreTransferModel model = new StoreTransferModel();
            return View("~/Views/Stock/StoreTransfer/StoreTransfer.cshtml", model);
        }

        public ActionResult StoreTransferGrid()
        {
            StoreTransferModel model = new StoreTransferModel();
            StoreTransferManager storeTransferManager = new StoreTransferManager();
            model.storeTransferList = storeTransferManager.Get();

            return PartialView("~/Views/Stock/StoreTransfer/Partial/StoreTransferGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult StoreTransferDetails(int StoreTransferId)
        {
            StoreTransferModel model = new StoreTransferModel();
            StoreTransfer storeTransfer = new StoreTransfer();
            StoreTransferManager storeTransferManager = new StoreTransferManager();
            storeTransfer = storeTransferManager.GetstoreTransferId(StoreTransferId);

            if (storeTransfer.StoreTransferId != 0)
            {
                model.StoreTransferId = storeTransfer.StoreTransferId;
                model.TrnNo = storeTransfer.TrnNo;
                model.Date = Convert.ToDateTime(storeTransfer.Date).Date.ToString("dd/MM/yyyy");
                model.FromStores = Convert.ToDateTime(storeTransfer.FromStores).Date.ToString("dd/MM/yyyy");
                model.ToStores = Convert.ToDateTime(storeTransfer.ToStores).Date.ToString("dd/MM/yyyy"); 
                model.MaterialCategoryId = storeTransfer.MaterialCategoryId;
                model.MaterialGroupId = storeTransfer.MaterialGroupId;
                model.MaterialMasterId = storeTransfer.MaterialMasterId;
                model.ColourMasterId = storeTransfer.ColourMasterId;
                model.Description = storeTransfer.Description;
                model.Rate = storeTransfer.Rate;
                model.ClosingInAllStores = storeTransfer.ClosingInAllStores;
                model.ClosingInCurrentStores = storeTransfer.ClosingInCurrentStores;
                model.CreatedDate = storeTransfer.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
            }
            return PartialView("~/Views/Stock/StoreTransfer/Partial/StoreTransferDetails.cshtml", model);
        }

        #endregion

        #region Crud Operations

        [HttpPost]
        public ActionResult StoreTransfer(StoreTransferModel model)
        {
            StoreTransfer storeTransfer = new StoreTransfer();
            StoreTransferManager storeTransferManager = new StoreTransferManager();

            storeTransfer.TrnNo = model.TrnNo;
            var format = "dd/MM/yyyy";
            DateTime STdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
            DateTime STfromstore = DateTime.ParseExact(model.FromStores, format, CultureInfo.InvariantCulture);
            DateTime STtostore = DateTime.ParseExact(model.ToStores, format, CultureInfo.InvariantCulture);
            storeTransfer.Date = STdate.Date;
            storeTransfer.FromStores = STfromstore.Date;
            storeTransfer.ToStores = STtostore.Date;
            storeTransfer.MaterialCategoryId = model.MaterialCategoryId;
            storeTransfer.MaterialGroupId = model.MaterialGroupId;
            storeTransfer.MaterialMasterId = model.MaterialMasterId;
            storeTransfer.ColourMasterId = model.ColourMasterId;
            storeTransfer.Description = model.Description;
            storeTransfer.Rate = model.Rate;
            storeTransfer.ClosingInAllStores = model.ClosingInAllStores;
            storeTransfer.ClosingInCurrentStores = model.ClosingInCurrentStores;
            storeTransfer.CreatedDate = DateTime.Now;
            storeTransferManager.Post(storeTransfer);

            return Json(storeTransfer, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Update(StoreTransferModel model)
        {
            StoreTransfer storeTransfer = new StoreTransfer();
            if (ModelState.IsValid)
            {
                StoreTransfer storeTransf = new StoreTransfer();
                StoreTransferManager storeTransferManager = new StoreTransferManager();
                storeTransf = storeTransferManager.GetstoreTransferId(model.StoreTransferId);
                if (storeTransf != null)
                {
                    storeTransfer.StoreTransferId = model.StoreTransferId;
                    storeTransfer.TrnNo = model.TrnNo;
                    var format = "dd/MM/yyyy";
                    DateTime STdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    DateTime STfromstore = DateTime.ParseExact(model.FromStores, format, CultureInfo.InvariantCulture);
                    DateTime STtostore = DateTime.ParseExact(model.ToStores, format, CultureInfo.InvariantCulture);
                    storeTransfer.Date = STdate.Date;
                    storeTransfer.FromStores = STfromstore.Date;
                    storeTransfer.ToStores = STtostore.Date;
                    storeTransfer.MaterialCategoryId = model.MaterialCategoryId;
                    storeTransfer.MaterialGroupId = model.MaterialGroupId;
                    storeTransfer.MaterialMasterId = model.MaterialMasterId;
                    storeTransfer.ColourMasterId = model.ColourMasterId;
                    storeTransfer.Description = model.Description;
                    storeTransfer.Rate = model.Rate;
                    storeTransfer.ClosingInAllStores = model.ClosingInAllStores;
                    storeTransfer.ClosingInCurrentStores = model.ClosingInCurrentStores;
                    storeTransfer.CreatedDate = storeTransf.CreatedDate;
                    storeTransferManager.Put(storeTransfer);
                }
                return Json(storeTransfer, JsonRequestBehavior.AllowGet);
            }
            return Json(storeTransfer, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int StoreTransferId)
        {
            StoreTransfer storeTransfer = new StoreTransfer();
            string status = "";
            StoreTransferManager storeTransferManager = new StoreTransferManager();
            storeTransfer = storeTransferManager.GetstoreTransferId(StoreTransferId);
            if (storeTransfer != null)
            {
                status = "Success";
                storeTransferManager.Delete(storeTransfer.StoreTransferId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Method

        public ActionResult Search(string filter)
        {
            List<StoreTransfer> storeTransferlist = new List<StoreTransfer>();
            StoreTransferManager storeTransferManager = new StoreTransferManager();

            List<MaterialCategoryMaster> materialCategoryMasterList = new List<MaterialCategoryMaster>();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();

            List<materialgroupmaster> materialGroupMasterList = new List<materialgroupmaster>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            storeTransferlist = storeTransferManager.Get();
            materialCategoryMasterList = materialCategoryManager.Get();
            materialGroupMasterList = materialGroupManager.Get();



            var storeTranslist = from X in storeTransferlist
                                 join Y in materialCategoryMasterList on X.MaterialCategoryId equals Y.MaterialCategoryMasterId
                                 join Z in materialGroupMasterList on X.MaterialGroupId equals Z.MaterialGroupMasterId
                                 where (Y.CategoryName.ToLower().Trim().Contains(filter.ToLower().Trim()) || Z.GroupName.ToLower().Trim().Contains(filter.ToLower().Trim()))
                                 select X;

            storeTransferlist =  storeTranslist.ToList(); 
            StoreTransferModel model = new StoreTransferModel();
            model.storeTransferList = storeTransferlist;

            return PartialView("~/Views/Stock/StoreTransfer/Partial/StoreTransferGrid.cshtml", model);
        }

        #endregion

    }
}
