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
   public  class GateEntryVisitorManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryVisitorMaster> gateEntryVisitorRepository;
        private UnitOfWork2 unitOfWork2 = new UnitOfWork2();
        private Repository2<EmployDesignationMaster> empDesignationRepository;


        public GateEntryVisitorManager()
        {         
            gateEntryVisitorRepository = unitOfWork.Repository<GateEntryVisitorMaster>();
           empDesignationRepository = unitOfWork2.Repository<EmployDesignationMaster>();
        }

        #region Add/Update/Delete Operation
        public bool Post(GateEntryVisitorMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.GateEntryVisitorId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                   arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryVisitorRepository.Insert(arg);
                    result = true;
                    return result;
                }
                else
                {
                   
                    GateEntryVisitorMaster model = gateEntryVisitorRepository.Table.Where(p => p.GateEntryVisitorId == arg.GateEntryVisitorId).FirstOrDefault();
                    if (model != null)
                    {                       
                        model.EntryDateTime = arg.EntryDateTime;
                        model.ReturnDateTime = arg.ReturnDateTime;
                        model.GateEntryVisitorId = arg.GateEntryVisitorId;                     
                        model.GateEntryNo = arg.GateEntryNo;
                        model.EntryType = arg.EntryType;
                        model.Designation = arg.Designation;
                        model.VisitorType = arg.VisitorType;
                        model.Purpose = arg.Purpose;
                        model.VisitorName = arg.VisitorName;
                        model.CompanyName = arg.CompanyName;
                        model.VehicleNo = arg.VehicleNo;
                        model.VisitorIdNo = arg.VisitorIdNo;
                        model.VisitorId = arg.VisitorId;
                        model.ComeBy = arg.ComeBy;
                        model.ToMeet = arg.ToMeet;
                        model.MobileNo = arg.MobileNo;
                        model.ReasonForVisit = arg.ReasonForVisit;
                        model.HandCarried = arg.HandCarried;
                        model.OtherInfo = arg.OtherInfo;
                        model.Remarks = arg.Remarks;
                        model.UpdatedDate = DateTime.Now;                       
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        gateEntryVisitorRepository.Update(model);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }

        public bool Delete(int GateEntryVisitorId)
        {
            bool result = false;
            try
            {
                GateEntryVisitorMaster model = gateEntryVisitorRepository.GetById(GateEntryVisitorId);
                model.IsDeleted = true;
                gateEntryVisitorRepository.Update(model);
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
        public GateEntryVisitorMaster GetGateEntryVisitorById(int GateEntryVisitorId)
        {
            GateEntryVisitorMaster gateEntryVisitor = new GateEntryVisitorMaster();
            if (GateEntryVisitorId != 0)
            {
                gateEntryVisitor = gateEntryVisitorRepository.Table.Where(x => x.GateEntryVisitorId == GateEntryVisitorId).SingleOrDefault();
            }
            return gateEntryVisitor;
        }

        public List<GateEntryVisitorMaster> Get()
        {
            List<GateEntryVisitorMaster> gateentryvisitorlist = new List<GateEntryVisitorMaster>();
            try
            {
                gateentryvisitorlist = gateEntryVisitorRepository.Table.Where(x => x.IsDeleted == false).ToList<GateEntryVisitorMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateentryvisitorlist;
        }

        public List<EmployDesignationMaster> GetEmployDesignationList()
        {
            List<EmployDesignationMaster> employDesignationList = new List<EmployDesignationMaster>();
            try
            {
                employDesignationList = empDesignationRepository.Table.Where(x => x.IsDeleted == false).ToList<EmployDesignationMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return employDesignationList;
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

