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
    public class UserTypeManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<UserTypeMaster> UserTypeMasterRepository;

        public UserTypeManager()
        {
            UserTypeMasterRepository = unitOfWork.Repository<UserTypeMaster>();
        }
        public bool Post(UserTypeMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.UserTypeID == 0)
                {
                    arg.CreatedBy = "Admin";
                    arg.UpdatedBy = "Admin";
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = DateTime.Now;
                    UserTypeMasterRepository.Insert(arg);
                }
                else
                {
                    UserTypeMaster model = UserTypeMasterRepository.Table.Where(p => p.UserTypeID == arg.UserTypeID).FirstOrDefault();
                    if (model != null)
                    {
                        model.UserType = arg.UserType;
                        model.UserTypeDesc = arg.UserTypeDesc;
                        model.UpdatedDate = DateTime.Now;
                        model.UpdatedBy = "Admin";
                        UserTypeMasterRepository.Update(model);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }

        public bool Put(UserTypeMaster arg)
        {
            bool result = false;
            try
            {

                UserTypeMaster model = UserTypeMasterRepository.Table.Where(p => p.UserTypeID == arg.UserTypeID).FirstOrDefault();
                if (model != null)
                {
                    model.UserType = arg.UserType;
                    model.UserTypeDesc = arg.UserTypeDesc;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    UserTypeMasterRepository.Update(model);
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
                UserTypeMaster model = UserTypeMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                UserTypeMasterRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public UserTypeMaster Get(int id)
        {
            return null;
        }

        public List<UserTypeMaster> Get()
        {
            List<UserTypeMaster> UserTypeMaster = new List<UserTypeMaster>();

            try
            {
                UserTypeMaster = UserTypeMasterRepository.Table.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList<UserTypeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return UserTypeMaster;
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
