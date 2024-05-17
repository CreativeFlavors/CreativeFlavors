using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.StockModel;
using MMS.Repository.Managers.StockManager;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Common;
using MMS.Repository.Managers;
using System.Globalization;
using Newtonsoft.Json;
using MMS.Web.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter][CrossSiteJson]
    public class GRN_DetailNewController : Controller
    {
        public enum INRCalculation
        {
            Euro = 76,
            Dollar = 64
        }
        // GET: /GRN_Detail/

        public ActionResult GoodReceipeNote()
        {
            return View("~/Views/Stock/GRN/GoodReceipeNoteNew.cshtml");
        }
        public ActionResult GoodReceipeNoteGrid()
        {
            List<GRN_EntityModelNew> grn_EntityModel = new List<GRN_EntityModelNew>();
            GRN_Details_ModelNew model = new GRN_Details_ModelNew();
            GrnManagerNew grnManager = new GrnManagerNew();
            grn_EntityModel = grnManager.Get().ToList();
            var groupList = grn_EntityModel.GroupBy(x => x.GrnNo.ToString().Trim()).Select(g => g.First()).ToList();
            model.GRNDetailsModelList = groupList;
            return PartialView("~/Views/Stock/GRN/Partial/GoodReceipeNoteGridNew.cshtml", model);
        }
        public ActionResult GetPoDetails(int POID)
        {
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder = purchaseOrderManager.GetPoOrderId(POID);
            return Json(purchaseOrder, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GrnDetailSearch(string filter)
        {
            GRN_Details_ModelNew model = new GRN_Details_ModelNew();
            GRN_EntityModelNew EntObj = new GRN_EntityModelNew();
            GrnManagerNew Manager = new GrnManagerNew();
            //string Search = Request.QueryString["SearchFilter"];
            GRN_Details_ModelNew Model = new GRN_Details_ModelNew();
            Model.GRNDetailsModelList = Manager.Get();
            var groupList = Model.GRNDetailsModelList.GroupBy(x => x.GrnNo.ToString().Trim()).Select(g => g.First()).ToList();
            List<GRN_EntityModelNew> grn_EntityModel = new List<GRN_EntityModelNew>();
            if (filter != null)
            {
                Model.GRNDetailsModelList = groupList.Where(x => (x.GrnNo.ToString().Contains(filter))).ToList();
            }

            return PartialView("~/Views/Stock/GRN/Partial/GoodReceipeNoteGridNew.cshtml", Model);
        }


        public ActionResult FillPoBasedMaterialName(string PoNo)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            ColorManager colormanager = new ColorManager();
            MaterialManager materialManager = new MaterialManager();
            string Message = "";
            bool WithoutIndent = false;
            purchaseOrder = purchaseOrderManager.GetPoOrderId(Convert.ToInt32(PoNo));
            if (purchaseOrder != null)
            {
                GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
                GRNTypeManager grnTypeManager = new GRNTypeManager();
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(purchaseOrder.OrderType));
                if (purchaseOrder.OrderType != "0")
                {
                    if (purchaseOrder.OrderType != "" && purchaseOrder.OrderType != "0" && grnTypeMaster.IssueType != "Direct Po")
                    {
                        var items_ = (from x in purchaseOrderManager.Get()
                                          //  join z in indentMaterialManager.Get() on x.IndentMaterialID equals z.IndentMaterialID
                                      join w in materialManager.Get() on x.Material equals w.MaterialMasterId
                                      join y in materialNameManager.Get() on w.MaterialName equals y.MaterialMasterID
                                      join c in colormanager.Get() on w.ColorMasterId equals c.ColorMasterId
                                      //  where x.PoNo == purchaseOrder.PoNo
                                      select new
                                      {
                                          MaterialDescription = y.MaterialDescription + " " + c.Color,
                                          IndentMaterialID = x.PoOrderId,
                                          x.PoDate,
                                          x.PoNo
                                      }).Where(x => x.PoNo == purchaseOrder.PoNo);
                        var distinctList_ = items_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                        var distinctList_podate = items_.GroupBy(x => x.PoDate).Select(g => g.First()).ToList();

                        return Json(new { Material = distinctList_, Date = distinctList_podate, IsIndent = WithoutIndent, Message = Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        var items = (from x in purchaseOrderManager.Get()
                                     join z in materialManager.Get() on x.Material equals z.MaterialMasterId
                                     join y in materialNameManager.Get() on z.MaterialName equals y.MaterialMasterID
                                     join c in colormanager.Get() on z.ColorMasterId equals c.ColorMasterId
                                     where x.PoNo == purchaseOrder.PoNo
                                     select new
                                     {
                                         MaterialDescription = y.MaterialDescription + " " + c.Color,
                                         IndentMaterialID = x.PoOrderId,
                                         PoDate = x.PoDate,
                                     });

                        var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                        var distinctList_podate = items.GroupBy(x => x.PoDate).Select(g => g.First()).ToList();
                        if (distinctList != null && distinctList.Count > 0)
                        {
                            WithoutIndent = true;
                        }
                        return Json(new { Material = distinctList, Date = distinctList_podate, IsIndent = WithoutIndent, Message = Message }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                Message = "There is no data";
                return Json(Message, JsonRequestBehavior.AllowGet);
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillIndentMaterialIDBasedPopulateDetails(int IndentMaterialID, string grnno)
        {
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            IndentMaterials indentMaterial = new IndentMaterials();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrder IspurchaseOrder = new PurchaseOrder();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            GrnManagerNew grnManager = new GrnManagerNew();
            GRN_EntityModelNew grn = new GRN_EntityModelNew();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            OrderEntry internalOrderEntryForm = new OrderEntry();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            CurrencyManager currencyManager = new CurrencyManager();
            IspurchaseOrder = purchaseOrderManager.GetPoOrderId(IndentMaterialID);
            if (IspurchaseOrder != null && IspurchaseOrder.IndentMaterialID != null && IspurchaseOrder.IndentMaterialID != 0)
            {
                indentMaterial = indentMaterialManager.GetIndentMaterialId(IspurchaseOrder.IndentMaterialID.Value);
                if (indentMaterial == null || indentMaterial.IndentMaterialID == 0)
                {
                    string[] indenno = IspurchaseOrder.IndentNo.Split(',');
                    foreach (var item in indenno)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            indentMaterial = indentMaterialManager.GetIsPOUpdate(Convert.ToInt32(item), IspurchaseOrder.Material);
                        }
                    }
                }
            }
            else
            {
                indentMaterial = indentMaterialManager.GetIndentMaterialId(IndentMaterialID);
            }

            if (IspurchaseOrder != null && IspurchaseOrder.IndentMaterialID != null && IspurchaseOrder.IndentMaterialID != 0)
            {
                materialMaster = materialManager.GetMaterialMasterId(indentMaterial.MaterialMasterID);

                purchaseOrder = purchaseOrderManager.GetPoNOWithMaterial(indentMaterial.IndentMaterialID);
                if (purchaseOrder != null && purchaseOrder.PoOrderId != 0)
                {
                    grn = grnManager.CheckDuplicate_GRN(Convert.ToInt32(purchaseOrder.PoOrderId), materialMaster.MaterialMasterId);
                }

                storeMaster = storeMasterManager.GetStoreMasterId(materialMaster.StoreMasterId);
            }
            else
            {
                if (IspurchaseOrder != null && IspurchaseOrder.PoOrderId != 0)
                {
                    materialMaster = materialManager.GetMaterialMasterId(IspurchaseOrder.Material);
                    //   purchaseOrder = purchaseOrderManager.GetPoNOWithMaterial(indentMaterial.IndentMaterialID);
                    if (IspurchaseOrder != null && IspurchaseOrder.PoOrderId != 0)
                    {
                        grn = grnManager.CheckDuplicate_GRN(Convert.ToInt32(IspurchaseOrder.PoOrderId), materialMaster.MaterialMasterId);
                    }
                    storeMaster = storeMasterManager.GetStoreMasterId(materialMaster.StoreMasterId);
                }
                purchaseOrder = IspurchaseOrder;

            }
            decimal? poQty = 0;
            bool isExist = false;
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            GrnObjList = grnManager.GetPOQty(Convert.ToInt32(purchaseOrder.PoOrderId), materialMaster.MaterialMasterId);
            if (GrnObjList != null && GrnObjList.Count > 0)
            {
                poQty = Convert.ToDecimal(GrnObjList.LastOrDefault().PendingQty);
                isExist = true;
            }
            List<ApprovedPrice> approvedPriceListExcessPo = new List<ApprovedPrice>();
            approvedPriceListExcessPo = grnManager.getApprovedPrice(purchaseOrder.Supplier.ToString(), purchaseOrder.Material.ToString());
            approvedPriceList = approvedPricelistManager.GetMaterialList(materialMaster.MaterialMasterId);
            var resultCurrency = currencyManager.GetCurrentMasterId(purchaseOrder.Currency);
            var currentgrnExists = GrnObjList.Where(x => x.GrnNo.ToString() == grnno && x.Grn_MaterialID == purchaseOrder.Material).ToList();
            if (((approvedPriceList == null || approvedPriceList.Count == 0)))
            {
                string Message = "Please make a entry on approved price list";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            if (grn != null && grn.GrnID != 0 && poQty == 0 && grnno == grn.GrnNo.ToString())
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }
            if (grn != null && grn.GrnID != 0 && poQty > 0 && grnno == grn.GrnNo.ToString())
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }
            else if (grn != null && grn.GrnID != 0 && poQty == 0 && grnno != grn.GrnNo.ToString())
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }
            else if (grn != null && grn.GrnID != 0 && poQty > 0 && currentgrnExists != null && currentgrnExists.Count > 0)
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                OrderEntry internalOrderEntryFormLotno = new OrderEntry();

                List<PurchaseOrderSizeRangeQuantity> listPurchaseOrderSizeRangeQuantity = new List<PurchaseOrderSizeRangeQuantity>();
                if (purchaseOrder != null && indentMaterial != null)
                {
                    listPurchaseOrderSizeRangeQuantity = purchaseOrderManager.GetPurchaseOrderSizeRangeQuantityPoOrderId(purchaseOrder.PoOrderId);
                    internalOrderEntryForm = buyerOrderEntryManager.GetOrderEntryId(purchaseOrder.IONO != null ? purchaseOrder.IONO.Value : 0);
                    internalOrderEntryFormLotno = buyerOrderEntryManager.GetBuyerOderSlNo(indentMaterial.BuyerPoNo);
                    return Json(new { IndentMaterial = indentMaterial, PurchaseOrder = purchaseOrder, Material = materialMaster, orderDetails = internalOrderEntryFormLotno, store = storeMaster, SizeRange = listPurchaseOrderSizeRangeQuantity, resultCurrency = resultCurrency, approvedPriceListExcessPo = approvedPriceListExcessPo, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    listPurchaseOrderSizeRangeQuantity = purchaseOrderManager.GetPurchaseOrderSizeRangeQuantityPoOrderId(IspurchaseOrder.PoOrderId);
                    internalOrderEntryForm = buyerOrderEntryManager.GetOrderEntryId(IspurchaseOrder != null ? IspurchaseOrder.IONO != null ? IspurchaseOrder.IONO.Value : 0 : 0);
                    if (!string.IsNullOrEmpty(IspurchaseOrder.IndentNo) && !IspurchaseOrder.IndentNo.Contains(','))
                    {
                        indentMaterial = indentMaterialManager.GetIsPOUpdate(Convert.ToInt16(IspurchaseOrder.IndentNo), IspurchaseOrder.Material);
                        if (indentMaterial != null)
                        {
                            internalOrderEntryFormLotno = buyerOrderEntryManager.GetBuyerOderSlNo(indentMaterial.BuyerPoNo);
                        }
                    }

                    if (internalOrderEntryFormLotno != null)
                    {
                        internalOrderEntryForm.LotNo = internalOrderEntryFormLotno.LotNo;
                    }
                    return Json(new { IndentMaterial = indentMaterial, PurchaseOrder = IspurchaseOrder, Material = materialMaster, orderDetails = internalOrderEntryForm, store = storeMaster, SizeRange = listPurchaseOrderSizeRangeQuantity, resultCurrency = resultCurrency, approvedPriceListExcessPo = approvedPriceListExcessPo, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
                }

            }
        }


        public ActionResult FillWithOutIndentMaterialIDBasedPopulateDetails(int MaterialID, int PoOrderID, string AutoMaticPoNumber, string grnno)
       {
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            IndentMaterials indentMaterial = new IndentMaterials();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            GrnManagerNew grnManager = new GrnManagerNew();
            GRN_EntityModelNew grn = new GRN_EntityModelNew();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            OrderEntry internalOrderEntryForm = new OrderEntry();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();

            List<ApprovedPrice> approvedPriceListExcessPo = new List<ApprovedPrice>();
            purchaseOrder = purchaseOrderManager.GetPoOrderId(MaterialID);
            if (purchaseOrder != null)
            {
                //grn = grnManager.CheckDuplicate_GRN(Convert.ToInt32(purchaseOrder.PoOrderId), purchaseOrder.Material.Value);
                grn = grnManager.CheckDuplicate_GRN(Convert.ToInt32(purchaseOrder.PoOrderId), purchaseOrder.Material.Value);
                materialMaster = materialManager.GetMaterialMasterId(purchaseOrder.Material);
                // GrnObjList = grnManager.GetPOQty(PoOrderID, materialMaster.MaterialMasterId);
                GrnObjList = grnManager.GetGSTPOQty(Convert.ToInt32(purchaseOrder.PoOrderId), materialMaster.MaterialMasterId);
                approvedPriceListExcessPo = grnManager.getApprovedPrice(purchaseOrder.Supplier.ToString(), purchaseOrder.Material.ToString());
            }
            else
            {
                materialMaster = materialManager.GetMaterialMasterId(MaterialID);
                GrnObjList = grnManager.GetAutomaticPOQty(AutoMaticPoNumber, materialMaster.MaterialMasterId);

            }

            decimal? poQty = 0;
            bool isExist = false;
            if (GrnObjList != null && GrnObjList.Count > 0 && PoOrderID != 0)
            {
                poQty = Convert.ToDecimal(GrnObjList.FirstOrDefault().PendingQty);
                isExist = true;
            }
            approvedPriceList = approvedPricelistManager.GetMaterialList(materialMaster.MaterialMasterId);
            var currentgrnExists = GrnObjList.Where(x => x.GrnNo.ToString() == grnno && x.Grn_MaterialID == purchaseOrder.Material).ToList();
            if (approvedPriceList == null || approvedPriceList.Count == 0)
            {
                string Message = "Please make a entry on approved price list";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            if (grn != null && grn.GrnID != 0 && poQty == 0 && grnno == grn.GrnNo.ToString())
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }
            if (grn != null && grn.GrnID != 0 && poQty > 0 && grnno == grn.GrnNo.ToString())
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }
            else if (grn != null && grn.GrnID != 0 && poQty == 0 && grnno != grn.GrnNo.ToString())
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }

            else if (grn != null && grn.GrnID != 0 && poQty > 0 && currentgrnExists != null && currentgrnExists.Count > 0)
            {
                string Message = "Already Exist";
                return Json(new { Message = Message, poQty = poQty, isExist = isExist, grn = grn }, JsonRequestBehavior.AllowGet);
            }

            CurrencyManager currencyManager = new CurrencyManager();

            var resultCurrency = currencyManager.GetCurrentMasterId(purchaseOrder != null ? purchaseOrder.PoOrderId != 0 ? purchaseOrder.Currency.Value : 0 : 0);
            storeMaster = storeMasterManager.GetStoreMasterId(materialMaster.StoreMasterId);
            internalOrderEntryForm = buyerOrderEntryManager.GetBuyerOderSlNo(indentMaterial.BuyerPoNo);
            List<PurchaseOrderSizeRangeQuantity> listPurchaseOrderSizeRangeQuantity = new List<PurchaseOrderSizeRangeQuantity>();
            if (purchaseOrder != null)
            {

                listPurchaseOrderSizeRangeQuantity = purchaseOrderManager.GetPurchaseOrderSizeRangeQuantityPoOrderId(purchaseOrder.PoOrderId);
            }
            return Json(new { IndentMaterial = indentMaterial, PurchaseOrder = purchaseOrder, Material = materialMaster, store = storeMaster, SizeRange = listPurchaseOrderSizeRangeQuantity, orderDetails = internalOrderEntryForm, poQty = poQty, isExist = isExist, resultCurrency = resultCurrency, approvedPriceListExcessPo = approvedPriceListExcessPo }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillGRNMaterialIDetails(int MaterialID)
        {
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            IndentMaterials indentMaterial = new IndentMaterials();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            GrnManagerNew grnManager = new GrnManagerNew();
            GRN_EntityModelNew grn = new GRN_EntityModelNew();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            materialMaster = materialManager.GetMaterialMasterId(MaterialID);
            storeMaster = storeMasterManager.GetStoreMasterId(materialMaster.MaterialMasterId);
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            listSizeItemMaterial = materialManager.GetSizeItemMaterial(materialMaster.MaterialMasterId);
            return Json(new { Material = materialMaster, store = storeMaster, SizeRange = listSizeItemMaterial }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetLastGRNO()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateGRNNO();
            ID++;

            return Json(ID.ToString(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApprovedPriceDetails(int SupplierID, int MaterialID, string MaterialType)
        {
            ApprovedPriceList approvedPrieList = new ApprovedPriceList();
            ApprovedPriceListManager approvedPriceListManger = new ApprovedPriceListManager();
            approvedPrieList = approvedPriceListManger.GetApprovedPriceList(MaterialID, SupplierID);
            CurrencyManager currencyManager = new CurrencyManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMasterlist_ = new MaterialMaster();
            if (approvedPrieList != null)
            {
                var resultCurrency = currencyManager.GetCurrentMasterId(approvedPrieList.CurrencyID);

                return Json(new { approvedPrieList = approvedPrieList, resultCurrency = resultCurrency }, JsonRequestBehavior.AllowGet);
            }
            //else
            //{

            //   // var materialMasterlist_;

            //    if (MaterialType == "2")
            //    {
            //         materialMasterlist_ = materialManager.Get().Where(x => x.isImportCustomer == true).SingleOrDefault();
            //    }
            //    else if (MaterialType == "1")
            //    {
            //         materialMasterlist_ = materialManager.Get().Where(x => x.isLocal == true, x.MaterialMasterId == MaterialID).SingleOrDefault();
            //    }
            //    else if (MaterialType == "3")
            //    {
            //         materialMasterlist_ = materialManager.Get().Where(x => x.isImport == true).SingleOrDefault();
            //    }
            //    else 
            //        {
            //      string  a = "GRN MAterial type not matached";
            //        return Json(new { data = a, materialMasterlist_ = materialMasterlist_ }, JsonRequestBehavior.AllowGet);
            //    }
            //    return Json(new { materialMasterlist_ =  materialMasterlist_ }, JsonRequestBehavior.AllowGet);
            //}
            return Json(new { approvedPrieList = approvedPrieList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGrnID(int GrnID)
        {
            GRN_Details_ModelNew model = new GRN_Details_ModelNew();
            GRN_EntityModelNew EntObj = new GRN_EntityModelNew();
            GrnManagerNew Manager = new GrnManagerNew();
            EntObj = Manager.GetGRNSelectedRow(GrnID);
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
            MaterialNameManager materialnameManager = new MaterialNameManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            List<SupplierMaster> supplierMasterList = new List<SupplierMaster>();
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            supplierMasterList = supplierMasterManager.Get();
            GRNTypeManager GRNTypeManager = new GRNTypeManager();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
            SubstanceMaster substanceMaster = new SubstanceMaster();
            List<GRNSizeQuantityObjectNew> listGRNSizeQuantityObject = new List<GRNSizeQuantityObjectNew>();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            if (EntObj != null && EntObj.GrnType != 0)
            {
                grnTypeMaster = GRNTypeManager.GetIssueTypeMasterId(EntObj.GrnType);
            }
            if (grnTypeMaster.IssueType != null && grnTypeMaster.IssueType.ToLower() != "Direct GRN".ToLower())
            {
                purchaseOrder = purchaseOrderManager.GetPoOrderId(EntObj.MaterialId.Value);
            }

            substanceMaster = substanceMasterManager.GetsubstanceMasterId(Convert.ToInt32(EntObj.Substance));
            if (substanceMaster != null && substanceMaster.SubstanceMasterId != 0)
            {
                EntObj.Substance = substanceMaster.SubstanceRange;
            }

            listGRNSizeQuantityObject = Manager.GetGRNSizeQuantity(EntObj.GrnID);
            MaterialMaster materialMaster = new MaterialMaster();
            materialMaster = materialManager.GetMaterialMasterId(EntObj.Grn_MaterialID);
            if (materialMaster != null && materialMaster.MaterialMasterId != null && materialMaster.MaterialMasterId != 0)
            {
                materialNameMaster = materialnameManager.GetMaterialNameMasterId(materialMaster.MaterialName);
            }
            storeMaster = storeMasterManager.GetStoreMasterId(EntObj.Stores);
            ColorManager colorManager = new ColorManager();
            if (purchaseOrder != null && grnTypeMaster.IssueType != null && grnTypeMaster.IssueType.ToLower() != "Direct GRN".ToLower())
            {
                if (purchaseOrder.IndentNo != null)
                {

                    var items_ = (from x in purchaseOrderManager.Get()
                                  join z in indentMaterialManager.Get() on x.IndentNo equals z.IndentID.ToString()
                                  join w in materialManager.Get() on x.Material equals w.MaterialMasterId
                                  join y in materialNameManager.Get() on w.MaterialName equals y.MaterialMasterID
                                  join c in colorManager.Get() on w.ColorMasterId equals c.ColorMasterId
                                  where x.PoNo == purchaseOrder.PoNo
                                  select new
                                  {
                                      MaterialDescription = y.MaterialDescription + " " + c.Color,
                                      IndentMaterialID = x.PoOrderId
                                  });

                    var distinctList_ = items_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                    return Json(new { GrnDetailS = EntObj, store = storeMaster, grnType = grnTypeMaster, Material = materialMaster, ListOfSizeRange = listGRNSizeQuantityObject, purchaseOrder = purchaseOrder, distinctList_ = distinctList_, supplierMasterList = supplierMasterList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var items_ = (from x in purchaseOrderManager.Get()
                                  join w in materialManager.Get() on x.Material equals w.MaterialMasterId
                                  join y in materialNameManager.Get() on w.MaterialName equals y.MaterialMasterID
                                  join c in colorManager.Get() on w.ColorMasterId equals c.ColorMasterId
                                  where x.PoNo == purchaseOrder.PoNo
                                  select new
                                  {
                                      MaterialDescription = y.MaterialDescription + " " + c.Color,
                                      IndentMaterialID = x.PoOrderId
                                  });

                    var distinctList_ = items_.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
                    return Json(new { GrnDetailS = EntObj, store = storeMaster, grnType = grnTypeMaster, Material = materialMaster, ListOfSizeRange = listGRNSizeQuantityObject, purchaseOrder = purchaseOrder, distinctList_ = distinctList_, supplierMasterList = supplierMasterList }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {

                return Json(new { GrnDetailS = EntObj, store = storeMaster, grnType = grnTypeMaster, Material = materialMaster, ListOfSizeRange = listGRNSizeQuantityObject, purchaseOrder = purchaseOrder, supplierMasterList = supplierMasterList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDetails(int GrnID)
        {
            GRN_Details_ModelNew model = new GRN_Details_ModelNew();
            GRN_EntityModelNew EntObj = new GRN_EntityModelNew();
            IndentMaterials indentMaterial = new IndentMaterials();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialmater = new MaterialMaster();
            MaterialMaster isExistMaterialmater = new MaterialMaster();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            GRNTypeManager GRNTypeManager = new GRNTypeManager();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            GrnManagerNew Manager = new GrnManagerNew();

            AutoGenGrnNo autoGrn = new AutoGenGrnNo();
            AutoGenGrnNo autoGrnNo = new AutoGenGrnNo();
            AutoGenGrnManager autoManager = new AutoGenGrnManager();

            if (GrnID == 0)
            {
                int Id;
                autoGrn = autoManager.Get().LastOrDefault();

                if (autoGrn != null)
                {
                    autoGrn = autoManager.Get().LastOrDefault();
                    var autoId = Convert.ToInt32(autoGrn.GrnId);
                    autoId++;

                    autoGrnNo.GrnId = autoId.ToString();
                    autoManager.Post(autoGrnNo);
                }
                else
                {
                    autoGrnNo.GrnId = "1";
                    autoManager.Post(autoGrnNo);
                }
            }
            autoGrn = autoManager.Get().LastOrDefault();
            int ID = Convert.ToInt32(autoGrn.GrnId);

            if (GrnID != 0)
            {
                EntObj = Manager.GetGRNSelectedRow(GrnID);
                List<GRN_Details_ModelNew> listGRN_Details_Model = new List<GRN_Details_ModelNew>();
                var listGrn = Manager.GetGRNNO(EntObj.GrnNo);
                model.TotalAmountTax = listGrn.Sum(x => x.AmountplusTax);
                foreach (var item in listGrn)
                {
                    GRN_Details_ModelNew modellist = new GRN_Details_ModelNew();
                    MaterialNameManager materialNameManager = new MaterialNameManager();
                    tbl_materialnamemaster materialNamemaster = new tbl_materialnamemaster();
                    MaterialMaster materialMaster_ = new MaterialMaster();
                    GrnTypeMaster grnTypeMaster_ = new GrnTypeMaster();
                    StoreMaster storeMaster_ = new StoreMaster();
                    UOMManager uomManager = new UOMManager();
                    UomMaster uomMaster = new UomMaster();
                    MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                    materialgroupmaster materialGroupMaster = new materialgroupmaster();
                    MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                    MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                    SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
                    SupplierMaster supplierMaster = new SupplierMaster();
                    SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                    SubstanceMaster substanceMaster = new SubstanceMaster();

                    List<GRNSizeQuantityObjectNew> GRNSizeQuantityObjectList = new List<Core.Entities.Stock.GRNSizeQuantityObjectNew>();
                    GRNSizeQuantityObjectList = Manager.GetGRNSizeQuantity(item.GrnID);
                    modellist.grnSizeRangeQuantityList = GRNSizeQuantityObjectList;
                    modellist.GrnID = item.GrnID;
                    modellist.BuyerSeasonID = item.BuyerSeasonID;
                    modellist.BuyerMasterId = item.BuyerMasterId;
                    modellist.GrnNo = item.GrnNo;
                    modellist.PoQty = item.PoQty;
                    modellist.AutomaticPONumber = item.AutomaticPONumber;
                    modellist.GateEntryNo = item.GateEntryNo;
                    modellist.AmountplusTax = item.AmountplusTax;
                    modellist.IONo = item.IONo;
                    modellist.IndentMaterialId = item.IndentMaterialId;
                    modellist.BarCodeNo = item.BarCodeNo;
                    modellist.FreightAmount = item.FreightAmount;
                    modellist.GrnDate = item.GrnDate.ToString();
                    modellist.GEDate = item.GEDate;
                    modellist.IndentNo = item.IndentNo.ToString();
                    modellist.GETime = EntObj.GETime;
                    modellist.InsuranceAmount = item.InsuranceAmount;
                    modellist.GrnRefNo = item.GrnRefNo;
                    modellist.GrnRefNo = item.GrnRefNo;
                    modellist.CustomsDuty = item.CustomsDuty;
                    modellist.QtyAsPerDc = item.QtyAsPerDc;
                    modellist.QtyAsPerPCS = item.QtyAsPerPCS;
                    modellist.TotalCount = item.TotalCount;
                    modellist.MaterialType = item.MaterialType.ToString();
                    modellist.QtyAsPerSQFT = item.QtyAsPerSQFT;
                    modellist.QTYType = item.QTYType;
                    modellist.AcceptedPCS = item.AcceptedPCS;
                    modellist.AcceptedQty = item.AcceptedQty;
                    modellist.AcceptedSQFT = item.AcceptedSQFT;
                    modellist.AcceptedType = item.AcceptedType;
                    modellist.ReceivedPCS = item.ReceivedPCS;
                    modellist.ReceivedQty = item.ReceivedQty;
                    modellist.ReceivedSQFT = item.ReceivedSQFT;
                    modellist.ReceivedType = item.ReceivedType;
                    modellist.RejectedPCS = item.RejectedPCS;
                    modellist.RejectedQty = item.RejectedQty;
                    modellist.RejectedSQFT = item.RejectedSQFT;
                    modellist.RejectedType = item.RejectedType;
                    modellist.SupplierNameId = item.SupplierNameId;
                    modellist.GrnType = item.GrnType;
                    modellist.BENo = item.BENo;
                    modellist.Pack_Forward = item.Pack_Forward;
                    modellist.DC_No = item.DC_No;
                    modellist.ST_VATcredit = item.ST_VATcredit;
                    modellist.ServiceTax = item.ServiceTax;
                    modellist.DCDate = item.DCDate.ToString();
                    modellist.ImportClearance = item.ImportClearance;
                    modellist.OtherCharges = item.OtherCharges;
                    modellist.INVoiceNo = item.INVoiceNo;
                    modellist.ExchangeRate = item.ExchangeRate;
                    modellist.PoQty = item.PoQty;
                    modellist.ShortagePCS = item.ShortagePCS == null ? 0 : item.ShortagePCS;
                    modellist.Transporter = item.Transporter;
                    modellist.INVoiceDate = item.INVoiceDate != null ? item.INVoiceDate.ToString() : "";
                    modellist.ShipmentMode = item.ShipmentMode;
                    modellist.GeneralRemarks = item.GeneralRemarks;
                    modellist.PoNO = item.PoNO;
                    modellist.LOTNo = item.LOTNo;
                    modellist.IndentNo = item.IndentNo.ToString();
                    modellist.Stores = item.Stores;
                    modellist.ShortageQty = item.ShortageQty;
                    modellist.ShortageSQFT = item.ShortageSQFT;
                    modellist.ShortagePCS = item.ShortagePCS;
                    modellist.ShortageType = item.ShortageType;
                    modellist.Grade = item.Grade;
                    modellist.GroupID = item.GroupID;
                    modellist.CategoryId = item.CategoryId;
                    modellist.IfGroupIsMaintenance = item.IfGroupIsMaintenance;
                    modellist.ColourId = item.ColourId;
                    modellist.Substance = item.Substance;
                    modellist.QtyAsPerDc = item.QtyAsPerDc;
                    modellist.QtyExcess = item.QtyExcess;
                    modellist.UOMId = item.UOMId;
                    modellist.Weight = item.Weight;
                    modellist.ReceivedQty = item.ReceivedQty;
                    modellist.DOM = item.DOM;
                    modellist.AcceptedQty = item.AcceptedQty;
                    modellist.PendingQty = item.PendingQty;
                    modellist.StoreLocation = item.StoreLocation;
                    modellist.Disp_SelectedMatOfPO = item.Disp_SelectedMatOfPO;
                    modellist.Disp_AllPOBasedOnSelecMat = item.Disp_AllPOBasedOnSelecMat;
                    modellist.GeneralRemarks = item.GeneralRemarks;
                    modellist.Disp_AllMatOfPO = item.Disp_AllMatOfPO;
                    modellist.Rate = item.Rate;
                    modellist.Value = item.Value;
                    modellist.MRPMargin = item.MRPMargin;
                    modellist.ServiceTaxPer = item.ServiceTaxPer;
                    modellist.MRPPrice = item.MRPPrice;
                    modellist.AccessibleValue = item.AccessibleValue;
                    modellist.Discount_Per = item.Discount_Per;
                    modellist.Discount_Val = item.Discount_Val;
                    modellist.Ecess_Per = item.Ecess_Per;
                    modellist.Ecess_Val = item.Ecess_Val;
                    modellist.MRPMargin = item.MRPMargin;
                    modellist.ExciseDuty_Per = item.ExciseDuty_Per;
                    modellist.ExciseDuty_Val = item.ExciseDuty_Val;
                    modellist.SH_Ecess_Per = item.SH_Ecess_Per;
                    modellist.SH_Ecess_Val = item.SH_Ecess_Val;
                    modellist.ST_VAT_CST_Per = item.ST_VAT_CST_Per;
                    modellist.ST_VAT_CST_Val = item.ST_VAT_CST_Val;
                    modellist.Surcharge_Per = item.Surcharge_Per;
                    modellist.Surcharge_Val = item.Surcharge_Val;
                    modellist.GateRefNo = item.GateRefNo;
                    modellist.ExcessApproval = item.ExcessApproval;
                    modellist.AutomaticPONumber = item.AutomaticPONumber;
                    modellist.TagsPiecesDiscount_Per = item.TagsPiecesDiscount_Per;
                    modellist.TagsPiecesDiscount_Val = item.TagsPiecesDiscount_Val;
                    modellist.PoDate = item.PoDate;
                    modellist.BEDate = item.BEDate;
                    modellist.ModeofTransport = item.ModeofTransport;
                    modellist.MachineryName = item.MachineryName;
                    modellist.PoReceivedQty = item.PoReceivedQty;
                    modellist.GST = item.GST;
                    modellist.SGST = item.SGST;
                    modellist.CGST = item.CGST;
                    modellist.IGST = item.IGST;
                    modellist.TCS = item.TCS;
                    modellist.TCS = item.TCSValue;
                    modellist.ImportIGST = item.ImportIGST;
                    modellist.Surcharges = item.Surcharges;
                    modellist.GSTValue = item.GSTValue;
                    modellist.SGSTValue = item.SGSTValue;
                    modellist.CGSTValue = item.CGSTValue;
                    modellist.IGSTValue = item.IGSTValue;
                    modellist.ImportIGSTValue = item.ImportIGSTValue;
                    modellist.SurchargesValue = item.SurchargesValue;
                    modellist.FreightAmtPercent = item.FreightAmtPercent;
                    modellist.InsuranceAmtPercent = item.InsuranceAmtPercent;
                    modellist.CustomDutyPercent = item.CustomDutyPercent;
                    modellist.ClearingChargePercent = item.ClearingChargePercent;
                    modellist.PackforwardPercent = item.PackforwardPercent;
                    modellist.FreightTaxAmt = item.FreightTaxAmt;
                    modellist.InsuranceTaxAmt = item.InsuranceTaxAmt;
                    modellist.CustomDutyTax = item.CustomDutyTax;
                    modellist.ClearingChargeTax = item.ClearingChargeTax;
                    modellist.PackforwardTax = item.PackforwardTax;
                    modellist.ServiceChargeTax = item.ServiceChargeTax;
                    modellist.ValueInRps = item.ValueInRps;
                    modellist.Currencykey = item.Currencykey;
                    modellist.NetTotal = item.NetTotal;
                    modellist.TotalAmount = item.TotalAmount;
                    modellist.TotalCost = item.TotalCost;
                    modellist.TagsPiecesDiscount = item.TagsPiecesDiscount;
                    modellist.IndentMaterialId_ = item.IndentMaterialId;
                    modellist.IndentMaterialId = item.IndentMaterialId;
                    substanceMaster = substanceMasterManager.GetsubstanceMasterId(Convert.ToInt32(item.Substance));
                    if (substanceMaster != null && substanceMaster.SubstanceMasterId != 0)
                    {
                        modellist.SubstanceName = substanceMaster.SubstanceRange;
                    }
                    else
                    {
                        modellist.GroupName = "";
                    }
                    materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(item.GroupID);
                    if (materialGroupMaster != null && materialGroupMaster.MaterialGroupMasterId != 0)
                    {
                        modellist.GroupName = materialGroupMaster.GroupName;
                    }
                    else
                    {
                        modellist.GroupName = "";
                    }
                    materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(item.CategoryId);
                    if (materialCategoryMaster != null && materialCategoryMaster.MaterialCategoryMasterId != 0)
                    {
                        modellist.CategoryName = materialCategoryMaster.CategoryName;
                    }
                    else
                    {
                        modellist.GroupName = "";
                    }
                    supplierMaster = supplierMasterManager.GetSupplierMasterId(item.SupplierNameId);
                    if (supplierMaster != null && supplierMaster.SupplierMasterId != 0)
                    {
                        modellist.SupplierName = supplierMaster.SupplierName;
                    }
                    else
                    {
                        modellist.SupplierName = "";
                    }
                    if (item != null && item.GrnType != 0)
                    {
                        grnTypeMaster = GRNTypeManager.GetIssueTypeMasterId(item.GrnType);
                        model.grnTypeMaster = grnTypeMaster;
                    }
                    storeMaster = storeMasterManager.GetStoreMasterId(materialmater.StoreMasterId);
                    modellist.storeMaster = storeMaster;
                    modellist.materialMaster = materialManager.GetMaterialMasterId(item.Grn_MaterialID);
                    materialMaster_ = materialManager.GetMaterialMasterId(item.Grn_MaterialID);
                    ColorMaster colorMaster = new ColorMaster();
                    ColorManager colorManager = new ColorManager();

                    if (materialMaster_ != null && materialMaster_.MaterialMasterId != 0)
                    {
                        colorMaster = colorManager.GetColorMasterID(item.ColourId);
                        materialNamemaster = materialNameManager.GetMaterialNameMasterId(materialMaster_.MaterialName);
                        modellist.MaterialDescription = materialNamemaster.MaterialDescription + " " + colorMaster.Color;
                    }
                    else
                    {
                        modellist.MaterialDescription = "";
                    }
                    storeMaster_ = storeMasterManager.GetStoreMasterId(item.Stores);
                    if (storeMaster_ != null && storeMaster_.StoreMasterId != 0)
                    {
                        modellist.StoresName = storeMaster_.StoreName;
                    }
                    else
                    {
                        modellist.StoresName = "";
                    }
                    grnTypeMaster_ = GRNTypeManager.GetIssueTypeMasterId(item.GrnType);
                    if (grnTypeMaster_ != null && grnTypeMaster_.GrnTypeMasterId != 0)
                    {
                        modellist.GrnTypeName = grnTypeMaster_.IssueType;
                    }
                    else
                    {
                        modellist.GrnTypeName = "";
                    }
                    uomMaster = uomManager.GetUomMasterId(item.QtyAsPerSQFT);
                    if (uomMaster != null && uomMaster.UomMasterId != 0)
                    {
                        modellist.UomName = uomMaster.ShortUnitName;
                    }
                    listGRN_Details_Model.Add(modellist);

                }
                model.GrnID = listGRN_Details_Model.FirstOrDefault().GrnID;
                model.listOFGrnModel = listGRN_Details_Model;
            }
            else
            {
                int? GrnNo_ = Convert.ToInt32(autoGrn.GrnId);
                model.QtyAsPerPCS = 0;
                model.ReceivedPCS = 0;
                model.ShortagePCS = 0;
                model.RejectedPCS = 0;
                model.AcceptedPCS = 0;
                model.InsuranceAmount = 0;
                model.CustomsDuty = "0";
                model.Pack_Forward = "0";
                model.ServiceTax = 0;
                model.OtherCharges = 0;
                model.Transporter = "0";
                model.Discount_Per = "0";
                model.ExciseDuty_Per = "0";
                model.ST_VAT_CST_Per = "0";
                model.Ecess_Per = "0";
                model.TotalCount = 0;
                model.SH_Ecess_Per = "0";
                model.QtyExcess = "0";
                model.ExcessApproval = "0";
                model.TagsPiecesDiscount_Per = "0";
                model.Surcharge_Per = "0";
                model.AccessibleValue = "0";
                model.MRPMargin = "0";
                model.GrnRefNo = "0";
                model.BarCodeNo = "0";
                model.ST_VATcredit = "0";
                model.ImportClearance = "0";
                model.BENo = "0";
                model.ExchangeRate = 0;
                model.ShipmentMode = "0";
                model.FreightAmount = 0;
                model.MRPPrice = "0";
                model.Discount_Val = "0";
                model.ExciseDuty_Val = "0";
                model.ST_VAT_CST_Val = "0";
                model.Ecess_Val = "0";
                model.SH_Ecess_Val = "0";
                model.TagsPiecesDiscount_Val = "0";
                model.AmountplusTax = 0;
                model.Surcharge_Val = "0";
                model.QtyAsPerDc = 0;
                model.ReceivedQty = 0;
                model.ShortageQty = 0;
                model.GrnNo = Convert.ToInt32(ID);
                model.AcceptedQty = 0;
                model.PoQty = 0;
                model.PendingQty = "0";
                model.Weight = 0;
                model.ServiceTaxPer = 0;
            }
            return PartialView("~/Views/Stock/GRN/Partial/GoodReceipeDetailsNew.cshtml", model);
        }
        public ActionResult GoodReceipeDetailsNew_(int GrnNo)
        {
            //string GrnID = Request.QueryString["GrnID"];
            GRN_Details_ModelNew model = new GRN_Details_ModelNew();
            GRN_EntityModelNew EntObj = new GRN_EntityModelNew();
            IndentMaterials indentMaterial = new IndentMaterials();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialmater = new MaterialMaster();
            MaterialMaster isExistMaterialmater = new MaterialMaster();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            GRNTypeManager GRNTypeManager = new GRNTypeManager();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            GrnManagerNew Manager = new GrnManagerNew();

            AutoGenGrnNo autoGrn = new AutoGenGrnNo();
            AutoGenGrnNo autoGrnNo = new AutoGenGrnNo();
            AutoGenGrnManager autoManager = new AutoGenGrnManager();

            if (GrnNo == 0)
            {
                int Id;
                autoGrn = autoManager.Get().LastOrDefault();

                if (autoGrn != null)
                {
                    autoGrn = autoManager.Get().LastOrDefault();
                    var autoId = Convert.ToInt32(autoGrn.GrnId);
                    autoId++;

                    autoGrnNo.GrnId = autoId.ToString();
                    autoManager.Post(autoGrnNo);
                }
                else
                {
                    autoGrnNo.GrnId = "1";
                    autoManager.Post(autoGrnNo);
                }
            }
            autoGrn = autoManager.Get().LastOrDefault();
            int ID = Convert.ToInt32(autoGrn.GrnId);

            if (GrnNo != 0)
            {
                //  EntObj = Manager.GetGRNSelectedRow(GrnID);
                List<GRN_Details_ModelNew> listGRN_Details_Model = new List<GRN_Details_ModelNew>();
                var listGrn = Manager.GetGRNNO(GrnNo);
                model.TotalAmountTax = listGrn.Sum(x => x.AmountplusTax);
                foreach (var item in listGrn)
                {
                    GRN_Details_ModelNew modellist = new GRN_Details_ModelNew();
                    MaterialNameManager materialNameManager = new MaterialNameManager();
                    tbl_materialnamemaster materialNamemaster = new tbl_materialnamemaster();
                    MaterialMaster materialMaster_ = new MaterialMaster();
                    GrnTypeMaster grnTypeMaster_ = new GrnTypeMaster();
                    StoreMaster storeMaster_ = new StoreMaster();
                    UOMManager uomManager = new UOMManager();
                    UomMaster uomMaster = new UomMaster();
                    MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                    materialgroupmaster materialGroupMaster = new materialgroupmaster();
                    MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
                    MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
                    SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
                    SupplierMaster supplierMaster = new SupplierMaster();
                    SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                    SubstanceMaster substanceMaster = new SubstanceMaster();

                    List<GRNSizeQuantityObjectNew> GRNSizeQuantityObjectList = new List<Core.Entities.Stock.GRNSizeQuantityObjectNew>();
                    GRNSizeQuantityObjectList = Manager.GetGRNSizeQuantity(item.GrnID);
                    modellist.grnSizeRangeQuantityList = GRNSizeQuantityObjectList;
                    modellist.GrnID = item.GrnID;
                    modellist.BuyerSeasonID = item.BuyerSeasonID;
                    modellist.BuyerMasterId = item.BuyerMasterId;
                    modellist.GrnNo = item.GrnNo;
                    modellist.PoQty = item.PoQty;
                    modellist.AutomaticPONumber = item.AutomaticPONumber;
                    modellist.GateEntryNo = item.GateEntryNo;
                    modellist.AmountplusTax = item.AmountplusTax;
                    modellist.IONo = item.IONo;
                    modellist.IndentMaterialId = item.IndentMaterialId;
                    modellist.BarCodeNo = item.BarCodeNo;
                    modellist.FreightAmount = item.FreightAmount;
                    modellist.GrnDate = item.GrnDate.ToString();
                    modellist.GEDate = item.GEDate;
                    modellist.IndentNo = item.IndentNo.ToString();
                    modellist.GETime = EntObj.GETime;
                    modellist.InsuranceAmount = item.InsuranceAmount;
                    modellist.GrnRefNo = item.GrnRefNo;
                    modellist.GrnRefNo = item.GrnRefNo;
                    modellist.CustomsDuty = item.CustomsDuty;
                    modellist.QtyAsPerDc = item.QtyAsPerDc;
                    modellist.QtyAsPerPCS = item.QtyAsPerPCS;
                    modellist.TotalCount = item.TotalCount;
                    modellist.MaterialType = item.MaterialType.ToString();
                    modellist.QtyAsPerSQFT = item.QtyAsPerSQFT;
                    modellist.QTYType = item.QTYType;
                    modellist.AcceptedPCS = item.AcceptedPCS;
                    modellist.AcceptedQty = item.AcceptedQty;
                    modellist.AcceptedSQFT = item.AcceptedSQFT;
                    modellist.AcceptedType = item.AcceptedType;
                    modellist.ReceivedPCS = item.ReceivedPCS;
                    modellist.ReceivedQty = item.ReceivedQty;
                    modellist.ReceivedSQFT = item.ReceivedSQFT;
                    modellist.ReceivedType = item.ReceivedType;
                    modellist.RejectedPCS = item.RejectedPCS;
                    modellist.RejectedQty = item.RejectedQty;
                    modellist.RejectedSQFT = item.RejectedSQFT;
                    modellist.RejectedType = item.RejectedType;
                    modellist.SupplierNameId = item.SupplierNameId;
                    modellist.GrnType = item.GrnType;
                    modellist.BENo = item.BENo;
                    modellist.Pack_Forward = item.Pack_Forward;
                    modellist.DC_No = item.DC_No;
                    modellist.ST_VATcredit = item.ST_VATcredit;
                    modellist.ServiceTax = item.ServiceTax;
                    modellist.DCDate = item.DCDate.ToString();
                    modellist.ImportClearance = item.ImportClearance;
                    modellist.OtherCharges = item.OtherCharges;
                    modellist.INVoiceNo = item.INVoiceNo;
                    modellist.ExchangeRate = item.ExchangeRate;
                    modellist.PoQty = item.PoQty;
                    modellist.ShortagePCS = item.ShortagePCS == null ? 0 : item.ShortagePCS;
                    modellist.Transporter = item.Transporter;
                    modellist.INVoiceDate = item.INVoiceDate != null ? item.INVoiceDate.ToString() : "";
                    modellist.ShipmentMode = item.ShipmentMode;
                    modellist.GeneralRemarks = item.GeneralRemarks;
                    modellist.PoNO = item.PoNO;
                    modellist.LOTNo = item.LOTNo;
                    modellist.IndentNo = item.IndentNo.ToString();
                    modellist.Stores = item.Stores;
                    modellist.ShortageQty = item.ShortageQty;
                    modellist.ShortageSQFT = item.ShortageSQFT;
                    modellist.ShortagePCS = item.ShortagePCS;
                    modellist.ShortageType = item.ShortageType;
                    modellist.Grade = item.Grade;
                    modellist.GroupID = item.GroupID;
                    modellist.CategoryId = item.CategoryId;
                    modellist.IfGroupIsMaintenance = item.IfGroupIsMaintenance;
                    modellist.ColourId = item.ColourId;
                    modellist.Substance = item.Substance;
                    modellist.QtyAsPerDc = item.QtyAsPerDc;
                    modellist.QtyExcess = item.QtyExcess;
                    modellist.UOMId = item.UOMId;
                    modellist.Weight = item.Weight;
                    modellist.ReceivedQty = item.ReceivedQty;
                    modellist.DOM = item.DOM;
                    modellist.AcceptedQty = item.AcceptedQty;
                    modellist.PendingQty = item.PendingQty;
                    modellist.StoreLocation = item.StoreLocation;
                    modellist.Disp_SelectedMatOfPO = item.Disp_SelectedMatOfPO;
                    modellist.Disp_AllPOBasedOnSelecMat = item.Disp_AllPOBasedOnSelecMat;
                    modellist.GeneralRemarks = item.GeneralRemarks;
                    modellist.Disp_AllMatOfPO = item.Disp_AllMatOfPO;
                    modellist.Rate = item.Rate;
                    modellist.Value = item.Value;
                    modellist.MRPMargin = item.MRPMargin;
                    modellist.ServiceTaxPer = item.ServiceTaxPer;
                    modellist.MRPPrice = item.MRPPrice;
                    modellist.AccessibleValue = item.AccessibleValue;
                    modellist.Discount_Per = item.Discount_Per;
                    modellist.Discount_Val = item.Discount_Val;
                    modellist.Ecess_Per = item.Ecess_Per;
                    modellist.Ecess_Val = item.Ecess_Val;
                    modellist.MRPMargin = item.MRPMargin;
                    modellist.ExciseDuty_Per = item.ExciseDuty_Per;
                    modellist.ExciseDuty_Val = item.ExciseDuty_Val;
                    modellist.SH_Ecess_Per = item.SH_Ecess_Per;
                    modellist.SH_Ecess_Val = item.SH_Ecess_Val;
                    modellist.ST_VAT_CST_Per = item.ST_VAT_CST_Per;
                    modellist.ST_VAT_CST_Val = item.ST_VAT_CST_Val;
                    modellist.Surcharge_Per = item.Surcharge_Per;
                    modellist.Surcharge_Val = item.Surcharge_Val;
                    modellist.GateRefNo = item.GateRefNo;
                    modellist.ExcessApproval = item.ExcessApproval;
                    modellist.AutomaticPONumber = item.AutomaticPONumber;
                    modellist.TagsPiecesDiscount_Per = item.TagsPiecesDiscount_Per;
                    modellist.TagsPiecesDiscount_Val = item.TagsPiecesDiscount_Val;
                    modellist.PoDate = item.PoDate;
                    modellist.BEDate = item.BEDate;
                    modellist.ModeofTransport = item.ModeofTransport;
                    modellist.MachineryName = item.MachineryName;
                    modellist.PoReceivedQty = item.PoReceivedQty;
                    modellist.GST = item.GST;
                    modellist.SGST = item.SGST;
                    modellist.CGST = item.CGST;
                    modellist.IGST = item.IGST;
                    modellist.TCS = item.TCS;
                    modellist.TCSValue = item.TCSValue;
                    modellist.ImportIGST = item.ImportIGST;
                    modellist.Surcharges = item.Surcharges;
                    modellist.GSTValue = item.GSTValue;
                    modellist.SGSTValue = item.SGSTValue;
                    modellist.CGSTValue = item.CGSTValue;
                    modellist.IGSTValue = item.IGSTValue;
                    modellist.ImportIGSTValue = item.ImportIGSTValue;
                    modellist.SurchargesValue = item.SurchargesValue;
                    modellist.FreightAmtPercent = item.FreightAmtPercent;
                    modellist.InsuranceAmtPercent = item.InsuranceAmtPercent;
                    modellist.CustomDutyPercent = item.CustomDutyPercent;
                    modellist.ClearingChargePercent = item.ClearingChargePercent;
                    modellist.PackforwardPercent = item.PackforwardPercent;
                    modellist.FreightTaxAmt = item.FreightTaxAmt;
                    modellist.InsuranceTaxAmt = item.InsuranceTaxAmt;
                    modellist.CustomDutyTax = item.CustomDutyTax;
                    modellist.ClearingChargeTax = item.ClearingChargeTax;
                    modellist.PackforwardTax = item.PackforwardTax;
                    modellist.ServiceChargeTax = item.ServiceChargeTax;
                    modellist.ValueInRps = item.ValueInRps;
                    modellist.Currencykey = item.Currencykey;
                    modellist.NetTotal = item.NetTotal;
                    modellist.TotalAmount = item.TotalAmount;
                    modellist.TotalCost = item.TotalCost;
                    modellist.TagsPiecesDiscount = item.TagsPiecesDiscount;
                    modellist.IndentMaterialId_ = item.IndentMaterialId;
                    modellist.IndentMaterialId = item.IndentMaterialId;
                    substanceMaster = substanceMasterManager.GetsubstanceMasterId(Convert.ToInt32(item.Substance));
                    if (substanceMaster != null && substanceMaster.SubstanceMasterId != 0)
                    {
                        modellist.SubstanceName = substanceMaster.SubstanceRange;
                    }
                    else
                    {
                        modellist.GroupName = "";
                    }
                    materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(item.GroupID);
                    if (materialGroupMaster != null && materialGroupMaster.MaterialGroupMasterId != 0)
                    {
                        modellist.GroupName = materialGroupMaster.GroupName;
                    }
                    else
                    {
                        modellist.GroupName = "";
                    }
                    materialCategoryMaster = materialCategoryManager.GetMaterialCategoryMaster(item.CategoryId);
                    if (materialCategoryMaster != null && materialCategoryMaster.MaterialCategoryMasterId != 0)
                    {
                        modellist.CategoryName = materialCategoryMaster.CategoryName;
                    }
                    else
                    {
                        modellist.GroupName = "";
                    }
                    supplierMaster = supplierMasterManager.GetSupplierMasterId(item.SupplierNameId);
                    if (supplierMaster != null && supplierMaster.SupplierMasterId != 0)
                    {
                        modellist.SupplierName = supplierMaster.SupplierName;
                    }
                    else
                    {
                        modellist.SupplierName = "";
                    }
                    if (item != null && item.GrnType != 0)
                    {
                        grnTypeMaster = GRNTypeManager.GetIssueTypeMasterId(item.GrnType);
                        model.grnTypeMaster = grnTypeMaster;
                    }
                    storeMaster = storeMasterManager.GetStoreMasterId(materialmater.StoreMasterId);
                    modellist.storeMaster = storeMaster;
                    modellist.materialMaster = materialManager.GetMaterialMasterId(item.Grn_MaterialID);
                    materialMaster_ = materialManager.GetMaterialMasterId(item.Grn_MaterialID);
                    ColorMaster colorMaster = new ColorMaster();
                    ColorManager colorManager = new ColorManager();
                    if (materialMaster_ != null && materialMaster_.MaterialMasterId != 0)
                    {
                        colorMaster = colorManager.GetColorMasterID(item.ColourId);
                        materialNamemaster = materialNameManager.GetMaterialNameMasterId(materialMaster_.MaterialName);
                        modellist.MaterialDescription = materialNamemaster.MaterialDescription + " " + colorMaster.Color;
                    }
                    else
                    {
                        modellist.MaterialDescription = "";
                    }
                    storeMaster_ = storeMasterManager.GetStoreMasterId(item.Stores);
                    if (storeMaster_ != null && storeMaster_.StoreMasterId != 0)
                    {
                        modellist.StoresName = storeMaster_.StoreName;
                    }
                    else
                    {
                        modellist.StoresName = "";
                    }
                    grnTypeMaster_ = GRNTypeManager.GetIssueTypeMasterId(item.GrnType);
                    if (grnTypeMaster_ != null && grnTypeMaster_.GrnTypeMasterId != 0)
                    {
                        modellist.GrnTypeName = grnTypeMaster_.IssueType;
                    }
                    else
                    {
                        modellist.GrnTypeName = "";
                    }
                    uomMaster = uomManager.GetUomMasterId(item.QtyAsPerSQFT);
                    if (uomMaster != null && uomMaster.UomMasterId != 0)
                    {
                        modellist.UomName = uomMaster.ShortUnitName;
                    }
                    listGRN_Details_Model.Add(modellist);

                }
                model.GateEntryNo = listGRN_Details_Model.FirstOrDefault().GateEntryNo;
                model.GEDate = listGRN_Details_Model.FirstOrDefault().GEDate;
                model.GrnType = listGRN_Details_Model.FirstOrDefault().GrnType;
                model.GrnTypeName = grnTypeMaster.IssueType;
                model.Discount_Val = listGRN_Details_Model.FirstOrDefault().Discount_Val;
                model.GrnNo = listGRN_Details_Model.FirstOrDefault().GrnNo;
                model.GrnDate = listGRN_Details_Model.FirstOrDefault().GrnDate;
                model.AutomaticPONumber = listGRN_Details_Model.FirstOrDefault().AutomaticPONumber;
                model.PoDate = listGRN_Details_Model.FirstOrDefault().PoDate;
                model.BuyerMasterId = listGRN_Details_Model.FirstOrDefault().BuyerMasterId;
                model.BuyerSeasonID = listGRN_Details_Model.FirstOrDefault().BuyerSeasonID;
                model.DC_No = listGRN_Details_Model.FirstOrDefault().DC_No;
                model.DCDate = listGRN_Details_Model.FirstOrDefault().DCDate;
                model.INVoiceNo = listGRN_Details_Model.FirstOrDefault().INVoiceNo;
                model.INVoiceDate = listGRN_Details_Model.FirstOrDefault().INVoiceDate;
                model.BENo = listGRN_Details_Model.FirstOrDefault().BENo;
                model.BEDate = listGRN_Details_Model.FirstOrDefault().BEDate;
                model.ServiceTaxPer = listGRN_Details_Model.FirstOrDefault().ServiceTaxPer;
                model.ServiceChargeTax = listGRN_Details_Model.FirstOrDefault().ServiceChargeTax;
                model.ServiceTax = listGRN_Details_Model.FirstOrDefault().ServiceTax;
                model.listOFGrnModel = listGRN_Details_Model;
                model.FreightAmount = listGRN_Details_Model.FirstOrDefault().FreightAmount;
                model.InsuranceAmount = listGRN_Details_Model.FirstOrDefault().InsuranceAmount;
                model.CustomsDuty = listGRN_Details_Model.FirstOrDefault().CustomsDuty;
                model.OtherCharges = listGRN_Details_Model.FirstOrDefault().OtherCharges;
                model.Pack_Forward = listGRN_Details_Model.FirstOrDefault().Pack_Forward;
                model.ServiceTaxPer = listGRN_Details_Model.FirstOrDefault().ServiceTaxPer;
                model.Weight = listGRN_Details_Model.FirstOrDefault().Weight;
                model.ServiceChargeTax = listGRN_Details_Model.FirstOrDefault().ServiceChargeTax;
                model.MRPMargin = listGRN_Details_Model.FirstOrDefault().MRPMargin;




            }
            else
            {

                int? GrnNo_ = Convert.ToInt32(autoGrn.GrnId);
                model.QtyAsPerPCS = 0;
                model.ReceivedPCS = 0;
                model.ShortagePCS = 0;
                model.RejectedPCS = 0;
                model.AcceptedPCS = 0;
                model.InsuranceAmount = 0;
                model.CustomsDuty = "0";
                model.Pack_Forward = "0";
                model.ServiceTax = 0;
                model.OtherCharges = 0;
                model.Transporter = "0";
                model.Discount_Per = "0";
                model.ExciseDuty_Per = "0";
                model.ST_VAT_CST_Per = "0";
                model.Ecess_Per = "0";
                model.TotalCount = 0;
                model.SH_Ecess_Per = "0";
                model.QtyExcess = "0";
                model.ExcessApproval = "0";
                model.TagsPiecesDiscount_Per = "0";
                model.Surcharge_Per = "0";
                model.AccessibleValue = "0";
                model.MRPMargin = "0";
                model.GrnRefNo = "0";
                model.BarCodeNo = "0";
                model.ST_VATcredit = "0";
                model.ImportClearance = "0";
                model.BENo = "0";
                model.ExchangeRate = 0;
                model.ShipmentMode = "0";
                model.FreightAmount = 0;
                model.MRPPrice = "0";
                model.Discount_Val = "0";
                model.ExciseDuty_Val = "0";
                model.ST_VAT_CST_Val = "0";
                model.Ecess_Val = "0";
                model.SH_Ecess_Val = "0";
                model.TagsPiecesDiscount_Val = "0";
                model.AmountplusTax = 0;
                model.Surcharge_Val = "0";
                model.QtyAsPerDc = 0;
                model.ReceivedQty = 0;
                model.ShortageQty = 0;
                model.GrnNo = Convert.ToInt32(ID);
                model.AcceptedQty = 0;
                model.PoQty = 0;
                model.PendingQty = "0";
                model.Weight = 0;
                model.ServiceTaxPer = 0;

            }
            return PartialView("~/Views/Stock/GRN/Partial/GoodReceipeDetailsNew_.cshtml", model);
        }
        public ActionResult isExistGRNMaterialNameBasedonGRNNO(int MaterialNameID=0, int GrnNO=0)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            GrnManagerNew grnManager = new GrnManagerNew();
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            GrnObjList = grnManager.isExistGRNNOWithMaterial(MaterialNameID, GrnNO);
            if (GrnObjList == null || GrnObjList.Count == 0)
            {
                /// List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
                List<SupplierSuppliedMaterialPrice> supplierMaterialPrice = new List<SupplierSuppliedMaterialPrice>();
                ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
                supplierMaterialPrice = approvedPriceListManager.SupplierSuppliedMaterialPrice(MaterialNameID, 0);
                approvedPriceList = approvedPricelistManager.GetMaterialList(MaterialNameID);
                if (approvedPriceList == null || approvedPriceList.Count == 0)
                {
                    string Message = "Please make a entry on approved price list";
                    return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var items = (from x in materialManager.Get()
                                 join y in materialNameManager.Get()
                                  on x.MaterialName equals y.MaterialMasterID
                                 join z in colorManager.Get()
                                 on x.ColorMasterId equals z.ColorMasterId into yG
                                 from y1 in yG.DefaultIfEmpty()
                                 where x.MaterialMasterId == MaterialNameID
                                 select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isLocal, x.isImport, x.isImportCustomer });

                    var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();


                    listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
                    listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

                    storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
                    CurrencyManager currencyManager = new CurrencyManager();
                    var resultCurrency = currencyManager.GetCurrentMasterId(approvedPriceList.FirstOrDefault().CurrencyID);

                    var supplierItems = (from x in approvedPricelistManager.Get()
                                         join y in supplierMasterManager.Get()
                                         on x.SupplierId equals y.SupplierMasterId
                                         where x.MaterialID == MaterialNameID
                                         select new { x.SupplierId, y.SupplierName });
                    supplierItems = supplierItems.GroupBy(x => x.SupplierName).Select(x => x.First());

                    return Json(new { Material = distinctList, SizeRange = listSizeItemMaterial, store = storeMaster, approvedPriceList = approvedPriceList, supplierItems = supplierItems, resultCurrency = resultCurrency }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string Message = "Already Existed";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult isExistGRNMaterialNameBasedonGRNNO_new(int MaterialNameID = 0, int GrnNO = 0)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();
            GrnManagerNew grnManager = new GrnManagerNew();
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            ApprovedPriceListManager approvedPricelistManager = new ApprovedPriceListManager();
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            GrnObjList = grnManager.isExistGRNNOWithMaterial(MaterialNameID, GrnNO);
            if (GrnObjList == null || GrnObjList.Count == 0)
            {
                /// List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
                List<SupplierSuppliedMaterialPrice> supplierMaterialPrice = new List<SupplierSuppliedMaterialPrice>();
                ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
                supplierMaterialPrice = approvedPriceListManager.SupplierSuppliedMaterialPrice(MaterialNameID, 0);
                approvedPriceList = approvedPricelistManager.GetMaterialList(MaterialNameID);
                if (approvedPriceList == null || approvedPriceList.Count == 0)
                {
                    string Message = "Please make a entry on approved price list";
                    return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var items = (from x in materialManager.Get()
                                 join y in materialNameManager.Get()
                                  on x.MaterialName equals y.MaterialMasterID
                                 join z in colorManager.Get()
                                 on x.ColorMasterId equals z.ColorMasterId into yG
                                 from y1 in yG.DefaultIfEmpty()
                                 where x.MaterialMasterId == MaterialNameID
                                 select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, y1 != null ? y1.Color != null ? y1.Color : string.Empty : string.Empty), x.MaterialMasterId, x.Price, ColorMasterId = (y1 != null ? y1.ColorMasterId != 0 ? y1.ColorMasterId : 0 : 0), x.Uom, x.UomUnit, x.SizeRangeMasterId, x.SubstanceMasterId, x.MaterialCategoryMasterId, x.MaterialGroupMasterId, x.GradeMasterId, x.StoreMasterId, x.PurchasePrimary, x.PrimaryUnit, x.PurchasePacket, x.PacketUnit, x.CurrencyMasterId, x.isLocal, x.isImport, x.isImportCustomer });

                    var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();


                    listSizeItemMaterial = materialManager.GetSizeItemMaterial(distinctList.FirstOrDefault().MaterialMasterId);
                    listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

                    storeMaster = storeMasterManager.GetStoreMasterId(distinctList.FirstOrDefault().StoreMasterId);
                    CurrencyManager currencyManager = new CurrencyManager();
                    var resultCurrency = currencyManager.GetCurrentMasterId(approvedPriceList.FirstOrDefault().CurrencyID);

                    var supplierItems = (from x in approvedPricelistManager.Get()
                                         join y in supplierMasterManager.Get()
                                         on x.SupplierId equals y.SupplierMasterId
                                         where x.MaterialID == MaterialNameID
                                         select new { x.SupplierId, y.SupplierName });
                    supplierItems = supplierItems.GroupBy(x => x.SupplierName).Select(x => x.First());

                    return Json(new { Material = distinctList, SizeRange = listSizeItemMaterial, store = storeMaster, approvedPriceList = approvedPriceList, supplierItems = supplierItems, resultCurrency = resultCurrency }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                string Message = "Already Existed";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SaveDetails(GRN_Details_ModelNew Model, string PoDate, string BEDate)
        {
            LeatherShoeGradeManager leatherShoeGradeManager = new LeatherShoeGradeManager();
            MaterialGroupManager materialGropuManager = new MaterialGroupManager();
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            UOMManager UomManager = new UOMManager();
            SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MMS.Core.Entities.Stock.GRN_EntityModelNew GrnModel = new Core.Entities.Stock.GRN_EntityModelNew();
            string Message = "";
            MMS.Core.Entities.Stock.GRN_EntityModelNew GrnModel_ = new Core.Entities.Stock.GRN_EntityModelNew();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            IndentMaterials indentMaterial = new IndentMaterials();
            PurchaseOrderManager purchaserOrderManager = new PurchaseOrderManager();
            PurchaseOrder isPurchaseOrderDetails = new PurchaseOrder();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderManager purchaeOrderManager = new PurchaseOrderManager();
            GRNTypeManager grnTypeManager = new GRNTypeManager();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            GrnTypeMaster grnTypeMasters = new GrnTypeMaster();
            GrnManagerNew grnManager = new GrnManagerNew();

            
            grnTypeMasters = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(Model.GrnType));
            if (grnTypeMasters.IssueType == "General")
            {
                purchaseOrder = purchaeOrderManager.GetPoOrderId(Model.MaterialId.Value);
            }
            if (purchaseOrder != null && purchaseOrder.PoOrderId != 0)
            {
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(purchaseOrder.OrderType));
            }

            if (grnTypeMaster != null && grnTypeMaster.GrnTypeMasterId != 0 && grnTypeMaster.IssueType != "Direct Po")
            {
                PurchaseOrder purchaseOrders = new PurchaseOrder();
                purchaseOrders = purchaeOrderManager.GetPoOrderId(Model.MaterialId.Value);
                if (!string.IsNullOrEmpty(purchaseOrders.IndentNo))
                {
                    string[] indentno = purchaseOrders.IndentNo.Split(',');
                    foreach (var item in indentno)
                    {
                        indentMaterial = indentMaterialManager.GetIsPOUpdate(Convert.ToInt32(item), purchaseOrders.Material);
                    }
                }


                if (indentMaterial != null && indentMaterial.IndentMaterialID != 0)
                {
                    GrnModel.IndentMaterialId = indentMaterial.IndentMaterialID;
                    GrnModel.Grn_MaterialID = indentMaterial.MaterialMasterID.Value;
                    GrnModel.MaterialId = Model.MaterialId;
                }
                else
                {
                    GrnModel.PoNO = Model.PoNO;
                    purchaseOrder = new PurchaseOrder();
                    purchaseOrder = purchaeOrderManager.GetPoOrderId(Model.MaterialId.Value);
                    GrnModel.MaterialId = Model.MaterialId.Value;
                    GrnModel.Grn_MaterialID = purchaseOrder.Material.Value;
                }
            }
            else
            {
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(Model.GrnType));
                if (grnTypeMaster != null && grnTypeMaster.IssueType == "Direct GRN")
                {
                    GrnModel.MaterialId = Model.MaterialId;
                    GrnModel.Grn_MaterialID = Model.MaterialId;
                }
                else
                {
                    if (purchaseOrder != null && purchaseOrder.PoOrderId != 0)
                    {
                        GrnModel.MaterialId = purchaseOrder.PoOrderId;
                        GrnModel.Grn_MaterialID = purchaseOrder.Material.Value;
                    }
                    else
                    {
                        GrnModel.MaterialId = Model.PoNO;
                        purchaseOrder = new PurchaseOrder();
                        purchaseOrder = purchaeOrderManager.GetPoOrderId(Model.PoNO);
                        GrnModel.Grn_MaterialID = purchaseOrder.Material.Value;

                    }

                }
            }
            GrnManagerNew Manager = new GrnManagerNew();
            var MaterialDescription = "";
            var GradeDescription = leatherShoeGradeManager.Get().Where(x => x.LeatherShoesGradeMasterId == Convert.ToInt32(Model.Grade)).Select(x => x.Grade).FirstOrDefault();
            var GroupDescription = materialGropuManager.Get().Where(x => x.MaterialGroupMasterId == Model.GroupID).Select(x => x.GroupName).FirstOrDefault();
            var StoreDescription = storeMasterManager.Get().Where(x => x.StoreMasterId == Model.StoreLocation).Select(x => x.Location).FirstOrDefault();
            var CategoryDescription = materialCategoryManager.Get().Where(x => x.MaterialCategoryMasterId == Model.CategoryId).Select(x => x.CategoryName).FirstOrDefault();
            var ColorDescription = colorManager.Get().Where(x => x.ColorMasterId == Model.ColourId).Select(x => x.Color).FirstOrDefault();
            var UomDescription = UomManager.Get().Where(x => x.UomMasterId == Model.UOMId).Select(x => x.LongUnitName).FirstOrDefault();
            var QtyAsPerDescription = UomManager.Get().Where(x => x.UomMasterId == Model.QtyAsPerSQFT).Select(x => x.LongUnitName).FirstOrDefault();
            var ReceivedSQFTDescription = UomManager.Get().Where(x => x.UomMasterId == Model.ReceivedSQFT).Select(x => x.LongUnitName).FirstOrDefault();
            var RejectedSQFTDescription = UomManager.Get().Where(x => x.UomMasterId == Model.RejectedSQFT).Select(x => x.LongUnitName).FirstOrDefault();
            var AcceptedSQFTDescription = UomManager.Get().Where(x => x.UomMasterId == Model.AcceptedSQFT).Select(x => x.LongUnitName).FirstOrDefault();
            var QTYTypeDescription = UomManager.Get().Where(x => x.UomMasterId == Model.QTYType).Select(x => x.LongUnitName).FirstOrDefault();
            var ReceivedTypeDescription = UomManager.Get().Where(x => x.UomMasterId == Model.ReceivedType).Select(x => x.LongUnitName).FirstOrDefault();
            var RejectedTypeDescription = UomManager.Get().Where(x => x.UomMasterId == Model.RejectedType).Select(x => x.LongUnitName).FirstOrDefault();
            var AcceptedTypeDescription = UomManager.Get().Where(x => x.UomMasterId == Model.AcceptedType).Select(x => x.LongUnitName).FirstOrDefault();
            var storeNameDescription = storeMasterManager.Get().Where(x => x.StoreMasterId == Model.Stores).Select(x => x.StoreName).FirstOrDefault();
            var SubstanceRangeDescription = substanceMasterManager.Get().Where(x => x.SubstanceMasterId == Convert.ToInt32(Model.Substance)).Select(x => x.SubstanceRange).FirstOrDefault();

            bool Result = false;

            if (storeNameDescription == "Wetblue Stores")
            {
                int lot = grnManager.Get().Where(x => x.Stores == Model.Stores ).Count();
                int lot_count = lot + 1;
                GrnModel.LOTNo = lot_count.ToString();
            }
            else
            {
                GrnModel.LOTNo = Model.LOTNo;
            }
            MaterialMaster materialMaster = new MaterialMaster();
            MaterialManager materialManage_ = new MaterialManager();
            tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
            materialMaster = materialManage_.GetMaterialMasterId(GrnModel.Grn_MaterialID);
            if (materialMaster != null && materialMaster.MaterialMasterId != 0)
            {
                materialNameMaster = materialNameManager.GetMaterialNameMasterId(materialMaster.MaterialName);
                MaterialDescription = materialNameManager.Get().Where(x => x.MaterialMasterID == materialMaster.MaterialName).Select(x => x.MaterialDescription).FirstOrDefault();
            }
            GrnModel.GrnID = Model.GrnID;
            GrnModel.GateEntryNo = Model.GateEntryNo;
            GrnModel.FreightAmount = Model.FreightAmount;
            GrnModel.BarCodeNo = Model.BarCodeNo;
            GrnModel.MaterialType = Convert.ToInt32(Model.MaterialType);
            GrnModel.IONo = Model.IONo;
            GrnModel.TotalCount = Model.TotalCount;
            GrnModel.ExcessApproval = Model.ExcessApproval;
            //DateTime d = DateTime.ParseExact(Model.GrnDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            GrnModel.GrnDate = Model.GrnDate;
            GrnModel.AmountplusTax = Model.AmountplusTax;
            GrnModel.GEDate = Model.GEDate;
            GrnModel.GrnNo = Model.GrnNo;
            GrnModel.IndentNo = Convert.ToInt32(Model.IndentNo);
            GrnModel.GETime = Model.GETime;
            GrnModel.InsuranceAmount = Model.InsuranceAmount;
            GrnModel.GrnRefNo = Model.GrnRefNo;
            GrnModel.GateRefNo = Model.GateRefNo;
            GrnModel.PoQty = Model.PoQty;
            GrnModel.AutomaticPONumber = Model.AutomaticPONumber;
            GrnModel.BENo = Model.BENo;
            GrnModel.CustomsDuty = Model.CustomsDuty;
            GrnModel.SupplierNameId = Model.SupplierNameId;
            GrnModel.GrnType = Model.GrnType;
            GrnModel.Pack_Forward = Model.Pack_Forward;
            GrnModel.DC_No = Model.DC_No;
            GrnModel.ST_VATcredit = Model.ST_VATcredit;
            GrnModel.ServiceTax = Model.ServiceTax;
            GrnModel.DCDate = Model.DCDate;
            GrnModel.ImportClearance = Model.ImportClearance;
            GrnModel.OtherCharges = Model.OtherCharges;
            GrnModel.INVoiceNo = Model.INVoiceNo;
            GrnModel.ExchangeRate = Model.ExchangeRate;
            GrnModel.Transporter = Model.Transporter;
            GrnModel.INVoiceDate = Model.INVoiceDate;
            GrnModel.ShipmentMode = Model.ShipmentMode;
            GrnModel.GeneralRemarks = Model.GeneralRemarks;
            GrnModel.PoNO = Model.PoNO;
         
            GrnModel.IndentNo = Convert.ToInt32(Model.IndentNo);
            GrnModel.Stores = Model.Stores;
            GrnModel.Grade = Model.Grade;
            GrnModel.GroupID = Model.GroupID;
            GrnModel.CategoryId = Model.CategoryId;
            GrnModel.IfGroupIsMaintenance = Model.IfGroupIsMaintenance;
            GrnModel.ColourId = Model.ColourId;
            GrnModel.Substance = Model.Substance;
            GrnModel.ServiceTaxPer = Model.ServiceTaxPer;
            GrnModel.QtyAsPerDc = Model.QtyAsPerDc;
            GrnModel.QtyAsPerPCS = Model.QtyAsPerPCS;
            GrnModel.QtyAsPerSQFT = Model.QtyAsPerSQFT;
            GrnModel.QTYType = Model.QTYType;
            GrnModel.QtyExcess = Model.QtyExcess;
            GrnModel.UOMId = Model.UOMId;
            GrnModel.Weight = Model.Weight;
            GrnModel.ReceivedQty = Model.ReceivedQty;
            GrnModel.ReceivedPCS = Model.ReceivedPCS;
            GrnModel.ReceivedSQFT = Model.ReceivedSQFT;
            GrnModel.ReceivedType = Model.ReceivedType;
            GrnModel.ShortageQty = Model.ShortageQty;
            GrnModel.ShortageSQFT = Model.ShortageSQFT;
            GrnModel.ShortagePCS = Model.ShortagePCS;
            GrnModel.ShortageType = Model.ShortageType;
            GrnModel.DOM = Model.DOM;
            GrnModel.AcceptedQty = Model.AcceptedQty;
            GrnModel.AcceptedSQFT = Model.AcceptedSQFT;
            GrnModel.AcceptedType = Model.AcceptedType;
            GrnModel.AcceptedPCS = Model.AcceptedPCS;
            GrnModel.PendingQty = Model.PendingQty;
            GrnModel.StoreLocation = Model.StoreLocation;
            GrnModel.Disp_SelectedMatOfPO = Model.Disp_SelectedMatOfPO;
            GrnModel.Disp_AllPOBasedOnSelecMat = Model.Disp_AllPOBasedOnSelecMat;
            GrnModel.GeneralRemarks = Model.GeneralRemarks;
            GrnModel.Disp_AllMatOfPO = Model.Disp_AllMatOfPO;
            GrnModel.Rate = Model.Rate;
            GrnModel.Value = Model.Value;
            GrnModel.MRPMargin = Model.MRPMargin;
            GrnModel.MRPPrice = Model.MRPPrice;
            GrnModel.AccessibleValue = Model.AccessibleValue;
            GrnModel.Discount_Per = Model.Discount_Per;
            GrnModel.Discount_Val = Model.Discount_Val;
            GrnModel.Ecess_Per = Model.Ecess_Per;
            GrnModel.Ecess_Val = Model.Ecess_Val;
            GrnModel.MRPMargin = Model.MRPMargin;
            GrnModel.BuyerMasterId = Model.BuyerMasterId;
            GrnModel.BuyerSeasonID = Model.BuyerSeasonID;
            GrnModel.ExciseDuty_Per = Model.ExciseDuty_Per;
            GrnModel.ExciseDuty_Val = Model.ExciseDuty_Val;
            GrnModel.SH_Ecess_Per = Model.SH_Ecess_Per;
            GrnModel.SH_Ecess_Val = Model.SH_Ecess_Val;
            GrnModel.ST_VAT_CST_Per = Model.ST_VAT_CST_Per;
            GrnModel.ST_VAT_CST_Val = Model.ST_VAT_CST_Val;
            GrnModel.Surcharge_Per = Model.Surcharge_Per;
            GrnModel.Surcharge_Val = Model.Surcharge_Val;
            GrnModel.RejectedPCS = Model.RejectedPCS;
            GrnModel.RejectedSQFT = Model.RejectedSQFT;
            GrnModel.RejectedType = Model.RejectedType;
            GrnModel.IONo = Model.IONo;
            GrnModel.RejectedQty = Model.RejectedQty;
            GrnModel.ExcessApproval = Model.ExcessApproval;
            GrnModel.AutomaticPONumber = Model.AutomaticPONumber;
            GrnModel.TagsPiecesDiscount_Per = Model.TagsPiecesDiscount_Per;
            GrnModel.TagsPiecesDiscount_Val = Model.TagsPiecesDiscount_Val;
            string[] dates = { PoDate };
            string[] formattedDates = new string[dates.Length];
            var format = "dd/MM/yyyy";
            if (!string.IsNullOrEmpty(PoDate))
            {
                DateTime Podate_ = DateTime.ParseExact(PoDate, format, CultureInfo.InvariantCulture);
                GrnModel.PoDate = Podate_;
            }
            else
            {
                GrnModel.PoDate = Model.PoDate != null ? Model.PoDate : null;
            }
            if (!string.IsNullOrEmpty(BEDate))
            {
                DateTime Bedate_ = DateTime.ParseExact(BEDate, format, CultureInfo.InvariantCulture);
                GrnModel.BEDate = Bedate_;
            }
            else
            {
                GrnModel.BEDate = Model.BEDate != null ? Model.BEDate : null;
            }

            GrnModel.ModeofTransport = Model.ModeofTransport;
            GrnModel.MachineryName = Model.MachineryName;
            GrnModel.PoReceivedQty = Model.PoReceivedQty;
            GrnModel.GST = Model.GST;
            GrnModel.SGST = Model.SGST;
            GrnModel.CGST = Model.CGST;
            GrnModel.IGST = Model.IGST;
            GrnModel.TCS = Model.TCS;
            GrnModel.TCSValue = Model.TCSValue;
            GrnModel.ImportIGST = Model.ImportIGST;
            GrnModel.Surcharges = Model.Surcharges;
            GrnModel.GSTValue = Model.GSTValue;
            GrnModel.SGSTValue = Model.SGSTValue;
            GrnModel.CGSTValue = Model.CGSTValue;
            GrnModel.IGSTValue = Model.IGSTValue;
            GrnModel.ImportIGSTValue = Model.ImportIGSTValue;
            GrnModel.SurchargesValue = Model.SurchargesValue;
            GrnModel.FreightAmtPercent = Model.FreightAmtPercent;
            GrnModel.InsuranceAmtPercent = Model.InsuranceAmtPercent;
            GrnModel.CustomDutyPercent = Model.CustomDutyPercent;
            GrnModel.ClearingChargePercent = Model.ClearingChargePercent;
            GrnModel.PackforwardPercent = Model.PackforwardPercent;
            GrnModel.FreightTaxAmt = Model.FreightTaxAmt;
            GrnModel.InsuranceTaxAmt = Model.InsuranceTaxAmt;
            GrnModel.CustomDutyTax = Model.CustomDutyTax;
            GrnModel.ClearingChargeTax = Model.ClearingChargeTax;
            GrnModel.PackforwardTax = Model.PackforwardTax;
            GrnModel.ValueInRps = Model.ValueInRps;
            GrnModel.Currencykey = Model.Currencykey;
            GrnModel.TotalAmount = Model.TotalAmount;
            GrnModel.TotalCost = Model.TotalCost;
            GrnModel.NetTotal = Model.NetTotal;
            GrnModel.TagsPiecesDiscount = Model.TagsPiecesDiscount;
            GrnModel.ServiceChargeTax = Model.ServiceChargeTax;
            GrnModel.CreatedBy = Session["UserName"].ToString();
            GrnModel.CreatedDate = DateTime.Now;
            GRN_EntityModelNew GRN = new GRN_EntityModelNew();
            GRN = Manager.IsSupplierWithInvoice(Model.INVoiceNo, Model.SupplierNameId, Model.MaterialId.Value, Model.DC_No);
            if (GRN == null && Model.GrnID == 0)
            {
                GrnModel_ = Manager.Post(GrnModel);
            }
            else if (GRN != null && Model.GrnID != 0)
            {
                GrnModel_ = Manager.Post(GrnModel);
            }
            else if (Model.GrnID != 0)
            {
                GrnModel_ = Manager.Post(GrnModel);
            }
            else if (GRN != null && Model.GrnID == 0)
            {
                Message = "Already Existed";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            List<GRN_EntityModelNew> listGRNEntityModel = new List<GRN_EntityModelNew>();
            listGRNEntityModel = Manager.GetGRNNO(GrnModel.GrnNo);
            decimal? TotalAmount = 0;
            TotalAmount = listGRNEntityModel.Sum(x => x.AmountplusTax);
            if (Model.SizeRangeRefValue != null)
            {

                string[] sizeAray = Model.SizeRangeRefValue.Split(',');
                string[] Qty_ValueAray = Model.Qty_Value.Split(',');
                string[] Received_valueAray = Model.Received_value.Split(',');
                string[] Shortage_valueAray = Model.Shortage_value.Split(',');
                string[] Rejectd_valueAray = Model.Rejectd_value.Split(',');
                string[] Accepted_valueAray = Model.Accepted_value.Split(',');
                int count_ = 1;

                List<GRNSizeQuantityObjectNew> listGRNOrderSizeRangeQuantity = new List<GRNSizeQuantityObjectNew>();
                listGRNOrderSizeRangeQuantity = Manager.GRNSizeQuantityGet().Where(x => x.Grnid_FK == GrnModel_.GrnID).ToList();
                if (count_ == 1)
                {
                    Manager.GRNSizeRangeQuantityDelete(GrnModel_.GrnID);
                    count_++;
                }

                int count = 0;
                foreach (var item in sizeAray)
                {
                    GRNSizeQuantityObjectNew sizeItemMaterial = new GRNSizeQuantityObjectNew();
                    sizeItemMaterial.Size = item;
                    sizeItemMaterial.SizeRangeRefValue = Convert.ToDecimal(item);
                    sizeItemMaterial.quantity = 0;
                    sizeItemMaterial.Qty_Value = Convert.ToDecimal(Qty_ValueAray[count]);
                    sizeItemMaterial.Received_value = Convert.ToDecimal(Received_valueAray[count]);
                    sizeItemMaterial.Shortage_value = Convert.ToDecimal(Shortage_valueAray[count]);
                    sizeItemMaterial.Rejectd_value = Convert.ToDecimal(Rejectd_valueAray[count]);
                    sizeItemMaterial.Accepted_value = Convert.ToDecimal(Accepted_valueAray[count]);
                    sizeItemMaterial.Grnid_FK = GrnModel_.GrnID;
                    sizeItemMaterial.CreatedDate = DateTime.Now;
                    Manager.GRNSizeRangeDetailsPost(sizeItemMaterial);
                    count++;
                }
            }

            if (Model.GrnID == 0 && GrnModel_ != null && GrnModel_.GrnID != 0)
            {
                Message = "Saved Successfully";

            }
            else if (Model.GrnID != 0 && GrnModel_ != null && GrnModel_.GrnID != 0)
            {
                Message = "Updated Succesfully";
            }
            List<OpeningStockPinkModel> openingStockModelList = new List<OpeningStockPinkModel>();
            OpeningStockPinCardManager openingStockPinCardManager = new OpeningStockPinCardManager();
            IssueSlip_SingleManager manager = new IssueSlip_SingleManager();
            IssueSlipManager issueSlipManager = new IssueSlipManager();
            openingStockModelList = manager.GetOpeningMaterialList();
            openingStockModelList = openingStockModelList.Where(x => x.materialmasterid == GrnModel_.Grn_MaterialID).OrderByDescending(x => x.OpeningStockDate).ToList();
            foreach (var item in openingStockModelList)
            {
                tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
                OpeningStockPinCard openingStockPinCard = new OpeningStockPinCard();
                openingStockPinCard.materialmasterid = item.materialmasterid;
                openingStockPinCard.MaterialDescription = item.MaterialDescription;
                openingStockPinCard.shortunitname = item.shortunitname;
                openingStockPinCard.categoryname = item.categoryname;
                openingStockPinCard.storename = item.storename;
                if (GrnModel_.MaterialType == 1)
                {
                    openingStockPinCard.MaterialType = "Local";
                }
                if (GrnModel_.MaterialType == 2)
                {
                    openingStockPinCard.MaterialType = "Customer Import";
                }
                if (GrnModel_.MaterialType == 3)
                {
                    openingStockPinCard.MaterialType = "Direct Import";
                }
                openingStockPinCard.groupname = item.groupname;
                if (GrnModel_.Grn_MaterialID != null && GrnModel_.Grn_MaterialID != 0)
                {
                    issueSlip_MaterialDetails = issueSlipManager.GetMultipleIssueWithMaterialID(GrnModel_.Grn_MaterialID.Value);
                }

                DateTime stockdate = DateTime.ParseExact(GrnModel_.GrnDate, @"d/M/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);
                openingStockPinCard.OpeningStockDate = stockdate;
                openingStockPinCard.Rate = item.Rate;
                openingStockPinCard.materialpcs = item.materialpcs;
                openingStockPinCard.OpeningAsOnStock = item.OpeningAsOnStock;
                openingStockPinCard.Issues = item.Issues;
                openingStockPinCard.Receipt = GrnModel_.AcceptedQty;
                openingStockPinCard.ClosingStock = item.ClosingStock + GrnModel_.AcceptedQty;
                openingStockPinCard.ClosingStockValue = item.ClosingStockValue;
                openingStockPinCardManager.Post(openingStockPinCard);
            }
            GrnModel.UpdatedBy = "";
            MaterialManager materialManager = new MaterialManager();
            var result = new
            {
                Model = GrnModel_,
                GradeDesc = GradeDescription,
                GroupDesc = GroupDescription,
                StoreDesc = StoreDescription,
                CategoryDesc = CategoryDescription,
                ColorDesc = ColorDescription,
                UomDesc = UomDescription,
                QtyAsPerDesc = QtyAsPerDescription,
                ReceivedSQFTDesc = ReceivedSQFTDescription,
                RejectedSQFTDesc = RejectedSQFTDescription,
                AcceptedSQFTDesc = AcceptedSQFTDescription,
                QTYTypeDesc = QTYTypeDescription,
                ReceivedTypeDesc = ReceivedTypeDescription,
                RejectedTypeDesc = RejectedTypeDescription,
                AcceptedTypeDesc = AcceptedTypeDescription,
                storeNameDesc = storeNameDescription,
                MaterialDesc = MaterialDescription + " " + ColorDescription,
                SubstanceDesc = SubstanceRangeDescription
            };
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            SupplierMaster supplierMaster = new SupplierMaster();
            if (GrnModel_ != null && GrnModel_.SupplierNameId != 0)
            {
                supplierMaster = supplierMasterManager.GetSupplierMasterId(GrnModel_.SupplierNameId);
            }

            return Json(new { result = result, Message = Message, TotalAmount = TotalAmount, supplierMaster = supplierMaster }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult Delete(int GrnID)
        {

            GrnManagerNew Manager = new GrnManagerNew();
            string status = "";
            GRN_EntityModelNew Model = Manager.GetGRNSelectedRow(GrnID);
            if (Model != null)
            {
                status = "Success";
                string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM  tbl_grnnew WHERE grnno='" + Model.GrnNo + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
                //Manager.Delete(Model.GrnID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMaterial(int GrnID)
        {

            GrnManagerNew Manager = new GrnManagerNew();
            string status = "";
            GRN_EntityModelNew Model = Manager.GetGRNSelectedRow(GrnID);
            if (Model != null)
            {
                status = "Success";
                string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM  tbl_grnnew WHERE GrnID='" + Model.GrnID + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchDetails()
        {

            string Search = Request.QueryString["GrnRefNo"];


            return RedirectToAction("GrnDetails", "GRN_Detail", new { SearchFilter = Search });
        }


    }
}

