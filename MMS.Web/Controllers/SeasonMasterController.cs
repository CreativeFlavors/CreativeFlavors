using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SeasonMasterModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class  SeasonMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SeasonMaster()
        {
            SeasonMasterModel vm= new SeasonMasterModel();
            return View(vm);
        }
        public ActionResult  SeasonMasterGrid()
        {
             SeasonMasterModel vm= new SeasonMasterModel();
             SeasonManager seasonManager = new SeasonManager();
             vm.SeasonMasterList = seasonManager.Get();
            return PartialView("Partial/SeasonMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult SeasonMasterDetails(int SeasonMasterId)
        {
            SeasonManager seasonManager = new SeasonManager();
            SeasonMaster seasonMaster = new SeasonMaster();
            SeasonMasterModel model = new SeasonMasterModel();
            seasonMaster = seasonManager.GetSeasonMasterId(SeasonMasterId);
            if (seasonMaster != null)
            {
                model.SeasonMasterId = seasonMaster.SeasonMasterId;
                model.SeasonName = seasonMaster.SeasonName;
                model.SpringSummerYear = seasonMaster.SpringSummerYear;
                model.SpringDescription = seasonMaster.SpringDescription;
                model.PeriodFrom = Convert.ToDateTime(seasonMaster.PeriodFrom).Date.ToString("dd/MM/yyyy");
                model.PeriodTo = Convert.ToDateTime(seasonMaster.PeriodTo).Date.ToString("dd/MM/yyyy");
                model.CreatedBy = seasonMaster.CreatedBy;
                model.UpdatedBy = seasonMaster.UpdatedBy;
                model.CreatedDate = seasonMaster.CreatedDate;
                model.UpdatedDate = seasonMaster.UpdatedDate;
            }
            return PartialView("Partial/SeasonMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SeasonMaster(SeasonMasterModel model)
        {
            SeasonMaster SeasonMasters = new SeasonMaster();
            SeasonMaster seasonMaster = new SeasonMaster();
           
                //SeasonMaster seasonMaster = new SeasonMaster();
                SeasonManager seasonManager = new SeasonManager();
                seasonMaster = seasonManager.GetSeasonFullName(model.SeasonName);
                if (seasonMaster  == null)
                {
                    SeasonMasters.SeasonMasterId = model.SeasonMasterId;
                    SeasonMasters.SeasonName = model.SeasonName;
                    SeasonMasters.SpringSummerYear = model.SpringSummerYear;
                    SeasonMasters.SpringDescription = model.SpringDescription;
                var format = "dd/MM/yyyy";
                DateTime Fromdate = DateTime.ParseExact(model.PeriodFrom, format, CultureInfo.InvariantCulture);
                DateTime Todate = DateTime.ParseExact(model.PeriodTo, format,  CultureInfo.InvariantCulture);
                    SeasonMasters.PeriodFrom = Fromdate.Date;
                    SeasonMasters.PeriodTo = Todate.Date;                  
                    SeasonMasters.CreatedDate = DateTime.Now ;
                    seasonManager.Post(SeasonMasters);
                }
                else if (seasonMaster != null && model.SeasonName == seasonMaster.SeasonName&&model.SeasonMasterId==0)
                {
                    seasonMaster.SeasonMasterId = 0;
                    return Json(seasonMaster, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(seasonMaster, JsonRequestBehavior.AllowGet);
                }

            
            return Json(SeasonMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SeasonMasterModel model)
        {
            SeasonMaster SeasonMasters = new SeasonMaster();
            SeasonMaster seasonMaster = new SeasonMaster();
           
                 SeasonManager seasonManager = new SeasonManager();
                 seasonMaster = seasonManager.GetSeasonMasterId(model.SeasonMasterId);
                if (seasonMaster != null)
                {
                    SeasonMasters.SeasonMasterId = model.SeasonMasterId;
                    SeasonMasters.SeasonName = model.SeasonName;
                    SeasonMasters.SpringSummerYear = model.SpringSummerYear;
                    SeasonMasters.SpringDescription = model.SpringDescription;
                var format = "dd/MM/yyyy";
                DateTime Fromdate = DateTime.ParseExact(model.PeriodFrom, format, CultureInfo.InvariantCulture);
                DateTime Todate = DateTime.ParseExact(model.PeriodTo, format, CultureInfo.InvariantCulture);
                    SeasonMasters.PeriodFrom = Fromdate.Date;
                    SeasonMasters.PeriodTo = Todate.Date;     
                    seasonManager.Put(SeasonMasters);
                }
                else
                {
                    return Json(SeasonMasters, JsonRequestBehavior.AllowGet);
                }

            
            return Json(SeasonMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SeasonMasterId)
        {
            SeasonMaster  seasonMaster = new SeasonMaster();
            string status = "";
            SeasonManager seasonManager = new SeasonManager();
            seasonMaster = seasonManager.GetSeasonMasterId(SeasonMasterId);
            if (seasonMaster != null)
            {
                status = "Success";
                seasonManager.Delete(seasonMaster.SeasonMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SeasonMaster> seasonMasterList = new List<SeasonMaster>();
            SeasonManager seasonManager = new SeasonManager();
            seasonMasterList  = seasonManager.Get();
            seasonMasterList = seasonMasterList.Where(x => x.SeasonName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.SpringSummerYear.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            SeasonMasterModel sm = new SeasonMasterModel();
            sm.SeasonMasterList = seasonMasterList;
            return PartialView("Partial/SeasonMasterGrid", sm);
        }
        #endregion


    }
}
