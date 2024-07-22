using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class BatchStockManager : BatchStockServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BatchStock> BatchStockrep;
        public BatchStockManager()
        {
            BatchStockrep = unitOfWork.Repository<BatchStock>();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public BatchStock POST(BatchStock arg)
        {
            BatchStock salesorder = new BatchStock();
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.status = 1;
                BatchStockrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public bool Put(BatchStock arg)
        {
            bool result = false;
            try
            {
                BatchStock model = BatchStockrep.Table.Where(f => f.BatchStockId == arg.BatchStockId).FirstOrDefault();
                if (model != null)
                {
                    model.BatchStockId = arg.BatchStockId;
                    model.SupplierId = arg.SupplierId;
                    model.StoreCode = arg.StoreCode;
                    model.productid = arg.productid;
                    model.BatchCode = arg.BatchCode;
                    model.AltBatchCode = arg.AltBatchCode;
                    model.ExpiryDate = arg.ExpiryDate;
                    model.Quantity = arg.Quantity;
                    model.GrnNumber = arg.GrnNumber;
                    model.GrnDate = arg.GrnDate;
                    model.GrnDetailId = arg.GrnDetailId;
                    model.Price = arg.Price;
                    model.Cost = arg.Cost;
                    model.TaxCode = arg.TaxCode;
                    model.UomId = arg.UomId;
                    model.ReservedQty = arg.ReservedQty;
                    model.LastTransMode = arg.LastTransMode;
                    model.LastTransNumber = arg.LastTransNumber;
                    model.LastTransDate = arg.LastTransDate;
                    model.LastTransQty = arg.LastTransQty;
                    model.status = arg.status;
                    model.producttype = arg.producttype;
                    model.ProductCode = arg.ProductCode;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    //string username = HttpContext.Current.Session["UserName"]?.ToString();
                    //if (username != null)
                    //{
                    //    model.UpdatedBy = username;
                    //}
                    model.UpdatedBy = "Admin";
                    BatchStockrep.Update(model);
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
        public List<BatchStock> Get()
        {
            List<BatchStock> obj = new List<BatchStock>();
            obj = BatchStockrep.Table.ToList<BatchStock>();
            return obj;
        }
        public BatchStock GetByProductCode(string productcode)
        {
            BatchStock finishedgoodlist = new BatchStock();
            if (productcode != null)
            {
                try
                {
                    finishedgoodlist = BatchStockrep.Table.Where(x => x.ProductCode == productcode).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return finishedgoodlist;
        }
    }
}
