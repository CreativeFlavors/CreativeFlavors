using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.LeatherShoeGradeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class LeatherShoeGradeController : Controller
    {

        #region Accounts View

        [HttpGet]
        public ActionResult LeatherShoeGrade()
        {
            LeatherShoeGradeModel vm = new LeatherShoeGradeModel();
            return View(vm);
        }
        public ActionResult LeatherShoeGradeGrid()
        {
            LeatherShoeGradeModel vm = new LeatherShoeGradeModel();
            LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
            vm.leatherShoesGradeMasterList = leatherShoeGradeManager.Get();
            return PartialView("Partial/LeatherShoeGradeGrid", vm);
        }
        [HttpPost]
        public ActionResult LeatherShoeGradeDetails(int LeatherShoesGradeMasterId)
        {
            LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
            LeatherShoesGradeMaster leatherShoesGradeMaster = new LeatherShoesGradeMaster();
            LeatherShoeGradeModel model = new LeatherShoeGradeModel();
            leatherShoesGradeMaster = leatherShoeGradeManager.GetLeatherShoesGradeMasterId(LeatherShoesGradeMasterId);
            int id_;
            string id = MMS.Web.ExtensionMethod.HtmlHelper.getAutoLeatherShoreGradeMasterID();
            if (id != "")
            {
                id_ = Convert.ToInt32(id);
            }
            else
            {
                id_ = 0;
            }
            
            id_++;

            if (leatherShoesGradeMaster != null)
            {
                model.LeatherShoesGradeMasterId = leatherShoesGradeMaster.LeatherShoesGradeMasterId;
                model.Grade = leatherShoesGradeMaster.Grade;
                model.GradeCode = leatherShoesGradeMaster.GradeCode;
                model.CreatedDate = leatherShoesGradeMaster.CreatedDate;
                model.UpdatedDate = leatherShoesGradeMaster.UpdatedDate;
                model.CreatedBy = leatherShoesGradeMaster.CreatedBy;
                model.UpdatedBy = leatherShoesGradeMaster.UpdatedBy;
            }
            if (LeatherShoesGradeMasterId == 0)
            {
                model.GradeCode = "LSG" + id_;
            }
            else
            {
                model.GradeCode = model.GradeCode;
            }
            return PartialView("Partial/LeatherShoeGradeDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult LeatherShoeGrade(LeatherShoeGradeModel model)
        {
            LeatherShoesGradeMaster leatherShoesGradeMasters = new LeatherShoesGradeMaster();
            if (ModelState.IsValid)
            {
                LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
                LeatherShoesGradeMaster leatherShoesGradeMaster = new LeatherShoesGradeMaster();
                leatherShoesGradeMaster = leatherShoeGradeManager.GetIssueType(model.Grade);
                if (leatherShoesGradeMaster == null)
                {
                    leatherShoesGradeMasters.GradeCode = model.GradeCode;
                    leatherShoesGradeMasters.Grade = model.Grade;
                    leatherShoesGradeMasters.CreatedDate = DateTime.Now;
                    leatherShoeGradeManager.Post(leatherShoesGradeMasters);
                }
               
                else if (leatherShoesGradeMaster != null && leatherShoesGradeMaster.Grade == model.Grade && model.LeatherShoesGradeMasterId == 0)
                {
                    leatherShoesGradeMasters.LeatherShoesGradeMasterId = 0;
                    return Json(leatherShoesGradeMasters, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(leatherShoesGradeMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(leatherShoesGradeMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(LeatherShoeGradeModel model)
        {
            LeatherShoesGradeMaster leatherShoesGradeMasters = new LeatherShoesGradeMaster();
            if (ModelState.IsValid)
            {
                LeatherShoesGradeMaster leatherShoesGradeMaster = new LeatherShoesGradeMaster();
                LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
                leatherShoesGradeMaster = leatherShoeGradeManager.GetLeatherShoesGradeMasterId(model.LeatherShoesGradeMasterId);
                if (leatherShoesGradeMaster != null)
                {
                    leatherShoesGradeMasters.LeatherShoesGradeMasterId = model.LeatherShoesGradeMasterId;
                    leatherShoesGradeMasters.Grade = model.Grade;
                    leatherShoesGradeMasters.GradeCode = model.GradeCode;
                    leatherShoesGradeMasters.CreatedDate = model.CreatedDate;
                    leatherShoesGradeMasters.UpdatedDate = model.UpdatedDate;
                    leatherShoesGradeMasters.CreatedBy = leatherShoesGradeMaster.CreatedBy;
                    leatherShoesGradeMasters.UpdatedBy = leatherShoesGradeMaster.UpdatedBy;
                    leatherShoeGradeManager.Put(leatherShoesGradeMasters);
                }
                else
                {
                    return Json(leatherShoesGradeMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(leatherShoesGradeMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int LeatherShoesGradeMasterId)
        {
            LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
            string status = "";
            LeatherShoesGradeMaster leatherShoesGradeMaster = new LeatherShoesGradeMaster();
            leatherShoesGradeMaster = leatherShoeGradeManager.GetLeatherShoesGradeMasterId(LeatherShoesGradeMasterId);
            if (leatherShoesGradeMaster != null)
            {
                status = "Success";
                leatherShoeGradeManager.Delete(leatherShoesGradeMaster.LeatherShoesGradeMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<LeatherShoesGradeMaster> leatherShoesGradeMaster = new List<LeatherShoesGradeMaster>();
            LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
            leatherShoesGradeMaster = leatherShoeGradeManager.Get();
            leatherShoesGradeMaster = leatherShoesGradeMaster.Where(x => x.GradeCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.Grade.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            LeatherShoeGradeModel model = new LeatherShoeGradeModel();
            model.leatherShoesGradeMasterList = leatherShoesGradeMaster;
            return PartialView("Partial/LeatherShoeGradeGrid", model);
        }
        #endregion

    }
}
