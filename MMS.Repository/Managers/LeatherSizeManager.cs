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
    public class LeatherSizeManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<LeatherSizeMaster> LeatherSizeMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(LeatherSizeMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;

                LeatherSizeMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(LeatherSizeMaster arg)
        {
            bool result = false;
            try
            {

                LeatherSizeMaster model = LeatherSizeMasterRepository.Table.Where(p => p.LeatherSizeMasterId == arg.LeatherSizeMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.Length = arg.Length;
                    model.Width = arg.Width;
                    model.Average = arg.Average;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                   // model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                   
                    model.UpdatedBy = username;
                    LeatherSizeMasterRepository.Update(model);
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
                LeatherSizeMaster model = LeatherSizeMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                LeatherSizeMasterRepository.Update(model);
                // LeatherSizeMasterRepository.Delete(model);
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

        public LeatherSizeManager()
        {
            LeatherSizeMasterRepository = unitOfWork.Repository<LeatherSizeMaster>();
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

        public LeatherSizeMaster GetleatherMasterId(int LeatherSizeMasterId)
        {
            LeatherSizeMaster leatherSizeMaster = new LeatherSizeMaster();
            if (LeatherSizeMasterId != 0)
            {
                leatherSizeMaster = LeatherSizeMasterRepository.Table.Where(x => x.LeatherSizeMasterId == LeatherSizeMasterId).SingleOrDefault();
            }
            return leatherSizeMaster;
        }

        public LeatherSizeMaster Get(int id)
        {
            return null;
        }

        public List<LeatherSizeMaster> Get()
        {
            List<LeatherSizeMaster> leatherSizeMaster = new List<LeatherSizeMaster>();

            try
            {
                leatherSizeMaster = LeatherSizeMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<LeatherSizeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return leatherSizeMaster;
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
