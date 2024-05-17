using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.StoreMasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class StoreMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult StoreMaster()
        {
            StoreMasterModel vm = new StoreMasterModel();
            return View(vm);
        }
        public ActionResult StoreMasterGrid()
        {
            StoreMasterModel vm = new StoreMasterModel();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            vm.StoreMasterList = storeMasterManager.Get();

            return PartialView("Partial/StoreMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult StoreMasterDetails(int StoreMasterId)
        {
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            StoreMasterModel model = new StoreMasterModel();
            storeMaster = storeMasterManager.GetStoreMasterId(StoreMasterId);
            if (StoreMasterId == 0)
            {
                model.StoreCode = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateStoreID();
                model.StoreCode = "STORE" + model.StoreCode;
            }
            else
            {
                model.StoreCode = "STORE" + storeMaster.StoreCode;
            }
            if (storeMaster != null)
            {
                model.StoreMasterId = storeMaster.StoreMasterId;
                model.StoreName = storeMaster.StoreName;
                model.Location = storeMaster.Location;
                model.CreatedDate = storeMaster.CreatedDate;
                model.UpdatedDate = storeMaster.UpdatedDate;
                model.CreatedBy = storeMaster.CreatedBy;
                model.UpdatedBy = storeMaster.UpdatedBy;
            }
            return PartialView("Partial/StoreMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult StoreMaster(StoreMasterModel model)
        {
            StoreMaster storeMasters = new StoreMaster();
            StoreMaster storeMaster_ = new StoreMaster();
            if (ModelState.IsValid)
            {
                StoreMaster storeMaster = new StoreMaster();              
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                if (model.StoreMasterId == 0)
                {
                    storeMaster = storeMasterManager.GetStoreName(model.StoreName);
                    model.StoreCode = model.StoreCode.Substring(5);
                    if (storeMaster == null)
                    {
                        storeMasters.StoreCode = model.StoreCode;
                        storeMasters.StoreName = model.StoreName;
                        storeMasters.Location = model.Location;
                        storeMasters.CreatedDate = DateTime.Now;
                        storeMaster_ = storeMasterManager.Post(storeMasters);
                    }
                }
                else if (model != null && model.StoreMasterId != 0)
                {
                    storeMaster = storeMasterManager.GetStoreName(model.StoreName);
                    model.StoreCode = model.StoreCode.Substring(5);
                    storeMaster.StoreCode = model.StoreCode;
                    storeMaster.StoreName = model.StoreName;
                    storeMaster.Location = model.Location;
                    storeMaster.CreatedDate = DateTime.Now;
                    storeMaster_ = storeMasterManager.Put(storeMaster);
                    return Json(storeMaster_, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(storeMaster_, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(storeMaster_, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(StoreMasterModel model)
        {
            StoreMaster storeMasters = new StoreMaster();
            if (ModelState.IsValid)
            {
                StoreMaster storeMaster = new StoreMaster();
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                storeMaster = storeMasterManager.GetStoreMasterId(model.StoreMasterId);
                if (storeMaster != null)
                {
                    storeMasters.StoreMasterId = model.StoreMasterId;
                    storeMasters.StoreCode = model.StoreCode;
                    storeMasters.StoreName = model.StoreName;
                    storeMasters.Location = model.Location;
                    storeMasters.CreatedDate = model.CreatedDate;
                    storeMasters.UpdatedDate = DateTime.Now;
                    storeMasters.CreatedBy = storeMaster.CreatedBy;
                    storeMasters.UpdatedBy = storeMaster.UpdatedBy;
                    storeMasterManager.Put(storeMasters);
                }
                else
                {
                    return Json(storeMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(storeMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int StoreMasterId)
        {
            StoreMaster storeMaster = new StoreMaster();
            string status = "";
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            storeMaster = storeMasterManager.GetStoreMasterId(StoreMasterId);
            if (storeMaster != null)
            {
                status = "Success";
                storeMasterManager.Delete(storeMaster.StoreMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<StoreMaster> storeMasterList = new List<StoreMaster>();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            storeMasterList = storeMasterManager.Get();
            storeMasterList = storeMasterList.Where(x => x.StoreName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            StoreMasterModel model = new StoreMasterModel();
            model.StoreMasterList = storeMasterList;
            return PartialView("Partial/StoreMasterGrid", model);
        }
        #endregion

    }
}
