using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.CommisionCritriaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class CommisionCritiriaController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult CommisionCritiria()
        {
            CommisionCritriaModel vm = new CommisionCritriaModel();
            return View(vm);
        }
        public ActionResult CommisionCritiriaGrid()
        {
            CommisionCritriaModel vm = new CommisionCritriaModel();
            CommisionCritiriaManager commisionCritiriaManager = new CommisionCritiriaManager();
            vm.CommisionCriteriaList = commisionCritiriaManager.Get();

            return PartialView("Partial/CommisionCritiriaGrid", vm);
        }
        [HttpPost]
        public ActionResult CommisionCritiriaDetails(int CommisionCriteriaId)
        {
            CommisionCritiriaManager commisionCritiriaManager = new CommisionCritiriaManager();
            CommisionCriteria commisionCriteria = new CommisionCriteria();
            CommisionCritriaModel model = new CommisionCritriaModel();
            commisionCriteria = commisionCritiriaManager.GetCommisionCriteriaId(CommisionCriteriaId);
            if (commisionCriteria != null)
            {
                model.CommisionCriteriaId = commisionCriteria.CommisionCriteriaId;
                model.CommisionName = commisionCriteria.CommisionName;
                model.CommisionPercent = commisionCriteria.CommisionPercent;
                model.Criteria = commisionCriteria.Criteria;
                model.Value = commisionCriteria.Value;
                model.CreatedDate = commisionCriteria.CreatedDate;
                model.UpdatedDate = commisionCriteria.UpdatedDate;
                model.CreatedBy = commisionCriteria.CreatedBy;
                model.UpdatedBy = commisionCriteria.UpdatedBy;
            }
            return PartialView("Partial/CommisionCritiriaDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult CommisionCritiria(CommisionCritriaModel model)
        {
            CommisionCriteria commisionCriterias = new CommisionCriteria();
            if (ModelState.IsValid)
            {
                CommisionCriteria commisionCriteria = new CommisionCriteria();
                CommisionCritiriaManager commisionCritiriaManager = new CommisionCritiriaManager();
                commisionCriteria = commisionCritiriaManager.GetCommisionName(model.CommisionName);
                if (commisionCriteria == null)
                {
                   
                    commisionCriterias.CommisionName = model.CommisionName;
                    commisionCriterias.CommisionPercent = model.CommisionPercent;
                    commisionCriterias.Criteria = model.Criteria;
                    commisionCriterias.Value = model.Value;
                    commisionCriterias.CreatedDate = model.CreatedDate;
                    commisionCriterias.UpdatedDate = model.UpdatedDate;
                    commisionCritiriaManager.Post(commisionCriterias);
                }
                else
                {
                    return Json(commisionCriterias, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(commisionCriterias, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(CommisionCritriaModel model)
        {
            CommisionCriteria commisionCriterias = new CommisionCriteria();
            if (ModelState.IsValid)
            {
                CommisionCriteria commisionCriteria = new CommisionCriteria();
                CommisionCritiriaManager commisionCritiriaManager = new CommisionCritiriaManager();
                commisionCriteria = commisionCritiriaManager.GetCommisionCriteriaId(model.CommisionCriteriaId);
                if (commisionCriteria != null)
                {
                    commisionCriterias.CommisionCriteriaId = model.CommisionCriteriaId;
                    commisionCriterias.CommisionName = model.CommisionName;
                    commisionCriterias.CommisionPercent = model.CommisionPercent;
                    commisionCriterias.Criteria = model.Criteria;
                    commisionCriterias.Value = model.Value;
                    commisionCriterias.CreatedDate = model.CreatedDate;
                    commisionCriterias.UpdatedDate = model.UpdatedDate;
                    commisionCriterias.CreatedBy = commisionCriteria.CreatedBy;
                    commisionCriterias.UpdatedBy = commisionCriteria.UpdatedBy;
                    commisionCritiriaManager.Put(commisionCriterias);
                }
                else
                {
                    return Json(commisionCriterias, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(commisionCriterias, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int CommisionCriteriaId)
        {
            CommisionCriteria commisionCriteria = new CommisionCriteria();
            string status = "";
            CommisionCritiriaManager commisionCritiriaManager = new CommisionCritiriaManager();
            commisionCriteria = commisionCritiriaManager.GetCommisionCriteriaId(CommisionCriteriaId);
            if (commisionCriteria != null)
            {
                status = "Success";
                commisionCritiriaManager.Delete(commisionCriteria.CommisionCriteriaId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<CommisionCriteria> commisionCriteriaList = new List<CommisionCriteria>();
            CommisionCritiriaManager commisionCritiriaManager = new CommisionCritiriaManager();
            commisionCriteriaList = commisionCritiriaManager.Get();
            commisionCriteriaList = commisionCriteriaList.Where(x => x.CommisionName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            CommisionCritriaModel model = new CommisionCritriaModel();
            model.CommisionCriteriaList = commisionCriteriaList;
            return PartialView("Partial/CommisionCritiriaGrid", model);
        }
        #endregion


    }
}
