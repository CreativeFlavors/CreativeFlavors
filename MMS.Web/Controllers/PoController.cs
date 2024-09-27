using iTextSharp.text;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Web.Models;
using MMS.Web.Models.Addressdetails;
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
        #region start
        public ActionResult PoMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PoGrid(int page = 1, int pageSize = 15)
        {
            PoManager poManager = new PoManager();
            PoModel poModel = new PoModel();
            var totalList = poManager.GetPODetails();
            var totalListsummary = poManager.GetPOHeader();
            var totalCount1 = totalListsummary.Count();

            int totalPages1 = (int)Math.Ceiling((double)totalCount1 / pageSize);

            int startIndex1 = (page - 1) * pageSize;
            int endIndex1 = Math.Min(startIndex1 + pageSize - 1, totalCount1 - 1);
            totalListsummary = totalListsummary.OrderByDescending(s => s.ponumber)
                         .Skip(startIndex1)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPagessummary = totalPages1;
            ViewBag.CurrentPagesummary = page;
            ViewBag.PageSizesummary = pageSize;
            poModel.purchaceorderheaders = totalListsummary;

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
            return PartialView("partial/PoGrid", poModel);
        }
        [HttpGet]
        public ActionResult POSummaryGrid(int page = 1, int pageSize = 15)
        {
            PoManager poManager = new PoManager();
            PoModel poModel = new PoModel();
            var totalListsummary = poManager.GetPOHeader();
            var totalCount1 = totalListsummary.Count();

            int totalPages1 = (int)Math.Ceiling((double)totalCount1 / pageSize);

            int startIndex1 = (page - 1) * pageSize;
            int endIndex1 = Math.Min(startIndex1 + pageSize - 1, totalCount1 - 1);
            totalListsummary = totalListsummary.OrderByDescending(s => s.ponumber)
                         .Skip(startIndex1)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPagessummary = totalPages1;
            ViewBag.CurrentPagesummary = page;
            ViewBag.PageSizesummary = pageSize;
            poModel.purchaceorderheaders = totalListsummary;

            return PartialView("~/Views/Po/Partial/SummeryGrid.cshtml", poModel);
        }
        [HttpGet]
        public ActionResult PODetailsGrid(int page = 1, int pageSize = 15)
        {
            PoManager poManager = new PoManager();
            PoModel poModel = new PoModel();
            var totalList = poManager.GetPODetails();

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

            return PartialView("~/Views/Po/Partial/DetailsGrid.cshtml");
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
                if (pono == 0)
                {
                    model.PoNumber = 1;
                }
                else
                {
                    model.PoNumber = pono;
                }
            }

            return View(model);
        }
        #endregion
        #region Crud Operation
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
                IndentCart indentcart = indentNewMaterialManager.GetIndentCartById(model.IndentNumber);
                if (model.IndentPoMapId == 0)
                {
                    indentPoMapping.IndentPoMapId = model.IndentPoMapId;
                    indentPoMapping.SupplierId = model.SupplierId;
                    indentPoMapping.ProductId = model.ProductId;
                    if (indentcart != null)
                    {
                        indentPoMapping.IndentId = indentcart.IndentCartId;
                    }
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
            var count = productlist.Count;
            if (count <= 0)
            {
                status = "Not Existed";
                return Json(new { message = status }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (model.PodetailId == 0)
                {
                    PoHeader poHeader = new PoHeader();
                    poHeader.PoDate = model.PoDate;
                    poHeader.SupplierId = model.SupplierId;
                    poHeader.qty = model.Items;
                    poHeader.items = count;
                    poHeader.TotalDiscountValue = model.Total_DiscountValue;
                    poHeader.TotalSubtotalValue = model.Total_SubtotalValue;
                    poHeader.TotalTaxValue = model.Total_TaxValue;
                    poHeader.TotalTotalValue = model.Total_TotalValue;
                    poHeader.TotalPrice = model.Total_Price;
                    poHeader.PoNumber = model.PoNumber;
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
                        poDetail.indentqty = i.IndentQty;
                        poDetail.Quantity = i.PoQty;
                        poDetail.DiscountPercentage = i.DiscountPercentage;
                        poDetail.DiscountValue = i.DiscountValue;
                        poDetail.Subtotal = i.Subtotal;
                        poDetail.TaxValue = i.TaxValue;
                        poDetail.TotalValue = i.TotalValue;
                        poDetail.TaxInclusive = i.TaxInclusive;
                        poDetail.PoNumber = i.PoNumber;
                        poDetail.IndentNumber = (int)i.IndentNumber;
                        poManager.PostPoDetail(poDetail);
                    }
                    status = "Inserted";
                }
                else
                {
                    PoHeader poHeader = new PoHeader();
                    poHeader.PoNumber = model.PoNumber;
                    poHeader.TotalDiscountValue = model.Total_DiscountValue;
                    poHeader.TotalSubtotalValue = model.Total_SubtotalValue;
                    poHeader.TotalTotalValue = model.Total_TotalValue;
                    poHeader.TotalTaxValue = model.Total_TaxValue;
                    poHeader.TotalPrice = model.Total_Price;
                    poHeader.SupplierId = model.SupplierId;
                    poManager.PutPoHeader(poHeader);

                    PoDetail poDetail = new PoDetail();
                    poDetail.PodetailId = model.PodetailId;
                    poDetail.SupplierId = model.SupplierId;
                    poDetail.ProductId = model.ProductId;
                    poDetail.UnitPrice = model.UnitPrice;
                    poDetail.indentqty = model.IndentQty;
                    poDetail.Quantity = model.PoQty;
                    poDetail.PoDate = model.PoDate;
                    poDetail.Status = model.Status;
                    poDetail.TaxValue = model.TaxValue;
                    poDetail.DiscountPercentage = model.DiscountPercentage;
                    poDetail.TaxPercentage = model.TaxPercentage;
                    poDetail.DiscountValue = model.DiscountValue;
                    poDetail.Subtotal = model.Subtotal;
                    poDetail.TotalValue = model.TotalValue;
                    poDetail.PoNumber = model.PoNumber;
                    poDetail.IndentNumber = model.IndentNumber;
                    poManager.PutPodetail(poDetail);
                    status = "Updated";
                }
                return Json(new { Success = true, message = status }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult PoIndentEdit(int indentpomapid)
        {
            PoManager poManager = new PoManager();
            PoModel model = new PoModel();
            var data = poManager.Getpoid(indentpomapid);
            var data1 = poManager.Get().Where(m => m.PoheaderId == data.PoheaderId);
            model.PodetailId = data.PodetailId;
            model.PoDate = data.PoDate;
            model.PoNumber = data.PoNumber;
            model.IndentNumber = (int)data.IndentNumber;
            model.SupplierId = data.SupplierId;
            model.ProductId = data.ProductId;
            model.StoreCode = data.StoreCode;
            model.UomId = data.UomId;
            model.UnitPrice = data.UnitPrice;
            model.IndentQty = data.indentqty;
            model.PoQty = data.Quantity;
            model.TaxPercentage = data.TaxPercentage;
            model.TaxValue = data.TaxValue;
            model.DiscountPercentage = data.DiscountPercentage;
            model.DiscountValue = data.DiscountValue;
            model.Subtotal = data.Subtotal;
            model.TotalValue = data.TotalValue;
            model.DataListsp = poManager.GetindendetailsId(indentpomapid);

            // Serialize DataList to JSON
            ViewBag.DataListJson = Newtonsoft.Json.JsonConvert.SerializeObject(model.DataListsp);
            return View("~/Views/Po/PoDetails.cshtml", model);
        }

        [HttpGet]
        public ActionResult GetIndentNoDetails(int indentheaderid)
        {
            IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
            List<IndentMaterialModel> indentDetails = new List<IndentMaterialModel>();
            List<Indentdetail> indentdetail = indentNewMaterialManager.GetIndentdetailsList(indentheaderid);

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
            return Json(indentDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductDetails(int? productId)
        {
            IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
            List<IndentMaterialModel> indentDetails = new List<IndentMaterialModel>();
            //List<Product> indentDetails = new List<Product>();
            //List<Indentdetail> indentdetail = indentNewMaterialManager.GetProductsId(productId);
            ProductManager productManager = new ProductManager();
            product product1 = productManager.GetId((int)productId);

            //foreach (var detail in product1)
            //{
            //    ProductManager productManager1 = new ProductManager();
            product product = productManager.GetId((int)product1.ProductId);
            StoreMasterManager storeManager = new StoreMasterManager();
            StoreMaster store = storeManager.GetStoreMasterId(product1.StoreId);
            UOMManager uomManager = new UOMManager();
            UomMaster uom = uomManager.GetUomMasterId(product1.UomMasterId);
            TaxTypeManager taxManager = new TaxTypeManager();
            TaxTypeMaster tax = taxManager.GetTaxMasterId(product1.TaxMasterId);
            IndentMaterialModel Model = new IndentMaterialModel
            {
                MaterialId = product.ProductId,
                MaterialNameId = product.ProductName,
                StoreNameId = store.StoreName,
                UomNameId = uom.ShortUnitName,
                Price = product.Price,
                //IndentQty = (decimal)detail.IndentQty,
                TaxNameId = tax.TaxValue
            };
            indentDetails.Add(Model);
            //}
            return Json(indentDetails, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetSupplierProducts(int indentNumber, int supplierId)
        {
            IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
            List<IndentMaterialModel> indentcart = new List<IndentMaterialModel>();
            List<Indentdetail> indentdetail = indentNewMaterialManager.GetIndentdetailsList(indentNumber);

            foreach (var detail in indentdetail)
            {
                // Check if the material (product) is supplied by the selected supplier
                if (IsMaterialSuppliedBySupplier(detail.MaterialId, supplierId))
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
                    indentcart.Add(Model);
                }
            }
            return Json(indentcart, JsonRequestBehavior.AllowGet);
        }

        private bool IsMaterialSuppliedBySupplier(int? productId, int supplierId)
        {
            var supplierMaterialManager = new SupplierMaterialManager();
            var isSupplied = supplierMaterialManager.IsMaterialSuppliedBySupplier(productId, supplierId);

            return isSupplied;
        }

        [HttpPost]
        public ActionResult PoDelete(int poId, bool IsChecked)
        {
            string status = "";
            PoManager poManager = new PoManager();
            var vindentpomapping = poManager.Getpoid(poId);
            if (vindentpomapping != null)
            {
                status = "Success";
                poManager.Deletepodt(poId, IsChecked);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Filter
        [HttpGet]
        public ActionResult PoSearch(string filter)
        {
            try
            {
                PoManager poManager = new PoManager();
                var totalList = poManager.GetPODetails();
                List<PODetails> indentpomappingsps = new List<PODetails>();
                indentpomappingsps = totalList
                   .Where(x =>
                       x.ProductName.ToLower().Contains(filter.ToLower()))
                   .ToList();
                return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PoSearchsummary(PoModel model)
        {
            PoManager poManager = new PoManager();
            Supplier_masterManager supplier_MasterManager = new Supplier_masterManager();
            var supplier = supplier_MasterManager.getsupplierId(model.SupplierId);
            var totalList = poManager.GetPOHeader();
            if(supplier.SupplierId != 0)
            {
                if (model.CreatedDate != null && model.FulfillDate != null)
                {
                    var indentpomappingsps = totalList
         .Where(x =>
             x.ponumber.ToString().ToLower().Contains(model.PoNumber.ToString().ToLower()) &&
             x.suppliername == supplier.Suppliername &&
             x.createddate.Value.Date == model.CreatedDate.Value.Date &&
             x.fullfilldate.Value.Date == model.FulfillDate.Value.Date &&
             x.status == model.Status)
         .ToList();
                    return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                }
                else if (model.CreatedDate == null && model.FulfillDate == null)
                {
                    if (model.CreatedDate == null && model.FulfillDate == null && model.PoNumber == 0)
                    {
                        var indentpomappingsps = totalList
    .Where(x => x.suppliername == supplier.Suppliername &&
    x.status == model.Status)
    .ToList();
                        return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                    }
                    else if (model.CreatedDate == null && model.FulfillDate == null && model.Status == null)
                    {
                        var indentpomappingsps = totalList
.Where(x =>
x.ponumber.ToString().ToLower().Contains(model.PoNumber.ToString().ToLower()) &&
x.suppliername == supplier.Suppliername &&
x.ponumber == model.PoNumber)
.ToList();
                        return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var indentpomappingsps = totalList
.Where(x =>
x.ponumber.ToString().ToLower().Contains(model.PoNumber.ToString().ToLower()) &&
x.suppliername == supplier.Suppliername &&
x.status == model.Status)
.ToList();
                        return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var indentpomappingsps = totalList
.Where(x =>
x.ponumber.ToString().ToLower().Contains(model.PoNumber.ToString().ToLower()) &&
x.suppliername == supplier.Suppliername &&
x.createddate.Value.Date == model.CreatedDate.Value.Date &&
x.status == model.Status)
.ToList();
                    return Json(indentpomappingsps, JsonRequestBehavior.AllowGet);

                }
            }
          
            return Json(totalList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Getpono(int id)
        {
            PoManager PoManager = new PoManager();
            var data = PoManager.GetPOId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
