using MMS.Common;
using MMS.Core.Entities.GateEntry;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.GateEntryManager
{
   public class VehicleManager: IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<VehicleMaster> vehicleRepository;
        public VehicleManager()
        {
            vehicleRepository = unitOfWork.Repository<VehicleMaster>();
        }

        #region Add/Update/Delete Operation
        public bool Post(VehicleMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.VehicleId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    vehicleRepository.Insert(arg);
                    result = true;
                    return result;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.UpdatedBy = username;
                    arg.UpdatedDate = DateTime.Now;

                    vehicleRepository.Update(arg);
                    result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Delete(int VehicleId)
        {
            bool result = false;
            try
            {
                VehicleMaster model = vehicleRepository.GetById(VehicleId);
                model.IsDeleted = true;
                vehicleRepository.Update(model);
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
        public VehicleMaster GetVehicleById(int VehicleId)
        {
            VehicleMaster gateEntryDocType = new VehicleMaster();
            if (VehicleId != 0)
            {
                gateEntryDocType = vehicleRepository.Table.Where(x => x.VehicleId == VehicleId).SingleOrDefault();
            }
            return gateEntryDocType;
        }

        public List<VehicleMaster> Get()
        {
            List<VehicleMaster> gateEntryDocTypelist = new List<VehicleMaster>();
            try
            {
                gateEntryDocTypelist = vehicleRepository.Table.Where(x => x.IsDeleted == false).ToList<VehicleMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateEntryDocTypelist;
        }
        
        #endregion
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}
