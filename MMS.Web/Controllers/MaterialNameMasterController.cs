using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MMS.Web.ExtensionMethod.HtmlHelper;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class MaterialNameMasterController : Controller
    {
        //
        // GET: /MaterialNameMaster/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MaterialNameMaster()
        {

            return View();
        }
        public ActionResult MaterialNameMasterGrid()
        {
            MaterialNameModel mm = new MaterialNameModel();
            MaterialNameManager materialManager = new MaterialNameManager();
            mm.MaterilNameList = materialManager.Get();
            return PartialView("Partial/MaterialNameMasterGrid", mm);
        }

        public ActionResult GetLeatherType(int MaterialGroupMasterId)
        {
            bool isleather = false;
            MaterialCategoryManager categoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster categoryMaster = new MaterialCategoryMaster();
            MaterialGroupManager groupManager = new MaterialGroupManager();
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            materialGroupMaster = groupManager.GetmaterialgroupmasterId(MaterialGroupMasterId);
            categoryMaster = categoryManager.GetMaterialCategoryMaster(materialGroupMaster.MaterialCategoryMasterId);
            if (categoryMaster.CategoryName == GetEnumDescription((Categoryname)Categoryname.Leathers))
            {
                isleather = true;
            }

            return Json(isleather, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLeatherTypeName(string MaterialGroupMasterId)
        {
            bool isleather = false;
            MaterialCategoryManager categoryManager = new MaterialCategoryManager();
            MaterialCategoryMaster categoryMaster = new MaterialCategoryMaster();
            MaterialGroupManager groupManager = new MaterialGroupManager();
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            materialGroupMaster = groupManager.GetMaterialGroupName(MaterialGroupMasterId);
            categoryMaster = categoryManager.GetMaterialCategoryMaster(materialGroupMaster.MaterialCategoryMasterId);
            if (categoryMaster.CategoryName == GetEnumDescription((Categoryname)Categoryname.Leathers))
            {
                isleather = true;
            }

            return Json(isleather, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMaxMaterialCode()
        {
            MaterialNameModel mm = new MaterialNameModel();
            MaterialNameManager materialManager = new MaterialNameManager();

            int MaxMaterialCode = MMS.Web.ExtensionMethod.HtmlHelper.getMaterialNameCodeId();

            mm.MaterialCode = "MN" + (Convert.ToInt32(MaxMaterialCode) + 1).ToString();
            return Json(mm.MaterialCode + "~" + (Convert.ToInt32(MaxMaterialCode) + 1).ToString(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMaterialEdit(MaterialNameModel Model)
        {
            tbl_materialnamemaster mm = new tbl_materialnamemaster();
            MaterialNameManager materialManager = new MaterialNameManager();
            mm = materialManager.GetMaterialNameMasterId(Model.MaterialMasterID);
            if (mm != null)
            {
                Model.MaterialCode ="MN"+mm.MaterialCode;
                Model.MaterialMasterID = mm.MaterialMasterID;
                Model.MaterialDescription = mm.MaterialDescription;
                Model.MaterialGroupMasterId = mm.MaterialGroupMasterId;
                Model.LeatherMaterialName = mm.LeatherMaterialName;
                Model.LeatherMaterilFirstValue = mm.LeatherMaterilFirstValue;
                Model.LeatherMaterilLastValue = mm.LeatherMaterilLastValue;
            }
            return PartialView("Partial/MaterialNameMasterDetails", Model);
        }


        [HttpPost]
        public ActionResult MaterialNameMasterDetails(MaterialNameModel Model)
        {
            bool result = false;
            MaterialNameModel mm = new MaterialNameModel();
            MaterialNameManager materialManager = new MaterialNameManager();
            tbl_materialnamemaster MasterObj = new tbl_materialnamemaster();
            tbl_materialnamemaster MasterObj_ = new tbl_materialnamemaster();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialgroupmaster materialgroupmaster = new materialgroupmaster();
            materialgroupmaster = materialGroupManager.GetMaterialGroupName(Model.MaterialGroup);
            if (Model.MaterialDescription != null && Model.MaterialMasterID == 0)
            {
                MasterObj_ = materialManager.GetMaterialName(Model.MaterialDescription);
            }
            else if (Model.MaterialMasterID != 0)
            {
                MasterObj_ = materialManager.GetMaterialNameMasterId(Model.MaterialMasterID);
            }
            else if (Model.LeatherMaterilFirstValue != null && Model.LeatherMaterilLastValue != null && Model.LeatherMaterialName != null)
            {
                MasterObj_ = materialManager.GetLeatherMaterialName(Model.LeatherMaterilFirstValue, Model.LeatherMaterilLastValue, Model.LeatherMaterialName, materialgroupmaster.MaterialGroupMasterId);
            }
            else
            {
                MasterObj.MaterialMasterID = 0;
                return Json(MasterObj, JsonRequestBehavior.AllowGet);
            }
      

            if (MasterObj_ == null)
            {
                Model.MaterialCode = Model.MaterialCode.Remove(0, 2);
                MasterObj.MaterialCode = Model.MaterialCode;
                MasterObj.MaterialMasterID = Model.MaterialMasterID;
                MasterObj.MaterialDescription = Model.MaterialDescription;
                MasterObj.LeatherMaterialName = Model.LeatherMaterialName;
                MasterObj.LeatherMaterilFirstValue = Model.LeatherMaterilFirstValue;
                MasterObj.LeatherMaterilLastValue = Model.LeatherMaterilLastValue;

                MasterObj.MaterialGroupMasterId = materialgroupmaster.MaterialGroupMasterId;

                materialManager.Post(MasterObj);
            }
            else if(Model.MaterialMasterID==0&& MasterObj_!=null)
            {
                MasterObj_.MaterialMasterID = 0;
                return Json(MasterObj_, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Model.MaterialCode = Model.MaterialCode.Remove(0, 2);
                MasterObj.MaterialMasterID = Model.MaterialMasterID;
                MasterObj.MaterialDescription = Model.MaterialDescription;
                MasterObj.LeatherMaterialName = Model.LeatherMaterialName;
                MasterObj.LeatherMaterilFirstValue = Model.LeatherMaterilFirstValue;
                MasterObj.LeatherMaterilLastValue = Model.LeatherMaterilLastValue;
                materialgroupmaster = materialGroupManager.GetMaterialGroupName(Model.MaterialGroup);
                MasterObj.MaterialGroupMasterId = materialgroupmaster.MaterialGroupMasterId;

                materialManager.Put(MasterObj);
            }

            return Json(MasterObj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(MaterialNameModel Model)
        {
            bool result = false;
            MaterialNameModel mm = new MaterialNameModel();
            MaterialNameManager materialManager = new MaterialNameManager();
            tbl_materialnamemaster MasterObj = new tbl_materialnamemaster();
            result = materialManager.Delete(Model.MaterialMasterID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string filter)
        {
            MaterialNameModel mm = new MaterialNameModel();
            MaterialNameManager materialManager = new MaterialNameManager();
            List<tbl_materialnamemaster> listMaterialName = new List<tbl_materialnamemaster>();
            listMaterialName = materialManager.Get().ToList();
            listMaterialName = listMaterialName.Where(x => x.MaterialDescription!=null&& x.MaterialDescription.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            mm.MaterilNameList = listMaterialName;
            return PartialView("Partial/MaterialNameMasterGrid", mm);
        }
    }
}
