using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Managers.StockManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MMS.Repository.Managers
{
    public class SimpleMRPManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SimpleMRP> simpleRepository;
        private Repository<ImportIO> importIORepository;
        private Repository<MRPSelectedValues> MRPSelectedValuesRepository;
        public SimpleMRPManager()
        {
            simpleRepository = unitOfWork.Repository<SimpleMRP>();
            importIORepository = unitOfWork.Repository<ImportIO>();
            MRPSelectedValuesRepository = unitOfWork.Repository<MRPSelectedValues>();
        }


        public SimpleMRP Post(SimpleMRP arg)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                simpleRepository.Insert(arg);
                simpleMRP = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return simpleMRP;
        }
        public SimpleMRP SimpleMRPPost(SimpleMRP arg)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.UpdatedDate = null;
                simpleRepository.Insert(arg);
                EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
                EmailTemplate emailTemplate = new EmailTemplate();
                emailTemplate = emailTemplateManager.GetTemplateName("MRP Update");
                CompanyManager companyManager = new CompanyManager();
                StoreMasterManager storeManager = new StoreMasterManager();
                StoreMaster storeMaster = new StoreMaster();
                List<Company> listCompany = new List<Company>();
                listCompany = companyManager.Get();
                //if (emailTemplate != null)
                //{
                //    string contents = emailTemplate.Body;
                //    MaterialManager materialManager = new MaterialManager();
                //    contents = contents.Replace("[[MRPNO]]", !string.IsNullOrEmpty(arg.MRPNO) ? arg.MRPNO : string.Empty);
                //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                //    emailTemplate.Body = contents;
                //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                //}
                simpleMRP = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return simpleMRP;
        }
        
        public SimpleMRP GetSimpleMRPID(int? SimpleMRPID)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            simpleMRP = simpleRepository.Table.Where(x => x.SimpleMRPID == SimpleMRPID).SingleOrDefault();
            return simpleMRP;
        }
        public SimpleMRP GetMRPNO(int? MRPNO)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            simpleMRP = simpleRepository.Table.Where(x => x.MRPNO == MRPNO.ToString()).FirstOrDefault();
            return simpleMRP;
        }
        public MRPSelectedValues GetIONumber()
        {
            MRPSelectedValues mRPSelectedValues = new MRPSelectedValues();
            mRPSelectedValues = MRPSelectedValuesRepository.Table.Where(x =>x.IsDeleted==false).FirstOrDefault();
            return mRPSelectedValues;
        }
        public MRPSelectedValues checkOrderNumber(string orderNO)
        {
            MRPSelectedValues mRPSelectedValues = new MRPSelectedValues();
            mRPSelectedValues = MRPSelectedValuesRepository.Table.Where(x => x.IsDeleted == false&&x.IONumberID==orderNO).FirstOrDefault();
            return mRPSelectedValues;
        }
        public SimpleMRP IscheckDuplicate(int WeekNO,int SeasonID,int LOTNO,int BuyerID)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            simpleMRP = simpleRepository.Table.Where(x =>x.WeekNO== WeekNO && x.SeasonID== SeasonID && x.LotNO==LOTNO && x.BuyerNameid== BuyerID&&x.IsDeleted==false).FirstOrDefault();
            return simpleMRP;
        }
        public SimpleMRP GetMRPNO(string MRPNO)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            simpleMRP = simpleRepository.Table.Where(x => x.MRPNO == MRPNO &&x.IsDeleted==false).FirstOrDefault();
            return simpleMRP;
        }
        public SimpleMRP GetMRPNO1(string MRPNO)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            simpleMRP = simpleRepository.Table.Where(x => x.MRPNO == MRPNO && x.IsDeleted == false).FirstOrDefault();
            return simpleMRP;
        }
        public SimpleMRP GetMRPCode(string MRPCODE)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            simpleMRP = simpleRepository.Table.Where(x => x.MRPNO == MRPCODE && x.IsDeleted == false).FirstOrDefault();
            return simpleMRP;
        }

        public List<ImportIO> GetImportMRPID(int MRPNO)
        {
            List<ImportIO> simpleMRP = new List<ImportIO>();
            simpleMRP = importIORepository.Table.Where(x => x.SimpleMRPID == MRPNO).ToList();
            return simpleMRP;
        }
        public SimpleMRP Put(SimpleMRP arg)
        {
            SimpleMRP simpleMRP = new SimpleMRP();
            try
            {
                SimpleMRP model = simpleRepository.Table.Where(p => p.SimpleMRPID == arg.SimpleMRPID).FirstOrDefault();
                if (model != null)
                {
                    model = arg;
                    model.CreatedDate = model.CreatedDate;
                    model.CreatedBy = model.CreatedBy;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    // simpleMRP_ = simpleMRPManager.GetMRPNO(Model.MRPNO);
                    model.SimpleMRPID = arg.SimpleMRPID;
                    model.MRPNO = arg.MRPNO;
                    model.MRPType = arg.MRPType;
                    model.BuyerNameid = arg.BuyerNameid;
                    model.WeekNO = arg.WeekNO;
                    model.SeasonID = arg.SeasonID;
                    model.LotNO = arg.LotNO;
                    model.TotalOrderCount = arg.TotalOrderCount;
                    model.SizeRangeID = arg.SizeRangeID;
                    model.From = arg.From;
                    // var dateTimeTO = DateTime.ParseExact(model.TO.ToString(), formats, new CultureInfo("en-US"), DateTimeStyles.None);
                    model.TO = arg.TO;
                    model.CustomerPlan = arg.CustomerPlan;
                    model.ProductionPlanBasic = arg.ProductionPlanBasic;
                    model.OrderBasic = arg.OrderBasic;
                    model.JobWork = arg.JobWork;
                    model.RejectionorReplacement = arg.RejectionorReplacement;
                    model.OverConsumption = arg.OverConsumption;
                    model.Req_Qty = arg.Req_Qty;
                    model.UOM = arg.UOM;
                    model.Rate = arg.Rate;
                    model.TotalNorms = arg.TotalNorms;
                    model.Detailed = arg.Detailed;
                    model.Consolidate = arg.Consolidate;
                    model.IsDeleted = arg.IsDeleted;
                    model.DeletedBy = arg.DeletedBy;
                    model.DeletedOn = arg.DeletedOn;
                    model.MRPDate = (arg.MRPDate);
                    simpleRepository.Update(model);
                    //EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
                    //EmailTemplate emailTemplate = new EmailTemplate();
                    //emailTemplate = emailTemplateManager.GetTemplateName("MRP Update");
                    CompanyManager companyManager = new CompanyManager();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    //List<Company> listCompany = new List<Company>();
                    //listCompany = companyManager.Get();
                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;
                    //    MaterialManager materialManager = new MaterialManager();
                    //    contents = contents.Replace("[[MRPNO]]", !string.IsNullOrEmpty(model.MRPNO) ? model.MRPNO : string.Empty);
                    //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    //    emailTemplate.Body = contents;
                    //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    //}
                    simpleMRP = model;
                }

                else
                {
                    return simpleMRP;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }

            return simpleMRP;
        }




        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                List<ImportIO> importio = new List<ImportIO>();
                importio = importIORepository.GetAll().Where(x => x.SimpleMRPID == id).ToList();
                foreach (var item in importio)
                {
                    importIORepository.Delete(item);
                }
                List<MRPSelectedValues> MRPSelectedValuesList = new List<MRPSelectedValues>();
                MRPSelectedValuesList = MRPSelectedValuesRepository.GetAll().Where(x => x.SimpleMRPID == id).ToList();
                foreach (var item in MRPSelectedValuesList)
                {
                    MRPSelectedValuesRepository.Delete(item);
                }
                SimpleMRP model = simpleRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedOn = DateTime.Now;
                simpleRepository.Delete(model);
                //EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
                //EmailTemplate emailTemplate = new EmailTemplate();
                //emailTemplate = emailTemplateManager.GetTemplateName("MRP Delete");
                CompanyManager companyManager = new CompanyManager();
                StoreMasterManager storeManager = new StoreMasterManager();
                StoreMaster storeMaster = new StoreMaster();
                List<Company> listCompany = new List<Company>();
                listCompany = companyManager.Get();               
                //if (emailTemplate != null)
                //{
                //    string contents = emailTemplate.Body;
                //    MaterialManager materialManager = new MaterialManager();
                //    contents = contents.Replace("[[MRPNO]]", !string.IsNullOrEmpty(model.MRPNO) ? model.MRPNO : string.Empty);            
                //    contents = contents.Replace("[[UserName]]",HttpContext.Current.Session["UserName"].ToString());
                //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                //    emailTemplate.Body = contents;
                //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                //}
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public SimpleMRP Get(int id)
        {
            return null;
        }

        public List<SimpleMRP> Get()
        {
            List<SimpleMRP> simpleMRPList = new List<SimpleMRP>();
            try
            {
                simpleMRPList = simpleRepository.Table.Where(X => X.IsDeleted == false).ToList<SimpleMRP>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return simpleMRPList;
        }
        public List<SimpleMRP> GetAutoMRPNO()
        {
            List<SimpleMRP> simpleMRPList = new List<SimpleMRP>();
            try
            {
                simpleMRPList = simpleRepository.Table.ToList<SimpleMRP>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return simpleMRPList;
        }
        public List<MRPSelectedValues> GeMRPSelectedValuest()
        {
            List<MRPSelectedValues> simpleMRPList = new List<MRPSelectedValues>();
            try
            {
                simpleMRPList = MRPSelectedValuesRepository.Table.Where(X => X.IsDeleted == false).ToList<MRPSelectedValues>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return simpleMRPList;
        }
        #region SelectedPost
        public MRPSelectedValues SimpleMRPSelectedPost(MRPSelectedValues arg)
        {
            MRPSelectedValues mrpSelectedValue = new MRPSelectedValues();
            try
            {
                mrpSelectedValue.IONumberID = arg.IONumberID;
                mrpSelectedValue.SimpleMRPID = arg.SimpleMRPID;
                mrpSelectedValue.DeletedOn = arg.DeletedOn;
                mrpSelectedValue.DeletedBy = arg.DeletedBy;
                mrpSelectedValue.CreatedBy = HttpContext.Current.Session["UserName"].ToString();
                mrpSelectedValue.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                mrpSelectedValue.CreatedDate = DateTime.Now;
                mrpSelectedValue.UpdatedDate = DateTime.Now;
                mrpSelectedValue.IsDeleted = false;

                MRPSelectedValuesRepository.Insert(mrpSelectedValue);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.StackTrace.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                Logger.Log(ex.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                Logger.Log(ex.Source.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

                Logger.Log(ex.TargetSite.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return mrpSelectedValue;
        }
        public MRPSelectedValues SimpleMRPSelectedPut(MRPSelectedValues arg)
        {
            MRPSelectedValues simpleMRP = new MRPSelectedValues();
            try
            {
                MRPSelectedValues model = MRPSelectedValuesRepository.Table.Where(p => p.MRPSelectedID == arg.MRPSelectedID).FirstOrDefault();
                if (model != null)
                {
                    model = arg;
                    model.CreatedDate = model.CreatedDate;
                    model.CreatedBy = model.CreatedBy;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.SimpleMRPID = arg.SimpleMRPID;
                    model.MRPSelectedID = model.MRPSelectedID;
                    model.IONumberID = arg.IONumberID;
                    model.IsDeleted = arg.IsDeleted;
                    model.DeletedBy = arg.DeletedBy;
                    model.DeletedOn = arg.DeletedOn;
                    MRPSelectedValuesRepository.Update(model);
                    simpleMRP = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return simpleMRP;
        }
        #endregion

        public  void BomSizeMatchPost(List<SizeRangeQtyRate> result,string consString,BOMMaterial each, SizeScheduleRange sizeScheduleRange)
        {
            foreach (var iteration_ in result)
            {

                List<SizeRangeQtyRate> rangeQtyRateList = new List<SizeRangeQtyRate>();
                BomSizeMatching bomSizeMatching = new BomSizeMatching();
                String query = "INSERT INTO BomSizeMatching (Size,BomMaterialID,Frame,Qty,Rate,SizeScheduleMasterID) VALUES (@Size,@BomMaterialID, @Frame, @Qty,@Rate,@SizeScheduleMasterID)";
                using (SqlConnection connection = new SqlConnection(consString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.Add("@Size", SqlDbType.Decimal).Value = Convert.ToDecimal(iteration_.SizeRange);
                        command.Parameters.Add("@BomMaterialID", SqlDbType.Int).Value = each.BOMMaterialID;
                        command.Parameters.Add("@Frame", SqlDbType.Decimal).Value = Convert.ToDecimal(iteration_.SizeRange);
                        command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = iteration_.Qty;
                        command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = each.Rate;
                        command.Parameters.Add("@SizeScheduleMasterID", SqlDbType.Int).Value = sizeScheduleRange.SizeScheduleMasterID;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
        }
        public void BulkInsertTOMRPRequirement(List<MRPRequirement> listOfMRPRequirement,string consString)
        {
            DataTable dt = new DataTable();
            dt = ListToDataTable.ConvertToDataTable(listOfMRPRequirement);
            using (SqlConnection connection = new SqlConnection(consString))
            {
                connection.Open();
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection))
                {
                    sqlBulkCopy.ColumnMappings.Add("MRPRequirementID", "MRPRequirementID");
                    sqlBulkCopy.ColumnMappings.Add("BOMID", "BOMID");
                    sqlBulkCopy.ColumnMappings.Add("MaterialCategoryMasterId", "MaterialCategoryMasterId");
                    sqlBulkCopy.ColumnMappings.Add("MaterialGroupMasterId", "MaterialGroupMasterId");
                    sqlBulkCopy.ColumnMappings.Add("SubstanceMasterId", "SubstanceMasterId");
                    sqlBulkCopy.ColumnMappings.Add("MaterialName", "MaterialName");
                    sqlBulkCopy.ColumnMappings.Add("ColorMasterId", "ColorMasterId");
                    sqlBulkCopy.ColumnMappings.Add("SampleNorms", "SampleNorms");
                    sqlBulkCopy.ColumnMappings.Add("Wastage", "Wastage");
                    sqlBulkCopy.ColumnMappings.Add("WastageQty", "WastageQty");
                    sqlBulkCopy.ColumnMappings.Add("WastageQtyUOM", "WastageQtyUOM");
                    sqlBulkCopy.ColumnMappings.Add("TotalNorms", "TotalNorms");
                    sqlBulkCopy.ColumnMappings.Add("TotalNormsUOM", "TotalNormsUOM");
                    sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                    sqlBulkCopy.ColumnMappings.Add("UpdatedDate", "UpdatedDate");
                    sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                    sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                    sqlBulkCopy.ColumnMappings.Add("DeletedBy", "DeletedBy");
                    sqlBulkCopy.ColumnMappings.Add("Deletedon", "Deletedon");
                    sqlBulkCopy.ColumnMappings.Add("IsDeleted", "IsDeleted");
                    sqlBulkCopy.ColumnMappings.Add("RequiredQty", "RequiredQty");
                    sqlBulkCopy.ColumnMappings.Add("IsMRP", "IsMRP");
                    sqlBulkCopy.ColumnMappings.Add("SimpleMRPID", "SimpleMRPID");
                    sqlBulkCopy.ColumnMappings.Add("SupplierMasterID", "SupplierMasterID");
                    sqlBulkCopy.ColumnMappings.Add("SizeScheduleMasterId", "SizeScheduleMasterId");
                    sqlBulkCopy.ColumnMappings.Add("Rate", "Rate");
                    sqlBulkCopy.ColumnMappings.Add("Size", "Size");
                    sqlBulkCopy.ColumnMappings.Add("SizeRangeMasterID", "SizeRangeMasterID");
                    sqlBulkCopy.ColumnMappings.Add("BuyerNorms", "BuyerNorms");
                    sqlBulkCopy.ColumnMappings.Add("NoofSets", "NoofSets");
                    sqlBulkCopy.ColumnMappings.Add("OrderNo", "OrderNo");
                    sqlBulkCopy.ColumnMappings.Add("Conversion", "Conversion");
                    sqlBulkCopy.ColumnMappings.Add("ParentBOMID", "ParentBOMID");
                    sqlBulkCopy.ColumnMappings.Add("ParentCommonBOMID", "ParentCommonBOMID");
                    sqlBulkCopy.DestinationTableName = "MRPRequirement";
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
        }
        public MRPRequirement IsGroupSize(MRPRequirement mrpRequirement,BOMMaterial each,MaterialMaster materialMaster,List<SizeRangeQtyRate> listOfSizeRange,string consString, OrderEntry order)
        {
            try
            {
                BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
                SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
                SizeScheduleRange sizeScheduleRange = new SizeScheduleRange();
                List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                decimal? qty = 0;
                decimal? Amount = 0;
                if (((each.SizeScheduleMasterId != null && each.SizeScheduleMasterId != 0) && (each.SizeRangeMasterID == null || each.SizeRangeMasterID == 0)))
                {
                    List<SizeScheduleRange> listSizeScheduleRange = new List<SizeScheduleRange>();
                    List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
                    listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(each.BOMMaterialID);
                    sizeScheduleRange = sizeScheduleMasterManager.GetSizeScheduleID(each.SizeScheduleMasterId.Value);
                    listSizeScheduleRange = sizeScheduleMasterManager.GetSizeRange(each.SizeScheduleMasterId.Value);
                    if (sizeScheduleRange != null)
                    {
                        sizeRangeQtyRateList = listOfSizeRange.Where(x => x.OrderEntryId == order.OrderEntryId).ToList();
                        sizeRangeQtyRateList = sizeRangeQtyRateList.OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                        var OrderEntryList = (from w1 in sizeRangeQtyRateList
                                              join w2 in listSizeScheduleRange on Convert.ToDecimal(w1.SizeRange) equals w2.Size
                                              select new { w1, w2 }).ToList();
                        var result = sizeRangeQtyRateList.Where(p => !listSizeScheduleRange.Any(p2 => p2.Size == Convert.ToDecimal(p.SizeRange)));
                        BomSizeMatchPost(result.ToList(), consString, each, sizeScheduleRange);
                        mrpRequirement.Size = sizeScheduleRange.Size.ToString();
                        mrpRequirement.RequiredQty = sizeRangeQtyRateList.Sum(x => x.Qty);
                        if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                        {
                            if (materialMaster.IsSecondaryPackage == true)
                            {
                                qty = mrpRequirement.RequiredQty;
                                materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                                if (materialMaster.PurchasePacket != 0)
                                {
                                    mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                                }
                                else
                                {
                                    mrpRequirement.RequiredQty = qty;
                                }
                                mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }
                    else if (listDisplaySizeMaterial != null && listDisplaySizeMaterial.Count > 0)
                    {
                        var displaySize = sizeRangeQtyRateList.Where(p => !listDisplaySizeMaterial.Any(p2 => Convert.ToDecimal(p2.SizeRange) == Convert.ToDecimal(p.SizeRange)));
                    }
                }
                else if (((each.SizeScheduleMasterId != null && each.SizeScheduleMasterId != 0) && (each.SizeRangeMasterID != null && each.SizeRangeMasterID != 0)))
                {
                    List<SizeScheduleRange> listSizeScheduleRange = new List<SizeScheduleRange>();
                    sizeRangeQtyRateList = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(order.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                    listSizeScheduleRange = sizeScheduleMasterManager.GetSizeRange(each.SizeScheduleMasterId.Value);


                mrpRequirement.RequiredQty = sizeRangeQtyRateList.Sum(x => x.Qty);
                if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                {
                    if (materialMaster.IsSecondaryPackage == true)
                    {
                        qty = mrpRequirement.RequiredQty;
                        materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                        if (materialMaster.PurchasePacket != 0)
                        {
                            mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                        }
                        else
                        {
                            mrpRequirement.RequiredQty = qty;
                        }
                        mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                    }

                }
            }
            else if (each.SizeRangeMasterID != null && each.SizeRangeMasterID != 0)
            {
                List<SizeScheduleRange> listSizeScheduleRange = new List<SizeScheduleRange>();
                List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();

                listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(each.BOMMaterialID);
                listDisplaySizeMaterial = listDisplaySizeMaterial.Where(x => x.SizeIsChecked == true).ToList();
                sizeRangeQtyRateList = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(order.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

                if (listDisplaySizeMaterial != null && listDisplaySizeMaterial.Count > 0)
                {
                    var displaySize = sizeRangeQtyRateList.Where(p => listDisplaySizeMaterial.Any(p2 => Convert.ToDecimal(p2.SizeRange) == Convert.ToDecimal(p.SizeRange)));
                    string endurea = ConfigurationManager.AppSettings["EnduraURL"].ToString();
                    string host = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                    string Hostname = "http://" + host;
                    if (!string.IsNullOrEmpty(endurea) && Hostname == endurea)
                    {
                        mrpRequirement.RequiredQty = displaySize.Sum(x => x.Qty) * Convert.ToDecimal(each.BuyerNorms);
                    }
                    else
                    {
                        mrpRequirement.RequiredQty = displaySize.Sum(x => x.Qty) * Convert.ToDecimal(each.TotalNorms);
                    }

                }
            }
            else
            {
                string endurea = ConfigurationManager.AppSettings["EnduraURL"].ToString();
                string host = System.Web.HttpContext.Current.Request.Url.Host.ToString();
                string Hostname = "http://" + host;
                if (!string.IsNullOrEmpty(endurea) && Hostname == endurea)
                {
                    Amount = (Convert.ToDecimal(each.BuyerNorms) * Convert.ToDecimal(order.TotalAmount));
                }
                else
                {
                    Amount = (Convert.ToDecimal(each.TotalNorms) * Convert.ToDecimal(order.TotalAmount));
                }

                    qty = Amount;
                    materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                    if (materialMaster.PurchasePacket != 0)
                    {
                        mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                    }
                    else
                    {
                        mrpRequirement.RequiredQty = qty;
                    }
                    mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                }
                return mrpRequirement;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

    }
}
