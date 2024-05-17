using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class BuyerOrderCreationManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BuyerOrderCreation> BuyerOrderMasterRepository;
        private Repository<MaterialMaster> MaterialrMasterRepository;
        #region Add/Update/Delete Operation

        public bool Post(BuyerOrderCreation arg)
        {
            bool result = false;
            try
            {
                if (arg.BuyerOrderCreationID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = null;
                    arg.Deletedon = null;
                    BuyerOrderMasterRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    BuyerOrderCreation model = BuyerOrderMasterRepository.Table.Where(p => p.BuyerOrderCreationID == arg.BuyerOrderCreationID && p.IsDeleted == false).FirstOrDefault();
                    if (model != null)
                    {
                        model.BuyerOrderCreationID = arg.BuyerOrderCreationID;
                        //model.GroupID = arg.GroupID;
                        model.BuyerId = arg.BuyerId;
                        model.MaterialCode = arg.MaterialCode;
                        model.MaterialName = arg.MaterialName;
                        model.StockUnit = arg.StockUnit;
                        model.SizeRange = arg.SizeRange;
                        model.StandardPrice = arg.StandardPrice;
                        model.ComplexitityFactor = arg.ComplexitityFactor;
                        model.SketchNo = arg.SketchNo;
                        model.LeatherMainRawMaterial = arg.LeatherMainRawMaterial;
                        model.ProductColor = arg.ProductColor;
                        model.BuyerStyleNo = arg.BuyerStyleNo;
                        model.IsDeleted = arg.IsDeleted;
                        model.CreatedDate = model.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        model.CreatedBy = model.CreatedBy;
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        BuyerOrderMasterRepository.Update(model);
                        result = true;
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
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                BuyerOrderCreation model = BuyerOrderMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.Deletedon = DateTime.Now;
                BuyerOrderMasterRepository.Update(model);
                //  CountryMasterRepository.Delete(model);
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

        #region Helper Method

        public BuyerOrderCreationManager()
        {
            BuyerOrderMasterRepository = unitOfWork.Repository<BuyerOrderCreation>();
            MaterialrMasterRepository = unitOfWork.Repository<MaterialMaster>();
        }
        public BuyerOrderCreation GetMaterialCode(string MaterialCode)
        {
            BuyerOrderCreation buyerOrderCreation = new BuyerOrderCreation();
            if (MaterialCode != "" && MaterialCode != null)
            {
                buyerOrderCreation = BuyerOrderMasterRepository.Table.Where(x => x.MaterialCode == MaterialCode && x.IsDeleted == false).SingleOrDefault();
            }
            return buyerOrderCreation;
        }
        public MaterialMaster GetMaterialMasterID(int MaterialMasterid)
        {
            MaterialMaster materialMaster = new MaterialMaster();
            if (MaterialMasterid != 0)
            {
                materialMaster = MaterialrMasterRepository.Table.Where(x => x.MaterialMasterId == MaterialMasterid && (x.IsDeleted == false || x.IsDeleted==null)).FirstOrDefault();


            }
            return materialMaster;
        }
        public BuyerOrderCreation GetOurStyleID(string OurStyle)
        {
            BuyerOrderCreation materialMaster = new BuyerOrderCreation();
            if (OurStyle != "")
            {
                int buyerOrderid = Convert.ToInt32(OurStyle);
                materialMaster = BuyerOrderMasterRepository.Table.Where(x => x.BuyerOrderCreationID == buyerOrderid && x.IsDeleted == false).FirstOrDefault();


            }
            return materialMaster;
        }
        public BuyerOrderCreation GetBuyerOrderCreationID(int BuyerOrderCreationID)
        {
            BuyerOrderCreation buyerOrderCreation = new BuyerOrderCreation();
            if (BuyerOrderCreationID != 0)
            {
                buyerOrderCreation = BuyerOrderMasterRepository.Table.Where(x => x.BuyerOrderCreationID == BuyerOrderCreationID && x.IsDeleted == false).SingleOrDefault();


            }
            return buyerOrderCreation;
        }
        public BuyerOrderCreation Get(int id)
        {
            return null;
        }
        public List<BuyerOrderCreation> Get()
        {
            List<BuyerOrderCreation> buyerOrderCreationList = new List<BuyerOrderCreation>();
            try
            {
                buyerOrderCreationList = BuyerOrderMasterRepository.Table.Where(X => X.IsDeleted == false).ToList<BuyerOrderCreation>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return buyerOrderCreationList;
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        #endregion
    }
}
