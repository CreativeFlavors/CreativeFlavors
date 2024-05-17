using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class IndentMaterialManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private Repository<IndentMaterials> indentMaterialsRepository;
        private Repository<Indent> indentRepository;
        private Repository<PurchaseOrder> purchaseOrderRepository;
        private Repository<IndentSizeRangeQty> indentSizeRangeQtyRepository;
        BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
        IndentManager indentManager = new IndentManager();
        #region curd Operation
        public bool Post(IndentMaterials arg)
        {
            bool result = false;
            string username = HttpContext.Current.Session["UserName"].ToString();
            try
            {
                IndentMaterials model = indentMaterialsRepository.Table.Where(p => p.IndentMaterialID == arg.IndentMaterialID).FirstOrDefault();
                if (model != null)
                {
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.StoreId = arg.StoreId;
                    model.MaterialMasterID = arg.MaterialMasterID;
                    model.RequiredQty = arg.RequiredQty;
                    if (arg.IsPo == true)
                    {
                        model.IsPo = true;
                    }
                    else
                    {
                        model.IsPo = arg.IsPo;
                    }
                    model.Value = arg.Value;
                    model.IndentQTY = arg.IndentQTY;
                    model.StoreStock = arg.StoreStock;
                    model.FreeStock = arg.FreeStock;
                    model.SupplierId = arg.SupplierId;
                    model.MaterialMasterID = arg.MaterialMasterID;
                    model.MaterialMasterID = arg.MaterialMasterID;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;

                    indentMaterialsRepository.Update(model);
               //     IndentSizeRangeQtyDelete(model.IndentMaterialID);
                   // List<ClubIndentSizeRange> listIndent = new List<ClubIndentSizeRange>();
                    //listIndent = FromMRPToGetIndentSizerange(arg.MRPNO, arg.MaterialMasterID);
                    //foreach (var item in listIndent)
                    //{
                    //    IndentSizeRangeQty indentSizeRange = new IndentSizeRangeQty();
                    //    indentSizeRange.CreatedDate = DateTime.Now;
                    //    indentSizeRange.CreatedBy = username;
                    //    indentSizeRange.quantity = item.quantity;
                    //    indentSizeRange.Size = item.Size;
                    //    indentSizeRange.IndentMaterialID = arg.IndentMaterialID;
                    //    IndentSizeRangeDetailsPost(indentSizeRange);
                    //}
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    EmailTemplate emailTemplate = new EmailTemplate();
                    ItesmaterialName = bomMaterialListManager.GetMaterial(model.MaterialMasterID.Value);
                    emailTemplate = emailTemplateManager.GetTemplateName("Indent Upade");
                    Indent indent = new Indent();
                    indent = indentManager.GetIndentID(model.IndentID.Value);
                    if (emailTemplate != null)
                    {
                        string contents = emailTemplate.Body;
                        MaterialManager materialManager = new MaterialManager();
                        contents = contents.Replace("[[IndentNo]]", !string.IsNullOrEmpty(indent.IndentNo) ? indent.IndentNo : string.Empty);
                        contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) != null ? ItesmaterialName.materialdescription : string.Empty : string.Empty);
                        emailTemplate.Body = contents;
                        MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    }
                    result = true;
                }

                else
                {
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = null;
                    indentMaterialsRepository.Insert(arg);
                    List<ClubIndentSizeRange> listIndent = new List<ClubIndentSizeRange>();
                    listIndent = FromMRPToGetIndentSizerange(arg.MRPNO, arg.MaterialMasterID);
                    foreach (var item in listIndent)
                    {
                        IndentSizeRangeQty indentSizeRange = new IndentSizeRangeQty();
                        indentSizeRange.CreatedDate = DateTime.Now;
                        indentSizeRange.CreatedBy = username;
                        indentSizeRange.quantity = item.quantity;
                        indentSizeRange.Size = item.Size;
                        indentSizeRange.IndentMaterialID = arg.IndentMaterialID;
                        IndentSizeRangeDetailsPost(indentSizeRange);
                    }
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

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                IndentMaterials model = indentMaterialsRepository.GetById(id);
                Indent indent = new Indent();
                indent = indentManager.GetIndentID(model.IndentID.Value);
                indentMaterialsRepository.Delete(model);
                MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                EmailTemplate emailTemplate = new EmailTemplate();
                ItesmaterialName = bomMaterialListManager.GetMaterial(model.MaterialMasterID.Value);
                emailTemplate = emailTemplateManager.GetTemplateName("Indent Delete");
                if (emailTemplate != null)
                {
                    string contents = emailTemplate.Body;
                    MaterialManager materialManager = new MaterialManager();
                    contents = contents.Replace("[[IndentNo]]", !string.IsNullOrEmpty(indent.IndentNo) ? indent.IndentNo : string.Empty);
                    contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) != null ? ItesmaterialName.materialdescription : string.Empty : string.Empty);
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
        public bool DeleteIndentID(int id)
        {
            bool result = false;
            try
            {
                List<IndentMaterials> model = indentMaterialsRepository.GetAll().Where(x => x.IndentID == id).ToList();
                foreach (var item in model)
                {
                    indentMaterialsRepository.Delete(item);
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
        #region Helper Method
        public IndentMaterialManager()
        {
            indentMaterialsRepository = unitOfWork.Repository<IndentMaterials>();
            purchaseOrderRepository = unitOfWork.Repository<PurchaseOrder>();
            indentSizeRangeQtyRepository = unitOfWork.Repository<IndentSizeRangeQty>();
            indentRepository = unitOfWork.Repository<Indent>();

        }
        public List<ClubIndentSizeRange> FromMRPToGetIndentSizerange(string SimpleMRPID, int? MaterialID)
        {
            List<ClubIndentSizeRange> indentSizeRangeQtyList = new List<ClubIndentSizeRange>();
            indentSizeRangeQtyList = indentSizeRangeQtyRepository.FromMRPToGetIndentSizerange(SimpleMRPID, MaterialID);
            return indentSizeRangeQtyList;
        }
        public List<IndentSizeRangeQty> IndentSizeRangeQtyGet()
        {
            List<IndentSizeRangeQty> tndentSizeRangeQtyList = new List<IndentSizeRangeQty>();
            try
            {
                tndentSizeRangeQtyList = indentSizeRangeQtyRepository.Table.ToList<IndentSizeRangeQty>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return tndentSizeRangeQtyList;
        }
        public List<IndentSizeRangeQty> IndentSizeRangeQtyID(int IndentMaterialID)
        {
            List<IndentSizeRangeQty> tndentSizeRangeQtyList = new List<IndentSizeRangeQty>();
            try
            {
                tndentSizeRangeQtyList = indentSizeRangeQtyRepository.Table.Where(x => x.IndentMaterialID == IndentMaterialID).ToList<IndentSizeRangeQty>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return tndentSizeRangeQtyList;
        }
        public List<ClubIndentSizeRange> GetIndentSizerangeWithMaterial(string IndentID, int? MaterialID)
        {
            List<ClubIndentSizeRange> indentSizeRangeList = new List<ClubIndentSizeRange>();
            indentSizeRangeList = indentSizeRangeQtyRepository.GetIndentSizerangeWithMaterial(IndentID, MaterialID);
            return indentSizeRangeList;
        }
        
        public List<ClubIndentSizeRange> GetIndentSizerange(string Indentid)
        {
            List<ClubIndentSizeRange> tndentSizeRangeQtyList = new List<ClubIndentSizeRange>();
            tndentSizeRangeQtyList = indentSizeRangeQtyRepository.GetIndentSizerange(Indentid);
            return tndentSizeRangeQtyList;
        }
        public bool IndentSizeRangeQtyDelete(int IndentMaterialID)
        {
            bool result = false;
            try
            {
                List<IndentSizeRangeQty> sizeRangeQuantityList = new List<IndentSizeRangeQty>();
                IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                sizeRangeQuantityList = indentMaterialManager.IndentSizeRangeQtyGet().Where(x => x.IndentMaterialID == IndentMaterialID).ToList();
                foreach (var deleteItem in sizeRangeQuantityList)
                {
                    IndentSizeRangeQty model = indentSizeRangeQtyRepository.GetById(deleteItem.IdentSizeRangeID);
                    indentSizeRangeQtyRepository.Delete(model);
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

        public bool IndentSizeRangeDetailsPost(IndentSizeRangeQty arg)
        {
            bool result = false;
            try
            {
                if (arg.IdentSizeRangeID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    //arg.UpdatedBy = username;
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    indentSizeRangeQtyRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    IndentSizeRangeQty model = indentSizeRangeQtyRepository.Table.Where(m => m.IdentSizeRangeID == arg.IdentSizeRangeID).FirstOrDefault();
                    model.IndentMaterialID = arg.IndentMaterialID;
                    model.Size = arg.Size;
                    model.quantity = arg.quantity;
                    model.Rate = arg.Rate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    indentSizeRangeQtyRepository.Update(model);
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
        public List<IndentMaterials> GetMRPID(int MRPNO)
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            try
            {
                SimpleMRPManager MRPManager = new SimpleMRPManager();
                SimpleMRP simpleMRP = new SimpleMRP();
                simpleMRP = MRPManager.GetSimpleMRPID(MRPNO);
                if (simpleMRP != null)
                {
                    IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.MRPNO == simpleMRP.MRPNO).ToList();
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<IndentMaterials> GetIndentID(int IndentID)
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            try
            {
                if (IndentID != 0)
                {
                    IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.IndentID == IndentID && x.IsPo == false).ToList();
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }

        public List<IndentMaterials> GetEditPO(int IndentID)
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            try
            {
                if (IndentID != 0)
                {
                    IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.IndentID == IndentID).ToList();
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<IndentMaterials> GetIndentMaterilDeails(string MRPNO)
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            SimpleMRP simpleMRP = new SimpleMRP();
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            simpleMRP = simpleMRPManager.GetMRPNO(MRPNO);
            try
            {
                IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.MRPNO ==MRPNO).ToList();
                //if (simpleMRP != null)
                //{
                //    IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.MRPNO == simpleMRP.MRPNO).ToList();
                //}

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<IndentMaterials> Get(string MRPNO)
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            try
            {
                IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.MRPNO == MRPNO&&x.IndentID!=0).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<IndentMaterials> IndentRaise(string MRPNO)
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            try
            {
                IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.MRPNO == MRPNO).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<IndentMaterials> Get()
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            try
            {
                IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }

        public IndentMaterials GetIndentMaterialId(int IndentMaterialID)
        {
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialsRepository.Table.Where(x => x.IndentMaterialID == IndentMaterialID).FirstOrDefault();
            return indentMaterial;
        }
        public IndentMaterials GetIsPOUpdate(int? indentid, int? materialid)
        {
            IndentMaterials indentMaterial = new IndentMaterials();
            indentMaterial = indentMaterialsRepository.Table.Where(x => x.IndentID == indentid && x.MaterialMasterID == materialid).FirstOrDefault();
            return indentMaterial;
        }
        public IndentMaterials GetGeneralIndentExist(string MaterialName, string MRPNO,int IndentId)
        {
            IndentMaterials indentMaterial = new IndentMaterials();
            if (MRPNO != "0")
            {
                indentMaterial = indentMaterialsRepository.Table.Where(x => x.MaterialDescription == MaterialName && x.MRPNO == MRPNO && x.IsPo == false).FirstOrDefault();
            }
           else if (IndentId != 0) {
                indentMaterial = indentMaterialsRepository.Table.Where(x => x.MaterialDescription == MaterialName && x.MRPNO =="0" && x.IsPo == false && x.IndentID== IndentId).FirstOrDefault();
            }
            return indentMaterial;
        }
        public List<IndentMaterial> GetIndentIDWithSupplier(int Supplierid, string IndentID)
        {
            List<IndentMaterial> IndentList = new List<IndentMaterial>();

            try
            {
                IndentList = purchaseOrderRepository.GetBOMMaterialbySupplier(Supplierid, IndentID);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }

        public List<IndentMaterial> GetIndentIDWithSupplierPurchaseOrder(int Supplierid, string IndentID)
        {
            List<IndentMaterial> IndentList = new List<IndentMaterial>();

            try
            {
                IndentList = purchaseOrderRepository.GetIndentIDWithSupplierPurchaseOrder(Supplierid, IndentID);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> PurchaseOrderGrid(int? supplier, string IndentID, string PONO)
        {
            List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();

            try
            {
                poList = purchaseOrderRepository.PurchaseOrderGrid(supplier, IndentID, PONO);

                //IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.IndentID == IndentID && x.SupplierId == Supplierid && x.IsPo == false).ToList();


            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return poList;
        }
        public List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> PurchaesOrderedwithNeedToIndent(int? supplier, string IndentID, string PONO)
        {
            List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();
            try
            {
                poList = purchaseOrderRepository.PurchaesOrderedwithNeedToIndent(supplier, IndentID, PONO);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return poList;
        }
        public List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> PurchaseOrderGrid1(string PONO)
        {
            List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();

            try
            {
                poList = purchaseOrderRepository.PurchaseOrderGrid1(PONO);

                //IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.IndentID == IndentID && x.SupplierId == Supplierid && x.IsPo == false).ToList();


            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return poList;
        }
        public List<IndentMaterials> GetEditPOIndentIDWithSupplier(string IndentID, int Supplierid)
        {
            List<IndentMaterials> IndentList = new List<IndentMaterials>();
            try
            {

                IndentList = indentMaterialsRepository.Table.ToList<IndentMaterials>().Where(x => x.IndentID == Convert.ToInt32(IndentID) && x.SupplierId == Supplierid).ToList();


            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }

        public IndentMaterials GetIndentIDInMaterial(int IndentID)
        {
            IndentMaterials IndentList = new IndentMaterials();
            try
            {
                if (IndentID != 0)
                {
                    IndentList = indentMaterialsRepository.Table.Where(x => x.IndentID == IndentID && x.IsPo == false).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        #endregion
    }
}
