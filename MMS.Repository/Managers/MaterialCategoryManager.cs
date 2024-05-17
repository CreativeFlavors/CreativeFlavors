using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;  
namespace MMS.Repository.Managers
{
    public class MaterialCategoryManager
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<MaterialCategoryMaster> materialCategoryRepository;

       public MaterialCategoryManager()
        {
            materialCategoryRepository = unitOfWork.Repository<MaterialCategoryMaster>();
        }
       public MaterialCategoryMaster Post(MaterialCategoryMaster arg)
        {
            bool result = false;
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                materialCategoryRepository.Insert(arg);
                materialCategoryMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return materialCategoryMaster;

        }
       public MaterialCategoryMaster GetCategoryName(string CategoryName)
        {
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            if (CategoryName != "" && CategoryName != null)
            {
                materialCategoryMaster = materialCategoryRepository.Table.Where(x => x.CategoryName == CategoryName).SingleOrDefault();
            }
            return materialCategoryMaster;
        }

       public MaterialCategoryMaster GetMaterialCategoryMaster(int? MaterialCategoryMasterId)
        {
            MaterialCategoryMaster materialCategoryMaster = new MaterialCategoryMaster();
            if (MaterialCategoryMasterId != 0)
            {
                materialCategoryMaster = materialCategoryRepository.Table.Where(x => x.MaterialCategoryMasterId == MaterialCategoryMasterId).SingleOrDefault();
            }
            return materialCategoryMaster;
        }
       public bool Put(MaterialCategoryMaster arg)
        {
            bool result = false;
            try
            {

                MaterialCategoryMaster model = materialCategoryRepository.Table.Where(p => p.MaterialCategoryMasterId == arg.MaterialCategoryMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.MaterialCategoryMasterId = arg.MaterialCategoryMasterId;
                    model.CategoryCode = arg.CategoryCode;
                    model.CategoryName = arg.CategoryName;                  
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString(); 
                    model.UpdatedBy = username;
                    materialCategoryRepository.Update(model);
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




        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                MaterialCategoryMaster model = materialCategoryRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                materialCategoryRepository.Update(model);
                //materialCategoryRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public MaterialCategoryMaster Get(int id)
        {
            return null;
        }

        public List<MaterialCategoryMaster> Get()
        {
            List<MaterialCategoryMaster> materialCategoryMaster = new List<MaterialCategoryMaster>();

            try
            {
                materialCategoryMaster = materialCategoryRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<MaterialCategoryMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialCategoryMaster;
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
