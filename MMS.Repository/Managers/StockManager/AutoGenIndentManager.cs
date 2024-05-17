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
        private Repository<tblAutoGenIndent> agentMasterRepository;


        public AutoGenIndentManager()
        {
            agentMasterRepository = unitOfWork.Repository<tblAutoGenIndent>();
        }
        #region Add/Update/Delete Operation

        public bool Post(tblAutoGenIndent arg)
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
        public bool Put(tblAutoGenIndent arg)
        {
            bool result = false;
            try
            {
                tblAutoGenIndent model = agentMasterRepository.Table.Where(p => p.AutoGenerateId == arg.AutoGenerateId).FirstOrDefault();
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
                tblAutoGenIndent model = agentMasterRepository.GetById(id);

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


        public tblAutoGenIndent GetAgentFullName(string IssueSlipDetailsId)
        {
            tblAutoGenIndent agentMaster = new tblAutoGenIndent();
            if (IssueSlipDetailsId != "" && IssueSlipDetailsId != null)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.IndentId == IssueSlipDetailsId).FirstOrDefault();
            }
            return agentMaster;
        }

        public tblAutoGenIndent GetAgentMasterId(int AutoGenerateId)
        {
            tblAutoGenIndent agentMaster = new tblAutoGenIndent();
            if (AutoGenerateId != 0)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AutoGenerateId == AutoGenerateId).SingleOrDefault();
            }
            return agentMaster;
        }

        public tblAutoGenIndent Get(int id)
        {
            return null;
        }

        public List<tblAutoGenIndent> Get()
        {
            List<tblAutoGenIndent> agentMasterlist = new List<tblAutoGenIndent>();
            try
            {
                agentMasterlist = agentMasterRepository.Table.ToList<tblAutoGenIndent>();
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
