using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.NormsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class NormsMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult NormsMaster()
        {
            NormsModel vm = new NormsModel();
            return View(vm);
        }
        public ActionResult NormsGrid()
        {
            NormsModel vm = new NormsModel();
            NormsManager normsManager = new NormsManager();
            vm.normsList = normsManager.Get();
            return PartialView("Partial/NormsGrid", vm);
        }
       
        [HttpPost]
        public ActionResult NormsDetails(int NormsID)
        {
            NormsManager normsManager = new NormsManager();
            Norms norms = new Norms();
            NormsModel model = new NormsModel();
            norms = normsManager.GetNormsID(NormsID);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getNormsCodeId();
            if (norms != null)
            {
                model.GroupId = norms.GroupId;
                model.BuyerNameid = norms.BuyerNameid;
                model.BuyerOption = norms.BuyerOption;
                model.IssueNormsid = norms.IssueNormsid;
                model.PurchaseNormsid = norms.PurchaseNormsid;
                model.CostingNorms = norms.CostingNorms;
                model.OurNorms = norms.OurNorms;
                model.Normsid = norms.Normsid;
                model.NormsPercentage = norms.NormsPercentage;
                model.NormsPercentage1 = norms.NormsPercentage1;
                model.NormsPercentage2 = norms.NormsPercentage2;
                model.NormsPercentage3 = norms.NormsPercentage3;
                model.CreatedBy = norms.CreatedBy;
                model.UpdatedBy = norms.UpdatedBy;
               

            }
           
            return PartialView("Partial/NormsDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult NormsMaster(NormsModel model)
        {
            Norms norms = new Norms();

            string Message = "";
            if (ModelState.IsValid)
            {
                Norms norm = new Norms();
                Norms nors = new Norms();
                NormsManager normsManager = new NormsManager();
                if(model.Normsid==0)
                {
                    norm = normsManager.GetGroupID(model.GroupId);
                }
              
                if (norm == null||model.Normsid!=0)
                {
                    norms.GroupId = model.GroupId;
                    norms.BuyerNameid = model.BuyerNameid;
                    norms.BuyerOption = model.BuyerOption;
                    norms.IssueNormsid = model.IssueNormsid;
                    norms.PurchaseNormsid = model.PurchaseNormsid;
                    norms.CostingNorms = model.CostingNorms;
                    norms.OurNorms = model.OurNorms;
                    norms.Normsid = model.Normsid;
                    norms.NormsPercentage = model.NormsPercentage;
                    norms.NormsPercentage1 = model.NormsPercentage1;
                    norms.NormsPercentage2 = model.NormsPercentage2;
                    norms.NormsPercentage3 = model.NormsPercentage3;
                    norms.CreatedDate = DateTime.Now;

                    nors = normsManager.Post(norms);
                }
                if (norm != null)
                {
                    Message = "Already Exist";
                }
                if (nors.Normsid != 0 && model.Normsid == 0)
                {
                    Message = "Saved Successfully";
                }
                else if (nors.Normsid != 0 && model.Normsid != 0)
                {
                    Message = "Updated Successfully";
                }

            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int NormsID)
        {
            Norms norms = new Norms();
            string status = "";
            NormsManager normsManager = new NormsManager();
            norms = normsManager.GetNormsID(NormsID);
            if (norms != null)
            {
                status = "Success";
                normsManager.Delete(norms.Normsid);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<Norms> normsList = new List<Norms>();
            NormsManager normsManager = new NormsManager();
            normsList = normsManager.Get();
   
            NormsModel model = new NormsModel();
            model.normsList = normsList;
            return PartialView("Partial/NormsGrid", model);
        }
        #endregion
    }
}
