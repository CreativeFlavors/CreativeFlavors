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
  public  class StockTransferManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StockTransfer> StockTransferRepository;

        #region Add/Update/Delete Operation

        public bool Post(StockTransfer arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
               // arg.UpdatedBy = username;
                arg.CreatedDate = DateTime.Now;
                StockTransferRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(StockTransfer arg)
        {
            bool result = false;
            try
            {
                StockTransfer model = StockTransferRepository.Table.Where(p => p.StockTransferID == arg.StockTransferID).FirstOrDefault();
                if (model != null)
                {
                    model.StockTransferID = arg.StockTransferID;
                    model.TypeId = arg.TypeId;
                    model.MaterialCategoryID = arg.MaterialCategoryID;
                    model.MaterialGroupID = arg.MaterialGroupID;
                    model.ColorID = arg.ColorID;
                    model.QuantityAmount = arg.QuantityAmount;
                    model.QuantityValue = arg.QuantityValue;
                    model.IssuedToFrom = arg.IssuedToFrom;
                    model.Stores = arg.Stores;
                    model.DcGrnNo = arg.DcGrnNo;
                    model.TransportDetails = arg.TransportDetails;
                    model.ReservedStock = arg.ReservedStock;
                    model.RefDcNo = arg.RefDcNo;
                    model.ClosingStockInAllStores = arg.ClosingStockInAllStores;
                    model.ClosingStockInCurrentStores = arg.ClosingStockInCurrentStores; 
                    model.FreeStock = arg.FreeStock;
                    model.Remarks = arg.Remarks;
                    model.Rate = arg.Rate;
                    model.Value = arg.Value;
                    model.PartyDcNo = arg.PartyDcNo;
                    model.InvoiceRef = arg.InvoiceRef;
                    model.InvoiceDate = arg.InvoiceDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.CreatedBy = username;
                    model.UpdatedBy = username;
                    StockTransferRepository.Update(model);
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
                StockTransfer model = StockTransferRepository.GetById(id);
                StockTransferRepository.Delete(model);
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

        public StockTransferManager()
        {
            StockTransferRepository = unitOfWork.Repository<StockTransfer>();
        }

        public StockTransfer GetStockTransferByID(int StockTransferID)
        {
            StockTransfer spprocedPriceList = new StockTransfer();
            if (StockTransferID != 0)
            {
                spprocedPriceList = StockTransferRepository.Table.Where(x => x.StockTransferID == StockTransferID).SingleOrDefault();
            }
            return spprocedPriceList;
        }

        public StockTransfer Get(int id)
        {
            return null;
        }

        public List<StockTransfer> Get()
        {
            List<StockTransfer> StockTransfer = new List<StockTransfer>();

            try
            {
                StockTransfer = StockTransferRepository.Table.ToList<StockTransfer>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return StockTransfer;
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
