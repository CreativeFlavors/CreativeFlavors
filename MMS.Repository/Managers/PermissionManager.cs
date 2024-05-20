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
          private Repository<tbl_Permission> tbl_PermissionRepository;

        public PermissionManager()
        {
            tbl_PermissionRepository = unitOfWork.Repository<tbl_Permission>();
        }
        public bool Post(tbl_Permission arg)
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
                    tbl_PermissionRepository.Insert(arg);
                }
                else
                {
                    tbl_Permission model = tbl_PermissionRepository.Table.Where(p => p.PermissionID == arg.PermissionID).FirstOrDefault();
                    if (model != null)
                    {
                        model.PermissionName = arg.PermissionName;
                        model.PermissionDesc = arg.PermissionDesc;
                        model.UpdatedDate = DateTime.Now;
                        model.UpdatedBy = "Admin";
                        tbl_PermissionRepository.Update(model);
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
        public List<tbl_Permission> Get()
        {
            List<tbl_Permission> tbl_Permission = new List<tbl_Permission>();

            try
            {
                tbl_Permission = tbl_PermissionRepository.Table.OrderBy(x=>x.PermissionName).ToList<tbl_Permission>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return tbl_Permission;
        }

        public List<tbl_Permission> GetPermissionList(string PageName, string PermissionListIDs)
        {
            List<tbl_Permission> PermissionSetList = new List<tbl_Permission>();
            if (!string.IsNullOrEmpty(PermissionListIDs))
            {
                int[] obj = PermissionListIDs.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

                PermissionManager Manager = new PermissionManager();
               
                PermissionSetList = tbl_PermissionRepository.Table.Where(x => x.PermissionName == PageName && obj.Contains(x.PermissionID)).ToList();
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
