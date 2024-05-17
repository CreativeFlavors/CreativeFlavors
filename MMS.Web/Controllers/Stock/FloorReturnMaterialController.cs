using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class FloorReturnMaterialController : Controller
    {

        #region FloorReturnMaterila View
        [HttpGet]
        public ActionResult FloorReturnMaterial()
        {
            FloorReturnMaterialModel model = new FloorReturnMaterialModel();
            return View("~/Views/stock/FloorReturnMaterial/FloorReturnMaterial.cshtml", model);
        }
        public ActionResult FloorReturnMaterialGrid()
        {
            FloorReturnMaterialGrid model = new FloorReturnMaterialGrid();
            FloorRetMaterialManager floorRetMaterialManager = new FloorRetMaterialManager();
            model.FloorMaterialList = floorRetMaterialManager.Get();
            return PartialView("~/Views/stock/FloorReturnMaterial/Partial/FloorReturnMaterialGrid.cshtml", model);
        }
        [HttpPost]
        public ActionResult FloorReturnMaterialDetails(int FloorReturnMaterialId)
        {
            FloorReturnMaterialGrid model = new FloorReturnMaterialGrid();
            FloorMaterial mastermodel = new FloorMaterial();
            FloorRetMaterialManager floorRetMaterialManager = new FloorRetMaterialManager();

            mastermodel = floorRetMaterialManager.GetFloorReturnMaterialId(FloorReturnMaterialId);
            if (FloorReturnMaterialId == 0)
            {
                model.FrmNo = GetFrmNo();
                model.FrmNo = "FrmN00" + model.FrmNo;
            }
            else
            {
                model.FrmNo = "FrmN00" + model.FrmNo;
            }
            if (mastermodel.FloorReturnMaterialId != 0)
            {

                model.FloorReturnMaterialId = mastermodel.FloorReturnMaterialId;
                model.FrmNo = mastermodel.FrmNo;
                model.FloorDate = mastermodel.FloorDate.ToString();
                model.IssueNo = mastermodel.IssueNo;
                model.Remarks = mastermodel.Remarks; 
                    model.Is_IssueNo = mastermodel.Is_IssueNo;
                model.floorReturnMaterialDetails = GetFloorMaterialsByIssueNo(model.FloorReturnMaterialId);


                return PartialView("~/Views/stock/FloorReturnMaterial/Partial/FloorReturnMaterialDetails.cshtml", model);
            }

            return PartialView("~/Views/stock/FloorReturnMaterial/Partial/FloorReturnMaterialDetails.cshtml", model);
        }

        #endregion

        #region Crud Operations

        [HttpPost]
        public ActionResult FloorReturnMaterial(FloorReturnMaterialGrid model)
        {
            FloorReturnMaterialGrid viewmodel = new FloorReturnMaterialGrid();
            FloorMaterial floorMaterial = new FloorMaterial();
            FloorReturnMaterialDetails floorReturnMaterialDetails = new FloorReturnMaterialDetails();
            FloorReturnSizeRange floorReturnSizeRange = new FloorReturnSizeRange();
            DateTime? FloorreturnDate = null;
            FloorRetMaterialManager floorRetMaterialManager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager floorMaterialManager = new FloorReturnMaterialDetailsManager();
            FloorReturnSizeManager floorSizeRangeManager = new FloorReturnSizeManager();
            string[] dateFormats = {"M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt",
                         "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss","dd/MM/yyyy HH:mm:ss","dd/MM/yyyy","MM/dd/yyyy",
                         "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt",
                         "M/d/yyyy h:mm", "M/d/yyyy h:mm",
                         "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm",
                         "MM/d/yyyy HH:mm:ss.ffffff" };
            if (model.FloorReturnMaterialId != 0 && model.FloorReturnMaterialDetailsId == 0)
            {
                floorReturnMaterialDetails.FloorReturnMaterialId = model.FloorReturnMaterialId;
                floorReturnMaterialDetails.StoreMasterId = model.StoreMasterId;
                floorReturnMaterialDetails.GroupMasterId = model.GroupMasterId;
                floorReturnMaterialDetails.MaterialMasterId = model.MaterialMasterId;
                floorReturnMaterialDetails.IoNo =Convert.ToInt32(model.IoNo);
                floorReturnMaterialDetails.Uom = model.Uom;
                floorReturnMaterialDetails.Style = model.Style;
                floorReturnMaterialDetails.IssuedQuantity = model.IssuedQuantity;
                floorReturnMaterialDetails.ReturnQuantity = model.ReturnQuantity;
                floorReturnMaterialDetails.Rate = model.Rate.ToString();
                floorReturnMaterialDetails.LotNo = model.LotNo;
                floorReturnMaterialDetails.Category = model.Category;
                floorReturnMaterialDetails.PiecesCurrentType = model.PiecesCurrentType;
                floorReturnMaterialDetails.Piecies = model.Piecies;
                floorReturnMaterialDetails.MaterialType = model.MaterialType;
                floorReturnMaterialDetails.CreatedDate = DateTime.Now;
                floorReturnMaterialDetails = floorMaterialManager.Post(floorReturnMaterialDetails);
                viewmodel.floorReturnMaterialDetails = GetFloorMaterialsByIssueNo(floorReturnMaterialDetails.FloorReturnMaterialId);
                floorMaterial = floorRetMaterialManager.GetFloorReturnMaterialId(model.FloorReturnMaterialId);
                floorReturnSizeRange.FloorReturnMaterialDetailsId = floorReturnMaterialDetails.FloorReturnMaterialDetailsId;
                var materialselectlist = MMS.Web.ExtensionMethod.HtmlHelper.MaterialNameBasedOnFloorIssueNo(model.IssueNo);

                if (model.Size != null)
                {
                    string[] sizeAray = model.Size.Split(',');
                    string[] QtyAray = model.Quantity.Split(',');
                    string[] RQtyAray = model.ReturnTotalQuantity.Split(',');

                    int count = 0;

                    foreach (var item in sizeAray)
                    {

                        floorReturnSizeRange.SizeRange = item;
                        floorReturnSizeRange.Quantity = Convert.ToDecimal(QtyAray[count]);
                        floorReturnSizeRange.ReturnedQuantity = Convert.ToDecimal(RQtyAray[count]);

                        floorReturnSizeRange.CreatedDate = DateTime.Now;
                        floorSizeRangeManager.Post(floorReturnSizeRange);
                        count++;
                    }
                    
                    return Json(new { floormaterials = viewmodel.floorReturnMaterialDetails, floordetails = floorMaterial, material_list = materialselectlist }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { floormaterials = viewmodel.floorReturnMaterialDetails, floordetails = floorMaterial, material_list = materialselectlist }, JsonRequestBehavior.AllowGet);
                }
            }
            else if (model.FloorReturnMaterialId == 0)
            {
                if (!string.IsNullOrEmpty(model.FloorDate))
                {
                    FloorreturnDate = DateTime.ParseExact(model.FloorDate, dateFormats,
                                                    new CultureInfo("en-US"),
                                                    DateTimeStyles.None);
                }
                floorMaterial.FrmNo = model.FrmNo;
                floorMaterial.FloorDate = FloorreturnDate;
                floorMaterial.Remarks = model.Remarks;
                floorMaterial.Is_IssueNo = model.Is_IssueNo;
                if (model.IssueNo != null)
                {
                    floorMaterial.IssueNo = model.IssueNo;
                }
                else
                {
                    floorMaterial.IssueNo = "0";
                }
                floorMaterial.CreatedDate = DateTime.Now;
                floorReturnMaterialDetails.FloorReturnMaterialId = floorRetMaterialManager.Post(floorMaterial);
                floorReturnMaterialDetails.StoreMasterId = model.StoreMasterId;
                floorReturnMaterialDetails.GroupMasterId = model.GroupMasterId;
                floorReturnMaterialDetails.MaterialMasterId = model.MaterialMasterId;
                floorReturnMaterialDetails.IoNo =Convert.ToInt32(model.IoNo);
                floorReturnMaterialDetails.Uom = model.Uom;
                floorReturnMaterialDetails.Style = model.Style;
                floorReturnMaterialDetails.IssuedQuantity = model.IssuedQuantity;
                floorReturnMaterialDetails.ReturnQuantity = model.ReturnQuantity;

                if (model.Rate != null)
                {
                    floorReturnMaterialDetails.Rate = model.Rate.ToString();
                }
                else
                {
                    floorReturnMaterialDetails.Rate = "0";
                }
                floorReturnMaterialDetails.LotNo = model.LotNo;
                floorReturnMaterialDetails.MaterialType = model.MaterialType;
                floorReturnMaterialDetails.Category = model.Category;
                floorReturnMaterialDetails.PiecesCurrentType = model.PiecesCurrentType;
                floorReturnMaterialDetails.Piecies = model.Piecies;
                floorReturnMaterialDetails.CreatedDate = DateTime.Now;
                floorReturnMaterialDetails = floorMaterialManager.Post(floorReturnMaterialDetails);
                viewmodel.floorReturnMaterialDetails = GetFloorIssueNo(model.FrmNo);
                floorReturnSizeRange.FloorReturnMaterialDetailsId = floorReturnMaterialDetails.FloorReturnMaterialDetailsId;

                if (model.Size != null)
                {
                    string[] sizeAray = model.Size.Split(',');
                    string[] QtyAray = model.Quantity.Split(',');
                    string[] RQtyAray = model.ReturnTotalQuantity.Split(',');

                    int count = 0;

                    foreach (var item in sizeAray)
                    {

                        floorReturnSizeRange.SizeRange = item;
                        floorReturnSizeRange.Quantity = Convert.ToDecimal(QtyAray[count]);
                        floorReturnSizeRange.ReturnedQuantity = Convert.ToDecimal(RQtyAray[count]);

                        floorReturnSizeRange.CreatedDate = DateTime.Now;
                        floorSizeRangeManager.Post(floorReturnSizeRange);
                        count++;
                    }
                    return Json(new { floormaterials = viewmodel.floorReturnMaterialDetails, floordetails = floorMaterial }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { floormaterials = viewmodel.floorReturnMaterialDetails, floordetails = floorMaterial }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Failed", JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetFloorReturnMaterial(int FloorReturnMaterialDetailsId)
        {
            FloorReturnMaterialDetailsModel floorMaterialDetails = new FloorReturnMaterialDetailsModel();
            List<FloorReturnSizeRange> floorReturnSizeRange = new List<FloorReturnSizeRange>();
            FloorReturnMaterialDetailsManager floorMaterialManager = new FloorReturnMaterialDetailsManager();
            FloorReturnSizeManager floorSizeRangeManager = new FloorReturnSizeManager();
            if (FloorReturnMaterialDetailsId != 0)
            {
                floorMaterialDetails = GetFloorMaterialNamesByMaterialId(FloorReturnMaterialDetailsId);
                floorReturnSizeRange = GetFloorSizeByFloorId(FloorReturnMaterialDetailsId);
                return Json(new { floormaterials = floorMaterialDetails, SizeRangeList = floorReturnSizeRange }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failed", JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult Update(FloorReturnMaterialGrid model)
        {
            bool sizeresult = false;
            bool result = false;
            FloorReturnMaterialGrid viewmodel = new FloorReturnMaterialGrid();
            FloorMaterial floorMaterial = new FloorMaterial();
            FloorReturnMaterialDetails floorReturnMaterialDetails = new FloorReturnMaterialDetails();
            FloorReturnSizeRange floorReturnSizeRange = new FloorReturnSizeRange();

            FloorRetMaterialManager floorRetMaterialManager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager floorMaterialManager = new FloorReturnMaterialDetailsManager();
            FloorReturnSizeManager floorSizeRangeManager = new FloorReturnSizeManager();

            if (model.FloorReturnMaterialId != 0 && model.FloorReturnMaterialDetailsId != 0)
            {
                floorReturnMaterialDetails.FloorReturnMaterialId = model.FloorReturnMaterialId;
                floorReturnMaterialDetails.FloorReturnMaterialDetailsId = model.FloorReturnMaterialDetailsId;
                floorReturnMaterialDetails.StoreMasterId = model.StoreMasterId;
                floorReturnMaterialDetails.GroupMasterId = model.GroupMasterId;
                floorReturnMaterialDetails.MaterialMasterId = model.MaterialMasterId;
                floorReturnMaterialDetails.IoNo =Convert.ToInt32(model.IoNo);
                floorReturnMaterialDetails.Uom = model.Uom;
                floorReturnMaterialDetails.Style = model.Style;
                floorReturnMaterialDetails.IssuedQuantity = model.IssuedQuantity;
                floorReturnMaterialDetails.ReturnQuantity = model.ReturnQuantity;
             
                if (model.Rate != null)
                {
                    floorReturnMaterialDetails.Rate = model.Rate.ToString();
                }
                else
                {
                    floorReturnMaterialDetails.Rate = "0";
                }
                floorReturnMaterialDetails.LotNo = model.LotNo;
                floorReturnMaterialDetails.MaterialType = model.MaterialType;
                floorReturnMaterialDetails.Category = model.Category;
                floorReturnMaterialDetails.PiecesCurrentType = model.PiecesCurrentType;
                floorReturnMaterialDetails.Piecies = model.Piecies;
                floorReturnMaterialDetails.CreatedDate = DateTime.Now;
                floorReturnMaterialDetails = floorMaterialManager.Put(floorReturnMaterialDetails);
                
                floorReturnSizeRange.FloorReturnMaterialDetailsId = floorReturnMaterialDetails.FloorReturnMaterialDetailsId;
                var materialselectlist = MMS.Web.ExtensionMethod.HtmlHelper.MaterialNameBasedOnFloorIssueNo(model.IssueNo);
                floorMaterial = floorRetMaterialManager.GetFloorReturnMaterialId(model.FloorReturnMaterialId);
                viewmodel.floorReturnMaterialDetails = GetFloorIssueNo(floorMaterial.FrmNo);
                if (model.Size != null)
                {
                    string[] sizeAray = model.Size.Split(',');
                    string[] QtyAray = model.Quantity.Split(',');
                    string[] RQtyAray = model.ReturnTotalQuantity.Split(',');
                    string[] SizeIdArray = model.SizeId.Split(',');
                    int count = 0;

                    foreach (var item in sizeAray)
                    {

                        floorReturnSizeRange.SizeRange = item;
                        floorReturnSizeRange.Quantity = Convert.ToDecimal(QtyAray[count]);
                        floorReturnSizeRange.ReturnedQuantity = Convert.ToDecimal(RQtyAray[count]);
                        floorReturnSizeRange.SizeRangeFloorMaterialId = Convert.ToInt32(SizeIdArray[count]);
           
                        sizeresult = floorSizeRangeManager.Put(floorReturnSizeRange);
                        count++;
                    }
             
                }
                if (floorReturnMaterialDetails.FloorReturnMaterialDetailsId != 0)
                {
                    return Json(new { result = "Success", floormaterials = viewmodel.floorReturnMaterialDetails, floordetails = floorMaterial, material_list = materialselectlist }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    return Json(new { floormaterials = viewmodel.floorReturnMaterialDetails, floordetails = floorMaterial, material_list = materialselectlist }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(floorMaterial, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int FloorReturnMaterialId)
        {
            FloorMaterial floorMaterial = new FloorMaterial();
            string status = "";
            FloorRetMaterialManager floorRetMaterialManager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager floorMaterialDetailsManager = new FloorReturnMaterialDetailsManager();
            FloorReturnSizeManager floorSizeManager = new FloorReturnSizeManager();

            floorMaterial = floorRetMaterialManager.GetFloorReturnMaterialId(FloorReturnMaterialId);
            var FloorReturnMaterialDetailsId = floorMaterialDetailsManager.Get().Where(x => x.FloorReturnMaterialId == FloorReturnMaterialId).Select(x => x.FloorReturnMaterialDetailsId);
            foreach (var i in FloorReturnMaterialDetailsId)
            {
                var SizeRangeFloorMaterialId = floorSizeManager.Get().Where(x => x.FloorReturnMaterialDetailsId == i).Select(x => x.SizeRangeFloorMaterialId);
                foreach (var result in SizeRangeFloorMaterialId)
                {
                    floorSizeManager.Delete(result);
                }

                floorMaterialDetailsManager.Delete(i);
            }
            if (floorMaterial != null)
            {
                status = "Success";

                floorRetMaterialManager.Delete(floorMaterial.FloorReturnMaterialId);


            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMaterial(int FloorReturnMaterialId)
        {
            FloorMaterial floorMaterial = new FloorMaterial();
            string status = "";
            FloorRetMaterialManager floorRetMaterialManager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager floorMaterialDetailsManager = new FloorReturnMaterialDetailsManager();
            FloorReturnSizeManager floorSizeManager = new FloorReturnSizeManager();
            FloorReturnMaterialDetails floorReturnMaterialDetails = new FloorReturnMaterialDetails();
            floorReturnMaterialDetails = floorMaterialDetailsManager.GetFloorReturnMaterialId(FloorReturnMaterialId);
            var SizeRangeFloorMaterialId = floorSizeManager.Get().Where(x => x.FloorReturnMaterialDetailsId == FloorReturnMaterialId).Select(x => x.SizeRangeFloorMaterialId);
            foreach (var result in SizeRangeFloorMaterialId)
            {
                floorSizeManager.Delete(result);
            }
            if (floorReturnMaterialDetails != null)
            {
                floorMaterialDetailsManager.Delete(floorReturnMaterialDetails.FloorReturnMaterialDetailsId);
                status = "Success";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FloorMaterialDelete(int FloorReturnMaterialDetailsId, int FloorReturnMaterialId)
        {
            FloorReturnMaterialDetails floorMaterial = new FloorReturnMaterialDetails();
            FloorMaterial fmaterial = new FloorMaterial();
            List<FloorReturnMaterialDetailsModel> model = new List<FloorReturnMaterialDetailsModel>();
            bool status = false;
            FloorRetMaterialManager fmanager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager floorMaterialDetailsManager = new FloorReturnMaterialDetailsManager();
            FloorReturnSizeManager floorSizeManager = new FloorReturnSizeManager();

            floorMaterial = floorMaterialDetailsManager.GetFloorReturnMaterialId(FloorReturnMaterialDetailsId);
            var SizeRangeFloorMaterialId = floorSizeManager.Get().Where(x => x.FloorReturnMaterialDetailsId == floorMaterial.FloorReturnMaterialDetailsId).Select(x => x.SizeRangeFloorMaterialId);
            foreach (var i in SizeRangeFloorMaterialId)
            {
                floorSizeManager.Delete(i);

            }
            if (floorMaterial != null)
            {


                status = floorMaterialDetailsManager.Delete(floorMaterial.FloorReturnMaterialDetailsId);


            }
            fmaterial = fmanager.GetFloorReturnMaterialId(FloorReturnMaterialId);
            model = GetFloorMaterialsByIssueNo(FloorReturnMaterialId);

            return Json(new { status, floormaterials = model, floordetails = fmaterial }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<FloorMaterial> model_list = new List<FloorMaterial>();
            FloorRetMaterialManager floorRetMaterialManager = new FloorRetMaterialManager();
            //

            model_list = floorRetMaterialManager.Get();
            model_list = model_list.Where(x => x.FrmNo.Trim().Contains(filter.Trim()) || x.IssueNo.Trim().Contains(filter.Trim())).ToList();
            FloorReturnMaterialGrid vm = new FloorReturnMaterialGrid();
            vm.FloorMaterialList = model_list;
            return PartialView("~/Views/stock/FloorReturnMaterial/Partial/FloorReturnMaterialGrid.cshtml", vm);

        }
        public string GetFrmNo()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateFloorID();
            ID++;
            return ID.ToString();
        }
        public ActionResult GetMaterial(string IssueSlipNo)
        {

            if (IssueSlipNo != null)
            {
                IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
                IssueSlipManager issueslipmanager = new IssueSlipManager();
                MaterialNameManager namemanager = new MaterialNameManager();
                MaterialManager materialManager = new MaterialManager();
                var MaterialList = (from x in manager.GetMultipleIssueSlip()
                                    join y in issueslipmanager.Get() on x.MultipleIssueSlipID equals y.IssueSlipFK
                                    join w in materialManager.Get() on y.MaterialMasterId equals w.MaterialMasterId
                                    join z in namemanager.Get() on w.MaterialName equals z.MaterialMasterID
                                    where x.IssueSlipNo == IssueSlipNo
                                    select new
                                    {
                                        MaterialName = y.MaterialName + "~" + y.IssueSlipId,
                                        MaterialMasterId = w.MaterialMasterId
                                    });


                

                Session["issueno"] = IssueSlipNo;

                return Json(MaterialList, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMaterialDetails(string MaterialName, string IssueSlipNo,string Materialname_id)
        {

            if (MaterialName != null)
            {
                char[] splitchar = { '~' };
                string[] material = MaterialName.Split(splitchar);
                MaterialName = material[0];
                
                IssueSlipManager issueslipmanager = new IssueSlipManager();
                List<SizeItemsIssueSlip> listSizeItemMaterial = new List<SizeItemsIssueSlip>();
                List<SizeItemMaterial> listSizeItemMaterials = new List<SizeItemMaterial>();
                FloorRetMaterialManager floorReturnManager = new FloorRetMaterialManager();
                MaterialGroupManager groupmanager = new MaterialGroupManager();
                MaterialManager materialManager = new MaterialManager();
                UOMManager uomManager = new UOMManager();
                StoreMasterManager smanager = new StoreMasterManager();
                List<FloorReturnMaterial> floorReturnList = new List<FloorReturnMaterial>();
                List<SizeRangeIssueModelcs> floorReturnList_new = new List<SizeRangeIssueModelcs>();
                List<FloorReturnMaterial_Withoutissue_No> FloorReturnMaterial_Withoutissue_No_list = new List<FloorReturnMaterial_Withoutissue_No>();
                if (IssueSlipNo != null && IssueSlipNo != "Please Select")
                {
                    int IssueSlipId = Convert.ToInt32(material[1]);
                    floorReturnList = floorReturnManager.GetFloorReturnMaterial(MaterialName, IssueSlipId);
                
                    decimal? Sum = floorReturnList.Sum(x => Convert.ToDecimal(x.Quantity));
                    decimal? leatherpiece = floorReturnList.Sum(x => x.Piecies);
                    listSizeItemMaterial = issueslipmanager.GetSizeItemsIssueSlip(Convert.ToInt32(IssueSlipId));
                    listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                    var distinctList = floorReturnList.GroupBy(x => x.IssueSlipNo).Select(g => g.First()).ToList();
                    var dis= floorReturnList.GroupBy(x => x.Groupid).Select(g => g.First()).ToList();

                    return Json(new { data = distinctList, quantity = Sum, pieces = leatherpiece, SizeRangelist = listSizeItemMaterial }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int Materialname_id_int = Int32.Parse(Materialname_id);

                    FloorReturnMaterial_Withoutissue_No_list = floorReturnManager.GetFloorReturnMaterial_withoutissueno(Materialname_id);
                    var dis = FloorReturnMaterial_Withoutissue_No_list.GroupBy(x => x.Groupid).Select(g => g.First()).ToList();
                    string strore = dis[0].StoreName.ToString();
                    if (strore != "Sole Store")
                    {
                        return Json(new { data = dis, quantity = "", pieces = 0.0, SizeRangelist = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        floorReturnList_new = floorReturnManager.MaterialOpeningSizeRange_withoutissueno(Convert.ToInt32(Materialname_id));
                    }

                    return Json(new { data = dis, quantity = "", pieces = 0.0, SizeRangelist = floorReturnList_new }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public List<FloorReturnMaterialDetailsModel> GetFloorMaterialsByIssueNo(int FloorReturnMaterialId)
        {
            List<FloorReturnMaterialDetailsModel> model = new List<FloorReturnMaterialDetailsModel>();
            MaterialNameManager mnmanager = new MaterialNameManager();
            MaterialManager mmanager = new MaterialManager();
            MaterialGroupManager gmanager = new MaterialGroupManager();
            StoreMasterManager smanager = new StoreMasterManager();
            UOMManager umanager = new UOMManager();
            FloorRetMaterialManager floorReturnManager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager fmanager = new FloorReturnMaterialDetailsManager();
            model = (from b in fmanager.Get()
                     join frm in floorReturnManager.Get() on b.FloorReturnMaterialId equals frm.FloorReturnMaterialId
                     join y in mmanager.Get() on b.MaterialMasterId equals y.MaterialMasterId
                     join x in mnmanager.Get() on y.MaterialName equals x.MaterialMasterID          
                     join z in gmanager.Get() on y.MaterialGroupMasterId equals z.MaterialGroupMasterId
                     join a in smanager.Get() on y.StoreMasterId equals a.StoreMasterId
                     where b.FloorReturnMaterialId == FloorReturnMaterialId
                     select new FloorReturnMaterialDetailsModel
                     {
                         FloorReturnMaterialDetailsId = b.FloorReturnMaterialDetailsId,
                         MaterialName = x.MaterialDescription,
                         MaterialMasterId= b.MaterialMasterId,
                         GroupName = z.GroupName,
                         StoreName = a.StoreName,
                         Style = b.Style,
                         Category = b.Category,
                         ReturnQuantity = b.ReturnQuantity,
                         IssuedQuantity = b.IssuedQuantity,
                         Rate = b.Rate,
                         Is_IssueNo = frm.Is_IssueNo,
                         MaterialType=b.MaterialType,


                     }).ToList();

            return model;
        }
        public List<FloorReturnMaterialDetailsModel> GetFloorIssueNo(string FrmNO)
        {
            List<FloorReturnMaterialDetailsModel> model = new List<FloorReturnMaterialDetailsModel>();
            List<FloorReturnMaterialDetailsModel> listmodel = new List<FloorReturnMaterialDetailsModel>();
            MaterialNameManager mnmanager = new MaterialNameManager();
            MaterialManager mmanager = new MaterialManager();
            MaterialGroupManager gmanager = new MaterialGroupManager();
            StoreMasterManager smanager = new StoreMasterManager();
            UOMManager umanager = new UOMManager();
            FloorRetMaterialManager floorReturnManager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager fmanager = new FloorReturnMaterialDetailsManager();
            model = (from frm in floorReturnManager.Get()
                     join b in fmanager.Get() on frm.FloorReturnMaterialId equals b.FloorReturnMaterialId
                    
                     join y in mmanager.Get() on b.MaterialMasterId equals y.MaterialMasterId
                     join x in mnmanager.Get() on y.MaterialName equals x.MaterialMasterID
                     join z in gmanager.Get() on y.MaterialGroupMasterId equals z.MaterialGroupMasterId
                     join a in smanager.Get() on y.StoreMasterId equals a.StoreMasterId
                     where frm.FrmNo == FrmNO
                     select new FloorReturnMaterialDetailsModel
                     {
                         FloorReturnMaterialDetailsId = b.FloorReturnMaterialDetailsId,
                         MaterialName = x.MaterialDescription,
                         MaterialMasterId= b.MaterialMasterId,
                         GroupName = z.GroupName,
                         FrmNo = frm.FrmNo,
                         StoreName = a.StoreName,
                         Style = b.Style,
                         Category = b.Category,
                         ReturnQuantity = b.ReturnQuantity,
                         IssuedQuantity = b.IssuedQuantity,
                         Rate = b.Rate,
                           MaterialType = b.MaterialType
                     }).Distinct().ToList();
            var distinctList = model.GroupBy(x => x.MaterialName).Select(g => g.First()).ToList();
            listmodel = distinctList;
            return listmodel;
        }
        public FloorReturnMaterialDetailsModel GetFloorMaterialNamesByMaterialId(int FloorReturnMaterialDetailsId)
        {
            FloorReturnMaterialDetailsModel model = new FloorReturnMaterialDetailsModel();
            MaterialNameManager mnmanager = new MaterialNameManager();
            MaterialManager mmanager = new MaterialManager();
            MaterialGroupManager gmanager = new MaterialGroupManager();
            StoreMasterManager smanager = new StoreMasterManager();
            UOMManager umanager = new UOMManager();
            FloorRetMaterialManager floorReturnManager = new FloorRetMaterialManager();
            FloorReturnMaterialDetailsManager fmanager = new FloorReturnMaterialDetailsManager();
            model = (from b in fmanager.Get()
                     join frm in floorReturnManager.Get() on b.FloorReturnMaterialId equals frm.FloorReturnMaterialId
                     join y in mmanager.Get() on b.MaterialMasterId equals y.MaterialMasterId
                     join x in mnmanager.Get() on y.MaterialName equals x.MaterialMasterID                   
                     join u in umanager.Get() on y.Uom equals u.UomMasterId
                     join z in gmanager.Get() on y.MaterialGroupMasterId equals z.MaterialGroupMasterId
                     join a in smanager.Get() on y.StoreMasterId equals a.StoreMasterId
                     where b.FloorReturnMaterialDetailsId == FloorReturnMaterialDetailsId
                     select new FloorReturnMaterialDetailsModel
                     {
                         FloorReturnMaterialId = b.FloorReturnMaterialId,
                         FloorReturnMaterialDetailsId = b.FloorReturnMaterialDetailsId,
                         MaterialMasterId = b.MaterialMasterId,
                         MaterialName = x.MaterialDescription,
                         GroupMasterId = b.GroupMasterId,
                         GroupName = z.GroupName,
                         Uom = b.Uom,
                         LongUnitName = u.LongUnitName,
                         StoreMasterId = b.StoreMasterId,
                         StoreName = a.StoreName,
                         Style = b.Style,
                         Category = b.Category,
                         ReturnQuantity = b.ReturnQuantity,
                         IssuedQuantity = b.IssuedQuantity,
                         Rate = b.Rate,
                         IoNo = b.IoNo,
                         Is_IssueNo = frm.Is_IssueNo,
                         MaterialType = b.MaterialType

                     }).FirstOrDefault();

            return model;
        }

        public List<FloorReturnSizeRange> GetFloorSizeByFloorId(int FloorReturnMaterialDetailsId)
        {
            List<FloorReturnSizeRange> model = new List<FloorReturnSizeRange>();

            FloorReturnSizeManager fmanager = new FloorReturnSizeManager();
            model = (from b in fmanager.Get()


                     where b.FloorReturnMaterialDetailsId == FloorReturnMaterialDetailsId
                     select new FloorReturnSizeRange
                     {
                         SizeRangeFloorMaterialId = b.SizeRangeFloorMaterialId,
                         FloorReturnMaterialDetailsId = b.FloorReturnMaterialDetailsId,
                         SizeRange = b.SizeRange,
                         Quantity = b.Quantity,
                         ReturnedQuantity = b.ReturnedQuantity,


                                                     
                     }).ToList();

            return model;
        }





        #endregion
    }
}
