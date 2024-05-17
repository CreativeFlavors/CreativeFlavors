using MMS.Web.Models.Product_BuyerStyleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers;
using MMS.Core.Entities;
using System.IO;
using MMS.Common;
using MMS.Web.Models;
using System.Reflection;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using Excel;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class Product_BuyerStyleController : Controller
    {
        //
        // GET: /Product_BuyerStyle/

        public ActionResult Index()
        {
            return View();
        }
        #region Product_BuyerStyle
        [HttpGet]
        public ActionResult ProductBuyerStyle()
        {
            Product_BuyerStyleModel model = new Product_BuyerStyleModel();
            return View(model);
        }

        public ActionResult ProductBuyerStyleGrid()
        {
            Product_BuyerStyleModel Model = new Product_BuyerStyleModel();
            Product_BuyerStyleManager ProductManager = new Product_BuyerStyleManager();
            Model.Product_BuyerStyleList = ProductManager.Get();
            return PartialView("Partial/ProductBuyerStyleGrid", Model);
        }

        [HttpPost]
        public ActionResult ProductBuyerStyleDetails(int ProductOrBuyerStyleId)
        {
            Product_BuyerStyleManager Manager = new Product_BuyerStyleManager();
            Product_BuyerStyleMaster Master = new Product_BuyerStyleMaster();
            Product_BuyerStyleModel model = new Product_BuyerStyleModel();
            Master = Manager.GetProductOrBuyerStyleId(ProductOrBuyerStyleId);
            if (Master != null)
            {
                model.ProductOrBuyerStyleId = Master.ProductOrBuyerStyleId;
                model.BuyerName_ProductGroup = Master.BuyerName_ProductGroup;
                model.BuyerModel_ProductType = Master.BuyerModel_ProductType;
                model.BuyerStyle = Master.BuyerStyle;
                model.Last = Master.Last;
                model.Color = Master.ProductColour;
                model.OurStyle = Master.OurStyle;
                model.SizeRange = Master.SizeRange;
                model.BomNo = Master.BomNo;
                model.LeatherName_1 = Master.LeatherName_1;
                model.LeatherName_2 = Master.LeatherName_2;
                model.LeatherName_3 = Master.LeatherName_3;
                model.LeatherName_4 = Master.LeatherName_4;
                model.UOM = Master.UOM;
                model.Width_Fit = Master.Width_Fit;
                model.FinishedProductType = Master.FinishedProductType;
                model.StandardPrice = Master.StandardPrice;
                model.Product_Image = Master.Product_Image;

                model.Weight = Convert.ToDecimal(Master.Weight);
                model.Destination = Master.Destination;

            }
            return PartialView("Partial/ProductBuyerStyleDetails", model);
        }
        #endregion

        #region CommentedByMatin
        #endregion

        #region CRUD Operation
        [HttpPost]
        public ActionResult ProductBuyerStyle(Product_BuyerStyleModel model)
        {
            Alert alert = new Alert();
            Product_BuyerStyleMaster Masters = new Product_BuyerStyleMaster();
            if (ModelState.IsValid)
            {
                Product_BuyerStyleMaster Master = new Product_BuyerStyleMaster();
                Product_BuyerStyleManager Manager = new Product_BuyerStyleManager();
                ColorManager colorManager = new ColorManager();
                ColorMaster colormaster = new ColorMaster();

                colormaster = colorManager.GetColor(model.ProductColour);
                if (colormaster == null)
                {
                    ColorMaster colormaster_ = new ColorMaster();
                    colormaster_.Color = model.ProductColour;
                    colormaster_.BuyerColorCode = model.ProductColour;
                    colormaster_.Color = model.ProductColour;
                    colormaster_.CreatedDate = DateTime.Now;
                    colormaster = colorManager.ColorPost(colormaster_);
                }
                Master = Manager.GetOurStyle(model.OurStyle);
                Masters.ProductOrBuyerStyleId = model.ProductOrBuyerStyleId;
                Masters.BuyerName_ProductGroup = model.BuyerName_ProductGroup;
                Masters.BuyerModel_ProductType = model.BuyerModel_ProductType;
                Masters.BuyerStyle = model.BuyerStyle;
                Masters.Last = model.Last;
                Masters.ProductColour = colormaster.ColorMasterId;
                Masters.OurStyle = model.OurStyle;
                Masters.SizeRange = model.SizeRange;
                Masters.BomNo = model.BomNo;
                Masters.LeatherName_1 = model.LeatherName_1;
                Masters.LeatherName_2 = model.LeatherName_2;
                Masters.LeatherName_3 = model.LeatherName_3;
                Masters.LeatherName_4 = model.LeatherName_4;
                Masters.UOM = model.UOM;
                Masters.Width_Fit = model.Width_Fit;
                Masters.FinishedProductType = model.FinishedProductType;
                Masters.StandardPrice = model.StandardPrice;
                Masters.Weight = Convert.ToDecimal(model.Weight);
                Masters.Destination = model.Destination;
                if (Session["UploadImage"] != null)
                    Masters.Product_Image = Session["UploadImage"].ToString();
                Masters.CreatedDate = DateTime.Now;
                if (model.ProductOrBuyerStyleId == 0 && Master == null)
                {
                    Masters = Manager.Post(Masters);
                    alert.Status = "Saved";
                }
                else if (model.ProductOrBuyerStyleId != 0 && Master != null)
                {
                    Masters = Manager.Post(Masters);
                    alert.Status = "Updated";
                }
                else if (model.ProductOrBuyerStyleId != 0 && Master == null)
                {
                    Masters = Manager.Post(Masters);
                    alert.Status = "Updated";
                }
                else if (model.ProductOrBuyerStyleId == 0 && Master != null)
                {
                    alert.Status = "Already Existed";
                }

            }
            return Json(alert, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(Product_BuyerStyleModel model)
        {
            Product_BuyerStyleMaster ComponentMasters = new Product_BuyerStyleMaster();
            ColorManager colorManager = new ColorManager();
            ColorMaster colormaster = new ColorMaster();
            colormaster = colorManager.GetColor(model.ProductColour);
            if (colormaster == null)
            {
                colormaster.Color = model.ProductColour;
                colormaster.BuyerColorCode = model.ProductColour;
                colormaster.Color = model.ProductColour;
                colormaster.CreatedDate = DateTime.Now;
                colorManager.Post(colormaster);
            }
            if (ModelState.IsValid)
            {
                Product_BuyerStyleMaster Master = new Product_BuyerStyleMaster();
                Product_BuyerStyleManager Manager = new Product_BuyerStyleManager();
                Master = Manager.GetProductOrBuyerStyleId(model.ProductOrBuyerStyleId);
                if (Master != null)
                {
                    ComponentMasters.ProductOrBuyerStyleId = model.ProductOrBuyerStyleId;
                    ComponentMasters.BuyerName_ProductGroup = model.BuyerName_ProductGroup;
                    ComponentMasters.BuyerModel_ProductType = model.BuyerModel_ProductType;
                    ComponentMasters.BuyerStyle = model.BuyerStyle;
                    ComponentMasters.Last = model.Last;
                    ComponentMasters.ProductColour = model.Color;
                    ComponentMasters.OurStyle = model.OurStyle;
                    ComponentMasters.SizeRange = model.SizeRange;
                    ComponentMasters.BomNo = model.BomNo;
                    ComponentMasters.LeatherName_1 = model.LeatherName_1;
                    ComponentMasters.LeatherName_2 = model.LeatherName_2;
                    ComponentMasters.LeatherName_3 = model.LeatherName_3;
                    ComponentMasters.LeatherName_4 = model.LeatherName_4;
                    ComponentMasters.UOM = model.UOM;
                    ComponentMasters.Width_Fit = model.Width_Fit;
                    ComponentMasters.FinishedProductType = model.FinishedProductType;
                    ComponentMasters.StandardPrice = model.StandardPrice;
                    ComponentMasters.Weight = model.Weight;
                    ComponentMasters.Destination = model.Destination;
                    if (Session["UploadImage"] == null)
                    {
                        ComponentMasters.Product_Image = Master.Product_Image;
                    }
                    else
                    {
                        ComponentMasters.Product_Image = Session["UploadImage"].ToString();
                    }

                    ComponentMasters = Manager.Put(ComponentMasters);
                }
                else
                {
                    return Json(ComponentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(ComponentMasters, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int ProductOrBuyerStyleId)
        {
            Product_BuyerStyleMaster Master = new Product_BuyerStyleMaster();
            string status = "";
            Product_BuyerStyleManager Manager = new Product_BuyerStyleManager();
            Master = Manager.GetProductOrBuyerStyleId(ProductOrBuyerStyleId);
            if (Master != null)
            {
                status = "Success";
                Manager.Delete(Master.ProductOrBuyerStyleId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                Guid Guid = Guid.NewGuid();
                var fileName = Guid.ToString() + ".jpg";
                Session["UploadImage"] = fileName;
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                file.SaveAs(path);
            }

        }

        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<Product_BuyerStyleMaster> Masterlist = new List<Product_BuyerStyleMaster>();
            Product_BuyerStyleManager uOMManager = new Product_BuyerStyleManager();
            Masterlist = uOMManager.Get();
            Masterlist = Masterlist.Where(x => x.BuyerName_ProductGroup.ToString().Trim().Contains(filter.Trim()) || x.BuyerModel_ProductType.ToString().Trim().Contains(filter.Trim()) || x.BuyerStyle.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            Product_BuyerStyleModel model = new Product_BuyerStyleModel();
            model.Product_BuyerStyleList = Masterlist;
            return PartialView("Partial/ProductBuyerStyleGrid", model);
        }
        #endregion

        #region Product Style Master Import Excel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadData(HttpPostedFileBase upload)
        {
            Product_BuyerStyleModel model = new Product_BuyerStyleModel();
            string Error = "";
            if (ModelState.IsValid)
            {
                DataSet result = null;
                if (upload != null && upload.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader = null;
                    if (upload.FileName.ToLower().EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.ToLower().EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        TempData["successBody"] = "This file format is not supported";
                        return RedirectToAction("ProductBuyerStyle");
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    result = reader.AsDataSet();
                    reader.Close();
                }
                else
                {
                    TempData["successBody"] = "Please Upload Your file";
                    return RedirectToAction("ProductBuyerStyle");
                }
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateSupplierID();
                string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                var table = new DataTable();
                table = result.Tables[0];
                DataTable table_ = new DataTable();
                table_.Columns.Add("ProductOrBuyerStyleId", typeof(int));
                table_.Columns.Add("BuyerName_ProductGroup", typeof(int));
                table_.Columns.Add("BuyerModel_ProductType", typeof(int));
                table_.Columns.Add("BuyerStyle", typeof(string));
                table_.Columns.Add("Last", typeof(string));
                table_.Columns.Add("ProductColour", typeof(int));
                table_.Columns.Add("OurStyle", typeof(string));
                table_.Columns.Add("SizeRange", typeof(string));
                table_.Columns.Add("BomNo", typeof(string));
                table_.Columns.Add("LeatherName_1", typeof(string));
                table_.Columns.Add("LeatherName_2", typeof(string));
                table_.Columns.Add("LeatherName_3", typeof(string));
                table_.Columns.Add("LeatherName_4", typeof(string));
                table_.Columns.Add("LeatherName_5", typeof(string));
                table_.Columns.Add("UOM", typeof(string));
                table_.Columns.Add("Width_Fit", typeof(string));
                table_.Columns.Add("FinishedProductType", typeof(string));
                table_.Columns.Add("StandardPrice", typeof(string));
                table_.Columns.Add("Product_Image", typeof(string));
                table_.Columns.Add("CreatedDate", typeof(DateTime));
                table_.Columns.Add("UpdatedDate", typeof(DateTime));
                table_.Columns.Add("CreatedBy", typeof(string));
                table_.Columns.Add("UpdatedBy", typeof(string));

                table_.Columns.Add("Weight", typeof(decimal));
                table_.Columns.Add("Destination", typeof(string));
                table.Columns.Add("UpdatedDate").DefaultValue = null;
                DataRow drowItem;
                Product_BuyerStyleManager product_BuyerStyleManager = new Product_BuyerStyleManager();
                List<Product_BuyerStyleMaster> listProductBuyerStyleMaster = new List<Product_BuyerStyleMaster>();
                foreach (DataRow dr in table.Rows)
                {
                    var isEmpty = dr.ItemArray.All(c => c is DBNull);
                    if (!isEmpty)
                    {
                        //Your Logic
                        //  string a ="5";

                        Product_BuyerStyleMaster productBuyerStyleMaster = new Product_BuyerStyleMaster();
                        BuyerManager buyerManager = new BuyerManager();
                        BuyerMaster buyerMaster = new BuyerMaster();
                        BuyerModel buyerModel = new BuyerModel();
                        BuyerModelManager buyerModelManager = new BuyerModelManager();
                        MaterialManager materialManager = new MaterialManager();
                        MaterialMaster materialMaster = new MaterialMaster();
                        MaterialNameManager materialNameManager = new MaterialNameManager();
                        tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
                        UOMManager uomManager = new UOMManager();
                        UomMaster uomMaster = new UomMaster();
                        uomMaster = uomManager.GetLongUnitName(dr[4].ToString());
                        string a = dr[3].ToString();
                        if (dr[3].ToString() == "J.BOSCH NUBUCK 2586")
                        {
                            string message = "";
                        }
                        materialNameMaster = materialNameManager.GetMaterialName(dr[3].ToString());
                        if (materialNameMaster != null && materialNameMaster.MaterialMasterID != 0)
                        {
                            materialMaster = materialManager.GetMaterialName_(materialNameMaster.MaterialMasterID);
                        }
                        else if (materialNameMaster == null || materialNameMaster.MaterialMasterID == 0)
                        {
                            Error = "Material Name Master not created:" + dr[3].ToString();
                        }
                        if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                        {

                            SizeRangeDetailsManager sizeRangeManager = new SizeRangeDetailsManager();
                            SizeRangeDetails sizeDetailsMaster = new SizeRangeDetails();
                            sizeDetailsMaster = sizeRangeManager.GetSizeRangeName(dr[6].ToString());
                            buyerMaster = buyerManager.GetBuyerFullName(dr[0].ToString());
                            productBuyerStyleMaster = product_BuyerStyleManager.GetOurStyle(dr["BUYER STYLE"].ToString());
                            buyerModel = buyerModelManager.GetBuyerModel(dr[1].ToString());
                            ColorManager colorManager = new ColorManager();
                            ColorMaster colormaster = new ColorMaster();
                            string[] split = dr[2].ToString().Split(new char[0]);
                            colormaster = colorManager.GetColor(split[2]);
                            if (colormaster == null)
                            {
                                ColorMaster colormaster_ = new ColorMaster();
                                colormaster_.Color = split[1];
                                colormaster_.BuyerColorCode = split[1];
                                colormaster_.CreatedDate = DateTime.Now;
                                colormaster = colorManager.ColorPost(colormaster_);
                            }
                            if (buyerModel == null)
                            {
                                Error = "Please check the Buyer style" + dr[1].ToString();
                            }
                            else
                            {
                                if ((productBuyerStyleMaster == null || productBuyerStyleMaster.ProductOrBuyerStyleId == 0) || (productBuyerStyleMaster != null && productBuyerStyleMaster.ProductOrBuyerStyleId != 0))
                                {
                                    DataRow newrow = table_.NewRow();
                                    var productBuyerStyleMaster_ = new Product_BuyerStyleMaster();
                                    productBuyerStyleMaster_.BuyerName_ProductGroup = buyerMaster.BuyerMasterId;
                                    productBuyerStyleMaster_.BuyerModel_ProductType = buyerModel.BuyerModelID;
                                    productBuyerStyleMaster_.BuyerStyle = dr[2].ToString();
                                    productBuyerStyleMaster_.Last = split[1];
                                    productBuyerStyleMaster_.ProductColour = colormaster.ColorMasterId;
                                    productBuyerStyleMaster_.OurStyle = dr[2].ToString();
                                    productBuyerStyleMaster_.SizeRange = sizeDetailsMaster.SizeRangeDetailsId.ToString();
                                    productBuyerStyleMaster_.BomNo = dr[2].ToString();
                                    productBuyerStyleMaster_.LeatherName_1 = materialMaster.MaterialMasterId.ToString();
                                    productBuyerStyleMaster_.LeatherName_2 = null;
                                    productBuyerStyleMaster_.LeatherName_3 = null;
                                    productBuyerStyleMaster_.LeatherName_4 = null;
                                    productBuyerStyleMaster_.LeatherName_5 = null;
                                    productBuyerStyleMaster_.UOM = uomMaster.UomMasterId.ToString();
                                    productBuyerStyleMaster_.Width_Fit = "H";
                                    productBuyerStyleMaster_.FinishedProductType = dr[7].ToString();
                                    productBuyerStyleMaster_.StandardPrice = dr[8].ToString();
                                    if (dr[9].ToString() != null)
                                    {
                                        productBuyerStyleMaster_.Weight = Convert.ToDecimal(dr[9]);
                                    }
                                    else
                                    {
                                        productBuyerStyleMaster_.Weight = null;
                                    }
                                    var t = dr[9];
                                    productBuyerStyleMaster_.Destination = dr[10].ToString();
                                    productBuyerStyleMaster_.Product_Image = string.Empty;
                                    productBuyerStyleMaster_.CreatedDate = DateTime.Now;
                                    productBuyerStyleMaster_.UpdatedDate = null;
                                    productBuyerStyleMaster_.CreatedBy = Session["UserName"].ToString();
                                    productBuyerStyleMaster_.UpdatedBy = string.Empty;
                                    table_.Rows.Add(newrow);
                                    if (productBuyerStyleMaster == null || productBuyerStyleMaster.ProductOrBuyerStyleId == 0)
                                    {
                                        listProductBuyerStyleMaster.Add(productBuyerStyleMaster_);
                                    }
                                }
                            }

                        }
                        else if (materialMaster == null || materialMaster.MaterialMasterId == 0)
                        {
                            Error = "Material Master is not created:" + dr[3].ToString();
                        }
                        if (!string.IsNullOrEmpty(Error))
                        {
                            Session["Product_error"] = null;
                            Session["Product_error"] = Error;
                            return RedirectToAction("ProductBuyerStyle");
                        }
                    }
                }
                if (string.IsNullOrEmpty(Error))
                {
                    int count = 0;
                    Product_BuyerStyleMaster productBuyerStyle = new Product_BuyerStyleMaster();
                    foreach (var item in listProductBuyerStyleMaster)
                    {
                        productBuyerStyle = product_BuyerStyleManager.Post(item);
                        if (productBuyerStyle != null && productBuyerStyle.ProductOrBuyerStyleId != 0)
                        {
                            count++;
                        }
                    }
                    Session["successBody"] = count + " " + "Items imported successfully";
                }
                else
                {
                    Session["Product_error"] = null;
                    Session["Product_error"] = Error;
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            return RedirectToAction("ProductBuyerStyle");
        }

        #endregion
    }
}
