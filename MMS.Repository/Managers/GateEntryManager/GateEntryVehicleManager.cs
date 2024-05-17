using MMS.Common;
using MMS.Core.Entities.GateEntry;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Repository.Managers.GateEntryManager
{

    public class GateEntryVehicleManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryVehicleMaster> gateEntryVehicleRepository;
        private Repository<VehicleMaster> vehicleMasterRepo;
        private Repository<DriverMaster> DriverMasterRepo;
        public GateEntryVehicleManager()
        {
            gateEntryVehicleRepository = unitOfWork.Repository<GateEntryVehicleMaster>();
            vehicleMasterRepo = unitOfWork.Repository<VehicleMaster>();
            DriverMasterRepo = unitOfWork.Repository<DriverMaster>();
        }

        #region Add/Update/Delete Operation
        public bool Post(GateEntryVehicleMaster arg)
        {
            
            bool result = false;
            try
            {
                if (arg.VehicleEntryId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryVehicleRepository.Insert(arg);
                    result = true;
                    return result;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.UpdatedBy = username;
                    arg.UpdatedDate = DateTime.Now;

                    gateEntryVehicleRepository.Update(arg);
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
        
        public bool Delete(int VehicleEntryId)
        {
            bool result = false;
            try
            {
                GateEntryVehicleMaster model = gateEntryVehicleRepository.GetById(VehicleEntryId);
                model.IsDeleted = true;
                gateEntryVehicleRepository.Update(model);
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
        public GateEntryVehicleMaster GetGateEntryVehicleById(int VehicleEntryId)
        {
            GateEntryVehicleMaster gateEntryVisitor = new GateEntryVehicleMaster();
            if (VehicleEntryId != 0)
            {
                gateEntryVisitor = gateEntryVehicleRepository.Table.Where(x => x.VehicleEntryId == VehicleEntryId).SingleOrDefault();
            }
            return gateEntryVisitor;
        }
        public List<GateEntryVehicleMaster> Get()
        {
            List<GateEntryVehicleMaster> gateentryvisitorlist = new List<GateEntryVehicleMaster>();
            try
            {
                gateentryvisitorlist = gateEntryVehicleRepository.Table.Where(x => x.IsDeleted == false).ToList<GateEntryVehicleMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateentryvisitorlist;
        }
        public VehicleMaster GetVehicleMasterId(int VehicleId)
        {
            VehicleMaster vehicleMaster = new VehicleMaster();
            if (VehicleId != 0)
            {
                vehicleMaster = vehicleMasterRepo.Table.Where(x => x.VehicleId == VehicleId && x.IsDeleted == false).FirstOrDefault();
            }
            return vehicleMaster;
        }
        public string GetPurpose(string PurposeID)
        {
           if (PurposeID == "1")
            {
                PurposeID = "Official";
            }
           else if(PurposeID == "2")
            {
                PurposeID = "Personal";
            }
           else
            {
                PurposeID = "";
            }
            return PurposeID;
        }

         public List<DriverMaster> GetDriverList()
        {
            List<DriverMaster> employNameList = new List<DriverMaster>();
            try
            {
               employNameList = DriverMasterRepo.Table.Where(x => x.IsDeleted == false).ToList<DriverMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return employNameList;
        }

        public List<int> GetOutwardDCNOBasedID(string DCNo)
        {
            GateEntryOutwardManager Manager = new GateEntryOutwardManager();
            GateEntryVehicleManager vehicleManager = new GateEntryVehicleManager();
            var list = (from x in Manager.Get()
                        join y in vehicleManager.Get() on x.DcNo equals y.DCNo
                        where y.DCNo == DCNo
                        select new
                        {
                            GateEntryOutwardID = x.GateEntryOutwardId
                        });
            var res = list.Select(x => x.GateEntryOutwardID).ToList();
            return res;

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
