using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class ImportIOManager:IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ImportIO> importIORepository;

        public ImportIOManager()
        {
            importIORepository = unitOfWork.Repository<ImportIO>();
        }



        public ImportIO Get(int id)
        {
            return null;
        }

        public List<ImportIO> Get()
        {
            List<ImportIO> importIOList = new List<ImportIO>();
            try
            {
                importIOList = importIORepository.Table.Where(X => X.IsDeleted == false).ToList<ImportIO>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return importIOList;
        }
        public List<ImportIO> GetIO(int SimpleMRPID)
        {
            List<ImportIO> importIOList = new List<ImportIO>();
            try
            {
        
               importIOList= importIORepository.Table.Where(X => X.IsDeleted == false && X.SimpleMRPID == SimpleMRPID).ToList<ImportIO>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return importIOList;
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
