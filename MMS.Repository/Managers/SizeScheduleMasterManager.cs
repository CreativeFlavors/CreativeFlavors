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
    public class SizeScheduleMasterManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SizeScheduleMaster> SizeScheduleMasterRepository;
        private Repository<SizeScheduleRange> sizeScheduleRangeRepository;
        #region Add/Update/Delete Operation

        public SizeScheduleMaster Post(SizeScheduleMaster arg)
        {
            SizeScheduleMaster sizeScheduleMaster = new SizeScheduleMaster();
            bool result = false;
            try
            {
                if (arg.SizeScheduleMasterId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    // arg.UpdatedBy = username;
                    SizeScheduleMasterRepository.Insert(arg);
                    sizeScheduleMaster = arg;
                    result = true;
                }
                else
                {
                    SizeScheduleMaster model = SizeScheduleMasterRepository.Table.Where(p => p.SizeScheduleMasterId == arg.SizeScheduleMasterId).FirstOrDefault();
                    if (model != null)
                    {
                        model.SizeScheduleMasterId = arg.SizeScheduleMasterId;
                        model.SizeMatchingNo = arg.SizeMatchingNo;
                        model.SizeRangeName = arg.SizeRangeName;
                        model.ShortUnitID = arg.ShortUnitID;
                        model.FromValue = arg.FromValue;
                        model.ToValue = arg.ToValue;
                        model.Equals = arg.Equals;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        model.CreatedBy = "";
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        SizeScheduleMasterRepository.Update(model);
                        sizeScheduleMaster = model;
                        result = true;
                    }
                }
               
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return sizeScheduleMaster;

        }
        public SizeScheduleRange SizeScheduleSave(SizeScheduleRange arg)
        {
            SizeScheduleRange sizeScheduleMaster = new SizeScheduleRange();
            bool result = false;
            try
            {
                if (arg.SizeScheduleID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    sizeScheduleRangeRepository.Insert(arg);
                    sizeScheduleMaster = arg;
                    result = true;
                }
                else if (arg.SizeScheduleID != 0)
                {
                    SizeScheduleRange model = sizeScheduleRangeRepository.Table.Where(p => p.SizeScheduleID == arg.SizeScheduleID).FirstOrDefault();
                    if (model != null)
                    {
                        model.SizeScheduleID = arg.SizeScheduleID;
                        model.SizeScheduleMasterID = arg.SizeScheduleMasterID;
                        model.Size = arg.Size;
                        model.Frame = arg.Frame;
                        model.IsDeleted = arg.IsDeleted;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        model.CreatedBy = arg.CreatedBy; ;
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        sizeScheduleRangeRepository.Update(model);
                        result = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return sizeScheduleMaster;

        }
        //public bool Put(SizeScheduleMaster arg)
        //{
        //    bool result = false;
        //    try
        //    {
        //        SizeScheduleMaster model = SizeScheduleMasterRepository.Table.Where(p => p.SizeScheduleMasterId == arg.SizeScheduleMasterId).FirstOrDefault();
        //        if (model != null)
        //        {
        //            model.SizeScheduleMasterId = arg.SizeScheduleMasterId;
        //            model.SizeMatchingNo = arg.SizeMatchingNo;
        //            model.SizeRangeName = arg.SizeRangeName;
        //            model.FromValue = arg.FromValue;
        //            model.ToValue = arg.ToValue;
        //            model.Equals = arg.Equals;
        //            model.CreatedDate = arg.CreatedDate;
        //            model.UpdatedDate = DateTime.Now;
        //            model.CreatedBy = "";
        //            string username = HttpContext.Current.Session["UserName"].ToString();
        //            model.UpdatedBy = username;
        //            SizeScheduleMasterRepository.Update(model);
        //            result = true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }

        //    return result;
        //}
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                SizeScheduleMaster model = SizeScheduleMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                SizeScheduleMasterRepository.Update(model);

                SizeScheduleRange model_ = sizeScheduleRangeRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                sizeScheduleRangeRepository.Update(model_);
                //SizeScheduleMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public bool SizeScheduleRangeDelete(int id)
        {
            bool result = false;
            try
            {
                SizeScheduleRange model = sizeScheduleRangeRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedOn = DateTime.Now;
                sizeScheduleRangeRepository.Update(model);              
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

        public SizeScheduleMasterManager()
        {
            SizeScheduleMasterRepository = unitOfWork.Repository<SizeScheduleMaster>();
            sizeScheduleRangeRepository = unitOfWork.Repository<SizeScheduleRange>();
        }

        public SizeScheduleMaster GetSizeRangeName(string SizeRangeName)
        {
            SizeScheduleMaster sizeScheduleMaster = new SizeScheduleMaster();
            if (SizeRangeName != "" && SizeRangeName != null)
            {
                sizeScheduleMaster = SizeScheduleMasterRepository.Table.Where(x => x.SizeRangeName == SizeRangeName).SingleOrDefault();
            }
            return sizeScheduleMaster;
        }
        public SizeScheduleRange GetSizeScheduleID(int SizeScheduleMasterID)
        {
            SizeScheduleRange sizeRangeMaster = new SizeScheduleRange();
            if (SizeScheduleMasterID != 0)
            {
                sizeRangeMaster = sizeScheduleRangeRepository.Table.Where(x => x.SizeScheduleMasterID == SizeScheduleMasterID).FirstOrDefault();
            }
            return sizeRangeMaster;
        }
        public List<SizeScheduleRange> GetSizeRange(int SizeScheduleMasterID)
        {
            List<SizeScheduleRange> sizeScheduleList = new List<SizeScheduleRange>();
            sizeScheduleList = sizeScheduleRangeRepository.Table.Where(x=>x.SizeScheduleMasterID== SizeScheduleMasterID && x.IsDeleted==false).ToList();
            return sizeScheduleList;
        }


        public SizeScheduleMaster GetSizeScheduleMasterId(int SizeScheduleMasterId)
        {
            SizeScheduleMaster sizeScheduleMaster = new SizeScheduleMaster();
            if (SizeScheduleMasterId != 0)
            {
                sizeScheduleMaster = SizeScheduleMasterRepository.Table.Where(x => x.SizeScheduleMasterId == SizeScheduleMasterId).SingleOrDefault();
            }
            return sizeScheduleMaster;
        }
        public SizeScheduleMaster Get(int id)
        {
            return null;
        }

        public List<SizeScheduleMaster> Get()
        {
            List<SizeScheduleMaster> sizeScheduleMasterlist = new List<SizeScheduleMaster>();
            try
            {
                sizeScheduleMasterlist = SizeScheduleMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SizeScheduleMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return sizeScheduleMasterlist;
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
