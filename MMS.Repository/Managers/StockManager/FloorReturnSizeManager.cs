using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.StoredProcedureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    
    public class FloorReturnSizeManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<FloorReturnSizeRange> floorReturnSizeRepository;

        public FloorReturnSizeManager()
        {
            floorReturnSizeRepository = unitOfWork.Repository<FloorReturnSizeRange>();
        }


        #region Add/Update/Delete Operations

        public bool Post(FloorReturnSizeRange arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                // arg.UpdatedBy = username;
                floorReturnSizeRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(FloorReturnSizeRange arg)
        {
            bool result = false;
            try
            {
                FloorReturnSizeRange model = floorReturnSizeRepository.Table.Where(p => p.SizeRangeFloorMaterialId == arg.SizeRangeFloorMaterialId).FirstOrDefault();
                if (model != null)
                {
                   // model.SizeRangeFloorMaterialId = arg.SizeRangeFloorMaterialId;
                    model.FloorReturnMaterialDetailsId = arg.FloorReturnMaterialDetailsId;
                    model.SizeRange = arg.SizeRange;
                    model.Quantity = arg.Quantity;
                    model.ReturnedQuantity = arg.ReturnedQuantity;
                    //model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    floorReturnSizeRepository.Update(model);
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
                FloorReturnSizeRange model = floorReturnSizeRepository.GetById(id);
                floorReturnSizeRepository.Delete(model);
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

       

        public FloorReturnSizeRange GetFloorReturnMaterialId(int SizeRangeFloorMaterialId)
        {
            FloorReturnSizeRange floorMaterial = new FloorReturnSizeRange();
            if (SizeRangeFloorMaterialId != 0)
            {
                floorMaterial = floorReturnSizeRepository.Table.Where(x => x.SizeRangeFloorMaterialId == SizeRangeFloorMaterialId).SingleOrDefault();
            }
            return floorMaterial;
        }

        public FloorMaterial Get(int id)
        {
            return null;
        }

        public List<FloorReturnSizeRange> Get()
        {
            List<FloorReturnSizeRange> floorMaterial = new List<FloorReturnSizeRange>();
            try
            {
                floorMaterial = floorReturnSizeRepository.Table.ToList<FloorReturnSizeRange>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorMaterial;
        }
        public List<FloorReturnMaterialGrid> GetFloorReturnGrid(string IssueNo)
        {
            List<FloorReturnMaterialGrid> floorreturnlist = new List<FloorReturnMaterialGrid>();
            try
            {
                floorreturnlist = floorReturnSizeRepository.SearchFloorReturnList(IssueNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorreturnlist;
        }

        public List<FloorReturnMaterialGrid> GetFloorReturnDetails(int FloorReturnMaterialId)
        {
            List<FloorReturnMaterialGrid> floorreturnlist = new List<FloorReturnMaterialGrid>();
            try
            {
                floorreturnlist = floorReturnSizeRepository.FloorReturnDetails(FloorReturnMaterialId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorreturnlist;
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
