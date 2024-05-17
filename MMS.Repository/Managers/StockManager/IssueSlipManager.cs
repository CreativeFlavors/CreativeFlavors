using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Common;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;

namespace MMS.Repository.Managers.StockManager
{
    public class IssueSlipManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<tbl_issueslipdetails> IssueSlipRepository;
        private Repository<SizeItemsIssueSlip> sizeItemsIssueSlipRepository;
        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
        #region Helper Method

        public IssueSlipManager()
        {
            IssueSlipRepository = unitOfWork.Repository<tbl_issueslipdetails>();
            sizeItemsIssueSlipRepository = unitOfWork.Repository<SizeItemsIssueSlip>();
        }
        public List<MMS.Web.Models.PendingQty> MaterialOpeningStock(int MaterialNameID)
        {
            List<MMS.Web.Models.PendingQty> PendingList = new List<MMS.Web.Models.PendingQty>();
            try
            {
                PendingList = IssueSlipRepository.MaterialOpeningStock(MaterialNameID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PendingList;
        }
        public List<MMS.Web.Models.PendingQty> MaterialOpeningStockIssueDate(int MaterialNameID,DateTime IssueDate)
        {
            List<MMS.Web.Models.PendingQty> PendingList = new List<MMS.Web.Models.PendingQty>();
            try
            {
                PendingList = IssueSlipRepository.IssueMaterialOpeningStock(MaterialNameID,IssueDate);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PendingList;
        }
        
         public List<MMS.Web.Models.PendingQty> MaterialOpeningStockIssueDate_wetblue(int MaterialNameID, DateTime IssueDate,int LOTNo,int SupplierNameId)
        {
            List<MMS.Web.Models.PendingQty> PendingList = new List<MMS.Web.Models.PendingQty>();
            try
            {
                PendingList = IssueSlipRepository.IssueMaterialOpeningStock_Wetblue(MaterialNameID, IssueDate, LOTNo, SupplierNameId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PendingList;
        }
        public List<SizeRangeIssueModelcs> MaterialOpeningSizeRangeIssueStock(int MaterialNameID)
        {
            List<SizeRangeIssueModelcs> PendingList = new List<SizeRangeIssueModelcs>();
            try
            {
                PendingList = IssueSlipRepository.GetIssueSizerangeWithMaterial(MaterialNameID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PendingList;
        }
        public List<MMS.Web.Models.PendingQty> GetCurrentStock(int MaterialNameID, int MaterialType)
        {
            List<MMS.Web.Models.PendingQty> PendingList = new List<MMS.Web.Models.PendingQty>();
            try
            {
                //PendingList = IssueSlipRepository.GetCurrentStock(MaterialNameID, MaterialType);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PendingList;
        }
        public List<tbl_issueslipdetails> Get()
        {
            List<tbl_issueslipdetails> IssueObjList = new List<tbl_issueslipdetails>();
            try
            {
                IssueObjList = IssueSlipRepository.Table.ToList<tbl_issueslipdetails>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IssueObjList;
        }

        public tbl_issueslipdetails GetGRNSelectedRow(int IssueID)
        {
            tbl_issueslipdetails IssueObj = new tbl_issueslipdetails();
            if (IssueID != 0)
            {
                IssueObj = IssueSlipRepository.Table.Where(x => x.IssueSlipId == IssueID).FirstOrDefault();
            }
            return IssueObj;
        }
        public List<tbl_issueslipdetails> GetMultipleIssueid(int IssueID)
        {
            List<tbl_issueslipdetails> IssueObj = new List<tbl_issueslipdetails>();
            try
            {
                if (IssueID != 0)
                {
                    IssueObj = IssueSlipRepository.Table.Where(x => x.IssueSlipFK == IssueID).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
         
            return IssueObj;
        }
        public List<tbl_issueslipdetails> GetMultipleIssueid_list(int[] list)
        {
            List<tbl_issueslipdetails> IssueObj = new List<tbl_issueslipdetails>();
            try
            {

                int?[] list_new = list.Cast<int?>().ToArray();

                //  JobSheet_pair = JobSheet_PairRepository.Table.Where(x => list.Contains(x.jobsheet_pair_Code_id)).ToList();

                // IssueObj = IssueSlipRepository.Table.Where(x => x.IssueSlipFK == IssueID).ToList();

                IssueObj = IssueSlipRepository.Table.Where(x => list_new.Contains(x.IssueSlipFK)).ToList();
             
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return IssueObj;
        }
        public List<tbl_issueslipdetails> GetMultipleIssueid_list_Jobworktype_Id(int[] list)
        {
            List<tbl_issueslipdetails> IssueObj = new List<tbl_issueslipdetails>();
            try
            {

                int?[] list_new = list.Cast<int?>().ToArray();

                //  JobSheet_pair = JobSheet_PairRepository.Table.Where(x => list.Contains(x.jobsheet_pair_Code_id)).ToList();

                // IssueObj = IssueSlipRepository.Table.Where(x => x.IssueSlipFK == IssueID).ToList();

                IssueObj = IssueSlipRepository.Table.Where(x => list_new.Contains(x.IssueSlipFK)).ToList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return IssueObj;
        }
        public tbl_issueslipdetails GetMultipleIssueWithMaterialID(int materialID)
        {
            tbl_issueslipdetails IssueObj = new tbl_issueslipdetails();
            if (materialID != 0)
            {
                IssueObj = IssueSlipRepository.Table.Where(x => x.MaterialMasterId == materialID).OrderByDescending(x => x.IssueDate).FirstOrDefault();
            }
            return IssueObj;
        }
        public tbl_issueslipdetails GetIssueSlipMaterial(string orderNo, string MaterialName)
        {
            tbl_issueslipdetails IssueObj = new tbl_issueslipdetails();
            if (orderNo != "" && MaterialName != "")
            {
                IssueObj = IssueSlipRepository.Table.Where(x => x.OrderNo == orderNo && x.MaterialName == MaterialName).FirstOrDefault();
            }
            return IssueObj;
        }
        public List<tbl_issueslipdetails> GetOrderNo(string OrderNo)
        {
            List<tbl_issueslipdetails> issueSlip_MaterialDetailsList = new List<tbl_issueslipdetails>();
            if (OrderNo != "")
            {
                issueSlip_MaterialDetailsList = IssueSlipRepository.Table.Where(x => x.OrderNo == OrderNo).ToList();
            }
            return issueSlip_MaterialDetailsList;
        }
        public List<SizeItemsIssueSlip> GetSizeItemsIssueSlip(int MultipleIssueslipFk)
        {
            List<SizeItemsIssueSlip> issueSlip_MaterialDetailsList = new List<SizeItemsIssueSlip>();
            if (MultipleIssueslipFk != 0)
            {
                issueSlip_MaterialDetailsList = sizeItemsIssueSlipRepository.Table.Where(x => x.MultipleIssueslipFk == MultipleIssueslipFk && x.IsDeleted == false).ToList();
            }
            return issueSlip_MaterialDetailsList;
        }
        #endregion

        public tbl_issueslipdetails Post(tbl_issueslipdetails arg)
        {
            bool result = false;
            List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
            SizeRangeQtyRateManager sizeRangeQtyRateMgr = new SizeRangeQtyRateManager();
            OrderEntry orderEntry = new OrderEntry();
            BuyerOrderEntryManager buyerOrderManager = new BuyerOrderEntryManager();
            tbl_issueslipdetails issueSlip_MaterialDetails = new tbl_issueslipdetails();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();

            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
            SizeScheduleRange sizeScheduleRange = new SizeScheduleRange();
            SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();

            try
            {
                if (arg.IssueSlipId == 0 || arg.IssueSlipId ==null || arg.IssueSlipId==arg.IssueSlipFK)
                {
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = null;
                    arg.UpdatedBy = string.Empty;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    IssueSlipRepository.Insert(arg);
                    issueSlip_MaterialDetails = arg;
                    GRNTypeManager grnTypeManager = new GRNTypeManager();
                    GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialManager materialManager = new MaterialManager();
                    grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(arg.IssueType));
                    materialMaster = materialManager.GetMaterialMasterId(arg.MaterialMasterId);
                    orderEntry = buyerOrderManager.GetBuyerOderSlNo(arg.InternalValue);
                    Bom billOfMaterial = new Bom();
                    BOMMaterial bomMaterial = new BOMMaterial();
                    billOfMaterial = billOfMaterialManager.getLinkBomNumber(orderEntry.OurStyle);
                    bomMaterial = billOfMaterialManager.getBOMIDWithMaterial(billOfMaterial.BomId, arg.MaterialMasterId);
                    var SizeQuantityRt = sizeRangeQtyRateMgr.GetSizeRangeByOrderEntryId(orderEntry.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                    List<SizeScheduleRange> listSizeScheduleRange = new List<SizeScheduleRange>();
                    List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
                    if (bomMaterial != null)
                    {
                        listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(bomMaterial.BOMMaterialID);
                    }
                    sizeRangeQtyRateList = SizeQuantityRt.Where(x => x.OrderEntryId == orderEntry.OrderEntryId).ToList();
                    sizeRangeQtyRateList = sizeRangeQtyRateList.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

                    if (grnTypeMaster != null && grnTypeMaster.IssueType == "Order" && materialMaster != null && materialMaster.SizeRangeMasterId != 0 && materialMaster.SizeRangeMasterId != null)
                    {

                        if (bomMaterial.SizeScheduleMasterId != null && bomMaterial.SizeRangeMasterID != 0)
                        {
                            sizeScheduleRange = sizeScheduleMasterManager.GetSizeScheduleID(bomMaterial.SizeScheduleMasterId.Value);
                            listSizeScheduleRange = sizeScheduleMasterManager.GetSizeRange(bomMaterial.SizeScheduleMasterId.Value);
                            listSizeScheduleRange = listSizeScheduleRange.OrderBy(x => Convert.ToDecimal(x.Size)).ToList();
                        }

                        if (sizeScheduleRange != null)
                        {


                            if (listSizeScheduleRange != null && listSizeScheduleRange.Count > 0)
                            {
                                foreach (var iteration in listSizeScheduleRange)
                                {
                                    decimal? sizeQty = 0;
                                    string[] sizerange = iteration.Frame.Split('-');
                                    var sizeranges = sizerange.Distinct().ToList();
                                    var sizeRangeList = sizeRangeQtyRateList.Where(x => Convert.ToDecimal(x.SizeRange) >= Convert.ToDecimal(sizerange[0]) && Convert.ToDecimal(x.SizeRange) <= Convert.ToDecimal(sizerange[1])).ToList();
                                    foreach (var item in sizeRangeList)
                                    {
                                        sizeQty += sizeRangeQtyRateList.Where(x => x.SizeRange == item.SizeRange).Sum(x => x.Qty);
                                    }
                                    SizeItemsIssueSlip sizeItemMaterial = new SizeItemsIssueSlip();
                                    sizeItemMaterial.SizeRange = iteration.Size.ToString();
                                    sizeItemMaterial.Qty = sizeQty;
                                    sizeItemMaterial.MultipleIssueslipFk = arg.IssueSlipId;
                                    sizeItemMaterial.CreatedDate = DateTime.Now;
                                    sizeItemMaterial.CreatedBY = username;
                                    sizeItemsIssueSlipPost(sizeItemMaterial);

                                }
                            }
                            else
                            {
                                List<DisplaySizeMaterial> listDispalySizeMaterial = new List<DisplaySizeMaterial>();
                                if (bomMaterial != null && bomMaterial.BOMMaterialID != 0)
                                {
                                    decimal? sizeqty = 0;
                                    listDispalySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(Convert.ToInt32(bomMaterial.BOMMaterialID)).Where(x => x.SizeIsChecked == true).ToList();
                                    foreach (var item in listDisplaySizeMaterial)
                                    {                                        
                                        sizeqty += sizeRangeQtyRateList.Where(x => x.SizeRange == item.SizeRange).Sum(x => x.Qty);                                       

                                    }
                                    arg.CurrentIssue = sizeqty;
                                    IssueSlipRepository.Update(arg);

                                }
                            }                          
                        }
                        else
                        {
                            if (listDisplaySizeMaterial != null && listDisplaySizeMaterial.Count > 0)
                            {
                                listDisplaySizeMaterial = listDisplaySizeMaterial.Where(x => x.SizeIsChecked == true).ToList();
                                if (listDisplaySizeMaterial != null && listDisplaySizeMaterial.Count > 0)
                                {
                                    decimal? sizeqty = 0;
                                    listDisplaySizeMaterial = listDisplaySizeMaterial.Where(x => x.SizeIsChecked == true).ToList();
                                    foreach (var item in listDisplaySizeMaterial)
                                    {                                       
                                        sizeqty += sizeRangeQtyRateList.Where(x => x.SizeRange == item.SizeRange).Sum(x => x.Qty);                                   

                                    }
                                    arg.CurrentIssue = sizeqty;
                                    IssueSlipRepository.Update(arg);
                                }
                            }                            
                        }

                    }
                    else
                    {
                        //if (listDisplaySizeMaterial != null && listDisplaySizeMaterial.Count > 0)
                        //{
                        //    decimal? sizeqty = 0;
                        //    listDisplaySizeMaterial = listDisplaySizeMaterial.Where(x => x.SizeIsChecked == true).ToList();
                        //    foreach (var item in listDisplaySizeMaterial)
                        //    {                                
                        //        sizeqty += sizeRangeQtyRateList.Where(x => x.SizeRange == item.SizeRange).Sum(x => x.Qty);                              

                        //    }
                        //    arg.CurrentIssue = sizeqty;
                            IssueSlipRepository.Update(arg);
                       // }
                    }

                    result = true;
                }
                else
                {
                    tbl_issueslipdetails model = IssueSlipRepository.Table.Where(m => m.IssueSlipId == arg.IssueSlipId).FirstOrDefault();

                    model.UpdatedDate = DateTime.Now;
                    model.IsChecked = arg.IsChecked;
                    model.OrderNo = arg.OrderNo;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.Style = arg.Style.ToString();
                    model.IssueType = arg.IssueType;
                    model.MaterialTypes = arg.MaterialTypes;
                    model.NoOfSets = arg.NoOfSets;
                    model.RequiredQty = arg.RequiredQty;
                    model.StoreName = arg.StoreName;
                    model.MaterialName = arg.MaterialName;
                    model.ColourId = arg.ColourId;
                    model.CategoryId = arg.CategoryId;
                    model.GroupId = arg.GroupId;
                    model.RequiredQty = arg.RequiredQty;
                    model.AlredayIssued = arg.AlredayIssued;
                    model.CurrentIssue = arg.CurrentIssue;
                    model.Rate = arg.Rate;
                    model.Piecies = arg.Piecies;
                    model.CurrentStock = arg.CurrentStock;
                    model.IssueSlipType = arg.IssueSlipType;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.StoreMasterId = arg.StoreMasterId;
                    model.MaterialType = arg.MaterialType;
                    model.IssueType = arg.IssueType;
                    model.InternalOrderingFor = arg.InternalOrderingFor;
                    model.LotNo = arg.LotNo;
                    model.MachineName = arg.MachineName;
                    model.PieciesType = arg.PieciesType;
                    model.InternalValue = arg.InternalValue;
                    model.DirectIssue_Style = arg.DirectIssue_Style;
                    model.BalanceInThisListlot = arg.BalanceInThisListlot;
                    model.BalanceInthisListType = arg.BalanceInthisListType;
                    model.MachineName = arg.MachineName;
                    model.SubtanceID = arg.SubtanceID;
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.RequiredType = arg.RequiredType;
                    model.AlreadyUsedType = arg.AlreadyUsedType;
                    model.CurrentIssuesType = arg.CurrentIssuesType;
                    model.PiecesRequiredQTY = arg.PiecesRequiredQTY;
                    model.PiecesRequiredQtyType = arg.PiecesRequiredQtyType;
                    model.PiecesAlreadyIssue = arg.PiecesAlreadyIssue;
                    model.PiecesAlreadyIssueType = arg.PiecesAlreadyIssueType;
                    model.PiecesCurrentIssue = arg.PiecesCurrentIssue;
                    model.PiecesCurrentType = arg.PiecesCurrentType;
                    model.InternalValue = arg.InternalValue;
                    model.IssueSlipNo = arg.IssueSlipNo;
                    model.TotalQty = arg.TotalQty;
                    model.ConveyorID = arg.ConveyorID;
                    model.Season = arg.Season;
                    model.IssueDate = arg.IssueDate;
                    model.Component_Id = arg.Component_Id;
                    IssueSlipRepository.Update(model);
                    issueSlip_MaterialDetails = model;
                    GRNTypeManager grnTypeManager = new GRNTypeManager();
                    GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
                    MaterialMaster materialMaster = new MaterialMaster();
                    MaterialManager materialManager = new MaterialManager();
                    CompanyManager companyManager = new CompanyManager();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    List<Company> listCompany = new List<Company>();
                    listCompany = companyManager.Get();


                    grnTypeMaster = grnTypeManager.GetIssueTypeMasterId(Convert.ToInt32(model.IssueType));
                    materialMaster = materialManager.GetMaterialMasterId(model.MaterialMasterId);
                    if (grnTypeMaster != null && grnTypeMaster.IssueType == "Order" && materialMaster != null && materialMaster.SizeRangeMasterId != 0 && materialMaster.SizeRangeMasterId != null)
                    {
                        orderEntry = buyerOrderManager.GetBuyerOderSlNo(model.OrderNo);
                        var SizeQuantityRt = sizeRangeQtyRateMgr.GetSizeRangeByOrderEntryId(orderEntry.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                        sizeRangeQtyRateMgr.Delete(orderEntry.OrderEntryId);
                        foreach (var item in SizeQuantityRt)
                        {
                            SizeItemsIssueSlip sizeItemMaterial = new SizeItemsIssueSlip();
                            sizeItemMaterial.SizeRange = item.SizeRange;
                            sizeItemMaterial.Qty = item.Qty;
                            sizeItemMaterial.MultipleIssueslipFk = arg.IssueSlipId;
                            sizeItemMaterial.CreatedDate = DateTime.Now;
                            sizeItemMaterial.CreatedBY = username;
                            sizeItemsIssueSlipPost(sizeItemMaterial);
                        }
                    }
                    //EmailTempate emailTemplate = new EmailTempate();
                    //emailTemplate = emailTemplateManager.GetTemplateName("Multiple Issue Update");
                    //storeMaster = storeManager.GetStoreMasterId(model.StoreMasterId);
                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;
                    //    contents = contents.Replace("[[issueno]]", !string.IsNullOrEmpty(model.IssueSlipNo) ? model.IssueSlipNo : string.Empty);
                    //    contents = contents.Replace("[[MaterialName]]", !string.IsNullOrEmpty(model.MaterialName) ? model.MaterialName : string.Empty);
                    //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[StoreName]]", storeMaster != null ? storeMaster.StoreName != null ? storeMaster.StoreName : string.Empty : string.Empty);
                    //    emailTemplate.Body = contents;
                    //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    //}
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return issueSlip_MaterialDetails;
        }

        public SizeItemsIssueSlip sizeItemsIssueSlipPost(SizeItemsIssueSlip arg)
        {
            bool result = false;
            SizeItemsIssueSlip sizeItemsIssueSlip = new SizeItemsIssueSlip();
            try
            {
                if (arg.SizeMaterialD == 0)
                {
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = null;
                    arg.UpdatedBy = string.Empty;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBY = username;
                    sizeItemsIssueSlipRepository.Insert(arg);
                    sizeItemsIssueSlip = arg;
                    result = true;
                }
                else
                {
                    SizeItemsIssueSlip model = sizeItemsIssueSlipRepository.Table.Where(m => m.SizeMaterialD == arg.SizeMaterialD).FirstOrDefault();
                    model.MultipleIssueslipFk = arg.MultipleIssueslipFk;
                    model = arg;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    sizeItemsIssueSlipRepository.Update(model);
                    sizeItemsIssueSlip = model;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return sizeItemsIssueSlip;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                CompanyManager companyManager = new CompanyManager();
                StoreMasterManager storeManager = new StoreMasterManager();
                StoreMaster storeMaster = new StoreMaster();
                List<Company> listCompany = new List<Company>();
                listCompany = companyManager.Get();

                tbl_issueslipdetails model = IssueSlipRepository.GetById(id);
                IssueSlipRepository.Delete(model);
                EmailTemplate emailTemplate = new EmailTemplate();
                emailTemplate = emailTemplateManager.GetTemplateName("Multiple Issue Mateial Delete");
                if (emailTemplate != null)
                {
                    storeMaster = storeManager.GetStoreMasterId(model.StoreMasterId);
                    string contents = emailTemplate.Body;
                    MaterialManager materialManager = new MaterialManager();
                    contents = contents.Replace("[[issueno]]", !string.IsNullOrEmpty(model.IssueSlipNo) ? model.IssueSlipNo : string.Empty);
                    contents = contents.Replace("[[MaterialName]]", !string.IsNullOrEmpty(model.MaterialName) ? model.MaterialName : string.Empty);
                    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    contents = contents.Replace("[[StoreName]]", storeMaster != null ? storeMaster.StoreName != null ? storeMaster.StoreName : string.Empty : string.Empty);
                    emailTemplate.Body = contents;
                    MMS.Common.EmailHelper.SendEmail(emailTemplate);
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
        public bool SizeItemsIssueSlipDelete(int id)
        {
            bool result = false;
            try
            {
                SizeItemsIssueSlip model = sizeItemsIssueSlipRepository.GetById(id);
                sizeItemsIssueSlipRepository.Delete(model);
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
