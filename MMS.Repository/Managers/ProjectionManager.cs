﻿using MMS.Common;
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
  public class ProjectionManager:IProjectionService,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ProjectionMaster> projectMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(ProjectionMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                projectMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(ProjectionMaster arg)
        {
            bool result = false;
            try
            {
                ProjectionMaster model = projectMasterRepository.Table.Where(p => p.ProjectionId == arg.ProjectionId).FirstOrDefault();
                if (model != null)
                {
                    model.ProjectionId = arg.ProjectionId;
                    model.OrderIndicationNo = arg.OrderIndicationNo;
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.SeasonMasterId = arg.SeasonMasterId;
                    model.Style = arg.Style;
                    model.Date = arg.Date;
                    model.WeekNo = arg.WeekNo;
                    model.CustomerPlan = arg.CustomerPlan;
                    model.Quantity = arg.Quantity;
                    model.IsSize = arg.IsSize;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();                    
                    model.UpdatedBy = username;
                    projectMasterRepository.Update(model);
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
                ProjectionMaster model = projectMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                projectMasterRepository.Update(model);
                //projectMasterRepository.Delete(model);
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

        public ProjectionManager()
        {
            projectMasterRepository = unitOfWork.Repository<ProjectionMaster>();
        }

        public ProjectionMaster GetOrderIndicationNo(string OrderIndicationNo)
        {
            ProjectionMaster projectionMaster = new ProjectionMaster();
            if (OrderIndicationNo != "" && OrderIndicationNo != null)
            {
                projectionMaster = projectMasterRepository.Table.Where(x => x.OrderIndicationNo == OrderIndicationNo).SingleOrDefault();
            }
            return projectionMaster;
        }

        public ProjectionMaster GetProjectionMasterId(int ProjectionId)
        {
            ProjectionMaster projectionMaster = new ProjectionMaster();
            if (ProjectionId != 0)
            {
                projectionMaster = projectMasterRepository.Table.Where(x => x.ProjectionId == ProjectionId).SingleOrDefault();
            }
            return projectionMaster;
        }

        public ProjectionMaster Get(int id)
        {
            return null;
        }

        public List<ProjectionMaster> Get()
        {
            List<ProjectionMaster> projectionMasterlist = new List<ProjectionMaster>();
            try
            {
                projectionMasterlist = projectMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<ProjectionMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return projectionMasterlist;
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