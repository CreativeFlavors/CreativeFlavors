using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.OrginMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class OrginMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult OrginMaster()
        {
            OrginMasterModel vm = new OrginMasterModel();
            return View(vm);
        }
        public ActionResult OrginGrid()
        {
            OrginMasterModel vm = new OrginMasterModel();
            OrgingMasterManager orgingMasterManager = new OrgingMasterManager();
            vm.OrginMasterlist = orgingMasterManager.Get();

            return PartialView("Partial/OrginGrid", vm);
        }
        [HttpPost]
        public ActionResult OrginDetails(int OriginMasterId)
        {
            OrgingMasterManager orgingMasterManager = new OrgingMasterManager();
            OriginMaster orginMaster = new OriginMaster();
            OrginMasterModel model = new OrginMasterModel();
            orginMaster = orgingMasterManager.GetOriginMasterId(OriginMasterId);
            string autoId = GetAutoid();
            if (orginMaster != null)
            {
                model.OriginMasterId = orginMaster.OriginMasterId;
                model.Code = orginMaster.Code;
                model.OriginName = orginMaster.OriginName;
                model.CreatedDate = orginMaster.CreatedDate;
                model.UpdatedDate = orginMaster.UpdatedDate;
                model.CreatedBy = orginMaster.CreatedBy;
                model.UpdatedBy = orginMaster.UpdatedBy;
            }
            if (OriginMasterId == 0)
            {
                model.Code = "ORG" + autoId;
            }
            else
            {
                model.Code = "ORG" + orginMaster.OriginMasterId;
            }
            return PartialView("Partial/OrginDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult OrginMaster(OrginMasterModel model)
        {
            OriginMaster orginMasters = new OriginMaster();
            OriginMaster orginMaster = new OriginMaster();
            OrgingMasterManager orgingMasterManager = new OrgingMasterManager();
            orginMaster = orgingMasterManager.GetOriginName(model.OriginName);
            if (orginMaster == null)
            {
                orginMasters.OriginMasterId = model.OriginMasterId;
                orginMasters.Code = model.Code;
                orginMasters.OriginName = model.OriginName;
                orginMasters.CreatedDate = DateTime.Now;
                orgingMasterManager.Post(orginMasters);
            }
            else if (orginMaster != null && orginMaster.OriginName == model.OriginName && model.OriginMasterId == 0)
            {
                orginMasters.OriginMasterId = 0;
                return Json(orginMasters, JsonRequestBehavior.AllowGet);
            }
            return Json(orginMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(OrginMasterModel model)
        {
            OriginMaster orginMasters = new OriginMaster();
            if (ModelState.IsValid)
            {
                OriginMaster orginMaster = new OriginMaster();
                OrgingMasterManager orgingMasterManager = new OrgingMasterManager();
                orginMaster = orgingMasterManager.GetOriginMasterId(model.OriginMasterId);
                if (orginMaster != null)
                {
                    orginMasters.OriginMasterId = model.OriginMasterId;
                    orginMasters.Code = model.Code;
                    orginMasters.OriginName = model.OriginName;
                    orginMasters.CreatedDate = orginMaster.CreatedDate;
                    orginMasters.UpdatedDate = DateTime.Now;
                    orginMasters.CreatedBy = orginMaster.CreatedBy;
                    orginMasters.UpdatedBy = orginMaster.UpdatedBy;
                    orgingMasterManager.Put(orginMasters);
                }
                else
                {
                    return Json(orginMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(orginMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int OriginMasterId)
        {
            OriginMaster orginMaster = new OriginMaster();
            string status = "";
            OrgingMasterManager orgingMasterManager = new OrgingMasterManager();
            orginMaster = orgingMasterManager.GetOriginMasterId(OriginMasterId);
            if (orginMaster != null)
            {
                status = "Success";
                orgingMasterManager.Delete(orginMaster.OriginMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<OriginMaster> manufacturerMasterlsit = new List<OriginMaster>();
            OrgingMasterManager orgingMasterManager = new OrgingMasterManager();
            manufacturerMasterlsit = orgingMasterManager.Get();
            manufacturerMasterlsit = manufacturerMasterlsit.Where(x => x.OriginName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            OrginMasterModel model = new OrginMasterModel();
            model.OrginMasterlist = manufacturerMasterlsit;
            return PartialView("Partial/OrginGrid", model);
        }
        public string GetAutoid()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoOrginID();
            ID++;
            return ID.ToString();
        }
        #endregion


    }
}
