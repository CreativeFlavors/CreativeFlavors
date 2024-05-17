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
    public class SubstanceMasterManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SubstanceMaster> SubstanceMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(SubstanceMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                SubstanceMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }

        public bool Put(SubstanceMaster arg)
        {
            bool result = false;
            try
            {

                SubstanceMaster model = SubstanceMasterRepository.Table.Where(p => p.SubstanceMasterId == arg.SubstanceMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.SubstanceRange = arg.SubstanceRange;
                    string username = HttpContext.Current.Session["UserName"].ToString();                 
                    model.UpdatedBy = username;
                    SubstanceMasterRepository.Update(model);
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
                SubstanceMaster model = SubstanceMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                SubstanceMasterRepository.Update(model);
                //SubstanceMasterRepository.Delete(model);
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

        public SubstanceMasterManager()
        {
            SubstanceMasterRepository = unitOfWork.Repository<SubstanceMaster>();
        }

        //public LeatherSizeMaster GetBuyerFullName(string BuyerFullName)
        //{
        //    LeatherSizeMaster leatherSizeMaster = new LeatherSizeMaster();
        //    if (BuyerFullName != "" && BuyerFullName != null)
        //    {
        //        leatherSizeMaster = buyerMasterRepository.Table.Where(x => x.BuyerFullName == BuyerFullName).SingleOrDefault();
        //    }
        //    return leatherSizeMaster;
        //}

        public SubstanceMaster GetsubstanceMasterId(int SubstanceMasterId)
        {
            SubstanceMaster substanceMaster = new SubstanceMaster();
            if (SubstanceMasterId != 0)
            {
                substanceMaster = SubstanceMasterRepository.Table.Where(x => x.SubstanceMasterId == SubstanceMasterId).SingleOrDefault();
            }
            return substanceMaster;
        }
        public SubstanceMaster GetsubstanceMasterId(int? SubstanceMasterId)
        {
            SubstanceMaster substanceMaster = new SubstanceMaster();
            if (SubstanceMasterId != 0)
            {
                substanceMaster = SubstanceMasterRepository.Table.Where(x => x.SubstanceMasterId == SubstanceMasterId).SingleOrDefault();
            }
            return substanceMaster;
        }

        public SubstanceMaster Get(int id)
        {
            return null;
        }

        public List<SubstanceMaster> Get()
        {
            List<SubstanceMaster> substanceMaster = new List<SubstanceMaster>();

            try
            {
                substanceMaster = SubstanceMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SubstanceMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return substanceMaster;
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
