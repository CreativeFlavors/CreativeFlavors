using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<BatchStock> Get()
        {
            List<BatchStock> obj = new List<BatchStock>();
            obj = BatchStockrep.Table.ToList<BatchStock>();
            return obj;
        }
    }
}
