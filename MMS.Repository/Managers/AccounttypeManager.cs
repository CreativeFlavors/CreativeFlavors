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
    public class AccounttypeManager : Iaccounttype, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<accounttype> accounttyperep;
        public AccounttypeManager()
        {
            accounttyperep = unitOfWork.Repository<accounttype>();
            
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public accounttype GettypeId(int? id)
        {
            accounttype accounttype = new accounttype();
            try
            {
                if (id != 0)
                {
                    accounttype = accounttyperep.Table.FirstOrDefault(x => x.Id == id);
                }
                return accounttype;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        public List<accounttype> Get()
        {

            List<accounttype> paymentList = new List<accounttype>();
            try
            {
                paymentList = accounttyperep.Table.ToList<accounttype>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return paymentList;
        }
    }
}
