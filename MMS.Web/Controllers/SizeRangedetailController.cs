using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SizeRangeDetailsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SizeRangedetailController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SizeRangedetail()
        {
            SizeRangeDetails vm = new SizeRangeDetails();
            return View(vm);
        }
        public ActionResult SizeRangeDetailsGrid()
        {
            SizeRangeDetailsModel vm = new SizeRangeDetailsModel();
            SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
            vm.SizeRangeDetailslsit = sizeRangeDetailsManager.Get();

            return PartialView("Partial/SizeRangeDetailsGrid", vm);
        }
        [HttpPost]
        public ActionResult SizeRangeDetailsView(int SizeRangeDetailsId)
        {
            SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
            SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
            SizeRangeDetailsModel model = new SizeRangeDetailsModel();
            sizeRangeDetails = sizeRangeDetailsManager.GetSizeRangeDetailsId(SizeRangeDetailsId);
            if (sizeRangeDetails != null)
            {
                model.SizeRangeDetailsId = sizeRangeDetails.SizeRangeDetailsId;            
                model.SizeNo = sizeRangeDetails.SizeNo;
                model.SizenRangeName = sizeRangeDetails.SizeRangeName;
                model.CreatedDate = sizeRangeDetails.CreatedDate;
                model.UpdatedDate = sizeRangeDetails.UpdatedDate;
                model.CreatedBy = sizeRangeDetails.CreatedBy;
                model.UpdatedBy = sizeRangeDetails.UpdatedBy;
            }
            return PartialView("Partial/SizeRangeDetailsView", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SizeRangedetail(SizeRangeDetailsModel model)
        {
            SizeRangeDetails sizeRangeDetail = new SizeRangeDetails();
            SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
            SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
            sizeRangeDetails.SizeRangeDetailsId = model.SizeRangeDetailsId;          
            sizeRangeDetails.SizeNo = model.SizeNo;
            sizeRangeDetails.SizeRangeName = model.SizenRangeName;
            sizeRangeDetailsManager.Post(sizeRangeDetails);            
            return Json(sizeRangeDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SizeRangeDetailsModel model)
        {
            SizeRangeDetails sizeRangeDetail = new SizeRangeDetails();
            SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
            if (ModelState.IsValid)
            {
                SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
                sizeRangeDetail = sizeRangeDetailsManager.GetSizeRangeDetailsId(model.SizeRangeDetailsId);
                if (sizeRangeDetail != null)
                {
                    sizeRangeDetails.SizeRangeDetailsId = model.SizeRangeDetailsId;                 
                    sizeRangeDetails.SizeNo = model.SizeNo;
                    sizeRangeDetails.SizeRangeName = model.SizenRangeName;
                    sizeRangeDetails.CreatedDate = sizeRangeDetail.CreatedDate;
                    sizeRangeDetails.UpdatedDate = DateTime.Now;
                    sizeRangeDetails.CreatedBy = sizeRangeDetail.CreatedBy;
                    sizeRangeDetails.UpdatedBy = sizeRangeDetail.UpdatedBy;
                    sizeRangeDetailsManager.Put(sizeRangeDetails);
                }
                else
                {
                    return Json(sizeRangeDetails, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(sizeRangeDetails, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SizeRangeDetailsId)
        {
            SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
            string status = "";
            SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
            sizeRangeDetails = sizeRangeDetailsManager.GetSizeRangeDetailsId(SizeRangeDetailsId);
            if (sizeRangeDetails != null)
            {
                status = "Success";
                sizeRangeDetailsManager.Delete(sizeRangeDetails.SizeRangeDetailsId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SizeRangeDetails> SizeRangeDetailslist = new List<SizeRangeDetails>();
            SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
            SizeRangeDetailslist = sizeRangeDetailsManager.Get();
            SizeRangeDetailslist = SizeRangeDetailslist.Where(x => x.SizeRangeName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            SizeRangeDetailsModel model = new SizeRangeDetailsModel();
            model.SizeRangeDetailslsit = SizeRangeDetailslist;
            return PartialView("Partial/SizeRangeDetailsGrid", model);
        }
        #endregion


    }
}
