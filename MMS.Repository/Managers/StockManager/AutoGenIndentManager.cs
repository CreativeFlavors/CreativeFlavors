using System;
using System.Collections.Generic;
using System.Linq;
using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
namespace MMS.Repository.Managers.StockManager
{
 
       public class AutoGenIndentManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<AutoGenIndent> agentMasterRepository;


        public AutoGenIndentManager()
        {
            agentMasterRepository = unitOfWork.Repository<AutoGenIndent>();
        }
        #region Add/Update/Delete Operation

        public bool Post(AutoGenIndent arg)
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
        public bool Put(AutoGenIndent arg)
        {
            bool result = false;
            try
            {
                AutoGenIndent model = agentMasterRepository.Table.Where(p => p.AutoGenerateId == arg.AutoGenerateId).FirstOrDefault();
                if (model != null)
                {
                    model.AutoGenerateId = arg.AutoGenerateId;
                    model.IndentId = arg.IndentId;



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
                AutoGenIndent model = agentMasterRepository.GetById(id);

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


        public AutoGenIndent GetAgentFullName(string IssueSlipDetailsId)
        {
            AutoGenIndent agentMaster = new AutoGenIndent();
            if (IssueSlipDetailsId != "" && IssueSlipDetailsId != null)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.IndentId == IssueSlipDetailsId).FirstOrDefault();
            }
            return agentMaster;
        }

        public AutoGenIndent GetAgentMasterId(int AutoGenerateId)
        {
            AutoGenIndent agentMaster = new AutoGenIndent();
            if (AutoGenerateId != 0)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AutoGenerateId == AutoGenerateId).SingleOrDefault();
            }
            return agentMaster;
        }

        public AutoGenIndent Get(int id)
        {
            return null;
        }

        public List<AutoGenIndent> Get()
        {
            List<AutoGenIndent> agentMasterlist = new List<AutoGenIndent>();
            try
            {
                agentMasterlist = agentMasterRepository.Table.ToList<AutoGenIndent>();
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
