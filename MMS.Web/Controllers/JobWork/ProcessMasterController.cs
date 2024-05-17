using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Repository.Managers.JobWork;
using MMS.Web.Models;
using MMS.Web.Models.ColorMasterModel;
using MMS.Web.Models.JobworkModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class ProcessMasterController : Controller
    {
        //
        // GET: /ProcessMaster/
        [HttpGet]
        public ActionResult ProcessMaster()
        {
            MMS.Web.Models.JobworkModel.ProcessModel Pm = new ProcessModel();
            return View("~/Views/Jobwork/Job_Master/ProcessMaster/ProcessMaster.cshtml", Pm);
        }
        public ActionResult ProcessGrid()
        {
            ProcessModel vm = new ProcessModel();
            ProcessManager processManager = new ProcessManager();
            vm.ProcesMasterList = processManager.Get().OrderBy(x=>x.ProcessId).ToList();

            return PartialView("~/Views/Jobwork/Job_Master/ProcessMaster/Partial/ProcessGrid.cshtml", vm);
        }
        [HttpPost]
        public ActionResult EditProcessList(int ProcessId)
        {

            ProcessManager processManager = new ProcessManager();
            ProcessMaster processMaster = new ProcessMaster();
            ProcessModel model = new ProcessModel();
            processMaster = processManager.GetprocessMasterID(ProcessId);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getProcessCodeId();


            if (processMaster != null)
            {
              //  model.ProcessCode = processMaster.ProcessCode;
                model.ProcessName = processMaster.ProcessName;
                model.Ioinvolved = processMaster.Ioinvolved;
                model.CreatedBy = processMaster.CreatedBy;
                model.UpdatedBy = processMaster.UpdatedBy;
                model.ProcessId = processMaster.ProcessId;
                model.ProcessCode = processMaster.ProcessCode;
            }
           
            else
            {
                ID++;
                model.ProcessCode = "PRO" + ID;
            }
            return PartialView("~/Views/Jobwork/Job_Master/ProcessMaster/Partial/ProcessEdit.cshtml", model);
        }

        #region Curd Operation
        [HttpPost]
        public ActionResult saveProcess(ProcessModel ProcessModel)
        {
            ProcessMaster ProcessMaster = new ProcessMaster();

            if (ModelState.IsValid)
            {
                ProcessMaster processMaster = new ProcessMaster();
                ProcessManager ProcessManager = new ProcessManager();
                //  colorMaster = colorManager.GetColor(ColorMasterModel.Color);
                //int ID = MMS.Web.ExtensionMethod.HtmlHelper.getColorCodeId();
                //ID++;
                //if (colorMaster == null)
                //{
                ProcessMaster.ProcessName = ProcessModel.ProcessName;
                ProcessMaster.ProcessCode = ProcessModel.ProcessCode;
                ProcessMaster.Ioinvolved = ProcessModel.Ioinvolved;
                ProcessMaster.CreatedDate = DateTime.Now;
                ProcessManager.Post(ProcessMaster);
            }
            
            return Json(ProcessMaster, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateProcess(ProcessModel ProcessModel)
        {
            ProcessMaster ProcessMaster = new ProcessMaster();
            if (ModelState.IsValid)
            {
                ProcessMaster processMaster = new ProcessMaster();
                ProcessManager ProcessManager = new ProcessManager();
                processMaster = ProcessManager.GetprocessMasterID(ProcessModel.ProcessId);
                if (processMaster != null)
                {
                    ProcessMaster.ProcessName = ProcessModel.ProcessName;
                    ProcessMaster.ProcessCode = ProcessModel.ProcessCode;
                    ProcessMaster.Ioinvolved = ProcessModel.Ioinvolved;
                    ProcessMaster.ProcessId = ProcessModel.ProcessId;
                    ProcessMaster.CreatedDate = ProcessModel.CreatedDate;
                    ProcessMaster.UpdatedDate = DateTime.Now;
                    ProcessManager.Put(ProcessMaster);
                }
                else
                {
                    return Json(ProcessMaster, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(ProcessMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Deleteprocess(int ProcessId)
        {
            ProcessMaster processMaster = new ProcessMaster();
            string status = "";
            ProcessManager ProcessManager = new ProcessManager();
            processMaster = ProcessManager.GetprocessMasterID(ProcessId);
            if (processMaster != null)
            {
                status = "Success";
                ProcessManager.Delete(processMaster.ProcessId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
