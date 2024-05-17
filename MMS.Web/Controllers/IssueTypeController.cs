using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.IssueTypeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class IssueTypeController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult IssueType()
        {
            IssueTypeModel vm = new IssueTypeModel();
            return View(vm);
        }
        public ActionResult IssueTypeGrid()
        {
            IssueTypeModel vm = new IssueTypeModel();
            IssueTypeManager issueTypeManager = new IssueTypeManager();
            vm.IssueTypeMasterList = issueTypeManager.Get();
            return PartialView("Partial/IssueTypeGrid", vm);
        }
        public JsonResult GetIssueId()
        {
            IssueTypeManager gm = new IssueTypeManager();
            List<IssueTypeMaster> agents = new List<IssueTypeMaster>(); int result = 0;
            try
            {
                agents = gm.Get();
                if (agents.Count == 0)
                {
                    result = 1;
                }
                else
                {
                    result = agents.OrderByDescending(s => s.IssueTypeMasterId).FirstOrDefault().IssueTypeMasterId + 1;
                }

            }
            catch (Exception ex)
            { }

            return Json("ISSUE0" + result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IssueTypeDetail(int IssueTypeMasterId)
        {
            IssueTypeManager issueTypeManager = new IssueTypeManager();
            IssueTypeMaster issueTypeMaster = new IssueTypeMaster();
            IssueTypeModel model = new IssueTypeModel();
            IssueTypeManager gm = new IssueTypeManager();
            List<IssueTypeMaster> agents = new List<IssueTypeMaster>();
            issueTypeMaster = issueTypeManager.GetIssueTypeMasterId(IssueTypeMasterId);
            int id_;
            string id = MMS.Web.ExtensionMethod.HtmlHelper.getAutoIssueTypeMasterID();
            id_ = Convert.ToInt32(id);
            id_++;
            if (issueTypeMaster != null)
            {
                model.IssueTypeMasterId = issueTypeMaster.IssueTypeMasterId;
                model.IssueType = issueTypeMaster.IssueType;
                model.CreatedDate = issueTypeMaster.CreatedDate;
                model.UpdatedDate = issueTypeMaster.UpdatedDate;
                model.CreatedBy = issueTypeMaster.CreatedBy;
                model.UpdatedBy = issueTypeMaster.UpdatedBy;
            }
            if (IssueTypeMasterId == 0)
            {
                model.IssueTypeCode = "IST" + id_;
            }
            else
            {
                model.IssueTypeCode = "IST" + issueTypeMaster.IssueTypeMasterId; ;
            }
            return PartialView("Partial/IssueTypeDetail", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult IssueType(IssueTypeModel model)
        {
            IssueTypeMaster issueTypeMasters = new IssueTypeMaster();
            if (ModelState.IsValid)
            {
                IssueTypeMaster issueTypeMaster = new IssueTypeMaster();
                IssueTypeManager issueTypeManager = new IssueTypeManager();
                issueTypeMaster = issueTypeManager.GetIssueType(model.IssueType);
                if (issueTypeMaster == null)
                {
                    issueTypeMasters.IssueType = model.IssueType;
                    issueTypeMasters.CreatedDate = model.CreatedDate;
                    issueTypeManager.Post(issueTypeMasters);
                }
                else
                {
                    return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(IssueTypeModel model)
        {
            IssueTypeMaster issueTypeMasters = new IssueTypeMaster();
            if (ModelState.IsValid)
            {
                IssueTypeMaster issueTypeMaster = new IssueTypeMaster();
                IssueTypeManager issueTypeManager = new IssueTypeManager();
                issueTypeMaster = issueTypeManager.GetIssueTypeMasterId(model.IssueTypeMasterId);
                if (issueTypeMaster != null)
                {
                    issueTypeMasters.IssueTypeMasterId = model.IssueTypeMasterId;
                    issueTypeMasters.IssueType = model.IssueType;
                    issueTypeMasters.CreatedDate = model.CreatedDate;              
                    issueTypeMasters.CreatedBy = issueTypeMasters.CreatedBy;               
                    issueTypeManager.Put(issueTypeMasters);
                }
                else
                {
                    return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(issueTypeMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int IssueTypeMasterId)
        {
            IssueTypeMaster issueTypeMaster = new IssueTypeMaster();
            string status = "";
            IssueTypeManager issueTypeManager = new IssueTypeManager();
            issueTypeMaster = issueTypeManager.GetIssueTypeMasterId(IssueTypeMasterId);
            if (issueTypeMaster != null)
            {
                status = "Success";
                issueTypeManager.Delete(issueTypeMaster.IssueTypeMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<IssueTypeMaster> issueTypeMastersList = new List<IssueTypeMaster>();
            IssueTypeManager issueTypeManager = new IssueTypeManager();
            issueTypeMastersList = issueTypeManager.Get();
            issueTypeMastersList = issueTypeMastersList.Where(x => x.IssueType.ToLower().Contains(filter.ToLower())).ToList();
            IssueTypeModel model = new IssueTypeModel();
            model.IssueTypeMasterList = issueTypeMastersList;
            return PartialView("Partial/IssueTypeGrid", model);
        }
        #endregion

    }
}
