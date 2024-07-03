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

namespace MMS.Repository.Managers
{
    public class StatusProductionSubassemblyManager : IStatusProductionSubaasemblyService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StatusProductionSubassembly> statusproductionsubassemblyRepository;
        public StatusProductionSubassemblyManager()
        {
            statusproductionsubassemblyRepository= unitOfWork.Repository<StatusProductionSubassembly>();
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        public List<StatusProductionSubassembly> Get()
        {
            List<StatusProductionSubassembly> statusProductions = new List<StatusProductionSubassembly>();
            try
            {
                statusProductions = statusproductionsubassemblyRepository.Table.Where(X => X.IsDeleted == false).ToList<StatusProductionSubassembly>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return statusProductions;
        }
    }
}
