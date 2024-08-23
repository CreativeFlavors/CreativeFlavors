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
    public class CustomertransactionManager : Icustomertransactioncs, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<customertransaction> customertransactionrep;
        public CustomertransactionManager()
        {
            customertransactionrep = unitOfWork.Repository<customertransaction>();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Post(customertransaction arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                customertransactionrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public List<customertransaction> Get()
        {

            List<customertransaction> customertransaction = new List<customertransaction>();
            try
            {
                customertransaction = customertransactionrep.Table.ToList<customertransaction>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return customertransaction;
        }

        public List<customertransaction> GetiNId(int id)
        {
            List<customertransaction> customertransaction = new List<customertransaction>();
            try
            {
                if (id != 0)
                {
                    customertransaction = customertransactionrep.Table.Where(x => x.InvRefNumber == id).ToList();
                }
                return customertransaction;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //public customertransaction GetiNId(int id)
        //{
        //    customertransaction customertransaction = new customertransaction();
        //    try
        //    {
        //        if (id != 0)
        //        {
        //            customertransaction = customertransactionrep.Table.FirstOrDefault(x => x.InvRefNumber == id);
        //        }
        //        return customertransaction;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}
        //public bool Put(customertransaction arg)
        //{
        //    bool result = false;
        //    try
        //    {

        //        customertransaction model = customertransactionrep.Table.Where(p => p.Id == arg.Id).FirstOrDefault();
        //        if (model != null)
        //        {
        //            model.InvBalanceAmount = arg.InvBalanceAmount;
        //            var data = model.InvPaidAmount;
        //            data += arg.InvPaidAmount;
        //            model.InvPaidAmount = data;
        //            model.Cash = arg.Cash;
        //            model.Debitnoteref = arg.Debitnoteref;
        //            model.Debitnotedate = arg.Debitnotedate;
        //            model.Debitnotevalue = arg.Debitnotevalue;
        //            model.PaymentAmount = arg.PaymentAmount;
        //            model.PaymentRefNo = arg.PaymentRefNo;

        //            model.UpdatedDate = DateTime.Now;
        //            //string username = HttpContext.Current.Session["UserName"].ToString();
        //            model.UpdatedBy = "admin";
        //            //model.CreatedBy = username;
        //            customertransactionrep.Update(model);
        //            result = true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }

        //    return result;
        //}
    }
}
