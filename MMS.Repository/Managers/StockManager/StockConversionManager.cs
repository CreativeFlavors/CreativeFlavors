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
    public class StockConversionManager 
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StockConversionForm> StockConversionFormRepository;

        #region Add/Update/Delete operation

        public bool Post(StockConversionForm arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                StockConversionFormRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(StockConversionForm arg)
        {
            bool result = false;
            try
            {
                StockConversionForm model = StockConversionFormRepository.Table.Where(p => p.StockConversionId == arg.StockConversionId).FirstOrDefault();
                if (model != null)
                {
                    model.DocNo = arg.DocNo;
                    model.FromStore = arg.FromStore;
                    model.ToStore = arg.ToStore;
                    model.Remarks = arg.Remarks;
                    model.Date = arg.Date;
                    model.StockValueChange = arg.StockValueChange;
                    model.MaterialGroupId = arg.MaterialGroupId;
                    model.UomId = arg.UomId;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.ColourMasterId = arg.ColourMasterId;
                    model.IoNo = arg.IoNo;
                    model.Quantity = arg.Quantity;
                    model.Rate = arg.Rate;
                    model.ReservedStock = arg.ReservedStock;
                    model.FreeStock = arg.FreeStock;
                    model.StockInAllStores = arg.StockInAllStores;
                    model.StockInCurrentStores = arg.StockInCurrentStores;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;

                    StockConversionFormRepository.Update(model);
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
            return false;
        }

        public bool Delete(int Id)
        {
            bool result = false;
            try
            {
                StockConversionForm model = StockConversionFormRepository.GetById(Id);
                StockConversionFormRepository.Delete(model);
                result = true;
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        #endregion

        #region Helper Method
        public StockConversionManager()
        {
            StockConversionFormRepository = unitOfWork.Repository<StockConversionForm>();
        }

        public StockConversionForm GetStockConversionId(int StockConversionId)
        {
            StockConversionForm stockConversionForm = new StockConversionForm();
            if (StockConversionId != 0)
            {
                stockConversionForm = StockConversionFormRepository.Table.Where(x => x.StockConversionId == StockConversionId).SingleOrDefault();
            }
            return stockConversionForm;
        }

        public StockConversionForm Get(int id)
        {
            return null;
        }

        public List<StockConversionForm> Get()
        {
            List<StockConversionForm> stockConversionForm = new List<StockConversionForm>();
            try
            {
                stockConversionForm = StockConversionFormRepository.Table.ToList<StockConversionForm>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return stockConversionForm;
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
