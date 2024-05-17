using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.AgentMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class AgentMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult AgentMaster()
        {
            AgentMasterModel vm = new AgentMasterModel();
            return View(vm);
        }
        public ActionResult AgentMasterGrid()
        {
            AgentMasterModel vm = new AgentMasterModel();
            AgentManager agentManager = new AgentManager();
            vm.AgentMastetlist = agentManager.Get();

            return PartialView("Partial/AgentMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult AgentMasterDetails(int AgentMasterId)
        {
            AgentManager agentManager = new AgentManager();
            CommisionCritiriaManager commisionCritirialManager = new CommisionCritiriaManager();
            AgentMaster agentMaster = new AgentMaster();
            AgentMasterModel model = new AgentMasterModel();
            agentMaster = agentManager.GetAgentMasterId(AgentMasterId);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAgentCodeId();
         

            if (agentMaster != null)
            {
                model.AgentMasterId = agentMaster.AgentMasterId;
                model.AgentFullName = agentMaster.AgentFullName;
                model.AgentShortName = agentMaster.AgentShortName;
                model.Currency = agentMaster.Currency;
                model.CommisionCriteriaId = agentMaster.CommisionCriteriaId;
                model.AddressLine1 = agentMaster.AddressLine1;
                model.AddressLine2 = agentMaster.AddressLine2;
                model.AddressLine3 = agentMaster.AddressLine3;
                model.Pincode = agentMaster.Pincode;
                model.CreatedDate = agentMaster.CreatedDate;
                model.UpdatedDate = agentMaster.UpdatedDate;
                model.CreatedBy = agentMaster.CreatedBy;
                model.UpdatedBy = agentMaster.UpdatedBy;
                model.ContactPerson = agentMaster.ContactPerson;
                model.MobileNo = agentMaster.MobileNo;
                model.EmailID = agentMaster.EmailID;
                model.CountryMasterId = agentMaster.CountryMasterId;
                model.CommisionPercentage = agentMaster.CommisionPercentage;
                model.CommisionCriteriaList = commisionCritirialManager.Get();
            }
            if (model.AgentMasterId != 0)
            {
                model.AgentCode = agentMaster.AgentCode;
            }
            else 
            {
                ID++;
                model.AgentCode = "AGT" + ID;
            }
            return PartialView("Partial/AgentMasterDetails", model);
        }

        public ActionResult InternalModelDetail(int CommisionCriteriaId)
        {
            CommisionCritiriaManager commissionCriteriaManager = new CommisionCritiriaManager();
            CommisionCriteria commissionCriteriaMaster = new CommisionCriteria();
            commissionCriteriaMaster = commissionCriteriaManager.GetCommisionCriteriaId(CommisionCriteriaId);
            return Json(new { CommissionPercent = commissionCriteriaMaster.CommisionPercent }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult AgentMaster(AgentMasterModel model)
        {
            AgentMaster AgentMasters = new AgentMaster();
            if (ModelState.IsValid)
            {
                AgentMaster agentMaster = new AgentMaster();
                AgentManager agentManager = new AgentManager();

                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAgentCodeId();
                ID++;

                agentMaster = agentManager.GetAgentFullName(model.AgentFullName);
                if (agentMaster == null)
                {
                    AgentMasters.AgentCode = "AGT" + ID;
                    AgentMasters.AgentFullName = model.AgentFullName;
                    AgentMasters.AgentShortName = model.AgentShortName;
                    AgentMasters.Currency = model.Currency;
                    AgentMasters.CommisionCriteriaId = model.CommisionCriteriaId;
                    AgentMasters.AddressLine1 = model.AddressLine1;
                    AgentMasters.AddressLine2 = model.AddressLine2;
                    AgentMasters.AddressLine3 = model.AddressLine3;
                    AgentMasters.Pincode = model.Pincode;
                    AgentMasters.CreatedDate = DateTime.Now;
                    AgentMasters.ContactPerson = model.ContactPerson;
                    AgentMasters.MobileNo = model.MobileNo;
                    AgentMasters.EmailID = model.EmailID;
                    AgentMasters.CountryMasterId = model.CountryMasterId;
                    AgentMasters.CommisionPercentage = model.CommisionPercentage;
                    agentManager.Post(AgentMasters);
                }
                else if(agentMaster != null && agentMaster.AgentFullName == model.AgentFullName && model.AgentMasterId == 0)
                {
                    agentMaster.AgentMasterId = 0;
                    return Json(agentMaster, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(AgentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(AgentMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(AgentMasterModel model)
        {
            AgentMaster AgentMasters = new AgentMaster();
            if (ModelState.IsValid)
            {
                AgentMaster agentMaster = new AgentMaster();
                AgentManager agentManager = new AgentManager();
                agentMaster = agentManager.GetAgentMasterId(model.AgentMasterId);
                if (agentMaster != null)
                {
                    AgentMasters.AgentMasterId = model.AgentMasterId;
                    AgentMasters.AgentCode = model.AgentCode;
                    AgentMasters.AgentFullName = model.AgentFullName;
                    AgentMasters.AgentShortName = model.AgentShortName;
                    AgentMasters.Currency = model.Currency;
                    AgentMasters.CommisionCriteriaId = model.CommisionCriteriaId;
                    AgentMasters.AddressLine1 = model.AddressLine1;
                    AgentMasters.AddressLine2 = model.AddressLine2;
                    AgentMasters.AddressLine3 = model.AddressLine3;
                    AgentMasters.Pincode = model.Pincode;
                    AgentMasters.CreatedDate = agentMaster.CreatedDate;
                    AgentMasters.UpdatedDate = DateTime.Now;
                    AgentMasters.ContactPerson = model.ContactPerson;
                    AgentMasters.MobileNo = model.MobileNo;
                    AgentMasters.EmailID = model.EmailID;
                    AgentMasters.CountryMasterId = model.CountryMasterId;
                    AgentMasters.CommisionPercentage = model.CommisionPercentage;
                    agentManager.Put(AgentMasters);
                }
                else
                {
                    return Json(AgentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(AgentMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int AgentMasterId)
        {
            AgentMaster agentMaster = new AgentMaster();
            string status = "";
            AgentManager agentManager = new AgentManager();
            agentMaster = agentManager.GetAgentMasterId(AgentMasterId);
            if (agentMaster != null)
            {
                status = "Success";
                agentManager.Delete(agentMaster.AgentMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<AgentMaster> agentmasterList = new List<AgentMaster>();
            AgentManager agentManager = new AgentManager();
            agentmasterList = agentManager.Get();
            agentmasterList = agentmasterList.Where(x => x.AgentFullName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.AgentCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.AgentShortName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            AgentMasterModel vm = new AgentMasterModel();
            vm.AgentMastetlist = agentmasterList;
            return PartialView("Partial/AgentMasterGrid", vm);
        }
        #endregion


    }
}
