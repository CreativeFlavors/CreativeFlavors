using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SizeScheduleDetailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SizeScheduleDetailsController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SizeScheduleDetails()
        {
            SizeScheduleDetailsModel vm = new SizeScheduleDetailsModel();
            return View(vm);
        }
        public ActionResult SizeScheduleDetailsGrid()
        {
            SizeScheduleDetailsModel vm = new SizeScheduleDetailsModel();
            SizeScheduledDetailManager sizeScheduledDetailManager = new SizeScheduledDetailManager();
            vm.SizeScheduleDetailsList = sizeScheduledDetailManager.Get();

            return PartialView("Partial/SizeScheduleDetailsGrid", vm);
        }
        [HttpPost]
        public ActionResult ViewSizeSchedule(int SizeScheduleDetailsId)
        {
            SizeScheduledDetailManager sizeScheduledDetailManager = new SizeScheduledDetailManager();
            SizeScheduleDetails commisionCriteria = new SizeScheduleDetails();
            SizeScheduleDetailsModel model = new SizeScheduleDetailsModel();
            commisionCriteria = sizeScheduledDetailManager.GetSizeScheduleDetailsId(SizeScheduleDetailsId);
            if (commisionCriteria != null)
            {
                model.SizeScheduleDetailsId = commisionCriteria.SizeScheduleDetailsId;
                model.Size = commisionCriteria.Size;
                model.SizeNo = commisionCriteria.SizeNo;
                model.SizeScheduleMasterId = commisionCriteria.SizeScheduleMasterId;
                model.CreatedDate = commisionCriteria.CreatedDate;
                model.UpdatedDate = commisionCriteria.UpdatedDate;
                model.CreatedBy = commisionCriteria.CreatedBy;
                model.UpdatedBy = commisionCriteria.UpdatedBy;
            }
            return PartialView("Partial/ViewSizeSchedule", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SizeScheduleDetails(SizeScheduleDetailsModel model)
        {
            SizeScheduleDetails sizeScheduleDetailss = new SizeScheduleDetails();           
            SizeScheduledDetailManager sizeScheduledDetailManager = new SizeScheduledDetailManager();
            sizeScheduleDetailss.SizeScheduleDetailsId = model.SizeScheduleDetailsId;
            sizeScheduleDetailss.Size = model.Size;
            sizeScheduleDetailss.SizeNo = model.SizeNo;
            sizeScheduleDetailss.SizeScheduleMasterId = model.SizeScheduleMasterId;
            sizeScheduleDetailss.CreatedDate = DateTime.Now;
            sizeScheduleDetailss.UpdatedDate = DateTime.Now;
            sizeScheduledDetailManager.Post(sizeScheduleDetailss);
            return Json(sizeScheduleDetailss, JsonRequestBehavior.AllowGet);



        }
        [HttpPost]
        public ActionResult Update(SizeScheduleDetailsModel model)
        {
            SizeScheduleDetails sizeScheduleDetailss = new SizeScheduleDetails();
            if (ModelState.IsValid)
            {
                SizeScheduleDetails sizeScheduleDetails = new SizeScheduleDetails();
                SizeScheduledDetailManager sizeScheduledDetailManager = new SizeScheduledDetailManager();
                sizeScheduleDetails = sizeScheduledDetailManager.GetSizeScheduleDetailsId(model.SizeScheduleDetailsId);
                if (sizeScheduleDetails != null)
                {
                    sizeScheduleDetailss.SizeScheduleDetailsId = model.SizeScheduleDetailsId;
                    sizeScheduleDetailss.Size = model.Size;
                    sizeScheduleDetailss.SizeNo = model.SizeNo;
                    sizeScheduleDetailss.SizeScheduleMasterId = model.SizeScheduleMasterId;
                    sizeScheduleDetailss.CreatedDate = sizeScheduleDetails.CreatedDate;
                    sizeScheduleDetailss.UpdatedDate = DateTime.Now;
                    sizeScheduleDetailss.CreatedBy = sizeScheduleDetails.CreatedBy;
                    sizeScheduleDetailss.UpdatedBy = sizeScheduleDetails.UpdatedBy;
                    sizeScheduledDetailManager.Put(sizeScheduleDetailss);
                }
                else
                {
                    return Json(sizeScheduleDetailss, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(sizeScheduleDetailss, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SizeScheduleDetailsId)
        {
            SizeScheduleDetails sizeScheduleDetails = new SizeScheduleDetails();
            string status = "";
            SizeScheduledDetailManager sizeScheduledDetailManager = new SizeScheduledDetailManager();
            sizeScheduleDetails = sizeScheduledDetailManager.GetSizeScheduleDetailsId(SizeScheduleDetailsId);
            if (sizeScheduleDetails != null)
            {
                status = "Success";
                sizeScheduledDetailManager.Delete(sizeScheduleDetails.SizeScheduleDetailsId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SizeScheduleDetails> sizeScheduleDetailsList = new List<SizeScheduleDetails>();
            SizeScheduledDetailManager sizeScheduledDetailManager = new SizeScheduledDetailManager();
            sizeScheduleDetailsList = sizeScheduledDetailManager.Get();
            SizeScheduleDetailsModel model = new SizeScheduleDetailsModel();
            model.SizeScheduleDetailsList = sizeScheduleDetailsList;
            return PartialView("Partial/SizeScheduleDetailsGrid", model);
        }
        #endregion

    }
}
