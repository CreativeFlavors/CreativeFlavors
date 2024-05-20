using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class countryListmanager : IcountryList, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<countries> Countryrep;

     
        public List<countries> Get()
        {
            List<countries> CustAddresslist = new List<countries>();
            try
            {
                CustAddresslist = Countryrep.getcountriesList();

            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CustAddresslist;
        }
        public countryListmanager()
        {
            Countryrep = unitOfWork.Repository<countries>();
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}
