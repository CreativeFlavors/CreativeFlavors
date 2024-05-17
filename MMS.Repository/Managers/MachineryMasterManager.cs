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
   public class MachineryMasterManager:IMachineryMasterService,IDisposable
    {
       
         private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<MachineryMaster> MachineryMasterRepository;

        #region Add/Update/Delete Operation

         public MachineryMaster Post(MachineryMaster arg)
        {
            bool result = false;
            MachineryMaster machineryMaster = new MachineryMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                MachineryMasterRepository.Insert(arg);
                machineryMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return machineryMaster;

        }
        public bool Put(MachineryMaster arg)
        {
            bool result = false;
            try
            {
                MachineryMaster model = MachineryMasterRepository.Table.Where(p => p.MachineryMasterId == arg.MachineryMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.MachineryMasterId = arg.MachineryMasterId;
                    model.MachineCode = arg.MachineCode;
                    model.MachineName = arg.MachineName;
                    model.Make = arg.Make;
                    model.Model = arg.Model;
                    model.Image = arg.Image;
                    model.AssesListNo = arg.AssesListNo;
                    model.MachineSerialNo = arg.MachineSerialNo;
                    model.Kilowatt = arg.Kilowatt;
                    model.HorsePower = arg.HorsePower;
                    model.Volt = arg.Volt;
                    model.OperationUsedFor = arg.OperationUsedFor;
                    model.Specification = arg.Specification;                   
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                    
                    model.UpdatedBy = username;

                    MachineryMasterRepository.Update(model);
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
                MachineryMaster model = MachineryMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                MachineryMasterRepository.Update(model);
                //MachineryMasterRepository.Delete(model);
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

        public MachineryMasterManager()
        {
            MachineryMasterRepository = unitOfWork.Repository<MachineryMaster>();
        }

        public MachineryMaster GetMachineName(string MachineName)
        {
            MachineryMaster machineryMaster = new MachineryMaster();
            if (MachineName != "" && MachineName != null)
            {
                machineryMaster = MachineryMasterRepository.Table.Where(x => x.MachineName == MachineName).SingleOrDefault();
            }
            return machineryMaster;
        }

        public MachineryMaster GetMachineryMasterId(int MachineryMasterId)
        {
              MachineryMaster machineryMaster = new MachineryMaster();
            if (MachineryMasterId != 0)
            {
                machineryMaster = MachineryMasterRepository.Table.Where(x => x.MachineryMasterId == MachineryMasterId).SingleOrDefault();
            }
            return machineryMaster;
        }
        public MachineryMaster Get(int id)
        {
            return null;
        }

        public List<MachineryMaster> Get()
        {
            List<MachineryMaster> MachineryMasterList = new List<MachineryMaster>();
            try
            {
                MachineryMasterList = MachineryMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<MachineryMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MachineryMasterList;
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
