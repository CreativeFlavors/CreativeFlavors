using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;  
namespace MMS.Repository.Managers
{
    public class OrgingMasterManager : IOrginMasterService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<OriginMaster> OriginMasterRepository;

        #region Add/Update/Delete Operation

        public OriginMaster Post(OriginMaster arg)
        {
            bool result = false;
            OriginMaster OriginMaster = new OriginMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
               // arg.UpdatedBy = username;

                OriginMasterRepository.Insert(arg);
                OriginMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return OriginMaster;

        }
        public bool Put(OriginMaster arg)
        {
            bool result = false;
            try
            {
                OriginMaster model = OriginMasterRepository.Table.Where(p => p.OriginMasterId == arg.OriginMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.OriginMasterId = arg.OriginMasterId;
                    model.Code = arg.Code;
                    model.OriginName = arg.OriginName;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                   
                    model.UpdatedBy = username;
                    OriginMasterRepository.Update(model);
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
                OriginMaster model = OriginMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                OriginMasterRepository.Update(model);
               // OriginMasterRepository.Delete(model);
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

        public OrgingMasterManager()
        {
            OriginMasterRepository = unitOfWork.Repository<OriginMaster>();
        }

        public OriginMaster GetOriginName(string OriginName)
        {
            OriginMaster OriginMaster = new OriginMaster();
            if (OriginName != "" && OriginName != null)
            {
                OriginMaster = OriginMasterRepository.Table.Where(x => x.OriginName == OriginName).SingleOrDefault();
            }
            return OriginMaster;
        }

        public OriginMaster GetOriginMasterId(int OriginMasterId)
        {
            OriginMaster OriginMaster = new OriginMaster();
            if (OriginMasterId != 0)
            {
                OriginMaster = OriginMasterRepository.Table.Where(x => x.OriginMasterId == OriginMasterId).SingleOrDefault();
            }
            return OriginMaster;
        }
        public OriginMaster Get(int id)
        {
            return null;
        }

        public List<OriginMaster> Get()
        {
            List<OriginMaster> OriginMaster = new List<OriginMaster>();
            try
            {
                OriginMaster = OriginMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<OriginMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return OriginMaster;
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
