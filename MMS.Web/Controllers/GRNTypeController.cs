using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.GRNTypeMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class GRNTypeController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult GRNType()
        {
            GRNTypeMasterModel vm = new GRNTypeMasterModel();
            return View(vm);
        }
        public ActionResult GRNTypeGrid()
        {
            GRNTypeMasterModel vm = new GRNTypeMasterModel();
            GRNTypeManager issueTypeManager = new GRNTypeManager();
            vm.GRNTypeList = issueTypeManager.Get();
            return PartialView("Partial/GRNTypeGrid", vm);
        }
        [HttpPost]
        public ActionResult GRNTypeDetails(int GrnTypeMasterId)
        {
            GRNTypeManager issueTypeManager = new GRNTypeManager();
            GrnTypeMaster issueTypeMaster = new GrnTypeMaster();
            GRNTypeMasterModel model = new GRNTypeMasterModel();
            List<GrnTypeMaster> agents = new List<GrnTypeMaster>(); int result = 0;

            agents = issueTypeManager.Get();
            if (GrnTypeMasterId == 0)
            {
                if (agents.Count == 0)
                {
                    result = 1;
                }
                else
                {
                    result = agents.OrderByDescending(s => s.GrnTypeMasterId).FirstOrDefault().GrnTypeMasterId + 1;
                }
            }
            else
            {
                result = GrnTypeMasterId;
            }
            issueTypeMaster = issueTypeManager.GetIssueTypeMasterId(GrnTypeMasterId);
            if (issueTypeMaster != null)
            {
                model.GrnTypeMasterId = issueTypeMaster.GrnTypeMasterId;
                model.GRNCode = "GRN0" + result;
                model.IssueType = issueTypeMaster.IssueType;
                model.CreatedDate = issueTypeMaster.CreatedDate;
                model.UpdatedDate = issueTypeMaster.UpdatedDate;
                model.CreatedBy = issueTypeMaster.CreatedBy;
                model.UpdatedBy = issueTypeMaster.UpdatedBy;
            }
            return PartialView("Partial/GRNTypeDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult GRNType(GRNTypeMasterModel model)
        {
            GrnTypeMaster issueTypeMasters = new GrnTypeMaster();
            if (ModelState.IsValid)
            {
                GRNTypeManager issueTypeManager = new GRNTypeManager();
                GrnTypeMaster issueTypeMaster = new GrnTypeMaster();
                issueTypeMaster = issueTypeManager.GetIssueType(model.IssueType);
                if (issueTypeMaster == null)
                {
                    issueTypeMasters.IssueType = model.IssueType;
                    issueTypeMasters.CreatedDate = DateTime.Now;
                    issueTypeMasters.UpdatedDate = DateTime.Now;
                  
                    issueTypeManager.Post(issueTypeMasters);
                }
                    else if(issueTypeMaster != null && issueTypeMaster.IssueType == model.IssueType && model.GrnTypeMasterId == 0)
                    {
                        issueTypeMaster.GrnTypeMasterId = 0;
                        return Json(issueTypeMaster, JsonRequestBehavior.AllowGet);
                    }
                else
                {
                    return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIssueId()
        {
            GRNTypeManager gm = new GRNTypeManager();
            List<GrnTypeMaster> agents = new List<GrnTypeMaster>(); int result = 0;
            try
            {
                agents = gm.Get();
                if (agents.Count == 0)
                {
                    result = 1;
                }
                else
                {
                    result = agents.OrderByDescending(s => s.GrnTypeMasterId).FirstOrDefault().GrnTypeMasterId + 1;
                }

            }
            catch (Exception ex)
            { }

            return Json("GRN0" + result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(GRNTypeMasterModel model)
        {
            GrnTypeMaster issueTypeMasters = new GrnTypeMaster();
            if (ModelState.IsValid)
            {
                GrnTypeMaster issueTypeMaster = new GrnTypeMaster();
                GRNTypeManager issueTypeManager = new GRNTypeManager();
                issueTypeMaster = issueTypeManager.GetIssueTypeMasterId(model.GrnTypeMasterId);
                issueTypeMaster.GrnTypeMasterId = Convert.ToInt32(model.GRNCode.Substring(4));
                if (issueTypeMaster != null)
                {
                    issueTypeMasters.GrnTypeMasterId = issueTypeMaster.GrnTypeMasterId;
                    issueTypeMasters.IssueType = model.IssueType;
                    issueTypeMasters.CreatedDate = model.CreatedDate;
                    issueTypeMasters.UpdatedDate = model.UpdatedDate;
                    issueTypeManager.Put(issueTypeMasters);
                }
                else
                {
                    return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int GrnTypeMasterId)
        {
            GRNTypeManager issueTypeManager = new GRNTypeManager();
            string status = "";
            GrnTypeMaster issueTypeMaster = new GrnTypeMaster();
            issueTypeMaster = issueTypeManager.GetIssueTypeMasterId(GrnTypeMasterId);
            if (issueTypeMaster != null)
            {
                status = "Success";
                issueTypeManager.Delete(issueTypeMaster.GrnTypeMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<GrnTypeMaster> issueTypeMastersList = new List<GrnTypeMaster>();
            GRNTypeManager issueTypeManager = new GRNTypeManager();
            issueTypeMastersList = issueTypeManager.Get();
            issueTypeMastersList = issueTypeMastersList.Where(x => x.IssueType.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            GRNTypeMasterModel model = new GRNTypeMasterModel();
            model.GRNTypeList = issueTypeMastersList;
            return PartialView("Partial/GRNTypeGrid", model);
        }
        #endregion

    }
}
