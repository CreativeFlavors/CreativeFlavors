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
    public class StoreTransferManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StoreTransfer> storeTransferRepository;

        #region Add/Update/Delete Operations
        public bool Post(StoreTransfer arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                storeTransferRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(StoreTransfer arg)
        {
            bool result = false;
            try
            {
                StoreTransfer model = storeTransferRepository.Table.Where(p => p.StoreTransferId == arg.StoreTransferId).FirstOrDefault();
                if (model != null)
                {
                    model.TrnNo = arg.TrnNo;
                    model.Date = arg.Date;
                    model.FromStores = arg.FromStores;
                    model.ToStores = arg.ToStores;
                    model.MaterialCategoryId = arg.MaterialCategoryId;
                    model.MaterialGroupId = arg.MaterialGroupId;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.ColourMasterId = arg.ColourMasterId;
                    model.Description = arg.Description;
                    model.Rate = arg.Rate;
                    model.ClosingInAllStores = arg.ClosingInAllStores;
                    model.ClosingInCurrentStores = arg.ClosingInCurrentStores;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedBy = arg.UpdatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;

                    storeTransferRepository.Update(model);
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
            return false;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                StoreTransfer model = storeTransferRepository.GetById(id);
                storeTransferRepository.Delete(model);
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

        public StoreTransferManager()
        {
            storeTransferRepository = unitOfWork.Repository<StoreTransfer>();
        }

        public StoreTransfer GetstoreTransferId(int StoreTransferId)
        {
            StoreTransfer storeTransfer = new StoreTransfer();
            if (StoreTransferId != 0)
            {
                storeTransfer = storeTransferRepository.Table.Where(x => x.StoreTransferId == StoreTransferId).SingleOrDefault();
            }
            return storeTransfer;
        }

        public StoreTransfer Get(int id)
        {
            return null;
        }

        public List<StoreTransfer> Get()
        {
            List<StoreTransfer> storeTransfer = new List<StoreTransfer>();
            try
            {
                storeTransfer = storeTransferRepository.Table.ToList<StoreTransfer>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return storeTransfer;
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
