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
   public class StageMasterManager
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<StageMaster> StageMasterRepository;

        #region Add/Update/Delete Operation

         public StageMaster Post(StageMaster arg)
        {
            bool result = false;
            StageMaster stageMaster = new StageMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
             //   arg.UpdatedBy = username;
                StageMasterRepository.Insert(arg);
                stageMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return stageMaster;

        }
        public bool Put(StageMaster arg)
        {
            bool result = false;
            try
            {
                StageMaster model = StageMasterRepository.Table.Where(p => p.StageMasterId == arg.StageMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.StageMasterId = arg.StageMasterId;
                    model.ProcessCode = arg.ProcessCode;
                    model.OrderType = arg.OrderType;
                    model.StageCode = arg.StageCode;
                    model.StageName = arg.StageName;
                    model.SubStage = arg.SubStage;
                   model.Sequence = arg.Sequence;
                    model.IsInspection = arg.IsInspection;                   
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.IsDeleted = false;
                    string username = HttpContext.Current.Session["UserName"].ToString();                  
                    model.UpdatedBy = username;
                    StageMasterRepository.Update(model);
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
        public bool Putsedquence(StageMaster arg)
            {
            bool result = false;
            try
            {
                StageMaster model = StageMasterRepository.Table.Where(p => p.StageMasterId == arg.StageMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.StageMasterId = arg.StageMasterId;
                   model.Sequence = arg.Sequence;
                    model.UpdatedDate = DateTime.Now;
                   // model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();
        model.UpdatedBy = username;
                    StageMasterRepository.Update(model);
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
                StageMaster model = StageMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                StageMasterRepository.Update(model);
                //StageMasterRepository.Delete(model);
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

        public StageMasterManager()
        {
            StageMasterRepository = unitOfWork.Repository<StageMaster>();
        }

        public StageMaster GetStageName(string StageName)
        {

            StageMaster buyerMaster = new StageMaster();
            try
            {
                if (StageName != "" && StageName != null)
                {
                    buyerMaster = StageMasterRepository.Table.Where(x => x.StageName == StageName).FirstOrDefault();
                }
                
               
            }
            catch (Exception ex)
            {
             //   return 0;

                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return buyerMaster;
        }

        public StageMaster GetStageMasterId(int StageMasterId)
        {
            StageMaster stageMaster = new StageMaster();
            if (StageMasterId != 0)
            {
                stageMaster = StageMasterRepository.Table.Where(x => x.StageMasterId == StageMasterId).SingleOrDefault();
            }
            return stageMaster;
        }
        public List<StageMaster> GetStageProcessId(int ProcessCode)
        {
            List<StageMaster> stageMaster = new List<StageMaster>();
            if (ProcessCode != 0)
            {
                stageMaster = StageMasterRepository.Table.Where(x => x.ProcessCode == ProcessCode).ToList();
            }
            return stageMaster;
        }
        public StageMaster Get(int id)
        {
            return null;
        }
        public List<StageMaster> Get()
        {
            List<StageMaster> stageMaster = new List<StageMaster>();

            try
            {
                stageMaster = StageMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<StageMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return stageMaster;
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
