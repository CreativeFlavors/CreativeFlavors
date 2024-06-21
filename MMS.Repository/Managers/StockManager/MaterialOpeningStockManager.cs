using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
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
    public class MaterialOpeningStockManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MaterialOpeningMaster> MaterialOpeningMasterRepository;
        private Repository<MaterialOpeningSizeQtyRate> MaterialOpeningSizeQtyRateRepository;

        #region Add/Update/Delete Operation

        public int Post(MaterialOpeningMaster arg)
        {
            int result = 0;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
               
                if (arg.MaterialOpeningId == 0)
                {
                    MaterialOpeningMasterRepository.Insert(arg);
                    result = arg.MaterialOpeningId;
                }
               else if (arg.MaterialOpeningId != 0)
                {
                    // arg.UpdatedBy = username;
                    MaterialOpeningMaster model = MaterialOpeningMasterRepository.Table.Where(p => p.MaterialOpeningId == arg.MaterialOpeningId).FirstOrDefault();
                    if (model != null)
                    {
                        model.Store = arg.Store;
                        model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                        model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                        model.MaterialMasterId = arg.MaterialMasterId;
                        model.ColorMasterId = arg.ColorMasterId;
                        model.PrimaryUomMasterId = arg.PrimaryUomMasterId;
                        model.SecondaryUomMasterId = arg.SecondaryUomMasterId;
                        model.Date = arg.Date;
                        model.Qty = arg.Qty;
                        model.MaterialPcs = arg.MaterialPcs;
                        model.Rate = arg.Rate;                        
                        model.Remarks = arg.Remarks;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        string username_ = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username_;
                        MaterialOpeningMasterRepository.Update(model);
                        result = arg.MaterialOpeningId;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }

        public bool Put(MaterialOpeningMaster arg)
        {
            bool result = false;
            try
            {

                MaterialOpeningMaster model = MaterialOpeningMasterRepository.Table.Where(p => p.MaterialOpeningId == arg.MaterialOpeningId).FirstOrDefault();
                if (model != null)
                {
                    model.Store = arg.Store;
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.MaterialGroupMasterId = arg.MaterialGroupMasterId;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.MaterialCode = arg.MaterialCode;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.PrimaryUomMasterId = arg.PrimaryUomMasterId;
                    model.SecondaryUomMasterId = arg.SecondaryUomMasterId;
                    model.Date = arg.Date;
                    model.Qty = arg.Qty;
                    model.QtyPcs = arg.QtyPcs;
                    model.MaterialPcs = arg.MaterialPcs;
                    model.Rate = arg.Rate;
                    model.Remarks = arg.Remarks;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    MaterialOpeningMasterRepository.Update(model);
                    result = true;
                }
                else
                {
                    return false;
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
                MaterialOpeningMaster model = MaterialOpeningMasterRepository.GetById(id);
                if (model != null)
                {
                    model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                    model.DeletedDate = DateTime.Now;
                    model.IsDeleted = true;
                    MaterialOpeningMasterRepository.Update(model);
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

        public bool PostMaterialOpeningSizeQtyRate(MaterialOpeningSizeQtyRate arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;

                if (arg.MaterialOpeningSizeQtyRateId == 0)
                {
                    MaterialOpeningSizeQtyRateRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    // arg.UpdatedBy = username;
                    MaterialOpeningSizeQtyRate model = MaterialOpeningSizeQtyRateRepository.Table.Where(p => p.MaterialOpeningSizeQtyRateId == arg.MaterialOpeningSizeQtyRateId).FirstOrDefault();
                    if (model != null)
                    {
                        model.Size = arg.Size;
                        model.Quantity = arg.Quantity;
                        model.Rate = arg.Rate;
                        model.MaterialOpeningId = arg.MaterialOpeningId;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        string username_ = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username_;
                        MaterialOpeningSizeQtyRateRepository.Update(model);
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

        public bool DeleteMaterialOpeningSizeQtyRate(int MaterialOpeningId)
        {
            bool result = false;
            try
            {
                List<MaterialOpeningSizeQtyRate> model = GetSizeQuantityRangeById(MaterialOpeningId);
                foreach (var item in model)
                {
                    MaterialOpeningSizeQtyRateRepository.Delete(item);
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

        #endregion

        #region Helper Method

        public MaterialOpeningStockManager()
        {
            MaterialOpeningMasterRepository = unitOfWork.Repository<MaterialOpeningMaster>();
            MaterialOpeningSizeQtyRateRepository = unitOfWork.Repository<MaterialOpeningSizeQtyRate>();
        }

        public MaterialOpeningMaster GetmaterialOpeningId(int MaterialOpeningId)
        {
            MaterialOpeningMaster materialOpeningMaster = new MaterialOpeningMaster();
            if (MaterialOpeningId != 0)
            {
                materialOpeningMaster = MaterialOpeningMasterRepository.Table.Where(x => x.MaterialOpeningId == MaterialOpeningId).FirstOrDefault();
            }
            return materialOpeningMaster;
        }
        public MaterialOpeningMaster iSExistmaterialOpening(int MaterialOpeningId,int MaterialType)
        {
            MaterialOpeningMaster materialOpeningMaster = new MaterialOpeningMaster();
            if (MaterialOpeningId != 0)
            {
                materialOpeningMaster = MaterialOpeningMasterRepository.Table.Where(x => x.MaterialMasterId == MaterialOpeningId&&x.MaterialType==MaterialType&&x.IsDeleted==false).FirstOrDefault();
            }
            return materialOpeningMaster;
        }
        public MaterialOpeningMaster GetmaterialOpeningMaterialID(int? MaterialId)
        {
            MaterialOpeningMaster materialOpeningMaster = new MaterialOpeningMaster();
            if (MaterialId != 0)
            {
                materialOpeningMaster = MaterialOpeningMasterRepository.Table.Where(x => x.MaterialMasterId == MaterialId && x.IsDeleted==false).FirstOrDefault();
            }
            return materialOpeningMaster;
        }

        public MaterialOpeningMaster Get(int id)
        {
            return null;
        }

        public List<MaterialOpeningMaster> Get()
        {
            List<MaterialOpeningMaster> materialOpeningMaster = new List<MaterialOpeningMaster>();

            try
            {
                materialOpeningMaster = MaterialOpeningMasterRepository.Table.ToList<MaterialOpeningMaster>().Where(x=>x.IsDeleted==false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialOpeningMaster;
        }

        public List<MaterialOpeningSizeQtyRate> GetSizeQuantityRangeById(int MaterialOpeningId)
        { 
            List<MaterialOpeningSizeQtyRate> model = new List<MaterialOpeningSizeQtyRate>();
            if (MaterialOpeningId != 0)
            {
                model = MaterialOpeningSizeQtyRateRepository.Table.Where(x => x.MaterialOpeningId == MaterialOpeningId).ToList();
            }
            return model;
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
