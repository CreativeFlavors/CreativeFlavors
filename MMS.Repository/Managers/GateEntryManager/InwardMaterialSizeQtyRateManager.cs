using MMS.Common;
using MMS.Core.Entities.GateEntry;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.GateEntryManager
{
    public class InwardMaterialSizeQtyRateManager : IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork(); 
        private Repository<InwardMaterialSizeQtyRateMaster> InwardSizeQtyRateRepository;
        private Repository<InwardMaterialSizeQtyRateMasterTemp> InwardSizeQtyRateRepository_Temp;
        private Repository<GateEntryInwardMaterialsMaster> OutwardMasterRateRepository;
        private Repository<PurchaseOrderSizeRangeQuantity> PurchaseRepository;
        public InwardMaterialSizeQtyRateManager()
        {
            InwardSizeQtyRateRepository_Temp = unitOfWork.Repository<InwardMaterialSizeQtyRateMasterTemp>();
            InwardSizeQtyRateRepository = unitOfWork.Repository<InwardMaterialSizeQtyRateMaster>();
            OutwardMasterRateRepository = unitOfWork.Repository<GateEntryInwardMaterialsMaster>();
            PurchaseRepository = unitOfWork.Repository<PurchaseOrderSizeRangeQuantity>();
        }

        public bool Post(InwardMaterialSizeQtyRateMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;

                if (arg.MaterialId == 0)
                {
                    InwardSizeQtyRateRepository.Insert(arg);
                    result = true;
                }
                else if (arg.MaterialId != 0)
                {
                   
                    InwardMaterialSizeQtyRateMaster model = InwardSizeQtyRateRepository.Table.Where(p => p.MaterialId == arg.MaterialId).FirstOrDefault();
                    if (model != null)
                    {
                        model.MaterialId = arg.MaterialId;
                        model.Size = arg.Size;
                        model.PoQty = arg.PoQty;
                        model.DcQty = arg.DcQty;
                        model.ArrivedQty = arg.ArrivedQty;
                        model.GateEntryInwardId = arg.GateEntryInwardId;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        string username_ = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username_;
                        InwardSizeQtyRateRepository.Update(model);
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

        public bool PostMaterialSizeQtyRate(InwardMaterialSizeQtyRateMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;

                if (arg.InwardMaterialSizeQtyRateId == 0)
                {
                    InwardSizeQtyRateRepository.Insert(arg);
                    result = true;
                }
                //InwardMaterialSizeQtyRateMaster model = InwardSizeQtyRateRepository.Table.Where(p => p.InwardMaterialSizeQtyRateId == arg.InwardMaterialSizeQtyRateId).FirstOrDefault();
                //if (model != null)
                //{
                //    model.MaterialId = arg.MaterialId;
                //    model.Size = arg.Size;
                //    model.PoQty = arg.PoQty;
                //    model.DcQty = arg.DcQty;
                //    model.ArrivedQty = arg.ArrivedQty;
                //    model.GateEntryInwardId = arg.GateEntryInwardId;
                //    model.UpdatedDate = DateTime.Now;
                //    string username_ = HttpContext.Current.Session["UserName"].ToString();
                //    model.UpdatedBy = username_;
                //    InwardSizeQtyRateRepository.Update(model);
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
        public bool PostMaterialSizeQtyRate_Temp(InwardMaterialSizeQtyRateMasterTemp arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                InwardSizeQtyRateRepository_Temp.Insert(arg);
          


            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Delete_Temp(int GateEntryInwardId, int MaterialId)
        {
            bool result = false;
            try
            {
               var Temp= InwardSizeQtyRateRepository_Temp.Delete_TempSize( GateEntryInwardId,  MaterialId);
                  result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public List<InwardMaterialSizeQtyRateMaster> Get()
        {
            List<InwardMaterialSizeQtyRateMaster> MaterialSizeQtyRate = new List<InwardMaterialSizeQtyRateMaster>();
            try
            {
                MaterialSizeQtyRate = InwardSizeQtyRateRepository.Table.ToList<InwardMaterialSizeQtyRateMaster>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MaterialSizeQtyRate;
        }
        public List<InwardMaterialSizeQtyRateMaster> Get_LIST(int GateEntryInwardId, int MaterialId)
        {
            List<InwardMaterialSizeQtyRateMaster> MaterialSizeQtyRate = new List<InwardMaterialSizeQtyRateMaster>();
            try
            {
                MaterialSizeQtyRate = InwardSizeQtyRateRepository.Table.Where(X => X.GateEntryInwardId == GateEntryInwardId && X.MaterialId == MaterialId).ToList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MaterialSizeQtyRate;
        }
        public List<InwardMaterialSizeQtyRateMasterTemp> Get_TEMP_LIST(int GateEntryInwardId,int MaterialId)
        {
            List<InwardMaterialSizeQtyRateMasterTemp> MaterialSizeQtyRate = new List<InwardMaterialSizeQtyRateMasterTemp>();
            try
            {
                MaterialSizeQtyRate = InwardSizeQtyRateRepository_Temp.Table.Where(X => X.GateEntryInwardId == GateEntryInwardId && X.MaterialId == MaterialId).ToList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MaterialSizeQtyRate;
        }
        public List<InwardMaterialSizeQtyRateMaster> GetSizeItemMaterial(int MaterialInwardId)
        {
            List<InwardMaterialSizeQtyRateMaster> sizeItemMaterialList = new List<InwardMaterialSizeQtyRateMaster>();
            if (MaterialInwardId != 0)
            {
                sizeItemMaterialList = InwardSizeQtyRateRepository.Table.Where(x => x.GateEntryInwardId == MaterialInwardId).ToList();
            }
            return sizeItemMaterialList;
        }

        public List<PurchaseOrderSizeRangeQuantity> GetquantityMaterial(int PoNoID)
        {
            List<PurchaseOrderSizeRangeQuantity> sizeItemMaterialList = new List<PurchaseOrderSizeRangeQuantity>();
            if (PoNoID != 0)
            {
                sizeItemMaterialList = PurchaseRepository.Table.Where(x => x.PoOrderID == PoNoID).ToList();
            }
            return sizeItemMaterialList;
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


