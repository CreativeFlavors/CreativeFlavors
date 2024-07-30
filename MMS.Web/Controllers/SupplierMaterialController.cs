using iTextSharp.text;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.FinishedGoods;
using MMS.Web.Models.SupplierMaterialModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMS.Web.Controllers
{
    public class SupplierMaterialController : Controller
    {
        #region view
        public ActionResult SupplierMaster()
        {
            return View();
        } 
        public ActionResult SupplierGrid(int page = 1, int pageSize = 15)
        {
            SupplierMaterialManager SupplierMaterialManager = new SupplierMaterialManager();
            var totalist = SupplierMaterialManager.SupplierMaterialgrid();
            List<SupplierMaterialModel> totaldata = new List<SupplierMaterialModel>();
            foreach (var i in totalist)
            {
                SupplierMaterialModel model = new SupplierMaterialModel();
                model.ProductName = i.ProductName;
                model.Productcode = i.Productcode;
                model.suppliername = i.suppliername;
                model.categoryname = i.category;
                model.tax = i.tax;
                model.uom = i.uom;
                model.SupplierMaterialId = i.supmaterialid;
                totaldata.Add(model);
            }
            var totalCount = totaldata.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totaldata = totaldata.OrderByDescending(d => d.ProductName)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return PartialView("~/Views/SupplierMaterial/Partial/SupplierGrid.cshtml", totaldata);
        }
        public ActionResult SupplierMaterial_Details()
        {
            return PartialView("~/Views/SupplierMaterial/Partial/SupplierMaterial_Details.cshtml");
        }
        [HttpPost]
        public ActionResult EditsupplierDetails(int SupplierMaterialId)
        {
            SupplierMaterialManager SupplierMaterialManager = new SupplierMaterialManager();
            SupplierMaterial SupplierMaterial = new SupplierMaterial();
            SupplierMaterialModel model = new SupplierMaterialModel();
            SupplierMaterial = SupplierMaterialManager.GetCustAddressId(SupplierMaterialId);
            if (SupplierMaterial != null)
            {
                model.SupplierId = SupplierMaterial.SupplierId;
                model.ProductId = SupplierMaterial.ProductId;
                model.UomId = SupplierMaterial.UomId;
                model.TaxId = SupplierMaterial.TaxId;
                model.category = SupplierMaterial.Categoryid;
                model.SupplierMaterialId = SupplierMaterial.SupplierMaterialId;
            }
            return PartialView("~/Views/SupplierMaterial/Partial/SupplierMaterial_Details.cshtml",model);
        }
        #endregion
        #region curd operation
        [HttpPost]
        public ActionResult SupplierMaterialInsert(SupplierMaterialModel model)
        {
            SupplierMaterial SupplierMaterial = new SupplierMaterial();
            SupplierMaterialManager SupplierMaterialManager = new SupplierMaterialManager();
            SupplierMaterial.SupplierId = model.SupplierId;
            SupplierMaterial.ProductId = model.ProductId;
            SupplierMaterial.UomId = model.UomId;
            SupplierMaterial.TaxId = model.TaxId;
            SupplierMaterial.IsActive = true;
            SupplierMaterial.Categoryid = model.category;
            SupplierMaterialManager.Post(SupplierMaterial);
            return Json(SupplierMaterial, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SupplierMaterialModel model)
        {
            SupplierMaterial SupplierMaterial = new SupplierMaterial();

            SupplierMaterialManager SupplierMaterialManager = new SupplierMaterialManager();
            SupplierMaterial = SupplierMaterialManager.GetCustAddressId(model.SupplierMaterialId);
            if (model != null)
            {
                SupplierMaterial.SupplierId = model.SupplierId;
                SupplierMaterial.ProductId = model.ProductId;
                SupplierMaterial.Categoryid = model.category;
                SupplierMaterial.UomId = model.UomId;
                SupplierMaterial.TaxId = model.TaxId;
                SupplierMaterial.IsActive = true;
                SupplierMaterialManager.Put(SupplierMaterial);
            }
            else
            {
                return Json(SupplierMaterial, JsonRequestBehavior.AllowGet);
            }

            return Json(SupplierMaterial, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int SupplierMaterialId)
        {
            SupplierMaterial SupplierMaterial = new SupplierMaterial();
            string status = "";
            SupplierMaterialManager SupplierMaterialManager = new SupplierMaterialManager();
            SupplierMaterial = SupplierMaterialManager.GetCustAddressId(SupplierMaterialId);
            if (SupplierMaterialId != 0)
            {
                status = "Success";
                SupplierMaterialManager.Delete(SupplierMaterial.SupplierMaterialId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region filter
        public ActionResult FillUOMBasedonproductName(int productid)
        {

            List<product> materialNameMasterList = new List<product>();
            ProductManager ProductManager = new ProductManager();

            MaterialManager materialManager = new MaterialManager();
            UOMManager uomManager = new UOMManager();
            var items = (from x in ProductManager.Get()
                         join z in uomManager.Get()
                         on x.UomMasterId equals z.UomMasterId
                         where x.ProductId == productid
                         select new
                         {
                             UomName = z.LongUnitName,
                             uomid = z.UomMasterId,
                         });

            var distinctList = items.GroupBy(x => x.UomName).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Search(string filter)
        {
            List<SupplierMaterialModel> totaldata = new List<SupplierMaterialModel>();
            SupplierMaterialManager SupplierMaterialManager = new SupplierMaterialManager();
            var totalist = SupplierMaterialManager.SupplierMaterialgrid();
            var list = totalist.Where(x => x.suppliername.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();

            foreach (var i in list)
            {
                SupplierMaterialModel model = new SupplierMaterialModel();
                model.ProductName = i.ProductName;
                model.Productcode = i.Productcode;
                model.suppliername = i.suppliername;
                model.categoryname = i.category;
                model.tax = i.tax;
                model.uom = i.uom;
                model.SupplierMaterialId = i.supmaterialid;
                totaldata.Add(model);
            }
            return Json(totaldata, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
