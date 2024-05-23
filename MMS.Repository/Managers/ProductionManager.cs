using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.JobWork;
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
    public class ProductionManager : IProductionServices,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Production> productionrep;
        public ProductionManager()
        {
            productionrep = unitOfWork.Repository<Production>();

        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool Post(Production arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                productionrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }


        public Production Getproductionid(int? productionid)
        {
            Production productionlist = new Production();
            if (productionid != 0)
            {
                try
                {
                    productionlist = productionrep.Table.Where(x => x.ProductionId == productionid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return productionlist;
        }

        public List<Production> Get_byCodes(int ProductionId)
        {
            List<Production> productions = new List<Production>();
            try
            {
                productions = productionrep.Table.Where(X => X.ProductionId== ProductionId).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return productions;
        }

        public List<Production> GetProductions()
        {
            List<Production> productions = new List<Production>();

            try
            {
                productions = productionrep.Table.ToList<Production>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return productions;
        }
    }
}
