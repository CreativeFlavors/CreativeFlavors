using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class paymentmethodmanager : IPaymentmethod, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private  Repository<paymentmethod> paymentmethodrep;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<paymentmethod> Get()
        {
            List<paymentmethod> paymentList = new List<paymentmethod>();
            try
            {
                paymentList = paymentmethodrep.Table.ToList<paymentmethod>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return paymentList;
        }

        public paymentmethodmanager()
        {

            paymentmethodrep = unitOfWork.Repository<paymentmethod>();

        }

    }
}
