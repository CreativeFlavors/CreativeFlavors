using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.Substance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SubstanceMasterController : Controller
    {
        //
        // GET: /SubstanceMaster/
        #region Substance View

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Substance()
        {
            SubstanceMasterModel sm = new SubstanceMasterModel();
            return View(sm);
        }

        public ActionResult SubstanceMasterGrid()
        {
            SubstanceMasterModel sm = new SubstanceMasterModel();
            SubstanceMasterManager substanceManager = new SubstanceMasterManager();
            sm.SubstanceMasterList = substanceManager.Get();

            return PartialView("Partial/SubstanceMasterGrid", sm);
        }

        [HttpPost]
        public ActionResult SubstanceMasterDetails(int SubstanceMasterId)
        {
            SubstanceMasterManager substanceManager = new SubstanceMasterManager();
            SubstanceMaster substanceMaster = new SubstanceMaster();
            SubstanceMasterModel model = new SubstanceMasterModel();
            substanceMaster = substanceManager.GetsubstanceMasterId(SubstanceMasterId);
            if (substanceMaster.SubstanceMasterId != 0)
            {
                model.SubstanceRange = substanceMaster.SubstanceRange;
                model.CreatedDate = substanceMaster.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;

                model.SubstanceMasterId = substanceMaster.SubstanceMasterId;
                
            }
            return PartialView("Partial/SubstanceMasterDetails", model);
        }

         #endregion

        #region Curd Operation

        [HttpPost]
        public ActionResult Substance(SubstanceMasterModel sm)
        {
            
            SubstanceMaster sMaster = new SubstanceMaster();
            if (ModelState.IsValid)
            {
               
                SubstanceMasterManager substanceManager = new SubstanceMasterManager();
                
                    sMaster.SubstanceRange = sm.SubstanceRange;
                    sMaster.CreatedDate = DateTime.Now;
                    sMaster.UpdatedDate = DateTime.Now;
                    substanceManager.Post(sMaster);
               
            }
            return Json(sMaster, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(SubstanceMasterModel model)
        {
            SubstanceMaster substanceMaster = new SubstanceMaster();
            if (ModelState.IsValid)
            {
                SubstanceMaster sMaster = new SubstanceMaster();
                SubstanceMasterManager substanceManager = new SubstanceMasterManager();
                sMaster = substanceManager.GetsubstanceMasterId(model.SubstanceMasterId);
                if (sMaster != null)
                {
                    substanceMaster.SubstanceRange = model.SubstanceRange;
                    substanceMaster.SubstanceMasterId = model.SubstanceMasterId;

                    substanceMaster.CreatedDate = sMaster.CreatedDate;
                    substanceMaster.UpdatedDate = DateTime.Now;

                    substanceManager.Put(substanceMaster);
                }
                else
                {
                    return Json(substanceMaster, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(substanceMaster, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int SubstanceMasterId)
        {
            SubstanceMaster substanceMaster = new SubstanceMaster();
            string status = "";
            SubstanceMasterManager substanceManager = new SubstanceMasterManager();
            substanceMaster = substanceManager.GetsubstanceMasterId(SubstanceMasterId);
            if (substanceMaster != null)
            {
                status = "Success";
                substanceManager.Delete(substanceMaster.SubstanceMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SubstanceMaster> substanceMasterlist = new List<SubstanceMaster>();
            SubstanceMasterManager substanceManager = new SubstanceMasterManager();
            substanceMasterlist = substanceManager.Get();
            substanceMasterlist = substanceMasterlist.ToList();
            SubstanceMasterModel model = new SubstanceMasterModel();
            model.SubstanceMasterList = substanceMasterlist;
            return PartialView("Partial/SubstanceMasterGrid", model);
        }
        #endregion
    }
}
