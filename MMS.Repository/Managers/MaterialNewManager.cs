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
    public class MaterialNewManager : IMaterialService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Material> materialRepository;
        public MaterialNewManager()
        {
            materialRepository = unitOfWork.Repository<Material>();
        }
        public List<Material> Get()
        {
            List<Material> materials = new List<Material>();
            try
            {
                materials = materialRepository.Table.ToList<Material>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materials;
        }

        public Material GetId(int materialid)
        {
            Material materiallist = new Material();
            if (materialid != 0)
            {
                try
                {
                    materiallist = materialRepository.Table.Where(x => x.MaterialId == materialid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return materiallist;
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
