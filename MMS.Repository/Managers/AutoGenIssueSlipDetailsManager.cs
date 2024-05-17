using System;
using System.Collections.Generic;
using System.Linq;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using MMS.Web;

using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
   public class AutoGenIssueSlipDetailsManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<tblautogenissueslipdetails> agentMasterRepository;


        public AutoGenIssueSlipDetailsManager()
        {
            agentMasterRepository = unitOfWork.Repository<tblautogenissueslipdetails>();
        }
        #region Add/Update/Delete Operation

        public bool Post(tblautogenissueslipdetails arg)
        {
            bool result = false;
            try
            {
                arg.CreatedDate = DateTime.Now;    
                    
                //arg.UpdatedBy = username;
                agentMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(tblautogenissueslipdetails arg)
        {
            bool result = false;
            try
            {
                tblautogenissueslipdetails model = agentMasterRepository.Table.Where(p => p.AutoGenerateId == arg.AutoGenerateId).FirstOrDefault();
                if (model != null)
                {
                    model.AutoGenerateId = arg.AutoGenerateId;
                    model.IssueSlipDetailsId = arg.IssueSlipDetailsId;             
                    
                    

                    agentMasterRepository.Update(model);
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
                tblautogenissueslipdetails model = agentMasterRepository.GetById(id);
                 
                agentMasterRepository.Delete(model);

                //agentMasterRepository.Delete(model);
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

       
        public tblautogenissueslipdetails GetAgentFullName(string IssueSlipDetailsId)
        {
            tblautogenissueslipdetails agentMaster = new tblautogenissueslipdetails();
            if (IssueSlipDetailsId != "" && IssueSlipDetailsId != null)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.IssueSlipDetailsId == IssueSlipDetailsId).SingleOrDefault();
            }
            return agentMaster;
        }

        public tblautogenissueslipdetails GetAgentMasterId(int AutoGenerateId)
        {
            tblautogenissueslipdetails agentMaster = new tblautogenissueslipdetails();
            if (AutoGenerateId != 0)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AutoGenerateId == AutoGenerateId).SingleOrDefault();
            }
            return agentMaster;
        }

        public AgentMaster Get(int id)
        {
            return null;
        }

        public List<tblautogenissueslipdetails> Get()
        {
            List<tblautogenissueslipdetails> agentMasterlist = new List<tblautogenissueslipdetails>();
            try
            {
                agentMasterlist = agentMasterRepository.Table.ToList<tblautogenissueslipdetails>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return agentMasterlist;
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
