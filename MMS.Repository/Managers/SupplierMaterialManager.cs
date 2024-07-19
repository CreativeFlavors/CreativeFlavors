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
    public class SupplierMaterialManager : ISupplierMaterialService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SupplierMaterial> suppliermaterialRepository;
        public SupplierMaterialManager()
        {
            suppliermaterialRepository = unitOfWork.Repository<SupplierMaterial>();
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool IsMaterialSuppliedBySupplier(int? productId, int supplierId)
        {
            bool isSupplied = false;

            try
            {
                isSupplied = suppliermaterialRepository.Table
                                .Any(x => x.ProductId == productId && x.SupplierId == supplierId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return isSupplied;
        }

    }
}
