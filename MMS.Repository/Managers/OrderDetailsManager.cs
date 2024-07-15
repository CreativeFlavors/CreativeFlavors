using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
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
    public class OrderDetailsManager : IOrderDetailsService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<orderdetails> order_detailsRepository;

        public OrderDetailsManager()
        {
            order_detailsRepository = unitOfWork.Repository<orderdetails>();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public orderdetails GettypeId(int id)
        {
            throw new NotImplementedException();
        }
        public orderdetails POST(orderdetails arg)
        {
            orderdetails salesorder = new orderdetails();
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = 0;
                order_detailsRepository.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public orderdetails Get(int id)
        {
            orderdetails oorderheader = new orderdetails();
            if (id != 0)
            {
                oorderheader = order_detailsRepository.Table.Where(x => x.invoicehd_id == id).FirstOrDefault();
            }
            return oorderheader;
        }
        public List<orderdetails> Get()
        {
            List<orderdetails> obj = new List<orderdetails>();
            obj = order_detailsRepository.Table.ToList<orderdetails>();
            return obj;
        }
        public List<orderdetails> GetSOId(int id)
        {
            List<orderdetails> salesorders = new List<orderdetails>();
            salesorders = order_detailsRepository.Table.Where(x => x.SalesOrderId_dt == id).ToList();
            return salesorders;
        }
    }
}
