using Excel;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.Addressdetails;
using MMS.Web.Models.CustomerTransaction;
using MMS.Web.Models.Material;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
    public class BillOfMaterialController : Controller
    {

        #region Helper Method
        public ActionResult BOMMaterialListGrid(int page = 1, int pageSize = 15)
        {
            List<ParentBillofMaterial> totalList = new List<ParentBillofMaterial>();

            ParentbomManager ParentbomManager = new ParentbomManager();
            var data = ParentbomManager.Get();
            foreach (var i in data)
            {
                ParentBillofMaterial parentBillofMaterial = new ParentBillofMaterial();
                parentBillofMaterial.Bomid = i.BomId;
                parentBillofMaterial.Bomno = i.BomNo;
                parentBillofMaterial.Description = i.Description;
                parentBillofMaterial.Date = i.Date;
                parentBillofMaterial.isdeleted = i.IsDelete;
                totalList.Add(parentBillofMaterial);
            }
            var totalCount = totalList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(d => d.Bomid)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();
            ViewBag.TotalPage = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return View("~/Views/Stock/BillOfMaterial/BOMMaterialListGrid.cshtml", totalList);
        }
        public ActionResult BOMSearch(string filter)
        {
            List<ParentBillofMaterial> ParentBillofMaterial = new List<ParentBillofMaterial>();
            ParentbomManager ParentbomManager = new ParentbomManager();
            var data = ParentbomManager.Get();
            var list = data.Where(x => x.BomNo.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            var detailslist = list.OrderByDescending(m =>m.BomId);
            foreach (var i in detailslist)
            {
                ParentBillofMaterial parentBillofMaterial = new ParentBillofMaterial();
                parentBillofMaterial.Bomid = i.BomId;
                parentBillofMaterial.Bomno = i.BomNo;
                parentBillofMaterial.Description = i.Description;
                parentBillofMaterial.Date = i.Date;
                parentBillofMaterial.isdeleted = i.IsDelete;
                ParentBillofMaterial.Add(parentBillofMaterial);
            }


            return Json(ParentBillofMaterial, JsonRequestBehavior.AllowGet);
        }
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
        public ActionResult BOMmaterialSearch(string filter, int bomid)
        {
            Parentbom_materialManager parentbom_MaterialManager = new Parentbom_materialManager();
            var datas = parentbom_MaterialManager.GetBomList(bomid);

            var namelist = datas.Where(x => x.productname != null && x.productname.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            return Json(namelist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BOMsubassemblySearch(string filter, int bomid)
        {
            subassemblyManager subassemblyManager = new subassemblyManager();
            var datas = subassemblyManager.GetBomList(bomid);

            var namelist = datas.Where(x => x.productname != null && x.productname.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            return Json(namelist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BillOfMaterialDetails()
        {
            ParentbomManager parentbomManager = new ParentbomManager();
            ParentBillofMaterial model = new ParentBillofMaterial();
            string LastBOmNO = string.Empty;

            LastBOmNO = parentbomManager.GetLastbomNumber();

            if (LastBOmNO == null || LastBOmNO == "")
            {
                model.Lastbom = "NotApplicable";
            }
            model.Date = DateTime.Now;
            model.Lastbom = LastBOmNO;
            return View("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }

        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SubAssemblyDetails(ParentBillofMaterial model)
        {
            if (model.subassemblyid == 0)
            {
                if (model.Bomid == 0)
                {
                    parentbom parentboms = new parentbom();
                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    parentboms.Description = model.Bomno;
                    parentboms.Date = DateTime.Now;
                    parentboms.LastBom = model.Lastbom;
                    string AlertMessage = "";
                    var totallist = manager.Get();
                    var bomList = totallist.Where(x => x.BomNo.ToLower().Contains(model.Bomno.ToLower())).ToList();
                    if (bomList.Count() == 0 && model.Bomid == 0)
                    {
                        parentboms = manager.Post(parentboms);
                        AlertMessage = "Saved Successfully";
                    }
                    else if (bomList.Count() != 0)
                    {
                        AlertMessage = "Already Existed";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }

                    subassembly subassembly = new subassembly();
                    subassemblyManager subassemblyManager = new subassemblyManager();
                    subassembly.BomId = parentboms.BomId;
                    subassembly.ProductId = model.ProductSUBid;
                    subassembly.RequiredQty = model.Requiredqty;
                    var bOMMaterial = subassemblyManager.Post(subassembly);
                    return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, bomids = parentboms.BomId }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    parentbom parentboms = new parentbom();
                    parentbom parentbomsexists = new parentbom();
                    subassemblyManager subassemblyManager = new subassemblyManager();
                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    parentboms.Description = model.Bomno;
                    parentboms.Date = DateTime.Now;
                    parentboms.LastBom = model.Lastbom;
                    string AlertMessage = "";
                    var totallist1 = subassemblyManager.Get();
                    var productListcode = totallist1.Where(x => x.ProductId.ToString().ToLower().Contains(model.ProductSUBid.ToString().ToLower()) && x.BomId.ToString().ToLower().Contains(model.Bomid.ToString().ToLower())).ToList();
                    if (productListcode.Count() != 0 && model.Bomid != 0)
                    {
                        AlertMessage = "Already Existed SUB";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }
                    subassembly subassembly = new subassembly();
                    subassembly.BomId = model.Bomid;
                    subassembly.ProductId = model.ProductSUBid;
                    subassembly.RequiredQty = model.Requiredqty;
                    var bOMMaterial = subassemblyManager.Post(subassembly);
                    AlertMessage = "Updated Successfully";
                    return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, update = model.Bomid }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                parentbom parentboms = new parentbom();
                parentbom parentbomsexists = new parentbom();
                subassemblyManager subassemblyManager = new subassemblyManager();

                ParentbomManager manager = new ParentbomManager();
                parentboms.BomNo = model.Bomno;
                parentboms.Description = model.Bomno;
                parentboms.Date = model.Date;
                parentboms.LastBom = model.Lastbom;
                string AlertMessage = "";
                var totallist1 = subassemblyManager.Get();
                var productListcode = totallist1.Where(x => x.ProductId.ToString().ToLower().Contains(model.ProductSUBid.ToString().ToLower()) && x.BomId.ToString().ToLower().Contains(model.Bomid.ToString().ToLower())).ToList();
                if (productListcode.Count() != 0 && model.Bomid != 0)
                {
                    AlertMessage = "Already Existed SUB";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
                subassembly subassembly = new subassembly();
                subassembly.BomId = model.Bomid;
                subassembly.ProductId = model.ProductSUBid;
                subassembly.RequiredQty = model.Requiredqty;
                subassembly.SubassemblyId = model.subassemblyid;
                var bOMMaterial = subassemblyManager.Put(subassembly);
                AlertMessage = "Updated Successfully";
                return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, update = model.Bomid }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult BillOfMaterialDetails(ParentBillofMaterial model)
        {
            if (model.Bommaterialid == 0)
            {
                if (model.Bomid == 0)
                {
                    parentbom parentboms = new parentbom();
                    parentbom parentbomsexists = new parentbom();
                    Parentbom_materialManager parentbom_MaterialManager = new Parentbom_materialManager();
                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    parentboms.Description = model.Bomno;
                    parentboms.Date =DateTime.Now;
                    parentboms.LastBom = model.Lastbom;
                    string AlertMessage = "";
                    var totallist = manager.Get();
                    var bomList = totallist.Where(x => x.BomNo.ToLower().Contains(model.Bomno.ToLower())).ToList();
                    if (bomList.Count() == 0 && model.Bomid == 0)
                    {
                        parentboms = manager.Post(parentboms);
                        AlertMessage = "Saved Successfully";
                    }
                    else if (bomList.Count() != 0 && model.Bomid == 0)
                    {
                        AlertMessage = "Already Existed";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }


                    parentbom_material parentbom_Material = new parentbom_material();
                    Parentbom_materialManager parentbom_materialManager = new Parentbom_materialManager();

                    parentbom_Material.BomID = parentboms.BomId;
                    parentbom_Material.ProductId = model.Productid;
                    parentbom_Material.UomId = model.Uomid;
                    parentbom_Material.RequiredQty = model.Requiredqty;
                    var bOMMaterial = parentbom_materialManager.Post(parentbom_Material);
                    return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, bomids = parentboms.BomId }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    parentbom parentboms = new parentbom();
                    parentbom parentbomsexists = new parentbom();
                    Parentbom_materialManager parentbom_materialManager = new Parentbom_materialManager();
                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    string AlertMessage = "";
                    var totallist1 = parentbom_materialManager.Get();
                    var productListcode = totallist1.Where(x => x.ProductId.ToString().ToLower().Contains(model.Productid.ToString().ToLower()) && x.BomID.ToString().ToLower().Contains(model.Bomid.ToString().ToLower())).ToList();
                    if (productListcode.Count() != 0 && model.Bomid != 0)
                    {
                        AlertMessage = "Already Existed Product";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }
                    parentbom_material parentbom_Material = new parentbom_material();
                    parentbom_Material.BomID = model.Bomid;
                    parentbom_Material.ProductId = model.Productid;
                    parentbom_Material.UomId = model.Uomid;
                    parentbom_Material.RequiredQty = model.Requiredqty;
                    var bOMMaterial = parentbom_materialManager.Post(parentbom_Material);
                    AlertMessage = "Updated Successfully";
                    return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, update = model.Bomid }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                parentbom parentboms = new parentbom();
                parentbom parentbomsexists = new parentbom();
                Parentbom_materialManager parentbom_materialManager1 = new Parentbom_materialManager();
                ParentbomManager manager = new ParentbomManager();
                parentboms.BomNo = model.Bomno;
                parentboms.Description = model.Bomno;
                string AlertMessage = "";
                var totallist1 = parentbom_materialManager1.Get();
                var productListcode = totallist1.Where(x => x.ProductId.ToString().ToLower().Contains(model.Productid.ToString().ToLower()) && x.BomID.ToString().ToLower().Contains(model.Bomid.ToString().ToLower())).ToList();
                if (productListcode.Count() != 0 && model.Bomid != 0)
                {
                    AlertMessage = "Already Existed Product";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
                parentbom_material parentbom_Material = new parentbom_material();
                Parentbom_materialManager parentbom_materialManager = new Parentbom_materialManager();
                parentbom_Material.BomMaterialId = model.Bommaterialid;
                parentbom_Material.BomID = model.Bomid;
                parentbom_Material.ProductId = model.Productid;
                parentbom_Material.UomId = model.Uomid;
                parentbom_Material.RequiredQty = model.Requiredqty;
                var bOMMaterial = parentbom_materialManager.Put(parentbom_Material);
                AlertMessage = "Updated Successfully";
                return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, update = model.Bomid }, JsonRequestBehavior.AllowGet);

            }

        }
        public ActionResult EditBOMDetails(int BOMID)
        {
            ParentbomManager manager = new ParentbomManager();
            parentbom parentboms = new parentbom();
            Parentbom_materialManager parentbom_MaterialManager = new Parentbom_materialManager();
            ParentBillofMaterial model = new ParentBillofMaterial();
            subassemblyManager subassemblyManager = new subassemblyManager();
            parentboms = manager.Getbomid(BOMID);
            var datas = parentbom_MaterialManager.GetBomList(BOMID);
            var sunassembly = subassemblyManager.GetBomList(BOMID);
            if (parentboms != null)
            {
                model.Bomid = parentboms.BomId;
                model.Bomno = parentboms.BomNo;
                model.Date = parentboms.Date;
                model.Description = parentboms.Description;
                model.Lastbom = parentboms.LastBom;
                model.bomMaterialGridList = datas;
                model.bomsubassemblyGridList = sunassembly;

            }
            return View("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }
        [HttpGet]
        public ActionResult EditBOMmaterialDetails(int BOMID, int BOMMaterialID)
        {
            ParentbomManager manager = new ParentbomManager();
            parentbom parentboms = new parentbom();
            Parentbom_materialManager parentbom_MaterialManager = new Parentbom_materialManager();
            ParentBillofMaterial model = new ParentBillofMaterial();
            ProductManager productManager = new ProductManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            subassemblyManager subassemblyManager = new subassemblyManager();
            UOMManager uomManager = new UOMManager();

            parentboms = manager.Getbomid(BOMID);
            var datas = parentbom_MaterialManager.GetBomList(BOMID);
            var subassembly = subassemblyManager.GetBomList(BOMID);
            var material = parentbom_MaterialManager.Getbommaterialid(BOMMaterialID);
            var namematerial = materialNameManager.GetMaterialNameMaterial(material.MaterialMasterId);

            if (parentboms != null)
            {
                model.Bomid = parentboms.BomId;
                model.Bomno = parentboms.BomNo;
                model.Date = parentboms.Date;
                model.Description = parentboms.Description;
                model.Lastbom = parentboms.LastBom;
                model.Productid = material.ProductId;
                model.Uomid = material.UomId;
                model.Requiredqty = material.RequiredQty;
                model.Bommaterialid = material.BomMaterialId;
                model.bomMaterialGridList = datas;
                model.bomsubassemblyGridList = subassembly;

            }
            return PartialView("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }
        [HttpGet]
        public ActionResult EditSubassemblyDetails(int BOMID, int BOMsubassemblyID)
        {
            ParentbomManager manager = new ParentbomManager();
            parentbom parentboms = new parentbom();
            Parentbom_materialManager parentbom_MaterialManager = new Parentbom_materialManager();
            ParentBillofMaterial model = new ParentBillofMaterial();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            subassemblyManager subassemblyManager = new subassemblyManager();

            parentboms = manager.Getbomid(BOMID);
            var datas = parentbom_MaterialManager.GetBomList(BOMID);
            var subassembly = subassemblyManager.GetBomList(BOMID);
            var material = subassemblyManager.Getbommaterialid(BOMsubassemblyID);

            if (parentboms != null)
            {
                model.Bomid = parentboms.BomId;
                model.Bomno = parentboms.BomNo;
                model.Date = parentboms.Date;
                model.Description = parentboms.Description;
                model.Lastbom = parentboms.LastBom;
                model.ProductSUBid = material.ProductId;
                model.subRequiredqty = material.RequiredQty;
                model.subassemblyid = material.SubassemblyId;
                model.bomMaterialGridList = datas;
                model.bomsubassemblyGridList = subassembly;

            }
            return PartialView("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }
        [HttpPost]
        public ActionResult BOMGridDelete(int BomId, bool IsChecked)
        {
            ParentbomManager ParentbomManager = new ParentbomManager();
            string status = "";
            parentbom parentbom = new parentbom();
            parentbom = ParentbomManager.Getbomid(BomId);
            if (parentbom != null)
            {
                status = "Success";
                ParentbomManager.Delete(parentbom.BomId, IsChecked);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BOMMaterialDelete(int BOMMaterialID)
        {
            Parentbom_materialManager ParentbomManager = new Parentbom_materialManager();
            string status = "";
            parentbom_material parentbom = new parentbom_material();
            parentbom = ParentbomManager.Getbommaterialid(BOMMaterialID);
            if (parentbom != null)
            {
                status = "Success";
                ParentbomManager.Delete(parentbom.BomMaterialId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BOMsubassemblyDelete(int BOMsubassemblyid)
        {
            subassemblyManager subassemblyManager = new subassemblyManager();
            string status = "";
            subassembly subassembly = new subassembly();
            subassembly = subassemblyManager.Getsubassemblyid(BOMsubassemblyid);
            if (subassembly != null)
            {
                status = "Success";
                subassemblyManager.Delete(subassembly.SubassemblyId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckBomNo(string BomNo)
        {
            ParentbomManager ParentbomManager = new ParentbomManager();
            parentbom parentbom = new parentbom();
            parentbom = ParentbomManager.GetBomNO(BomNo.Trim());
            Product_BuyerStyleManager productStyleManager = new Product_BuyerStyleManager();
            string Message = "";
            if (parentbom != null)
            {
                Message = "Already Existed";
            }
            else
            {
                Message = "Success";
            }
            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}
