using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.StageMaser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.JobWork
{

    [CustomFilter]
    public class StageMaster_JobController : Controller
    {
        //
        // GET: /StageMaster_Job/

        #region Accounts View

        [HttpGet]
        public ActionResult StageMaster()
        {
            StageMasterModel vm = new StageMasterModel();
            return View("~/Views/Jobwork/Job_Master/StageMaster/StageMaster.cshtml", vm);
        }
        public ActionResult StageMaserGrid()
        {
            StageMasterModel vm = new StageMasterModel();
            StageMasterManager stageMasterManager = new StageMasterManager();
            vm.StageMasterList = stageMasterManager.Get().GroupBy(x=>x.ProcessCode).Select(g => g.First()).ToList();



            return PartialView("~/Views/Jobwork/Job_Master/StageMaster/Partial/StageMaserGrid.cshtml", vm);
            
        }
        [HttpGet]
        public ActionResult StageMasteDetails(int ProcessCode_,string OrderType)
        {
            StageMasterManager stageMasterManager = new StageMasterManager();
            StageMaster stageMaster = new StageMaster();
            StageMasterModel model = new StageMasterModel();
        //    stageMaster = stageMasterManager.GetStageMasterId(StageMasterId);

            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getStageMasterCodeId();

            int Process = MMS.Web.ExtensionMethod.HtmlHelper.getStageMaster_ProcessId();
            ID++;
            Process++;
            if (ProcessCode_ != 0)
            {
                List<StageMasterModel> StageMasterModelList = new List<StageMasterModel>();
                model .StageMasterList= stageMasterManager.GetStageProcessId(ProcessCode_);
                model.ProcessCode = ProcessCode_;
                model.StageCode = "SM" + ID;
                var VALUE = stageMasterManager.GetStageProcessId(ProcessCode_).FirstOrDefault();
                model.OrderType = VALUE.OrderType;
                //   model.orderType = Convert.ToInt32(VALUE.OrderType);


                //List<SelectListItem> Provinces = new List<SelectListItem>();
                //Provinces.Add(new SelectListItem() { Text = "Please select ...", Value = "0" });
                //Provinces.Add(new SelectListItem() { Text = "Full Shoes", Value = "1" });
                //Provinces.Add(new SelectListItem() { Text = "Upper", Value = "2" });

                //ViewBag.Provinces = Provinces;

                return PartialView("~/Views/Jobwork/Job_Master/StageMaster/Partial/StageMasteDetails.cshtml", model);
            }
            if (model.StageMasterId != 0)
            {
                model.StageCode = stageMaster.StageCode;
                
            }
            else
            {
                model.StageCode = "SM" + ID;
                model.ProcessCode = Process;
              
            }

            return PartialView("~/Views/Jobwork/Job_Master/StageMaster/Partial/StageMasteDetails.cshtml", model);

        }
        [HttpPost]
        public ActionResult StageMasteDetails_sequence(int StageMasterId)
        {
            StageMasterManager stageMasterManager = new StageMasterManager();
            StageMaster stageMaster = new StageMaster();
            StageMasterModel model = new StageMasterModel();
              stageMaster = stageMasterManager.GetStageMasterId(StageMasterId);

           // int ID = MMS.Web.ExtensionMethod.HtmlHelper.getStageMasterCodeId();



            return Json(stageMaster, JsonRequestBehavior.AllowGet);

        }
        public ActionResult StageMasteDetails_sequence_ProcessCode(int ProcessCode)
        {
            StageMasterManager stageMasterManager = new StageMasterManager();
            StageMaster stageMaster = new StageMaster();
            StageMasterModel model = new StageMasterModel();
            stageMaster = stageMasterManager.GetStageProcessId(ProcessCode).FirstOrDefault();

            // int ID = MMS.Web.ExtensionMethod.HtmlHelper.getStageMasterCodeId();



            return Json(stageMaster, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult StageMaster(StageMasterModel model)
        {
            StageMaster StageMasters = new StageMaster();
            StageMaster stageMaster = new StageMaster();
            StageMasterManager stageMasterManager = new StageMasterManager();
            stageMaster = stageMasterManager.GetStageName(model.StageName);
            if (stageMaster == null)
            {
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getStageMasterCodeId();
                ID++;

                StageMasters.OrderType = model.OrderType;
                StageMasters.IsInspection = model.IsInspection;
                StageMasters.ProcessCode = model.ProcessCode;
                StageMasters.StageCode = "SM" + ID;
                StageMasters.StageName = model.StageName;
                StageMasters.SubStage = model.SubStage;
                StageMasters.Sequence = model.Sequence;
                StageMasters.CreatedDate = DateTime.Now;
                stageMasterManager.Post(StageMasters);
            }
            else if (stageMaster != null && stageMaster.StageName == model.StageName)
            {
                StageMasters.StageMasterId = 0;
                return Json(StageMasters, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(StageMasters, JsonRequestBehavior.AllowGet);
            }


            return Json(StageMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(StageMasterModel model)
        {
            StageMaster StageMasters = new StageMaster();
            StageMaster stageMaster = new StageMaster();
            StageMasterManager stageMasterManager = new StageMasterManager();
            stageMaster = stageMasterManager.GetStageMasterId(model.StageMasterId);
            if (stageMaster != null)
            {
                StageMasters.StageMasterId = model.StageMasterId;
                StageMasters.OrderType = model.OrderType;
                StageMasters.Sequence = model.Sequence;
                StageMasters.IsInspection = model.IsInspection;
                StageMasters.ProcessCode = model.ProcessCode;
                StageMasters.StageCode = model.StageCode;
                StageMasters.StageName = model.StageName;
                StageMasters.SubStage = model.SubStage;
                StageMasters.UpdatedDate = DateTime.Now;
                StageMasters.CreatedBy = stageMaster.CreatedBy;
                StageMasters.UpdatedBy = stageMaster.UpdatedBy;
                stageMasterManager.Put(StageMasters);
            }
            return Json(StageMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int StageMasterId)
        {
            StageMaster stageMaster = new StageMaster();
            string status = "";
            StageMasterManager stageMasterManager = new StageMasterManager();
            stageMaster = stageMasterManager.GetStageMasterId(StageMasterId);
            if (stageMaster != null)
            {
                status = "Success";
                stageMasterManager.Delete(stageMaster.StageMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        public ActionResult Updatesequence(int sequence, int StageMasterId)
        {
            StageMaster StageMasters = new StageMaster();
            StageMaster stageMaster = new StageMaster();
            StageMasterManager stageMasterManager = new StageMasterManager();
            stageMaster = stageMasterManager.GetStageMasterId(StageMasterId);
            if (stageMaster != null)
            {
                StageMasters.StageMasterId = StageMasterId;
                StageMasters.Sequence = sequence;
                
                stageMasterManager.Putsedquence(StageMasters);
            }
            return Json(StageMasters, JsonRequestBehavior.AllowGet);
        }
    }
}
