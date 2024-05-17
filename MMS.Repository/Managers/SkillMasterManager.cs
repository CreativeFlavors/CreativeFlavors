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
   public class SkillMasterManager
    {
       
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SkillMaster> SkillMasterRepository;

        #region Add/Update/Delete Operation

        public SkillMaster Post(SkillMaster arg)
        {
            bool result = false;
            SkillMaster skillMaster = new SkillMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                SkillMasterRepository.Insert(arg);
                skillMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return skillMaster;

        }
        public bool Put(SkillMaster arg)
        {
            bool result = false;
            try
            {
                SkillMaster model = SkillMasterRepository.Table.Where(p => p.SkillMasterId == arg.SkillMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.SkillMasterId = arg.SkillMasterId;
                    model.SkillCode = arg.SkillCode;
                    model.SkillName = arg.SkillName;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = string.Empty;
                    string username = HttpContext.Current.Session["UserName"].ToString();             
                    model.UpdatedBy = username;
                    SkillMasterRepository.Update(model);
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
                SkillMaster model = SkillMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                SkillMasterRepository.Update(model);
                // SkillMasterRepository.Delete(model);
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

        public SkillMasterManager()
        {
            SkillMasterRepository = unitOfWork.Repository<SkillMaster>();
        }

        public SkillMaster GetSkillName(string SkillName)
        {
            SkillMaster sizeRangeMaster = new SkillMaster();
            if (SkillName != "" && SkillName != null)
            {
                sizeRangeMaster = SkillMasterRepository.Table.Where(x => x.SkillName == SkillName).SingleOrDefault();
            }
            return sizeRangeMaster;
        }

        public SkillMaster GetSkillMasterId(int SkillMasterId)
        {
            SkillMaster sizeScheduleDetails = new SkillMaster();
            if (SkillMasterId != 0)
            {
                sizeScheduleDetails = SkillMasterRepository.Table.Where(x => x.SkillMasterId == SkillMasterId).SingleOrDefault();
            }
            return sizeScheduleDetails;
        }
        public SizeRangeMaster Get(int id)
        {
            return null;
        }

        public List<SkillMaster> Get()
        {
            List<SkillMaster> SkillMasterlist = new List<SkillMaster>();
            try
            {
                SkillMasterlist = SkillMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SkillMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return SkillMasterlist;
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
