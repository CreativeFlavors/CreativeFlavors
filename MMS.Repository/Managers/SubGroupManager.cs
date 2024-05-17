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
    public class SubGroupManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SubGroup> SubGroupMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(SubGroup arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                // arg.UpdatedBy = username;
                SubGroupMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Put(SubGroup arg)
        {
            bool result = false;
            try
            {
                SubGroup model = SubGroupMasterRepository.Table.Where(p => p.SubGroupID == arg.SubGroupID).FirstOrDefault();
                if (model != null)
                {
                    model.SubGroupID = arg.SubGroupID;
                    //model.SubGroupCode = arg.SubGroupCode;
                    model.SubGroupDescription = arg.SubGroupDescription;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;

                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    SubGroupMasterRepository.Update(model);
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
                SubGroup model = SubGroupMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.IsDeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.IsDeletedOn = DateTime.Now;
                SubGroupMasterRepository.Update(model);
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

        public SubGroupManager()
        {
            SubGroupMasterRepository = unitOfWork.Repository<SubGroup>();
        }
        public SubGroup GetsubGroupDescriptionName(string SubGroupDescription)
        {
            SubGroup subGroup = new SubGroup();
            if (SubGroupDescription != "" && SubGroupDescription != null)
            {
                subGroup = SubGroupMasterRepository.Table.Where(x => x.SubGroupDescription == SubGroupDescription).SingleOrDefault();
            }
            return subGroup;
        }
        public SubGroup GetSubGroupMasterId(int SubGroupID)
        {
            SubGroup subGroup = new SubGroup();
            if (SubGroupID != 0)
            {
                subGroup = SubGroupMasterRepository.Table.Where(x => x.SubGroupID == SubGroupID).SingleOrDefault();
            }
            return subGroup;
        }
        public SubGroup Get(int id)
        {
            return null;
        }
        public List<SubGroup> Get()
        {
            List<SubGroup> subGroupList = new List<SubGroup>();
            try
            {
                subGroupList = SubGroupMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SubGroup>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return subGroupList;
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
