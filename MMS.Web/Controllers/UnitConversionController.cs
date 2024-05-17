using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.UnitConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class UnitConversionController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult UnitConversion()
        {
            UnitConversionModel vm = new UnitConversionModel();
            return View(vm);
        }
        public ActionResult UnitConversionGrid()
        {
            UnitConversionModel vm = new UnitConversionModel();
            UnitConversionManager unitConversionManager = new UnitConversionManager();
            vm.UnitConversionList = unitConversionManager.Get();

            return PartialView("Partial/UnitConversionGrid", vm);
        }
        [HttpPost]
        public ActionResult UnitConversionDetails(int UnitConversionId)
        {
            UnitConversionManager unitConversionManager = new UnitConversionManager();
            UnitConversation uomMaster = new UnitConversation();
            UnitConversionModel model = new UnitConversionModel();
            uomMaster = unitConversionManager.GetUnitConversionId(UnitConversionId);
            if (uomMaster != null)
            {
                model.Category = uomMaster.Category;
                model.UcGroup = uomMaster.UcGroup;
                model.Material = uomMaster.Material;
                model.UnitConversionId = uomMaster.UnitConversionId;
                model.ShortUnitNameValue = uomMaster.ShortUnitNameValue;
                model.LongUnitNameValue = uomMaster.LongUnitNameValue;
                model.ShortUnitID = uomMaster.ShortUnitId;
                model.LongUnitValue = uomMaster.LongUnitID;
                model.Material = uomMaster.Material;
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.CreatedBy = uomMaster.CreatedBy;
                model.UpdatedBy = uomMaster.UpdatedBy;
            }
            return PartialView("Partial/UnitConversionDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult UnitConversion(UnitConversionModel model)
        {
            UnitConversation unitConversations = new UnitConversation();
            if (ModelState.IsValid)
            {
                UnitConversation unitConversation = new UnitConversation();
                UnitConversionManager unitConversionManager = new UnitConversionManager();
                unitConversations.Category = model.Category;
                unitConversations.UcGroup = model.UcGroup;
                unitConversations.Material = model.Material;
                unitConversations.ShortUnitNameValue = model.ShortUnitNameValue;
                unitConversations.LongUnitNameValue = model.LongUnitNameValue;
                unitConversations.CreatedDate = DateTime.Now;
                unitConversation.ShortUnitId = model.ShortUnitID;
                unitConversation.LongUnitID = model.LongUnitValue;
                unitConversionManager.Post(unitConversations);
                return Json(unitConversations, JsonRequestBehavior.AllowGet);
            }
            return Json(unitConversations, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(UnitConversionModel model)
        {
            UnitConversation unitConversations = new UnitConversation();
            if (ModelState.IsValid)
            {
                UnitConversation unitConversation = new UnitConversation();
                UnitConversionManager unitConversionManager = new UnitConversionManager();
                unitConversation = unitConversionManager.GetUnitConversionId(model.UnitConversionId);
                if (unitConversation != null)
                {
                    unitConversations.Category = model.Category;
                    unitConversations.UcGroup = model.UcGroup;
                    unitConversations.Material = model.Material;
                    unitConversations.UnitConversionId = model.UnitConversionId;
                    unitConversations.ShortUnitNameValue = model.ShortUnitNameValue;
                    unitConversations.LongUnitNameValue = model.LongUnitNameValue;
                    unitConversations.CreatedDate = unitConversation.CreatedDate;
                    unitConversations.LongUnitID = model.LongUnitValue;
                    unitConversations.ShortUnitId = model.ShortUnitID;
                    unitConversations.UpdatedDate = DateTime.Now;
                    unitConversations.CreatedBy = unitConversation.CreatedBy;
                    unitConversionManager.Put(unitConversations);
                }
                else
                {
                    return Json(unitConversations, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(unitConversations, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int UnitConversionId)
        {
            UnitConversation unitConversation = new UnitConversation();
            string status = "";
            UnitConversionManager unitConversionManager = new UnitConversionManager();
            unitConversation = unitConversionManager.GetUnitConversionId(UnitConversionId);
            if (unitConversation != null)
            {
                status = "Success";
                unitConversionManager.Delete(unitConversation.UnitConversionId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<UnitConversation> unitConversationlist = new List<UnitConversation>();
            UnitConversionManager unitConversionManager = new UnitConversionManager();
            List<MaterialGroupMaster_> materialGroupMasterList = new List<MaterialGroupMaster_>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            materialGroupMasterList = materialGroupManager.Get();
            List<MaterialCategoryMaster> materialCatogoryMasterList = new List<MaterialCategoryMaster>();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            materialCatogoryMasterList = materialCategoryManager.Get();
            unitConversationlist = unitConversionManager.Get();

            var unitConversationLi = from X in unitConversationlist
                                     join Y in materialGroupMasterList on X.UcGroup equals Y.MaterialGroupMasterId
                                     join Z in materialCatogoryMasterList on X.Category equals Z.MaterialCategoryMasterId
                                     where (Y.GroupName.ToLower().Trim().Contains(filter.ToLower().Trim()) || Z.CategoryName.ToLower().Trim().Contains(filter.ToLower().Trim()))
                                     select X;

            unitConversationlist = unitConversationLi.ToList();


            UnitConversionModel model = new UnitConversionModel();
            model.UnitConversionList = unitConversationlist;
            return PartialView("Partial/UnitConversionGrid", model);
        }
        #endregion

    }
}
