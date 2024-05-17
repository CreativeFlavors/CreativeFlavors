using System;
using System.Collections.Generic;
using System.Linq;
using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers.StockManager
{
    public class AutoGenPoManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository< AutoGenPo> agentMasterRepository;


        public AutoGenPoManager()
        {
            agentMasterRepository = unitOfWork.Repository< AutoGenPo>();
        }
        #region Add/Update/Delete Operation

        public bool Post( AutoGenPo arg)
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
        public bool Put( AutoGenPo arg)
        {
            bool result = false;
            try
            {
                 AutoGenPo model = agentMasterRepository.Table.Where(p => p.AutoGenerateId == arg.AutoGenerateId).FirstOrDefault();
                if (model != null)
                {
                    model.AutoGenerateId = arg.AutoGenerateId;
                    model.PoId = arg.PoId;



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
                 AutoGenPo model = agentMasterRepository.GetById(id);

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


        public  AutoGenPo GetAgentFullName(string IssueSlipDetailsId)
        {
             AutoGenPo agentMaster = new  AutoGenPo();
            if (IssueSlipDetailsId != "" && IssueSlipDetailsId != null)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.PoId == IssueSlipDetailsId).SingleOrDefault();
            }
            return agentMaster;
        }

        public  AutoGenPo GetAgentMasterId(int AutoGenerateId)
        {
             AutoGenPo agentMaster = new  AutoGenPo();
            if (AutoGenerateId != 0)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AutoGenerateId == AutoGenerateId).SingleOrDefault();
            }
            return agentMaster;
        }

        public  AutoGenPo Get(int id)
        {
            return null;
        }

        public List< AutoGenPo> Get()
        {
            List< AutoGenPo> agentMasterlist = new List< AutoGenPo>();
            try
            {
                agentMasterlist = agentMasterRepository.Table.ToList< AutoGenPo>();
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
