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
  public class OperationManager:IOperationService,IDisposable
  {
          private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<OperationMaster> OperationMasterRepository;

        public OperationManager()
        {
            OperationMasterRepository = unitOfWork.Repository<OperationMaster>();
        }

        public OperationMaster Post(OperationMaster arg)
        {
            bool result = false;
            OperationMaster operationMaster = new OperationMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;            
                OperationMasterRepository.Insert(arg);
                operationMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return operationMaster;
        }
        public OperationMaster GetOperationMasterID(int OperationMasterId)
        {
            OperationMaster operationMaster = new OperationMaster();
            operationMaster = OperationMasterRepository.Table.Where(x => x.OperationMasterId == OperationMasterId).SingleOrDefault();
            return operationMaster;
        }
        public OperationMaster GetOperationName(string OperationName)
        {
            OperationMaster operationMaster = new OperationMaster();
            operationMaster = OperationMasterRepository.Table.Where(x => x.OperationName == OperationName).SingleOrDefault();
            return operationMaster;
        }
        public bool Put(OperationMaster arg)
        {
            bool result = false;
            try
            {
                OperationMaster model = OperationMasterRepository.Table.Where(p => p.OperationMasterId == arg.OperationMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.OperationMasterId = arg.OperationMasterId;
                    model.OperationName = arg.OperationName;
                    model.OperationTypeCode = arg.OperationTypeCode;                 
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();                     
                    model.UpdatedBy = username;
                    OperationMasterRepository.Update(model);
                    result = true;
                }

                else
                {
                    result = false;
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
                OperationMaster model = OperationMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                OperationMasterRepository.Update(model);
                //OperationMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public OperationMaster Get(int id)
        {
            return null;
        }
        public List<OperationMaster>Get()
        {
            List<OperationMaster> OperationMaster = new List<OperationMaster>();
            try
            {
                OperationMaster = OperationMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<OperationMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return OperationMaster;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}

    
