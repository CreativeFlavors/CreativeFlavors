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
    public class AgentManager : AgentService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<AgentMaster> agentMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(AgentMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
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
        public bool Put(AgentMaster arg)
        {
            bool result = false;
            try
            {
                AgentMaster model = agentMasterRepository.Table.Where(p => p.AgentMasterId == arg.AgentMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.AgentMasterId = arg.AgentMasterId;
                    model.AgentCode = arg.AgentCode;
                    model.AgentFullName = arg.AgentFullName;
                    model.AgentShortName = arg.AgentShortName;
                    model.Currency = arg.Currency;
                    model.CommisionCriteriaId = arg.CommisionCriteriaId;
                    model.AddressLine1 = arg.AddressLine1;
                    model.AddressLine2 = arg.AddressLine2;
                    model.AddressLine3 = arg.AddressLine3;
                    model.Pincode = arg.Pincode;
                    //model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.ContactPerson = arg.ContactPerson;
                    model.MobileNo = arg.MobileNo;
                    model.EmailID = arg.EmailID;
                    model.CountryMasterId = arg.CountryMasterId;
                    model.CommisionPercentage = arg.CommisionPercentage;

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
                AgentMaster model = agentMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                agentMasterRepository.Update(model);

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

        public AgentManager()
        {
            agentMasterRepository = unitOfWork.Repository<AgentMaster>();
        }

        public AgentMaster GetAgentFullName(string AgentFullName)
        {
            AgentMaster agentMaster = new AgentMaster();
            if (AgentFullName != "" && AgentFullName != null)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AgentFullName == AgentFullName).SingleOrDefault();
            }
            return agentMaster;
        }

        public AgentMaster GetAgentMasterId(int AgentMasterId)
        {
            AgentMaster agentMaster = new AgentMaster();
            if (AgentMasterId != 0)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AgentMasterId == AgentMasterId).SingleOrDefault();
            }
            return agentMaster;
        }

        public AgentMaster Get(int id)
        {
            return null;
        }

        public List<AgentMaster> Get()
        {
            List<AgentMaster> agentMasterlist = new List<AgentMaster>();
            try
            {
                agentMasterlist = agentMasterRepository.Table.Where(x=>x.IsDeleted==false || x.IsDeleted == null).ToList<AgentMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return agentMasterlist;
        }

        public AgentMaster GetAgent(string agentName)
        {
            AgentMaster agentMaster = new AgentMaster();
            if (agentName != "")
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AgentFullName == agentName).SingleOrDefault();
            }
            return agentMaster;
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
