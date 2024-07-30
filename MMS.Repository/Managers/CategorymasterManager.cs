using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class CategorymasterManager : ICategorymasterServices
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<CategoryMaster> CatRepository;
        public CategorymasterManager()
        {
            CatRepository = unitOfWork.Repository<CategoryMaster>();
        }

        public List<CategoryMaster> Get()
        {
            List<CategoryMaster> CategoryMaster = new List<CategoryMaster>();
            try
            {
                CategoryMaster = CatRepository.Table.Where(x => x.IsDeleted == true).ToList<CategoryMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return CategoryMaster;
        }
    }
}
