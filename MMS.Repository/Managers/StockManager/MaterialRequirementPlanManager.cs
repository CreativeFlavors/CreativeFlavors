using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class MaterialRequirementPlanManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MaterialRequirementPlanning> materialRequirementPlanningRepository;
        private Repository<MRPSizeRange> MPRSizeRangeRepository;
        #region Add/Update/Delete Operations

        public bool Post(MaterialRequirementPlanning arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                materialRequirementPlanningRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(MaterialRequirementPlanning arg)
        {
            bool result = false;
            try
            {
                MaterialRequirementPlanning model = materialRequirementPlanningRepository.Table.Where(m => m.MaterialRequirementPlanId == arg.MaterialRequirementPlanId).SingleOrDefault();
                if (model != null)
                {
                    model.MaterialRequirementPlanId = arg.MaterialRequirementPlanId;
                    model.IsProductionPlanBasis = arg.IsProductionPlanBasis;
                    model.IsOrderBasis = arg.IsOrderBasis;
                    model.IsRejectionOrReplacement = arg.IsRejectionOrReplacement;
                    model.IsOverConsumption = arg.IsOverConsumption;
                    model.MrpNo = arg.MrpNo;
                    model.Buyer = arg.Buyer;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.Date = arg.Date;
                    model.MrpType = arg.MrpType;
                    model.WeekNo = arg.WeekNo;
                    model.SeasonMasterId = arg.SeasonMasterId;
                    model.LotNo = arg.LotNo;
                    model.FROM = arg.FROM;
                    model.TO = arg.TO;
                    model.CustPlan = arg.CustPlan;
                    model.ListOfCategory = arg.ListOfCategory;
                    model.ListOfGroup = arg.ListOfGroup;
                    model.IsCritical = arg.IsCritical;
                    model.IsCustomerSupplied = arg.IsCustomerSupplied;
                    model.IsGeneral = arg.IsGeneral;
                    model.IsImported = arg.IsImported;
                    model.IsReOrder = arg.IsReOrder;
                    model.IsSelectAll = arg.IsSelectAll;
                    model.IsBalToOrder = arg.IsBalToOrder;
                    model.MaterialList = arg.MaterialList;
                    model.TotalNoOfIos = arg.TotalNoOfIos;
                    model.TotalNoOfPrs = arg.TotalNoOfPrs;
                    model.ETA = arg.ETA;
                    model.Weight = arg.Weight;
                    model.ShinkagePercent = arg.ShinkagePercent;
                    model.WastagePercent = arg.WastagePercent;
                    model.ShortagePercent = arg.ShortagePercent;
                    model.Category = arg.Category;
                    model.Material = arg.Material;
                    model.BomQty = arg.BomQty;
                    model.ConversionTable = arg.ConversionTable;
                    model.Wastage = arg.Wastage;
                    model.IsDetailed = arg.IsDetailed;
                    model.IsConsolidated = arg.IsConsolidated;
                    model.MultipleIO = "";
                    model.DisplayIO = arg.DisplayIO;
                    model.CreatedDate = arg.CreatedDate; 
                    model.UpdatedDate = DateTime.Now;
                    model.IsConversionInhouse = arg.IsConversionInhouse;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    materialRequirementPlanningRepository.Update(model);
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
                MaterialRequirementPlanning model = materialRequirementPlanningRepository.GetById(id);
                materialRequirementPlanningRepository.Delete(model);
             
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

        #region Helper Methods

        public MaterialRequirementPlanManager()
        {
            materialRequirementPlanningRepository = unitOfWork.Repository<MaterialRequirementPlanning>();
            MPRSizeRangeRepository = unitOfWork.Repository<MRPSizeRange>();
        }

        public MaterialRequirementPlanning GetMaterialReqPlanId(int MaterialRequirementPlanId)
        {
            MaterialRequirementPlanning materialRequirementPlanning = new MaterialRequirementPlanning();
            if (MaterialRequirementPlanId != 0)
            {
                materialRequirementPlanning = materialRequirementPlanningRepository.Table.Where(x => x.MaterialRequirementPlanId == MaterialRequirementPlanId).SingleOrDefault();

            }
            return materialRequirementPlanning;
        }

        public MaterialRequirementPlanning Get(int id)
        {
            return null;
        }

        public List<MaterialRequirementPlanning> Get()
        {
            List<MaterialRequirementPlanning> materialRequirementPlanning = new List<MaterialRequirementPlanning>();
            try
            {
                materialRequirementPlanning = materialRequirementPlanningRepository.Table.ToList<MaterialRequirementPlanning>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialRequirementPlanning;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        #endregion

        #region MRPSizeRange
        public bool Post(MRPSizeRange arg)
        {
            bool result = false;
            try
            {               
                MPRSizeRangeRepository.Insert(arg);
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
        #region MRPSizeRange Helper 
        public List<MRPSizeRange> MRPSizeRangeGet()
        {
            List<MRPSizeRange> mrpSizeRangeList = new List<MRPSizeRange>();
            try
            {
                mrpSizeRangeList = MPRSizeRangeRepository.Table.ToList<MRPSizeRange>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return mrpSizeRangeList;
        }
        #endregion
    }
}
