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
    }
}
