using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.StockModel;
using MMS.Repository.Managers.StockManager;
using MMS.Core.Entities;
using MMS.Common;
using MMS.Repository.Managers;
using System.Globalization;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using MMS.Core.Entities.Stock;
using MMS.Web.Models.SimpleMRPModel;
using Newtonsoft.Json;
using MMS.Web.Models;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class IndentController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Indent()
        {
            return View("~/Views/Stock/Indent/Indent.cshtml");
        }
        public ActionResult IndentGrid()
        {
            IndentModel vm = new IndentModel();
            IndentManager IndentManager = new IndentManager();
            vm.IndentList = IndentManager.Get();
            SimpleMRP simpleMRP = new SimpleMRP();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            List<SimpleMRP> _items = new List<SimpleMRP>();
            var MRPList = (from x in simpleMRPManager.Get()
                           select new { MRPNO = x.MRPNO, SimpleMRPID = x.SimpleMRPID });
            var distinctList = MRPList.GroupBy(x => x.MRPNO).Select(g => g.First()).ToList();
            foreach (var item in distinctList)
            {
                SimpleMRP mrp = new SimpleMRP();
                mrp.MRPNO = item.MRPNO;
                mrp.SimpleMRPID = item.SimpleMRPID;
                _items.Add(mrp);
            }
            vm.MRPList = _items.GroupBy(x => x.MRPNO).Select(g => g.First()).ToList();
            return PartialView("~/Views/Stock/Indent/Partial/IndentGrid.cshtml", vm);
        }

        public ActionResult IndentExample()
        {
            return View();
        }

        public JsonResult GetSelectedIOVal(int IndentId)
        {
            IndentManager indentManager = new IndentManager();
            IndentModel model = new IndentModel();
            MMS.Core.Entities.Indent indent = new Core.Entities.Indent();
            SelectList items = MMS.Web.ExtensionMethod.HtmlHelper.GetInternalOrderIONO();
            indent = indentManager.GetIndentMasterId(IndentId);
            model.IoNo = indent.IoNo;
            model.SelectedIndentNO = indent.SelectedIndentNO;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IndentDetailsView(int IndentId)
        {
            IndentManager indentManager = new IndentManager();
            IndentModel model = new IndentModel();
            MMS.Core.Entities.Indent indent = new Core.Entities.Indent();

            indent = indentManager.GetIndentMasterId(IndentId);
            tblAutoGenIndent autoIndent = new tblAutoGenIndent();
            tblAutoGenIndent autoIndentNo = new tblAutoGenIndent();
            AutoGenIndentManager autoManager = new AutoGenIndentManager();
            SimpleMRP simpleMRP = new SimpleMRP();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            List<SimpleMRP> _items = new List<SimpleMRP>();
            var MRPList = (from x in simpleMRPManager.Get()
                           select new { MRPNO = x.MRPNO, SimpleMRPID = x.SimpleMRPID });
            var distinctList = MRPList.GroupBy(x => x.MRPNO).Select(g => g.First()).ToList();
            foreach (var item in distinctList)
            {
                SimpleMRP mrp = new SimpleMRP();
                mrp.MRPNO = item.MRPNO;
                mrp.SimpleMRPID = item.SimpleMRPID;
                _items.Add(mrp);
            }
            model.MRPList = _items.GroupBy(x => x.MRPNO).Select(g => g.First()).ToList();
            if (IndentId == 0)
            {
                autoIndent = autoManager.Get().LastOrDefault();
                if (autoIndent != null)
                {
                    var autoId = Convert.ToInt32(autoIndent.AutoGenerateId);
                    autoId++;
                    autoIndentNo.IndentId = autoId.ToString();
                    autoManager.Post(autoIndentNo);
                }
                else
                {
                    autoIndentNo.IndentId = "1";
                    autoManager.Post(autoIndentNo);
                }
                model.IndentNo = autoIndentNo.AutoGenerateId.ToString();

            }
            else
            {
                autoIndent = autoManager.GetAgentFullName(indent.IndentNo);

                if (autoIndent != null)
                {
                    model.IndentNo = indent.IndentNo.ToString();
                }
                else
                {
                    autoIndentNo.IndentId = "1";
                    autoManager.Post(autoIndentNo);
                    model.IndentNo = autoIndentNo.AutoGenerateId.ToString();
                }
            }


            int ID = Convert.ToInt32(autoIndentNo.IndentId);
            if (indent != null && indent.IndentId != 0)
            {
                model.IndentId = indent.IndentId;
                model.AmmendmentNo = indent.AmmendmentNo;
                model.AmmendmentDate = Convert.ToDateTime(indent.AmmendmentDate).Date.ToString("dd/MM/yyyy");
                model.IndentType = indent.IndentType;
                model.IoNo = indent.IoNo;
                if (model.To != null)
                {
                    foreach (var item in model.To)
                    {
                        indent.SelectedIndentNO += item + ",";
                    }
                    indent.SelectedIndentNO = indent.SelectedIndentNO.TrimEnd(',');
                }


                model.UnitId = indent.UnitId;
                model.MaterialType = indent.MaterialType;
                model.Indendingfor = indent.Indendingfor;
                model.MaterialCatId = indent.MaterialCatId;
                model.Date = Convert.ToDateTime(indent.Date).Date.ToString("dd/MM/yyyy");
                model.SeasonId = indent.SeasonId;
                model.MaterialGrpId = indent.MaterialGrpId;
                model.StoreId = indent.StoreId;
                model.MRPNO = indent.MRPNO.ToString();
                model.BuyerId = indent.BuyerId;
                model.RequestedBy = indent.RequestedBy;
                model.UOMId = indent.UOMId;
                model.ReqQty = indent.ReqQty;

                model.StoreStock = indent.StoreStock;
                model.MaterialName = indent.MaterialId;
                model.Rate = indent.Rate;
                model.IntendQty = indent.IntendQty;
                model.FreeStock = indent.FreeStock;
                model.ColourId = indent.ColourId;
                model.OrderTypeID = indent.OrderTypeID;
                model.Value = indent.Value;
                model.SupplierId = indent.SupplierId;
                model.Remarks = indent.Remarks;
                model.PreparedBy = indent.PreparedBy;
                model.CheckedBy = indent.CheckedBy;
                model.ApprovedBy = indent.ApprovedBy;
                GRNTypeManager grnTypeManager = new GRNTypeManager();
                GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
                IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                List<IndentMaterials> listIndentMaterials = new List<IndentMaterials>();
                listIndentMaterials = indentMaterialManager.GetIndentID(IndentId);
                grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(indent.OrderTypeID));
                if (grnTypeMaster.IssueType != "General")
                {
                    if (grnTypeMaster != null && grnTypeMaster.GrnTypeMasterId != 0)
                    {
                        model.OrderType = grnTypeMaster.IssueType;
                        if (grnTypeMaster.IssueType == "Order")
                        {
                            model.MRPNO = indent.MRPNO.ToString();
                        }
                        else
                        {
                            model.MRPNO_txtValue = indent.MRPNO.ToString();
                        }
                        model.OrderTypeID = grnTypeMaster.GrnTypeMasterId;
                    }
                    if (model.MRPNO != "")
                    {
                        List<IndentMaterials> bomMaterialList = new List<IndentMaterials>();
                        simpleMRP = simpleMRPManager.GetMRPNO(model.MRPNO);
                        IndentMaterials isExistIndentMaterials = new IndentMaterials();
                        List<IndentMaterials> indentMaterialList = new List<IndentMaterials>();
                        indentMaterialList = indentMaterialManager.GetIndentID(indent.IndentId);
                        if (((simpleMRP != null && simpleMRP.MRPNO != "") || (indentMaterialList.Count > 0 || indentMaterialList.Count == 0)))
                        {
                            if (indentMaterialList.Count > 0)
                            {
                                bomMaterialList = indentManager.IndentMaterialLists(model.MRPNO).ToList();
                            }
                            else
                            {
                                if (simpleMRP != null && simpleMRP.SimpleMRPID != 0)
                                {
                                    bomMaterialList = indentManager.IndentMaterialLists(simpleMRP.MRPNO).ToList();
                                }

                            }

                        }
                        model.IndentMaterialsList = bomMaterialList;

                    }
                }

                else if (grnTypeMaster.IssueType == "General" && (model.MRPNO == "0" || model.MRPNO == null) && listIndentMaterials != null && listIndentMaterials.Count > 0)
                {
                    model.IndentMaterialsList = listIndentMaterials;
                }
                else if (grnTypeMaster.IssueType == "General" && listIndentMaterials != null && listIndentMaterials.Count > 0)
                {
                    model.IndentMaterialsList = listIndentMaterials;
                }
                else if (model != null || model.MRPNO == string.Empty || (model.MRPNO) == "0")
                {
                    List<Indent> indentList = new List<Core.Entities.Indent>();
                    indentList = indentManager.Get().Where(x => x.IndentNo == indent.IndentNo).ToList();
                    model.IndentMateriallist = indentList;

                }

            }

            return PartialView("~/Views/Stock/Indent/Partial/IndentDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult Indent(IndentModel model)
        {
            bool Result = false;
            IndentManager Manager = new IndentManager();
            MMS.Core.Entities.Indent indent = new Core.Entities.Indent();
            MMS.Core.Entities.Indent indent_ = new Core.Entities.Indent();
            indent_ = Manager.GetIndentMasterId(model.IndentId);

            if (model.OrderType != null && model.OrderType != "")
            {
                indent.OrderTypeID = Convert.ToInt32(model.OrderType);
            }
            else
            {
                indent.OrderTypeID = Convert.ToInt32(model.OrderTypeID);
            }
            indent.MaterialType = model.MaterialType;
            if (model.IndentId == 0)
            {
                indent.IndentId = model.IndentId;
                indent.AmmendmentNo = model.AmmendmentNo;
                var format = "dd/MM/yyyy";
                if (model.AmmendmentDate != null && model.AmmendmentDate != string.Empty)
                {
                    DateTime intAmndtDate = DateTime.ParseExact(model.AmmendmentDate, format, CultureInfo.InvariantCulture);
                    indent.AmmendmentDate = intAmndtDate.Date;
                }
                indent.IndentType = model.IndentType;
                indent.IndentNo = model.IndentNo;
                indent.IoNo = model.IoNo;

                if (model.MRPSelectedValues != null)
                {
                    string[] MRPArray = model.MRPSelectedValues.Split(',');
                    foreach (var item in MRPArray)
                    {
                        if (item == "") { } else { indent.MRPNO += item + ","; }
                    }
                }
                if (indent.MRPNO != null)
                {
                    indent.MRPNO = indent.MRPNO.TrimEnd(',');
                }
                if (model.To != null)
                {
                    foreach (var item in model.To)
                    {
                        indent.SelectedIndentNO += item + ",";
                    }
                }
                if (model.SelectedIndentNO != null)
                {
                    indent.SelectedIndentNO = indent.SelectedIndentNO.TrimEnd(',');
                }

                indent.UnitId = model.UnitId;
                indent.Indendingfor = model.Indendingfor;
                indent.MaterialCatId = model.MaterialCatId;
                if (model.Date != null && model.Date != string.Empty)
                {
                    DateTime indate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    indent.Date = indate.Date;
                }
                indent.SeasonId = model.SeasonId;
                indent.MaterialGrpId = model.MaterialGrpId;
                indent.StoreId = model.StoreId;
                indent.BuyerId = model.BuyerId;
                indent.RequestedBy = model.RequestedBy;
                indent.UOMId = model.UOMId;
                indent.ReqQty = model.ReqQty;
                indent.StoreStock = model.StoreStock;
                indent.MaterialId = model.MaterialName;
                indent.Rate = model.Rate;
                indent.IntendQty = model.IntendQty;
                indent.FreeStock = model.FreeStock;
                indent.ColourId = model.ColourId;
                indent.Value = model.Value;
                indent.SupplierId = model.SupplierId;
                indent.Remarks = model.Remarks;
                string username = Session["UserName"].ToString();
                indent.PreparedBy = username;
                indent.CheckedBy = model.CheckedBy;
                indent.ApprovedBy = model.ApprovedBy;

            }
            else if (model.IndentId != 0)
            {
                indent = Manager.GetIndentMasterId(model.IndentId);
                indent.IndentId = model.IndentId;
                indent.AmmendmentNo = model.AmmendmentNo;

                if (indent.MRPNO != null)
                {
                    indent.MRPNO = indent.MRPNO.TrimEnd(',');
                }
                var format = "dd/MM/yyyy";
                if (model.AmmendmentDate != null && model.AmmendmentDate != string.Empty)
                {
                    DateTime intAmndtDate = DateTime.ParseExact(model.AmmendmentDate, format, CultureInfo.InvariantCulture);
                    indent.AmmendmentDate = intAmndtDate.Date;
                }
                indent.IndentType = model.IndentType;
                indent.IndentNo = indent.IndentNo;
                indent.IoNo = model.IoNo;
                if (model.To != null)
                {
                    foreach (var item in model.To)
                    {
                        indent.SelectedIndentNO += item + ",";
                    }
                }
                if (model.SelectedIndentNO != null)
                {
                    indent.SelectedIndentNO = indent.SelectedIndentNO.TrimEnd(',');
                }
                indent.UnitId = model.UnitId;
                indent.Indendingfor = model.Indendingfor;
                indent.MaterialCatId = model.MaterialCatId;
                if (model.Date != null && model.Date != string.Empty)
                {
                    DateTime indate = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);
                    indent.Date = indate.Date;
                }
                indent.SeasonId = model.SeasonId;
                indent.MaterialGrpId = model.MaterialGrpId;
                indent.StoreId = model.StoreId;
                indent.BuyerId = model.BuyerId;
                indent.RequestedBy = model.RequestedBy;
                indent.UOMId = model.UOMId;
                indent.ReqQty = model.ReqQty;
                indent.StoreStock = model.StoreStock;
                indent.MaterialId = model.MaterialName;
                indent.Rate = model.Rate;
                indent.IntendQty = model.IntendQty;
                indent.FreeStock = model.FreeStock;
                indent.ColourId = model.ColourId;
                indent.Value = model.Value;
                indent.SupplierId = model.SupplierId;
                indent.Remarks = model.Remarks;
                indent.PreparedBy = indent_.PreparedBy;
                indent.CheckedBy = model.CheckedBy;
                indent.ApprovedBy = model.ApprovedBy;

            }
            Result = Manager.Post(indent, model.MRPNO);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMaterial(int IndentMaterialID)
        {
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();

            List<IndentMaterials> listIndentMaterial = new List<IndentMaterials>();
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialManager.GetIndentMaterialId(IndentMaterialID);
            bool isDeleted = indentMaterialManager.Delete(IndentMaterialID);
            listIndentMaterial = indentMaterialManager.Get(indentMaterial.MRPNO);
            return Json(listIndentMaterial, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(IndentModel model)
        {
            IndentManager IndentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            string status = "";
            CompanyManager companyManager = new CompanyManager();
            StoreMasterManager storeManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            List<Company> listCompany = new List<Company>();
            listCompany = companyManager.Get();

            MMS.Core.Entities.Indent IndentMaster = IndentManager.GetIndentMasterId(model.IndentId);
            //       storeMaster = storeManager.GetStoreMasterId(IndentMaster.StoreId.Value);
            if (IndentMaster != null)
            {
                status = "Success";
                indentMaterialManager.DeleteIndentID(IndentMaster.IndentId);
                IndentManager.Delete(IndentMaster.IndentId);
                EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
                MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                EmailTemplate emailTemplate = new EmailTemplate();
                emailTemplate = emailTemplateManager.GetTemplateName("Indent Delete");
                if (emailTemplate != null)
                {
                    string contents = emailTemplate.Body;
                    MaterialManager materialManager = new MaterialManager();
                    contents = contents.Replace("[[IndentNo]]", !string.IsNullOrEmpty(IndentMaster.IndentNo) ? IndentMaster.IndentNo : string.Empty);
                    contents = contents.Replace("[[MaterialName]]", "Above indent all the materials has been deleted");
                    contents = contents.Replace("[[UserName]]", Session["UserName"].ToString());
                    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    contents = contents.Replace("[[StoreName]]", storeMaster != null ? storeMaster.StoreName != null ? storeMaster.StoreName : string.Empty : string.Empty);
                    emailTemplate.Body = contents;
                    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public string GetIndentNo()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateIndentNo();
            ID++;
            return ID.ToString();
        }

        public ActionResult FillMaterialName(int MaterialGroupMasterId)
        {
            List<tbl_materialnamemaster> materialNameMasterList = new List<tbl_materialnamemaster>();
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            MaterialManager materialManager = new MaterialManager();
            ColorManager colorManager = new ColorManager();

            var items = (from x in materialManager.Get()
                         join y in materialNameManager.Get()
                          on x.MaterialName equals y.MaterialMasterID
                         join z in colorManager.Get()
                         on x.ColorMasterId equals z.ColorMasterId
                         where x.MaterialGroupMasterId == MaterialGroupMasterId
                         select new { MaterialDescription = string.Format("{0} {1} {2}", y.MaterialDescription, x.OptionMaterialValue, z.Color != null ? z.Color : ""), x.MaterialMasterId, x.Price, z.ColorMasterId, x.Uom, x.UomUnit, x.SizeRangeMasterId });

            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();
            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillColorName(int MaterialmasterId)
        {
            MaterialNameManager materialNameManager = new MaterialNameManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialCategoryManager materialCategoryManager = new MaterialCategoryManager();
            ColorManager colorManager = new ColorManager();
            var items = (from x in materialNameManager.Get()
                         join y in materialManager.Get()
                          on x.MaterialMasterID equals y.MaterialName
                         join z in colorManager.Get()
                         on y.ColorMasterId equals z.ColorMasterId
                         where y.MaterialMasterId == MaterialmasterId
                         select new { MaterialDescription = string.Format("{0} {1} {2} {3}", x.MaterialDescription, y.OptionMaterialValue, y.MaterialCode, z.Color), y.MaterialCode, y.MaterialMasterId, z.ColorMasterId, y.SizeRangeMasterId, y.Price, y.Uom });
            var distinctList = items.GroupBy(x => x.MaterialDescription).Select(g => g.First()).ToList();

            return Json(distinctList, JsonRequestBehavior.AllowGet);
        }

        #region Helper Method

        [HttpGet]
        public ActionResult GETMRPMaterialList(string MRPNO)
        {
            IndentManager indentManager = new IndentManager();
            List<MMS.Web.Models.SPBomMaterialList> spBOMMaterialList = new List<Models.SPBomMaterialList>();
            spBOMMaterialList = indentManager.GetBOMMaterial(MRPNO).ToList();
            var jsonResult = Json(spBOMMaterialList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public ActionResult subGETMRPMaterialList(string MRPNO)
        {
            IndentManager indentManager = new IndentManager();
            List<MMS.Web.Models.subspbommateriallist> spBOMMaterialList = new List<Models.subspbommateriallist>();
            spBOMMaterialList = indentManager.subGetBOMMaterial(MRPNO).ToList();
            var jsonResult = Json(spBOMMaterialList, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public ActionResult ShowMaterialList(string MRPNO, int? MaterialCategoryID, int? SeasonId, int? MaterialGroupMasterId, int? MaterialType, int? StoreId)
        {
            IndentManager indentManager = new IndentManager();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();

            string[] MRPArray = MRPNO.Split(',');
            MRPNO = "";
            decimal? TotalIndentPairs = 0;
            foreach (var item in MRPArray)
            {
                SimpleMRP simpleMRP = new SimpleMRP();
                if (item == "")
                {

                }
                else
                {
                    simpleMRP = simpleMRPManager.GetMRPNO(item);
                    TotalIndentPairs += simpleMRP != null ? simpleMRP.TotalOrderCount : 0;
                    MRPNO += item + ",";
                }
            }
            MRPNO = MRPNO.TrimEnd(',');
            List<MMS.Web.Models.subspbommateriallist> spBOMMaterialList = new List<Models.subspbommateriallist>();

            spBOMMaterialList = indentManager.subGetBOMMaterial(MRPNO).ToList();
            List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            indentMaterials = indentMaterialManager.IndentRaise(MRPNO);
            indentMaterials = indentMaterials.Where(x => x.IndentID == 0).ToList();
            if (indentMaterials.Count > 0)
            {
                var jsonResult = Json(indentMaterials, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                var query = spBOMMaterialList.ToList();
                DataTable dt = new DataTable("MyTable");
                dt = ConvertToDataTable(query);
                dt.Columns.Add("IndentID", typeof(System.Int32));
                dt.Columns.Add("CreatedDate", typeof(System.DateTime));
                dt.Columns.Add("UpdatedDate", typeof(System.DateTime));
                dt.Columns.Add("Value", typeof(System.Decimal));
                dt.Columns.Add("IndentQTY", typeof(System.Decimal));
                dt.Columns.Add("bomid", typeof(System.Int32)); 
                dt.Columns.Add("orderentryid", typeof(System.Int32)); 
                dt.Columns.Add("uommasterid", typeof(System.Int32)); 
                dt.Columns.Add("supplierid", typeof(System.Int32)); 
                dt.Columns.Add("buyerpono", typeof(System.String)); 
                dt.Columns.Add("suppliermastername", typeof(System.String));
                List<IndentMaterialsList> BOMMaterialList = new List<IndentMaterialsList>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];

                    List<MMS.Web.Models.PendingQty> pendingList = new List<PendingQty>();
                    IndentMaterialsList spBomMaterialList = new IndentMaterialsList();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialManager materialManager = new MaterialManager();

                    materialMaster = materialManager.GetMaterialMasterId(Convert.ToInt32(row["materialmasterid"].ToString())); // Replace "YourColumnName" with the actual column name
                    if (materialMaster != null)
                    {
                        pendingList = materialManager.MaterialOpeningStock(materialMaster.MaterialMasterId);
                    }
                    spBomMaterialList.CategoryName = row["categoryname"].ToString();
                    spBomMaterialList.GroupName = row["groupname"].ToString();
                    spBomMaterialList.MaterialDescription = row["materialdescription"].ToString();
                    spBomMaterialList.Color = row["color"].ToString();
                    spBomMaterialList.WastageName = row["wastagename"].ToString();
                    spBomMaterialList.TotalNormsName = row["totalnorms"].ToString();
                    spBomMaterialList.TotalIndentPairs = TotalIndentPairs.ToString();

                    if (!string.IsNullOrEmpty(row["freestock"].ToString()))
                    {
                        spBomMaterialList.SampleNorms = Convert.ToDecimal(row["freestock"].ToString());
                    }
                    else
                    {
                        spBomMaterialList.SampleNorms = 0;
                    }

                    if (!string.IsNullOrEmpty(row["wastage"].ToString()))
                    {
                        spBomMaterialList.Wastage = Convert.ToDecimal(row["wastage"].ToString());
                    }
                    else
                    {
                        spBomMaterialList.Wastage = 0;
                    }

                    if (!string.IsNullOrEmpty(row["wastageqty"].ToString()))
                    {
                        spBomMaterialList.WastageQty = Convert.ToDecimal(row["wastageqty"].ToString());
                    }
                    else
                    {
                        spBomMaterialList.WastageQty = 0;
                    }

                    spBomMaterialList.WastageQtyUOM = Convert.ToInt32(row["wastageqtyuom"].ToString());

                    if (!string.IsNullOrEmpty(row["totalnorms"].ToString()))
                    {
                        spBomMaterialList.TotalNorms = Convert.ToDecimal(row["totalnorms"].ToString());
                    }
                    else
                    {
                        spBomMaterialList.TotalNorms = 0;
                    }

                    spBomMaterialList.MRPNO = MRPNO;
                    spBomMaterialList.MaterialCategoryMasterId = Convert.ToInt32(row["materialcategorymasterid"].ToString());
                    spBomMaterialList.MaterialGroupMasterId = Convert.ToInt32(row["materialgroupmasterid"].ToString());
                    spBomMaterialList.MaterialMasterID = Convert.ToInt32(row["materialmasterid"].ToString());
                    spBomMaterialList.SubstanceRange = row["substancerange"].ToString();
                    spBomMaterialList.SubstanceMasterId = !string.IsNullOrEmpty(row["substancemasterid"].ToString()) ? Convert.ToInt32(row["substancemasterid"].ToString()) : 0; //Convert.ToInt32(row["substancemasterid"].ToString());
                    spBomMaterialList.BOMMaterialID = !string.IsNullOrEmpty(row["bommaterialid"].ToString()) ? Convert.ToInt32(row["bommaterialid"].ToString()) : 0;
                    spBomMaterialList.BOMID = !string.IsNullOrEmpty(row["bomid"].ToString()) ? Convert.ToInt32(row["bomid"].ToString()) : 0;
                    spBomMaterialList.ColorMasterID = Convert.ToInt32(row["colormasterid"].ToString());
                    spBomMaterialList.BuyerSeason = !string.IsNullOrEmpty(row["buyerseason"].ToString()) ? Convert.ToInt32(row["buyerseason"].ToString()) : 0;
                    spBomMaterialList.SeasonName = row["seasonname"].ToString();
                    spBomMaterialList.RequiredQty = 0;
                    spBomMaterialList.RequiredQty = Math.Round(Convert.ToDecimal(row["requiredqty"].ToString()), 0, MidpointRounding.AwayFromZero);

                    if (spBomMaterialList.MaterialMasterID == 548)
                    {
                        string Message = "";
                    }

                    spBomMaterialList.IndentQTY = spBomMaterialList.RequiredQty;
                    spBomMaterialList.OrderEntryId = !string.IsNullOrEmpty(row["orderentryid"].ToString()) ? Convert.ToInt32(row["orderentryid"].ToString()) : 0;
                    spBomMaterialList.UomMasterId = !string.IsNullOrEmpty(row["uommasterid"].ToString()) ? Convert.ToInt32(row["uommasterid"].ToString()) : 0;
                    spBomMaterialList.BuyerFullName = row["buyerfullname"].ToString();
                    spBomMaterialList.TotalPairs = row["totalpairs"].ToString();

                    if (!string.IsNullOrEmpty(row["StoreMasterId"].ToString()))
                    {
                        spBomMaterialList.StoreId = Convert.ToInt32(row["StoreMasterId"].ToString());
                    }

                    if (!string.IsNullOrEmpty(row["supplierid"].ToString()))
                    {
                        spBomMaterialList.SupplierId = Convert.ToInt32(row["supplierid"].ToString());
                    }
                    else
                    {
                        spBomMaterialList.SupplierId = null;
                    }

                    spBomMaterialList.BuyerPoNo = !string.IsNullOrEmpty(row["buyerpono"].ToString()) ? row["buyerpono"].ToString() : "0"; 
                    spBomMaterialList.Price = row["rate"].ToString();
                    spBomMaterialList.SupplierMasterName = row["suppliermastername"].ToString();

                    if (!string.IsNullOrEmpty(row["materialmasterid"].ToString()))
                    {
                        if (materialMaster != null && (materialMaster.PurchasePacket == null || materialMaster.PurchasePacket == 0))
                        {
                            spBomMaterialList.Value = (Convert.ToDecimal(row["requiredqty"].ToString()) * Convert.ToDecimal(row["rate"].ToString()));
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(row["rate"].ToString()))
                            {
                                spBomMaterialList.Value = (spBomMaterialList.RequiredQty * 0);
                            }
                            else
                            {
                                spBomMaterialList.Value = (spBomMaterialList.RequiredQty * Convert.ToDecimal(row["rate"].ToString()));
                            }
                        }
                    }
                    else
                    {
                        spBomMaterialList.Value = 0;
                    }

                    if (pendingList.Count > 0)
                    {
                        spBomMaterialList.StoreStock = pendingList.FirstOrDefault().BalanceStock;
                    }
                    spBomMaterialList.BuyerMasterId = Convert.ToInt32(row["buyermasterid"].ToString());

                    BOMMaterialList.Add(spBomMaterialList);
                }


                if (MaterialCategoryID != null)
                {
                    BOMMaterialList = BOMMaterialList.Where(x => x.MaterialCategoryMasterId == MaterialCategoryID).ToList();
                }
                if (StoreId != null)
                {
                    BOMMaterialList = BOMMaterialList.Where(x => x.StoreId == StoreId).ToList();
                }
                if (SeasonId != null)
                {
                    BOMMaterialList = BOMMaterialList.Where(x => x.BuyerSeason == SeasonId).ToList();
                }
                if (MaterialGroupMasterId != null)
                {
                    BOMMaterialList = BOMMaterialList.Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId).ToList();
                }
                Session["MRPMaterial"] = null;
                Session["MRPMaterial"] = BOMMaterialList;
                var jsonResult = Json(BOMMaterialList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

        [HttpGet]
        public ActionResult GetStoreMaterialList(string MRPNO, string StoreId, string MaterialType)
        {
            IndentManager indentManager = new IndentManager();
            string[] MRPArray = MRPNO.Split(',');
            MRPNO = "";
            foreach (var item in MRPArray)
            {
                if (item == "")
                {

                }
                else
                {
                    MRPNO += item + ",";
                }
            }
            MRPNO = MRPNO.TrimEnd(',');
            List<MMS.Web.Models.SPBomMaterialList> spBOMMaterialList = new List<Models.SPBomMaterialList>();
            spBOMMaterialList = indentManager.GetBOMMaterial(MRPNO).ToList();
            if (StoreId != "" && StoreId != null)
            {
                spBOMMaterialList = spBOMMaterialList.Where(x => x.StoreMasterId == Convert.ToInt32(StoreId)).ToList();
            }
            List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            indentMaterials = indentMaterialManager.IndentRaise(MRPNO);
            indentMaterials = indentMaterials.Where(x => x.IndentID == 0).ToList();
            if (indentMaterials.Count > 0)
            {
                var jsonResult = Json(indentMaterials, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                var query = spBOMMaterialList.ToList();
                DataTable dt = new DataTable("MyTable");
                dt = ConvertToDataTable(query);
                dt.Columns.Add("IndentID", typeof(System.Int32));
                dt.Columns.Add("CreatedDate", typeof(System.DateTime));
                dt.Columns.Add("UpdatedDate", typeof(System.DateTime));
                dt.Columns.Add("Value", typeof(System.Decimal));
                dt.Columns.Add("IndentQTY", typeof(System.Decimal));
                List<IndentMaterialsList> BOMMaterialList = new List<IndentMaterialsList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IndentMaterialsList spBomMaterialList = new IndentMaterialsList();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialManager materialManager = new MaterialManager();
                    materialMaster = materialManager.GetMaterialMasterId(Convert.ToInt32(dt.Rows[i].ItemArray[13].ToString()));
                    spBomMaterialList.CategoryName = dt.Rows[i].ItemArray[0].ToString();
                    spBomMaterialList.GroupName = dt.Rows[i].ItemArray[2].ToString();
                    spBomMaterialList.MaterialDescription = dt.Rows[i].ItemArray[14].ToString();
                    spBomMaterialList.Color = dt.Rows[i].ItemArray[3].ToString();
                    spBomMaterialList.WastageName = dt.Rows[i].ItemArray[19].ToString();
                    spBomMaterialList.TotalNormsName = dt.Rows[i].ItemArray[20].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[5].ToString()))
                    {
                        spBomMaterialList.SampleNorms = Convert.ToDecimal(dt.Rows[i].ItemArray[5].ToString());
                    }
                    else
                    {
                        spBomMaterialList.SampleNorms = 0;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[6].ToString()))
                    {
                        spBomMaterialList.Wastage = Convert.ToDecimal(dt.Rows[i].ItemArray[6].ToString());
                    }
                    else
                    {
                        spBomMaterialList.Wastage = 0;
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[7].ToString()))
                    {
                        spBomMaterialList.WastageQty = Convert.ToDecimal(dt.Rows[i].ItemArray[7].ToString());
                    }
                    else
                    {
                        spBomMaterialList.WastageQty = 0;
                    }

                    spBomMaterialList.WastageQtyUOM = Convert.ToInt32(dt.Rows[i].ItemArray[10].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[8].ToString()))
                    {
                        spBomMaterialList.TotalNorms = Convert.ToDecimal(dt.Rows[i].ItemArray[8].ToString());
                    }
                    else
                    {
                        spBomMaterialList.TotalNorms = 0;
                    }

                    spBomMaterialList.MRPNO = MRPNO;
                    spBomMaterialList.MaterialCategoryMasterId = Convert.ToInt32(dt.Rows[i].ItemArray[11].ToString());
                    spBomMaterialList.MaterialGroupMasterId = Convert.ToInt32(dt.Rows[i].ItemArray[12].ToString());
                    spBomMaterialList.MaterialMasterID = Convert.ToInt32(dt.Rows[i].ItemArray[13].ToString());
                    spBomMaterialList.SubstanceRange = dt.Rows[i].ItemArray[4].ToString();
                    spBomMaterialList.SubstanceMasterId = Convert.ToInt32(dt.Rows[i].ItemArray[16].ToString());
                    spBomMaterialList.BOMMaterialID = Convert.ToInt32(dt.Rows[i].ItemArray[17].ToString());
                    spBomMaterialList.BOMID = Convert.ToInt32(dt.Rows[i].ItemArray[18].ToString());
                    spBomMaterialList.ColorMasterID = Convert.ToInt32(dt.Rows[i].ItemArray[15].ToString());
                    spBomMaterialList.BuyerSeason = Convert.ToInt32(dt.Rows[i].ItemArray[21].ToString());
                    spBomMaterialList.SeasonName = dt.Rows[i].ItemArray[22].ToString();
                    decimal? Qty = 0;
                    string calulationQty = "";
                    if (materialMaster != null && materialMaster.PurchasePacket != null && materialMaster.PurchasePacket != 0)
                    {
                        calulationQty = String.Format("{0:0.00}", (Convert.ToDecimal(dt.Rows[i].ItemArray[24].ToString()) / materialMaster.PurchasePacket).ToString());
                        Qty = Convert.ToDecimal(calulationQty);
                    }
                    if (materialMaster != null && (materialMaster.PurchasePacket == null || materialMaster.PurchasePacket == 0))
                    {
                        spBomMaterialList.RequiredQty = Convert.ToDecimal(dt.Rows[i].ItemArray[24].ToString());
                    }
                    else
                    {
                        spBomMaterialList.RequiredQty = Qty;
                    }

                    spBomMaterialList.OrderEntryId = Convert.ToInt32(dt.Rows[i].ItemArray[25].ToString());
                    spBomMaterialList.UomMasterId = Convert.ToInt32(dt.Rows[i].ItemArray[29].ToString());
                    spBomMaterialList.BuyerFullName = dt.Rows[i].ItemArray[27].ToString();
                    spBomMaterialList.TotalPairs = dt.Rows[i].ItemArray[45].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[30].ToString()))
                    {
                        spBomMaterialList.SupplierId = Convert.ToInt32(dt.Rows[i].ItemArray[30]);
                    }
                    else
                    {
                        spBomMaterialList.SupplierId = null;
                    }
                    spBomMaterialList.BuyerPoNo = dt.Rows[i].ItemArray[41].ToString();
                    spBomMaterialList.Price = dt.Rows[i].ItemArray[37].ToString();
                    spBomMaterialList.SupplierMasterName = dt.Rows[i].ItemArray[31].ToString();
                    spBomMaterialList.DirectImport = materialMaster.isImport;
                    spBomMaterialList.Local = materialMaster.isLocal;
                    spBomMaterialList.CustomerImport = materialMaster.isImportCustomer;
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[32].ToString()))
                    {
                        if (materialMaster != null && (materialMaster.PurchasePacket == null || materialMaster.PurchasePacket == 0))
                        {
                            spBomMaterialList.Value = (Convert.ToDecimal(dt.Rows[i].ItemArray[24].ToString()) * Convert.ToDecimal(dt.Rows[i].ItemArray[37].ToString()));
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i].ItemArray[37].ToString()))
                            {
                                spBomMaterialList.Value = (Qty * 0);
                            }
                            else
                            {
                                spBomMaterialList.Value = (Qty * Convert.ToDecimal(dt.Rows[i].ItemArray[37].ToString()));
                            }
                        }
                    }
                    else
                    {
                        spBomMaterialList.Value = 0;
                    }
                    spBomMaterialList.BuyerMasterId = Convert.ToInt32(dt.Rows[i].ItemArray[28].ToString());
                    BOMMaterialList.Add(spBomMaterialList);
                }
                if (MaterialType != "" && MaterialType != null && MaterialType == "1")
                {
                    BOMMaterialList = BOMMaterialList.Where(x => x.Local == true).ToList();
                }
                else if (MaterialType != "" && MaterialType != null && MaterialType == "2")
                {
                    BOMMaterialList = BOMMaterialList.Where(x => x.CustomerImport == true).ToList();
                }
                else if (MaterialType != "" && MaterialType != null && MaterialType == "3")
                {
                    BOMMaterialList = BOMMaterialList.Where(x => x.DirectImport == true).ToList();
                }
                Session["MRPMaterial"] = null;
                Session["MRPMaterial"] = BOMMaterialList.ToList();
                var jsonResult = Json(BOMMaterialList, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpPost]
        public ActionResult ShowCategoryWithMRONOMaterialList(string MRPNO, int? MaterialCategoryID, int? SeasonId)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            List<IndentMaterials> indentMaterials_ = new List<IndentMaterials>();
            indentMaterials_ = indentMaterialManager.Get(MRPNO).Where(x => x.MaterialCategoryMasterId == MaterialCategoryID && x.BuyerSeason == SeasonId).ToList();

            return Json(indentMaterials_, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ShowGroupWithMRONOMaterialList(string MRPNO, int MaterialGroupMasterId, int SeasonId)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            List<IndentMaterials> indentMaterials_ = new List<IndentMaterials>();
            indentMaterials_ = indentMaterialManager.Get(MRPNO).Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId && x.BuyerSeason == SeasonId).ToList();

            return Json(indentMaterials_, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ShowSeasonWithMRONOMaterialList(int SeasonId_, string MRPNO_)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            List<IndentMaterials> indentMaterials_ = new List<IndentMaterials>();
            indentMaterials_ = indentMaterialManager.Get(MRPNO_).Where(x => x.BuyerSeason == SeasonId_).ToList();

            return Json(indentMaterials_, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetLastMRPNO()
        {
            string ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateSimpleMRPID();
            if (ID == "")
            {
                ID = "0";
            }
            int mrpID = Convert.ToInt32(ID);
            mrpID++;
            return Json(mrpID, JsonRequestBehavior.AllowGet);
        }
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        [HttpPost]
        public ActionResult SaveIndentMaterial(IndentMaterials materials, string SizeRangeDetails)
        {
            string StatusMessage = "";
            try
            {
                string[] MRPArray = materials.MRPNO.Split(',');
                materials.MRPNO = "";
                string MRPNO = "";
                foreach (var item in MRPArray)
                {
                    if (item == "")
                    {

                    }
                    else
                    {
                        MRPNO += item + ",";
                    }
                }
                materials.MRPNO = MRPNO.TrimEnd(',');
                IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                indentMaterialManager.Post(materials);
                StatusMessage = "Updated Successfully";

                if (SizeRangeDetails != null)
                {
                    var SizeDetails = JsonConvert.DeserializeObject<List<IndentSizeRangeQty>>(SizeRangeDetails);
                    int count1 = 1;
                    foreach (var item in SizeDetails)
                    {
                        IndentSizeRangeQty indentSizeRangeQty = new IndentSizeRangeQty();
                        indentSizeRangeQty.Size = item.Size;
                        indentSizeRangeQty.quantity = Convert.ToInt32(item.quantity);
                        indentSizeRangeQty.Rate = item.Rate;
                        indentSizeRangeQty.IdentSizeRangeID = 0;
                        indentSizeRangeQty.IndentMaterialID = materials.IndentMaterialID;
                        List<IndentSizeRangeQty> listPurchaseOrderSizeRangeQuantity = new List<IndentSizeRangeQty>();
                        listPurchaseOrderSizeRangeQuantity = indentMaterialManager.IndentSizeRangeQtyGet().Where(x => x.IndentMaterialID == materials.IndentMaterialID).ToList();
                        if (count1 == 1)
                        {
                            indentMaterialManager.IndentSizeRangeQtyDelete(materials.IndentMaterialID);
                            count1++;
                        }
                        indentMaterialManager.IndentSizeRangeDetailsPost(indentSizeRangeQty);
                    }
                }
                List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
                indentMaterials = indentMaterialManager.Get(materials.MRPNO);
                decimal? TotalAmount = indentMaterials.Sum(x => x.IndentQTY);
                return Json(new { indentMaterials = indentMaterials, TotalAmount = TotalAmount, StatusMessage = StatusMessage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message.ToString();
            }
            return Json(new { StatusMessage = StatusMessage }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveIndentMaterialGeneral(IndentMaterials materials, string UnitId, string IndentType, string Indendingfor, string hiddenIndentId, string BuyerSeason, string SizeRangeDetails, int OrderTypeID, string IndentNo_)
        {
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            IndentManager indentManager = new IndentManager();
            Indent indent = new Indent();
            Indent indent_ = new Indent();
            indent.Indendingfor = Indendingfor;
            indent.UnitId = Guid.Parse(UnitId);
            indent.IndentType = IndentType;
            if (hiddenIndentId == "0" || hiddenIndentId == null || hiddenIndentId == "")
            {
                if (materials.IndentID == 0)
                {
                    indent.IndentNo = IndentNo_;
                }
                else
                {
                    indent.IndentId = materials.IndentID.Value;
                }
            }
            else
            {
                indent.IndentId = Convert.ToInt32(hiddenIndentId);
            }
            indent.MRPNO = (materials.MRPNO);
            //Simle MRP entry start
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            SimpleMRP simpleMRP = new SimpleMRP();
            SimpleMRPModel model = new SimpleMRPModel();
            SimpleMRP ischeck = new SimpleMRP();
            //string Message = "";

            SimpleMRP simmplemrp = new SimpleMRP();
            simmplemrp.MRPType = 1;
            simmplemrp.BuyerNameid = materials.BuyerMasterId;
            simmplemrp.SeasonID = materials.BuyerSeason;
            simmplemrp.CustomerPlan = 1;
            simpleMRP = simpleMRPManager.GetMRPNO(materials.MRPNO);
            DateTime date = DateTime.Now;
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int? weekNum_ = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialManager.GetIndentMaterialId(materials.IndentMaterialID);
            simmplemrp.WeekNO = weekNum_;

            if (OrderTypeID != 7)
            {

                if (simpleMRP == null && indentMaterial == null)
                {
                    ischeck = simpleMRPManager.SimpleMRPPost(simmplemrp);
                }
                else
                {
                    ischeck = simpleMRPManager.GetMRPNO(indentMaterial.MRPNO);
                    if (ischeck != null)
                    {
                        simmplemrp.SimpleMRPID = ischeck.SimpleMRPID;
                        ischeck = simpleMRPManager.Put(simmplemrp);
                    }


                }
            }

            //SimpleMRP entry stop
            indent.OrderTypeID = OrderTypeID;
            IndentMaterials exist = new IndentMaterials();
            string StatusMessage = "";
            if (materials.IndentMaterialID == 0 && materials.MRPNO != null)
            {
                exist = indentMaterialManager.GetGeneralIndentExist(materials.MaterialDescription, materials.MRPNO, indent.IndentId);
                if (exist != null)
                {
                    if (exist.IndentID != null && exist.IndentID != 0)
                    {
                        StatusMessage = "Already Existed";
                        return Json(new { StatusMessage = StatusMessage }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (materials.IndentID == 0)
            {
                indent_ = indentManager.IndentSave(indent);
            }
            else if (materials.IndentID != null && materials.IndentID != 0)
            {

            }
            if (((indent_ != null && indent.IndentId != 0) || materials.IndentID != 0))
            {
                if (indent_ != null && indent.IndentId != 0)
                {
                    materials.IndentID = indent.IndentId;
                }
                SubstanceMaster substanceMaster = new SubstanceMaster();
                SubstanceMasterManager substanceMasterManager = new SubstanceMasterManager();
                substanceMaster = substanceMasterManager.GetsubstanceMasterId(materials.SubstanceMasterId);
                if (substanceMaster != null && substanceMaster.SubstanceMasterId != 0)
                {
                    materials.SubstanceRange = substanceMaster.SubstanceRange;
                }
                SizeRangeDetails sizeRangeDetails = new SizeRangeDetails();
                SizeRangeDetailsManager sizeRangeDetailsManager = new SizeRangeDetailsManager();
                if (materials.SizeRangeMasterID != null)
                {
                    sizeRangeDetails = sizeRangeDetailsManager.GetSizeRangeDetailsId(materials.SizeRangeMasterID.Value);
                    if (sizeRangeDetails != null && sizeRangeDetails.SizeRangeDetailsId != 0)
                    {
                        materials.SizeRangeName = sizeRangeDetails.SizeRangeName;
                    }
                }
                indentMaterialManager.Post(materials);
            }
            if (SizeRangeDetails != null)
            {
                var SizeDetails = JsonConvert.DeserializeObject<List<IndentSizeRangeQty>>(SizeRangeDetails);
                int count1 = 1;
                foreach (var item in SizeDetails)
                {
                    IndentSizeRangeQty indentSizeRangeQty = new IndentSizeRangeQty();
                    indentSizeRangeQty.Size = item.Size;
                    indentSizeRangeQty.quantity = Convert.ToInt32(item.quantity);
                    indentSizeRangeQty.Rate = item.Rate;
                    indentSizeRangeQty.IdentSizeRangeID = 0;
                    indentSizeRangeQty.IndentMaterialID = materials.IndentMaterialID;
                    List<IndentSizeRangeQty> listPurchaseOrderSizeRangeQuantity = new List<IndentSizeRangeQty>();
                    listPurchaseOrderSizeRangeQuantity = indentMaterialManager.IndentSizeRangeQtyGet().Where(x => x.IndentMaterialID == materials.IndentMaterialID).ToList();
                    if (count1 == 1)
                    {
                        indentMaterialManager.IndentSizeRangeQtyDelete(materials.IndentMaterialID);
                        count1++;
                    }
                    indentMaterialManager.IndentSizeRangeDetailsPost(indentSizeRangeQty);
                }
            }
            List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
            List<IndentMaterials> listIndentMaterials = new List<IndentMaterials>();
            listIndentMaterials = indentMaterialManager.GetIndentID(materials.IndentID.Value);
            decimal? totalAmount = listIndentMaterials.Sum(x => x.IndentQTY);
            //amar
            //   indentMaterials = indentMaterialManager.Get(materials.MRPNO);
            indentMaterials = indentMaterialManager.GetIndentID(materials.IndentID.Value);
            GRNTypeManager GRNManager = new GRNTypeManager();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            grnTypeMaster = GRNManager.GetIssueTypeMasterId(OrderTypeID);
            if (materials != null && materials.IndentMaterialID != 0)
            {
                StatusMessage = "Saved Successfully";
            }

            return Json(new { IndentMaterial = indentMaterials, OrderType = grnTypeMaster.IssueType, totalAmount = totalAmount, StatusMessage = StatusMessage }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        public ActionResult RowClickMaterialList(int IndentMaterialID, int BOMID, string MRPNO)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            List<IndentMaterials> listIndentMaterial = new List<IndentMaterials>();
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialManager.GetIndentMaterialId(IndentMaterialID);
            MaterialManager materialManager = new MaterialManager(); ;
            MaterialMaster materialmater = new MaterialMaster();
            List<IndentSizeRangeQty> indentSizeRange = new List<IndentSizeRangeQty>();
            materialmater = materialManager.GetMaterialMasterId(indentMaterial.MaterialMasterID);
            Indent indent = new Indent();

            indentSizeRange = indentMaterialManager.IndentSizeRangeQtyID(IndentMaterialID);
            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
            List<BomSizeMatching> listBOmSizeMatchingList = new List<BomSizeMatching>();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            if (indentSizeRange.Count == 0)
            {
                if (indentMaterial != null && indentMaterial.BOMMaterialID != null && indentMaterial.BOMMaterialID != 0)
                {
                    listBOmSizeMatchingList = billOfMaterialManager.GetBomSizeMatching(indentMaterial.BOMMaterialID.Value);
                }

                listSizeItemMaterial = materialManager.GetSizeItemMaterial(indentMaterial.MaterialMasterID.Value);
                listSizeItemMaterial = listSizeItemMaterial.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
            }
            indent = indentManager.GetIndentID(indentMaterial.IndentID.Value);
            GRNTypeManager grnTypeManager = new GRNTypeManager();
            GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
            grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(indentMaterial.OrderType));
            if (listBOmSizeMatchingList != null && listBOmSizeMatchingList.Count > 0)
            {
                listBOmSizeMatchingList = listBOmSizeMatchingList.OrderBy(x => Convert.ToDecimal(x.Size)).ToList();
                return Json(new { indentMaterial = indentMaterial, indentSizeRange = indentSizeRange, grnTypeMaster = grnTypeMaster, materialmater = materialmater, MaterialSizeRange = listSizeItemMaterial, MRPSizeScheduleMaterial = listBOmSizeMatchingList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { indentMaterial = indentMaterial, indentSizeRange = indentSizeRange, grnTypeMaster = grnTypeMaster, materialmater = materialmater, MaterialSizeRange = listSizeItemMaterial, MRPSizeScheduleMaterial = listBOmSizeMatchingList }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RowClick(int IndentMaterialID, int BOMID, string MRPNO)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();

            List<IndentMaterials> listIndentMaterial = new List<IndentMaterials>();
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialManager.GetIndentMaterialId(IndentMaterialID);
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialmater = new MaterialMaster();
            materialmater = materialManager.GetMaterialMasterId(indentMaterial.MaterialMasterID.Value);
            if (materialmater != null)
            {
                indentMaterial.MaterialMasterID = materialmater.MaterialMasterId;
            }

            if (materialmater != null)
            {
                indentMaterial.MaterialMasterID = materialmater.MaterialName;
            }
            return Json(indentMaterial, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PurchaseOrderRowClickMaterialList(int IndentMaterialID, int BOMID, string MRPNO)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();

            List<IndentMaterials> listIndentMaterial = new List<IndentMaterials>();
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialManager.GetIndentMaterialId(IndentMaterialID);
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialmater = new MaterialMaster();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterial bommaterial = new BOMMaterial();
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            Company company = new Company();
            CompanyManager companyManager = new CompanyManager();
            OrderEntry internalOrderEntryForm = new OrderEntry();
            BuyerOrderEntryManager buyerorderManager = new BuyerOrderEntryManager();
            if (indentMaterial != null)
            {
                bommaterial = billOfMaterialManager.getBomMaterialID(IndentMaterialID);
                internalOrderEntryForm = buyerorderManager.GetBuyerOderSlNo(bommaterial.OrderNo);
            }
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            ApprovedPriceList approvedPriceList = new ApprovedPriceList();
            var supplierItems = (from x in approvedPriceListManager.Get()
                                 join y in supplierMasterManager.Get()
                                 on x.SupplierId equals y.SupplierMasterId
                                 where x.MaterialID == IndentMaterialID
                                 select new { x.SupplierId, y.SupplierName, x.CreatedDate });
            if (supplierItems == null || supplierItems.Count() <= 0)
            {
                string Message = "There is no data on Approved price list for this material";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            supplierItems = supplierItems.OrderByDescending(x => x.CreatedDate).ToList();
            if (approvedPriceList == null)
            {
                string Message = "There is no data on Approved price list for this material";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            materialmater = materialManager.GetMaterialMasterId(IndentMaterialID);
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            var stockCount = purchaseOrderManager.MaterialOpeningStockValue(IndentMaterialID);
            decimal? stockTotal = 0;
            if (stockCount != null && stockCount.Count > 0)
            {
                stockTotal = stockCount.FirstOrDefault().BalanceStock;
            }
            else
            {
                stockTotal = 0;
            }
            if (materialmater != null)
            {
                indentMaterial.MaterialMasterID = materialmater.MaterialName;
            }
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            PurchaseOrder purchaerOrder = new PurchaseOrder();
            purchaerOrder = purchaseOrderManager.GetPoIndentOrderId(IndentMaterialID);
            storeMaster = storeMasterManager.GetStoreMasterId(materialmater.StoreMasterId);
            if (storeMaster != null && storeMaster.StoreMasterId != 0)
            {
                company = companyManager.GetCompanyCode(storeMaster.StoreMasterId);
            }
            return Json(new { indent = indentMaterial, InternalOrderForm = internalOrderEntryForm, stockTotal_ = stockTotal, Material = materialmater, store = storeMaster, purchaerOrder = purchaerOrder, company = company, approvedPriceList = supplierItems }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GeneralPurchaseOrderRowClickMaterialList(int IndentMaterialID, int BOMID, string IndentId)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            IndentId = IndentId.Trim().TrimStart(',');
            List<IndentMaterials> listIndentMaterial = new List<IndentMaterials>();
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialManager.GetIndentMaterialId(0);
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialmater = new MaterialMaster();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterial bommaterial = new BOMMaterial();
            InternalOrderForm internalOrderEntryForm = new InternalOrderForm();
            BuyerOrderEntryManager buyerorderManager = new BuyerOrderEntryManager();
            if (indentMaterial != null)
            {

            }
            Company company = new Company();
            CompanyManager companyManager = new CompanyManager();
            SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
            ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
            List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();
            var supplierItems = (from x in approvedPriceListManager.Get()
                                 join y in supplierMasterManager.Get()
                                 on x.SupplierId equals y.SupplierMasterId
                                 where x.MaterialID == IndentMaterialID && x.CreatedDate != null
                                 select new { x.SupplierId, y.SupplierName, x.CreatedDate, PriceRs = x.PriceRs });
            if (supplierItems == null || supplierItems.Count() <= 0)
            {
                string Message = "There is no data on Approved price list for this material";
                return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
            }
            supplierItems = supplierItems.OrderByDescending(x => x.CreatedDate).ToList();
            materialmater = materialManager.GetMaterialMasterId(IndentMaterialID);
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            var stockCount = purchaseOrderManager.MaterialOpeningStockValue(IndentMaterialID);
            decimal? stockTotal = 0;
            if (stockCount != null && stockCount.Count > 0)
            {
                stockTotal = stockCount.FirstOrDefault().BalanceStock;
            }
            else
            {
                stockTotal = 0;
            }
            StoreMasterManager storeMasterManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            storeMaster = storeMasterManager.GetStoreMasterId(materialmater.StoreMasterId);
            if (storeMaster != null && storeMaster.StoreMasterId != 0)
            {
                company = companyManager.GetCompanyCode(storeMaster.StoreMasterId);
            }
            List<ClubIndentSizeRange> listIndentSizeRange = new List<ClubIndentSizeRange>();
            listIndentSizeRange = indentMaterialManager.GetIndentSizerangeWithMaterial(IndentId, IndentMaterialID);
            PurchaseOrder purchaerOrder = new PurchaseOrder();
            List<PurchaseOrder> purchaerOrderList = new List<PurchaseOrder>();
            purchaerOrder = purchaseOrderManager.GetIndentClubMaterial(IndentMaterialID, IndentId);

            purchaerOrderList = purchaseOrderManager.GetIndentClubPoMaterialList(IndentMaterialID, IndentId);
            decimal partialQtyTotal = 0;
            if (purchaerOrderList != null && purchaerOrderList.Count > 0)
            {
                partialQtyTotal = purchaerOrderList.Sum(x => x.PoQty).Value;
                purchaerOrder.PoQty = 0;
                purchaerOrder.PendingQty = purchaerOrder.Qty - partialQtyTotal;
                purchaerOrder.Qty = purchaerOrder.Qty - partialQtyTotal;
            }

            return Json(new { indent = indentMaterial, InternalOrderForm = internalOrderEntryForm, stockTotal_ = stockTotal, SizeRange = listIndentSizeRange, Material = materialmater, store = storeMaster, approvedPriceList = supplierItems, purchaerOrder = purchaerOrder, company = company }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RowClickMaterialList_(int IndentMaterialID, int BOMID, string MRPNO)
        {
            IndentManager indentManager = new IndentManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            List<IndentMaterials> listIndentMaterial = new List<IndentMaterials>();
            IndentMaterials indentMaterial = new IndentMaterials();
            SizeRangeMasterManager sizeRangeMasteManager = new SizeRangeMasterManager();
            List<SizeRangeMaster> sizeRangeMasterList = new List<SizeRangeMaster>();
            indentMaterial = indentMaterialManager.GetIndentMaterialId(IndentMaterialID);
            if (indentMaterial != null)
            {
                MaterialManager materialManager = new MaterialManager();
                MaterialMaster materialMaster = new MaterialMaster();
                SizeRangeQtyRate sizeRangeQtyRate = new SizeRangeQtyRate();
                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                materialgroupmaster materialGroupmaster = new materialgroupmaster();
                materialMaster = materialManager.GetMaterialMasterId(indentMaterial.MaterialMasterID.Value);

                if (materialMaster != null)
                {
                    materialGroupmaster = materialGroupManager.GetmaterialgroupmasterId(materialMaster.MaterialGroupMasterId);
                    if (materialGroupmaster.IsSize == true)
                    {
                        if (materialMaster.SizeRangeMasterId != 0)
                        {
                            sizeRangeMasterList = sizeRangeMasteManager.GetSizeRangeMasterList(materialMaster.SizeRangeMasterId.Value).ToList();
                        }
                    }

                }

            }
            return Json(sizeRangeMasterList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string filter)
        {
            List<SimpleMRP> simpleMRPList = new List<SimpleMRP>();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            List<Core.Entities.Indent> indentList = new List<Core.Entities.Indent>();
            IndentManager indentManager = new IndentManager();
            var IndentList = (from X in indentManager.Get()
                              where (X.IndentNo.ToLower().Trim().Contains(filter.ToLower().Trim()))
                              select X).ToList();
            IndentModel model = new IndentModel();
            model.IndentList = IndentList;
            SimpleMRP simpleMRP = new SimpleMRP();
            List<SimpleMRP> _items = new List<SimpleMRP>();
            var MRPList = (from x in simpleMRPManager.Get()
                           select new { MRPNO = x.MRPNO, SimpleMRPID = x.SimpleMRPID });
            var distinctList = MRPList.GroupBy(x => x.MRPNO).Select(g => g.First()).ToList();
            foreach (var item in distinctList)
            {
                SimpleMRP mrp = new SimpleMRP();
                mrp.MRPNO = item.MRPNO;
                mrp.SimpleMRPID = item.SimpleMRPID;
                _items.Add(mrp);
            }
            model.MRPList = _items.GroupBy(x => x.MRPNO).Select(g => g.First()).ToList();
            return PartialView("~/Views/Stock/Indent/Partial/IndentGrid.cshtml", model);
        }

        public ActionResult GetLastIndentNO()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateIndentNo();
            ID++;
            return Json(ID.ToString(), JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
