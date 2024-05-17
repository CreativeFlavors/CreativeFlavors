using MMS.Core.Entities;
using MMS.Data.Context;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Common;

namespace MMS.Repository.Managers
{
    public class StatelistManager : Istatelistservices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<states> statesRep;

        public string State { get; set; }
        public List<states> Get()
        {
            List<states> list = new List<states>();
            try
            {
                list = statesRep.getstatesList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return list;
        }
        public StatelistManager()
        {
            statesRep = unitOfWork.Repository<states>();
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public states Get(int id)
        {
            throw new NotImplementedException();
        }

    }
}
