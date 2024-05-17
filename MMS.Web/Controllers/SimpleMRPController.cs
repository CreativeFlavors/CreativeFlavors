using Excel;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.SimpleMRPModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class SimpleMRPController : Controller
    {
        #region View
        public ActionResult SimpleMRP(SimpleMRPModel model)
        {
            // SimpleMRPModel model = new SimpleMRPModel();
            return View(model);
        }
        public ActionResult GetBuyermWithWeekNO(int BuyerName)
        {
            SeasonManager seasonManager = new SeasonManager();
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = uOMManager.GetInternalIO().Where(x => x.BuyerName == BuyerName && x.WeekNo != null).OrderBy(x => x.WeekNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.WeekNo,
                                        Value = item.WeekNo.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLotNo(string WeekNo)
        {
            SeasonManager seasonManager = new SeasonManager();
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<System.Web.Mvc.SelectListItem> items = uOMManager.GetInternalIOWeek(WeekNo).OrderBy(x => x.WeekNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.LotNo,
                                        Value = item.OrderEntryId.ToString()
                                    }
                                    ).ToList();
            items = items.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, ShotName);
            List<InternalOrderEntryForm> BuyerOrderEntryList = new List<InternalOrderEntryForm>();
            List<SelectListItem> seasonList = new List<SelectListItem>();
            var items_ = (from x in uOMManager.GetInternalIO()
                          join y in seasonManager.Get()
                           on x.BuyerSeason equals y.SeasonMasterId
                          where x.WeekNo == WeekNo
                          select new { y.SeasonName, y.SeasonMasterId });
            foreach (var item in items_)
            {
                SelectListItem selectlist = new SelectListItem()
                {
                    Text = item.SeasonName,
                    Value = item.SeasonMasterId.ToString()
                };
                seasonList.Add(selectlist);
            }
            seasonList = seasonList.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName_ = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            seasonList.Insert(0, ShotName_);
            return Json(new { lotNoList = items, seasonList_ = seasonList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLotNoWithSeason(string SeasonName)
        {
            SeasonManager seasonManager = new SeasonManager();
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<InternalOrderEntryForm> BuyerOrderEntryList = new List<InternalOrderEntryForm>();
            List<SelectListItem> lotNoList = new List<SelectListItem>();
            var items_ = (from x in uOMManager.GetInternalIO()
                          join y in seasonManager.Get()
                           on x.BuyerSeason equals y.SeasonMasterId
                          where y.SeasonName == SeasonName
                          select new { x.LotNo, x.OrderEntryId });
            foreach (var item in items_)
            {
                SelectListItem selectlist = new SelectListItem()
                {
                    Text = item.LotNo,
                    Value = item.OrderEntryId.ToString()
                };
                lotNoList.Add(selectlist);
            }
            lotNoList = lotNoList.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            var ShotName_ = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            lotNoList.Insert(0, ShotName_);
            return Json(new { lotNoList_ = lotNoList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLotWithOrderNO(string LotNo, string SeasonID, string buyer)
        {
            SeasonManager seasonManager = new SeasonManager();
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<InternalOrderEntryForm> BuyerOrderEntryList = new List<InternalOrderEntryForm>();
            List<SelectListItem> orderList = new List<SelectListItem>();
            var items_ = (from x in uOMManager.GetInternalIO()
                          where x.LotNo == LotNo && x.BuyerSeason == Convert.ToInt32(SeasonID) && x.BuyerName == Convert.ToInt32(buyer)
                          select new { x.BuyerOrderSlNo, x.OrderEntryId, x.TotalAmount }).ToList();

            decimal? totalCount = items_.Sum(x => x.TotalAmount);
            foreach (var item in items_)
            {
                SelectListItem selectlist = new SelectListItem()
                {
                    Text = item.BuyerOrderSlNo,
                    Value = item.OrderEntryId.ToString()
                };
                orderList.Add(selectlist);
            }
            orderList = orderList.GroupBy(x => x.Text).Select(g => g.First()).ToList();
            return Json(new { lotNoList_ = orderList, Count = totalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTotalCount(string LotNo)
        {
            SeasonManager seasonManager = new SeasonManager();
            BuyerOrderEntryManager uOMManager = new BuyerOrderEntryManager();
            List<InternalOrderEntryForm> BuyerOrderEntryList = new List<InternalOrderEntryForm>();
            List<SelectListItem> orderList = new List<SelectListItem>();
            var PackingDetail = JsonConvert.DeserializeObject<List<string>>(LotNo);
            decimal? totalCount = 0;
            foreach (var item in PackingDetail)
            {
                var items_ = (from x in uOMManager.GetInternalIO()
                              where x.BuyerPoNo == item
                              select new { x.BuyerOrderSlNo, x.OrderEntryId, x.TotalAmount });
                totalCount += items_.Sum(x => x.TotalAmount);
            }



            return Json(new { Count = totalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SimpleMRPModelWithDetails(SimpleMRPModel model)
        {
            List<SelectListItem> listBoxItem = new List<SelectListItem>();
            ImportIOManager importIOManager = new ImportIOManager();
            List<ImportIO> importList = new List<ImportIO>();
            importList = importIOManager.GetIO(model.SimpleMRPID);
            if (importList.Count > 0)
            {
                foreach (var item in importList)
                {
                    SelectListItem selectlist = new SelectListItem()
                    {
                        Text = item.IoNO,
                        Value = item.SimpleMRPID.ToString()
                    };
                    listBoxItem.Add(selectlist);
                }
                model.IOS = listBoxItem;
            }
            return View(model);
        }
        public ActionResult SimpleMRPGrid()
        {
            SimpleMRPModel model = new SimpleMRPModel();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            model.simpleMRPList = simpleMRPManager.Get();
            return PartialView("Partial/SimpleMRPGrid", model);
        }

        public ActionResult IOSelectList(string SelectText, string SimpleMRPID)
        {

            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            InternalOrderEntryForm order = new InternalOrderEntryForm();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            List<BomGrid> listMaterial = new List<BomGrid>();
            var SizeQuantityRate_ = JsonConvert.DeserializeObject<List<string>>(SelectText);
            List<BOMMaterial> bomMaterialList = new List<BOMMaterial>();
            BOMMaterial bomMaterial = new BOMMaterial();
            string OrderMessage = "";
            string BOMMessage = "";
            string SaveBOMMessage = "";
            List<SelectListItem> listBoxItem = new List<SelectListItem>();
            SimpleMRPModel model = new SimpleMRPModel();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            List<InternalOrderEntryForm> listOfOrders_ = new List<InternalOrderEntryForm>();
            List<BillOfMaterial> listBillOfMaterial = new List<BillOfMaterial>();
            List<MaterialMaster> listOfMaterial = new List<MaterialMaster>();
            MaterialManager materialManager = new MaterialManager();
            List<MaterialGroupMaster_> listOfMaterialGroupMaster = new List<MaterialGroupMaster_>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            List<SizeRangeQtyRate> listOfSizeRange = new List<SizeRangeQtyRate>();
            string orderExited = "";
            string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
            List<MRPRequirement> listOfMRPRequirement = new List<MRPRequirement>();
            try
            {
                listOfOrders_ = buyerOrderEntryManager.GetInternalIO();
                listBillOfMaterial = billOfMaterialManager.Get();
                listOfMaterial = materialManager.MaterialList();
                listOfMaterialGroupMaster = materialGroupManager.Get();
                listOfSizeRange = buyerOrderEntryManager.GetAllOrderSize();
                foreach (var item in SizeQuantityRate_)
                {
                    MRPSelectedValues selectedValues = new MRPSelectedValues();
                    BuyerOrderCreation buyerOrderCreation = new BuyerOrderCreation();
                    order = listOfOrders_.Where(x => x.BuyerOrderSlNo == item).FirstOrDefault();
                    if (order == null)
                    {
                        OrderMessage = "Please make a entry on internal order for this:" + item;
                        return Json(new { InternalStatus = OrderMessage, status = SaveBOMMessage, orderStatus = orderExited, BOMStatus = BOMMessage }, JsonRequestBehavior.AllowGet);
                    }
                    if (order != null && order.OrderEntryId != 0)
                    {
                        billOfMaterial = listBillOfMaterial.Where(x => x.LinkBomNo == order.OurStyle).FirstOrDefault();

                        if (billOfMaterial == null)
                        {
                            Product_BuyerStyleManager productBuyerStyleManager = new Product_BuyerStyleManager();
                            Product_BuyerStyleMaster productBuyerStyle = new Product_BuyerStyleMaster();
                            productBuyerStyle = productBuyerStyleManager.GetProductOrBuyerStyleId(Convert.ToInt32(order.OurStyle));
                            BOMMessage = "Please make a entry BOM for this Style:" + productBuyerStyle.OurStyle;
                            return Json(new { InternalStatus = OrderMessage, status = SaveBOMMessage, orderStatus = orderExited, BOMStatus = BOMMessage }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    MRPSelectedValues mrpselectedValues = new MRPSelectedValues();
                    mrpselectedValues = simpleMRPManager.checkOrderNumber(item);
                    if (mrpselectedValues != null)
                    {
                        orderExited += item + " ";
                    }
                    else
                    {
                        if (billOfMaterial != null)
                        {
                            bomMaterialList = billOfMaterialManager.GetBomMaterialBOMID(billOfMaterial.BomId);
                        }
                        selectedValues.IONumberID = item;
                        selectedValues.SimpleMRPID = Convert.ToInt32(SimpleMRPID);
                        SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
                        if (order != null && billOfMaterial != null)
                        {
                            simpleMRPManager.SimpleMRPSelectedPost(selectedValues);
                            foreach (var each in bomMaterialList)
                            {
                                MRPRequirement mrpRequirement = new MRPRequirement();
                                mrpRequirement.RequiredQty = 0;
                                decimal? Amount = 0;
                                decimal? qty = 0;
                                BOMMaterial bomMaterial_ = new BOMMaterial();
                                MaterialGroupMaster_ materialGroupMaster = new MaterialGroupMaster_();
                                SizeScheduleRange sizeScheduleRange = new SizeScheduleRange();
                                MaterialMaster materialMaster = new MaterialMaster();                              
                                materialMaster = listOfMaterial.Where(x => x.MaterialMasterId == each.MaterialName).FirstOrDefault();
                                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                                materialGroupMaster = listOfMaterialGroupMaster.Where(x => x.MaterialGroupMasterId == each.MaterialGroupMasterId).FirstOrDefault();
                                List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                                if (materialGroupMaster.IsSize == true)
                                {
                                    mrpRequirement = simpleMRPManager.IsGroupSize(mrpRequirement, each, materialMaster, listOfSizeRange, consString, order);
                                }
                                else
                                {
                                    string endurea = System.Configuration.ConfigurationManager.AppSettings["EnduraURL"].ToString();
                                    string host = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                                    string Hostname = "http://" + host;
                                    if (!string.IsNullOrEmpty(endurea) && Hostname == endurea)
                                    {
                                        Amount = Convert.ToDecimal(each.BuyerNorms) * order.TotalAmount;
                                    }
                                    else
                                    {
                                        Amount = Convert.ToDecimal(each.TotalNorms) * order.TotalAmount;
                                    }
                                    qty = Amount;
                                    materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                                    if (materialMaster.PurchasePacket != 0)
                                    {
                                        mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                                    }
                                    else
                                    {
                                        mrpRequirement.RequiredQty = qty;
                                    }
                                    mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                                }
                                mrpRequirement.IsMRP = true;
                                mrpRequirement.ParentCommonBOMID = each.ParentCommonBOMID;
                                mrpRequirement.ParentBOMID = each.ParentBOMID;
                                mrpRequirement.BOMID = each.BOMID;
                                mrpRequirement.MaterialCategoryMasterId = each.MaterialCategoryMasterId;
                                mrpRequirement.MaterialGroupMasterId = each.MaterialGroupMasterId;
                                mrpRequirement.SubstanceMasterId = each.SubstanceMasterId;
                                mrpRequirement.MaterialName = each.MaterialName;
                                mrpRequirement.Conversion = each.Conversion;
                                mrpRequirement.ColorMasterId = each.ColorMasterId;
                                mrpRequirement.SizeScheduleMasterId = each.SizeScheduleMasterId;
                                mrpRequirement.SampleNorms = each.SampleNorms;
                                mrpRequirement.Wastage = each.Wastage;
                                mrpRequirement.SupplierMasterID = each.SupplierMasterID;
                                mrpRequirement.WastageQty = each.WastageQty;
                                mrpRequirement.TotalNorms = each.TotalNorms;
                                mrpRequirement.BuyerNorms = each.BuyerNorms;
                                mrpRequirement.Rate = Convert.ToDecimal(materialMaster.Price);
                                mrpRequirement.SizeRangeMasterID = each.SizeRangeMasterID;
                                mrpRequirement.NoofSets = each.NoofSets;
                                mrpRequirement.BuyerNorms = each.BuyerNorms;
                                mrpRequirement.WastageQtyUOM = each.WastageQtyUOM;
                                mrpRequirement.TotalNormsUOM = each.TotalNormsUOM;
                                mrpRequirement.SimpleMRPID = selectedValues.SimpleMRPID;
                                mrpRequirement.OrderNo = order.BuyerOrderSlNo;
                                listOfMRPRequirement.Add(mrpRequirement);
                                SaveBOMMessage = "Saved Successfully";
                            }
                        }
                    }

                }
                simpleMRPManager.BulkInsertTOMRPRequirement(listOfMRPRequirement, consString);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
               // Logger.Log(ex.str.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Json(new { InternalStatus = OrderMessage, status = SaveBOMMessage, orderStatus = orderExited, BOMStatus = BOMMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SimpleMRPDetails(int SimpleMRPID)
        {
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            SimpleMRP simpleMRP = new SimpleMRP();
            SimpleMRPModel model = new SimpleMRPModel();
            simpleMRP = simpleMRPManager.GetSimpleMRPID(SimpleMRPID);
            List<SelectListItem> listBoxItem = new List<SelectListItem>();
            List<SelectListItem> SelectedlistBoxItem = new List<SelectListItem>();
            ImportIOManager importIOManager = new ImportIOManager();
            List<ImportIO> importList = new List<ImportIO>();
            List<MRPSelectedValues> mrpSelectedList = new List<MRPSelectedValues>();
            mrpSelectedList = simpleMRPManager.GeMRPSelectedValuest().Where(x => x.SimpleMRPID == SimpleMRPID).ToList();
            if (SimpleMRPID != 0)
            {
                importList = importIOManager.Get().Where(x => x.SimpleMRPID == SimpleMRPID).ToList();
            }

            if (importList.Count > 0)
            {
                foreach (var item in importList)
                {
                    SelectListItem selectlist = new SelectListItem()
                    {
                        Text = item.IoNO,
                        Value = item.SimpleMRPID.ToString()
                    };
                    listBoxItem.Add(selectlist);
                }
                model.IOS = listBoxItem;
            }
            if (mrpSelectedList.Count > 0)
            {
                foreach (var item in mrpSelectedList)
                {
                    SelectListItem selectlist_ = new SelectListItem()
                    {
                        Text = item.IONumberID,
                        Value = item.MRPSelectedID.ToString()
                    };
                    SelectedlistBoxItem.Add(selectlist_);
                }
                model.selectlist = SelectedlistBoxItem;
            }
            if (simpleMRP != null)
            {
                model.SimpleMRPID = simpleMRP.SimpleMRPID;
                model.MRPNO = simpleMRP.MRPNO;
                model.MRPType = simpleMRP.MRPType;
                model.BuyerNameid = simpleMRP.BuyerNameid;
                model.WeekNO = simpleMRP.WeekNO.Value;
                model.SeasonID = simpleMRP.SeasonID;
                model.LotNO = (simpleMRP.LotNO == null) ? 0 : simpleMRP.LotNO.Value;
                model.TotalOrderCount = simpleMRP.TotalOrderCount;
                model.SizeRangeID = simpleMRP.SizeRangeID;
                if (simpleMRP.TO != null)
                {
                    string[] formats = { "dd/MM/yyyy" };
                    var dateTime = DateTime.ParseExact(simpleMRP.TO, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                }
                if (simpleMRP.From != null)
                {
                    model.From = simpleMRP.From;
                }
                if (simpleMRP.TO != null)
                {
                    model.TO = simpleMRP.TO;
                }
                model.CustomerPlan = simpleMRP.CustomerPlan;
                model.ProductionPlanBasic = simpleMRP.ProductionPlanBasic;
                model.OrderBasic = simpleMRP.OrderBasic;
                model.RejectionorReplacement = simpleMRP.RejectionorReplacement;
                model.OverConsumption = simpleMRP.OverConsumption;
                model.Req_Qty = simpleMRP.Req_Qty;
                model.UOM = simpleMRP.UOM;
                model.Rate = simpleMRP.Rate;
                model.TotalNorms = simpleMRP.TotalNorms;
                model.Detailed = simpleMRP.Detailed;
                model.Consolidate = simpleMRP.Consolidate;
                model.IsDeleted = simpleMRP.IsDeleted;
                model.DeletedBy = simpleMRP.DeletedBy;
                model.DeletedOn = simpleMRP.DeletedOn;
                if (simpleMRP.MRPDate != null)
                {
                    //string date = Convert.ToDateTime(simpleMRP.MRPDate).Date.ToString();
                    model.MRPDate = simpleMRP.MRPDate;
                }

                model.CreatedBy = simpleMRP.CreatedBy;
                model.UpdatedBy = simpleMRP.UpdatedBy;
                model.CreatedDate = simpleMRP.CreatedDate;
                model.UpdatedDate = simpleMRP.UpdatedDate;
            }
            int id_;
            string id = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateSimpleMRPID_code();
            if (id == "")
            {
                id = "0";
            }
            id_ = Convert.ToInt32(id);
            id_++;
            if (SimpleMRPID == 0)
            {
                model.MRPCode = id_.ToString();
            }
            else
            {
                //model.MachineCode = "MAC" + id_;
                model.MRPCode = simpleMRP.MRPNO;
            }
            return PartialView("Partial/SimpleMRPDetails", model);
        }
        #endregion



        #region Curd Operation
        public ActionResult SimpleMRPSave(SimpleMRPModel Model)
        {
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            SimpleMRP simpleMRP_ = new SimpleMRP();
            SimpleMRP simple_ = new SimpleMRP();
            SimpleMRP simpleMRP = new SimpleMRP();
            SimpleMRPModel model = new SimpleMRPModel();
            SimpleMRP ischeck = new SimpleMRP();
            if (Model.MRPNO == null && Model.MRPCode != null)
            {
                simpleMRP_ = simpleMRPManager.GetMRPCode(Model.MRPCode);
            }
            else if (Model.MRPNO != null && Model.MRPCode == null)
            {
                simpleMRP_ = simpleMRPManager.GetMRPNO(Model.MRPNO);
            }
            simpleMRP.SimpleMRPID = Model.SimpleMRPID;
            if (Model.MRPNO != null)
            {
                simpleMRP.MRPNO = Model.MRPNO;
            }
            else
            {
                simpleMRP.MRPNO = Model.MRPCode;
            }
            string Message = "";
            ischeck = simpleMRPManager.IscheckDuplicate(Model.WeekNO, Model.SeasonID.Value, Model.LotNO, Model.BuyerNameid.Value);
            if (ischeck != null && Model.SimpleMRPID == 0)
            {
                Message = "Already Existed";
                return Json(new { Status = Message }, JsonRequestBehavior.AllowGet);
            }
            simpleMRP.MRPType = Model.MRPType;
            simpleMRP.BuyerNameid = Model.BuyerNameid;
            simpleMRP.WeekNO = Model.WeekNO;
            simpleMRP.SeasonID = Model.SeasonID;
            simpleMRP.LotNO = Model.LotNO;
            simpleMRP.TotalOrderCount = Model.TotalOrderCount;
            simpleMRP.SizeRangeID = Model.SizeRangeID;
            //var dateTime = DateTime.ParseExact(model.From.ToString(), formats, new CultureInfo("en-US"), DateTimeStyles.None);
            simpleMRP.From = Model.From;
            // var dateTimeTO = DateTime.ParseExact(model.TO.ToString(), formats, new CultureInfo("en-US"), DateTimeStyles.None);
            simpleMRP.TO = Model.TO;
            simpleMRP.CustomerPlan = Model.CustomerPlan;
            simpleMRP.ProductionPlanBasic = Model.ProductionPlanBasic;
            simpleMRP.OrderBasic = Model.OrderBasic;
            simpleMRP.RejectionorReplacement = Model.RejectionorReplacement;
            simpleMRP.OverConsumption = Model.OverConsumption;
            simpleMRP.Req_Qty = Model.Req_Qty;
            simpleMRP.UOM = Model.UOM;
            simpleMRP.Rate = Model.Rate;
            simpleMRP.TotalNorms = Model.TotalNorms;
            simpleMRP.Detailed = Model.Detailed;
            simpleMRP.Consolidate = Model.Consolidate;
            simpleMRP.IsDeleted = Model.IsDeleted;
            simpleMRP.DeletedBy = Model.DeletedBy;
            simpleMRP.DeletedOn = Model.DeletedOn;
            simpleMRP.MRPDate = (Model.MRPDate);
            if (simpleMRP_ == null)
            {
                simple_ = simpleMRPManager.SimpleMRPPost(simpleMRP);
            }
            else
            {
                simpleMRP.SimpleMRPID = simpleMRP_.SimpleMRPID;
                simple_ = simpleMRPManager.Put(simpleMRP);
            }
            return Json(simple_, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int SimpleMRPID)
        {
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<MRPRequirement> listBOMMaterial = new List<MRPRequirement>();
            SimpleMRP simpleMRP_ = new SimpleMRP();
            string Message = "";
            simpleMRP_ = simpleMRPManager.GetSimpleMRPID(SimpleMRPID);
            if (simpleMRP_ != null)
            {
                listBOMMaterial = bomMaterialListManager.GetMRPMaterialList(simpleMRP_.SimpleMRPID).ToList();              
                simpleMRPManager.Delete(SimpleMRPID);
                Message = "Success";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Excel Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Importexcel(HttpPostedFileBase upload, SimpleMRPModel model_)
        {
            List<SimpleMRPModel> listSimpleMRPModel = new List<SimpleMRPModel>();
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
                    SimpleMRP simpleMRP_ = new SimpleMRP();
                    SimpleMRP simpleMRP = new SimpleMRP();
                    SimpleMRPModel model = new SimpleMRPModel();
                    simpleMRP_ = simpleMRPManager.GetMRPNO1(model_.MRPCode);
                    simpleMRP.SimpleMRPID = model_.SimpleMRPID;
                    simpleMRP.MRPNO = model_.MRPCode;
                    simpleMRP.MRPType = model_.MRPType;
                    simpleMRP.BuyerNameid = model_.BuyerNameid;
                    simpleMRP.WeekNO = model_.WeekNO;
                    simpleMRP.SeasonID = model_.SeasonID;
                    simpleMRP.LotNO = model_.LotNO;
                    simpleMRP.SizeRangeID = model_.SizeRangeID;
                    simpleMRP.From = model_.From;
                    simpleMRP.TO = model_.TO;
                    simpleMRP.CustomerPlan = model_.CustomerPlan;
                    simpleMRP.ProductionPlanBasic = model_.ProductionPlanBasic;
                    simpleMRP.OrderBasic = model_.OrderBasic;
                    simpleMRP.RejectionorReplacement = model_.RejectionorReplacement;
                    simpleMRP.OverConsumption = model_.OverConsumption;
                    simpleMRP.Req_Qty = model_.Req_Qty;
                    simpleMRP.UOM = model_.UOM;
                    simpleMRP.Rate = model_.Rate;
                    simpleMRP.TotalNorms = model_.TotalNorms;
                    simpleMRP.Detailed = model_.Detailed;
                    simpleMRP.Consolidate = model_.Consolidate;
                    simpleMRP.IsDeleted = model_.IsDeleted;
                    simpleMRP.DeletedBy = model_.DeletedBy;
                    simpleMRP.DeletedOn = model_.DeletedOn;
                    simpleMRP.MRPDate = (model_.MRPDate);
                    if (simpleMRP_ == null)
                    {
                        simpleMRP = simpleMRPManager.SimpleMRPPost(simpleMRP);
                    }
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.InputStream;
                    // We return the interface, so that
                    IExcelDataReader reader = null;
                    DataSet result = null;
                    try
                    {

                        if (upload.FileName.EndsWith(".xls") || upload.FileName.EndsWith(".ods"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (upload.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                            return RedirectToAction("SimpleMRPModelWithDetails");
                        }

                        reader.IsFirstRowAsColumnNames = true;
                        result = reader.AsDataSet();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = ex.Message;
                    }
                    string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                    var table = new DataTable(); // create the datatable
                    table = result.Tables[0];
                    //for (int i = DTTable2.Rows.Count - 1; i >= 0; i--)
                    //    if (DTTable2.Rows[i]["ItemID"].ToString() == DTTable1.Rows[0]["ItemID"].ToString())
                    //        DTTable2.Rows.RemoveAt(i);
                    ImportIOManager importIOManager = new ImportIOManager();
                    List<ImportIO> listImportIO = new List<ImportIO>();
                    for (int i = table.Rows.Count - 1; i >= 0; i--)
                        if (table.Rows[i]["IoNO"].ToString() != "")
                        {
                            string io_ = table.Rows[i]["IoNO"].ToString();
                            listImportIO = importIOManager.Get().Where(x => x.IoNO == io_).ToList();
                            if (listImportIO.Count > 0)
                            {
                                table.Rows.RemoveAt(i);
                            }

                        }

                    System.Data.DataColumn newColumn = new System.Data.DataColumn("CreatedBy", typeof(System.String));
                    newColumn.DefaultValue = HttpContext.Session["UserName"].ToString();
                    System.Data.DataColumn newColumn1 = new System.Data.DataColumn("CreatedDate", typeof(System.String));
                    System.Data.DataColumn SimplemrpID = new System.Data.DataColumn("SimpleMRPID", typeof(System.Int32));
                    SimplemrpID.DefaultValue = simpleMRP.SimpleMRPID;
                    newColumn1.DefaultValue = DateTime.Now;
                    table.Columns.Add(newColumn1);
                    table.Columns.Add(newColumn);
                    table.Columns.Add("UpdatedBy").DefaultValue = "";
                    table.Columns.Add(SimplemrpID);
                    using (SqlConnection con = new SqlConnection(consString))

                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "ImportIO";
                            sqlBulkCopy.ColumnMappings.Add("Sno", "Sno");
                            sqlBulkCopy.ColumnMappings.Add("IoNO", "IoNO");
                            sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                            sqlBulkCopy.ColumnMappings.Add("SimplemrpID", "SimpleMRPID");
                            con.Open();
                            sqlBulkCopy.WriteToServer(table);
                            con.Close();
                        }
                    }
                    reader.Close();
                    var query = table.AsEnumerable().ToList();
                    foreach (var item in query)
                    {
                        SimpleMRPModel models = new SimpleMRPModel();

                        models.IsExit = false;
                        listSimpleMRPModel.Add(models);
                        model_.SimpleMRPID = simpleMRP.SimpleMRPID;
                        model_.importIOList = listSimpleMRPModel;
                    }

                    return RedirectToAction("SimpleMRPModelWithDetails", model_);
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return RedirectToAction("SimpleMRPModelWithDetails", model_);
        }
        public JsonResult IsMRPNo(string MRPNO)
        {
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            SimpleMRP simpleMRP_ = new SimpleMRP();
            bool mrpNO = false;
            simpleMRP_ = simpleMRPManager.GetMRPNO1(MRPNO);
            if (simpleMRP_ != null)
            {
                mrpNO = true;
            }
            return Json(!mrpNO, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string filter)
        {
            List<SimpleMRP> simpleMRPModelList = new List<SimpleMRP>();
            SimpleMRPManager simpleMrpManager = new SimpleMRPManager();
            simpleMRPModelList = simpleMrpManager.Get();

            simpleMRPModelList = simpleMRPModelList.Where(s => s.MRPNO != null && s.MRPNO.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            SimpleMRPModel model = new SimpleMRPModel();
            model.simpleMRPList = simpleMRPModelList;
            return PartialView("Partial/SimpleMRPGrid", model);
        }

        #endregion

    }
}
