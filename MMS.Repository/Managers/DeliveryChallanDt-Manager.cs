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

namespace MMS.Repository.Managers
{
    public class DeliveryChallanDt_Manager : IDeliveryChallanDTServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<DeliveryChallan_dt> DeliveryChallanDtrep;
        public DeliveryChallanDt_Manager()
        {
            DeliveryChallanDtrep = unitOfWork.Repository<DeliveryChallan_dt>();

        }
        public DeliveryChallan_dt POST(DeliveryChallan_dt arg)
        {
            DeliveryChallan_dt salesorder = new DeliveryChallan_dt();
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = 0;
                DeliveryChallanDtrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<DeliveryChallan_dt> Get()
        {
            List<DeliveryChallan_dt> obj = new List<DeliveryChallan_dt>();
            obj = DeliveryChallanDtrep.Table.ToList<DeliveryChallan_dt>();
            return obj;
        }
        public List<DeliveryChallan_dt> GetSOId(int id)
        {
             List<DeliveryChallan_dt> salesorders = new List<DeliveryChallan_dt>();
            salesorders = DeliveryChallanDtrep.Table.Where(x => x.SalesOrderId_dt == id).ToList();
            return salesorders;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public salesordercart GettypeId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
