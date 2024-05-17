using MMS.Common;
using MMS.Core.Entities.GateEntry;
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
    public class OutwardMaterialSizeQtyRateManager : IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<OutwardMaterialSizeQtyRateMaster> OutwardSizeQtyRateRepository;
        private Repository<GateEntryOutwardMaster> OutwardMasterRateRepository;

        public OutwardMaterialSizeQtyRateManager()
        {
            OutwardSizeQtyRateRepository = unitOfWork.Repository<OutwardMaterialSizeQtyRateMaster>();
            OutwardMasterRateRepository = unitOfWork.Repository<GateEntryOutwardMaster>();
        }

        public bool Post(OutwardMaterialSizeQtyRateMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;

                if (arg.MaterialId == 0)
                {
                    OutwardSizeQtyRateRepository.Insert(arg);
                    result = true;
                }
                else if (arg.MaterialId != 0)
                {  
                    OutwardMaterialSizeQtyRateMaster model = OutwardSizeQtyRateRepository.Table.Where(p => p.MaterialId == arg.MaterialId).FirstOrDefault();
                    if (model != null)
                    {

                        model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                        model.MaterialId = arg.MaterialId;
                        model.Size = arg.Size;
                        model.GateEntryOutwardId = arg.GateEntryOutwardId;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        string username_ = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username_;
                        OutwardSizeQtyRateRepository.Update(model);
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

        public bool PostMaterialSizeQtyRate(OutwardMaterialSizeQtyRateMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;

                if (arg.OutwardMaterialSizeQtyRateId == 0)
                {
                    OutwardSizeQtyRateRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    OutwardMaterialSizeQtyRateMaster model = OutwardSizeQtyRateRepository.Table.Where(p => p.OutwardMaterialSizeQtyRateId == arg.OutwardMaterialSizeQtyRateId).FirstOrDefault();
                    if (model != null)
                    {
                        model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                        model.MaterialId = arg.MaterialId;
                        model.Size = arg.Size;
                        model.Quantity = arg.Quantity;
                        model.GateEntryOutwardId = arg.GateEntryOutwardId;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        string username_ = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username_;
                        OutwardSizeQtyRateRepository.Update(model);
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

        public List<OutwardMaterialSizeQtyRateMaster> Get()
        {
            List<OutwardMaterialSizeQtyRateMaster> MaterialSizeQtyRate = new List<OutwardMaterialSizeQtyRateMaster>();
            try
            {
                MaterialSizeQtyRate = OutwardSizeQtyRateRepository.Table.ToList<OutwardMaterialSizeQtyRateMaster>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MaterialSizeQtyRate;
        }

        public List<OutwardMaterialSizeQtyRateMaster> GetSizeItemMaterial(int GateEntryOutwardId)
        {
            List<OutwardMaterialSizeQtyRateMaster> sizeItemMaterialList = new List<OutwardMaterialSizeQtyRateMaster>();
            if (GateEntryOutwardId != 0)
            {
                sizeItemMaterialList = OutwardSizeQtyRateRepository.Table.Where(x => x.GateEntryOutwardId == GateEntryOutwardId).ToList();
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


