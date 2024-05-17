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
    public class AttendanceIncentiveManager : IAttendanceIncentiveService,IDisposable
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<AttendanceIncentiveCalculation> AttendanceIncentiveRepository;

        #region Add/Update/Delete Operation

         public bool Post(AttendanceIncentiveCalculation arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                AttendanceIncentiveRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(AttendanceIncentiveCalculation arg)
        {
            bool result = false;
            try
            {
                AttendanceIncentiveCalculation model = AttendanceIncentiveRepository.Table.Where(p => p.AttendanceIncentiveCalcualtionId == arg.AttendanceIncentiveCalcualtionId).FirstOrDefault();
                if (model != null)
                {
                    model.AttendanceIncentiveCalcualtionId = arg.AttendanceIncentiveCalcualtionId;
                    model.Leave = arg.Leave;
                    model.Amount = arg.Amount;
                    model.Absent = arg.Absent;                  
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;    
                    AttendanceIncentiveRepository.Update(model);
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
                AttendanceIncentiveCalculation model = AttendanceIncentiveRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                AttendanceIncentiveRepository.Update(model);
                //  AttendanceIncentiveRepository.Delete(model);
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

        public AttendanceIncentiveManager()
        {
            AttendanceIncentiveRepository = unitOfWork.Repository<AttendanceIncentiveCalculation>();
        }

       

        public AttendanceIncentiveCalculation GetAttendanceIncentiveCalcualtionId(int AttendanceIncentiveCalcualtionId)
        {
            AttendanceIncentiveCalculation attendanceIncentiveCalcualtion = new AttendanceIncentiveCalculation();
            if (AttendanceIncentiveCalcualtionId != 0)
            {
                attendanceIncentiveCalcualtion = AttendanceIncentiveRepository.Table.Where(x => x.AttendanceIncentiveCalcualtionId == AttendanceIncentiveCalcualtionId).SingleOrDefault();
            }
            return attendanceIncentiveCalcualtion;
        }
        public AttendanceIncentiveCalculation Get(int id)
        {
            return null;
        }

        public List<AttendanceIncentiveCalculation> Get()
        {
            List<AttendanceIncentiveCalculation> attendanceIncentiveCalculationList = new List<AttendanceIncentiveCalculation>();
            try
            {
                attendanceIncentiveCalculationList = AttendanceIncentiveRepository.Table.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList<AttendanceIncentiveCalculation>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return attendanceIncentiveCalculationList;
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
