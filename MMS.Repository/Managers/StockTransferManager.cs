using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;   
namespace MMS.Repository.Managers
{
    public class StockTransferManager : IStockTransferService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StockTransferMaster> stockMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(StockTransferMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                stockMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(StockTransferMaster arg)
        {
            bool result = false;
            try
            {
                StockTransferMaster model = stockMasterRepository.Table.Where(p => p.StockTransferID == arg.StockTransferID).FirstOrDefault();
                if (model != null)
                {
                    model.StockTransferID = arg.StockTransferID;
                    model.GrnTypeMasterId = arg.GrnTypeMasterId;
                    model.GRNNo = arg.GRNNo;
                    model.IssuedTo = arg.IssuedTo;
                    model.StoreMasterId = arg.StoreMasterId;
                    model.TransportDetails = arg.TransportDetails;
                    model.Remarks = arg.Remarks;
                    model.Rate = arg.Rate;
                    model.Value = arg.Value;
                    model.MatCategoryId = arg.MatCategoryId;
                    model.MatGroupId = arg.MatGroupId;
                    model.ColourId = arg.ColourId;
                    model.Quantity = arg.Quantity;
                    model.ClosingStockInAllStores = arg.ClosingStockInAllStores;
                    model.ClosingStockingInCurrentStores = arg.ClosingStockingInCurrentStores;
                    model.ReservedStock = arg.ReservedStock;
                    model.FreeStock = arg.FreeStock;
                    model.InvoiceRef    = arg.InvoiceRef;
                    model.InvoiceDate = arg.InvoiceDate;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();                
                    model.UpdatedBy = username;
                    stockMasterRepository.Update(model);
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
                StockTransferMaster model = stockMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                stockMasterRepository.Update(model);
                //stockMasterRepository.Delete(model);
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
            stockMasterRepository = unitOfWork.Repository<StockTransferMaster>();
        }

        public StockTransferMaster GetGRNNo(string GRNNo)
        {
            StockTransferMaster StockTransferMaster = new StockTransferMaster();
            if (GRNNo != "" && GRNNo != null)
            {
                StockTransferMaster = stockMasterRepository.Table.Where(x => x.GRNNo == GRNNo).SingleOrDefault();
            }
            return StockTransferMaster;
        }

        public StockTransferMaster GetStockTransferMasterId(int StockTransferID)
        {
            StockTransferMaster StockTransferMaster = new StockTransferMaster();
            if (StockTransferID != 0)
            {
                StockTransferMaster = stockMasterRepository.Table.Where(x => x.StockTransferID == StockTransferID).SingleOrDefault();
            }
            return StockTransferMaster;
        }

        public StockTransferMaster Get(int id)
        {
            return null;
        }

        public List<StockTransferMaster> Get()
        {
            List<StockTransferMaster> StockTransferMasterlist = new List<StockTransferMaster>();
            try
            {
                StockTransferMasterlist = stockMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<StockTransferMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return StockTransferMasterlist;
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
