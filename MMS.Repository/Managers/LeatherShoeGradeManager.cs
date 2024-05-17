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
    public class LeatherShoeGradeManager : ILeatherShoeGradeService, IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<LeatherShoesGradeMaster> LeatherShoesGradeMasterIdRepository;

        #region Add/Update/Delete Operation

        public bool Post(LeatherShoesGradeMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;

                LeatherShoesGradeMasterIdRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(LeatherShoesGradeMaster arg)
        {
            bool result = false;
            try
            {
                LeatherShoesGradeMaster model = LeatherShoesGradeMasterIdRepository.Table.Where(p => p.LeatherShoesGradeMasterId == arg.LeatherShoesGradeMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.LeatherShoesGradeMasterId = arg.LeatherShoesGradeMasterId;
                    model.GradeCode = arg.GradeCode;
                    model.Grade = arg.Grade;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();          
                    model.UpdatedBy = username;
                    LeatherShoesGradeMasterIdRepository.Update(model);
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
                LeatherShoesGradeMaster model = LeatherShoesGradeMasterIdRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                LeatherShoesGradeMasterIdRepository.Update(model);
                // LeatherShoesGradeMasterIdRepository.Delete(model);
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

        public LeatherShoeGradeManager()
        {
            LeatherShoesGradeMasterIdRepository = unitOfWork.Repository<LeatherShoesGradeMaster>();
        }

        public LeatherShoesGradeMaster GetIssueType(string GradeCode)
        {
            LeatherShoesGradeMaster leatherShoesGradeMaster = new LeatherShoesGradeMaster();
            if (GradeCode != "" && GradeCode != null)
            {
                leatherShoesGradeMaster = LeatherShoesGradeMasterIdRepository.Table.Where(x => x.Grade == GradeCode).SingleOrDefault();
            }
            return leatherShoesGradeMaster;
        }

        public LeatherShoesGradeMaster GetLeatherShoesGradeMasterId(int LeatherShoesGradeMasterId)
        {
            LeatherShoesGradeMaster leatherShoesGradeMaster = new LeatherShoesGradeMaster();
            if (LeatherShoesGradeMasterId != 0)
            {
                leatherShoesGradeMaster = LeatherShoesGradeMasterIdRepository.Table.Where(x => x.LeatherShoesGradeMasterId == LeatherShoesGradeMasterId).SingleOrDefault();
            }
            return leatherShoesGradeMaster;
        }
        public LeatherShoesGradeMaster Get(int id)
        {
            return null;
        }

        public List<LeatherShoesGradeMaster> Get()
        {
            List<LeatherShoesGradeMaster> LeatherShoesGradeMasterlist = new List<LeatherShoesGradeMaster>();
            try
            {
                LeatherShoesGradeMasterlist = LeatherShoesGradeMasterIdRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<LeatherShoesGradeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return LeatherShoesGradeMasterlist;
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
