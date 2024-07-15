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
            ViewBag.TotalPages = totalPages;
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
            var detailslist = list;
            foreach (var i in detailslist)
            {
                ParentBillofMaterial parentBillofMaterial = new ParentBillofMaterial();
                parentBillofMaterial.Bomid = i.BomId;
                parentBillofMaterial.Bomno = i.BomNo;
                parentBillofMaterial.Description = i.Description;
                parentBillofMaterial.Date = i.Date;
                ParentBillofMaterial.Add(parentBillofMaterial);
            }


            return Json(ParentBillofMaterial, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BOMmaterialSearch(string filter, int bomid)
        {
            Parentbom_materialManager parentbom_MaterialManager = new Parentbom_materialManager();
            var datas = parentbom_MaterialManager.GetBomList(bomid);

            var namelist = datas.Where(x => x.MaterialNames != null && x.MaterialNames.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
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
                    parentbom parentbomsexists = new parentbom();

                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    parentboms.Description = model.Description;
                    parentboms.Date = model.Date;
                    parentboms.LastBom = model.Lastbom;
                    string AlertMessage = "";
                    parentbomsexists = manager.GetBomNO(model.Bomno);
                    if (parentbomsexists == null && model.Bomid == 0)
                    {
                        parentboms = manager.Post(parentboms);
                        AlertMessage = "Saved Successfully";
                    }
                    else if (parentbomsexists != null && parentboms.BomId == 0)
                    {
                        AlertMessage = "Already Existed";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }

                    subassembly subassembly = new subassembly();
                    subassemblyManager subassemblyManager = new subassemblyManager();
                    subassembly.BomId = parentboms.BomId;
                    subassembly.ProductId = model.Productid;
                    subassembly.RequiredQty = model.Requiredqty;
                    var bOMMaterial = subassemblyManager.Post(subassembly);
                    return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, bomids = parentboms.BomId }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    parentbom parentboms = new parentbom();
                    parentbom parentbomsexists = new parentbom();

                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    parentboms.Description = model.Description;
                    parentboms.Date = model.Date;
                    parentboms.LastBom = model.Lastbom;
                    string AlertMessage = "";
                    parentbomsexists = manager.GetBomNO(model.Bomno);
                    if (parentbomsexists == null && model.Bomid == 0)
                    {
                        parentboms = manager.Post(parentboms);
                        AlertMessage = "Saved Successfully";
                    }
                    else if (parentbomsexists != null && parentboms.BomId != 0)
                    {
                        AlertMessage = "Already Existed";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }
                    subassembly subassembly = new subassembly();
                    subassemblyManager subassemblyManager = new subassemblyManager();
                    subassembly.BomId = model.Bomid;
                    subassembly.ProductId = model.Productid;
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

                ParentbomManager manager = new ParentbomManager();
                parentboms.BomNo = model.Bomno;
                parentboms.Description = model.Description;
                parentboms.Date = model.Date;
                parentboms.LastBom = model.Lastbom;
                string AlertMessage = "";
                parentbomsexists = manager.GetBomNO(model.Bomno);
                if (parentbomsexists == null && model.Bomid == 0)
                {
                    parentboms = manager.Post(parentboms);
                    AlertMessage = "Saved Successfully";
                }
                else if (parentbomsexists != null && parentboms.BomId != 0)
                {
                    AlertMessage = "Already Existed";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }
                subassembly subassembly = new subassembly();
                subassemblyManager subassemblyManager = new subassemblyManager();
                subassembly.BomId = model.Bomid;
                subassembly.ProductId = model.Productid;
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

                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    parentboms.Description = model.Description;
                    parentboms.Date = model.Date;
                    parentboms.LastBom = model.Lastbom;
                    string AlertMessage = "";
                    parentbomsexists = manager.GetBomNO(model.Bomno);
                    if (parentbomsexists == null && model.Bomid == 0)
                    {
                        parentboms = manager.Post(parentboms);
                        AlertMessage = "Saved Successfully";
                    }
                    else if (parentbomsexists != null && parentboms.BomId == 0)
                    {
                        AlertMessage = "Already Existed";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }


                    parentbom_material parentbom_Material = new parentbom_material();
                    Parentbom_materialManager parentbom_materialManager = new Parentbom_materialManager();

                    parentbom_Material.BomID = parentboms.BomId;
                    parentbom_Material.ProductId = model.Productid;
                    parentbom_Material.MaterialCategory = model.MaterialCategoryid;
                    parentbom_Material.MaterialGroupId = model.MaterialGroupid;
                    parentbom_Material.MaterialMasterId = model.MaterialMasterid;
                    parentbom_Material.UomId = model.Uomid;
                    parentbom_Material.RequiredQty = model.Requiredqty;
                    var bOMMaterial = parentbom_materialManager.Post(parentbom_Material);
                    return Json(new { bomid = parentboms.BomId, AlertMessage = AlertMessage, bomids = parentboms.BomId }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    parentbom parentboms = new parentbom();
                    parentbom parentbomsexists = new parentbom();

                    ParentbomManager manager = new ParentbomManager();
                    parentboms.BomNo = model.Bomno;
                    parentboms.Description = model.Description;
                    parentboms.Date = model.Date;
                    parentboms.LastBom = model.Lastbom;
                    string AlertMessage = "";
                    parentbomsexists = manager.GetBomNO(model.Bomno);
                    if (parentbomsexists == null && model.Bomid == 0)
                    {
                        parentboms = manager.Post(parentboms);
                        AlertMessage = "Saved Successfully";
                    }
                    else if (parentbomsexists != null && parentboms.BomId != 0)
                    {
                        AlertMessage = "Already Existed";
                        return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                    }


                    parentbom_material parentbom_Material = new parentbom_material();
                    Parentbom_materialManager parentbom_materialManager = new Parentbom_materialManager();

                    parentbom_Material.BomID = model.Bomid;
                    parentbom_Material.ProductId = model.Productid;
                    parentbom_Material.MaterialCategory = model.MaterialCategoryid;
                    parentbom_Material.MaterialGroupId = model.MaterialGroupid;
                    parentbom_Material.MaterialMasterId = model.MaterialMasterid;
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

                ParentbomManager manager = new ParentbomManager();
                parentboms.BomNo = model.Bomno;
                parentboms.Description = model.Description;
                parentboms.Date = model.Date;
                parentboms.LastBom = model.Lastbom;
                string AlertMessage = "";
                parentbomsexists = manager.GetBomNO(model.Bomno);
                if (parentbomsexists != null && parentboms.BomId != 0)
                {
                    parentboms = manager.Post(parentboms);
                    AlertMessage = "Saved Successfully";
                }
                else if (parentbomsexists != null && parentboms.BomId != 0)
                {
                    AlertMessage = "Already Existed";
                    return Json(AlertMessage, JsonRequestBehavior.AllowGet);
                }


                parentbom_material parentbom_Material = new parentbom_material();
                Parentbom_materialManager parentbom_materialManager = new Parentbom_materialManager();
                parentbom_Material.BomMaterialId = model.Bommaterialid;
                parentbom_Material.BomID = model.Bomid;
                parentbom_Material.ProductId = model.Productid;
                parentbom_Material.MaterialCategory = model.MaterialCategoryid;
                parentbom_Material.MaterialGroupId = model.MaterialGroupid;
                parentbom_Material.MaterialMasterId = model.MaterialMasterid;
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
            ProductManager productManager = new ProductManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialGroupManager materialGroupManager = new MaterialGroupManager();
            subassemblyManager subassemblyManager = new subassemblyManager();
            UOMManager uomManager = new UOMManager();

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
                model.MaterialCategoryid = material.MaterialCategory;
                model.MaterialGroupid = material.MaterialGroupId;
                model.MaterialMasterid = namematerial.MaterialMasterID;
                model.Uomid = material.UomId;
                model.Requiredqty = material.RequiredQty;
                model.Bommaterialid = material.BomMaterialId;
                model.bomMaterialGridList = datas;
                model.bomsubassemblyGridList = subassembly;

            }
            return PartialView("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }
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
                model.Productid = material.ProductId;
                model.Requiredqty = material.RequiredQty;
                model.subassemblyid = material.SubassemblyId;
                model.bomMaterialGridList = datas;
                model.bomsubassemblyGridList = subassembly;

            }
            return PartialView("~/Views/Stock/BillOfMaterial/BillOfMaterialDetails.cshtml", model);
        }
        [HttpDelete]
        public ActionResult BOMGridDelete(int BomId)
        {
            ParentbomManager ParentbomManager = new ParentbomManager();
            string status = "";
            parentbom parentbom = new parentbom();
            parentbom = ParentbomManager.Getbomid(BomId);
            if (parentbom != null)
            {
                status = "Success";
                ParentbomManager.Delete(parentbom.BomId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
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
        [HttpDelete]
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
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            Bom billOfMaterial = new Bom();
            billOfMaterial = billOfMaterialManager.GetBomNO(BomNo.Trim());
            Product_BuyerStyleManager productStyleManager = new Product_BuyerStyleManager();
            Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
            productStyleMaster = productStyleManager.GetOurStyle(BomNo.Trim());
            string Message = "";
            if (billOfMaterial != null)
            {
                Message = "Already Existed";
            }
            else
            {
                Message = "Success";
            }
            return Json(new { productStyleMaster = productStyleMaster, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Excel Upload
        public ActionResult RemoveSession()
        {
            @Session["BOMerror_"] = null;
            return View("~/Views/Stock/BuyerOrderEntryForm/BuyerOrderEntryForm.cshtml");
        }
        [HttpPost]

        public ActionResult UploadData(HttpPostedFileBase upload)
        {
            BillOfMaterialModel model = new BillOfMaterialModel();
            string BOMError = "";
            if (ModelState.IsValid)
            {
                DataSet result = null;
                if (upload != null && upload.ContentLength > 0)
                {
                    Stream stream = upload.InputStream;

                    IExcelDataReader reader = null;
                    if (upload.FileName.ToLower().EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        TempData["successBody"] = "This file format is not supported";
                        return RedirectToAction("BOMMaterialListGrid");
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    result = reader.AsDataSet();
                    reader.Close();
                }
                else
                {
                    TempData["successBody"] = "Please Upload Your file";
                    return RedirectToAction("BOMMaterialListGrid");
                }
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoOrderEntryID();
                string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                var table = new DataTable();
                table = result.Tables[0];
                ID = ID++;
                DataTable table_ = new DataTable();
                table_.Columns.Add("BomId", typeof(int));
                table_.Columns.Add("BomNo", typeof(string));
                table_.Columns.Add("Description", typeof(string));
                table_.Columns.Add("BuyerMasterId", typeof(int));
                table_.Columns.Add("BuyerModel", typeof(string));
                table_.Columns.Add("MeanSize", typeof(int));
                table_.Columns.Add("Date", typeof(DateTime));
                table_.Columns.Add("ParentBomNo", typeof(string));
                table_.Columns.Add("LastBomNoEntered", typeof(string));
                table_.Columns.Add("LinkBomNo", typeof(string));
                table_.Columns.Add("Ammendment", typeof(string));
                table_.Columns.Add("CommonBomNo", typeof(string));
                table_.Columns.Add("CommnBOM1", typeof(string));
                table_.Columns.Add("CommnBOM2", typeof(string));
                table_.Columns.Add("CommnBOM3", typeof(string));
                table_.Columns.Add("CommnBOM4", typeof(string));
                table_.Columns.Add("CommnBOM5", typeof(string));
                table_.Columns.Add("PreparedBy", typeof(string));
                table_.Columns.Add("VerifiedBy", typeof(string));
                table_.Columns.Add("CheckedBy", typeof(string));
                table_.Columns.Add("CreatedDate", typeof(DateTime));
                table_.Columns.Add("UpdatedDate", typeof(DateTime));
                table_.Columns.Add("CreatedBy", typeof(string));
                table_.Columns.Add("UpdatedBy", typeof(string));
                table_.Columns.Add("MaterialMasterId", typeof(int));
                table_.Columns.Add("MaterialCategoryMasterId", typeof(int));
                table_.Columns.Add("MaterialGroupMasterId", typeof(int));
                table_.Columns.Add("ColorMasterId", typeof(int));
                table_.Columns.Add("SubstanceMasterId", typeof(int));
                table_.Columns.Add("SampleNorms", typeof(string));
                table_.Columns.Add("Wastage", typeof(decimal));
                table_.Columns.Add("SupplierMasterId", typeof(int));
                table_.Columns.Add("UomMasterId", typeof(int));
                table_.Columns.Add("SizeRangeMasterId", typeof(int));
                table_.Columns.Add("SizeScheduleMasterId", typeof(int));
                table_.Columns.Add("WastageQty", typeof(int));
                table_.Columns.Add("WastageQtyUOM", typeof(int));
                table_.Columns.Add("TotalNorms", typeof(decimal));
                table_.Columns.Add("TotalNormsUOM", typeof(int));
                table_.Columns.Add("ComponentName", typeof(int));
                table_.Columns.Add("NoOfSets", typeof(int));
                table_.Columns.Add("BuyerNorms", typeof(decimal));
                table_.Columns.Add("OurNormsPercent", typeof(decimal));
                table_.Columns.Add("PurchaseNorms", typeof(int));
                table_.Columns.Add("PurchaseNormsPercent", typeof(decimal));
                table_.Columns.Add("IssueNorms", typeof(int));
                table_.Columns.Add("IssueNormsPercent", typeof(decimal));
                table.Columns.Add("CostingNorms", typeof(int));
                table.Columns.Add("CostingNormsPercent", typeof(decimal));
                table_.Columns.Add("OurNorms", typeof(int));
                table_.Columns.Add("DeletedBy", typeof(string));
                table_.Columns.Add("IsDeleted", typeof(bool));
                table_.Columns.Add("DeletedOn", typeof(DateTime));
                table_.Columns.Add("RequirementQuantity", typeof(decimal));
                table_.Columns.Add("IsMRP", typeof(bool));
                List<Bom> listBillOfMaterial = new List<Bom>();
                List<BOMMaterial> listBomMaterial = new List<BOMMaterial>();
                int lotCount = 1;
                int? bomid = null;
                string bomno = "";
                //Check error start
                foreach (DataRow dr in table.Rows)
                {
                    Session["SuccessOrder"] = "Excel data imported Successfully";
                    List<InternalOrderForm> listOrderEntryFormList = new List<InternalOrderForm>();
                    Bom billOfMaterial_ = new Bom();
                    Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
                    Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
                    MaterialManager materialManager = new MaterialManager();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialNameManager materialNameManager = new MaterialNameManager();
                    tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
                    Bom bomDetails = new Bom();
                    BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();

                    if (lotCount == 1)
                    {
                        productStyleMaster = product_BuyerStyleManager.GetOurStyle(dr[1].ToString());
                        bomno = dr[1].ToString();
                        if (productStyleMaster != null && productStyleMaster.ProductOrBuyerStyleId != 0)
                        {
                            Bom isExistBOM = new Bom();
                            isExistBOM = billOfMaterialManager.GetBomNO(dr[1].ToString());
                            if (isExistBOM == null || isExistBOM.BomId == 0)
                            {
                                ID++;
                                if (lotCount == 1)
                                {
                                    billOfMaterial_.BomNo = dr[1].ToString();
                                    billOfMaterial_.Description = dr[1].ToString();

                                    billOfMaterial_.BuyerMasterId = productStyleMaster.BuyerName_ProductGroup;
                                    billOfMaterial_.BuyerModel = productStyleMaster.BuyerModel_ProductType.ToString();
                                    billOfMaterial_.MeanSize = 121;
                                    billOfMaterial_.Date = DateTime.Now;
                                    billOfMaterial_.ParentBomNo = null;
                                    string LastBOmNO = billOfMaterialManager.GetLastbomNumber();
                                    billOfMaterial_.LastBomNoEntered = LastBOmNO;
                                    billOfMaterial_.LinkBomNo = productStyleMaster.ProductOrBuyerStyleId.ToString();
                                    billOfMaterial_.Ammendment = string.Empty;
                                    billOfMaterial_.CommonBomNo = string.Empty;
                                    billOfMaterial_.CommnBOM1 = string.Empty;
                                    billOfMaterial_.CommnBOM2 = string.Empty;
                                    billOfMaterial_.CommnBOM3 = string.Empty;
                                    billOfMaterial_.CommnBOM4 = string.Empty;
                                    billOfMaterial_.CommnBOM5 = string.Empty;
                                    billOfMaterial_.PreparedBy = string.Empty;
                                    billOfMaterial_.VerifiedBy = string.Empty;
                                    billOfMaterial_.CheckedBy = string.Empty;
                                    billOfMaterial_.CreatedDate = DateTime.Now;
                                    billOfMaterial_.UpdatedDate = null;
                                    billOfMaterial_.CreatedBy = string.Empty;
                                    billOfMaterial_.UpdatedBy = string.Empty;
                                    billOfMaterial_.MaterialMasterId = null;
                                    billOfMaterial_.MaterialCategoryMasterId = null;
                                    billOfMaterial_.MaterialGroupMasterId = null;
                                    billOfMaterial_.ColorMasterId = null;
                                    billOfMaterial_.SubstanceMasterId = null;
                                    billOfMaterial_.SampleNorms = null;
                                    billOfMaterial_.Wastage = null;
                                    billOfMaterial_.SupplierMasterId = null;
                                    billOfMaterial_.UomMasterId = null;
                                    billOfMaterial_.SizeRangeMasterId = null;
                                    billOfMaterial_.SizeScheduleMasterId = null;
                                    billOfMaterial_.WastageQty = null;
                                    billOfMaterial_.WastageQtyUOM = null;
                                    billOfMaterial_.TotalNorms = null;
                                    billOfMaterial_.TotalNormsUOM = null;
                                    billOfMaterial_.ComponentName = null;
                                    billOfMaterial_.NoOfSets = null;
                                    billOfMaterial_.BuyerNorms = null;
                                    billOfMaterial_.OurNormsPercent = null;
                                    billOfMaterial_.PurchaseNorms = null;
                                    billOfMaterial_.PurchaseNormsPercent = null;
                                    billOfMaterial_.IssueNorms = null;
                                    billOfMaterial_.IssueNormsPercent = null;
                                    billOfMaterial_.CostingNorms = null;
                                    billOfMaterial_.CostingNormsPercent = null;
                                    billOfMaterial_.OurNorms = null;
                                    billOfMaterial_.DeletedBy = null;
                                    billOfMaterial_.IsDeleted = false;
                                    billOfMaterial_.DeletedOn = null;
                                    billOfMaterial_.RequirementQuantity = null;
                                    billOfMaterial_.IsMRP = false;
                                    bomid = bomDetails.BomId;
                                    listBillOfMaterial.Add(billOfMaterial_);
                                }
                            }
                            else
                            {
                                BOMError = "BOM is not Updated:" + bomno;
                            }
                        }
                        else
                        {
                            BOMError = "Insert Product style:" + bomno;
                        }
                        if (!string.IsNullOrEmpty(BOMError))
                        {
                            Session["BOMerror"] = null;
                            Session["BOMerror"] = BOMError.TrimEnd(',');
                            return RedirectToAction("BOMMaterialListGrid");
                        }
                        lotCount++;
                    }
                    if (bomDetails != null)
                    {
                        BOMMaterial bomMaterial = new BOMMaterial();
                        materialNameMaster = materialNameManager.GetMaterialName(dr[2].ToString());
                        if (materialNameMaster != null && materialNameMaster.MaterialMasterID != 0)
                        {
                            materialMaster = materialManager.GetMaterialName_(materialNameMaster.MaterialMasterID);
                        }
                        if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                        {
                            bomMaterial.BOMID = bomid.Value;
                            bomMaterial.MaterialName = materialMaster.MaterialMasterId;
                            bomMaterial.MaterialCategoryMasterId = materialMaster.MaterialCategoryMasterId;
                            bomMaterial.MaterialGroupMasterId = materialMaster.MaterialGroupMasterId;
                            bomMaterial.NoofSets = 1;
                            bomMaterial.ColorMasterId = materialMaster.ColorMasterId.Value;
                            bomMaterial.RequiredQty = Convert.ToDecimal(dr[4].ToString());
                            bomMaterial.SubstanceMasterId = materialMaster.SubstanceMasterId.Value;
                            bomMaterial.SupplierMasterID = null;
                            bomMaterial.WastageQtyUOM = materialMaster.Uom;
                            bomMaterial.TotalNormsUOM = materialMaster.UomUnit;
                            bomMaterial.CreatedDate = DateTime.Now;
                            bomMaterial.UpdatedDate = null;
                            bomMaterial.Deletedon = null;
                            bomMaterial.IsDeleted = false;
                            bomMaterial.IsMRP = false;
                            bomMaterial.DeletedBy = string.Empty;
                            bomMaterial.Rate = Convert.ToDecimal(materialMaster.Price);
                            BOMMaterial isCheckBOMMaterial = new BOMMaterial();
                            listBomMaterial.Add(bomMaterial);
                            if (isCheckBOMMaterial == null)
                            {
                                BOMError = "BOM Material is not saved:" + dr[1].ToString() + ":" + materialNameMaster.MaterialDescription;
                            }
                        }
                        else if (materialNameMaster == null || materialNameMaster.MaterialMasterID == 0)
                        {
                            BOMError += "Material Name Mater not added:" + dr[2].ToString();
                        }
                        else if (materialMaster == null || materialMaster.MaterialMasterId == 0)
                        {
                            BOMError += "Material  Mater not added:" + dr[2].ToString();
                        }
                        if (!string.IsNullOrEmpty(BOMError))
                        {
                            Session["BOMerror"] = null;
                            Session["BOMerror"] = BOMError.TrimEnd(',');
                            return RedirectToAction("BOMMaterialListGrid");
                        }
                        else
                        {
                            listBomMaterial.Add(bomMaterial);
                        }
                    }
                    else if (bomDetails == null)
                    {
                        BOMError = "BOM is not saved:" + dr[1].ToString();
                    }

                    else if (productStyleMaster == null)
                    {
                        BOMError = "Product Style is not Created:" + dr[1].ToString();
                    }
                    if (!string.IsNullOrEmpty(BOMError))
                    {
                        Session["BOMerror"] = null;
                        Session["BOMerror"] = BOMError.TrimEnd(',');
                        return RedirectToAction("BOMMaterialListGrid");
                    }

                }
                //Check error end

                //Insert record to bom and bomMaterial table start
                DataTable dataTable = new DataTable();
                dataTable = result.Tables[0];
                int iterationCount = 1;
                foreach (DataRow dr in dataTable.Rows)
                {
                    Session["SuccessOrder"] = "Excel data imported Successfully";
                    List<InternalOrderForm> listOrderEntryFormList = new List<InternalOrderForm>();
                    Bom billOfMaterial_ = new Bom();
                    Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
                    Product_BuyerStyleMaster productStyleMaster = new Product_BuyerStyleMaster();
                    MaterialManager materialManager = new MaterialManager();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialNameManager materialNameManager = new MaterialNameManager();
                    tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
                    Bom bomDetails = new Bom();
                    BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
                    if (iterationCount == 1)
                    {
                        productStyleMaster = product_BuyerStyleManager.GetOurStyle(dr[1].ToString());
                        if (productStyleMaster != null && productStyleMaster.ProductOrBuyerStyleId != 0)
                        {
                            Bom isExistBOM = new Bom();
                            isExistBOM = billOfMaterialManager.GetBomNO(dr[1].ToString());
                            if (isExistBOM == null || isExistBOM.BomId == 0)
                            {
                                ID++;
                                if (iterationCount == 1)
                                {
                                    billOfMaterial_.BomNo = dr[1].ToString();
                                    billOfMaterial_.Description = dr[1].ToString();

                                    billOfMaterial_.BuyerMasterId = productStyleMaster.BuyerName_ProductGroup;
                                    billOfMaterial_.BuyerModel = productStyleMaster.BuyerModel_ProductType.ToString();
                                    billOfMaterial_.MeanSize = 121;
                                    billOfMaterial_.Date = DateTime.Now;
                                    billOfMaterial_.ParentBomNo = null;
                                    string LastBOmNO = billOfMaterialManager.GetLastbomNumber();
                                    billOfMaterial_.LastBomNoEntered = LastBOmNO;
                                    billOfMaterial_.LinkBomNo = productStyleMaster.ProductOrBuyerStyleId.ToString();
                                    billOfMaterial_.Ammendment = string.Empty;
                                    billOfMaterial_.CommonBomNo = string.Empty;
                                    billOfMaterial_.CommnBOM1 = string.Empty;
                                    billOfMaterial_.CommnBOM2 = string.Empty;
                                    billOfMaterial_.CommnBOM3 = string.Empty;
                                    billOfMaterial_.CommnBOM4 = string.Empty;
                                    billOfMaterial_.CommnBOM5 = string.Empty;
                                    billOfMaterial_.PreparedBy = string.Empty;
                                    billOfMaterial_.VerifiedBy = string.Empty;
                                    billOfMaterial_.CheckedBy = string.Empty;
                                    billOfMaterial_.CreatedDate = DateTime.Now;
                                    billOfMaterial_.UpdatedDate = null;
                                    billOfMaterial_.CreatedBy = string.Empty;
                                    billOfMaterial_.UpdatedBy = string.Empty;
                                    billOfMaterial_.MaterialMasterId = null;
                                    billOfMaterial_.MaterialCategoryMasterId = null;
                                    billOfMaterial_.MaterialGroupMasterId = null;
                                    billOfMaterial_.ColorMasterId = null;
                                    billOfMaterial_.SubstanceMasterId = null;
                                    billOfMaterial_.SampleNorms = null;
                                    billOfMaterial_.Wastage = null;
                                    billOfMaterial_.SupplierMasterId = null;
                                    billOfMaterial_.UomMasterId = null;
                                    billOfMaterial_.SizeRangeMasterId = null;
                                    billOfMaterial_.SizeScheduleMasterId = null;
                                    billOfMaterial_.WastageQty = null;
                                    billOfMaterial_.WastageQtyUOM = null;
                                    billOfMaterial_.TotalNorms = null;
                                    billOfMaterial_.TotalNormsUOM = null;
                                    billOfMaterial_.ComponentName = null;
                                    billOfMaterial_.NoOfSets = null;
                                    billOfMaterial_.BuyerNorms = null;
                                    billOfMaterial_.OurNormsPercent = null;
                                    billOfMaterial_.PurchaseNorms = null;
                                    billOfMaterial_.PurchaseNormsPercent = null;
                                    billOfMaterial_.IssueNorms = null;
                                    billOfMaterial_.IssueNormsPercent = null;
                                    billOfMaterial_.CostingNorms = null;
                                    billOfMaterial_.CostingNormsPercent = null;
                                    billOfMaterial_.OurNorms = null;
                                    billOfMaterial_.DeletedBy = null;
                                    billOfMaterial_.IsDeleted = false;
                                    billOfMaterial_.DeletedOn = null;
                                    billOfMaterial_.RequirementQuantity = null;
                                    billOfMaterial_.IsMRP = false;
                                    bomDetails = billOfMaterialManager.Post(billOfMaterial_);
                                    bomid = bomDetails.BomId;
                                }
                            }

                            else
                            {
                                BOMError = "BOM is not Updated:" + dr[1].ToString();
                            }
                        }
                        else
                        {
                            BOMError = "BOM is not Updated:" + dr[1].ToString();
                        }
                        iterationCount++;
                    }
                    if (bomDetails != null)
                    {
                        BOMMaterial bomMaterial = new BOMMaterial();
                        materialNameMaster = materialNameManager.GetMaterialName(dr[2].ToString());
                        if (materialNameMaster != null && materialNameMaster.MaterialMasterID != 0)
                        {
                            materialMaster = materialManager.GetMaterialName_(materialNameMaster.MaterialMasterID);
                        }
                        if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                        {
                            bomMaterial.BOMID = bomid.Value;
                            bomMaterial.MaterialName = materialMaster.MaterialMasterId;
                            bomMaterial.MaterialCategoryMasterId = materialMaster.MaterialCategoryMasterId;
                            bomMaterial.MaterialGroupMasterId = materialMaster.MaterialGroupMasterId;
                            bomMaterial.NoofSets = 1;
                            bomMaterial.ColorMasterId = materialMaster.ColorMasterId.Value;
                            bomMaterial.RequiredQty = Convert.ToDecimal(dr[4].ToString());
                            bomMaterial.SubstanceMasterId = materialMaster.SubstanceMasterId.Value;
                            bomMaterial.SupplierMasterID = null;
                            bomMaterial.WastageQtyUOM = materialMaster.Uom;
                            bomMaterial.TotalNormsUOM = materialMaster.UomUnit;
                            bomMaterial.CreatedDate = DateTime.Now;
                            bomMaterial.UpdatedDate = null;
                            bomMaterial.Deletedon = null;
                            bomMaterial.IsDeleted = false;
                            bomMaterial.IsMRP = false;
                            bomMaterial.DeletedBy = string.Empty;
                            bomMaterial.Rate = Convert.ToDecimal(materialMaster.Price);
                            BOMMaterial isCheckBOMMaterial = new BOMMaterial();
                            isCheckBOMMaterial = billOfMaterialManager.BOMMaterialPost(bomMaterial);
                            if (isCheckBOMMaterial == null)
                            {
                                BOMError = "BOM Material is not saved:" + dr[1].ToString() + ":" + materialNameMaster.MaterialDescription;
                            }
                        }
                        else if (materialNameMaster == null || materialNameMaster.MaterialMasterID == 0)
                        {
                            BOMError += "Material Name Mater not added:" + dr[2].ToString();
                        }
                        else if (materialMaster == null || materialMaster.MaterialMasterId == 0)
                        {
                            BOMError += "Material Mater not added:" + dr[2].ToString();
                        }
                    }
                    else if (bomDetails == null)
                    {
                        BOMError = "BOM is not saved:" + dr[1].ToString();
                    }
                    else if (productStyleMaster == null)
                    {
                        BOMError = "Product Style is not Created:" + dr[1].ToString();
                    }
                    if (!string.IsNullOrEmpty(BOMError))
                    {
                        Session["BOMerror_"] = null;
                        Session["BOMerror_"] = BOMError.TrimEnd(',');
                        return RedirectToAction("BOMMaterialListGrid");
                    }
                }
            }

            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            if (BOMError != "")
            {
                Session["BOMerror"] = null;
                Session["BOMerror"] = BOMError.TrimEnd(',');
            }
            return RedirectToAction("BOMMaterialListGrid");

        }
        protected string GetBulkCopyColumnException(Exception ex, SqlBulkCopy bulkcopy)

        {
            string message = string.Empty;
            if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))

            {
                string pattern = @"\d+";
                Match match = Regex.Match(ex.Message.ToString(), pattern);
                var index = Convert.ToInt32(match.Value) - 1;

                FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                var sortedColumns = fi.GetValue(bulkcopy);
                var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                var metadata = itemdata.GetValue(items[index]);
                var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                message = (String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
            }
            return message;
        }

        #endregion
    }

}
