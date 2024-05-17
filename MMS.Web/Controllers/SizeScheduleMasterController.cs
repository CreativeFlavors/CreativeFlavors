using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.SizeRangeMasterModel;
using MMS.Web.Models.SizeScheduleMasterModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class SizeScheduleMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SizeScheduleMaster()
        {
            SizeScheduleMasterModel vm = new SizeScheduleMasterModel();
            return View(vm);
        }
        public ActionResult SizeScheduleMasterGrid()
        {
            SizeScheduleMasterModel vm = new SizeScheduleMasterModel();
            SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
            vm.SizeScheduleMasterList = sizeScheduleMasterManager.Get();

            return PartialView("Partial/SizeScheduleMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult SizeScheduleMasterDetails(int SizeScheduleMasterId)
        {
            SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
            SizeScheduleMaster sizeScheduleMaster = new SizeScheduleMaster();
            SizeScheduleMasterModel model = new SizeScheduleMasterModel();
            List<SizeScheduleRange> sizeScheduleRange = new List<SizeScheduleRange>().ToList();
            sizeScheduleMaster = sizeScheduleMasterManager.GetSizeScheduleMasterId(SizeScheduleMasterId);
            if (sizeScheduleMaster != null)
            {
                model.SizeScheduleMasterId = sizeScheduleMaster.SizeScheduleMasterId;
                model.SizeMatchingNo = sizeScheduleMaster.SizeMatchingNo;
                model.SizeRangeName = sizeScheduleMaster.SizeRangeName;
                model.FromValue = sizeScheduleMaster.FromValue;
                model.ToValue = sizeScheduleMaster.ToValue;
                model.SketchNo = sizeScheduleMaster.SketchNo;
                model.MaterialName = sizeScheduleMaster.MaterialName;
                model.ShortUnitID = sizeScheduleMaster.ShortUnitID;
                model.Equals = sizeScheduleMaster.Equals;
                model.CreatedDate = sizeScheduleMaster.CreatedDate;
                model.UpdatedDate = sizeScheduleMaster.UpdatedDate;
                model.CreatedBy = sizeScheduleMaster.CreatedBy;
                model.UpdatedBy = sizeScheduleMaster.UpdatedBy;
                sizeScheduleRange = sizeScheduleMasterManager.GetSizeRange(model.SizeScheduleMasterId);
                model.sizeScheduleRangeList = sizeScheduleRange;

            }
            return PartialView("Partial/SizeScheduleMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SizeScheduleMaster(SizeScheduleMasterModel model)
        {
            SizeScheduleMaster sizeScheduleMasters = new SizeScheduleMaster();
            var PackingDetail = JsonConvert.DeserializeObject<List<SizeRange>>(model.jsonOfLog);
            SizeScheduleRange sizeRangeMaster = new SizeScheduleRange();
            SizeScheduleRange sizeRangeMasters = new SizeScheduleRange();
            SizeRangeMasterManager sizeRangeMasterManager = new SizeRangeMasterManager();
            SizeScheduleRange range = new SizeScheduleRange();
            sizeRangeMasters.Frame = PackingDetail.Last().SizeRangeRefValue_;
            if (PackingDetail.Select(x => x.SizeRangeRefValue_).Any() == true)
            {
                PackingDetail.Select(x => x.SizeRangeRefValue_);
            }
            if (ModelState.IsValid)
            {
                SizeScheduleMaster sizeScheduleMaster = new SizeScheduleMaster();
                SizeScheduleMaster sizeScheduleMaster_ = new SizeScheduleMaster();
                SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
                sizeScheduleMaster = sizeScheduleMasterManager.GetSizeScheduleMasterId(model.SizeScheduleMasterId);
                if (sizeScheduleMaster == null)
                {
                    sizeScheduleMasters.SizeMatchingNo = model.SizeMatchingNo;
                    sizeScheduleMasters.SizeRangeName = model.SizeRangeName;
                    sizeScheduleMasters.FromValue = model.FromValue;
                    sizeScheduleMasters.SketchNo = model.SketchNo;
                    sizeScheduleMasters.MaterialName = model.MaterialName;
                    sizeScheduleMasters.ToValue = model.ToValue;
                    sizeScheduleMasters.Equals = model.Equals;
                    sizeScheduleMasters.ShortUnitID = model.ShortUnitID;
                    sizeScheduleMasters.CreatedDate = DateTime.Now;
                    sizeScheduleMaster_ = sizeScheduleMasterManager.Post(sizeScheduleMasters);
                }
                else
                {
                    sizeScheduleMasters.SizeMatchingNo = model.SizeMatchingNo;
                    sizeScheduleMasters.SizeRangeName = model.SizeRangeName;
                    sizeScheduleMasters.ShortUnitID = model.ShortUnitID;
                    sizeScheduleMasters.SketchNo = model.SketchNo;
                    sizeScheduleMasters.MaterialName = model.MaterialName;
                    sizeScheduleMasters.SizeScheduleMasterId = model.SizeScheduleMasterId;
                    sizeScheduleMaster_ = sizeScheduleMasterManager.Post(sizeScheduleMasters);
                }

                sizeRangeMaster = sizeScheduleMasterManager.GetSizeScheduleID(sizeScheduleMaster_.SizeScheduleMasterId);

                if (sizeScheduleMaster_ != null)
                {
                    int count = 0;
                    foreach (var items in PackingDetail.OrderBy(x => x.SizeRangeRefValue_))
                    {
                        SizeScheduleRange sizeScheduleRange = new SizeScheduleRange();
                        sizeScheduleRange.Frame = items.SizeRangeRefValue_;
                        sizeScheduleRange.Size = Convert.ToDecimal(items.SizeRangeRef_);
                        sizeScheduleRange.CreatedDate = DateTime.Now;

                        if (sizeRangeMaster != null && sizeRangeMaster.SizeScheduleID == 0)
                        {
                            List<SizeScheduleRange> sizeScheduleList = new List<SizeScheduleRange>();
                            if (sizeRangeMaster != null && sizeRangeMaster.SizeScheduleMasterID != 0)
                            {
                                sizeScheduleList = sizeScheduleMasterManager.GetSizeRange(sizeRangeMaster.SizeScheduleMasterID);
                            }
                            if (count == 0)
                            {
                                foreach (var item in sizeScheduleList)
                                {
                                    sizeScheduleMasterManager.SizeScheduleRangeDelete(item.SizeScheduleID);
                                    count++;
                                }
                            }
                            if (sizeScheduleMaster_.SizeScheduleMasterId == 0)
                            {
                                sizeScheduleRange.SizeScheduleMasterID = sizeScheduleMaster.SizeScheduleMasterId;
                            }
                            else
                            {
                                sizeScheduleRange.SizeScheduleMasterID = sizeScheduleMaster_.SizeScheduleMasterId;
                            }
                            sizeScheduleMasterManager.SizeScheduleSave(sizeScheduleRange);
                        }
                        else
                        {
                            List<SizeScheduleRange> sizeScheduleList = new List<SizeScheduleRange>();
                            if (sizeRangeMaster != null && sizeRangeMaster.SizeScheduleMasterID != 0)
                            {
                                sizeScheduleList = sizeScheduleMasterManager.GetSizeRange(sizeRangeMaster.SizeScheduleMasterID);
                            }

                            if (count == 0)
                            {
                                foreach (var item in sizeScheduleList)
                                {
                                    sizeScheduleMasterManager.SizeScheduleRangeDelete(item.SizeScheduleID);
                                    count++;
                                }
                            }

                            if (sizeScheduleMaster_.SizeScheduleMasterId == 0)
                            {
                                sizeScheduleRange.SizeScheduleMasterID = sizeScheduleMaster.SizeScheduleMasterId;
                            }
                            else
                            {
                                sizeScheduleRange.SizeScheduleMasterID = sizeScheduleMaster_.SizeScheduleMasterId;
                            }
                            range = sizeScheduleMasterManager.SizeScheduleSave(sizeScheduleRange);

                        }

                    }
                }
            }
            return Json(range, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SizeScheduleMasterModel model)
        {
            SizeScheduleMaster sizeScheduleMasters = new SizeScheduleMaster();
            if (ModelState.IsValid)
            {
                SizeScheduleMaster sizeScheduleMaster = new SizeScheduleMaster();
                SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
                sizeScheduleMaster = sizeScheduleMasterManager.GetSizeScheduleMasterId(model.SizeScheduleMasterId);
                if (sizeScheduleMaster != null)
                {
                    sizeScheduleMasters.SizeScheduleMasterId = model.SizeScheduleMasterId;
                    sizeScheduleMasters.SizeMatchingNo = model.SizeMatchingNo;
                    sizeScheduleMasters.SizeRangeName = model.SizeRangeName;
                    sizeScheduleMasters.FromValue = model.FromValue;
                    sizeScheduleMasters.ShortUnitID = model.ShortUnitID;
                    sizeScheduleMasters.ToValue = model.ToValue;
                    sizeScheduleMaster.SketchNo = model.SketchNo;
                    sizeScheduleMaster.MaterialName = model.MaterialName;
                    sizeScheduleMasters.Equals = model.Equals;
                    sizeScheduleMasters.CreatedDate = sizeScheduleMaster.CreatedDate;
                    sizeScheduleMasters.UpdatedDate = DateTime.Now;
                    sizeScheduleMasters.CreatedBy = sizeScheduleMaster.CreatedBy;
                    sizeScheduleMasters.UpdatedBy = sizeScheduleMaster.UpdatedBy;
                    sizeScheduleMasterManager.Post(sizeScheduleMasters);
                }
                else
                {
                    return Json(sizeScheduleMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(sizeScheduleMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int SizeScheduleMasterId)
        {
            SizeScheduleMaster sizeScheduleMaster = new SizeScheduleMaster();
            string status = "";
            SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
            sizeScheduleMaster = sizeScheduleMasterManager.GetSizeScheduleMasterId(SizeScheduleMasterId);
            if (sizeScheduleMaster != null)
            {
                status = "Success";
                sizeScheduleMasterManager.Delete(sizeScheduleMaster.SizeScheduleMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<SizeScheduleMaster> sizeScheduleMasterlist = new List<SizeScheduleMaster>();
            List<SizeScheduleMaster> sizeScheduleMasterlist1 = new List<SizeScheduleMaster>();
            SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
            List<SizeRangeDetails> sizeRangeDetailsList = new List<SizeRangeDetails>();
            SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
            sizeRangeDetailsList = sizeRangeDetailsManager.Get();
            sizeScheduleMasterlist = sizeScheduleMasterManager.Get();

             var   sizeScheduleList = from X in sizeRangeDetailsList
                                     join Y in sizeScheduleMasterlist on X.SizeRangeDetailsId equals Convert.ToInt32(Y.SizeRangeName)
                                     where X.SizeRangeName.ToLower().Trim().Contains(filter.ToLower().Trim())
                                     select Y;
            sizeScheduleMasterlist = sizeScheduleList.ToList(); 

            //sizeScheduleMasterlist = sizeScheduleMasterlist.Where(x => x.SizeRangeName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            SizeScheduleMasterModel model = new SizeScheduleMasterModel();
            model.SizeScheduleMasterList = sizeScheduleMasterlist;
            return PartialView("Partial/SizeScheduleMasterGrid", model);
        }
        #endregion


        public ActionResult MaterialPopUpValue()
        {

            MaterialNameManager Manager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items = Manager.Get().OrderBy(x => x.MaterialDescription).Select(
                                                  item => new System.Web.Mvc.SelectListItem()
                                                  {
                                                      Text = item.MaterialDescription,
                                                      Value = item.MaterialMasterID.ToString()
                                                  }).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
            };
            items.Insert(0, ShotName);

            return Json(items, JsonRequestBehavior.AllowGet);
        }


    }
}
