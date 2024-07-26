using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
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
        public List<FineshedgoodsReport> Fineshedgoods_Report()
        {
            List<FineshedgoodsReport> FineshedgoodsReport = new List<FineshedgoodsReport>();
            FineshedgoodsReport = BatchStockrep.Fineshedgoods();
            return FineshedgoodsReport;
        }

        public List<BatchStock> Get()
        {
            List<BatchStock> obj = new List<BatchStock>();
            obj = BatchStockrep.Table.ToList<BatchStock>();
            return obj;
        }
        public BatchStock GetmaterialOpeningMaterialID(int? productid)
        {
            BatchStock materialOpeningMaster = new BatchStock();
            if (productid != 0)
            {
                materialOpeningMaster = BatchStockrep.Table.Where(x => x.productid == productid ).FirstOrDefault();
            }
            return materialOpeningMaster;
        }

        public BatchStock GetBatchProductMaterialStock(int productid)
        {
            BatchStock materialOpeningMaster = new BatchStock();
            if (productid != 0)
            {
                materialOpeningMaster = BatchStockrep.Table.Where(x => x.productid == productid).FirstOrDefault();
            }
            return materialOpeningMaster;
        }

        public List<BatchStock> GetBatchProductMaterialStocks(int productid)
        {
            List<BatchStock> materialOpeningMaster = new List<BatchStock>();
            if (productid != 0)
            {
                materialOpeningMaster = BatchStockrep.Table.Where(x => x.productid == productid).ToList();
            }
            return materialOpeningMaster;
        }
    }
}
