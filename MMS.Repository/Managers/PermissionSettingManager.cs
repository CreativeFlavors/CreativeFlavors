using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class PermissionSettingManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<PermissionSettingMaster> PermissionSettingRepository;

        public bool Post(PermissionSettingMaster arg)
        {
           
            bool result = false;
            try
            {
                PermissionSettingMaster Model = PermissionSettingRepository.Table.Where(x => x.UserTypeID == arg.UserTypeID).FirstOrDefault();
                if (Model == null)
                {
                    arg.CreatedBy = "Admin";
                    arg.UpdatedBy = "Admin";
                    PermissionSettingRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    Model.PermissionID = arg.PermissionID;
                    Model.UpdatedBy = "Admin";
                    Model.UpdatedDate = DateTime.Now;
                    PermissionSettingRepository.Update(Model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public PermissionSettingManager()
        {
            PermissionSettingRepository = unitOfWork.Repository<PermissionSettingMaster>();
        }

        public PermissionSettingMaster GetByID(int UserTypeId)
        {
            PermissionSettingMaster PermissionSetList = new PermissionSettingMaster();
            try
            {
                PermissionSetList = PermissionSettingRepository.Table.Where(x => x.UserTypeID == UserTypeId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PermissionSetList;
        }

       

    }
}
