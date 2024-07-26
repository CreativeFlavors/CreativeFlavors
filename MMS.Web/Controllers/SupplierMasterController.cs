using Excel;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.SupplierMasterModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class SupplierMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult SupplierMaster()
        {
            SupplierMasterModel vm = new SupplierMasterModel();
            SupplierMasterManager am = new SupplierMasterManager();
            MaterialGroupManager am_ = new MaterialGroupManager();
            List<System.Web.Mvc.SelectListItem> items_ = (am_.Get().Select(r =>
             new System.Web.Mvc.SelectListItem()
             {
                 Value = r.MaterialGroupMasterId.ToString(),
                 Text = r.GroupName
             })).ToList();

            var emptyItem_ = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, emptyItem_);
            vm.MaterialGroupList = items_;
            List<System.Web.Mvc.SelectListItem> items = (am.Get().Select(r =>
            new System.Web.Mvc.SelectListItem()
            {
                Value = r.SupplierMasterId.ToString(),
                Text = r.SupplierName
            })).ToList();

            var emptyItem = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, emptyItem);
            vm.materialList = items;
            
            
            return View(vm);
        }
        public ActionResult SupplierMasterGrid()
        {
            SupplierMasterModel vm = new SupplierMasterModel();
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            vm.SupplierMasterList = supplierManager.Get();
            // SupplierMasterModel vm = new SupplierMasterModel();
            SupplierMasterManager am = new SupplierMasterManager();
            MaterialGroupManager am_ = new MaterialGroupManager();
            MaterialNameManager materialManager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items_ = (am_.Get().Select(r =>
             new System.Web.Mvc.SelectListItem()
             {
                 Value = r.MaterialGroupMasterId.ToString(),
                 Text = r.GroupName
             })).ToList();

            var emptyItem_ = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, emptyItem_);
            vm.MaterialGroupList = items_;
            List<System.Web.Mvc.SelectListItem> items = (materialManager.Get().Select(r =>
            new System.Web.Mvc.SelectListItem()
            {
                Value = r.MaterialMasterID.ToString(),
                Text = r.MaterialDescription
            })).ToList();

            var emptyItem = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, emptyItem);
            vm.materialList = items;
            if (TempData["successBody"] != null)
            {
                vm.UploadError = TempData["successBody"].ToString();
                TempData["successBody"] = null;
            }
            return PartialView("Partial/SupplierMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult SupplierMasterDetails(int SupplierMasterId)
        {
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            SupplierMaster supplierMaster = new SupplierMaster();
            SupplierMasterModel model = new SupplierMasterModel();
            supplierMaster = supplierManager.GetSupplierMasterId(SupplierMasterId);
            SupplierMasterModel vm = new SupplierMasterModel();
            SupplierMasterManager am = new SupplierMasterManager();
            MaterialNameManager materialManager = new MaterialNameManager();
            MaterialGroupManager am_ = new MaterialGroupManager();
            List<System.Web.Mvc.SelectListItem> items_ = (am_.Get().Select(r =>
             new System.Web.Mvc.SelectListItem()
             {
                 Value = r.MaterialGroupMasterId.ToString(),
                 Text = r.GroupName
             })).ToList();

            var emptyItem_ = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, emptyItem_);
            model.MaterialGroupList = items_;
            List<System.Web.Mvc.SelectListItem> items = (materialManager.Get().Select(r =>
            new System.Web.Mvc.SelectListItem()
            {
                Value = r.MaterialMasterID.ToString(),
                Text = r.MaterialDescription
            })).ToList();

            var emptyItem = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, emptyItem);
            model.materialList = items;
            if (SupplierMasterId == 0)
            {
                model.SupplierCode = GetSuplierMasterID();
                model.SupplierCode = "SUP00" + model.SupplierCode;
            }
            else
            {
                model.SupplierCode = "SUP00" + supplierMaster.SupplierCode;
            }
            if (supplierMaster != null && supplierMaster.SupplierMasterId != 0)
            {
                model.SupplierMasterId = supplierMaster.SupplierMasterId;
                string[] FrmID = supplierMaster.MaterialMasterID.TrimEnd(',').Split(',');
                string[] ToID = supplierMaster.MaterialGroupMasterID.TrimEnd(',').Split(',');
                model.SupplierName = supplierMaster.SupplierName;
                model.Currency = supplierMaster.Currency;
                model.AddressLine1 = supplierMaster.AddressLine1;
                model.SelectedItemId = FrmID;
                model.MaterialSelectedItemId = ToID;

                model.materialList = model.materialList.Where(x => FrmID.Contains(x.Value)).ToList();


                model.AddressLine2 = supplierMaster.AddressLine2;
                model.AddressLine3 = supplierMaster.AddressLine3;
                model.Country = supplierMaster.Country;
                model.ContactPerson = supplierMaster.ContactPerson;
                model.Designation = supplierMaster.Designation;
                model.Mobile = supplierMaster.Mobile;
                model.LandLine = supplierMaster.LandLine;
                model.Fax = supplierMaster.Fax;
                model.Email = supplierMaster.Email;
                model.StNo = supplierMaster.StNo;
                model.CstNo = supplierMaster.CstNo;
                model.Range = supplierMaster.Range;
                model.Division = supplierMaster.Division;
                model.PLANo = supplierMaster.PLANo;
                model.EOCNo = supplierMaster.EOCNo;
                model.RegnNo = supplierMaster.RegnNo;
                model.PaymentTerms = supplierMaster.PaymentTerms;
                model.DeliveryTerms = supplierMaster.DeliveryTerms;
                model.PackingDetails = supplierMaster.PackingDetails;
                model.Insurance = supplierMaster.Insurance;
                model.PortOfLoading = supplierMaster.PortOfLoading;
                model.DespatchThrough = supplierMaster.DespatchThrough;
                model.Freight = supplierMaster.Freight;
                model.FreightName = supplierMaster.FreightName;
                model.Form1 = supplierMaster.Form;
                model.FormName = supplierMaster.FormName;
                model.Forwarder = supplierMaster.Forwarder;
                model.BankName = supplierMaster.BankName;
                model.BankAddress = supplierMaster.BankAddress;
                model.BankCode = supplierMaster.BankCode;
                model.SwiftCode = supplierMaster.SwiftCode;
                model.AccountNumber = supplierMaster.AccountNumber;
                model.IBAN = supplierMaster.IBAN;
                model.OtherInformation = supplierMaster.OtherInformation;
                model.IsDomestic = Convert.ToBoolean(supplierMaster.IsDomestic);
                model.IsImport = Convert.ToBoolean(supplierMaster.IsImport);
                model.SupplierGSTIN = supplierMaster.SupplierGSTIN;
                model.CreditLimit = supplierMaster.CreditLimit;
                model.CreditDays = supplierMaster.CreditDays;
            }
            return PartialView("Partial/SupplierMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult SupplierMaster(SupplierMasterModel model)
        {
            SupplierMaster supplierMasters = new SupplierMaster();
            SupplierMaster supplierMaster = new SupplierMaster();
            SupplierMaster supplierMaster_ = new SupplierMaster();
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            supplierMaster = supplierManager.GetSupplierName(model.SupplierName);
            if (supplierMaster == null)
            {
                supplierMasters.SupplierMasterId = model.SupplierMasterId;
                supplierMasters.SupplierCode = GetSuplierMasterID();
                supplierMasters.SupplierName = model.SupplierName;
                supplierMasters.Currency = model.Currency;
                supplierMasters.AddressLine1 = model.AddressLine1;
                supplierMasters.AddressLine2 = model.AddressLine2;
                supplierMasters.AddressLine3 = model.AddressLine3;
                supplierMasters.Country = model.Country;
                supplierMasters.ContactPerson = model.ContactPerson;
                supplierMasters.Designation = model.Designation;
                supplierMasters.Mobile = model.Mobile;
                supplierMasters.LandLine = model.LandLine;
                supplierMasters.Fax = model.Fax;
                supplierMasters.Email = model.Email;
                supplierMasters.StNo = model.StNo;
                supplierMasters.CstNo = model.CstNo;
                supplierMasters.Range = model.Range;
                supplierMasters.Division = model.Division;
                supplierMasters.PLANo = model.PLANo;
                supplierMasters.EOCNo = model.EOCNo;
                supplierMasters.RegnNo = model.RegnNo;
                supplierMasters.PaymentTerms = model.PaymentTerms;
                supplierMasters.DeliveryTerms = model.DeliveryTerms;
                supplierMasters.PackingDetails = model.PackingDetails;
                supplierMasters.Insurance = model.Insurance;
                supplierMasters.PortOfLoading = model.PortOfLoading;
                supplierMasters.DespatchThrough = model.DespatchThrough;
                supplierMasters.Freight = model.Freight;
                supplierMasters.FreightName = model.FreightName;
                supplierMasters.Form = model.Form1;
                supplierMasters.FormName = model.FormName;
                supplierMasters.Forwarder = model.Forwarder;
                supplierMasters.BankName = model.BankName;
                supplierMasters.BankAddress = model.BankAddress;
                supplierMasters.BankCode = model.BankCode;
                supplierMasters.SwiftCode = model.SwiftCode;
                supplierMasters.AccountNumber = model.AccountNumber;
                supplierMasters.IBAN = model.IBAN;
                supplierMasters.OtherInformation = model.OtherInformation;
                supplierMasters.IsDomestic = model.IsDomestic;
                supplierMasters.IsImport = model.IsImport;
                supplierMasters.SupplierGSTIN = model.SupplierGSTIN;
                supplierMasters.CreatedDate = DateTime.Now;
                supplierMasters.CreatedBy = Session["UserName"].ToString();
                supplierMasters.CreditLimit = model.CreditLimit;
                supplierMasters.CreditDays = model.CreditDays;
                string MaterialId = string.Empty;
                string MaterialGroupMasterialId = string.Empty;
                if (model.SelectedItemId != null)
                {
                    foreach (var items in model.SelectedItemId)
                    {
                        MaterialId += items.ToString() + ",";
                    }
                }
                if (model.MaterialSelectedItemId != null)
                {
                    foreach (var item in model.MaterialSelectedItemId)
                    {
                        MaterialGroupMasterialId += item.ToString() + ",";
                    }
                }

                supplierMasters.MaterialMasterID = MaterialId;
                supplierMasters.MaterialGroupMasterID = MaterialGroupMasterialId;
                supplierMaster_ = supplierManager.Post(supplierMasters);



            }
            else if (supplierMaster != null && supplierMaster.SupplierName == model.SupplierName)
            {
                supplierMasters.SupplierMasterId = 0;
                return Json(supplierMasters, JsonRequestBehavior.AllowGet);
            }
            return Json(supplierMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(SupplierMasterModel model)
        {
            SupplierMaster supplierMaster = new SupplierMaster();
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            supplierMaster = supplierManager.GetSupplierMasterId(model.SupplierMasterId);
            if (supplierMaster != null)
            {
                supplierMaster.SupplierCode = supplierMaster.SupplierCode;
                supplierMaster.SupplierName = model.SupplierName;
                supplierMaster.Currency = model.Currency;
                supplierMaster.AddressLine1 = model.AddressLine1;
                supplierMaster.AddressLine2 = model.AddressLine2;
                supplierMaster.AddressLine3 = model.AddressLine3;
                supplierMaster.Country = model.Country;
                supplierMaster.MaterialMasterID = model.MaterialMasterID;
                supplierMaster.ContactPerson = model.ContactPerson;
                supplierMaster.Designation = model.Designation;
                supplierMaster.Mobile = model.Mobile;
                supplierMaster.LandLine = model.LandLine;
                supplierMaster.Fax = model.Fax;
                supplierMaster.Email = model.Email;
                supplierMaster.StNo = model.StNo;
                supplierMaster.CstNo = model.CstNo;
                supplierMaster.Range = model.Range;
                supplierMaster.Division = model.Division;
                supplierMaster.PLANo = model.PLANo;
                supplierMaster.EOCNo = model.EOCNo;
                supplierMaster.RegnNo = model.RegnNo;
                supplierMaster.PaymentTerms = model.PaymentTerms;
                supplierMaster.DeliveryTerms = model.DeliveryTerms;
                supplierMaster.PackingDetails = model.PackingDetails;
                supplierMaster.Insurance = model.Insurance;
                supplierMaster.PortOfLoading = model.PortOfLoading;
                supplierMaster.DespatchThrough = model.DespatchThrough;
                supplierMaster.Freight = model.Freight;
                supplierMaster.FreightName = model.FreightName;
                supplierMaster.Form = model.Form1;
                supplierMaster.FormName = model.FormName;
                supplierMaster.Forwarder = model.Forwarder;
                supplierMaster.BankName = model.BankName;
                supplierMaster.BankAddress = model.BankAddress;
                supplierMaster.BankCode = model.BankCode;
                supplierMaster.SwiftCode = model.SwiftCode;
                supplierMaster.AccountNumber = model.AccountNumber;
                supplierMaster.IBAN = model.IBAN;
                supplierMaster.OtherInformation = model.OtherInformation;
                supplierMaster.IsDomestic = model.IsDomestic;
                supplierMaster.IsImport = model.IsImport;
                supplierMaster.SupplierGSTIN = model.SupplierGSTIN;
                supplierMaster.UpdatedDate = DateTime.Now;
                supplierMaster.UpdatedBy = Session["UserName"].ToString();
                supplierMaster.UpdatedDate = DateTime.Now;
                supplierMaster.CreditLimit = model.CreditLimit;
                supplierMaster.CreditDays = model.CreditDays;
                string MaterialId = string.Empty;
                string MaterialGroupMasterialId = string.Empty;
                foreach (var items in model.SelectedItemId)
                {
                    MaterialId += items + ",";
                }
                foreach (var item in model.MaterialSelectedItemId)
                {
                    MaterialGroupMasterialId += item + ",";
                }
                supplierMaster.MaterialMasterID = MaterialId;
                supplierMaster.MaterialGroupMasterID = MaterialGroupMasterialId;
                supplierManager.Put(supplierMaster);

            }
            return Json(supplierMaster, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int BuyerMasterId)
        {
            SupplierMaster supplierMaster = new SupplierMaster();
            string status = "";
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            supplierMaster = supplierManager.GetSupplierMasterId(BuyerMasterId);
            if (supplierMaster != null)
            {
                status = "Success";
                supplierManager.Delete(supplierMaster.SupplierMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            SupplierMasterModel vm = new SupplierMasterModel();
            List<SupplierMaster> supplierMasterlist = new List<SupplierMaster>();
            SupplierMasterManager supplierManager = new SupplierMasterManager();
            supplierMasterlist = supplierManager.GetSupplierMasterGrid(filter);
            SupplierMasterManager am = new SupplierMasterManager();
            MaterialGroupManager am_ = new MaterialGroupManager();
            MaterialNameManager materialManager = new MaterialNameManager();
            List<System.Web.Mvc.SelectListItem> items_ = (am_.Get().Select(r =>
             new System.Web.Mvc.SelectListItem()
             {
                 Value = r.MaterialGroupMasterId.ToString(),
                 Text = r.GroupName
             })).ToList();

            var emptyItem_ = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items_.Insert(0, emptyItem_);
            vm.MaterialGroupList = items_;
            List<System.Web.Mvc.SelectListItem> items = (materialManager.Get().Select(r =>
            new System.Web.Mvc.SelectListItem()
            {
                Value = r.MaterialMasterID.ToString(),
                Text = r.MaterialDescription
            })).ToList();

            var emptyItem = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            items.Insert(0, emptyItem);
            vm.materialList = items;
            if (TempData["successBody"] != null)
            {
                vm.UploadError = TempData["successBody"].ToString();
                TempData["successBody"] = null;
            }

            vm.SupplierMasterList = supplierMasterlist;
            return PartialView("Partial/SupplierMasterGrid", vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            SupplierMasterModel model = new SupplierMasterModel();
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
                    if (upload.FileName.EndsWith(".xls"))
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
                        return RedirectToAction("SupplierMaster");
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    result = reader.AsDataSet();
                    reader.Close();                    
                }
                else
                {
                    TempData["successBody"] = "Please Upload Your file";                   
                    return RedirectToAction("SupplierMaster");
                }
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateSupplierID();
                string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
                var table = new DataTable(); 
                table = result.Tables[0];
                System.Data.DataColumn CurrencyColumn = new System.Data.DataColumn("Currency", typeof(System.Int32));
                System.Data.DataColumn CountryColumn = new System.Data.DataColumn("Country1", typeof(System.Int32));
                System.Data.DataColumn newColumn = new System.Data.DataColumn("CreatedBy", typeof(System.String));
                newColumn.DefaultValue = HttpContext.Session["UserName"].ToString();
                System.Data.DataColumn newColumn1 = new System.Data.DataColumn("CreatedDate", typeof(System.String));
                newColumn1.DefaultValue = DateTime.Now;
                table.Columns.Add(newColumn1);
                table.Columns.Add(newColumn);
                table.Columns.Add("UpdatedBy").DefaultValue = "";                
                System.Data.DataColumn newColumn__ = new System.Data.DataColumn("CreatedBy", typeof(System.String));
                ID = ID++;
                DataTable table_ = new DataTable();
                table_.Columns.Add("SupplierMasterId", typeof(int));
                table_.Columns.Add("SupplierCode", typeof(string));
                table_.Columns.Add("SupplierName", typeof(string));
                table_.Columns.Add("Currency", typeof(int));
                table_.Columns.Add("AddressLine1", typeof(string));
                table_.Columns.Add("AddressLine2", typeof(string));
                table_.Columns.Add("AddressLine3", typeof(string));
                table_.Columns.Add("Country", typeof(int));
                table_.Columns.Add("ContactPerson", typeof(string));
                table_.Columns.Add("Designation", typeof(string));
                table_.Columns.Add("Mobile", typeof(string));
                table_.Columns.Add("LandLine", typeof(string));
                table_.Columns.Add("Fax", typeof(string));
                table_.Columns.Add("Email", typeof(string));
                table_.Columns.Add("StNo", typeof(string));
                table_.Columns.Add("CstNo", typeof(string));
                table_.Columns.Add("Range", typeof(string));
                table_.Columns.Add("Division", typeof(string));
                table_.Columns.Add("PLANo", typeof(string));
                table_.Columns.Add("EOCNo", typeof(string));
                table_.Columns.Add("RegnNo", typeof(string));
                table_.Columns.Add("PaymentTerms", typeof(string));
                table_.Columns.Add("DeliveryTerms", typeof(string));
                table_.Columns.Add("PackingDetails", typeof(string));
                table_.Columns.Add("Insurance", typeof(string));
                table_.Columns.Add("PortOfLoading", typeof(string));
                table_.Columns.Add("DespatchThrough", typeof(string));
                table_.Columns.Add("Freight", typeof(string));
                table_.Columns.Add("FreightName", typeof(string));
                table_.Columns.Add("Form", typeof(string));
                table_.Columns.Add("FormName", typeof(string));
                table_.Columns.Add("Forwarder", typeof(string));
                table_.Columns.Add("BankName", typeof(string));
                table_.Columns.Add("BankAddress", typeof(string));
                table_.Columns.Add("BankCode", typeof(string));
                table_.Columns.Add("SwiftCode", typeof(string));
                table_.Columns.Add("AccountNumber", typeof(string));
                table_.Columns.Add("IBAN", typeof(string));
                table_.Columns.Add("OtherInformation", typeof(string));
                table_.Columns.Add("IsDomestic", typeof(bool));
                table_.Columns.Add("IsImport", typeof(bool));
                table_.Columns.Add("MaterialMasterID", typeof(string));
                table_.Columns.Add("MaterialGroupMasterID", typeof(string));
                table_.Columns.Add("CreatedDate", typeof(DateTime));
                table_.Columns.Add("UpdatedDate", typeof(DateTime));
                table_.Columns.Add("CreatedBy", typeof(string));
                table_.Columns.Add("UpdatedBy", typeof(string));
                table_.Columns.Add("IsDeleted", typeof(bool));
                table_.Columns.Add("DeletedBy", typeof(string));
                table_.Columns.Add("DeletedDate", typeof(DateTime));
                table.Columns.Add("UpdatedDate").DefaultValue = null;
                table.Columns.Add("DeletedDate").DefaultValue = null;
                //DataRow drowItem;
                foreach (DataRow dr in table.Rows)
                {
                    dr["Country1"] = "";
                    SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
                    SupplierMaster supplier = new SupplierMaster();
                    supplier= supplierMasterManager.GetSupplierName(dr["Name"].ToString());
                    if (dr["Sl.No"].ToString() != "0" && dr["Sl.No"].ToString() != ""&& supplier==null)
                    {
                        ID++;
                        DataRow newrow = table_.NewRow();
                        CountryMaster countryMaster = new CountryMaster();
                        CountryManager countryManager = new CountryManager();
                        CurrencyMaster currencyMaster = new CurrencyMaster();
                        CurrencyManager currencyManager = new CurrencyManager();

                        countryMaster = countryManager.GetContainCountryName(dr["Country1"].ToString());
                        if (dr["Currency"].ToString().ToLower() == "rs." || dr["Currency"].ToString().ToLower() == "inr" || dr["Currency"].ToString() == "$" )
                        {
                            currencyMaster = currencyManager.GetContainCurrencyFullName("Rs");
                        }
                        else
                        {
                            if (dr["Currency"].ToString() == "Euro" || dr["Currency"].ToString() == "euro")
                            {
                                dr["Currency"] = "Eur";
                            }
                            currencyMaster = currencyManager.GetContainCurrencyFullName(dr["Currency"].ToString());
                        }

                        if (countryMaster != null)
                        {
                            dr["Country1"] = countryMaster.CountryMasterId;
                        }
                        if (currencyMaster != null)
                        {
                            dr["Currency"] = currencyMaster.CurrencyMasterId;
                        }
                        newrow["SupplierMasterId"] = ID.ToString().Trim();
                        newrow["SupplierCode"] = ID.ToString().Trim();
                        newrow["SupplierName"] = dr[2].ToString().Trim();
                        newrow["Currency"] = Convert.ToInt32(dr["Currency"].ToString().Trim());
                        newrow["AddressLine1"] = dr[2].ToString().Trim();
                        newrow["AddressLine2"] = "";
                        newrow["AddressLine3"] = "";
                        newrow["Country"] = Convert.ToInt32(dr["Country1"].ToString().Trim());
                        newrow["ContactPerson"] = dr[14].ToString().Trim();
                        newrow["Designation"] = dr[15].ToString().Trim();
                        newrow["Mobile"] = dr[6].ToString().Trim();
                        newrow["LandLine"] = null;
                        newrow["Fax"] = dr[10].ToString().Trim();
                        newrow["Email"] = dr[11].ToString().Trim();
                        newrow["StNo"] = dr[13].ToString().Trim();
                        newrow["CstNo"] = dr[12].ToString().Trim();
                        newrow["Range"] = string.Empty;
                        newrow["Division"] = string.Empty;
                        newrow["PLANo"] = string.Empty;
                        newrow["EOCNo"] = string.Empty;
                        newrow["RegnNo"] = string.Empty;
                        newrow["PaymentTerms"] = string.Empty;
                        newrow["DeliveryTerms"] = string.Empty;
                        newrow["PackingDetails"] = string.Empty;
                        newrow["Insurance"] = string.Empty;
                        newrow["PortOfLoading"] = string.Empty;
                        newrow["DespatchThrough"] = string.Empty;
                        newrow["Freight"] = string.Empty;
                        newrow["FreightName"] = string.Empty;
                        newrow["Form"] = string.Empty;
                        newrow["FormName"] = string.Empty;
                        newrow["Forwarder"] = string.Empty;
                        newrow["BankName"] = string.Empty;
                        newrow["BankAddress"] = string.Empty;
                        newrow["BankCode"] = string.Empty;
                        newrow["SwiftCode"] = string.Empty;
                        newrow["AccountNumber"] = string.Empty;
                        newrow["IBAN"] = string.Empty;
                        newrow["OtherInformation"] = string.Empty;
                        newrow["IsDomestic"] = false;
                        newrow["IsImport"] = false;
                        newrow["MaterialMasterID"] = string.Empty;
                        newrow["MaterialGroupMasterID"] = string.Empty;
                        newrow["CreatedDate"] = DateTime.Now;
                        newrow["UpdatedDate"] = DBNull.Value;
                        newrow["CreatedBy"] = HttpContext.Session["UserName"].ToString();
                        newrow["UpdatedBy"] = string.Empty;
                        newrow["IsDeleted"] = false;
                        newrow["DeletedBy"] = string.Empty;
                        newrow["DeletedDate"] = DBNull.Value;
                        table_.Rows.Add(newrow);
                    }
                }


                //table.Columns.Add(SimplemrpID);
                using (SqlConnection con = new SqlConnection(consString))

                {

                    using (SqlBulkCopy sqlBulk = new SqlBulkCopy(con))
                    {
                        //Set the database table name   
                        try
                        {
                            sqlBulk.DestinationTableName = "SupplierMaster";
                            sqlBulk.ColumnMappings.Add("SupplierMasterId", "SupplierMasterId");
                            sqlBulk.ColumnMappings.Add("SupplierCode", "SupplierCode");
                            sqlBulk.ColumnMappings.Add("SupplierName", "SupplierName");
                            sqlBulk.ColumnMappings.Add("Currency", "Currency");
                            sqlBulk.ColumnMappings.Add("AddressLine1", "AddressLine1");
                            sqlBulk.ColumnMappings.Add("AddressLine2", "AddressLine2");
                            sqlBulk.ColumnMappings.Add("AddressLine3", "AddressLine3");
                            sqlBulk.ColumnMappings.Add("Country", "Country");
                            sqlBulk.ColumnMappings.Add("ContactPerson", "ContactPerson");
                            sqlBulk.ColumnMappings.Add("Designation", "Designation");
                            sqlBulk.ColumnMappings.Add("Mobile", "Mobile");
                            sqlBulk.ColumnMappings.Add("LandLine", "LandLine");
                            sqlBulk.ColumnMappings.Add("Fax", "Fax");
                            sqlBulk.ColumnMappings.Add("Email", "Email");
                            sqlBulk.ColumnMappings.Add("StNo", "StNo");
                            sqlBulk.ColumnMappings.Add("CstNo", "CstNo");
                            sqlBulk.ColumnMappings.Add("Range", "Range");
                            sqlBulk.ColumnMappings.Add("Division", "Division");
                            sqlBulk.ColumnMappings.Add("PLANo", "PLANo");
                            sqlBulk.ColumnMappings.Add("EOCNo", "EOCNo");
                            sqlBulk.ColumnMappings.Add("RegnNo", "RegnNo");
                            sqlBulk.ColumnMappings.Add("PaymentTerms", "PaymentTerms");
                            sqlBulk.ColumnMappings.Add("DeliveryTerms", "DeliveryTerms");
                            sqlBulk.ColumnMappings.Add("PackingDetails", "PackingDetails");
                            sqlBulk.ColumnMappings.Add("Insurance", "Insurance");
                            sqlBulk.ColumnMappings.Add("PortOfLoading", "PortOfLoading");
                            sqlBulk.ColumnMappings.Add("DespatchThrough", "DespatchThrough");
                            sqlBulk.ColumnMappings.Add("Freight", "Freight");
                            sqlBulk.ColumnMappings.Add("FreightName", "FreightName");
                            sqlBulk.ColumnMappings.Add("Form", "Form");
                            sqlBulk.ColumnMappings.Add("FormName", "FormName");
                            sqlBulk.ColumnMappings.Add("Forwarder", "Forwarder");
                            sqlBulk.ColumnMappings.Add("BankName", "BankName");
                            sqlBulk.ColumnMappings.Add("BankAddress", "BankAddress");
                            sqlBulk.ColumnMappings.Add("BankCode", "BankCode");
                            sqlBulk.ColumnMappings.Add("SwiftCode", "SwiftCode");
                            sqlBulk.ColumnMappings.Add("AccountNumber", "AccountNumber");
                            sqlBulk.ColumnMappings.Add("IBAN", "IBAN");
                            sqlBulk.ColumnMappings.Add("OtherInformation", "OtherInformation");
                            sqlBulk.ColumnMappings.Add("IsDomestic", "IsDomestic");
                            sqlBulk.ColumnMappings.Add("IsImport", "IsImport");
                            sqlBulk.ColumnMappings.Add("MaterialMasterID", "MaterialMasterID");
                            sqlBulk.ColumnMappings.Add("MaterialGroupMasterID", "MaterialGroupMasterID");
                            sqlBulk.ColumnMappings.Add("CreatedDate", "CreatedDate");
                            sqlBulk.ColumnMappings.Add("UpdatedDate", "UpdatedDate");
                            sqlBulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulk.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                            sqlBulk.ColumnMappings.Add("IsDeleted", "IsDeleted");
                            sqlBulk.ColumnMappings.Add("DeletedBy", "DeletedBy");
                            sqlBulk.ColumnMappings.Add("DeletedDate", "DeletedDate");
                            con.Open();
                            sqlBulk.WriteToServer(table_);
                            con.Close();
                            TempData["successBody"] = "Uploaded Successfully";
                        }
                        catch (Exception ex)
                        {
                            string errorMessage = string.Empty;
                            if (ex.Message.Contains("Received an invalid column length from the bcp client for colid"))
                            {
                                // this method gives message with column name with length.  
                                errorMessage = GetBulkCopyColumnException(ex, sqlBulk);
                                // errorMessage contains "Column: "XYZ" contains data with a length greater than: 20", column, length  
                                Exception exInvlidColumn = new Exception(errorMessage, ex);
                                // LogDataAccessException(exInvlidColumn, System.Reflection.MethodBase.GetCurrentMethod().Name);
                            }
                        }
                    }
                    
                   
                }
            }


            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            return RedirectToAction("SupplierMaster");
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
        public JsonResult MaterialGroupCheckboxeails(string MaterialGroupMasterId)
        {
            MaterialNameManager materialGroupManager = new MaterialNameManager();
            materialgroupmaster materialGroupMaster = new materialgroupmaster();
            List<tbl_materialnamemaster> listMaterial = new List<tbl_materialnamemaster>();
            List<tbl_materialnamemaster> listMaterial_ = new List<tbl_materialnamemaster>();
            if (MaterialGroupMasterId != null && MaterialGroupMasterId != "")
            {
                string[] splitanme = MaterialGroupMasterId.Split(',');
                foreach (var item in splitanme)
                {
                    int GroupID = Convert.ToInt32(item);
                    tbl_materialnamemaster materialGroup = new tbl_materialnamemaster();
                    listMaterial = materialGroupManager.Get();
                    listMaterial = listMaterial.Where(x => x.MaterialGroupMasterId == GroupID).ToList();
                    if (listMaterial.Count > 0)
                    {
                        listMaterial_.AddRange(listMaterial);
                    }
                }
            }
            return Json(listMaterial_, JsonRequestBehavior.AllowGet);
        }
        public string GetSuplierMasterID()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateSupplierID();
            ID++;
            return ID.ToString();
        }
        #endregion

    }
}
