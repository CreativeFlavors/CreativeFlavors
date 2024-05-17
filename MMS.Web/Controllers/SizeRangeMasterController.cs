using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SizeRangeMasterModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SizeRangeMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SizeRangeMaster()
        {
            SizeRangeMasterModel vm = new SizeRangeMasterModel();
            return View(vm);
        }
        public ActionResult SizeRangeMasterGrid()
        {
            SizeRangeMasterModel vm = new SizeRangeMasterModel();
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            vm.SizeRangeMasterList = sizeRangeMasterManager.Get().ToList();

            return PartialView("Partial/SizeRangeMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult SizeRangeMasterDetails(int SizeRangeMasterId)
        {
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            List<SizeRangeMaster> sizeRangeMasterlist = new List<SizeRangeMaster>();
            SizeRangeMaster sizeRangeMaster = new SizeRangeMaster();
            SizeRangeMasterModel model = new SizeRangeMasterModel();
            if (SizeRangeMasterId == 0)
            {
                sizeRangeMaster = sizeRangeMasterManager.GetSizeRangeMasterId(SizeRangeMasterId);
                if (sizeRangeMaster != null)
                {
                    model.SizeRangeMasterId = sizeRangeMaster.SizeRangeMasterId;
                    model.SizeRangeRef = sizeRangeMaster.SizeRangeRef;
                    model.SizeRangeRefValue = sizeRangeMaster.SizeRangeRefValue;
                    model.CreatedDate = sizeRangeMaster.CreatedDate;
                    model.UpdatedDate = sizeRangeMaster.UpdatedDate;
                    model.CreatedBy = sizeRangeMaster.CreatedBy;
                    model.UpdatedBy = sizeRangeMaster.UpdatedBy;
                }
            }
            else
            {
                sizeRangeMasterlist = sizeRangeMasterManager.Get().Where(x => x.SizeRangeRef == Convert.ToString(SizeRangeMasterId)).ToList();
                model.SizeRangeMasterList = sizeRangeMasterlist.OrderBy(x=>decimal.Parse(x.SizeRangeRefValue)).ToList();
            }
            return PartialView("Partial/SizeRangeMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SizeRangeMaster(string jsonOfLog, SizeRange model)
        {
            var PackingDetail = JsonConvert.DeserializeObject<List<SizeRange>>(jsonOfLog);
            SizeRangeMaster sizeRangeMaster = new SizeRangeMaster();
            SizeRangeMaster sizeRangeMasters = new SizeRangeMaster();
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            sizeRangeMasters.SizeRangeRef = PackingDetail.Last().SizeRangeRefValue_;
            if (PackingDetail.Select(x => x.SizeRangeRefValue_).Any() == true)
            {
                PackingDetail.Select(x => x.SizeRangeRefValue_);
            }
            sizeRangeMaster = sizeRangeMasterManager.GetSizeRangeRef(sizeRangeMasters.SizeRangeRef);

            if (sizeRangeMaster == null)
            {

                foreach (var items in PackingDetail.OrderBy(x => x.SizeRangeRefValue_))
                {
                    sizeRangeMasters.SizeRangeRef = PackingDetail.Last().SizeRangeRefValue_;
                    if (PackingDetail.Select(x => x.SizeRangeRefValue_).Any() == true)
                    {
                        PackingDetail.Select(x => x.SizeRangeRefValue_);
                    }
                    sizeRangeMasters.SizeRangeRefValue = items.SizeRangeRef_;
                    sizeRangeMasters.CreatedDate = DateTime.Now;

                    if (sizeRangeMaster == null)
                    {
                        sizeRangeMasterManager.SizeRangeMaster(sizeRangeMasters);
                    }

                }
            }
            else if (sizeRangeMaster != null)
            {
                List<SizeRangeMaster> listSizeRangeMaster = new List<SizeRangeMaster>().ToList();
                listSizeRangeMaster = sizeRangeMasterManager.Get().Where(x => x.SizeRangeRef == sizeRangeMaster.SizeRangeRef).ToList();
                foreach (var item in listSizeRangeMaster)
                {
                    sizeRangeMasterManager.Delete(item.SizeRangeMasterId);
                }
                foreach (var items in PackingDetail.OrderBy(x => x.SizeRangeRefValue_))
                {
                    sizeRangeMasters.SizeRangeRef = PackingDetail.Last().SizeRangeRefValue_;
                    if (PackingDetail.Select(x => x.SizeRangeRefValue_).Any() == true)
                    {
                        PackingDetail.Select(x => x.SizeRangeRefValue_);
                    }
                    sizeRangeMasters.SizeRangeRefValue = items.SizeRangeRef_;
                    sizeRangeMasters.CreatedDate = DateTime.Now;
                    sizeRangeMasterManager.SizeRangeMaster(sizeRangeMasters);
                }

            }

            return Json(sizeRangeMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SizeRangeMasterModel model)
        {
            SizeRangeMaster sizeRangeMaster = new SizeRangeMaster();
            SizeRangeMaster sizeRangeMasters = new SizeRangeMaster();
            if (ModelState.IsValid)
            {
                SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
                sizeRangeMaster = sizeRangeMasterManager.GetSizeRangeMasterId(model.SizeRangeMasterId);
                if (sizeRangeMaster != null)
                {
                    sizeRangeMasters.SizeRangeMasterId = model.SizeRangeMasterId;
                    sizeRangeMasters.SizeRangeRef = model.SizeRangeRef;
                    sizeRangeMasters.SizeRangeRefValue = model.SizeRangeRefValue;
                    sizeRangeMasters.CreatedDate = sizeRangeMaster.CreatedDate;
                    sizeRangeMasters.UpdatedDate = DateTime.Now;
                    sizeRangeMasters.CreatedBy = sizeRangeMaster.CreatedBy;
                    sizeRangeMasters.UpdatedBy = sizeRangeMaster.UpdatedBy;
                    sizeRangeMasterManager.Put(sizeRangeMasters);
                }
                else
                {
                    return Json(sizeRangeMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(sizeRangeMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SizeRangeMasterId)
        {
            List<SizeRangeMaster> sizeRangeMasterlist = new List<SizeRangeMaster>();
            string status = "";
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            sizeRangeMasterlist = sizeRangeMasterManager.Get().Where(x => x.SizeRangeRef == Convert.ToString(SizeRangeMasterId)).ToList();
            if (sizeRangeMasterlist != null)
            {
                foreach (var item in sizeRangeMasterlist)
                {
                    sizeRangeMasterManager.Delete(item.SizeRangeMasterId);
                }
                status = "Success";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SizeRangeMaster> sizeRangeMasterlist = new List<SizeRangeMaster>();
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            List<SizeRangeDetails> sizeRangeDetailsList = new List<SizeRangeDetails>();
            SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
            sizeRangeMasterlist = sizeRangeMasterManager.Get();
            sizeRangeDetailsList = sizeRangeDetailsManager.Get();

            var sizeRangeMasterDetailsList =   from X in sizeRangeMasterlist 
                                               join Y in sizeRangeDetailsList on (Convert.ToInt32(X.SizeRangeRef)) equals Y.SizeRangeDetailsId
                                               where Y.SizeRangeName.ToLower().Trim().Contains(filter.ToLower().Trim())
                                               select X;
            sizeRangeMasterlist = sizeRangeMasterDetailsList.ToList(); 
            SizeRangeMasterModel model = new SizeRangeMasterModel();
            model.SizeRangeMasterList = sizeRangeMasterlist;
            return PartialView("Partial/SizeRangeMasterGrid", model);
        }

        [HttpPost]
        public ActionResult GetSizeRangeValue(SizeRangeMasterModel model)
        {
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            List<SizeRangeMaster> sizeRangeMasterlist = new List<SizeRangeMaster>();
            SizeRangeMaster sizeRangeMaster = new SizeRangeMaster();
            if (model.SizeRangeRef != "" && model.SizeRangeRefValue != "")
            {
                sizeRangeMasterlist = sizeRangeMasterManager.Get().Where(x => x.SizeRangeRef == model.SizeRangeRef.ToString()).ToList();
            }
                bool isExist = sizeRangeMasterlist.Any(x => x.SizeRangeRefValue.Contains(model.SizeRangeRefValue));

                return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}
