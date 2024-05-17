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
    
     public class FloorReturnMaterialDetailsManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
       
        private Repository<FloorReturnMaterialDetails> floorMaterialDetailsRepository;
      

        public FloorReturnMaterialDetailsManager()
        {
            
            floorMaterialDetailsRepository = unitOfWork.Repository<FloorReturnMaterialDetails>();
          
        }

        #region Add/Update/Delete Operations

        public FloorReturnMaterialDetails Post(FloorReturnMaterialDetails arg)
        {
            FloorReturnMaterialDetails result = new FloorReturnMaterialDetails();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                // arg.UpdatedBy = username;
                floorMaterialDetailsRepository.Insert(arg);
                result = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                
            }
            return result;
        }

        public FloorReturnMaterialDetails Put(FloorReturnMaterialDetails arg)
        {
            FloorReturnMaterialDetails model = new FloorReturnMaterialDetails();
            try
            {
                model = floorMaterialDetailsRepository.Table.Where(p => p.FloorReturnMaterialDetailsId == arg.FloorReturnMaterialDetailsId).FirstOrDefault();
                if (model != null)
                {
                    model.FloorReturnMaterialId = arg.FloorReturnMaterialId;              
                    model.StoreMasterId = arg.StoreMasterId;
                    model.GroupMasterId = arg.GroupMasterId;                  
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.IoNo = arg.IoNo;
                    model.Uom = arg.Uom;
                    model.Style = arg.Style;
                    model.IssuedQuantity = arg.IssuedQuantity;
                    model.ReturnQuantity = arg.ReturnQuantity;
                    model.Rate = arg.Rate;
                    model.LotNo = arg.LotNo;
                    model.Category = arg.Category;
                    model.PiecesCurrentType = arg.PiecesCurrentType;
                    model.Piecies = arg.Piecies;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.MaterialType = arg.MaterialType;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    floorMaterialDetailsRepository.Update(model);
                   
                }
                else
                {
                    return model;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
              
            }
            return model;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                FloorReturnMaterialDetails model = floorMaterialDetailsRepository.GetById(id);
                floorMaterialDetailsRepository.Delete(model);
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



        public FloorReturnMaterialDetails GetFloorReturnMaterialId(int FloorReturnMaterialDetailsId)
        {
            FloorReturnMaterialDetails floorMaterial = new FloorReturnMaterialDetails();
            if (FloorReturnMaterialDetailsId != 0)
            {
                floorMaterial = floorMaterialDetailsRepository.Table.Where(x => x.FloorReturnMaterialDetailsId == FloorReturnMaterialDetailsId).SingleOrDefault();
            }
            return floorMaterial;
        }

        
        public List<FloorReturnMaterialDetails> Get()
        {
            List<FloorReturnMaterialDetails> floorMaterial = new List<FloorReturnMaterialDetails>();
            try
            {
                floorMaterial = floorMaterialDetailsRepository.Table.ToList<FloorReturnMaterialDetails>();
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
                floorreturnlist = floorMaterialDetailsRepository.SearchFloorReturnList(IssueNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return floorreturnlist;
        }

        public List<FloorReturnMaterialGrid> GetFloorReturnDetails(int FloorReturnMaterialDetailsId)
        {
            List<FloorReturnMaterialGrid> floorreturnlist = new List<FloorReturnMaterialGrid>();
            try
            {
                floorreturnlist = floorMaterialDetailsRepository.FloorReturnDetails(FloorReturnMaterialDetailsId);
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
