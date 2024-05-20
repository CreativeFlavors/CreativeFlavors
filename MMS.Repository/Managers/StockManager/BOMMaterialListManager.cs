using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.StockService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class BOMMaterialListManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BOMMaterial> repository;
        private Repository<BOMMaterialList> BOMMaterialListRepository = null;
        private Repository<MRPRequirement> MRPRequirementRepository = null;
        private Repository<DisplaySizeMaterial> DisplaySizeMaterialRepository = null;
        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();

        public BOMMaterialListManager()
        {
            repository = unitOfWork.Repository<BOMMaterial>();
            BOMMaterialListRepository = unitOfWork.Repository<BOMMaterialList>();
            MRPRequirementRepository = unitOfWork.Repository<MRPRequirement>();
            DisplaySizeMaterialRepository = unitOfWork.Repository<DisplaySizeMaterial>();
        }

        #region Add/Update/Delete Operation

        public BOMMaterialList Post(BOMMaterialList arg)
        {
            BOMMaterialList bOMMaterialList = new BOMMaterialList();
            try
            {

                if (arg.MaterilBomID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    BOMMaterialListRepository.Insert(arg);
                    bOMMaterialList = arg;
                }
                else
                {
                    BOMMaterialList model = BOMMaterialListRepository.Table.Where(p => p.MaterilBomID == arg.MaterilBomID).FirstOrDefault();
                    MMSContext context = new MMSContext();
                    model.MaterilBomID = arg.MaterilBomID;
                    model.BomID = arg.BomID;
                    model.BomNo = arg.BomNo;
                    model.Date = arg.Date;
                    model.ParentBomNo = arg.ParentBomNo;
                    model.CommnBOM1 = arg.CommnBOM1;
                    model.CommnBOM2 = arg.CommnBOM2;
                    model.CommnBOM3 = arg.CommnBOM3;
                    model.CommnBOM4 = arg.CommnBOM4;
                    model.CommnBOM5 = arg.CommnBOM5;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.SampleNorms = arg.SampleNorms;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.Wastage = arg.Wastage;
                    model.WastageQty = arg.WastageQty;
                    model.WastageQtyUOM = arg.WastageQtyUOM;
                    model.TotalNorms = arg.TotalNorms;
                    model.TotalNormsUOM = arg.TotalNormsUOM;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    BOMMaterialListRepository.Update(model);
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    ItesmaterialName = GetMaterial(arg.MaterialMasterId);
                    EmailTemplate emailTemplate = new EmailTemplate();
                    emailTemplate = emailTemplateManager.GetTemplateName("BOM Update");
                    if (emailTemplate != null)
                    {
                        string contents = emailTemplate.Body;
                        MaterialManager materialManager = new MaterialManager();
                        contents = contents.Replace("[[BOMno]]", !string.IsNullOrEmpty(model.BomNo) ? model.BomNo : string.Empty);
                        contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) ? ItesmaterialName.materialdescription : string.Empty : string.Empty);
                        emailTemplate.Body = contents;
                        MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    }
                    bOMMaterialList = arg;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);


            }
            return bOMMaterialList;
        }
        public MRPRequirement MRPRequirementPost(MRPRequirement arg)
        {
            MRPRequirement bomMeterial = new MRPRequirement();
            try
            {
                string username = string.Empty;
                if (arg.MRPRequirementID == 0)
                {
                    if (string.IsNullOrEmpty(arg.CreatedBy))
                    {
                        username = HttpContext.Current.Session["UserName"].ToString();
                    }
                    else
                    {
                        username = arg.CreatedBy;
                    }
                    MRPRequirement bOMMaterial = new MRPRequirement();
                    bOMMaterial = arg;
                    bOMMaterial.CreatedDate = DateTime.Now;
                    bOMMaterial.UpdatedDate = null;
                    bomMeterial.CreatedBy = username;
                    bomMeterial.UpdatedBy = null;
                    MRPRequirementRepository.Insert(bOMMaterial);
                    bomMeterial = bOMMaterial;
                }
                else if (arg.MRPRequirementID != 0)
                {
                    MRPRequirement model = MRPRequirementRepository.Table.Where(x => x.MRPRequirementID == arg.MRPRequirementID).FirstOrDefault();
                    model.MRPRequirementID = model.MRPRequirementID;
                    model.ParentCommonBOMID = model.ParentCommonBOMID;
                    model.ParentBOMID = model.ParentBOMID;
                    model.BOMID = arg.BOMID;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.SubstanceMasterId = arg.SubstanceMasterId;
                    model.MaterialName = arg.MaterialName;
                    model.Conversion = arg.Conversion;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.OrderNo = model.OrderNo;
                    model.SizeScheduleMasterId = arg.SizeScheduleMasterId;
                    model.SampleNorms = arg.SampleNorms;
                    model.Wastage = arg.Wastage;
                    model.SupplierMasterID = arg.SupplierMasterID;
                    model.WastageQty = arg.WastageQty;
                    model.TotalNorms = arg.TotalNorms;
                    model.BuyerNorms = arg.BuyerNorms;
                    model.Rate = arg.Rate;
                    model.SizeRangeMasterID = arg.SizeRangeMasterID;
                    model.NoofSets = model.NoofSets;
                    model.BuyerNorms = model.BuyerNorms;
                    model.WastageQtyUOM = arg.WastageQtyUOM;
                    model.TotalNorms = arg.TotalNorms;
                    model.TotalNormsUOM = arg.TotalNormsUOM;
                    model.CreatedDate = model.CreatedDate;
                    //   model.CreatedBy = model.CreatedBy;
                    model.UpdatedDate = DateTime.Now;
                    username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    MRPRequirementRepository.Update(model);
                    bomMeterial = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //  result = false;
            }
            return bomMeterial;
        }
        public bool MRPRequirementDelete(MRPRequirement bommaterial)
        {
            bool result = false;
            try
            {
                MRPRequirement model = MRPRequirementRepository.GetById(bommaterial.MRPRequirementID);
                if (model != null)
                {
                    //model.IsDeleted = true;
                    model.Deletedon = DateTime.Now;
                    model.IsMRP = false;
                    model.SimpleMRPID = null;
                    model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    MRPRequirementRepository.Update(model);
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
        #endregion
     public List<BOMMaterial> BOMMaterialID(int bomId)
{
    List<BOMMaterial> materialOpeningMaster = new List<BOMMaterial>();
    if (bomId != 0)
    {
        materialOpeningMaster = repository.Table.Where(x => x.BOMID == bomId && x.IsDeleted == false).ToList();
    }
    return materialOpeningMaster;
}


        public List<MRPRequirement> GetMRPMaterialList()
        {
            List<MRPRequirement> mOMMaterialList = new List<MRPRequirement>();
            try
            {
                mOMMaterialList = MRPRequirementRepository.Table.Where(x => x.IsDeleted == false).ToList<MRPRequirement>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return mOMMaterialList;
        }
        public MMS.Data.StoredProcedureModel.ItemMaterial GetMaterial(int MaterialNameID)
        {
            MMS.Data.StoredProcedureModel.ItemMaterial itemList = new MMS.Data.StoredProcedureModel.ItemMaterial();
            try
            {
                itemList = BOMMaterialListRepository.GetMaterial(MaterialNameID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return itemList;
        }
        public List<MRPRequirement> GetMRPMaterialList(int SimpleMrpID)
        {
            List<MRPRequirement> mrpRequirementlList = new List<MRPRequirement>();
            // mrpRequirementlList = MRPRequirementRepository.Table.Where(x => x.IsDeleted == false&&x.SimpleMRPID== SimpleMrpID).ToList<MRPRequirement>();
            // DataTable dt = new DataTable();
            string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
            //String query = "INSERT INTO BomSizeMatching (Size,BomMaterialID,Frame,Qty,Rate,SizeScheduleMasterID) VALUES (@Size,@BomMaterialID, @Frame, @Qty,@Rate,@SizeScheduleMasterID)";
            using (SqlConnection con = new SqlConnection(consString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM  MRPRequirement WHERE SimpleMRPID='" + SimpleMrpID + "'", con))
                {
                    command.ExecuteNonQuery();
                }
                con.Close();
            }


            //}
            //  dt = ListToDataTable.ConvertToDataTable(mrpRequirementlList);
            //using (var bulkCopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString, SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.KeepIdentity))
            //{
            //    bulkCopy.BatchSize = mrpRequirementlList.Count;
            //    bulkCopy.DestinationTableName = "MRPRequirement";
            //    bulkCopy.ColumnMappings.Clear();
            //    bulkCopy.ColumnMappings.Add("MRPRequirementID", "MRPRequirementID");
            //    bulkCopy.ColumnMappings.Add("BOMID", "BOMID");
            //    bulkCopy.ColumnMappings.Add("MaterialCategoryMasterId", "MaterialCategoryMasterId");
            //    bulkCopy.ColumnMappings.Add("MaterialGroupMasterId", "MaterialGroupMasterId");
            //    bulkCopy.ColumnMappings.Add("SubstanceMasterId", "SubstanceMasterId");
            //    bulkCopy.ColumnMappings.Add("MaterialName", "MaterialName");
            //    bulkCopy.ColumnMappings.Add("ColorMasterId", "ColorMasterId");
            //    bulkCopy.ColumnMappings.Add("SampleNorms", "SampleNorms");
            //    bulkCopy.ColumnMappings.Add("Wastage", "Wastage");
            //    bulkCopy.ColumnMappings.Add("WastageQty", "WastageQty");
            //    bulkCopy.ColumnMappings.Add("WastageQtyUOM", "WastageQtyUOM");
            //    bulkCopy.ColumnMappings.Add("TotalNorms", "TotalNorms");
            //    bulkCopy.ColumnMappings.Add("TotalNormsUOM", "TotalNormsUOM");
            //    bulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
            //    bulkCopy.ColumnMappings.Add("UpdatedDate", "UpdatedDate");
            //    bulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
            //    bulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
            //    bulkCopy.ColumnMappings.Add("DeletedBy", "DeletedBy");
            //    bulkCopy.ColumnMappings.Add("Deletedon", "Deletedon");
            //    bulkCopy.ColumnMappings.Add("IsDeleted", "IsDeleted");
            //    bulkCopy.ColumnMappings.Add("RequiredQty", "RequiredQty");
            //    bulkCopy.ColumnMappings.Add("IsMRP", "IsMRP");
            //    bulkCopy.ColumnMappings.Add("SimpleMRPID", "SimpleMRPID");
            //    bulkCopy.ColumnMappings.Add("SupplierMasterID", "SupplierMasterID");
            //    bulkCopy.ColumnMappings.Add("SizeScheduleMasterId", "SizeScheduleMasterId");
            //    bulkCopy.ColumnMappings.Add("Rate", "Rate");
            //    bulkCopy.ColumnMappings.Add("Size", "Size");
            //    bulkCopy.ColumnMappings.Add("SizeRangeMasterID", "SizeRangeMasterID");
            //    bulkCopy.ColumnMappings.Add("BuyerNorms", "BuyerNorms");
            //    bulkCopy.ColumnMappings.Add("NoofSets", "NoofSets");
            //    bulkCopy.ColumnMappings.Add("OrderNo", "OrderNo");
            //    bulkCopy.ColumnMappings.Add("Conversion", "Conversion");
            //    bulkCopy.ColumnMappings.Add("ParentBOMID", "ParentBOMID");
            //    bulkCopy.ColumnMappings.Add("ParentCommonBOMID", "ParentCommonBOMID");
            //    bulkCopy.DestinationTableName = "MRPRequirement";
            //    bulkCopy.WriteToServer(dt);
            //}
            //}

            return mrpRequirementlList;
        }
        public List<BOMMaterialList> Get()
        {
            List<BOMMaterialList> materialList = new List<BOMMaterialList>();

            try
            {
                materialList = BOMMaterialListRepository.Table.ToList<BOMMaterialList>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialList;
        }


        #region Display Size material

        public List<DisplaySizeMaterial> DisplaySizeMaterialGet(int BOMMaterialID)
        {
            List<DisplaySizeMaterial> displaySizeMaterialList = new List<DisplaySizeMaterial>();

            try
            {
                displaySizeMaterialList = DisplaySizeMaterialRepository.Table.ToList<DisplaySizeMaterial>().Where(x => x.IsDeleted == false && x.BOMmaterialID == BOMMaterialID).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return displaySizeMaterialList;
        }
        public List<DisplaySizeMaterial> GetDisplaySizeMaterialGet()
        {
            List<DisplaySizeMaterial> displaySizeMaterialList = new List<DisplaySizeMaterial>();

            try
            {
                displaySizeMaterialList = DisplaySizeMaterialRepository.Table.ToList<DisplaySizeMaterial>().Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return displaySizeMaterialList;
        }
        public DisplaySizeMaterial DisplaySizeBomMaterialDelete(int DisplaySizeMaterialD)
        {
            DisplaySizeMaterial displaySizeMaterialList = new DisplaySizeMaterial();

            try
            {
                DisplaySizeMaterial displaySizeMaterial = DisplaySizeMaterialRepository.Table.Where(p => p.DisplaySizeMaterialD == DisplaySizeMaterialD && p.IsDeleted == false).FirstOrDefault();
                if (displaySizeMaterial != null)
                {
                    displaySizeMaterial.IsDeleted = true;
                    displaySizeMaterial.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    displaySizeMaterial.DeletedOn = DateTime.Now;
                    DisplaySizeMaterialRepository.Delete(displaySizeMaterial);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return displaySizeMaterialList;
        }
        public DisplaySizeMaterial DisplaySizeMaterialDelete(int DisplaySizeMaterialD)
        {
            DisplaySizeMaterial displaySizeMaterialList = new DisplaySizeMaterial();

            try
            {
                DisplaySizeMaterial displaySizeMaterial = DisplaySizeMaterialRepository.Table.Where(p => p.DisplaySizeMaterialD == DisplaySizeMaterialD && p.IsDeleted == false).FirstOrDefault();
                if (displaySizeMaterial != null)
                {
                    DisplaySizeMaterialRepository.Delete(displaySizeMaterial);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return displaySizeMaterialList;
        }
        public DisplaySizeMaterial DisplaySizePost(DisplaySizeMaterial arg)
        {
            DisplaySizeMaterial displaySizeMaterial = new DisplaySizeMaterial();
            try
            {
                if (arg.DisplaySizeMaterialD == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    DisplaySizeMaterial displaySizeMaterial_ = new DisplaySizeMaterial();
                    displaySizeMaterial_ = arg;
                    displaySizeMaterial_.CreatedDate = DateTime.Now;
                    displaySizeMaterial_.UpdatedDate = null;
                    displaySizeMaterial_.UpdatedBy = null;
                    DisplaySizeMaterialRepository.Insert(displaySizeMaterial_);
                    displaySizeMaterial = displaySizeMaterial_;
                }
                else if (arg.DisplaySizeMaterialD != 0)
                {
                    DisplaySizeMaterial model = DisplaySizeMaterialRepository.Table.Where(x => x.DisplaySizeMaterialD == arg.DisplaySizeMaterialD && x.IsDeleted == false).FirstOrDefault();
                    model.DisplaySizeMaterialD = model.DisplaySizeMaterialD;
                    model.BOMmaterialID = arg.BOMmaterialID;
                    model.SizeIsChecked = arg.SizeIsChecked;
                    model.SizeRange = arg.SizeRange;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    DisplaySizeMaterialRepository.Update(model);
                    displaySizeMaterial = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //  result = false;
            }
            return displaySizeMaterial;
        }

       
        #endregion


    }
}
