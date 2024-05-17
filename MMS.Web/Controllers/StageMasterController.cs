using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.StageMaser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class StageMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult StageMaster()
        {
            StageMasterModel vm = new StageMasterModel();
            return View(vm);
        }
        public ActionResult StageMaserGrid()
        {
            StageMasterModel vm = new StageMasterModel();
            StageMasterManager stageMasterManager = new StageMasterManager();
            vm.StageMasterList = stageMasterManager.Get();

            return PartialView("Partial/StageMaserGrid", vm);
        }
        [HttpPost]
        public ActionResult StageMasteDetails(int StageMasterId)
        {
            StageMasterManager stageMasterManager = new StageMasterManager();
            StageMaster stageMaster = new StageMaster();
            StageMasterModel model = new StageMasterModel();
            stageMaster = stageMasterManager.GetStageMasterId(StageMasterId);

            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getStageMasterCodeId();
            ID++;

            if (stageMaster != null)
            {
                model.StageMasterId = stageMaster.StageMasterId;
                model.OrderType = stageMaster.OrderType;
                model.IsInspection = stageMaster.IsInspection;
                model.ProcessCode = stageMaster.ProcessCode;
                model.StageCode = stageMaster.StageCode;
                model.StageName = stageMaster.StageName;
                model.SubStage = stageMaster.SubStage;
                model.CreatedDate = stageMaster.CreatedDate;
                model.UpdatedDate = stageMaster.UpdatedDate;
                model.CreatedBy = stageMaster.CreatedBy;
                model.UpdatedBy = stageMaster.UpdatedBy;
            }
            if (model.StageMasterId != 0)
            {
                model.StageCode = stageMaster.StageCode;
            }
            else
            {
                model.StageCode = "SM" + ID;
            }
            return PartialView("Partial/StageMasteDetails", model);
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
            if (stageMaster == null && model.StageMasterId == 0)
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
                ID++;
                model.StageCode = "SM" + ID;
                model.StageName = "";
                model.SubStage = "";
                model.StageMasterList = stageMasterManager.GetStageProcessId(model.ProcessCode);
                ID++;
                //  return Json(new { items = StageMasters }, JsonRequestBehavior.AllowGet);
                return PartialView("~/Views/Jobwork/Job_Master/StageMaster/Partial/Stagegrid_sequence.cshtml", model);

            }
            if ( model.StageMasterId !=0)
            {
                StageMasters.StageMasterId = model.StageMasterId;
                StageMasters.OrderType = model.OrderType;
                StageMasters.IsInspection = model.IsInspection;
                StageMasters.ProcessCode = model.ProcessCode;
                StageMasters.StageCode = model.StageCode;
                StageMasters.StageName = model.StageName;
                StageMasters.SubStage = model.SubStage;
                StageMasters.Sequence = model.Sequence;
                stageMasterManager.Put(StageMasters);

                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getStageMasterCodeId();
                ID++;
                model.StageCode = "SM" + ID;
                model.StageName = "";
                model.SubStage = "";
                model.StageMasterList = stageMasterManager.GetStageProcessId(model.ProcessCode);
                ID++;
                //  return Json(new { items = StageMasters }, JsonRequestBehavior.AllowGet);
                return PartialView("~/Views/Jobwork/Job_Master/StageMaster/Partial/Stagegrid_sequence.cshtml", model);
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
                StageMasters.IsInspection = model.IsInspection;
                StageMasters.ProcessCode = model.ProcessCode;
                StageMasters.StageCode = model.StageCode;
                StageMasters.StageName = model.StageName;
                StageMasters.SubStage = model.SubStage;
                StageMasters.Sequence = model.Sequence;
                StageMasters.UpdatedDate = DateTime.Now;
                StageMasters.CreatedBy = stageMaster.CreatedBy;
                StageMasters.UpdatedBy = stageMaster.UpdatedBy;
                stageMasterManager.Put(StageMasters);
            }
            return Json(StageMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update_stageseq(StageMasterModel model)
        {
            StageMaster StageMasters = new StageMaster();
            StageMasterManager stageMasterManager = new StageMasterManager();
            string[] intArray = model.OrderType.Split(',');


            foreach (var item in intArray)
            {
               string[] sizearry = item.Split(';');
                if (sizearry[0] != "," && sizearry[1] != "," && sizearry[1] != ",")
                {
                    string a = sizearry[0];
                string b = sizearry[1];
                
                    StageMasters.Sequence = Convert.ToInt32(a);
                    StageMasters.StageMasterId = Convert.ToInt32(b);
                    stageMasterManager.Putsedquence(StageMasters);
                }
            }
            //StageMaster StageMasters = new StageMaster();
            //StageMaster stageMaster = new StageMaster();
            //StageMasterManager stageMasterManager = new StageMasterManager();
            //stageMaster = stageMasterManager.GetStageMasterId(model.StageMasterId);
            //if (stageMaster != null)
            //{
            //    StageMasters.StageMasterId = model.StageMasterId;
            //    StageMasters.OrderType = model.OrderType;
            //    StageMasters.IsInspection = model.IsInspection;
            //    StageMasters.ProcessCode = model.ProcessCode;
            //    StageMasters.StageCode = model.StageCode;
            //    StageMasters.StageName = model.StageName;
            //    StageMasters.SubStage = model.SubStage;
            //    StageMasters.Sequence = model.Sequence;
            //    StageMasters.UpdatedDate = DateTime.Now;
            //    StageMasters.CreatedBy = stageMaster.CreatedBy;
            //    StageMasters.UpdatedBy = stageMaster.UpdatedBy;
            //    stageMasterManager.Put(StageMasters);
            //}
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

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<StageMaster> stageMasterList = new List<StageMaster>();
            StageMasterManager stageMasterManager = new StageMasterManager();
            stageMasterList = stageMasterManager.Get();
            stageMasterList = stageMasterList.Where(x => x.StageName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.StageCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.OrderType.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            StageMasterModel vm = new StageMasterModel();
            vm.StageMasterList = stageMasterList;
            return PartialView("Partial/StageMaserGrid", vm);
        }
        #endregion

    }
}
