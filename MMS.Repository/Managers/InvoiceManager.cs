using MMS.Common;
using MMS.Core.Entities;
using MMS.Data.Context;
using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System.Runtime.Remoting.Contexts;
using System.Diagnostics;

namespace MMS.Repository.Managers
{
    public class InvoiceManager : InvoiceService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<order_header> order_headerRepository;
        private Repository<productprocessdeatils> productprocessdeatilsRepository;
        public InvoiceManager()
        {
            order_headerRepository = unitOfWork.Repository<order_header>();
            productprocessdeatilsRepository = unitOfWork.Repository<productprocessdeatils>();
        }

        #region Add/Update/Delete Operation
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                order_header model = order_headerRepository.GetById(id);
                model.IsActive = false;
                order_headerRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Post(order_header arg)
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
                order_headerRepository.Insert(arg);
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

        public List<order_header> Get()
        {
            List<order_header> list = new List<order_header>();
            try
            {
                list = order_headerRepository.Table.Where(x => x.IsActive == true).ToList<order_header>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return list;
        }

        public order_header GetInvoiceId(int id)
        {
            order_header oorderheader = new order_header();
            if (id != 0)
            {
                oorderheader = order_headerRepository.Table.Where(x => x.OrderId == id).SingleOrDefault();
            }
            return oorderheader;
        }

        public List<productprocessdeatils> GetProductProcess()
        {
            List<productprocessdeatils> list = new List<productprocessdeatils>();
            try
            {
                list = productprocessdeatilsRepository.Table.Where(x => x.isactive == true).ToList<productprocessdeatils>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return list;
        }

        //public AgentMaster Get(int id)
        //{
        //    return null;
        //}

        //public List<AgentMaster> Get()
        //{
        //    List<AgentMaster> agentMasterlist = new List<AgentMaster>();
        //    try
        //    {
        //        agentMasterlist = agentMasterRepository.Table.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList<AgentMaster>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    return agentMasterlist;
        //}

        //public AgentMaster GetAgent(string agentName)
        //{
        //    AgentMaster agentMaster = new AgentMaster();
        //    if (agentName != "")
        //    {
        //        agentMaster = agentMasterRepository.Table.Where(x => x.AgentFullName == agentName).SingleOrDefault();
        //    }
        //    return agentMaster;
        //}

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public order_header Get(int id)
        {
            order_header oorderheader = new order_header();
            if (id != 0)
            {
                    oorderheader = order_headerRepository.Table.Where(x => x.Id == id).FirstOrDefault();
            }
            return oorderheader;
        }
        public List<order_header> GetInvoiceListId(int id)
        {
            List<order_header> oorderheader = new List<order_header>();
            if (id != 0)
            {
                oorderheader = order_headerRepository.Table.Where(x => x.OrderId == id).ToList<order_header>();
            }
            return oorderheader;
        }
        public decimal Getcurrencyconversion(string p_primarycurrency, string p_secondarycurrency,  string todaydate)
        {
            decimal val = 0;
            val = order_headerRepository.Getcurrencyconversion(p_primarycurrency, p_secondarycurrency, todaydate);
            return val;
        }
        public List<order_header> GetOrderheaderListEntryId(int buerid, int Process, int OrderEntryId, int orderheaderid, DateTime? from_date, DateTime? to_date)
        {
            List<order_header> oorderheader = new List<order_header>();
            oorderheader = order_headerRepository.GetOrderheaderListEntryId(buerid, Process, OrderEntryId, orderheaderid, from_date, to_date);
            return oorderheader;
        }

        #endregion
    }
}
