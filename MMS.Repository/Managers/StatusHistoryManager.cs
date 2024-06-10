using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class StatusHistoryManager : IStatusHistoryService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StatusHistory> statushistoryrep;
        public StatusHistoryManager() 
        {
            statushistoryrep = unitOfWork.Repository<StatusHistory>();
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool Post(StatusHistory arg)
        {
            bool result = false;
            try
            {
                //string username = "admin";
                //arg.InitiatedBy = username;

                arg.CreatedDate = DateTime.Now;
                
                statushistoryrep.Insert(arg);
                
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
    }
}
