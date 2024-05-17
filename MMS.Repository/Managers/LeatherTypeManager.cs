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
  public  class LeatherTypeManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<LeatherType> leatherTypeRepository;

        #region Add/Update/Delete Operation

        public bool Post(LeatherType arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                leatherTypeRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Put(LeatherType arg)
        {
            bool result = false;
            try
            {
                LeatherType model = leatherTypeRepository.Table.Where(p => p.LeatherTypeID == arg.LeatherTypeID).FirstOrDefault();
                if (model != null)
                {
                    model.LeatherTypeCode = arg.LeatherTypeCode;
                    model.LeatherTypeDescription = arg.LeatherTypeDescription;                   
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    leatherTypeRepository.Update(model);
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
                LeatherType model = leatherTypeRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedON = DateTime.Now;
                leatherTypeRepository.Update(model);
                //  CountryMasterRepository.Delete(model);
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

        public LeatherTypeManager()
        {
            leatherTypeRepository = unitOfWork.Repository<LeatherType>();
        }
        public LeatherType GetLeatherTypeDescription(string LeatherTypeDescription)
        {
            LeatherType leatherType = new LeatherType();
            if (LeatherTypeDescription != "" && LeatherTypeDescription != null)
            {
                leatherType = leatherTypeRepository.Table.Where(x => x.LeatherTypeDescription == LeatherTypeDescription).SingleOrDefault();
            }
            return leatherType;
        }
        public LeatherType GetLeatherTypeID(int LeatherTypeID)
        {
            LeatherType leatherType = new LeatherType();
            if (LeatherTypeID != 0)
            {
                leatherType = leatherTypeRepository.Table.Where(x => x.LeatherTypeID == LeatherTypeID).SingleOrDefault();
            }
            return leatherType;
        }
        public LeatherType Get(int id)
        {
            return null;
        }
        public List<LeatherType> Get()
        {
            List<LeatherType> leatherTypeList = new List<LeatherType>();
            try
            {
                leatherTypeList = leatherTypeRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<LeatherType>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return leatherTypeList;
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
