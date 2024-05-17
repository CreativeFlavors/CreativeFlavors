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
        private Repository<OrginMaster> orginMasterRepository;

        #region Add/Update/Delete Operation

        public OrginMaster Post(OrginMaster arg)
        {
            bool result = false;
            OrginMaster orginMaster = new OrginMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
               // arg.UpdatedBy = username;

                orginMasterRepository.Insert(arg);
                orginMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return orginMaster;

        }
        public bool Put(OrginMaster arg)
        {
            bool result = false;
            try
            {
                OrginMaster model = orginMasterRepository.Table.Where(p => p.OriginMasterId == arg.OriginMasterId).FirstOrDefault();
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
                    orginMasterRepository.Update(model);
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
                OrginMaster model = orginMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                orginMasterRepository.Update(model);
               // orginMasterRepository.Delete(model);
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
            orginMasterRepository = unitOfWork.Repository<OrginMaster>();
        }

        public OrginMaster GetOriginName(string OriginName)
        {
            OrginMaster orginMaster = new OrginMaster();
            if (OriginName != "" && OriginName != null)
            {
                orginMaster = orginMasterRepository.Table.Where(x => x.OriginName == OriginName).SingleOrDefault();
            }
            return orginMaster;
        }

        public OrginMaster GetOriginMasterId(int OriginMasterId)
        {
            OrginMaster orginMaster = new OrginMaster();
            if (OriginMasterId != 0)
            {
                orginMaster = orginMasterRepository.Table.Where(x => x.OriginMasterId == OriginMasterId).SingleOrDefault();
            }
            return orginMaster;
        }
        public OrginMaster Get(int id)
        {
            return null;
        }

        public List<OrginMaster> Get()
        {
            List<OrginMaster> orginMaster = new List<OrginMaster>();
            try
            {
                orginMaster = orginMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<OrginMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return orginMaster;
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
