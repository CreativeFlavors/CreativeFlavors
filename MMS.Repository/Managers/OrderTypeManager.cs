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
  public  class OrderTypeManager:IOrderTypeService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<OrderType> orderTypeRepository;

        #region Add/Update/Delete Operation

         public OrderType Post(OrderType arg)
        {
            bool result = false;
            OrderType orderType = new OrderType();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                orderTypeRepository.Insert(arg);
                result = true;
                orderType = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return orderType;

        }
        public bool Put(OrderType arg)
        {
            bool result = false;
            try
            {
                OrderType model = orderTypeRepository.Table.Where(p => p.OrderTypeID == arg.OrderTypeID).FirstOrDefault();
                if (model != null)
                {
                    model.OrderTypeID = arg.OrderTypeID;
                    model.OrderTypeCode = arg.OrderTypeCode;
                    model.OrderTypeName = arg.OrderTypeName;                
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                
                    model.UpdatedBy = username;

                    orderTypeRepository.Update(model);
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
                OrderType model = orderTypeRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                orderTypeRepository.Update(model);
                // orderTypeRepository.Delete(model);
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

        public OrderTypeManager()
        {
            orderTypeRepository = unitOfWork.Repository<OrderType>();
        }

        public OrderType GetOrderTypeName(string OrderTypeName)
        {
            OrderType orderType = new OrderType();
            if (OrderTypeName != "" && OrderTypeName != null)
            {
                orderType = orderTypeRepository.Table.Where(x => x.OrderTypeName == OrderTypeName).SingleOrDefault();
            }
            return orderType;
        }

        public OrderType GetOrderTypeID(int OrderTypeID)
        {
            OrderType orderType = new OrderType();
            if (OrderTypeID != 0)
            {
                orderType = orderTypeRepository.Table.Where(x => x.OrderTypeID == OrderTypeID).SingleOrDefault();
            }
            return orderType;
        }
        public OrderType Get(int id)
        {
            return null;
        }

        public List<OrderType> Get()
        {
            List<OrderType> orderTypelist = new List<OrderType>();
            try
            {
                orderTypelist = orderTypeRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<OrderType>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return orderTypelist;
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
