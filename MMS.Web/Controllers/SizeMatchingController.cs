using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SizeMatchingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SizeMatchingController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SizeMatching()
        {
            SizeMatchingModel model = new SizeMatchingModel();
            return View(model);
        }
        public ActionResult SizeMatchingGrid()
        {
            SizeMatchingModel vm = new SizeMatchingModel();
            SizeMatchingManager sizeMatchingManager = new SizeMatchingManager();
            vm.sizeMatchinglist = sizeMatchingManager.Get();

            return PartialView("Partial/SizeMatchingGrid", vm);
        }
        [HttpPost]
        public ActionResult SizeMatchingDetails(int SizeMatchingMasterID)
        {
            SizeMatchingManager sizeMatchingManager = new SizeMatchingManager();
            SizeMatching sizeMatching = new SizeMatching();
            SizeMatchingModel model = new SizeMatchingModel();
            sizeMatching = sizeMatchingManager.GetSizeMatchingMasterID(SizeMatchingMasterID);
            string autoId = GetAutoid();
            if (sizeMatching != null)
            {
                model.SizeMatchingName = sizeMatching.SizeMatchingName;
                model.SizeMatchingMasterID = sizeMatching.SizeMatchingMasterID;
                model.CreatedBy = sizeMatching.CreatedBy;
                model.UpdatedBy = sizeMatching.UpdatedBy;
                model.CreatedDate = sizeMatching.CreatedDate.Value;

            }
            if (SizeMatchingMasterID != 0)
            {
                model.SizeMatchingCode = "SM" + sizeMatching.SizeMatchingMasterID;
            }
            else
            {
                model.SizeMatchingCode = "SM" + autoId;
            }
            return PartialView("Partial/SizeMatchingDetails", model);
        }



        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SizeMatching(SizeMatchingModel sizeMatchingModel)
        {
            SizeMatchingManager sizeMatchingManager = new SizeMatchingManager();
            SizeMatching sizeMatching = new SizeMatching();
            SizeMatchingModel model = new SizeMatchingModel();
            SizeMatching sizeMatchings = new SizeMatching();
            if (ModelState.IsValid)
            {
                sizeMatchings = sizeMatchingManager.GetSizeMatchingName(sizeMatchingModel.SizeMatchingName);
                if (sizeMatchings == null)
                {
                    sizeMatching.SizeMatchingName = sizeMatchingModel.SizeMatchingName;
                    sizeMatching.SizeMatchingMasterID = sizeMatchingModel.SizeMatchingMasterID;
                    sizeMatching.IsDeleted = false;
                    sizeMatching.CreatedDate = DateTime.Now;                 
                    sizeMatchingManager.Post(sizeMatching);
                }
                else if (sizeMatchings != null && sizeMatching.SizeMatchingName == model.SizeMatchingName && model.SizeMatchingMasterID == 0)
                {
                    sizeMatchings.SizeMatchingMasterID = 0;
                    return Json(sizeMatchings, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(sizeMatching, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(sizeMatching, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SizeMatchingModel sizeMatchingModel)
        {
            SizeMatchingManager sizeMatchingManager = new SizeMatchingManager();
            SizeMatching sizeMatching = new SizeMatching();
            SizeMatchingModel model = new SizeMatchingModel();
            SizeMatching sizeMatchings = new SizeMatching();
            if (ModelState.IsValid)
            {
                sizeMatching = sizeMatchingManager.GetSizeMatchingMasterID(sizeMatchingModel.SizeMatchingMasterID);
                if (sizeMatching != null)
                {


                    sizeMatchings.SizeMatchingName = sizeMatchingModel.SizeMatchingName;
                    sizeMatchings.SizeMatchingMasterID = sizeMatchingModel.SizeMatchingMasterID;
                    sizeMatchings.IsDeleted = sizeMatching.IsDeleted;
                    sizeMatchings.UpdatedDate = DateTime.Now;
                    sizeMatchings.CreatedDate = sizeMatching.CreatedDate;
                    sizeMatchings.CreatedBy = sizeMatching.CreatedBy;
                    sizeMatchingManager.Put(sizeMatchings);
                }
                else
                {
                    return Json(sizeMatchings, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(sizeMatchings, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SizeMatchingMasterID)
        {
            SizeMatching sizeMatching = new SizeMatching();
            string status = "";
            SizeMatchingManager sizeMatchingManager = new SizeMatchingManager();
            sizeMatching = sizeMatchingManager.GetSizeMatchingMasterID(SizeMatchingMasterID);
            if (sizeMatching != null)
            {
                status = "Success";
                sizeMatchingManager.Delete(sizeMatching.SizeMatchingMasterID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SizeMatching> sizeMatchinglist = new List<SizeMatching>();
            SizeMatchingManager sizeMatchingManager = new SizeMatchingManager();
            sizeMatchinglist = sizeMatchingManager.Get();
            sizeMatchinglist = sizeMatchinglist.Where(x => x.SizeMatchingName.ToLower().Contains(filter.ToLower())).ToList();
            Session["SizeMatching"] = sizeMatchinglist;
            SizeMatchingModel model = new SizeMatchingModel();
            model.sizeMatchinglist = sizeMatchinglist;
            return PartialView("Partial/ColorGrid", model);
        }
        public string GetAutoid()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoSizeMatchingID();
            ID++;
            return ID.ToString();
        }
        #endregion
    }
}
