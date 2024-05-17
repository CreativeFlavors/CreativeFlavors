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
    public class StoreMasterManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StoreMaster> storeMasterRepository;

        #region Add/Update/Delete Operation

        public StoreMaster Post(StoreMaster arg)
        {
         //   bool result = false;
            StoreMaster storeMaster = new StoreMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                storeMasterRepository.Insert(arg);
                storeMaster = arg;
             //   result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //result = false;
            }
            return storeMaster;

        }
        public StoreMaster Put(StoreMaster arg)
        {
            bool result = false;
            StoreMaster storeMaster = new StoreMaster();
            try
            {
                StoreMaster model = storeMasterRepository.Table.Where(p => p.StoreMasterId == arg.StoreMasterId && p.IsDeleted==false).FirstOrDefault();
                if (model != null)
                {
                    model.StoreMasterId = arg.StoreMasterId;
                    model.StoreCode = arg.StoreCode;
                    model.StoreName = arg.StoreName;
                    model.Location = arg.Location;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                   // model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                  
                    model.UpdatedBy = username;
                    storeMasterRepository.Update(model);
                    storeMaster = model;
                }
                else
                {
                    return storeMaster;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return storeMaster;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                StoreMaster model = storeMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                storeMasterRepository.Update(model);
                //storeMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        #endregion

        #region Helper Method

        public StoreMasterManager()
        {
            storeMasterRepository = unitOfWork.Repository<StoreMaster>();
        }

        public StoreMaster GetStoreName(string StoreName)
        {
            StoreMaster storeMaster = new StoreMaster();
            if (StoreName != "" && StoreName != null)
            {
                storeMaster = storeMasterRepository.Table.Where(x => x.StoreName.ToLower() == StoreName.ToLower() && x.IsDeleted==false).FirstOrDefault();
            }
            return storeMaster;
        }

        public StoreMaster GetStoreMasterId(int StoreMasterId)
        {
            StoreMaster storeMaster = new StoreMaster();
            if (StoreMasterId != 0)
            {
                storeMaster = storeMasterRepository.Table.Where(x => x.StoreMasterId == StoreMasterId&&x.IsDeleted==false).FirstOrDefault();
            }
            return storeMaster;
        }
        public StoreMaster Get(int id)
        {
            return null;
        }

        public List<StoreMaster> Get()
        {
            List<StoreMaster> storeMaster = new List<StoreMaster>();
            try
            {
                storeMaster = storeMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<StoreMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return storeMaster;
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        #endregion
    }
}
