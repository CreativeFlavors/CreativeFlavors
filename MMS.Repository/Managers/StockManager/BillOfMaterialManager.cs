using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using MMS.Repository.Service.StockService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MMS.Repository.Managers.StockManager
{
    public class BillOfMaterialManager
    //:IBomService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BillOfMaterial> BillOfMaterialRepository;
        private Repository<BOMAmendmentMaterial> BOMAmendmentMaterialRepository_;
        private Repository<BOMMaterialList> BillOfMaterialListRepository;
        private Repository<BomHistory> BomHistoryRepository;
        private Repository<BOMMaterial> BBOMMaterialRepository;
        private Repository<BomGrid> BomRepository;
        private Repository<Product_BuyerStyleMaster> Product_BuyerStyleRepository;
        private Repository<BuyerOrderCreation> buyerOrderCreationRepository;
        private Repository<BomSizeMatching> bomSizeMatchingRepository;
        private Repository<DisplaySizeMaterial> displaySizeMaterialRepository_;
        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
        #region Add/Update/Delete Operation

        public BOMMaterialList BOMMaterialListPost(BOMMaterialList arg)
        {
            BOMMaterialList bOMMaterialList = new BOMMaterialList();
            try
            {
                if (arg.MaterilBomID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.UpdatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = DateTime.Now;
                    BillOfMaterialListRepository.Insert(arg);
                    bOMMaterialList = arg;
                }
                else
                {
                    BOMMaterialList model = BillOfMaterialListRepository.Table.Where(p => p.MaterilBomID == arg.MaterilBomID).FirstOrDefault();
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
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.CreatedBy = model.CreatedBy;
                    BillOfMaterialListRepository.Update(model);
                    bOMMaterialList = arg;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);


            }
            return bOMMaterialList;
        }
        public BillOfMaterial Post(BillOfMaterial arg)
        {

            BillOfMaterial bom = new BillOfMaterial();
            try
            {
                if (arg.BomId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedBy = "";
                    arg.UpdatedDate = null;
                    arg.PreparedBy = username;
                    BillOfMaterialRepository.Insert(arg);
                    bom = arg;
                }
                else
                {
                    BillOfMaterial model = BillOfMaterialRepository.Table.Where(p => p.BomId == arg.BomId).FirstOrDefault();

                    MMSContext context = new MMSContext();
                    model.BomId = arg.BomId;
                    model.BomNo = arg.BomNo;
                    model.Description = arg.Description;
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.BuyerModel = arg.BuyerModel;
                    model.MeanSize = arg.MeanSize;
                    model.Date = arg.Date;
                    model.ParentBomNo = arg.ParentBomNo;
                    model.LastBomNoEntered = arg.LastBomNoEntered;
                    model.LinkBomNo = arg.LinkBomNo;
                    model.CommnBOM1 = arg.CommnBOM1;
                    model.CommnBOM2 = arg.CommnBOM2;
                    model.CommnBOM3 = arg.CommnBOM3;
                    model.CommnBOM4 = arg.CommnBOM4;
                    model.CommnBOM5 = arg.CommnBOM5;
                    model.Ammendment = arg.Ammendment;
                    model.ComponentName = arg.ComponentName;
                    model.CommonBomNo = arg.CommonBomNo;
                    model.PreparedBy = arg.PreparedBy;
                    model.VerifiedBy = arg.VerifiedBy;
                    model.CheckedBy = arg.CheckedBy;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.SubstanceMasterId = arg.SubstanceMasterId;

                    model.SampleNorms = arg.SampleNorms;
                    model.Wastage = arg.Wastage;
                    model.SupplierMasterId = arg.SupplierMasterId;
                    model.UomMasterId = arg.UomMasterId;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.WastageQty = arg.WastageQty;
                    model.WastageQtyUOM = arg.WastageQtyUOM;
                    model.TotalNorms = arg.TotalNorms;
                    model.TotalNormsUOM = arg.TotalNormsUOM;
                    model.NoOfSets = arg.NoOfSets;
                    model.BuyerNorms = arg.BuyerNorms;
                    model.OurNorms = arg.OurNorms;
                    model.OurNormsPercent = arg.OurNormsPercent;
                    model.PurchaseNorms = arg.PurchaseNorms;
                    model.PurchaseNormsPercent = arg.PurchaseNormsPercent;
                    model.IssueNorms = arg.IssueNorms;
                    model.IssueNormsPercent = arg.IssueNormsPercent;
                    model.CostingNorms = arg.CostingNorms;
                    model.CostingNormsPercent = arg.CostingNormsPercent;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedDate = model.CreatedDate;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.CreatedBy = arg.CreatedBy;
                    BillOfMaterialRepository.Update(model);
                    bom = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);


            }
            return bom;
        }


        public bool Delete(int id)
        {
            bool result = false;
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            List<BomGrid> bomGridList = new List<BomGrid>();
            List<BOMMaterial> bOMMaterialList = new List<BOMMaterial>();
            List<BOMMaterial> bOMMaterialList_email = new List<BOMMaterial>();
            BomGrid bomGrid = new BomGrid();
            try
            {
                bomGridList = BomRepository.Table.Where(p => p.BomId == id).ToList();

                foreach (var item in bomGridList)
                {
                    BomGrid bomModel = BomRepository.Table.Where(p => p.BomId == id).FirstOrDefault();
                    if (bomModel != null)
                    {
                        bomModel.IsDeleted = true;
                        bomModel.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                        bomModel.DeletedDate = DateTime.Now;
                        BomRepository.Update(bomModel);
                    }
                }

                CompanyManager companyManager = new CompanyManager();
                List<Company> listCompany = new List<Company>();
                listCompany = companyManager.Get();
                List<ItemMaterial> myList = new List<ItemMaterial>();
                bOMMaterialList = billOfMaterialManager.GetBomMaterialBOMID(id);
                bOMMaterialList_email = billOfMaterialManager.GetBomMaterialBOMID(id);

                foreach (var item in bOMMaterialList)
                {
                    // BomMaterialDelete(item.BOMMaterialID);
                    // ItesmaterialName = GetMaterial(item.BOMMaterialID);
                    myList.Add(GetMaterial(item.MaterialName));
                }

                BBOMMaterialRepository.BOMMaterial_delete(id);

                if (myList.Count > 0)
                {
                    string Material = string.Join(",", myList.Select(x => x.materialdescription).ToList());
                    BillOfMaterial billOfmaterial = new BillOfMaterial();
                    //EmailTempate emailTemplate = new EmailTempate();

                    //emailTemplate = emailTemplateManager.GetTemplateName("BOM  Mateial Delete");
                    //if (emailTemplate != null)
                    //{

                    //    string contents = emailTemplate.Body;
                    //    MaterialManager materialManager = new MaterialManager();
                    //    contents = contents.Replace("[[BOMno]]", billOfmaterial != null ? !string.IsNullOrEmpty(id.ToString()) ? id.ToString() : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[MaterialName]]", Material != null ? !string.IsNullOrEmpty(Material) ? Material : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    //    contents = contents.Replace("[[CompanyName]]", listCompany.LastOrDefault().CompanyName.ToString());
                    //    emailTemplate.Body = contents;
                    //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    //}
                }

                BillOfMaterial model = BillOfMaterialRepository.GetById(id);
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeletedOn = DateTime.Now;
                    model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    BillOfMaterialRepository.Update(model);
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    /// ItesmaterialName = GetMaterial(model.MaterialMasterId);
                    //EmailTempate emailTemplate = new EmailTempate();
                    //emailTemplate = emailTemplateManager.GetTemplateName("BOM Delete");
                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;
                    //    MaterialManager materialManager = new MaterialManager();
                    //    string host = HttpContext.Current.Request.Url.Host;
                    //    string Username = HttpContext.Current.Session["UserName"].ToString();
                    //    contents = contents.Replace("[[BOMno]]", !string.IsNullOrEmpty(model.BomNo) ? model.BomNo : string.Empty);
                    //    contents = contents.Replace("[[MaterialName]]", "All the material has been deleted");
                    //    contents = contents.Replace("[[UserName]]", Username.ToString());
                    //    contents = contents.Replace("[[CompanyName]]", listCompany.LastOrDefault().CompanyName.ToString());
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
            return result;
        }
        public bool BomGridDelete(int id)
        {
            bool result = false;
            try
            {
                BomGrid model = BomRepository.GetById(id);
                BomRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool BomMaterialDelete_new(int id)
        {
            //List<BOMMaterial> listOfBomMaterial = new List<BOMMaterial>();
            //BillOfMaterial bilOfMaterialBomno = new BillOfMaterial();
            //listOfBomMaterial = GetCOmmonBomMaterialBOMID(ParentBOMId, bilOfMaterialBomno.BomId);

            bool result = false;
            try
            {
                BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
                List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
                listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(id);
                foreach (var item in listDisplaySizeMaterial)
                {
                    bomMaterialListManager.DisplaySizeBomMaterialDelete(item.DisplaySizeMaterialD);
                }
                BOMMaterial model = BBOMMaterialRepository.GetById(id);
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.Deletedon = DateTime.Now;
                    model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    BBOMMaterialRepository.Delete(model);
                    BillOfMaterial billOfmaterial = new BillOfMaterial();
                    //  billOfmaterial = GetbomId(model.BOMID);
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    ItesmaterialName = GetMaterial(model.MaterialName);
                    //EmailTempate emailTemplate = new EmailTempate();
                    //emailTemplate = emailTemplateManager.GetTemplateName("BOM  Mateial Delete");
                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;
                    //    MaterialManager materialManager = new MaterialManager();

                    //    //   string urername = HttpContext.Current.Session.;
                    //    string host = HttpContext.Current.Request.Url.Host;

                    //    string Username = HttpContext.Current.Session["UserName"].ToString();

                    //    contents = contents.Replace("[[BOMno]]", billOfmaterial != null ? !string.IsNullOrEmpty(billOfmaterial.BomNo) ? billOfmaterial.BomNo : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) ? ItesmaterialName.materialdescription : string.Empty : string.Empty);

                    //    contents = contents.Replace("[[Username]]", Username.ToString());
                    //    contents = contents.Replace("[[Company]]", host.ToString());
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
            return result;
        }
        public bool BomMaterialDelete(int id)
        {
            //List<BOMMaterial> listOfBomMaterial = new List<BOMMaterial>();
            //BillOfMaterial bilOfMaterialBomno = new BillOfMaterial();
            //listOfBomMaterial = GetCOmmonBomMaterialBOMID(ParentBOMId, bilOfMaterialBomno.BomId);
            bool result = false;
            try
            {
                BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
                List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
                listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(id);
                foreach (var item in listDisplaySizeMaterial)
                {
                    bomMaterialListManager.DisplaySizeBomMaterialDelete(item.DisplaySizeMaterialD);
                }
                //BOMMaterial model = BBOMMaterialRepository.GetById(id);
                //if (model != null)
                //{
                //    model.IsDeleted = true;
                //    model.Deletedon = DateTime.Now;
                //    model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                //    BBOMMaterialRepository.Delete(model);
                //    BillOfMaterial billOfmaterial = new BillOfMaterial();
                //    //  billOfmaterial = GetbomId(model.BOMID);
                //    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                //    ItesmaterialName = GetMaterial(model.MaterialName);
                //    EmailTempate emailTemplate = new EmailTempate();
                //    emailTemplate = emailTemplateManager.GetTemplateName("BOM  Mateial Delete");
                //    if (emailTemplate != null)
                //    {
                //        string contents = emailTemplate.Body;
                //        MaterialManager materialManager = new MaterialManager();
                //        contents = contents.Replace("[[BOMno]]", billOfmaterial != null ? !string.IsNullOrEmpty(billOfmaterial.BomNo) ? billOfmaterial.BomNo : string.Empty : string.Empty);
                //        contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) ? ItesmaterialName.materialdescription : string.Empty : string.Empty);
                //        emailTemplate.Body = contents;
                //        MMS.Common.EmailHelper.SendEmail(emailTemplate);
                //    }
                //    result = true;
                //}

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        //dont modify this method used for MRP Deleted that time delete bommaterial 
        public bool SimpleMRPMaterialDelete(BOMMaterial bommaterial)
        {
            bool result = false;
            try
            {
                BOMMaterial model = BBOMMaterialRepository.GetById(bommaterial.BOMMaterialID);
                if (model != null)
                {
                    model.Deletedon = DateTime.Now;
                    model.IsMRP = false;
                    model.SimpleMRPID = null;
                    model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    BBOMMaterialRepository.Update(model);
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

        public BomGrid Post(BomGrid arg)
        {
            BomGrid BomGrids_ = new BomGrid();
            try
            {
                if (arg.BomGridId == 0)
                {

                    BomGrid grid = new BomGrid();
                    grid = arg;
                    grid.CreatedDate = DateTime.Now;
                    grid.UpdatedDate = null;
                    BomRepository.Insert(grid);
                    BomGrids_ = grid;
                }
                else if (arg.BomId != 0)
                {
                    BomGrid model = BomRepository.Table.Where(x => x.BomGridId == arg.BomGridId).FirstOrDefault();
                    model.BomGridId = model.BomGridId;
                    model.Component = arg.Component;
                    model.Length = arg.Length;
                    model.Width = arg.Width;
                    model.Nos = arg.Nos;
                    model.SampleNorms = arg.SampleNorms;
                    model.WastagePercent = arg.WastagePercent;
                    model.WastageQtyGrid = arg.WastageQtyGrid;
                    model.Sno = arg.Sno;
                    model.BomId = arg.BomId;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    BomRepository.Update(model);
                    BomGrids_ = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return BomGrids_;
        }

        #endregion

        #region Helper Method
        public MMS.Data.StoredProcedureModel.ItemMaterial GetMaterial(int MaterialNameID)
        {
            MMS.Data.StoredProcedureModel.ItemMaterial itemList = new MMS.Data.StoredProcedureModel.ItemMaterial();
            try
            {
                itemList = BillOfMaterialRepository.GetMaterial(MaterialNameID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return itemList;
        }
        public BillOfMaterialManager()
        {
            BillOfMaterialRepository = unitOfWork.Repository<BillOfMaterial>();
            BOMAmendmentMaterialRepository_ = unitOfWork.Repository<BOMAmendmentMaterial>();
            BomRepository = unitOfWork.Repository<BomGrid>();

            BomHistoryRepository = unitOfWork.Repository<BomHistory>();
            buyerOrderCreationRepository = unitOfWork.Repository<BuyerOrderCreation>();
            Product_BuyerStyleRepository = unitOfWork.Repository<Product_BuyerStyleMaster>();
            BBOMMaterialRepository = unitOfWork.Repository<BOMMaterial>();
            bomSizeMatchingRepository = unitOfWork.Repository<BomSizeMatching>();

        }
        public bool Insert_materialdetail(int BOMNo, int Parent)
        {

            if (BOMNo.ToString() != "")
            {
                BBOMMaterialRepository.BOMMaterial_Insert_parentmaterial(BOMNo, Parent);
            }
            //   string msg = "Save data";
            return true;
        }
        public bool Insert_materialdetail_common(int BOMNo, int Parent)
        {

            if (BOMNo.ToString() != "")
            {
                BBOMMaterialRepository.BOMMaterial_Insert_parentmaterial_common(BOMNo, Parent);
            }
            //   string msg = "Save data";
            return true;
        }
        public BillOfMaterial GetbomId(int BomId)
        {
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            if (BomId != 0)
            {
                billOfMaterial = BillOfMaterialRepository.Table.Where(x => x.BomId == BomId && x.IsDeleted == false).FirstOrDefault();
            }
            return billOfMaterial;
        }

        public BillOfMaterial GetBomNO(string BomNO)
        {
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            if (BomNO != "")
            {
                billOfMaterial = BillOfMaterialRepository.Table.Where(x => x.BomNo == BomNO && x.IsDeleted == false).FirstOrDefault();
            }
            return billOfMaterial;
        }
        public string GetLastbomNumber()
        {
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            string lastBomNo = "";
            List<BillOfMaterial> billOfMaterialList = new List<BillOfMaterial>();
            billOfMaterialList = BillOfMaterialRepository.Table.ToList<BillOfMaterial>();
            if (billOfMaterialList.Count > 0)
            {
                var billOfMaterial_ = (from u in BillOfMaterialRepository.Table
                                       select u.BomId).Max();
                billOfMaterial = BillOfMaterialRepository.Table.Where(x => x.BomId == billOfMaterial_).SingleOrDefault();
                lastBomNo = billOfMaterial.BomNo;
            }

            return lastBomNo;
        }
        public List<BuyerOrderCreation> BomStyleList()
        {
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            var result = buyerOrderCreationRepository.Table.ToList<BuyerOrderCreation>().Where(x => x.IsDeleted == false).ToList();
            return result;
        }

        public BillOfMaterial getLinkBomNumber(string CommnBOM)
        {
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            if (CommnBOM != "")
            {
                billOfMaterial = BillOfMaterialRepository.Table.Where(x => x.LinkBomNo == CommnBOM && x.IsDeleted == false).FirstOrDefault();
            }
            return billOfMaterial;
        }
        public BOMMaterial Chek_toInsertbom(int BOMID, int Parent)
        {
            BOMMaterial billOfMaterial = new BOMMaterial();
            if (BOMID.ToString() != "")
            {
                billOfMaterial = BBOMMaterialRepository.Table.Where(x => x.BOMID == BOMID && x.ParentCommonBOMID == Parent).FirstOrDefault();
            }
            return billOfMaterial;
        }


        public BillOfMaterial getBomNumber(string CommnBOM)
        {
            BillOfMaterial billOfMaterial = new BillOfMaterial();
            if (CommnBOM != "")
            {
                billOfMaterial = BillOfMaterialRepository.Table.Where(x => x.BomNo == CommnBOM).SingleOrDefault();
            }
            return billOfMaterial;
        }
        public BomGrid GetById(int BomGridId)
        {
            BomGrid bomGrid = new BomGrid();
            if (BomGridId != 0)
            {
                bomGrid = BomRepository.Table.Where(x => x.BomGridId == BomGridId).SingleOrDefault();
            }
            return bomGrid;
        }

        public BillOfMaterial Get(int id)
        {
            return null;
        }
        public List<BomHistory> getBomHistoryList()
        {
            List<BomHistory> nomHistorylist = new List<BomHistory>();
            try
            {
                nomHistorylist = BomHistoryRepository.Table.ToList<BomHistory>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return nomHistorylist;
        }
        public List<BomGrid> getBomIDHistoryList(int BOMID, int MaterialBOMID)
        {
            List<BomGrid> nomHistorylist = new List<BomGrid>();
            try
            {
                nomHistorylist = BomRepository.Table.ToList<BomGrid>().Where(x => x.BomId == BOMID && x.BOMMaterialID == MaterialBOMID).OrderBy(x => x.BOMMaterialID).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return nomHistorylist;
        }
        public List<MMS.Web.Models.SPBomMaterialList> BOMMaterialSearch(string Materialname, string BOMNo)
        {
            List<MMS.Web.Models.SPBomMaterialList> BomMaterialList = new List<MMS.Web.Models.SPBomMaterialList>();
            try
            {
                BomMaterialList = BillOfMaterialRepository.BOMMaterialSearch(Materialname, BOMNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BomMaterialList;
        }
        public BOMMaterial getBomMaterialID(int MaterialBOMID)
        {
            BOMMaterial bOMMateriallist = new BOMMaterial();
            try
            {
                bOMMateriallist = BBOMMaterialRepository.Table.Where(x => x.BOMMaterialID == MaterialBOMID).OrderBy(x => x.BOMMaterialID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMateriallist;
        }
        public List<BOMMaterial> getBomMaterialID_List(int MaterialBOMID)
        {
            List < BOMMaterial> bOMMateriallist = new List<BOMMaterial>();
            try
            {
                bOMMateriallist = BBOMMaterialRepository.Table.Where(x => x.BOMMaterialID == MaterialBOMID).OrderBy(x => x.BOMMaterialID).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMateriallist;
        }
        public List<BillOfMaterial> GetBomList(string MaterialDescription)
        {
            List<BillOfMaterial> billOfMaterial = new List<BillOfMaterial>();
            try
            {
                billOfMaterial = BillOfMaterialRepository.SearchBomMaterialList(MaterialDescription);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return billOfMaterial;
        }


        public List<BillOfMaterial> Get()
        {
            List<BillOfMaterial> billOfMaterial = new List<BillOfMaterial>();

            try
            {
                billOfMaterial = BillOfMaterialRepository.Table.ToList<BillOfMaterial>().Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return billOfMaterial;
        }
        public List<BillOfMaterial> Get_Arraylist(string[] LinkBomNo)
        {
            List<BillOfMaterial> billOfMaterial = new List<BillOfMaterial>();

            try
            {
                billOfMaterial = BillOfMaterialRepository.Table.ToList<BillOfMaterial>().Where(x => LinkBomNo.Contains(x.LinkBomNo)  && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return billOfMaterial;
        }

        public List<BOMMaterial> GetBomDetails_Arraylist(int[] BomGridId)
        {

            List<BOMMaterial> bomGrid = new List<BOMMaterial>().ToList();
            int?[] BomGridId_ = BomGridId.Select(i => (int?)i).ToArray();
            bomGrid = BBOMMaterialRepository.Table.Where(x => BomGridId_.Contains(x.BOMID)).ToList();
            return bomGrid;
        }
        public List<BOMMaterialList> GetBOMMaterialList()
        {
            List<BOMMaterialList> billOfMaterial = new List<BOMMaterialList>();

            try
            {
                billOfMaterial = BillOfMaterialListRepository.Table.ToList<BOMMaterialList>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return billOfMaterial;
        }

        public List<BomGrid> GridList()
        {
            List<BomGrid> bomGrid = new List<BomGrid>();

            try
            {
                bomGrid = BomRepository.Table.ToList<BomGrid>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bomGrid;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        #endregion


        public List<BomGrid> GetBomDetails(int BomGridId)
        {
            List<BomGrid> bomGrid = new List<BomGrid>().ToList();
            bomGrid = BomRepository.Table.Where(x => x.BomId == BomGridId).ToList();
            return bomGrid;
        }


        #region BOMAmmendment
        #region CURD OPERATION
        public BOMAmendmentMaterial Post(BOMAmendmentMaterial arg)
        {
            int result = 0;
            BOMAmendmentMaterial bom = new BOMAmendmentMaterial();
            try
            {
                if (arg.BOMAmendmentMaterialID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.PreparedBy = username;
                    BOMAmendmentMaterialRepository_.Insert(arg);
                    bom = arg;
                    bom.BOMAmendmentMaterialID = arg.BOMAmendmentMaterialID;

                }
                else
                {
                    BOMAmendmentMaterial model = BOMAmendmentMaterialRepository_.Table.Where(p => p.BOMAmendmentMaterialID == arg.BOMAmendmentMaterialID).FirstOrDefault();

                    MMSContext context = new MMSContext();
                    model.BOMAmendmentMaterialID = arg.BOMAmendmentMaterialID;
                    model.BOMID = model.BOMID;
                    model.BomNo = arg.BomNo;
                    model.Description = arg.Description;
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.BuyerModel = arg.BuyerModel;
                    model.MeanSize = arg.MeanSize;
                    model.date = arg.date;
                    model.ParentBomNo = arg.ParentBomNo;
                    model.LastBomNoEntered = arg.LastBomNoEntered;
                    model.LinkBomNo = arg.LinkBomNo;
                    model.CommnBOM1 = arg.CommnBOM1;
                    model.CommnBOM2 = arg.CommnBOM2;
                    model.CommnBOM3 = arg.CommnBOM3;
                    model.CommnBOM4 = arg.CommnBOM4;
                    model.CommnBOM5 = arg.CommnBOM5;
                    model.Ammendment = arg.Ammendment;
                    model.ComponentName = arg.ComponentName;
                    model.CommonBomNo = arg.CommonBomNo;
                    model.PreparedBy = arg.PreparedBy;
                    model.VerifiedBy = arg.VerifiedBy;
                    model.CheckedBy = arg.CheckedBy;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.SubstanceMasterId = arg.SubstanceMasterId;
                    model.SampleNorms = arg.SampleNorms;
                    model.Wastage = arg.Wastage;
                    model.SupplierMasterID = arg.SupplierMasterID;
                    model.UomMasterId = arg.UomMasterId;
                    model.SizeRangeMasterID = arg.SizeRangeMasterID;
                    model.WastageQty = arg.WastageQty;
                    model.WastageQtyUOM = arg.WastageQtyUOM;
                    model.TotalNorms = arg.TotalNorms;
                    model.TotalNormsUOM = arg.TotalNormsUOM;
                    model.NoOfSets = arg.NoOfSets;
                    model.BuyerNorms = arg.BuyerNorms;
                    model.OurNorms = arg.OurNorms;
                    model.OurNormsPercent = arg.OurNormsPercent;
                    model.PurchaseNorms = arg.PurchaseNorms;
                    model.PurchaseNormsPercent = arg.PurchaseNormsPercent;
                    model.IssueNorms = arg.IssueNorms;
                    model.IssueNormsPercent = arg.IssueNormsPercent;
                    model.CostingNorms = arg.CostingNorms;
                    model.CostingNormsPercent = arg.CostingNormsPercent;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedDate = arg.CreatedDate;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.CreatedBy = arg.CreatedBy;
                    BOMAmendmentMaterialRepository_.Update(model);
                    bom = model;

                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;

            }
            return bom;
        }
        #endregion

        #region Helper Method
        public BOMAmendmentMaterial GetBOMAmendmentMaterialID(int BOMAmendmentMaterialID)
        {
            BOMAmendmentMaterial BOMAmendmentMaterial = new BOMAmendmentMaterial();
            if (BOMAmendmentMaterialID != 0)
            {
                BOMAmendmentMaterial = BOMAmendmentMaterialRepository_.Table.Where(x => x.BOMAmendmentMaterialID == BOMAmendmentMaterialID && x.IsDeleted == false).FirstOrDefault();
            }
            return BOMAmendmentMaterial;
        }
        public List<BOMAmendmentMaterial> GetBomAmendmentMaterialBOMID(int BOMID)
        {
            List<BOMAmendmentMaterial> bOMMaterialList = new List<BOMAmendmentMaterial>();
            try
            {
                bOMMaterialList = BOMAmendmentMaterialRepository_.Table.ToList<BOMAmendmentMaterial>().Where(x => x.BOMID == BOMID).Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterialList;
        }
        #endregion
        #endregion

        #region BOMHistory
        public bool BOMHisotyPost(BomHistory arg)
        {
            bool result = false;
            try
            {
                if (arg.BOMHistoryId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    BomHistory bomHistory = new BomHistory();
                    bomHistory = arg;
                    bomHistory.CreatedDate = DateTime.Now;
                    bomHistory.UpdatedDate = null;
                    BomHistoryRepository.Insert(bomHistory);
                    result = true;
                }
                else if (arg.BomId != 0)
                {
                    BomHistory model = BomHistoryRepository.Table.Where(x => x.BOMHistoryId == arg.BOMHistoryId).FirstOrDefault();
                    model.BOMHistoryId = model.BOMHistoryId;
                    model.BomNo = arg.BomNo;
                    model.Description = arg.Description;
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.BuyerModel = arg.BuyerModel;
                    model.MeanSize = arg.MeanSize;
                    model.Date = arg.Date;
                    model.ParentBomNo = arg.ParentBomNo;
                    model.LastBomNoEntered = arg.LastBomNoEntered;
                    model.LinkBomNo = arg.LinkBomNo;
                    model.Ammendment = arg.Ammendment;
                    model.CommonBomNo = arg.CommonBomNo;
                    model.CommnBOM1 = arg.CommnBOM1;
                    model.CommnBOM2 = arg.CommnBOM2;
                    model.CommnBOM3 = arg.CommnBOM3;
                    model.CommnBOM4 = arg.CommnBOM4;
                    model.CommnBOM5 = arg.CommnBOM5;
                    model.PreparedBy = arg.PreparedBy;
                    model.VerifiedBy = arg.VerifiedBy;
                    model.CheckedBy = arg.CheckedBy;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;

                    model.ColorMasterId = arg.ColorMasterId;
                    model.SubstanceMasterId = arg.SubstanceMasterId;
                    model.SampleNorms = arg.SampleNorms;
                    model.Wastage = arg.Wastage;
                    model.SupplierMasterId = arg.SupplierMasterId;
                    model.UomMasterId = arg.UomMasterId;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.WastageQty = arg.WastageQty;
                    model.WastageQtyUOM = arg.WastageQtyUOM;
                    model.TotalNorms = arg.TotalNorms;
                    model.TotalNormsUOM = arg.TotalNormsUOM;
                    model.ComponentName = arg.ComponentName;
                    model.NoOfSets = arg.NoOfSets;
                    model.BuyerNorms = arg.BuyerNorms;
                    model.OurNormsPercent = arg.OurNormsPercent;
                    model.PurchaseNormsPercent = arg.PurchaseNormsPercent;
                    model.PurchaseNorms = arg.PurchaseNorms;
                    model.IssueNorms = arg.IssueNorms;
                    model.IssueNormsPercent = arg.IssueNormsPercent;
                    model.CostingNorms = arg.CostingNorms;
                    model.CostingNormsPercent = arg.CostingNormsPercent;
                    model.OurNorms = arg.OurNorms;
                    model.BomId = arg.BomId;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    BomHistoryRepository.Update(model);
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

        #region BOMMaterial

        public BOMMaterial BOMMaterialPost(BOMMaterial arg)
        {
            BOMMaterial bomMeterial = new BOMMaterial();
            try
            {
                if (arg.BOMMaterialID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    BOMMaterial bOMMaterial = new BOMMaterial();
                    bOMMaterial = arg;
                    bOMMaterial.CreatedDate = DateTime.Now;
                    bOMMaterial.UpdatedDate = null;
                    bomMeterial.CreatedBy = username;
                    bomMeterial.UpdatedBy = null;
                    BBOMMaterialRepository.Insert(bOMMaterial);
                    bomMeterial = bOMMaterial;
                }
                else if (arg.BOMMaterialID != 0)
                {
                    BOMMaterial model = BBOMMaterialRepository.Table.Where(x => x.BOMMaterialID == arg.BOMMaterialID).FirstOrDefault();

                    BOMAmendmentMaterial bomAmendmentMaterial = new BOMAmendmentMaterial();
                    BOMAmendmentMaterial bomAmendmentMaterial_ = new BOMAmendmentMaterial();
                    model.BOMMaterialID = model.BOMMaterialID;
                    model.ParentCommonBOMID = model.ParentCommonBOMID;
                    model.ParentBOMID = model.ParentBOMID;
                    model.BOMID = arg.BOMID;
                    model.SNO = arg.SNO;
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
                    model.OurNorms = arg.OurNorms;
                    model.OurNormsPercent = arg.OurNormsPercent;
                    model.PurchaseNorms = arg.PurchaseNorms;
                    model.PurchaseNormsPercent = arg.PurchaseNormsPercent;
                    model.IssueNorms = arg.IssueNorms;
                    model.IssueNormsPercent = arg.IssueNormsPercent;
                    model.CostingNorms = arg.CostingNorms;
                    model.CostingNormsPercent = arg.CostingNormsPercent;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    BBOMMaterialRepository.Update(model);
                    bomMeterial = model;
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    ItesmaterialName = GetMaterial(model.MaterialName);
                    BillOfMaterial billofMaterial = new BillOfMaterial();
                    billofMaterial = GetbomId(model.BOMID);
                    EmailTempate emailTemplate = new EmailTempate();
                    CompanyManager companyManager = new CompanyManager();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    List<Company> listCompany = new List<Company>();
                    listCompany = companyManager.Get();
                    MaterialManager materialManager = new MaterialManager();
                    MaterialMaster materialMaster = new MaterialMaster();
                    materialMaster = materialManager.GetMaterialMasterId(arg.MaterialName);
                    storeMaster = storeManager.GetStoreMasterId(materialMaster.StoreMasterId);
                    //emailTemplate = emailTemplateManager.GetTemplateName("BOM Update");
                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;                      
                    //    contents = contents.Replace("[[BOMno]]", billofMaterial != null ? !string.IsNullOrEmpty(billofMaterial.BomNo) ? billofMaterial.BomNo : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) ? ItesmaterialName.materialdescription : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[StoreName]]", storeMaster != null ? storeMaster.StoreName != null ? storeMaster.StoreName : string.Empty : string.Empty);
                    //    emailTemplate.Body = contents;
                    //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //  result = false;
            }
            return bomMeterial;
        }

        public List<BOMMaterial> GetBomMaterialList()
        {
            List<BOMMaterial> mOMMaterialList = new List<BOMMaterial>();
            try
            {
                mOMMaterialList = BBOMMaterialRepository.Table.Where(x => x.IsDeleted == false).ToList<BOMMaterial>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return mOMMaterialList;
        }
        public BOMMaterial GetBomMaterialID(int BomMateialID)
        {
            BOMMaterial BOMMaterial = new BOMMaterial();
            try
            {
                BOMMaterial = BBOMMaterialRepository.Table.Where(x => x.IsDeleted == false && x.BOMMaterialID == BomMateialID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BOMMaterial;
        }
        public List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> GetMaterialList()
        {
            List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel> bOMMaterialList = new List<MMS.Data.StoredProcedureModel.MaterialDropDownmodel>();
            try
            {
                bOMMaterialList = BBOMMaterialRepository.GetMaterialDropDown();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterialList;
        }
        public List<BOMMaterial> GetBomMaterialBOMID(int BOMID)
        {
            List<BOMMaterial> bOMMaterialList = new List<BOMMaterial>();
            try
            {
                bOMMaterialList = BBOMMaterialRepository.Table.ToList<BOMMaterial>().Where(x => x.BOMID == BOMID).Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterialList;
        }
        public List<BOMMaterial> GetCOmmonBomMaterialBOMID(int BOMID, int ParaentBOMID)
        {
            List<BOMMaterial> bOMMaterialList = new List<BOMMaterial>();
            try
            {
                bOMMaterialList = BBOMMaterialRepository.Table.ToList<BOMMaterial>().Where(x => x.ParentCommonBOMID == BOMID && x.BOMID == ParaentBOMID).Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterialList;
        }

        public BOMMaterial getBOMIDWithMaterial(int bomID, int materialMasterID)
        {
            BOMMaterial bOMMaterial = new BOMMaterial();
            try
            {
                bOMMaterial = BBOMMaterialRepository.Table.Where(x => x.BOMID == bomID && x.MaterialName == materialMasterID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterial;
        }
        public BOMMaterial IsExistMaterial(int category, int GroupMasterID, int MaterialID, int ColorID, int BOMID)
        {
            BOMMaterial bOMMaterial = new BOMMaterial();
            try
            {
                bOMMaterial = BBOMMaterialRepository.Table.Where(x => x.MaterialCategoryMasterId == category && x.MaterialGroupMasterId == GroupMasterID && x.MaterialName == MaterialID && x.ColorMasterId == ColorID && x.BOMID == BOMID && x.IsDeleted == false).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterial;
        }
        #endregion

        #region bomSizeMatching
        public BomSizeMatching BOMSizeMatchingPost(BomSizeMatching arg)
        {
            BomSizeMatching bomSizeMatching = new BomSizeMatching();
            try
            {
                if (arg.BOMSizeMatchingID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    BomSizeMatching bomSizeMatching_ = new BomSizeMatching();
                    bomSizeMatching_ = arg;
                    bomSizeMatching_.CreatedDate = DateTime.Now;
                    bomSizeMatching_.UpdatedDate = null;
                    bomSizeMatching_.CreatedBy = username;
                    bomSizeMatching_.UpdatedBy = null;
                    bomSizeMatchingRepository.Insert(bomSizeMatching_);
                    bomSizeMatching = bomSizeMatching_;
                }
                else if (arg.BOMSizeMatchingID != 0)
                {
                    BomSizeMatching model = bomSizeMatchingRepository.Table.Where(x => x.BOMSizeMatchingID == arg.BOMSizeMatchingID).FirstOrDefault();
                    model.BOMSizeMatchingID = model.BOMSizeMatchingID;
                    model.BomMaterialID = arg.BomMaterialID;
                    model.Frame = arg.Frame;
                    model.Size = arg.Size;
                    model.Rate = arg.Rate;
                    model.SizeScheduleMasterID = arg.SizeScheduleMasterID;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    bomSizeMatchingRepository.Update(model);
                    bomSizeMatching = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return bomSizeMatching;
        }

        public List<BomSizeMatching> GetBomSizeMatching()
        {
            List<BomSizeMatching> mOMMaterialList = new List<BomSizeMatching>();
            try
            {
                mOMMaterialList = bomSizeMatchingRepository.Table.Where(x => x.IsDeleted == false).ToList<BomSizeMatching>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return mOMMaterialList;
        }
        public List<BomSizeMatching> GetBomSizeMatching(int BOMMaterialID)
        {
            List<BomSizeMatching> mOMMaterialList = new List<BomSizeMatching>();
            try
            {
                mOMMaterialList = bomSizeMatchingRepository.Table.Where(x => x.IsDeleted == false && x.BomMaterialID == BOMMaterialID).ToList<BomSizeMatching>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return mOMMaterialList;
        }

        #endregion


    }

}
