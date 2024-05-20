using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class OrderDetailsManager : OrderDetailsService
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Order_Details> order_detailsRepository;

        public OrderDetailsManager()
        {
            order_detailsRepository = unitOfWork.Repository<Order_Details>();
        }
        public bool Post(Order_Details arg)
        {
            bool result = false;
            try
            {
                //string username = HttpContext.Current.Session["UserName"].ToString();
                //if (!string.IsNullOrEmpty(username))
                //{
                //    arg.CreatedBy = 1;
                //}
                //else
                //{ arg.CreatedBy = 1; }
                arg.CreatedBy = 1;
                arg.CreatedDate = DateTime.Now;
                order_detailsRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public List<Order_Details> GetOrderDetailsbyId(int orderid)
        {
            List<Order_Details> OrderList = new List<Order_Details>();
            try
            {
                OrderList = order_detailsRepository.Table.ToList<Order_Details>().Where(x => x.CustomerDocsId == orderid).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return OrderList;
        }
    }
}
