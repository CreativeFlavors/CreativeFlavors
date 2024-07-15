using iTextSharp.text;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Web.Models;
using MMS.Web.Models.IndentMaterial;
using MMS.Web.Models.Po;
using MMS.Web.Models.Product;
using MMS.Web.Models.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMS.Web.Controllers
{
    public class PoController : Controller
    {
        public ActionResult PoMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PoGrid(int page = 1, int pageSize = 9)
        {
            PoManager poManager = new PoManager();
            var totalList = poManager.GetindentPoMappingList();
            var totalCount = totalList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Productslist = totalList;
            return PartialView("partial/PoGrid");
        }

        [HttpGet]
        public ActionResult PoDetails(int? poheaderid)
        {
            PoModel model = new PoModel();
            PoManager poManager = new PoManager();

            if (poheaderid == null)
            {
                model.PoDate = DateTime.Today;
                int pono = poManager.GetNextPONumberFromDatabase();
                model.PoNumber = pono;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SavePoIndentMapping(PoModel model)
        {
            try
            {
                IndentPoMapping indentPoMapping = new IndentPoMapping();
                string status = "";
                PoManager poManager = new PoManager();
                ProductManager productManager = new ProductManager();
                product product = new product();
                product = productManager.GetId(model.ProductId);
                IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
                Indentdetail indentdetail = indentNewMaterialManager.GetIndentDetailById(model.IndentNumber);
                if (model.IndentPoMapId == 0)
                {
                    indentPoMapping.IndentPoMapId = model.IndentPoMapId;
                    indentPoMapping.SupplierId = model.SupplierId;
                    indentPoMapping.ProductId = model.ProductId;
                    indentPoMapping.IndentId = model.IndentNumber;
                    indentPoMapping.IndentProductId = model.ProductId;
                    indentPoMapping.StoreCode = product.StoreId;
                    indentPoMapping.UnitPrice = model.UnitPrice;
                    indentPoMapping.IndentQty = model.IndentQty;
                    indentPoMapping.PoQty = model.PoQty;
                    indentPoMapping.PoDate = model.PoDate;
                    indentPoMapping.Status = model.Status;
                    indentPoMapping.IsActive = true;
                    indentPoMapping.WithIndentReference = true;
                    indentPoMapping.UomId = product.UomMasterId;
                    indentPoMapping.TaxValue = model.TaxValue;
                    indentPoMapping.DiscountPercentage = model.DiscountPercentage;
                    indentPoMapping.TaxPercentage = model.TaxPercentage;
                    indentPoMapping.DiscountValue = model.DiscountValue;
                    indentPoMapping.Subtotal = model.Subtotal;
                    indentPoMapping.TotalValue = model.TotalValue;
                    indentPoMapping.TaxInclusive = true;
                    indentPoMapping.PoNumber = model.PoNumber;
                    indentPoMapping.IndentNumber = model.IndentNumber;
                    poManager.PostIndentPoMapping(indentPoMapping);
                    status = "Inserted";
                }
                else
                {
                    indentPoMapping.IndentPoMapId = model.IndentPoMapId;
                    indentPoMapping.SupplierId = model.SupplierId;
                    indentPoMapping.ProductId = model.ProductId;
                    indentPoMapping.IndentId = model.IndentNumber;
                    indentPoMapping.IndentProductId = model.ProductId;
                    indentPoMapping.StoreCode = product.StoreId;
                    indentPoMapping.UnitPrice = model.UnitPrice;
                    indentPoMapping.IndentQty = model.IndentQty;
                    indentPoMapping.PoQty = model.PoQty;
                    indentPoMapping.PoDate = model.PoDate;
                    indentPoMapping.Status = model.Status;
                    indentPoMapping.IsActive = true;
                    indentPoMapping.WithIndentReference = true;
                    indentPoMapping.UomId = product.UomMasterId;
                    indentPoMapping.TaxValue = model.TaxValue;
                    indentPoMapping.DiscountPercentage = model.DiscountPercentage;
                    indentPoMapping.TaxPercentage = model.TaxPercentage;
                    indentPoMapping.DiscountValue = model.DiscountValue;
                    indentPoMapping.Subtotal = model.Subtotal;
                    indentPoMapping.TotalValue = model.TotalValue;
                    indentPoMapping.TaxInclusive = true;
                    indentPoMapping.PoNumber = model.PoNumber;
                    indentPoMapping.IndentNumber = model.IndentNumber;
                    poManager.PutIndentPoMapping(indentPoMapping);
                    status = "Updated";
                }
                List<PoModel> updatedIndents = new List<PoModel>();
                updatedIndents = poManager.GetindentPoMappingList()
                                .Where(i => i.PoNumber == model.PoNumber)
                                .Select(i => new PoModel
                                {
                                    ProductName = i.ProductName,
                                    UnitPrice = i.UnitPrice,
                                    IndentQty = i.IndentQty,
                                    DiscountValue = i.DiscountValue,
                                    Subtotal = i.SubtotalValue,
                                    TaxValue = i.TaxValue,                                                                       
                                    TotalValue = i.TotalValue
                                })
                                .ToList();

                return Json(new { Success = true, message = status, Indents = updatedIndents });
            }
            catch (Exception ex)
            {
                string status = "";
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveConfirmPo(PoModel model)
        {
            var status = "";
            PoManager poManager = new PoManager();
            var productlist = poManager.GetIndentpomapList(model.PoNumber);
            //IndentCart indentCart = indentNewMaterialManager.Getindentcard(model.IndentCartId);
            var count = productlist.Count;
            if (count <= 0)
            {
                status = "Not Existed";
                return Json(new { message = status }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                PoHeader poHeader = new PoHeader();
                poHeader.PoDate = model.PoDate;
                poHeader.SupplierId = model.SupplierId;
                poHeader.Items = model.Items;
                poHeader.TotalDiscountValue = model.Total_DiscountValue;
                poHeader.TotalSubtotalValue = model.Total_SubtotalValue;
                poHeader.TotalTaxValue = model.Total_TaxValue;
                poHeader.TotalTotalValue = model.Total_TotalValue;
                poHeader.TotalPrice = model.Total_Price;
                poHeader.PoNumber = model.PoNumber;
                //poHeader.Quantity = model.IndentQty;
                //poHeader.Quantity = model.IndentQty;
                //poHeader.Quantity = model.IndentQty;
                poManager.PostPoHeader(poHeader);

                foreach (var i in productlist)
                {
                    PoDetail poDetail = new PoDetail();
                    poDetail.PoheaderId = poHeader.PoheaderId;
                    poDetail.PoDate = i.PoDate;
                    poDetail.StoreCode = i.StoreCode;
                    poDetail.SupplierId = i.SupplierId;
                    poDetail.ProductId = i.ProductId;
                    poDetail.UomId = i.UomId;
                    poDetail.TaxPercentage = i.TaxPercentage;
                    poDetail.UnitPrice = i.UnitPrice;
                    poDetail.Quantity = i.IndentQty;
                    poDetail.DiscountPercentage = i.DiscountPercentage;
                    poDetail.DiscountValue = i.DiscountValue;
                    poDetail.Subtotal = i.Subtotal;
                    poDetail.TaxValue = i.TaxValue;
                    poDetail.TotalValue = i.TotalValue;
                    poDetail.TaxInclusive = i.TaxInclusive;
                    poDetail.PoNumber = i.PoNumber;
                    poDetail.IndentNumber = i.IndentNumber;
                    //poDetail.DiscountPercentage = i.DiscountPercentage;
                    //poDetail.DiscountPercentage = i.DiscountPercentage;
                    //poDetail.DiscountPercentage = i.DiscountPercentage;
                    //poDetail.DiscountPercentage = i.DiscountPercentage;
                    //poDetail.DiscountPercentage = i.DiscountPercentage;


                    poManager.PostPoDetail(poDetail);
                    //indentNewMaterialManager.UpdateTempIndent(i.MaterialNameId, i.IndentRequiredQty);

                }
                status = "Inserted";
                return Json(new { Success = true, message = status }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult PoIndentEdit(int indentpomapid)
         {
            PoManager poManager = new PoManager();
            PoModel model = new PoModel();
            var data = poManager.GetIndentPoMapid(indentpomapid);
            var data1 = poManager.GetindentPoMappingId(indentpomapid);
            //if (data1 != null || data!=null)
            //{
            //    return Json(new
            //    {
            //        success = true,
            //        SupplierId = data.SupplierId,
            //        PoDate = data.PoDate,
            //        PoNumber = data.PoNumber,
            //        IndentNumber = data.IndentNumber,
            //        ProductName = data1.ProductName,
            //        StoreName = data1.StoreName,
            //        UomName = data1.UomName, 
            //        UnitPrice = data1.UnitPrice,
            //        IndentQty = data1.IndentQty,
            //        DiscountPercentage = data.DiscountPercentage,
            //        DiscountValue = data1.DiscountValue,
            //        SubtotalValue = data1.SubtotalValue,
            //        TaxPercentage = data.TaxPercentage,
            //        TaxValue = data1.TaxValue,
            //        TotalValue = data1.TotalValue
            //    }, JsonRequestBehavior.AllowGet);
            //}

            //return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            model.IndentPoMapId = data.IndentPoMapId;
            model.PoDate = data.PoDate;
            model.PoNumber = data.PoNumber;
            model.IndentNumber = data.IndentNumber;
            model.SupplierId = data.SupplierId;
            model.ProductId = data.ProductId;
            model.StoreCode = data.StoreCode;
            model.UomId = data.UomId;
            model.UnitPrice = data.UnitPrice;
            model.IndentQty = data.IndentQty;
            model.PoQty = data.PoQty;
            model.TaxPercentage = data.TaxPercentage;
            model.TaxValue = data.TaxValue;
            model.DiscountPercentage = data.DiscountPercentage;
            model.DiscountValue = data.DiscountValue;
            model.Subtotal = data.Subtotal;
            model.TotalValue = data.TotalValue;
            model.DataList = poManager.GetindentPoMappingForTableData(indentpomapid);

            // Serialize DataList to JSON
            ViewBag.DataListJson = Newtonsoft.Json.JsonConvert.SerializeObject(model.DataList);
            return View("~/Views/PurchaseOrder/PoDetails.cshtml", model);
        }

        [HttpGet]
        public ActionResult GetIndentNoDetails(int indentheaderid)
        {
            IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
            //IndentCart indentCart = indentNewMaterialManager.GetIndentCartById(indentnumber);
            List<IndentMaterialModel> indentDetails = new List<IndentMaterialModel>();
            List<Indentdetail> indentdetail = indentNewMaterialManager.GetIndentdetailsList(indentheaderid);

            //if (indentCart != null)
            //{
            foreach (var detail in indentdetail)
            {
                ProductManager productManager = new ProductManager();
                product product = productManager.GetId((int)detail.MaterialId);
                StoreMasterManager storeManager = new StoreMasterManager();
                StoreMaster store = storeManager.GetStoreMasterId(detail.StoreCode);
                UOMManager uomManager = new UOMManager();
                UomMaster uom = uomManager.GetUomMasterId(detail.UomId);
                TaxTypeManager taxManager = new TaxTypeManager();
                TaxTypeMaster tax = taxManager.GetTaxMasterId(detail.TaxId);
                IndentMaterialModel Model = new IndentMaterialModel
                {
                    MaterialId = product.ProductId,
                    MaterialNameId = product.ProductName,
                    StoreNameId = store.StoreName,
                    UomNameId = uom.ShortUnitName,
                    Price = product.Price,
                    IndentQty = (decimal)detail.IndentQty,
                    TaxNameId = tax.TaxValue
                };
                indentDetails.Add(Model);
            }
            //decimal? indentqty = indentCart.IndentRequiredQty;

            //ProductManager productManager = new ProductManager();
            //product product = productManager.GetId((int)indentCart.MaterialNameId);

            //int productid = 0;
            //string productname = null;
            //decimal? unitprice = 0;
            //string tax = null;
            //string store = null;
            //string uom = null;

            //if (product != null)
            //{
            //    productid = product.ProductId;
            //    productname = product.ProductName;
            //    unitprice = product.Price;

            //    UOMManager uOMManager = new UOMManager();
            //    UomMaster uomname = uOMManager.GetUomMasterId(product.UomMasterId);
            //    if (uomname != null)
            //    {
            //        uom = uomname.ShortUnitName;
            //    }

            //    TaxTypeManager taxTypeManager = new TaxTypeManager();
            //    TaxTypeMaster taxTypeMaster = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
            //    if (taxTypeMaster != null)
            //    {
            //        tax = taxTypeMaster.TaxValue;
            //    }

            //    StoreMasterManager storeMasterManager = new StoreMasterManager();
            //    StoreMaster storeMaster = storeMasterManager.GetStoreMasterId(product.StoreId);
            //    if (storeMaster != null)
            //    {
            //        store = storeMaster.StoreName;
            //    }
            //    }
            //}

            //return Json(new
            //{
            //    ProductId = productid,
            //    ProductName = productname,
            //    StoreCode = store,
            //    Uom = uom,
            //    UnitPrice = unitprice,
            //    IndentQty = indentqty,
            //    Tax = tax,
            //}, JsonRequestBehavior.AllowGet);
            return Json(indentDetails, JsonRequestBehavior.AllowGet);


        }

        //    return Json(null);
        //}

        public ActionResult PoDelete(int indentpomapid)
        {
            IndentPoMapping indentpomapping = new IndentPoMapping();
            string status = "";
            PoManager poManager = new PoManager();
            indentpomapping = poManager.GetIndentPoMapid(indentpomapid);
            if (indentpomapping.IndentPoMapId == indentpomapid)
            {
                status = "Success";
                poManager.Delete(indentpomapid);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PoSearch(string filter)
         {
            try
            {
                PoManager poManager = new PoManager();
                var totalList = poManager.GetindentPoMappingList();
                List<IndentPoMappingsp> indentpomappingsps = new List<IndentPoMappingsp>();
                 indentpomappingsps = totalList
                    .Where(x =>
                        x.ProductName.ToLower().Contains(filter.ToLower()) ||
                        x.SupplierName.ToLower().Contains(filter.ToLower()) ||
                        x.PoNumber.ToString().Contains(filter)) 
                    .ToList(); 
                return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
