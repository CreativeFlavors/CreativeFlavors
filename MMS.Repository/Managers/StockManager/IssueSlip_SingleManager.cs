using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Common;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.StoredProcedureModel;
using MMS.Web.Models;
using MMS.Core.Entities;

namespace MMS.Repository.Managers.StockManager
{
    public class IssueSlip_SingleManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<IssueSlip> IssueSlipSingleRepository;
        private Repository<MultipleIssueSlip> multipleIssueSlipRepository;
        private Repository<IssueSlip_MaterialDetails> issueSlip_MaterialDetailsepository;
        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
        public IssueSlip_SingleManager()
        {
            IssueSlipSingleRepository = unitOfWork.Repository<IssueSlip>();
            multipleIssueSlipRepository = unitOfWork.Repository<MultipleIssueSlip>();
            issueSlip_MaterialDetailsepository = unitOfWork.Repository<IssueSlip_MaterialDetails>();
        }

        #region Single Issue Slip Helper Method
        public List<IssueSlip> Get()
        {
            List<IssueSlip> IssueObjList = new List<IssueSlip>();
            try
            {
                IssueObjList = IssueSlipSingleRepository.Table.ToList<IssueSlip>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IssueObjList;
        }
        public List<IssueSlip> GetMultipleIssue()
        {
            List<IssueSlip> IssueObjList = new List<IssueSlip>();
            try
            {
                IssueObjList = IssueSlipSingleRepository.Table.ToList<IssueSlip>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IssueObjList;
        }
        public List<MultipleIssueSlip> GetMultipleIssueSlip()
        {
            List<MultipleIssueSlip> IssueObjList = new List<MultipleIssueSlip>();
            try
            {
                IssueObjList = multipleIssueSlipRepository.Table.ToList<MultipleIssueSlip>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IssueObjList;
        }
        public List<MultipleIssueSlip> GetMultipleIssueSlip(string IssueSlipNo)
        {
            List<MultipleIssueSlip> IssueObjList = new List<MultipleIssueSlip>();
            try
            {
                IssueObjList = multipleIssueSlipRepository.Table.ToList<MultipleIssueSlip>().Where(x=>x.IssueSlipNo== IssueSlipNo).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IssueObjList;
        }
        public decimal GetBuyerOderSlNoWithmaterial(int Material)
        {
            List<IssueSlip> issueSlipList = new List<IssueSlip>();
            decimal AlreadyIssued = 0;
            if (Material != null && Material != 0)
            {
                //  issueSlipList = IssueSlipSingleRepository.Table.ToList().Where(x => x.Material == Material).ToList();
                // AlreadyIssued = issueSlipList.Sum(x => x.CurrentIssues);               

            }
            return AlreadyIssued;
        }
        public List<MMS.Web.Models.SPBomMaterialList> GetBOMMaterialforIssueSlip(int BomNo)
        {
            List<MMS.Web.Models.SPBomMaterialList> bomMaterialList = new List<MMS.Web.Models.SPBomMaterialList>();
            if (BomNo != 0)
            {
                bomMaterialList = IssueSlipSingleRepository.IssueSlipMaterialList(BomNo);

            }
            return bomMaterialList;
        }
        public List<MMS.Web.Models.InteranalBuyerNo> GetInternalOrderList(string LotNo, string SeasonID)
        {
            List<MMS.Web.Models.InteranalBuyerNo> buyerList = new List<MMS.Web.Models.InteranalBuyerNo>();
            buyerList = IssueSlipSingleRepository.GetInternalOrderList(LotNo, SeasonID);
            return buyerList;
        }

        public List<MMS.Web.Models.SPBomMaterialList> GetInternalOrderMaterialforIssueSlip(string BomNo)
        {
            List<MMS.Web.Models.SPBomMaterialList> bomMaterialList = new List<MMS.Web.Models.SPBomMaterialList>();
            if (BomNo != "")
            {
                bomMaterialList = IssueSlipSingleRepository.IssueSlipMaterialList_(BomNo);

            }
            return bomMaterialList;
        }
        public List<string> IsExistsIssuewithMaterial(string IssueSlipNo, string MaterialMasterID, string OrderNO)
        {
            List<string> materialList = IssueSlipSingleRepository.IsExistsIssuewithMaterial(IssueSlipNo, MaterialMasterID, OrderNO);
            return materialList;
        }
        public List<string> IsExistsIssuewithMaterial_jobwork(string IssueSlipNo, string MaterialMasterID, string OrderNO)
        {
            List<string> materialList = IssueSlipSingleRepository.IsExistsIssuewithMaterial_jobwork(IssueSlipNo, MaterialMasterID, OrderNO);
            return materialList;
        }
        public List<string> IsExistsIssuewithMaterial_jobwork_Jobworktype_Id(string IssueSlipNo, string MaterialMasterID, string OrderNO,string Jobworktype_Id)
        {
            List<string> materialList = IssueSlipSingleRepository.IsExistsIssuewithMaterial_jobwork_Jobworktype_Id(IssueSlipNo, MaterialMasterID, OrderNO, Jobworktype_Id);
            return materialList;
        }
        public List<SPMultipleIssueCount> GetMaterialIssue(string OrderNo, string MaterialName)
        {
            List<SPMultipleIssueCount> materialListCount = new List<SPMultipleIssueCount>();
            if (OrderNo != "" && MaterialName != "")
            {
                materialListCount = IssueSlipSingleRepository.MultipleIssuesCount(OrderNo, MaterialName);

            }
            return materialListCount;
        }

        public List<MMS.Web.Models.SPBomMaterialList> GetInternalOrderMaterialforIssueSlip(string BomNo, string MaterialName, string Color)
        {
            List<MMS.Web.Models.SPBomMaterialList> bomMaterialList = new List<MMS.Web.Models.SPBomMaterialList>();
            if (BomNo != "")
            {
                bomMaterialList = IssueSlipSingleRepository.IssueSlipMaterialListforDetails(BomNo, MaterialName, Color);

            }
            return bomMaterialList;
        }
        public BOMMaterial GetBomMaterialDetails(int Material)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterial bomMaterial = new BOMMaterial();
            bomMaterial = billOfMaterialManager.getBomMaterialID(Material);
            return bomMaterial;
        }
        public IssueSlip GetGRNSelectedRow(int IssueID)
        {
            IssueSlip IssueObj = new IssueSlip();
            if (IssueID != 0)
            {
                IssueObj = IssueSlipSingleRepository.Table.Where(x => x.IssueSlipID == IssueID).FirstOrDefault();
            }
            return IssueObj;
        }
        public IssueSlip GetorderNo(string OrderNo)
        {
            IssueSlip IssueObj = new IssueSlip();
            if (OrderNo != "")
            {
                IssueObj = IssueSlipSingleRepository.Table.Where(x => x.InternalOderID == OrderNo).FirstOrDefault();
            }
            return IssueObj;
        }
        #endregion
        #region Multiple Issue Slip Helper Method
        public List<MultipleIssueSlip> MultipleGet()
        {
            List<MultipleIssueSlip> IssueObjList = new List<MultipleIssueSlip>();
            try
            {
                IssueObjList = multipleIssueSlipRepository.Table.ToList<MultipleIssueSlip>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IssueObjList;
        }
        //public List<IssueSlip_MaterialDetails> GetMaterialWithIssueNO(int MaterialID,int IssueSLipNO)
        //{
        //    List<IssueSlip_MaterialDetails> IssueObjList = new List<IssueSlip_MaterialDetails>();
        //    try
        //    {
        //        IssueObjList = multipleIssueSlipRepository.Table.Where(x=>x.IssueSlipNo==IssueSLipNO.ToString()&&x.)ToList<IssueSlip_MaterialDetails>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    return IssueObjList;
        //}

        public List<MultipleIssueSlip> MultipleIssueOrderNo(string OrderNo)
        {
            List<MultipleIssueSlip> IssueObjList = new List<MultipleIssueSlip>();
            try
            {
                IssueObjList = multipleIssueSlipRepository.Table.ToList<MultipleIssueSlip>().Where(x => x.InternalOderID.Contains(OrderNo)).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IssueObjList;
        }
        public decimal GetBuyerOderSlNoWithMultipleIssuematerial(int Material)
        {
            List<MultipleIssueSlip> issueSlipList = new List<MultipleIssueSlip>();
            decimal AlreadyIssued = 0;
            if (Material != null && Material != 0)
            {
                //  issueSlipList = IssueSlipSingleRepository.Table.ToList().Where(x => x.Material == Material).ToList();
                // AlreadyIssued = issueSlipList.Sum(x => x.CurrentIssues);               

            }
            return AlreadyIssued;
        }
        public List<MMS.Web.Models.SPBomMaterialList> GetBOMMaterialforMultipleIssueSlip(int BomNo)
        {
            List<MMS.Web.Models.SPBomMaterialList> bomMaterialList = new List<MMS.Web.Models.SPBomMaterialList>();
            if (BomNo != 0)
            {
                bomMaterialList = IssueSlipSingleRepository.IssueSlipMaterialList(BomNo);

            }
            return bomMaterialList;
        }
        public BOMMaterial GetMultipleIssueBomMaterialDetails(int Material)
        {
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterial bomMaterial = new BOMMaterial();
            bomMaterial = billOfMaterialManager.getBomMaterialID(Material);
            return bomMaterial;
        }
        public MultipleIssueSlip GetMultipleIssueGRNSelectedRow(int IssueID)
        {
            MultipleIssueSlip IssueObj = new MultipleIssueSlip();
            if (IssueID != 0)
            {
                IssueObj = multipleIssueSlipRepository.Table.Where(x => x.MultipleIssueSlipID == IssueID).SingleOrDefault();
            }
            return IssueObj;
        }
        public List<MultipleIssueSlip> GetMultipleIssueGRNSelectedRow_CONTAIN(int[] list)
        {
            List<MultipleIssueSlip> IssueObj = new List<MultipleIssueSlip>();
            
                //IssueObj = multipleIssueSlipRepository.Table.Where(x => x.MultipleIssueSlipID == IssueID).SingleOrDefault();
                IssueObj = multipleIssueSlipRepository.Table.Where(x => list.Contains(x.MultipleIssueSlipID)).ToList();
          
            return IssueObj;
        }
        public MultipleIssueSlip GetMultipleIssueorderNo(string OrderNo)
        {
            MultipleIssueSlip IssueObj = new MultipleIssueSlip();
            if (OrderNo != "")
            {

                IssueObj = multipleIssueSlipRepository.Table.Where(x => x.InternalOderID == OrderNo).FirstOrDefault();
            }
            return IssueObj;
        }
        public List<IssueSlip_SingleModel> GetMultipleIssueGrid(string IssueSlipNo)
        {
            List<IssueSlip_SingleModel> multipleIssueList = new List<IssueSlip_SingleModel>();
            try
            {
                multipleIssueList = multipleIssueSlipRepository.SearchMultipleIssueSlip(IssueSlipNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return multipleIssueList;
        }

        public List<IssueSlip_SingleModel> GetJobWorkMultipleIssueGrid(string IssueSlipNo)
        {
            List<IssueSlip_SingleModel> multipleIssueList = new List<IssueSlip_SingleModel>();
            try
            {
                multipleIssueList = multipleIssueSlipRepository.SearchJobWorkMultipleIssueSlip(IssueSlipNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return multipleIssueList;
        }

        public IssueSlip_MaterialDetails isExixtorderNo(string OrderNo)
        {
            IssueSlip_MaterialDetails IssueObj = new IssueSlip_MaterialDetails();
            if (OrderNo != "")
            {

                IssueObj = issueSlip_MaterialDetailsepository.Table.Where(x => x.OrderNo == OrderNo).FirstOrDefault();
            }
            return IssueObj;
        }
        public IssueSlip_MaterialDetails GetIssueslipID(int IssueSlipID)
        {
            IssueSlip_MaterialDetails IssueObj = new IssueSlip_MaterialDetails();
            if (IssueSlipID != 0)
            {

                IssueObj = issueSlip_MaterialDetailsepository.Table.Where(x => x.IssueSlipId == IssueSlipID).FirstOrDefault();
            }
            return IssueObj;
        }
        public List<IssueSlip_MaterialDetails> GetIssueslipList(string IssueSlipNo)
        {
            List<IssueSlip_MaterialDetails> IssueObj = new List<IssueSlip_MaterialDetails>();
            if (IssueSlipNo != "")
            {

                IssueObj = issueSlip_MaterialDetailsepository.Table.Where(x => x.IssueSlipNo == IssueSlipNo).ToList();
            }
            return IssueObj;
        }
        public List<IssueSlip_MaterialDetails> GetIssueslipOrderNO(string OrderNo)
        {
            List<IssueSlip_MaterialDetails> IssueObj = new List<IssueSlip_MaterialDetails>();
            if (OrderNo != "")
            {

                IssueObj = issueSlip_MaterialDetailsepository.Table.Where(x => x.OrderNo == OrderNo).ToList();
            }
            return IssueObj;
        }
        #endregion

        #region Single Issue slipe CURD
        public IssueSlip Post(IssueSlip arg)
        {
            bool result = false;
            IssueSlip issueSlip = new IssueSlip();
            try
            {
                if (arg.IssueSlipID == 0)
                {
                    IssueSlipSingleRepository.Insert(arg);
                    issueSlip = arg;
                    result = true;
                }
                else
                {
                    IssueSlip model = IssueSlipSingleRepository.Table.Where(m => m.IssueSlipID == arg.IssueSlipID).FirstOrDefault();
                    model.IssueSlipID = arg.IssueSlipID;
                    model.IssueSlipNo = arg.IssueSlipNo;
                    model.InternalOderID = arg.InternalOderID;
                    model.StyleNo = arg.StyleNo;
                    model.StyleNo = arg.StyleNo;
                    model.ConveyorID = arg.ConveyorID;
                    model.MachineName = arg.MachineName;
                    model.SubtanceID = arg.SubtanceID;
                    model.CurrentStock = arg.CurrentStock;
                    model.CurrentStockType = arg.CurrentStockType;
                    model.FreeStock = arg.FreeStock;
                    model.FreeStockType = arg.FreeStockType;
                    model.ReserverQty = arg.ReserverQty;
                    model.ReserverQtyType = arg.ReserverQtyType;
                    model.ClosingStockinAllStores = arg.ClosingStockinAllStores;
                    model.ClosingStockinAllStoredType = arg.ClosingStockinAllStoredType;
                    model.InTransitValue = arg.InTransitValue;
                    model.InTransitType = arg.InTransitType;
                    //model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    IssueSlipSingleRepository.Update(model);
                    result = true;
                    issueSlip = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return issueSlip;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                IssueSlip model = IssueSlipSingleRepository.GetById(id);
                IssueSlipSingleRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool IssueslipItemDelete(int id)
        {
            CompanyManager companyManager = new CompanyManager();
            StoreMasterManager storeManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            List<Company> listCompany = new List<Company>();
            listCompany = companyManager.Get();
            bool result = false;
            try
            {

                IssueSlip_MaterialDetails model = issueSlip_MaterialDetailsepository.GetById(id);
                storeMaster = storeManager.GetStoreMasterId(model.StoreMasterId);
                issueSlip_MaterialDetailsepository.Delete(model);

                //EmailTempate emailTemplate = new EmailTempate();
                //emailTemplate = emailTemplateManager.GetTemplateName("Multiple Issue Mateial Delete");
                //if (emailTemplate != null)
                //{
                //    string contents = emailTemplate.Body;
                //    MaterialManager materialManager = new MaterialManager();
                //    contents = contents.Replace("[[issueno]]", !string.IsNullOrEmpty(model.IssueSlipNo) ? model.IssueSlipNo : string.Empty);
                //    contents = contents.Replace("[[MaterialName]]", !string.IsNullOrEmpty(model.MaterialName) ? model.MaterialName : string.Empty);
                //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                //    contents = contents.Replace("[[CompanyName]]", listCompany!=null? listCompany.Count>0? listCompany.LastOrDefault().CompanyName.ToString():string.Empty:string.Empty);
                //    contents = contents.Replace("[[StoreName]]", storeMaster!=null? storeMaster.StoreName!=null? storeMaster.StoreName :string.Empty: string.Empty);
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
        public bool MultilpleIssueDelete(int id)
        {
            bool result = false;
            CompanyManager companyManager = new CompanyManager();
            StoreMasterManager storeManager = new StoreMasterManager();
            StoreMaster storeMaster = new StoreMaster();
            List<Company> listCompany = new List<Company>();
            listCompany = companyManager.Get();
            try
            {
              
                List<IssueSlip_MaterialDetails> issueSlip_MaterialDetails = issueSlip_MaterialDetailsepository.GetAll().Where(x => x.IssueSlipFK == id && x.IssueSlipType == "Multiple").ToList();
                storeMaster = storeManager.GetStoreMasterId(issueSlip_MaterialDetails.FirstOrDefault().StoreMasterId);
                foreach (var item in issueSlip_MaterialDetails)
                {
                    IssueSlip_MaterialDetails issueSlip_MaterialDetail = issueSlip_MaterialDetailsepository.GetById(item.IssueSlipId);
                    issueSlip_MaterialDetailsepository.Delete(issueSlip_MaterialDetail);
                }
                MultipleIssueSlip model = multipleIssueSlipRepository.GetById(id);
                multipleIssueSlipRepository.Delete(model);
                //EmailTempate emailTemplate = new EmailTempate();
                //EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
                //emailTemplate = emailTemplateManager.GetTemplateName("Multiple Issue Delete");
                //if (emailTemplate != null)
                //{
                //    string contents = emailTemplate.Body;
                //    MaterialManager materialManager = new MaterialManager();
                //    contents = contents.Replace("[[issueno]]", !string.IsNullOrEmpty(model.IssueSlipNo) ? model.IssueSlipNo : string.Empty);
                //    contents = contents.Replace("[[MaterialName]]", "All the material has been deleted");
                //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                //    contents = contents.Replace("[[StoreName]]", storeMaster != null ? storeMaster.StoreName != null ? storeMaster.StoreName : string.Empty : string.Empty);
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
        #endregion

        #region Single Issue slipe CURD
        public MultipleIssueSlip MultipleIssuePost(MultipleIssueSlip arg)
        {
            bool result = false;
            MultipleIssueSlip issueSlip = new MultipleIssueSlip();
            try
            {
                if (arg.MultipleIssueSlipID == 0)
                {
                    arg.CreatedDate = DateTime.Now;
                    arg.CreatedBy = HttpContext.Current.Session["UserName"].ToString();
                    multipleIssueSlipRepository.Insert(arg);
                    issueSlip = arg;
                    result = true;
                }
                else
                {
                    MultipleIssueSlip model = multipleIssueSlipRepository.Table.Where(m => m.MultipleIssueSlipID == arg.MultipleIssueSlipID).FirstOrDefault();
                    model.MultipleIssueSlipID = arg.MultipleIssueSlipID;
                    model.IssueSlipNo = arg.IssueSlipNo;
                    model.InternalOderID = arg.InternalOderID;
                    model.StyleNo = arg.StyleNo;
                    model.StyleNo = arg.StyleNo;
                    model.ConveyorID = arg.ConveyorID;
                    model.MachineName = arg.MachineName;
                    model.SubtanceID = arg.SubtanceID;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    multipleIssueSlipRepository.Update(model);
                    result = true;
                    issueSlip = model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return issueSlip;
        }
        public bool MultipleIssueDelete(int id)
        {
            bool result = false;
            try
            {
                MultipleIssueSlip model = multipleIssueSlipRepository.GetById(id);
                multipleIssueSlipRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        #endregion

        public List<OpeningStockPinkModel> GetOpeningMaterialList()
        {
            List<OpeningStockPinkModel> bOMMaterialList = new List<OpeningStockPinkModel>();
            try
            {
                bOMMaterialList = multipleIssueSlipRepository.OpeningStockPink();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterialList;
        }
        public List<OpeningStockPinkModel> GetOpeningMaterial(int Grn_MaterialID)
        {
            List<OpeningStockPinkModel> bOMMaterialList = new List<OpeningStockPinkModel>();
            try
            {
                bOMMaterialList = multipleIssueSlipRepository.OpeningMaterialStockPink(Grn_MaterialID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return bOMMaterialList;
        }
        #region jobwork
        public MultipleIssueSlip GETJobworktype_ID(int Jobworktype_Id)
        {
            MultipleIssueSlip IssueObj = new MultipleIssueSlip();
            if (Jobworktype_Id != 0)
            {
                IssueObj = multipleIssueSlipRepository.Table.Where(x => x.Jobworktype_Id == Jobworktype_Id).SingleOrDefault();
            }
            return IssueObj;
        }
        public int GETJob_IssueCount()
        {
            int IssueObj = 0;
           
                IssueObj = multipleIssueSlipRepository.Table.Where(x => x.IsJobWork == true && x.GateOut_No != "" && x.GateOut_No != null).Count();
         
            return IssueObj;
        }
        #endregion
    }

}
