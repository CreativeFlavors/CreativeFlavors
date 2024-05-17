using MMS.Common;
using MMS.Core.Entities.GateEntry;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.GateEntryManager
{
   public class GEVisitReasonManager : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryVisitReasonMaster> gateEntryVisitReasonRepository;
        public GEVisitReasonManager()
        {
            gateEntryVisitReasonRepository = unitOfWork.Repository<GateEntryVisitReasonMaster>();

        }

        #region Add/Update/Delete Operation
        public bool Post(GateEntryVisitReasonMaster arg)
        {
            bool result = false;
            try
            {
                if (arg.GEVisitReasonId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryVisitReasonRepository.Insert(arg);
                    result = true;
                    return result;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.UpdatedBy = username;
                    arg.UpdatedDate = DateTime.Now;

                    gateEntryVisitReasonRepository.Update(arg);
                    result = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Delete(int GEVisitReasonId)
        {
            bool result = false;
            try
            {
                GateEntryVisitReasonMaster model = gateEntryVisitReasonRepository.GetById(GEVisitReasonId);
                model.IsDeleted = true;
                gateEntryVisitReasonRepository.Update(model);
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
        public GateEntryVisitReasonMaster GetGEVisitReasonById(int GEVisitReasonId)
        {
            GateEntryVisitReasonMaster gateEntryVisitReason = new GateEntryVisitReasonMaster();
            if (GEVisitReasonId != 0)
            {
                gateEntryVisitReason = gateEntryVisitReasonRepository.Table.Where(x => x.GEVisitReasonId == GEVisitReasonId).SingleOrDefault();
            }
            return gateEntryVisitReason;
        }
        
        public List<GateEntryVisitReasonMaster> Get()
        {
            List<GateEntryVisitReasonMaster> gateEntryVisitReasonlist = new List<GateEntryVisitReasonMaster>();
            try
            {
                gateEntryVisitReasonlist = gateEntryVisitReasonRepository.Table.Where(x => x.IsDeleted == false).ToList<GateEntryVisitReasonMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateEntryVisitReasonlist;
        }
        
        #endregion
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}
