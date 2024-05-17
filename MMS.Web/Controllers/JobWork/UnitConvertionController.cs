using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Repository.Managers.JobWork;
using MMS.Web.Models.JobworkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Core.Entities.Stock;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;

namespace MMS.Web.Controllers.JobWork
{

    [CustomFilter]
    public class UnitConvertionController : Controller
    {
       
        #region Accounts View

        [HttpGet]
        public ActionResult UnitConvertionMaster()
        {
            UnitConvertionModel vm = new UnitConvertionModel();
            return View("~/Views/Jobwork/Job_Master/Job_Unit_Convertion/Job_Unit_Convertion_Master.cshtml", vm);
        }
        public ActionResult UnitConvertionGrid()
        {
            UnitConvertionModel vm = new UnitConvertionModel();
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            vm.UnitConvertionMasterList = Job_UnitConvertionManager.Get();



            return PartialView("~/Views/Jobwork/Job_Master/Job_Unit_Convertion/Partial/Job_Unit_Grid.cshtml", vm);

        }
        [HttpPost]
        public ActionResult UnitConvertioneDetails(int UC_No_Id)
        {
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            UnitConvertionMaster UnitConvertionMaster = new UnitConvertionMaster();
            UnitConvertionModel model = new UnitConvertionModel();
            UnitConvertionMaster = Job_UnitConvertionManager.Get_UC_No_Id(UC_No_Id);

            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getjob_UnitconvertionCodeId();
            ID++;

            if (UnitConvertionMaster != null)
            {
                model.UC_No_Id = UnitConvertionMaster.UC_No_Id;
                model.UC_No_Code = UnitConvertionMaster.UC_No_Code;
                model.Store_id_from = UnitConvertionMaster.Store_id_from;
                model.Group_From = UnitConvertionMaster.Group_From;
                model.Category_From = UnitConvertionMaster.Category_From;
                model.Material_id_From = UnitConvertionMaster.Material_id_From;
                model.Store_id_to = UnitConvertionMaster.Store_id_to;
                model.Group_To = UnitConvertionMaster.Group_To;
                model.Category_To = UnitConvertionMaster.Category_To;
                model.Material_id_To = UnitConvertionMaster.Material_id_To;
                model.Noms = UnitConvertionMaster.Noms;

                model.Uom1 = UnitConvertionMaster.Uom1;
                model.Uom2 = UnitConvertionMaster.Uom2;
                model.No_sheet = UnitConvertionMaster.No_sheet;
                model.No_sheet_Uom = UnitConvertionMaster.No_sheet_Uom;
                model.Sheet_Value = UnitConvertionMaster.Sheet_Value;
                model.Sheet_Value_Uom = UnitConvertionMaster.Sheet_Value_Uom;
                model.Value = UnitConvertionMaster.Value;
                model.Value_Uom = UnitConvertionMaster.Value_Uom;
                
                model.CreatedBy = UnitConvertionMaster.CreatedBy;
                model.UpdatedBy = UnitConvertionMaster.UpdatedBy;
            }
            if (model.UC_No_Id != 0)
            {
                model.UC_No_Code = UnitConvertionMaster.UC_No_Code;
            }
            else
            {
                model.UC_No_Code = "Un" + ID++;
            }

            return PartialView("~/Views/Jobwork/Job_Master/Job_Unit_Convertion/Partial/Job_Unit_convertion_Detail.cshtml", model);

            //return PartialView("~/Partial/Job_Unit_convertion_Detail.cshtml", model);

        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult UnitConvertionInsert(UnitConvertionModel model)
        {
            UnitConvertionMaster UnitConvertionMaster = new UnitConvertionMaster();
            UnitConvertionMaster UnitConvertionMaster_ = new UnitConvertionMaster();
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            UnitConvertionMaster = Job_UnitConvertionManager.Get_UC_No_Id(model.UC_No_Id);
            if (UnitConvertionMaster.UC_No_Id == 0 || UnitConvertionMaster.UC_No_Id == null)
            {

               UnitConvertionMaster_.UC_No_Code = model.UC_No_Code;
                UnitConvertionMaster_.Store_id_from = model.Store_id_from;
                UnitConvertionMaster_.Group_From = model.Group_From;
                UnitConvertionMaster_.Category_From = model.Category_From;
                UnitConvertionMaster_.Material_id_From = model.Material_id_From;
                UnitConvertionMaster_.Store_id_to = model.Store_id_to;
                UnitConvertionMaster_.Group_To = model.Group_To;
                UnitConvertionMaster_.Category_To = model.Category_To;
                UnitConvertionMaster_.Material_id_To = model.Material_id_To;
                UnitConvertionMaster_.Noms = model.Noms;

                UnitConvertionMaster_.Uom1 = model.Uom1;
                UnitConvertionMaster_.Uom2 = model.Uom2;
                UnitConvertionMaster_.No_sheet = model.No_sheet;
                UnitConvertionMaster_.No_sheet_Uom = model.No_sheet_Uom;
                UnitConvertionMaster_.Sheet_Value = model.Sheet_Value;
                UnitConvertionMaster_.Sheet_Value_Uom = model.Sheet_Value_Uom;
                UnitConvertionMaster_.Value = model.Value;
                UnitConvertionMaster_.Value_Uom = model.Value_Uom;

                UnitConvertionMaster_.CreatedDate = DateTime.Now;
                Job_UnitConvertionManager.Post(UnitConvertionMaster_);
            }
            else if (UnitConvertionMaster_ != null && UnitConvertionMaster_.UC_No_Code == model.UC_No_Code)
            {
                UnitConvertionMaster_.UC_No_Id = 0;
                return Json(UnitConvertionMaster_, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(UnitConvertionMaster_, JsonRequestBehavior.AllowGet);
            }


            return Json(UnitConvertionMaster_, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(UnitConvertionModel model)
        {
            UnitConvertionMaster UnitConvertionMaster = new UnitConvertionMaster();
            UnitConvertionMaster UnitConvertionMaster_ = new UnitConvertionMaster();
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            UnitConvertionMaster = Job_UnitConvertionManager.Get_UC_No_Id(model.UC_No_Id);
            if (UnitConvertionMaster != null)
            {
                UnitConvertionMaster_.UC_No_Code = model.UC_No_Code;
                UnitConvertionMaster_.UC_No_Id = model.UC_No_Id;
                UnitConvertionMaster_.Store_id_from = model.Store_id_from;
                UnitConvertionMaster_.Group_From = model.Group_From;
                UnitConvertionMaster_.Category_From = model.Category_From;
                UnitConvertionMaster_.Material_id_From = model.Material_id_From;
                UnitConvertionMaster_.Store_id_to = model.Store_id_to;
                UnitConvertionMaster_.Group_To = model.Group_To;
                UnitConvertionMaster_.Category_To = model.Category_To;
                UnitConvertionMaster_.Material_id_To = model.Material_id_To;
                UnitConvertionMaster_.Noms = model.Noms;

                UnitConvertionMaster_.Uom1 = model.Uom1;
                UnitConvertionMaster_.Uom2 = model.Uom2;
                UnitConvertionMaster_.No_sheet = model.No_sheet;
                UnitConvertionMaster_.No_sheet_Uom = model.No_sheet_Uom;
                UnitConvertionMaster_.Sheet_Value = model.Sheet_Value;
                UnitConvertionMaster_.Sheet_Value_Uom = model.Sheet_Value_Uom;
                UnitConvertionMaster_.Value = model.Value;
                UnitConvertionMaster_.Value_Uom = model.Value_Uom;
                UnitConvertionMaster_.UpdatedDate = DateTime.Now;
                UnitConvertionMaster_.CreatedBy = model.CreatedBy;
                UnitConvertionMaster_.UpdatedBy = model.UpdatedBy;
                Job_UnitConvertionManager.Put(UnitConvertionMaster_);
            }
            return Json(UnitConvertionMaster_, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int UC_No_Id)
        {
            UnitConvertionMaster UnitConvertionMaster = new UnitConvertionMaster();
            string status = "";
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            UnitConvertionMaster = Job_UnitConvertionManager.Get_UC_No_Id(UC_No_Id);
            if (UnitConvertionMaster != null)
            {
                status = "Success";
                Job_UnitConvertionManager.Delete(UnitConvertionMaster.UC_No_Id);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        public ActionResult Getmaterial_SelectDetail(int MaterialNameID)
        {
            IssueSlip_SingleManager issueSlipManager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlip = new IssueSlipManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            ColorManager colorManager = new ColorManager();

            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId into yG
                         from y1 in yG.DefaultIfEmpty()
                         where x.MaterialMasterId == MaterialNameID
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isImport, x.isLocal, x.isImportCustomer }).ToList();

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Check_UcNo(string UC_No_Code)
        {
            UnitConvertionMaster UnitConvertionMaster_ = new UnitConvertionMaster();
            Job_UnitConvertionManager Job_UnitConvertionManager = new Job_UnitConvertionManager();
            var items = Job_UnitConvertionManager.Get().Where(x => x.UC_No_Code == UC_No_Code.Trim()).ToList().Count();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
