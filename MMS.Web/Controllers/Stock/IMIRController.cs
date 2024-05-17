using MMS.Common;
using MMS.Core.Entities.Stock;
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
    public class IMIRController : Controller
    {
        //
        // GET: /IMIR/
        #region IMIR Form View

        [HttpGet]
        public ActionResult Imir()
        {
            ImirModel model = new ImirModel();
            return View("~/Views/Stock/IMIR/Imir.cshtml", model);
        }

        public ActionResult ImirGrid()
        {
            ImirModel model = new ImirModel();
            ImirManager imirManager = new ImirManager();
            model.ImirFormList = imirManager.Get();

            return PartialView("~/Views/Stock/IMIR/Partial/ImirGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult ImirDetails(int ImirId)
        {
            ImirModel model = new ImirModel();
            ImirForm imirForm = new ImirForm();
            ImirManager imirManager = new ImirManager();
            imirForm = imirManager.GetImirId(ImirId);

            if (imirForm.ImirId != 0)
            {
                model.ImirId = imirForm.ImirId;
                model.ReportNo = imirForm.ReportNo;
                model.Date = imirForm.Date;
                model.GateEntryNo = imirForm.GateEntryNo;
                model.RefInfrepNo = imirForm.RefInfrepNo;
                model.GrnNo = imirForm.GrnNo;
                model.DcNo = imirForm.DcNo;
                model.SupplierMasterId = imirForm.SupplierMasterId;
                model.MaterialMasterId = imirForm.MaterialMasterId;
                model.ColourMasterId = imirForm.ColourMasterId;
                model.Store = imirForm.Store;
                model.Uom = imirForm.Uom;
                model.InspectionType = imirForm.InspectionType;
                model.DcQty = imirForm.DcQty;
                model.ReceivedQty = imirForm.ReceivedQty;
                model.DcPcs = imirForm.DcPcs;
                model.ReceivedPcs = imirForm.ReceivedPcs;
                model.QtyInspected = imirForm.QtyInspected;
                model.QtyAccepted = imirForm.QtyAccepted;
                model.QtyRejected = imirForm.QtyRejected;
                model.QtyRejectionPercent = imirForm.QtyRejectionPercent;
                model.QtyExcess = imirForm.QtyExcess;
                model.PcsInspected = imirForm.PcsInspected;
                model.PcsAccepted = imirForm.PcsAccepted;
                model.PcsRejected = imirForm.PcsRejected;
                model.PcsRejectionPercent = imirForm.PcsRejectionPercent;
                model.PcsExcess = imirForm.PcsExcess;
                model.Remarks = imirForm.Remarks;
                model.InspectedBy = imirForm.InspectedBy;
                model.QcExcecutive = imirForm.QcExcecutive;
                model.CreatedDate = imirForm.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
            }

            return PartialView("~/Views/Stock/IMIR/Partial/ImirDetails.cshtml", model);
        }

        #endregion

        #region Crud Operations

        [HttpPost]
        public ActionResult Imir(ImirModel model, string Grid)
        {
            string Message = "";
            ImirGridDetails imirGrid = new ImirGridDetails();
            ImirForm imirForm = new ImirForm();
            ImirModel gridModel = new ImirModel();
            if (ModelState.IsValid)
            {
                ImirManager imirManager = new ImirManager();

                imirForm.ReportNo = model.ReportNo;
                imirForm.Date = model.Date;
                imirForm.GateEntryNo = model.GateEntryNo;
                imirForm.RefInfrepNo = model.RefInfrepNo;
                imirForm.GrnNo = model.GrnNo;
                imirForm.DcNo = model.DcNo;
                imirForm.SupplierMasterId = model.SupplierMasterId;
                imirForm.MaterialMasterId = model.MaterialMasterId;
                imirForm.ColourMasterId = model.ColourMasterId;
                imirForm.Store = model.Store;
                imirForm.Uom = model.Uom;
                imirForm.InspectionType = model.InspectionType;
                imirForm.DcQty = model.DcQty;
                imirForm.ReceivedQty = model.ReceivedQty;
                imirForm.DcPcs = model.DcPcs;
                imirForm.ReceivedPcs = model.ReceivedPcs;
                imirForm.QtyInspected = model.QtyInspected;
                imirForm.QtyAccepted = model.QtyAccepted;
                imirForm.QtyRejected = model.QtyRejected;
                imirForm.QtyRejectionPercent = model.QtyRejectionPercent;
                imirForm.QtyExcess = model.QtyExcess;
                imirForm.PcsInspected = model.PcsInspected;
                imirForm.PcsAccepted = model.PcsAccepted;
                imirForm.PcsRejected = model.PcsRejected;
                imirForm.PcsRejectionPercent = model.PcsRejectionPercent;
                imirForm.PcsExcess = model.PcsExcess;
                imirForm.Remarks = model.Remarks;
                imirForm.InspectedBy = model.InspectedBy;
                imirForm.QcExcecutive = model.QcExcecutive;
                imirForm.CreatedDate = DateTime.Now;
                imirForm.UpdatedDate = DateTime.Now;

                imirManager.Post(imirForm);


                List<ImirGridDetails> imirGridDetails = new List<ImirGridDetails>();
                model.ImirGridDetailslist = imirManager.GridList().Where(x => x.ImirId == imirForm.ImirId).Select(x => x.ImirGridId).ToList();
                string GridID = "";
                foreach (var imir in model.ImirGridDetailslist)
                {
                    GridID += imir + ",";
                }
                string[] strArray = Grid.Trim().Split('#');
                strArray = strArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int count = 0;
                foreach (string obj in strArray)
                {
                    string[] strRow = obj.Split(',');
                    string[] ImirGridId = GridID.Split(',');
                    if (strRow[0].ToString() != "")
                    {
                        if (model.ImirGridDetailslist.Count > 0 && ImirGridId[count] != "")
                        {
                            imirGrid.ImirGridId = Convert.ToInt16(ImirGridId[count].ToString());
                            count++;
                        }
                        else
                        {
                            imirGrid.ImirGridId = gridModel.ImirGridId;
                        }
                        imirGrid.ImirId = imirForm.ImirId;
                        imirGrid.SlNo = strRow[0].ToString();
                        imirGrid.ParameterId = strRow[1].ToString();
                        imirGrid.Parameter = strRow[2].ToString();
                        imirGrid.InspectionFrequency = strRow[3].ToString();
                        imirGrid.RejectionQty = strRow[4].ToString();
                        imirGrid.GridRemarks = strRow[5].ToString();
                        imirGrid.CreatedDate = DateTime.Now;
                        imirGrid.UpdatedDate = DateTime.Now;
                        imirManager.Post(imirGrid);
                        Message = "Saved Successfully"; 
                    }
                }
            }
            return Json(imirForm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(ImirModel model, string Grid)
        {
            string Message = "";
            ImirGridDetails imirGrid = new ImirGridDetails();
            ImirForm imirForm = new ImirForm();
            ImirModel gridModel = new ImirModel();

            if (ModelState.IsValid)
            {
                ImirForm imir = new ImirForm();
                ImirManager imirManager = new ImirManager();
                imir = imirManager.GetImirId(model.ImirId);
                if (imirForm != null)
                {
                    imirForm.ImirId = model.ImirId;
                    imirForm.ReportNo = model.ReportNo;
                    imirForm.Date = model.Date;
                    imirForm.GateEntryNo = model.GateEntryNo;
                    imirForm.RefInfrepNo = model.RefInfrepNo;
                    imirForm.GrnNo = model.GrnNo;
                    imirForm.DcNo = model.DcNo;
                    imirForm.SupplierMasterId = model.SupplierMasterId;
                    imirForm.MaterialMasterId = model.MaterialMasterId;
                    imirForm.ColourMasterId = model.ColourMasterId;
                    imirForm.Store = model.Store;
                    imirForm.Uom = model.Uom;
                    imirForm.InspectionType = model.InspectionType;
                    imirForm.DcQty = model.DcQty;
                    imirForm.ReceivedQty = model.ReceivedQty;
                    imirForm.DcPcs = model.DcPcs;
                    imirForm.ReceivedPcs = model.ReceivedPcs;
                    imirForm.QtyInspected = model.QtyInspected;
                    imirForm.QtyAccepted = model.QtyAccepted;
                    imirForm.QtyRejected = model.QtyRejected;
                    imirForm.QtyRejectionPercent = model.QtyRejectionPercent;
                    imirForm.QtyExcess = model.QtyExcess;
                    imirForm.PcsInspected = model.PcsInspected;
                    imirForm.PcsAccepted = model.PcsAccepted;
                    imirForm.PcsRejected = model.PcsRejected;
                    imirForm.PcsRejectionPercent = model.PcsRejectionPercent;
                    imirForm.PcsExcess = model.PcsExcess;
                    imirForm.Remarks = model.Remarks;
                    imirForm.InspectedBy = model.InspectedBy;
                    imirForm.QcExcecutive = model.QcExcecutive;
                    imirForm.CreatedDate = imir.CreatedDate;
                    imirForm.UpdatedDate = DateTime.Now;

                    imirManager.Put(imirForm);

                    List<ImirGridDetails> imirGridDetails = new List<ImirGridDetails>();
                    model.ImirGridDetailslist = imirManager.GridList().Where(x => x.ImirId == imirForm.ImirId).Select(x => x.ImirGridId).ToList();
                    string GridID = "";
                    foreach (var imir2 in model.ImirGridDetailslist)
                    {
                        GridID += imir2 + ",";
                    }
                    string[] strArray = Grid.Trim().Split('#');
                    strArray = strArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    int count = 0;
                    foreach (string obj in strArray)
                    {
                        string[] strRow = obj.Split(',');
                        string[] ImirGridId = GridID.Split(',');
                        if (strRow[0].ToString() != "")
                        {
                            if (model.ImirGridDetailslist.Count > 0 && ImirGridId[count] != "")
                            {
                                imirGrid.ImirGridId = Convert.ToInt16(ImirGridId[count].ToString());
                                count++;
                            }
                            else
                            {
                                imirGrid.ImirGridId = gridModel.ImirGridId;
                            }
                            imirGrid.ImirId = imirForm.ImirId;
                            imirGrid.SlNo = strRow[0].ToString();
                            imirGrid.ParameterId = strRow[1].ToString();
                            imirGrid.Parameter = strRow[2].ToString();
                            imirGrid.InspectionFrequency = strRow[3].ToString();
                            imirGrid.RejectionQty = strRow[4].ToString();
                            imirGrid.GridRemarks = strRow[5].ToString();
                            imirGrid.CreatedDate = DateTime.Now;
                            imirGrid.UpdatedDate = DateTime.Now;
                            imirManager.Post(imirGrid);
                            Message = "Updated Successfully";
                        }
                    }
                }
                else 
                {
                    return Json(imirForm, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(imirForm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int ImirId)
        {
            ImirForm imirForm = new ImirForm();
            string status = "";
            ImirManager imirManager = new ImirManager();
            imirForm = imirManager.GetImirId(ImirId);
            if (imirForm != null)
            {
                status = "Success";
                imirManager.Delete(imirForm.ImirId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Method

        public ActionResult Search(string Filter)
        {
            List<ImirForm> imirForm = new List<ImirForm>();
            ImirManager imirManager = new ImirManager();
            imirForm = imirManager.Get();
            ImirModel model = new ImirModel();
            model.ImirFormList = imirForm;
            return PartialView("Partial/ImirGrid", model);
        }

        public ActionResult GridDelete(int ImirGridId)
        {
            ImirManager imirManager = new ImirManager();
            string status = "";
            ImirGridDetails imirGridDetails = new ImirGridDetails();
            imirGridDetails = imirManager.GetImirGridId(ImirGridId);
            if (imirGridDetails != null)
            {
                status = "Success";
                imirManager.ImirGridDelete(imirGridDetails.ImirGridId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
