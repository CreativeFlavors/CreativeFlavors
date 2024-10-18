using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public CategoryMaster GetCategoryMasterid(int CatRepository_id)
        {
            CategoryMaster oCustAddress = new CategoryMaster();
            if (CatRepository_id != 0)
            {
                oCustAddress = CatRepository.Table.Where(x => x.CategoryId == CatRepository_id).FirstOrDefault();
            }
            return oCustAddress;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                CategoryMaster model = CatRepository.GetById(id);
                model.IsDeleted = false;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                CatRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Put(CategoryMaster arg)
        {
            bool result = false;
            try
            {
                CategoryMaster model = CatRepository.Table.Where(p => p.CategoryId == arg.CategoryId).FirstOrDefault();
                if (model != null)
                {
                    model.CategoryName = arg.CategoryName;
                    model.CategoryCode = arg.CategoryCode;
                    model.CategoryType = arg.CategoryType;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();

                    model.UpdatedBy = username;
                    CatRepository.Update(model);
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Post(CategoryMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                CatRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
    }
}
