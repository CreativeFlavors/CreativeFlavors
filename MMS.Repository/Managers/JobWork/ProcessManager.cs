using MMS.Common;
using MMS.Core.Entities.JobWork;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.JobWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.JobWork
{
    public class ProcessManager : IProcessService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ProcessMaster> processRepository;
        public ProcessManager()
        {
            processRepository = unitOfWork.Repository<ProcessMaster>();
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                ProcessMaster model = processRepository.GetById(id);
                model.IsDeleted = true;
                model.UpdatedBy = HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedDate = DateTime.Now;
                processRepository.Update(model);
                // colorRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<ProcessMaster> Get(int id)
        {
            return null;
        }

        public List<ProcessMaster> Get()
        {
            List<ProcessMaster> processMaster = new List<ProcessMaster>();
            try
            {
                processMaster = processRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<ProcessMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return processMaster;
        }

        public bool Post(ProcessMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                processRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(ProcessMaster arg)
        {
            bool result = false;
            try
            {
                ProcessMaster model = processRepository.Table.Where(p => p.ProcessId == arg.ProcessId).FirstOrDefault();
                if (model != null)
                {
                    model.ProcessCode = arg.ProcessCode;
                    model.ProcessName = arg.ProcessName;
                    model.Ioinvolved = arg.Ioinvolved;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    processRepository.Update(model);
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

        ProcessMaster IProcessService.Get(int id)
        {
            throw new NotImplementedException();
        }
        public ProcessMaster GetprocessMasterID(int? ProcessId)
        {
            ProcessMaster processMaster = new ProcessMaster();
            processMaster = processRepository.Table.Where(x => x.ProcessId == ProcessId).FirstOrDefault();
            return processMaster;
        }
    }
}
