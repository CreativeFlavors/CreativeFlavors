using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.Production;
using MMS.Core.Entities;
using MMS.Web.Models.IndentMaterial;
using MMS.Data.StoredProcedureModel;
using MMS.Common;
using MMS.Core.Entities.Stock;
using System.Data.Entity;
using MMS.Web.Models;
using Npgsql.Replication.PgOutput.Messages;

namespace MMS.Web.Controllers
{
    public class IndentMaterialController : Controller
    {
        public ActionResult IndentMaterialMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IndentMaterialGrid(int page = 1, int pageSize = 15)
        {
            IndentNewMaterialManager indentnewMaterialManager = new IndentNewMaterialManager();
            var totalList = indentnewMaterialManager.GetindentcartList();
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
            return PartialView("~/Views/IndentMaterial/Partial/IndentMaterialGrid.cshtml");                     
        }

        [HttpGet]
        public ActionResult IndentMaterialDetails(int? indentheaderid)
        {
            IndentMaterialModel model = new IndentMaterialModel();
            IndentNewMaterialManager indentnewMaterialManager = new IndentNewMaterialManager();

            if (indentheaderid == null)
            {  
                model.IndentDate = DateTime.Today;
                int? intentno = indentnewMaterialManager.GetNextIndentNumberFromDatabase();
                if (intentno == 0 || intentno ==null)
                {
                    model.IndentNumber = 1;
                }
                else
                {
                    model.IndentNumber = intentno;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveIndentCart(IndentMaterialModel model)
        {
            try
            {
                IndentCart indentcart = new IndentCart();
                string status = "";
                IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
                ProductManager productManager = new ProductManager();
                product product = new product();
                product = productManager.GetId(model.MaterialId);

                if (model.IndentCartId == 0)
                {
                    indentcart.IndentCartId = model.IndentCartId;
                    indentcart.MaterialNameId = model.MaterialId;
                    indentcart.StoreNameId = product.StoreId;
                    indentcart.TaxNameId = product.TaxMasterId;
                    indentcart.UomNameId = product.UomMasterId;
                    indentcart.Price = model.Price;
                    indentcart.IndentRequiredQty = model.IndentRequiredQty;
                    indentcart.RequiredQty = model.RequiredQty;
                    indentcart.Status = model.cartStatus;
                    indentcart.IndentNumber = model.IndentNumber;
                    indentcart.IndentDate = model.IndentDate;
                    indentcart.IsActive = true;
                    indentNewMaterialManager.Postindentcart(indentcart);
                    status = "Inserted";
                }
                else
                {
                    indentcart.IndentCartId = model.IndentCartId;
                    indentcart.MaterialNameId = model.MaterialId;
                    indentcart.StoreNameId = product.StoreId;
                    indentcart.TaxNameId = product.TaxMasterId;
                    indentcart.UomNameId = product.UomMasterId;
                    indentcart.Price = model.Price;
                    indentcart.IndentRequiredQty = model.IndentRequiredQty;
                    indentcart.RequiredQty = model.RequiredQty;
                    indentcart.Status = model.cartStatus;
                    indentcart.IndentNumber = model.IndentNumber;
                    indentcart.IndentDate = model.IndentDate;
                    indentcart.IsActive = true;
                    indentNewMaterialManager.PutIndentcart(indentcart);
                    status = "Updated";
                }
                List<IndentMaterialModel> updatedIndents = new List<IndentMaterialModel>();
                updatedIndents = indentNewMaterialManager.GetindentcartLists()
                                .Where(i => i.IndentNumber == model.IndentNumber)
                                .Select(i => new IndentMaterialModel
                                {
                                    MaterialNameId = i.ProductName,
                                    StoreNameId = i.StoreName,
                                    TaxNameId = i.TaxName,
                                    UomNameId = i.UomName,
                                    IndentRequiredQty = i.indentrequiredqty,
                                    Price = i.Price,
                                    RequiredQty = i.RequiredQty
                                })
                                .ToList();
            
                return Json(new { Success = true,message =status, Indents = updatedIndents });
            }
            catch (Exception ex)
            {
                string status = "";
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveConfirmIndent(IndentMaterialModel model)
        {
            var status = "";
            IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
            var productlist = indentNewMaterialManager.GetIndentCartsList(model.IndentNumber);
            //IndentCart indentCart = indentNewMaterialManager.Getindentcard(model.IndentCartId);
            var count = productlist.Count;
            if (count <= 0)
            {
                status = "Not Existed";
                return Json(new { message = status }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Indentheader indentheader = new Indentheader();
                indentheader.IndentDate = model.IndentDate;
                indentheader.IndentNo = model.IndentNumber;
                indentheader.IsActive = true;
                indentNewMaterialManager.Postindentheader(indentheader);

                foreach (var i in productlist)
                {
                    Indentdetail indentdetail = new Indentdetail();
                    indentdetail.IndentHeaderId = indentheader.IndentHeaderId;
                    indentdetail.IndentDate = i.IndentDate;
                    indentdetail.StoreCode = i.StoreNameId;
                    indentdetail.MaterialId = i.MaterialNameId;
                    indentdetail.UomId = i.UomNameId;
                    indentdetail.TaxId = i.TaxNameId;
                    indentdetail.UnitPrice = i.Price;
                    indentdetail.IndentQty = i.IndentRequiredQty;
                    indentdetail.Quantity = i.RequiredQty;
                    indentdetail.IndentNo = i.IndentNumber;
                    indentdetail.IsActive = i.IsActive;
                    indentNewMaterialManager.Postindentdetail(indentdetail);
                    indentNewMaterialManager.UpdateTempIndent(i.MaterialNameId, i.IndentRequiredQty);

                }
                status = "Inserted";
                return Json(new { Success = true,message = status }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetMaterialForIndent(int productid)
        {
            ProductManager productManager = new ProductManager();
            product product = productManager.GetId(productid);
            decimal? price = 0;
            decimal? minqty = 0;
            decimal? maxqty = 0;
            string materialname = null;
            if (product != null && product.ProductType == 3 || product.ProductType ==2) // Check if product type is 3
            {
                if (product != null)
                {
                    price = product.Price;
                    minqty = product.MinStock;
                    maxqty = product.MaxStock;
                    materialname = product.ProductName;
                }
                UOMManager uOMManager = new UOMManager();
                UomMaster uomname = uOMManager.GetUomMasterId(product.UomMasterId);
                string uom = null;
                if (uomname != null)
                {
                    uom = uomname.ShortUnitName;
                }
                TaxTypeManager taxTypeManager = new TaxTypeManager();
                TaxTypeMaster taxTypeMaster = taxTypeManager.GetTaxMasterId(product.TaxMasterId);
                string tax = null;
                if (taxTypeMaster != null)
                {
                    tax = taxTypeMaster.TaxCode;
                }
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                StoreMaster storeMaster = storeMasterManager.GetStoreMasterId(product.StoreId);
                string store = null;
                if (storeMaster != null)
                {
                    store = storeMaster.StoreName;
                }
                Temp_salesorderManager temp_SalesorderManager = new Temp_salesorderManager();
                List<Temp_Indent> temp_Indent = temp_SalesorderManager.GetStockRequiredForMaterials(productid);
                decimal? totalstockrequired = temp_Indent.Sum(t => t.stockRequired);
                return Json(new
                {
                    Price = price,
                    MinQty = minqty,
                    MaxQty = maxqty,
                    Uom = uom,
                    Tax = tax,
                    Store = store,
                    MaterialName = materialname,
                    Indentstock = totalstockrequired,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndentDelete(int indentcartid)
        {
            Indentdetail indentCart = new Indentdetail();
            string status = "";
            IndentNewMaterialManager indentnewMaterialManager = new IndentNewMaterialManager();
            indentCart = indentnewMaterialManager.Getindentcartid(indentcartid);
            if (indentCart.IndentDetailId == indentcartid)
            {
                status = "Success";
                indentnewMaterialManager.Delete(indentcartid);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }  
        public ActionResult IndentDeletechange(int indentcartid)
        {
            Indentdetail indentCart = new Indentdetail();
            string status = "";
            IndentNewMaterialManager indentnewMaterialManager = new IndentNewMaterialManager();
            indentCart = indentnewMaterialManager.Getindentcartid(indentcartid);
            if (indentCart.IndentDetailId == indentcartid)
            {
                status = "Success";
                indentnewMaterialManager.Deletechange(indentcartid);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IndentSearch(string filter)
        {
            try
            {
                IndentNewMaterialManager indentnewMaterialManager = new IndentNewMaterialManager();
                var totalList = indentnewMaterialManager.GetindentcartList();
                List<get_indent> indentCartsps = new List<get_indent>();
                indentCartsps = totalList.Where(x => x.ProductName.ToLower().Contains(filter.ToLower())).ToList();
                return Json(indentCartsps, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
