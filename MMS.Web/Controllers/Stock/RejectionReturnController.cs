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

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class RejectionReturnController : Controller
    {
        //
        // GET: /RejectionReturn/

        #region Rejection Return View
        [HttpGet]
        public ActionResult RejectionReturnForm()
        {
            RejectionReturnModel model = new RejectionReturnModel();
            return View("~/Views/Stock/RejectionReturn/RejectionReturnForm.cshtml", model);
        }

        public ActionResult RejectionReturnGrid()
        {
            RejectionReturnModel model = new RejectionReturnModel();
            RejectionReturnManager rejectionReturnManager = new RejectionReturnManager();
            model.RejectionReturnSupList = rejectionReturnManager.Get();

            return PartialView("~/Views/Stock/RejectionReturn/Partial/RejectionReturnGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult RejectionReturnDetails(int RejectionReturnId)
        {
            RejectionReturnSup rejectionReturnSup = new RejectionReturnSup();
            RejectionReturnManager rejectionReturnManager = new RejectionReturnManager();
            RejectionReturnModel model = new RejectionReturnModel();
            rejectionReturnSup = rejectionReturnManager.GetRejectionReturnId(RejectionReturnId);

            if (rejectionReturnSup.RejectionReturnId != 0)
            {
                model.RejectionReturnId = rejectionReturnSup.RejectionReturnId;
                model.RejectionDcNo = rejectionReturnSup.RejectionDcNo;
                model.GrnNo = rejectionReturnSup.GrnNo;
                model.Date = Convert.ToDateTime(rejectionReturnSup.Date).Date.ToString("dd/MM/yyyy");
                model.PoNo = rejectionReturnSup.PoNo;
                model.SupplierMasterId = rejectionReturnSup.SupplierMasterId;
                model.IMIRNo = rejectionReturnSup.IMIRNo;
                model.MaterialGroupId = rejectionReturnSup.MaterialGroupId;
                model.Uom = rejectionReturnSup.Uom;
                model.MaterialMasterId = rejectionReturnSup.MaterialMasterId;
                model.Quantity = rejectionReturnSup.Quantity;
                model.ColourMasterId = rejectionReturnSup.ColourMasterId;
                model.Rate = rejectionReturnSup.Rate;
                model.Remarks = rejectionReturnSup.Remarks;
                model.Pcs = rejectionReturnSup.Pcs;
                model.AmountTotal = rejectionReturnSup.AmountTotal;
                model.GatePassDc = rejectionReturnSup.GatePassDc;
                model.CreatedDate = rejectionReturnSup.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
            }
            return PartialView("~/Views/Stock/RejectionReturn/Partial/RejectionReturnDetails.cshtml", model);
        }

        #endregion

        #region Crud Operation
        [HttpPost]
        public ActionResult RejectionReturnForm(RejectionReturnModel model)
        {
            RejectionReturnSup rejectionReturnSup = new RejectionReturnSup();
           
                RejectionReturnManager rejectionReturnManager = new RejectionReturnManager();

                rejectionReturnSup.RejectionDcNo = model.RejectionDcNo;
                rejectionReturnSup.GrnNo = model.GrnNo;
                var format = "dd/MM/yyyy";
                DateTime rejDate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                rejectionReturnSup.Date = rejDate.Date;
                rejectionReturnSup.PoNo = model.PoNo;
                rejectionReturnSup.SupplierMasterId = model.SupplierMasterId;
                rejectionReturnSup.IMIRNo = model.IMIRNo;
                rejectionReturnSup.MaterialGroupId = model.MaterialGroupId;
                rejectionReturnSup.Uom = model.Uom;
                rejectionReturnSup.MaterialMasterId = model.MaterialMasterId;
                rejectionReturnSup.Quantity = model.Quantity;
                rejectionReturnSup.ColourMasterId = model.ColourMasterId;
                rejectionReturnSup.Rate = model.Rate;
                rejectionReturnSup.Remarks = model.Remarks;
                rejectionReturnSup.Pcs = model.Pcs;
                rejectionReturnSup.AmountTotal = model.AmountTotal;
                rejectionReturnSup.GatePassDc = model.GatePassDc;
                rejectionReturnSup.CreatedDate = DateTime.Now;
                rejectionReturnManager.Post(rejectionReturnSup);

            return Json(rejectionReturnSup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(RejectionReturnModel model)
        {
            RejectionReturnSup rejectionReturnSup = new RejectionReturnSup();
            if (ModelState.IsValid)
            {
                RejectionReturnSup rejectionReturn = new RejectionReturnSup();
                RejectionReturnManager rejectionReturnManager = new RejectionReturnManager();
                rejectionReturn = rejectionReturnManager.GetRejectionReturnId(model.RejectionReturnId);

                if (rejectionReturn != null)
                {
                    rejectionReturnSup.RejectionReturnId = model.RejectionReturnId;
                    rejectionReturnSup.RejectionDcNo = model.RejectionDcNo;
                    rejectionReturnSup.GrnNo = model.GrnNo;
                    var format = "dd/MM/yyyy";
                    DateTime rejDate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    rejectionReturnSup.Date = rejDate.Date;
                    rejectionReturnSup.PoNo = model.PoNo;
                    rejectionReturnSup.SupplierMasterId = model.SupplierMasterId;
                    rejectionReturnSup.IMIRNo = model.IMIRNo;
                    rejectionReturnSup.MaterialGroupId = model.MaterialGroupId;
                    rejectionReturnSup.Uom = model.Uom;
                    rejectionReturnSup.MaterialMasterId = model.MaterialMasterId;
                    rejectionReturnSup.Quantity = model.Quantity;
                    rejectionReturnSup.ColourMasterId = model.ColourMasterId;
                    rejectionReturnSup.Rate = model.Rate;
                    rejectionReturnSup.Remarks = model.Remarks;
                    rejectionReturnSup.Pcs = model.Pcs;
                    rejectionReturnSup.AmountTotal = model.AmountTotal;
                    rejectionReturnSup.GatePassDc = model.GatePassDc;
                    rejectionReturnSup.CreatedDate = rejectionReturn.CreatedDate;
                    rejectionReturnManager.Put(rejectionReturnSup);
                }
                else 
                {
                    return Json(rejectionReturnSup, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(rejectionReturnSup, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Delete(int RejectionReturnId)
        {
            RejectionReturnSup rejectionReturnSup = new RejectionReturnSup();
            string status = "";
            RejectionReturnManager rejectionReturnManager = new RejectionReturnManager();
            rejectionReturnSup = rejectionReturnManager.GetRejectionReturnId(RejectionReturnId);
            if (rejectionReturnSup != null)
            {
                status = "Success";
                rejectionReturnManager.Delete(rejectionReturnSup.RejectionReturnId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        
        #region Helper Method

        public ActionResult Search(string filter)
        {
            List<RejectionReturnSup> rejectionReturnSuplist = new List<RejectionReturnSup>();
            RejectionReturnManager rejectionReturnManager = new RejectionReturnManager();
            rejectionReturnSuplist = rejectionReturnManager.Get();
            rejectionReturnSuplist = rejectionReturnSuplist.Where(s => s.GrnNo.ToLower().Trim().Contains(filter.ToLower().Trim()) || s.PoNo.ToString().ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            RejectionReturnModel model = new RejectionReturnModel();
            model.RejectionReturnSupList = rejectionReturnSuplist;
            return PartialView("~/Views/Stock/RejectionReturn/Partial/RejectionReturnGrid.cshtml", model);
        }
        #endregion
    }
}
