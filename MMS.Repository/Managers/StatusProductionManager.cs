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
    public class StatusProductionManager : IStatusProduction, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StatusProduction> statusproductionRepository;
        public StatusProductionManager()
        {
            statusproductionRepository = unitOfWork.Repository<StatusProduction>();
        }
        public List<StatusProduction> Get()
        {
            List<StatusProduction> statusProductions = new List<StatusProduction>();
            try
            {
                statusProductions = statusproductionRepository.Table.Where(X => X.IsDeleted == false).ToList<StatusProduction>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return statusProductions;
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
