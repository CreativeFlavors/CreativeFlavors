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
    public class InwardMaterialPendingQuantityManager : IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<InwardMaterialPendingQuantityMaster> InwardSizeQtyRateRepository;
        private Repository<GateEntryInwardMaterialsMaster> OutwardMasterRateRepository;
        public InwardMaterialPendingQuantityManager()
        {
            InwardSizeQtyRateRepository = unitOfWork.Repository<InwardMaterialPendingQuantityMaster>();
            OutwardMasterRateRepository = unitOfWork.Repository<GateEntryInwardMaterialsMaster>();
        }

        public bool Post(InwardMaterialPendingQuantityMaster arg)
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

                    InwardMaterialPendingQuantityMaster model = InwardSizeQtyRateRepository.Table.Where(p => p.MaterialId == arg.MaterialId).FirstOrDefault();
                    if (model != null)
                    {
                        model.MaterialId = arg.MaterialId;
                        model.Size = arg.Size;
                        model.DcQty = arg.DcQty;
                        model.PoOrderID = arg.PoOrderID;
                        model.PoQuantity = arg.PoQuantity;
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

        public bool PostMaterialSizeQtyRate(InwardMaterialPendingQuantityMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;

                if (arg.PendingQuantityId == 0)
                {
                    InwardSizeQtyRateRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    InwardMaterialPendingQuantityMaster model = InwardSizeQtyRateRepository.Table.Where(p => p.PendingQuantityId == arg.PendingQuantityId).FirstOrDefault();
                    if (model != null)
                    {
                        model.MaterialId = arg.MaterialId;
                        model.Size = arg.Size;
                        model.PoQuantity = arg.PoQuantity;
                        model.DcQty = arg.DcQty;
                        model.PoOrderID = arg.PoOrderID;
                        model.ArrivedQty = arg.ArrivedQty;
                        model.PendingQty = arg.PendingQty;
                        model.GateEntryInwardId = arg.GateEntryInwardId;
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

        public List<InwardMaterialPendingQuantityMaster> Get()
        {
            List<InwardMaterialPendingQuantityMaster> MaterialSizeQtyRate = new List<InwardMaterialPendingQuantityMaster>();
            try
            {
                MaterialSizeQtyRate = InwardSizeQtyRateRepository.Table.ToList<InwardMaterialPendingQuantityMaster>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MaterialSizeQtyRate;
        }
        public List<InwardMaterialPendingQuantityMaster> GetPendingMaterial(int MaterialInwardId)
        {
            List<InwardMaterialPendingQuantityMaster> sizeItemMaterialList = new List<InwardMaterialPendingQuantityMaster>();
            if (MaterialInwardId != 0)
            {
                sizeItemMaterialList = InwardSizeQtyRateRepository.Table.Where(x => x.MaterialId == MaterialInwardId).ToList();
            }
            return sizeItemMaterialList;
        }
        public List<InwardMaterialPendingQuantityMaster> GetInwardIDBasedMaterial(int InwardId,int PoorderID)
        {
            List<InwardMaterialPendingQuantityMaster> sizeItemMaterialList = new List<InwardMaterialPendingQuantityMaster>();
            if (InwardId != 0)
            {
                sizeItemMaterialList = InwardSizeQtyRateRepository.Table.Where(x => x.GateEntryInwardId == InwardId&&x.PoOrderID== PoorderID).ToList();
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


