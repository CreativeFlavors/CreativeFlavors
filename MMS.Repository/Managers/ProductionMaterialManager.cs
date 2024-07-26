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
    public class ProductionMaterialManager : IProductionMaterialService,IDisposable
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ProductionMaterial> productionmaterialrepository;
        public ProductionMaterialManager()
        {
            productionmaterialrepository= unitOfWork.Repository<ProductionMaterial>();


        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool Post(ProductionMaterial arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                productionmaterialrepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public List<ProductionMaterial> GetProductionMaterial(int productionid)
        {
            List<ProductionMaterial> productionMaterial = new List<ProductionMaterial>();
            if (productionid != 0)
            {
                productionMaterial = productionmaterialrepository.Table.Where(x => x.ProductionId == productionid).ToList();
            }
            return productionMaterial;
        }
    }
}
