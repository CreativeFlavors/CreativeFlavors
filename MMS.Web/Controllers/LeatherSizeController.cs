using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.LeatherSize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class LeatherSizeController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult LeatherSize()
        {
            LeatherSizeModel vm = new LeatherSizeModel();
            return View(vm);
        }
        public ActionResult LeatherSizeGrid()
        {
            LeatherSizeModel vm = new LeatherSizeModel();
            LeatherSizeManager leatherSizeManager = new LeatherSizeManager();
            vm.LeatherMasterList = leatherSizeManager.Get();

            return PartialView("Partial/LeatherSizeGrid", vm);
        }
        [HttpPost]
        public ActionResult LeatherSizeDetails(int LeatherSizeMasterId)
        {
            LeatherSizeManager leatherSizeManager = new LeatherSizeManager();
            LeatherSizeMaster leatherSizeMaster = new LeatherSizeMaster();
            LeatherSizeModel model = new LeatherSizeModel();
            leatherSizeMaster = leatherSizeManager.GetleatherMasterId(LeatherSizeMasterId);
            if (leatherSizeMaster.LeatherSizeMasterId != 0)
            {
                model.Width = leatherSizeMaster.Width;
                model.Length = leatherSizeMaster.Length;
                model.Average = leatherSizeMaster.Average;
                model.LeatherSizeMasterId = leatherSizeMaster.LeatherSizeMasterId;
                model.CreatedDate = leatherSizeMaster.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
                model.CreatedBy = leatherSizeMaster.CreatedBy;
                model.UpdatedBy = leatherSizeMaster.UpdatedBy;
            }
            return PartialView("Partial/LeatherSizeDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult LeatherSize(LeatherSizeModel model)
        {
            BuyerMaster buyerMasters = new BuyerMaster();
            if (ModelState.IsValid)
            {
                LeatherSizeMaster leatherSizeMaster = new LeatherSizeMaster();
                LeatherSizeManager leatherSizeManager = new LeatherSizeManager();
                leatherSizeMaster.Width = model.Width;
                leatherSizeMaster.Length = model.Length;
                leatherSizeMaster.Average = model.Average;
                leatherSizeMaster.CreatedDate = DateTime.Now;
                leatherSizeMaster.UpdatedDate = DateTime.Now;
                leatherSizeManager.Post(leatherSizeMaster);
            }
            return Json(buyerMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(LeatherSizeModel model)
        {
            LeatherSizeMaster leatherSizeMasters = new LeatherSizeMaster();
            if (ModelState.IsValid)
            {
                LeatherSizeMaster leatherSizeMaster = new LeatherSizeMaster();
                LeatherSizeManager leatherSizeManager = new LeatherSizeManager();
                leatherSizeMaster = leatherSizeManager.GetleatherMasterId(model.LeatherSizeMasterId);
                if (leatherSizeMaster != null)
                {
                    leatherSizeMasters.Width = model.Width;
                    leatherSizeMasters.LeatherSizeMasterId = model.LeatherSizeMasterId;
                    leatherSizeMasters.Length = model.Length;
                    leatherSizeMasters.Average = model.Average;
                    leatherSizeMasters.CreatedDate = leatherSizeMaster.CreatedDate;
                    leatherSizeMasters.UpdatedDate = DateTime.Now;
                    leatherSizeMasters.CreatedBy = leatherSizeMaster.CreatedBy;
                    leatherSizeMasters.UpdatedBy = leatherSizeMaster.UpdatedBy;
                    leatherSizeManager.Put(leatherSizeMasters);
                }
                else
                {
                    return Json(leatherSizeMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(leatherSizeMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int LeatherSizeMasterId)
        {
            LeatherSizeMaster leatherSizeMaster = new LeatherSizeMaster();
            string status = "";
            LeatherSizeManager leatherSizeManager = new LeatherSizeManager();
            leatherSizeMaster = leatherSizeManager.GetleatherMasterId(LeatherSizeMasterId);
            if (leatherSizeMaster != null)
            {
                status = "Success";
                leatherSizeManager.Delete(leatherSizeMaster.LeatherSizeMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<LeatherSizeMaster> leatherSizeMasterlist = new List<LeatherSizeMaster>();
            LeatherSizeManager leatherSizeManager = new LeatherSizeManager();
            leatherSizeMasterlist = leatherSizeManager.Get();
            leatherSizeMasterlist = leatherSizeMasterlist.Where(s => s.Length.ToLower().Trim().Contains(filter.ToLower().Trim()) || s.Width.ToLower
           ().Trim().Contains(filter.ToLower().Trim()) || s.Average.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();

            LeatherSizeModel model = new LeatherSizeModel();
            model.LeatherMasterList = leatherSizeMasterlist;
            return PartialView("Partial/LeatherSizeGrid", model);
        }
        #endregion

    }
}
