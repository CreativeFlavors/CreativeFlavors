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
    public class CitylistManager : ICitylistservices , IDisposable
    {
    
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<cities> citiesRep;

        public cities Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<cities> Get()
        {
            List<cities> list = new List<cities>();
            try
            {
                list = citiesRep.getcitiesList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return list;
        }
        public CitylistManager()
        {
            citiesRep = unitOfWork.Repository<cities>();
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
