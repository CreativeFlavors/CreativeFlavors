using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class PermissionManager
    {
          private UnitOfWork unitOfWork = new UnitOfWork();
          private Repository<PermissionMaster> PermissionMasterRepository;

        public PermissionManager()
        {
            PermissionMasterRepository = unitOfWork.Repository<PermissionMaster>();
        }
        public bool Post(PermissionMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.PermissionID == 0)
                {
                    arg.CreatedBy = "Admin";
                    arg.UpdatedBy = "Admin";
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = DateTime.Now;
                    PermissionMasterRepository.Insert(arg);
                }
                else
                {
                    PermissionMaster model = PermissionMasterRepository.Table.Where(p => p.PermissionID == arg.PermissionID).FirstOrDefault();
                    if (model != null)
                    {
                        model.PermissionName = arg.PermissionName;
                        model.PermissionDesc = arg.PermissionDesc;
                        model.UpdatedDate = DateTime.Now;
                        model.UpdatedBy = "Admin";
                        PermissionMasterRepository.Update(model);
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
        public List<PermissionMaster> Get()
        {
            List<PermissionMaster> PermissionMaster = new List<PermissionMaster>();

            try
            {
                PermissionMaster = PermissionMasterRepository.Table.OrderBy(x=>x.PermissionName).ToList<PermissionMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return PermissionMaster;
        }

        public List<PermissionMaster> GetPermissionList(string PageName, string PermissionListIDs)
        {
            List<PermissionMaster> PermissionSetList = new List<PermissionMaster>();
            if (!string.IsNullOrEmpty(PermissionListIDs))
            {
                int[] obj = PermissionListIDs.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

                PermissionManager Manager = new PermissionManager();
               
                PermissionSetList = PermissionMasterRepository.Table.Where(x => x.PermissionName == PageName && obj.Contains(x.PermissionID)).ToList();
            }
            return PermissionSetList;
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
