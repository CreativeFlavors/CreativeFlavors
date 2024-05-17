using System;
using System.Collections.Generic;
using System.Linq;
using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using System.Text;
using System.Threading.Tasks;
using MMS.Core.Entities.JobWork;
using MMS.Repository.Service.JobWork;

namespace MMS.Repository.Managers.StockManager
{
   public class AutoGenGrnManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<AutoGenGrnNo> agentMasterRepository;
        private Repository<Tbl_Rejected_qtyListMaster> Tbl_Rejected_qtyListMasterRepository;


        public AutoGenGrnManager()
        {
            agentMasterRepository = unitOfWork.Repository<AutoGenGrnNo>();
            Tbl_Rejected_qtyListMasterRepository = unitOfWork.Repository<Tbl_Rejected_qtyListMaster>();
        }
        #region Add/Update/Delete Operation

        public bool Post(AutoGenGrnNo arg)
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
        public bool Post_Tbl_Rejected_qty(Tbl_Rejected_qtyListMaster arg)
        {
            bool result = false;
            try
            {
                arg.CreatedDate = DateTime.Now;
                //arg.UpdatedBy = username;
                Tbl_Rejected_qtyListMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public List<Tbl_Rejected_qtyListMaster> Get_Grnid(int Grnid)
        {
            List<Tbl_Rejected_qtyListMaster> materialMaster = new List<Tbl_Rejected_qtyListMaster>();
            try
            {
                materialMaster = Tbl_Rejected_qtyListMasterRepository.Table.Where(X => X.IsDeleted == false && X.Grn_id== Grnid).ToList<Tbl_Rejected_qtyListMaster>();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return materialMaster;
        }
        public bool Put(AutoGenGrnNo arg)
        {
            bool result = false;
            try
            {
                AutoGenGrnNo model = agentMasterRepository.Table.Where(p => p.AutoGenerateId == arg.AutoGenerateId).FirstOrDefault();
                if (model != null)
                {
                    model.AutoGenerateId = arg.AutoGenerateId;
                    model.GrnId = arg.GrnId;



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
                AutoGenGrnNo model = agentMasterRepository.GetById(id);

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


        public AutoGenGrnNo GetAgentFullName(string IssueSlipDetailsId)
        {
            AutoGenGrnNo agentMaster = new AutoGenGrnNo();
            if (IssueSlipDetailsId != "" && IssueSlipDetailsId != null)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.GrnId == IssueSlipDetailsId).SingleOrDefault();
            }
            return agentMaster;
        }

        public AutoGenGrnNo GetAgentMasterId(int AutoGenerateId)
        {
            AutoGenGrnNo agentMaster = new AutoGenGrnNo();
            if (AutoGenerateId != 0)
            {
                agentMaster = agentMasterRepository.Table.Where(x => x.AutoGenerateId == AutoGenerateId).SingleOrDefault();
            }
            return agentMaster;
        }

        public AutoGenGrnNo Get(int id)
        {
            return null;
        }

        public List<AutoGenGrnNo> Get()
        {
            List<AutoGenGrnNo> agentMasterlist = new List<AutoGenGrnNo>();
            try
            {
                agentMasterlist = agentMasterRepository.Table.ToList<AutoGenGrnNo>();
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
