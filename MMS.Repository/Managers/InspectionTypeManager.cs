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
    public class InspectionTypeManager : IInspectionService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<InspectionTypeMaster> inspectionMasterRepository;

        #region Add/Update/Delete Operation

        public InspectionTypeMaster Post(InspectionTypeMaster arg)
        {
            bool result = false;
            InspectionTypeMaster inspectionTypeMaster = new InspectionTypeMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                inspectionMasterRepository.Insert(arg);
                inspectionTypeMaster = arg;

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return inspectionTypeMaster;

        }
        public bool Put(InspectionTypeMaster arg)
        {
            bool result = false;
            try
            {
                InspectionTypeMaster model = inspectionMasterRepository.Table.Where(p => p.InspectionTypeMasterId == arg.InspectionTypeMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.InspectionTypeMasterId = arg.InspectionTypeMasterId;
                    model.Inspection = arg.Inspection;
                    model.InspectionReportName = arg.InspectionReportName;
                    model.OperationId = arg.OperationId;
                    model.Parameter = arg.Parameter;
                    model.Code = arg.Code;
                    model.InspectionFrequency = arg.InspectionFrequency;
                    model.Type = arg.Type;
                    model.CommonCause = arg.CommonCause;
                    model.CreatedDate = arg.CreatedDate;
                    
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;

                    inspectionMasterRepository.Update(model);
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
                InspectionTypeMaster model = inspectionMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                inspectionMasterRepository.Update(model);
                //inspectionMasterRepository.Delete(model);
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

        public InspectionTypeManager()
        {
            inspectionMasterRepository = unitOfWork.Repository<InspectionTypeMaster>();
        }

        public InspectionTypeMaster GetParameter(string InspectionReportName)
        {
            InspectionTypeMaster inspectionMaster = new InspectionTypeMaster();
            if (InspectionReportName != "" && InspectionReportName != null)
            {
                inspectionMaster = inspectionMasterRepository.Table.Where(x => x.InspectionReportName == InspectionReportName).SingleOrDefault();
            }
            return inspectionMaster;
        }

        public InspectionTypeMaster GetInspectionTypeMasterId(int InspectionTypeId)
        {
            InspectionTypeMaster inspectionMaster = new InspectionTypeMaster();
            if (InspectionTypeId != 0)
            {
                inspectionMaster = inspectionMasterRepository.Table.Where(x => x.InspectionTypeMasterId == InspectionTypeId).SingleOrDefault();
            }
            return inspectionMaster;
        }

        public InspectionTypeMaster Get(int id)
        {
            return null;
        }

        public List<InspectionTypeMaster> Get()
        {
            List<InspectionTypeMaster> InspectionTypeMasterlist = new List<InspectionTypeMaster>();
            try
            {
                InspectionTypeMasterlist = inspectionMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<InspectionTypeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return InspectionTypeMasterlist;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        //public int getCurrencyID()
        //{
        //    int? CID = inspectionMasterRepository.Table.Max(u => (int?)u.InspectionTypeMasterId);
        //    return Convert.ToInt32(CID);
        //}

    }
        #endregion
}

