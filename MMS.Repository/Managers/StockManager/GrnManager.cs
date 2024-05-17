using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Common;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Data.StoredProcedureModel;
using MMS.Core.Entities;

namespace MMS.Repository.Managers.StockManager
{
    public class GrnManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GRN_EntityModel> GrnRepository;
        private Repository<tbl_issueslipdetails> IssueRepository;
        private Repository<GRNSizeQuantityObject> GRNSizeRangeRepository;
        BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
        #region Helper Method

        public GrnManager()
        {
            GrnRepository = unitOfWork.Repository<GRN_EntityModel>();
            GRNSizeRangeRepository = unitOfWork.Repository<GRNSizeQuantityObject>();
        }
        public List<GRNSizeQuantityObject> GRNSizeQuantityGet()
        {
            List<GRNSizeQuantityObject> gRNSizeQuantityObject = new List<GRNSizeQuantityObject>();
            try
            {
                gRNSizeQuantityObject = GRNSizeRangeRepository.Table.ToList<GRNSizeQuantityObject>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gRNSizeQuantityObject;
        }
        public List<GRNSizeQuantityObject> GetGRNSizeQuantity(int GrnID)
        {
            List<GRNSizeQuantityObject> gRNSizeQuantityObject = new List<GRNSizeQuantityObject>();
            try
            {
                gRNSizeQuantityObject = GRNSizeRangeRepository.Table.Where(x => x.Grnid_FK == GrnID).ToList<GRNSizeQuantityObject>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gRNSizeQuantityObject;
        }
        public bool GRNNoDelete(int GrnNO)
        {

            List<GRN_EntityModel> listGrn = new List<GRN_EntityModel>();
            List<GRNSizeQuantityObject> sizeRangeQuantityList = new List<GRNSizeQuantityObject>();
            GrnManager grnManager = new GrnManager();
            bool result = false;
            try
            {
                listGrn = grnManager.GetGRNNO(GrnNO);
                string Materialname = string.Empty;
                if (listGrn != null && listGrn.Count > 0)
                {
                    foreach (var item in listGrn)
                    {
                        MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                        GRN_EntityModel GRN = GrnRepository.GetById(item.GrnID);
                        if (GRN != null && GRN.GrnID != 0)
                        {
                            ItesmaterialName = bomMaterialListManager.GetMaterial(GRN.Grn_MaterialID.Value);
                            sizeRangeQuantityList = grnManager.GRNSizeQuantityGet().Where(x => x.Grnid_FK == GRN.GrnID).ToList();
                            foreach (var deleteItem in sizeRangeQuantityList)
                            {
                                GRNSizeQuantityObject model = GRNSizeRangeRepository.GetById(deleteItem.GRNeSizeRangeID);
                                GRNSizeRangeRepository.Delete(model);
                            }
                            Materialname += ItesmaterialName.materialdescription + "@";
                            GrnRepository.Delete(GRN);
                            result = true;
                        }
                    }
                   
                    CompanyManager companyManager = new CompanyManager();
                    List<Company> listCompany = new List<Company>();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    listCompany = companyManager.Get();

                    EmailTemplate emailTemplate = new EmailTemplate();
                    emailTemplate = emailTemplateManager.GetTemplateName("GRN Delete");
                    storeMaster= storeManager.GetStoreMasterId(listGrn.FirstOrDefault().Stores);
                    if (emailTemplate != null)
                    {
                        Materialname = Materialname.Replace("@", "@" + System.Environment.NewLine);
                        string contents = emailTemplate.Body;
                        MaterialManager materialManager = new MaterialManager();
                        contents = contents.Replace("[[GRNNo]]", !string.IsNullOrEmpty(listGrn.FirstOrDefault().GrnNo.ToString()) ? listGrn.FirstOrDefault().GrnNo.ToString() : string.Empty);
                        contents = contents.Replace("[[MaterialName]]", Materialname);
                        contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                        contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                        contents = contents.Replace("[[StoreName]]", storeMaster!=null ? storeMaster.StoreName : string.Empty);
                        emailTemplate.Body = contents;
                        MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool GRNSizeRangeQuantityDelete(int GrnID)
        {
            bool result = false;
            try
            {
                List<GRNSizeQuantityObject> sizeRangeQuantityList = new List<GRNSizeQuantityObject>();
                GrnManager grnManager = new GrnManager();
                sizeRangeQuantityList = grnManager.GRNSizeQuantityGet().Where(x => x.Grnid_FK == GrnID).ToList();
                foreach (var deleteItem in sizeRangeQuantityList)
                {
                    GRNSizeQuantityObject model = GRNSizeRangeRepository.GetById(deleteItem.GRNeSizeRangeID);
                    GRNSizeRangeRepository.Delete(model);
                }
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool GRNSizeRangeDetailsPost(GRNSizeQuantityObject arg)
        {
            bool result = false;
            try
            {
                if (arg.GRNeSizeRangeID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = null;
                    GRNSizeRangeRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    GRNSizeQuantityObject model = GRNSizeRangeRepository.Table.Where(m => m.GRNeSizeRangeID == arg.GRNeSizeRangeID).FirstOrDefault();
                    model.Grnid_FK = arg.Grnid_FK;
                    model.Size = arg.Size;
                    model.quantity = arg.quantity;
                    model.Rate = arg.Rate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    GRNSizeRangeRepository.Update(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public List<GRN_EntityModel> Get()
        {
            List<GRN_EntityModel> GrnObjList = new List<GRN_EntityModel>();
            try
            {
                GrnObjList = GrnRepository.Table.ToList<GRN_EntityModel>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRNGrid> GetGRNGrid(string Filter)
        {
            List<GRNGrid> grnList = new List<GRNGrid>();
            grnList = GrnRepository.GetPurchaseOrderGrid(Filter);
            return grnList;
        }
        public List<GRN_EntityModel> GetGRNNO(int GRNNo)
        {
            List<GRN_EntityModel> GrnObjList = new List<GRN_EntityModel>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.GrnNo == GRNNo).ToList<GRN_EntityModel>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModel> GetGRNPOQty(int PoOrderID)
        {
            List<GRN_EntityModel> GrnObjList = new List<GRN_EntityModel>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.MaterialId == PoOrderID && x.PoNO != 0).OrderByDescending(x => x.CreatedDate).ToList<GRN_EntityModel>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModel> GetPOQty(int PoOrderID, int materialID)
        {
            List<GRN_EntityModel> GrnObjList = new List<GRN_EntityModel>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.MaterialId == PoOrderID && x.Grn_MaterialID == materialID && x.PoNO != 0).OrderByDescending(x => x.CreatedDate).ToList<GRN_EntityModel>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModel> GetAutomaticPOQty(string PoOrderID, int materialID)
        {
            List<GRN_EntityModel> GrnObjList = new List<GRN_EntityModel>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.AutomaticPONumber == PoOrderID && x.MaterialId == materialID && x.PoNO != 0).OrderByDescending(x => x.CreatedDate).ToList<GRN_EntityModel>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModel> isExistGRNNOWithMaterial(int MaterialID, int GRNNo)
        {
            List<GRN_EntityModel> GrnObjList = new List<GRN_EntityModel>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.GrnNo == GRNNo && x.MaterialId == MaterialID).ToList<GRN_EntityModel>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public GRN_EntityModel GetGRNSelectedRow(int GrnID)
        {
            GRN_EntityModel GrnObj = new GRN_EntityModel();
            if (GrnID != 0)
            {
                GrnObj = GrnRepository.Table.Where(x => x.GrnID == GrnID).FirstOrDefault();
            }
            return GrnObj;
        }
        public GRN_EntityModel IsSupplierWithInvoice(string INVoiceNo, int SupplierNameId, int MaterialId, string DC_No)
        {
            GRN_EntityModel GrnObj = new GRN_EntityModel();
            GrnObj = GrnRepository.Table.Where(x => x.INVoiceNo == INVoiceNo && x.SupplierNameId == SupplierNameId && x.Grn_MaterialID == MaterialId && x.DC_No == DC_No).FirstOrDefault();

            return GrnObj;
        }
        public GRN_EntityModel CheckDuplicate_GRN(int PONO, int Material)
        {
            GRN_EntityModel GrnObj = new GRN_EntityModel();
            if (Material != 0 && PONO != 0)
            {
                GrnObj = GrnRepository.Table.Where(x => x.MaterialId == PONO && x.Grn_MaterialID == Material).FirstOrDefault();
            }
            return GrnObj;
        }
        #endregion


        public List<GRN_EntityModel> GetGrnGrid(string GrnNo)
        {
            List<GRN_EntityModel> contactlist = new List<GRN_EntityModel>();
            try
            {
                contactlist = GrnRepository.SearchGrnList(GrnNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        public List<ApprovedPrice> getApprovedPrice(string SupplierID, string MaterialID)
        {
            List<ApprovedPrice> list = new List<ApprovedPrice>();
            list = GrnRepository.getApprovedPrice(SupplierID, MaterialID);
            return list;
        }

        public GRN_EntityModel Post(GRN_EntityModel arg)
        {
            bool result = false;
            GRN_EntityModel grn_EntityModel = new GRN_EntityModel();
            try
            {
                if (arg.GrnID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.UpdatedDate = null;
                    arg.UpdatedBy = string.Empty;
                    GrnRepository.Insert(arg);
                    grn_EntityModel = arg;
                }
                else
                {
                    GRN_EntityModel model = GrnRepository.Table.Where(m => m.GrnID == arg.GrnID).FirstOrDefault();
                    model.GrnID = arg.GrnID;
                    model.AmountplusTax = arg.AmountplusTax;
                    model.IONo = arg.IONo;
                    model.PoQty = arg.PoQty;
                    model.BarCodeNo = arg.BarCodeNo;
                    model.Transporter = arg.Transporter;
                    model.GrnNo = arg.GrnNo;
                    model.GateEntryNo = arg.GateEntryNo;
                    model.Grn_MaterialID = arg.Grn_MaterialID;
                    model.FreightAmount = arg.FreightAmount;
                    model.IndentMaterialId = arg.IndentMaterialId;
                    model.GrnDate = arg.GrnDate;
                    model.MaterialType = arg.MaterialType;
                    model.GEDate = arg.GEDate;
                    model.ServiceTaxPer = arg.ServiceTaxPer;
                    model.IndentNo = arg.IndentNo;
                    model.GETime = arg.GETime;
                    model.InsuranceAmount = arg.InsuranceAmount;
                    model.GrnRefNo = arg.GrnRefNo;
                    model.AutomaticPONumber = arg.AutomaticPONumber;
                    model.GateRefNo = arg.GateRefNo;
                    model.ShortageQty = arg.ShortageQty;
                    model.ShortageSQFT = arg.ShortageSQFT;
                    model.ShortagePCS = arg.ShortagePCS;
                    model.ShortageType = arg.ShortageType;
                    model.CustomsDuty = arg.CustomsDuty;
                    model.SupplierNameId = arg.SupplierNameId;
                    model.GrnType = arg.GrnType;
                    model.BENo = arg.BENo;
                    model.Pack_Forward = arg.Pack_Forward;
                    model.DC_No = arg.DC_No;
                    model.ST_VATcredit = arg.ST_VATcredit;
                    model.ServiceTax = arg.ServiceTax;
                    model.DCDate = arg.DCDate;
                    model.ImportClearance = arg.ImportClearance;
                    model.OtherCharges = arg.OtherCharges;
                    model.INVoiceNo = arg.INVoiceNo;
                    model.ExchangeRate = arg.ExchangeRate;
                    model.Transporter = arg.Transporter;
                    model.INVoiceDate = arg.INVoiceDate;
                    model.ShipmentMode = arg.ShipmentMode;
                    model.GeneralRemarks1 = arg.GeneralRemarks1;
                    model.PoNO = arg.PoNO;
                    model.LOTNo = arg.LOTNo;
                    model.IndentNo = arg.IndentNo;
                    model.Stores = arg.Stores;
                    model.MaterialId = arg.MaterialId;
                    model.Grade = arg.Grade;
                    model.GroupID = arg.GroupID;
                    model.CategoryId = arg.CategoryId;
                    model.IfGroupIsMaintenance = arg.IfGroupIsMaintenance;
                    model.ColourId = arg.ColourId;
                    model.Substance = arg.Substance;
                    model.QtyAsPerDc = arg.QtyAsPerDc;
                    model.QtyExcess = arg.QtyExcess;
                    model.UOMId = arg.UOMId;
                    model.Weight = arg.Weight;
                    model.ReceivedQty = arg.ReceivedQty;
                    model.DOM = arg.DOM;
                    model.AcceptedQty = arg.AcceptedQty;
                    model.PendingQty = arg.PendingQty;
                    model.StoreLocation = arg.StoreLocation;
                    model.Disp_SelectedMatOfPO = arg.Disp_SelectedMatOfPO;
                    model.Disp_AllPOBasedOnSelecMat = arg.Disp_AllPOBasedOnSelecMat;
                    model.GeneralRemarks = arg.GeneralRemarks;
                    model.Disp_AllMatOfPO = arg.Disp_AllMatOfPO;
                    model.Rate = arg.Rate;
                    model.Value = arg.Value;
                    model.MRPMargin = arg.MRPMargin;
                    model.MRPPrice = arg.MRPPrice;
                    model.AccessibleValue = arg.AccessibleValue;
                    model.Discount_Per = arg.Discount_Per;
                    model.Discount_Val = arg.Discount_Val;
                    model.Ecess_Per = arg.Ecess_Per;
                    model.Ecess_Val = arg.Ecess_Val;
                    model.MRPMargin = arg.MRPMargin;
                    model.ExciseDuty_Per = arg.ExciseDuty_Per;
                    model.ExciseDuty_Val = arg.ExciseDuty_Val;
                    model.SH_Ecess_Per = arg.SH_Ecess_Per;
                    model.SH_Ecess_Val = arg.SH_Ecess_Val;
                    model.ST_VAT_CST_Per = arg.ST_VAT_CST_Per;
                    model.ST_VAT_CST_Val = arg.ST_VAT_CST_Val;
                    model.Surcharge_Per = arg.Surcharge_Per;
                    model.Surcharge_Val = arg.Surcharge_Val;
                    model.ExcessApproval = arg.ExcessApproval;
                    model.RejectedQty = arg.RejectedQty;
                    model.RejectedPCS = arg.RejectedPCS;
                    model.RejectedSQFT = arg.RejectedSQFT;
                    model.RejectedType = arg.RejectedType;
                    model.TagsPiecesDiscount_Per = arg.TagsPiecesDiscount_Per;
                    model.TagsPiecesDiscount_Val = arg.TagsPiecesDiscount_Val;
                    model.UpdatedDate = DateTime.Now;
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.BuyerSeasonID = arg.BuyerSeasonID;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    GrnRepository.Update(model);
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    ItesmaterialName = bomMaterialListManager.GetMaterial(arg.Grn_MaterialID.Value);
                    EmailTemplate emailTemplate = new EmailTemplate();
                    emailTemplate = emailTemplateManager.GetTemplateName("GRN Update");
                    CompanyManager companyManager = new CompanyManager();
                    List<Company> listCompany = new List<Company>();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    listCompany = companyManager.Get();
                    storeMaster = storeManager.GetStoreMasterId(arg.Stores);
                    if (emailTemplate != null)
                    {
                        string contents = emailTemplate.Body;
                        MaterialManager materialManager = new MaterialManager();
                        contents = contents.Replace("[[GRNNo]]", !string.IsNullOrEmpty(model.GrnNo.ToString()) ? model.GrnNo.ToString() : string.Empty);
                        contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription.ToString()) ? ItesmaterialName.materialdescription.ToString() : string.Empty : string.Empty);
                        contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                        contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                        contents = contents.Replace("[[StoreName]]", storeMaster != null ? storeMaster.StoreName : string.Empty);
                        emailTemplate.Body = contents;
                        MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    }
                    grn_EntityModel = arg;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return grn_EntityModel;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                GRN_EntityModel model = GrnRepository.GetById(id);
                GrnRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
    }
}
