using MMS.Core.Entities;
using MMS.Data.Context;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Data.Mapping;
using MMS.Common;

namespace MMS.Repository.Managers
{
    public class StatusHistorySubassemblyManager : IStatusHistorySubassemblyService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StatusHistorySubassembly> statushistorysubassemblyrep;
        public StatusHistorySubassemblyManager()
        {
            statushistorysubassemblyrep = unitOfWork.Repository<StatusHistorySubassembly>();
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        public bool Post(StatusHistorySubassembly arg)
        {
            bool result = false;
            try
            {
                //string username = "admin";
                //arg.InitiatedBy = username;

                arg.CreatedDate = DateTime.Now;

                statushistorysubassemblyrep.Insert(arg);

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
