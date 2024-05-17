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
    public  class GateEntryOutwardDocumentManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryOutwardDocumentMaster> gateEntryOutwardDocumentRepository;
        
        private UnitOfWork2 unitOfWork2 = new UnitOfWork2();
        private Repository2<EmployNameMaster> empNameRepository;
        private Repository2<EmployDepartmentMaster> empDeptRepository;

        public GateEntryOutwardDocumentManager()
        {
            gateEntryOutwardDocumentRepository = unitOfWork.Repository<GateEntryOutwardDocumentMaster>();
            empNameRepository = unitOfWork2.Repository<EmployNameMaster>();
            empDeptRepository = unitOfWork2.Repository<EmployDepartmentMaster>();
        }

        #region Add/Update/Delete Operation
        public bool Post(GateEntryOutwardDocumentMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.GateEntryOutwardDocumentId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryOutwardDocumentRepository.Insert(arg);
                    result = true;
                    return result;
                }
                else
                {
                    GateEntryOutwardDocumentMaster model = gateEntryOutwardDocumentRepository.Table.Where(p => p.GateEntryOutwardDocumentId == arg.GateEntryOutwardDocumentId).FirstOrDefault();
                  
                        model.GateEntryOutwardDocumentId = arg.GateEntryOutwardDocumentId;
                        model.OutwardDocTypeId = arg.OutwardDocTypeId;
                        model.GateEntryNo = arg.GateEntryNo;
                        model.EntryDateTime = arg.EntryDateTime;
                        model.Mode = arg.Mode;
                        model.CourierNo = arg.CourierNo;
                        model.Company = arg.Company;
                        model.Unit = arg.Unit;
                        model.PersonName = arg.PersonName;
                        model.ModeofTransport = arg.ModeofTransport;
                        model.VehicleNo = arg.VehicleNo;
                        model.AddressToWhom = arg.AddressToWhom;
                        model.HandOverTo = arg.HandOverTo;
                        model.ReceivedBy = arg.ReceivedBy;
                        model.Remarks = arg.Remarks;
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        arg.UpdatedBy = username;
                        arg.UpdatedDate = DateTime.Now;
                        gateEntryOutwardDocumentRepository.Update(model);
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

        public bool Delete(int GateEntryDocumentId)
        {
            bool result = false;
            try
            {
                GateEntryOutwardDocumentMaster model = gateEntryOutwardDocumentRepository.GetById(GateEntryDocumentId);
                model.IsDeleted = true;
                gateEntryOutwardDocumentRepository.Update(model);
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
        public GateEntryOutwardDocumentMaster GetGateEntryDocumentById(int GateEntryOutwardDocumentId)
        {
            GateEntryOutwardDocumentMaster gateEntryDocument = new GateEntryOutwardDocumentMaster();
            if (GateEntryOutwardDocumentId != 0)
            {
                gateEntryDocument = gateEntryOutwardDocumentRepository.Table.Where(x => x.GateEntryOutwardDocumentId == GateEntryOutwardDocumentId).SingleOrDefault();
            }
            return gateEntryDocument;
        }

        public GateEntryOutwardDocumentMaster GetGateEntryDocumentByNo(string GateEntryNo)
        {
            GateEntryOutwardDocumentMaster gateEntryDocument = new GateEntryOutwardDocumentMaster();
            if (GateEntryNo != null)
            {
                gateEntryDocument = gateEntryOutwardDocumentRepository.Table.Where(x => x.GateEntryNo == GateEntryNo).SingleOrDefault();
            }
            return gateEntryDocument;
        }

        public List<EmployNameMaster> GetEmployNameList()
        {
            List<EmployNameMaster> employNameList = new List<EmployNameMaster>();
            try
            {
                employNameList = empNameRepository.Table.Where(x => x.IsDeleted == false).ToList<EmployNameMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return employNameList;
        }



        public List<EmployDepartmentMaster> GetEmployDeptList()
        {
            List<EmployDepartmentMaster> employDeptList = new List<EmployDepartmentMaster>();
            try
            {
                employDeptList = empDeptRepository.Table.Where(x => x.IsDeleted == false).ToList<EmployDepartmentMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return employDeptList;
        }

        public List<GateEntryOutwardDocumentMaster> Get()
        {
            List<GateEntryOutwardDocumentMaster> gateentrydocumentlist = new List<GateEntryOutwardDocumentMaster>();
            try
            {
                gateentrydocumentlist = gateEntryOutwardDocumentRepository.Table.Where(x => x.IsDeleted == false).ToList<GateEntryOutwardDocumentMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateentrydocumentlist;
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
