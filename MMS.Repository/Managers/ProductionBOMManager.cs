using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class ProductionBOMManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ProductionBOM> ProductionBOMrep;
        public ProductionBOMManager()
        {
            ProductionBOMrep = unitOfWork.Repository<ProductionBOM>();

        }
        public ProductionBOM GettypeId(int id)
        {
            ProductionBOM obj = new ProductionBOM();
            try
            {
                if (id != 0)
                {
                    obj = ProductionBOMrep.Table.FirstOrDefault(x => x.Id == id);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public List<ProductionBOM> Get()
        {

            List<ProductionBOM> List = new List<ProductionBOM>();
            try
            {
                List = ProductionBOMrep.Table.ToList<ProductionBOM>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return List;
        }
        public bool Post(ProductionBOM arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                arg.CreatedDate = DateTime.Now;
                ProductionBOMrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public List<ProductionBOM> GetSubAssembly()
        {

            List<ProductionBOM> List = new List<ProductionBOM>();
            try
            {
                List = ProductionBOMrep.GetProductionbom();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return List;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}
