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
    public class OrderHeaderManager : IOrderHeaderService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<orderheader_hd> OrderHeaderrep;
        public OrderHeaderManager()
        {
            OrderHeaderrep = unitOfWork.Repository<orderheader_hd>();

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public List<orderheader_hd> GettypeId(int id)
        {
            List<orderheader_hd> orderheader_hd = new List<orderheader_hd>();
            try
            {
                if (id != 0)
                {
                    orderheader_hd = OrderHeaderrep.Table.Where(x => x.CustomerId == id).ToList();
                }
                return orderheader_hd;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public orderheader_hd Get(int id)
        {
            orderheader_hd oorderheader = new orderheader_hd();
            if (id != 0)
            {
                oorderheader = OrderHeaderrep.Table.Where(x => x.invoicehd_id == id).FirstOrDefault();
            }
            return oorderheader;
        }
        public orderheader_hd POST(orderheader_hd arg)
        {
            orderheader_hd salesorder = new orderheader_hd();
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = 0;
                arg.IsDeleted = true;
                OrderHeaderrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<orderheader_hd> Get()
        {
            List<orderheader_hd> obj = new List<orderheader_hd>();
            obj = OrderHeaderrep.Table.ToList<orderheader_hd>();
            return obj;
        }
        public orderheader_hd GetSOId(int id)
        {
            orderheader_hd salesorders = new orderheader_hd();
            salesorders = OrderHeaderrep.Table.Where(x => x.invoicehd_id == id).FirstOrDefault();
            return salesorders;
        }


    }
}
