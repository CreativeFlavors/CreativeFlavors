using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SkillMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SkillMasterController : Controller
    {

        #region Accounts View

        [HttpGet]
        public ActionResult SkillMaster()
        {
            SkillMasterModel vm = new SkillMasterModel();
            return View(vm);
        }
        public ActionResult SkillMasterGrid()
        {
            SkillMasterModel vm = new SkillMasterModel();
            SkillMasterManager skillMasterManager = new SkillMasterManager();
            vm.SkillMasterList = skillMasterManager.Get();

            return PartialView("Partial/SkillMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult SkillMasterDetail(int SkillMasterId)
        {
            SkillMasterManager skillMasterManager = new SkillMasterManager();
            SkillMaster skillMaster = new SkillMaster();
            SkillMasterModel model = new SkillMasterModel();
            skillMaster = skillMasterManager.GetSkillMasterId(SkillMasterId);
            if (skillMaster != null)
            {
                model.SkillMasterId = skillMaster.SkillMasterId;
                model.SkillCode = skillMaster.SkillCode;
                model.SkillName = skillMaster.SkillName;
                model.CreatedDate = skillMaster.CreatedDate;
                model.UpdatedDate = skillMaster.UpdatedDate;
                model.CreatedBy = skillMaster.CreatedBy;
                model.UpdatedBy = skillMaster.UpdatedBy;

            }
            return PartialView("Partial/SkillMasterDetail", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SkillMaster(SkillMasterModel model)
        {
            SkillMaster skillMasters = new SkillMaster();
            if (ModelState.IsValid)
            {
                SkillMaster skillMaster = new SkillMaster();
                SkillMasterManager skillMasterManager = new SkillMasterManager();
                skillMaster = skillMasterManager.GetSkillName(model.SkillName);
                if (skillMaster == null)
                {
                    skillMasters.SkillMasterId = model.SkillMasterId;
                    skillMasters.SkillCode = model.SkillCode;
                    skillMasters.SkillName = model.SkillName;
                    skillMasters.CreatedDate = DateTime.Now;
                    skillMasters.UpdatedDate = DateTime.Now;
                    skillMasterManager.Post(skillMasters);
                }
                else if (skillMaster != null && skillMaster.SkillName == model.SkillName)
                {
                    skillMasters.SkillMasterId = 0;
                    return Json(skillMasters, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(skillMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(skillMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SkillMasterModel model)
        {
            SkillMaster skillMasters = new SkillMaster();
            if (ModelState.IsValid)
            {
                SkillMaster skillMaster = new SkillMaster();
                SkillMasterManager skillMasterManager = new SkillMasterManager();
                skillMaster = skillMasterManager.GetSkillMasterId(model.SkillMasterId);
                if (skillMaster != null)
                {
                    skillMasters.SkillMasterId = model.SkillMasterId;
                    skillMasters.SkillCode = model.SkillCode;
                    skillMasters.SkillName = model.SkillName;
                    skillMasters.CreatedDate = skillMaster.CreatedDate;
                    skillMasters.UpdatedDate = DateTime.Now;
                    skillMasters.CreatedBy = skillMaster.CreatedBy;
                    skillMasters.UpdatedBy = skillMaster.UpdatedBy;
                    skillMasterManager.Put(skillMasters);
                }
                else
                {
                    return Json(skillMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(skillMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SizeScheduleMasterId)
        {
            SkillMaster skillMaster = new SkillMaster();
            string status = "";
            SkillMasterManager skillMasterManager = new SkillMasterManager();
            skillMaster = skillMasterManager.GetSkillMasterId(SizeScheduleMasterId);
            if (skillMaster != null)
            {
                status = "Success";
                skillMasterManager.Delete(skillMaster.SkillMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SkillMaster> SkillMasterlist = new List<SkillMaster>();
            SkillMasterManager skillMasterManager = new SkillMasterManager();
            SkillMasterlist = skillMasterManager.Get();
            SkillMasterlist = SkillMasterlist.Where(x => x.SkillCode.ToLower().Contains(filter.ToLower()) || x.SkillName.ToLower().Contains(filter.ToLower())).ToList();
            SkillMasterModel model = new SkillMasterModel();
            model.SkillMasterList = SkillMasterlist;
            return PartialView("Partial/SizeScheduleMasterGrid", model);
        }
        #endregion

    }
}
