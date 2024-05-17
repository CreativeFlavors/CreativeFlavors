using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers;
using MMS.Web.Models.StockModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class MaterialOpeningStockController : Controller
    {
        //
        // GET: /MaterialOpeningStock/
        #region MaterialOpening View
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MaterialOpening()
        {
            MaterialOpeningMaster mo = new MaterialOpeningMaster();
            MaterialOpeningModel model = new MaterialOpeningModel();
            MaterialOpeningStockManager manager = new MaterialOpeningStockManager();
            return View("~/Views/Stock/MaterialOpening/MaterialOpening.cshtml", model);
        }

        public ActionResult MaterialOpeningMasterGrid()
        {
            MaterialOpeningModel sm = new MaterialOpeningModel();
            MaterialOpeningStockManager materialOpeningManager = new MaterialOpeningStockManager();
            sm.MaterialOpeningMasterList = materialOpeningManager.Get();

            return PartialView("~/Views/Stock/MaterialOpening/Partial/MaterialOpeningMasterGrid.cshtml", sm);
        }

        [HttpPost]
        public ActionResult MaterialOpeningMasterDetails(int MaterialOpeningId)
        {
            MaterialOpeningStockManager materilaoPeningManager = new MaterialOpeningStockManager();
            MaterialOpeningMaster materialMaster = new MaterialOpeningMaster();
            MaterialOpeningModel model = new MaterialOpeningModel();
            materialMaster = materilaoPeningManager.GetmaterialOpeningId(MaterialOpeningId);

            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getMaterialCodeId();
            ID++;

            if (materialMaster.MaterialOpeningId == 0)
            {
                model.sizeRangeDetailslist = null;
            }

            if (materialMaster.MaterialOpeningId != 0)
            {
                model.Store = materialMaster.Store;
                model.MaterialCategoryMasterId = materialMaster.MaterialCategoryMasterId;
                model.MaterialGroupMasterId = materialMaster.MaterialGroupMasterId;
                model.MaterialMasterId = materialMaster.MaterialMasterId;
                model.ColorMasterId = materialMaster.ColorMasterId;
                model.PrimaryUomMasterId = materialMaster.PrimaryUomMasterId;
                model.MaterialType = materialMaster.MaterialType;
                model.SecondaryUomMasterId = materialMaster.SecondaryUomMasterId;
                model.Date = Convert.ToDateTime(materialMaster.Date).Date.ToString("dd/MM/yyyy");
                MaterialMaster material = new MaterialMaster();
                MaterialManager materialManager = new MaterialManager();
                material= materialManager.GetMaterialMasterId(materialMaster.MaterialMasterId);
                if(model.ColorMasterId==0)
                {
                    model.ColorMasterId = material.ColorMasterId;
                }
                if(model.PrimaryUomMasterId==0)
                {
                    model.PrimaryUomMasterId = material.PrimaryUnit.Value;
                }
                if (model.SecondaryUomMasterId == 0)
                {
                    model.SecondaryUomMasterId = material.PacketUnit.Value;
                }
                model.SizeRangeMasterId = materialMaster.SizeRangeMasterId;
                model.Qty = materialMaster.Qty;
                model.MaterialCode = materialMaster.MaterialCode;
                model.Rate = materialMaster.Rate;
                model.Remarks = materialMaster.Remarks;
                model.MaterialPcs = materialMaster.MaterialPcs;
                model.QtyPcs = materialMaster.QtyPcs;
                model.CreatedDate = materialMaster.CreatedDate.Value;
                model.UpdatedDate = DateTime.Now;
                model.MaterialOpeningId = materialMaster.MaterialOpeningId;
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                StoreMaster storeMaster = new StoreMaster();
                storeMaster = storeMasterManager.GetStoreMasterId(Convert.ToInt32(materialMaster.Store));
                model.storeMaster = storeMaster;
                List<MaterialOpeningSizeQtyRate> sizeRangeDetais = new List<MaterialOpeningSizeQtyRate>();
                sizeRangeDetais = materilaoPeningManager.GetSizeQuantityRangeById(MaterialOpeningId);
                model.sizeRangeDetailslist = sizeRangeDetais;

            }

            if (model.MaterialName != 0)
            {
                model.MaterialCode = materialMaster.MaterialCode;
            }
            else
            {
                model.MaterialCode = "MAT" + ID;
            }

            return PartialView("~/Views/Stock/MaterialOpening/Partial/MaterialOpeningMasterDetails.cshtml", model);
        }

        public ActionResult GetSizeRange(int SizeRangeMasterId)
        {
            SizeRangeDetailsManager manager = new SizeRangeDetailsManager();
            List<SizeRangeDetails> result = new List<SizeRangeDetails>();
            result = manager.GetSizeRangeMasterId(SizeRangeMasterId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Curd Operation
        [HttpPost]
        public ActionResult MaterialOpeningStock(MaterialOpeningModel model)
        {
            MaterialOpeningMaster materialOpening = new MaterialOpeningMaster();
            int MaterialOpeningId = 0;
            string Message = "";
            if (ModelState.IsValid)
            {
                MaterialOpeningMaster materialstock = new MaterialOpeningMaster();
                MaterialOpeningMaster materialstock_ = new MaterialOpeningMaster();
                MaterialOpeningStockManager materialstockManager = new MaterialOpeningStockManager();

                materialOpening.Store = model.Store;
                materialOpening.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                materialOpening.MaterialGroupMasterId = model.MaterialGroupMasterId;
                materialOpening.MaterialMasterId = model.MaterialMasterId;
                materialOpening.MaterialCode = model.MaterialCode;
                materialOpening.ColorMasterId = model.ColorMasterId_!=0?model.ColorMasterId_ :model.ColorMasterId.Value;
                materialOpening.MaterialType = model.MaterialType;
                materialOpening.PrimaryUomMasterId = model.PrimaryUomMasterId;
                materialOpening.SecondaryUomMasterId =model.SecondaryUomMasterId;
                var format = "dd/MM/yyyy";
                DateTime MOdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                materialOpening.Date = MOdate.Date;
                materialOpening.Qty = model.Qty;
                materialOpening.SizeRangeMasterId = model.SizeRangeMasterId;
                materialOpening.Rate = model.Rate;
                materialOpening.MaterialOpeningId = model.MaterialOpeningId;
                materialOpening.MaterialPcs = model.MaterialPcs;
                materialOpening.QtyPcs = model.QtyPcs;
                materialOpening.Remarks = model.Remarks;
                materialOpening.CreatedDate = DateTime.Now;
                if (model.MaterialOpeningId == 0)
                {
                    materialstock_ = materialstockManager.iSExistmaterialOpening(model.MaterialMasterId,model.MaterialType.Value);
                }

                if (materialstock_ != null && model.MaterialOpeningId == 0)
                {
                    Message = "Alreday Exist";
                    return Json(Message, JsonRequestBehavior.AllowGet);
                }
                MaterialOpeningId = materialstockManager.Post(materialOpening);
                if (MaterialOpeningId != 0 && model.MaterialOpeningId == 0)
                {
                    Message = "Saved Successfully";
                }
                else if (MaterialOpeningId != 0 && model.MaterialOpeningId != 0)
                {
                    Message = "Updated Successfully";
                }
                if (model.MaterialOpeningId != 0)
                {
                    List<MaterialOpeningSizeQtyRate> listofmaterialOpening = new List<MaterialOpeningSizeQtyRate>();
                    listofmaterialOpening = materialstockManager.GetSizeQuantityRangeById(model.MaterialOpeningId);
                    if (listofmaterialOpening != null && listofmaterialOpening.Count > 0)
                    {
                        materialstockManager.DeleteMaterialOpeningSizeQtyRate(model.MaterialOpeningId);
                    }
                }
                if (model.SizeRange != null && model.SizeRange != "")
                {
                    string[] sizeAray = model.SizeRange.Split(',');
                    string[] QtyAray = model.Quantity.Split(',');
                    string[] RateAray = model.Rates.Split(',');
                    int count = 0;
                    foreach (var item in sizeAray)
                    {
                        MaterialOpeningSizeQtyRate SizeQtyRateDetails = new MaterialOpeningSizeQtyRate();
                        SizeQtyRateDetails.Size = item;
                        SizeQtyRateDetails.Quantity = QtyAray[count];
                        SizeQtyRateDetails.Rate = Convert.ToDecimal(RateAray[count]);
                        SizeQtyRateDetails.MaterialOpeningId = MaterialOpeningId;
                        SizeQtyRateDetails.CreatedDate = DateTime.Now;
                        materialstockManager.PostMaterialOpeningSizeQtyRate(SizeQtyRateDetails);
                        count++;
                    }
                }
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(MaterialOpeningModel model)
        {
            MaterialOpeningMaster materialOpening = new MaterialOpeningMaster();
            int MaterialOpeningId = 0;

            if (ModelState.IsValid)
            {
                MaterialOpeningMaster materialStock = new MaterialOpeningMaster();
                MaterialOpeningStockManager materialGroupManager = new MaterialOpeningStockManager();
                materialStock = materialGroupManager.GetmaterialOpeningId(model.MaterialOpeningId);
                if (materialStock != null)
                {
                    materialOpening.MaterialOpeningId = model.MaterialOpeningId;
                    materialOpening.Store = model.Store;
                    materialOpening.MaterialType = model.MaterialType;
                    materialOpening.MaterialCategoryMasterId = model.MaterialCategoryMasterId;
                    materialOpening.MaterialGroupMasterId = model.MaterialGroupMasterId;
                    materialOpening.MaterialMasterId = model.MaterialMasterId;
                    materialOpening.MaterialCode = model.MaterialCode;
                    if (model.ColorMasterId_ == 0)
                    {
                        materialOpening.ColorMasterId = materialStock.ColorMasterId;
                    }
                    else
                    {
                        materialOpening.ColorMasterId = model.ColorMasterId_;
                    }
                    if (model.PrimaryUomMasterId_ == 0)
                    {
                        materialOpening.PrimaryUomMasterId = materialStock.PrimaryUomMasterId;
                    }
                    else
                    {
                        materialOpening.PrimaryUomMasterId = model.PrimaryUomMasterId_;
                    }
                    if (model.SecondaryUomMasterId_ == 0)
                    {
                        materialOpening.SecondaryUomMasterId = model.SecondaryUomMasterId;
                    }
                    else
                    {
                        materialOpening.SecondaryUomMasterId = model.SecondaryUomMasterId_;
                    }
                    var format = "dd/MM/yyyy";
                    DateTime MOdate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    materialOpening.Date = MOdate.Date;
                    materialOpening.SizeRangeMasterId = model.SizeRangeMasterId;
                    materialOpening.Qty = model.Qty;
                    materialOpening.Rate = model.Rate;
                    materialOpening.MaterialPcs = model.MaterialPcs;
                    materialOpening.QtyPcs = model.QtyPcs;
                    materialOpening.Remarks = model.Remarks;
                    materialOpening.CreatedDate = materialStock.CreatedDate;
                    materialOpening.UpdatedDate = DateTime.Now;
                    materialGroupManager.Put(materialOpening);

                    if (model.SizeQuantityRate != null)
                    {
                        var SizeQuantityRate = JsonConvert.DeserializeObject<List<SizeQuantityRateObject>>(model.SizeQuantityRate);

                        materialGroupManager.DeleteMaterialOpeningSizeQtyRate(model.MaterialOpeningId);

                        foreach (var item in SizeQuantityRate)
                        {
                            MaterialOpeningSizeQtyRate SizeQtyRateDetails = new MaterialOpeningSizeQtyRate();
                            SizeQtyRateDetails.Size = item.Size;
                            SizeQtyRateDetails.Quantity = item.quantity;
                            if (item.Rate != "")
                            {
                                SizeQtyRateDetails.Rate = Convert.ToDecimal(item.Rate);
                            }
                            SizeQtyRateDetails.MaterialOpeningId = model.MaterialOpeningId;
                            SizeQtyRateDetails.CreatedDate = DateTime.Now;
                            SizeQtyRateDetails.UpdatedDate = DateTime.Now;
                            materialGroupManager.PostMaterialOpeningSizeQtyRate(SizeQtyRateDetails);
                        }
                    }
                }
                else
                {
                    return Json(materialOpening, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(materialOpening, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int MaterialOpeningId)
        {
            MaterialOpeningMaster materialOpening = new MaterialOpeningMaster();
            string status = "";
            MaterialOpeningStockManager materialStockManager = new MaterialOpeningStockManager();
            materialOpening = materialStockManager.GetmaterialOpeningId(MaterialOpeningId);
            if (materialOpening != null)
            {
                status = "Success";
                materialStockManager.DeleteMaterialOpeningSizeQtyRate(MaterialOpeningId);
                materialStockManager.Delete(materialOpening.MaterialOpeningId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<MaterialOpeningMaster> materialStockMasterlist = new List<MaterialOpeningMaster>();
            MaterialOpeningStockManager materialStockManager = new MaterialOpeningStockManager();
            List<StoreMaster> storeMasterList = new List<StoreMaster>();
            StoreMasterManager storeManager = new StoreMasterManager();
            List<MaterialGroupMaster_> materialGroupList_ = new List<MaterialGroupMaster_>();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            List<MaterialNameMaster> materialNameMasterlist = new List<MaterialNameMaster>();
            MaterialManager materialmanager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            List<MaterialMaster> MaterialMasterMasterlist = new List<MaterialMaster>();
            var materialStockList = (from X in materialStockManager.Get()
                                    join Y in materialmanager.Get() on X.MaterialMasterId equals Y.MaterialMasterId
                                    join Z in materialNameManager.Get() on Y.MaterialName equals Z.MaterialMasterID
                                    join C in colorManager.Get() on Y.ColorMasterId equals C.ColorMasterId
                                    where (Z.MaterialDescription.ToLower().Trim().Contains(filter.ToLower().Trim())|| C.Color.ToLower().Trim().Contains(filter.ToLower().Trim()))
                                    select X).ToList();

            materialStockMasterlist = materialStockList.ToList();
            
            MaterialOpeningModel model = new MaterialOpeningModel();
            model.MaterialOpeningMasterList = materialStockMasterlist;
            return PartialView("~/Views/Stock/MaterialOpening/Partial/MaterialOpeningMasterGrid.cshtml", model);
        }
        #endregion

    }
}
