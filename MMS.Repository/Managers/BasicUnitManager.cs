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
    public class BasicUnitManager
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BasicUnitMaster> BasicUnitMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(BasicUnitMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.UpdatedBy = username;
                arg.UpdatedDate = DateTime.Now;
               // arg.DeletedDate = DateTime.Now;
                BasicUnitMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(BasicUnitMaster arg)
        {
            bool result = false;
            try
            {
                BasicUnitMaster model = BasicUnitMasterRepository.Table.Where(p => p.BasicUnitMasterID == arg.BasicUnitMasterID).FirstOrDefault();
                if (model != null)
                {
                    model.CategoryId = arg.CategoryId;
                    model.GroupID = arg.GroupID;
                    model.MaterialID = arg.MaterialID;
                    model.ShortUnitValue = arg.ShortUnitValue;
                    model.ShortUnitID = arg.ShortUnitID;
                    model.LongUnitValue = arg.LongUnitValue;
                    model.LongUnitID = arg.LongUnitID;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    BasicUnitMasterRepository.Update(model);
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
                BasicUnitMaster model = BasicUnitMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                BasicUnitMasterRepository.Update(model);
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

        public BasicUnitManager()
        {
            BasicUnitMasterRepository = unitOfWork.Repository<BasicUnitMaster>();
        }



        public BasicUnitMaster Get(int id)
        {
            BasicUnitMaster BasicUnitMaster = new BasicUnitMaster();

            try
            {
                BasicUnitMaster = BasicUnitMasterRepository.Table.Where(x => x.BasicUnitMasterID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BasicUnitMaster;
        }

        public List<BasicUnitMaster> Get()
        {
            List<BasicUnitMaster> BasicUnitMaster = new List<BasicUnitMaster>();

            try
            {
                BasicUnitMaster = BasicUnitMasterRepository.Table.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList<BasicUnitMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BasicUnitMaster;
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
