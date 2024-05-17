using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class ProductionPlanManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ProductionPlanning> productionPlanningRepository;

        #region Add/Update/Delete Operations

        public bool Post(ProductionPlanning arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["Username"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                productionPlanningRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(ProductionPlanning arg)
        {
            bool result = false;
            try
            {
                ProductionPlanning model = productionPlanningRepository.Table.Where(m => m.ProductionPlanId == arg.ProductionPlanId).FirstOrDefault();
                if (model != null)
                {
                    model.PlanNo = arg.PlanNo;
                    model.CustomerName = arg.CustomerName;
                    model.WeekPlan = arg.WeekPlan;
                    model.From = arg.From;
                    model.To = arg.To;
                    model.InHouseCapacity = arg.InHouseCapacity;
                    model.OrderQty = arg.OrderQty;
                    model.PlanQty = arg.PlanQty;
                    //model.MultipleIO = "";
                    //model.DisplayIO = arg.DisplayIO;
                    model.ShipDate = arg.ShipDate;
                    model.ShipTo = arg.ShipTo;
                    model.IsStyleDurea = arg.IsStyleDurea;
                    model.IsStyle = arg.IsStyle;
                    model.IsSelectAll = arg.IsSelectAll;
                    model.Remarks = arg.Remarks;
                    model.PlanQtyInMultiples = arg.PlanQtyInMultiples;
                    model.IsAllocateStyleProp = arg.IsAllocateStyleProp;
                    model.IsAllocateSizeProp = arg.IsAllocateSizeProp;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;

                    productionPlanningRepository.Update(model);
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
                ProductionPlanning model = productionPlanningRepository.GetById(id);
                productionPlanningRepository.Delete(model);
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

        public ProductionPlanManager()
        {
            productionPlanningRepository = unitOfWork.Repository<ProductionPlanning>(); 
        }

        public ProductionPlanning GetProductionPlanId(int ProductionPlanId)
        {
            ProductionPlanning productionPlanning = new ProductionPlanning();
            if (ProductionPlanId != 0)
            {
                productionPlanning = productionPlanningRepository.Table.Where(x => x.ProductionPlanId == ProductionPlanId).SingleOrDefault();
            }
            return productionPlanning;
        }

        public ProductionPlanning Get(int id)
        {
            return null;
        }

        public List<ProductionPlanning> Get()
        {
            List<ProductionPlanning> productionPlanning = new List<ProductionPlanning>();
            try
            {
                productionPlanning = productionPlanningRepository.Table.ToList<ProductionPlanning>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return productionPlanning;
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
