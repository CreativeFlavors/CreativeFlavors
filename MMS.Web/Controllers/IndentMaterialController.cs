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

namespace MMS.Web.Controllers
{
    public class IndentMaterialController : Controller
    {
        public ActionResult IndentMaterialMaster()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IndentMaterialGrid()
        {
           

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
                int intentno = indentnewMaterialManager.GetNextIndentNumberFromDatabase();
                model.IndentNo = intentno;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveIndentHeader(IndentMaterialModel model)
        {
            try
            {
                Indentheader indentheader = new Indentheader();
                string status = "";
                IndentNewMaterialManager indentNewMaterialManager = new IndentNewMaterialManager();
                //indentheader = indentNewMaterialManager.Getproductionid(model.ProductionId);
                //ProductManager productManager = new ProductManager();
                //product products = new product();
                //products = productManager.GetId(model.ProductId);

                if (model.IndentHeaderId == 0)
                {
                    indentheader.IndentHeaderId = model.IndentHeaderId;
                    indentheader.IndentDate = model.IndentDate;
                    indentheader.StoreCode = model.StoreCode;
                    indentheader.SupplierCode = model.SupplierCode;
                    indentheader.PoRefNumber = model.PoRefNumber;
                    indentheader.Items = model.Items;
                    indentheader.Quantity = model.Quantity;
                    indentheader.ExtendedValue = model.ExtendedValue;
                    indentheader.DiscountValue = model.DiscountValue;
                    indentheader.SubtotalValue = model.SubtotalValue;
                    indentheader.TaxValue = model.TaxValue;
                    indentheader.TotalValue = model.TotalValue;
                    indentheader.ExpDeliveryDate = model.ExpDeliveryDate;
                    indentheader.PayByDate = model.PayByDate;
                    indentheader.FulfillDate = model.FulfillDate;
                    indentheader.Status = model.Status;
                    indentheader.IndentNo = model.IndentNo;
                    indentheader.IndentQty = model.IndentQty;
                    indentheader.IsActive = true;
                    indentNewMaterialManager.Postindentheader(indentheader);
                    status = "Inserted";
                }
                else
                {
                    indentheader.IndentHeaderId = model.IndentHeaderId;
                    indentheader.IndentDate = model.IndentDate;
                    indentheader.StoreCode = model.StoreCode;
                    indentheader.SupplierCode = model.SupplierCode;
                    indentheader.PoRefNumber = model.PoRefNumber;
                    indentheader.Items = model.Items;
                    indentheader.Quantity = model.Quantity;
                    indentheader.ExtendedValue = model.ExtendedValue;
                    indentheader.DiscountValue = model.DiscountValue;
                    indentheader.SubtotalValue = model.SubtotalValue;
                    indentheader.TaxValue = model.TaxValue;
                    indentheader.TotalValue = model.TotalValue;
                    indentheader.ExpDeliveryDate = model.ExpDeliveryDate;
                    indentheader.PayByDate = model.PayByDate;
                    indentheader.FulfillDate = model.FulfillDate;
                    indentheader.Status = model.Status;
                    indentheader.IndentNo = model.IndentNo;
                    indentheader.IndentQty = model.IndentQty;
                    indentheader.IsActive = true;
                    indentNewMaterialManager.PutIndentheader(indentheader);
                    status = "Updated";
                }
                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string status = "";
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetMaterialForIndent(int materialid)
        {
            MaterialNewManager materialNewManager = new MaterialNewManager();
            Material material = materialNewManager.GetId(materialid);
            decimal? price = 0;
            string materialname = null;
            if (material != null)
            {
                price = material.Price;
                materialname=material.MaterialName;
            }
            UOMManager uOMManager = new UOMManager();
            UomMaster uomname = uOMManager.GetUomMasterId(material.UomMasterId);
            string uom = null;
            if (uomname != null)
            {
                uom = uomname.ShortUnitName;
            }
            TaxTypeManager taxTypeManager = new TaxTypeManager();
            TaxTypeMaster taxTypeMaster = taxTypeManager.GetTaxMasterId(materialid);
            string tax = null;
            if(taxTypeMaster != null)
            {
                tax = taxTypeMaster.TaxCode;
            }
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = storeMasterManager.GetStoreMasterId(materialid);
            string store = null;
            if(storeMaster != null)
            {
                store = storeMaster.StoreName;
            }
            
            return Json(new
            {
                Price = price,
                Uom = uom,
                Tax = tax,
                Store = store,
                MaterialName = materialname,
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
