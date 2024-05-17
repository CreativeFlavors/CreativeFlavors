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
   public class ConveyorManager:IconveyorService,IDisposable
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ConveyorMaster> conveyorMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(ConveyorMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
               // arg.UpdatedBy = username;
                conveyorMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(ConveyorMaster arg)
        {
            bool result = false;
            try
            {
                ConveyorMaster model = conveyorMasterRepository.Table.Where(p => p.ConveyorMasterId == arg.ConveyorMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.ConveyorMasterId = arg.ConveyorMasterId;
                    model.ConveyorName = arg.ConveyorName;
                    model.ConveyorType = arg.ConveyorType;
                    model.ConveyorCode = arg.ConveyorCode;
                    model.CapacityPerDay = arg.CapacityPerDay;
                    model.CapacityUnits = arg.CapacityUnits;                  
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                   // model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                    
                    model.UpdatedBy = username;
                    conveyorMasterRepository.Update(model);
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
                ConveyorMaster model = conveyorMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                conveyorMasterRepository.Update(model);
                //conveyorMasterRepository.Delete(model);
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

        public ConveyorManager()
        {
            conveyorMasterRepository = unitOfWork.Repository<ConveyorMaster>();
        }

        public ConveyorMaster GetConveyorName(string ConveyorName)
        {
            ConveyorMaster conveyorMaster = new ConveyorMaster();
            if (ConveyorName != "" && ConveyorName != null)
            {
                conveyorMaster = conveyorMasterRepository.Table.Where(x => x.ConveyorName == ConveyorName).SingleOrDefault();
            }
            return conveyorMaster;
        }

        public ConveyorMaster GetConveyorMasterId(int ConveyorMasterId)
        {
            ConveyorMaster conveyorMaster = new ConveyorMaster();
            if (ConveyorMasterId != 0)
            {
                conveyorMaster = conveyorMasterRepository.Table.Where(x => x.ConveyorMasterId == ConveyorMasterId).SingleOrDefault();
            }
            return conveyorMaster;
        }

        public ConveyorMaster Get(int id)
        {
            return null;
        }

        public List<ConveyorMaster> Get()
        {
            List<ConveyorMaster> conveyorMasterlist = new List<ConveyorMaster>();

            try
            {
                conveyorMasterlist = conveyorMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<ConveyorMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return conveyorMasterlist;
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
