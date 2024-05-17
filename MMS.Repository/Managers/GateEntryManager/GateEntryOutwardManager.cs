using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.GateEntry;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMS.Repository.Managers.GateEntryManager
{
    public class GateEntryOutwardManager : IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GateEntryOutwardMaster> gateEntryOutwardRepo;
        private Repository<StoreMaster> storeMasterRepo;
        private Repository<Purpose> purposeRepo;
        private Repository<GateEntryOutwardCheck> gateEntry_OutwardCheck;
        public GateEntryOutwardManager()
        {
            gateEntryOutwardRepo = unitOfWork.Repository<GateEntryOutwardMaster>();
            storeMasterRepo = unitOfWork.Repository<StoreMaster>();
            purposeRepo = unitOfWork.Repository<Purpose>();
            gateEntry_OutwardCheck = unitOfWork.Repository<GateEntryOutwardCheck>();
        }


        #region Add/Update/Delete Operation
        public int Post(GateEntryOutwardMaster arg)
        {
            int result = 0;
            try
            {
                if (arg.GateEntryOutwardId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    arg.IsDeleted = false;
                    gateEntryOutwardRepo.Insert(arg);
                    result = arg.GateEntryOutwardId;
                    return result;
                }
                else
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.UpdatedBy = username;
                    arg.UpdatedDate = DateTime.Now;

                    gateEntryOutwardRepo.Update(arg);
                    result = arg.GateEntryOutwardId;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;

        }
        
        public bool Delete(int GateEntryOutwardId)
        {
            bool result = false;
            try
            {
                GateEntryOutwardMaster model = gateEntryOutwardRepo.GetById(GateEntryOutwardId);
                model.IsDeleted = true;
                gateEntryOutwardRepo.Update(model);
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
        public GateEntryOutwardMaster GetGateEntryOutById(int GateEntryOutwardId)
        {
            GateEntryOutwardMaster gateEntryOutward = new GateEntryOutwardMaster();
            if (GateEntryOutwardId != 0)
            {
                gateEntryOutward = gateEntryOutwardRepo.Table.Where(x => x.GateEntryOutwardId == GateEntryOutwardId).SingleOrDefault();
            }
            return gateEntryOutward;
        }     
        public List<GateEntryOutwardMaster> Get()
        {
            List<GateEntryOutwardMaster> gateEntryOutwardlist = new List<GateEntryOutwardMaster>();
            try
            {
                gateEntryOutwardlist = gateEntryOutwardRepo.Table.Where(x => x.IsDeleted == false).ToList<GateEntryOutwardMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gateEntryOutwardlist;
        }
        public List<Purpose> GetPurpose()
        {
            List<Purpose> Purposelist = new List<Purpose>();
            try
            {
                Purposelist = purposeRepo.Table.ToList<Purpose>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Purposelist;
        }

        public List<GateEntryOutwardMaster> GEMaterialListGridBasedOnPoNo(int PoNoId)
        {
            List<GateEntryOutwardMaster> ApprovedPriceList = new List<GateEntryOutwardMaster>();

            try
            {
                ApprovedPriceList = gateEntryOutwardRepo.Table.Where(x => x.PoNoId == PoNoId).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }
        public List<GateEntryOutwardMaster> GEMaterialListGridBasedOnGateEntryNo(string GateEntryNo)
        {
            List<GateEntryOutwardMaster> ApprovedPriceList = new List<GateEntryOutwardMaster>();

            try
            {
                ApprovedPriceList = gateEntryOutwardRepo.Table.Where(x => x.GateEntryNo == GateEntryNo && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }

        public StoreMaster GetStoreMasterId(int StoresName)
        {
            StoreMaster storeMaster = new StoreMaster();
            if (StoresName != 0)
            {
                storeMaster = storeMasterRepo.Table.Where(x => x.StoreMasterId == StoresName && x.IsDeleted == false).FirstOrDefault();
            }
            return storeMaster;
        }

        #endregion

        /// <summary>
        /// Pdf Generation method
        /// </summary>
        public List<GeneratePdf> GeneratePdf(string GateEntryNo)
        {
            List<GeneratePdf> receiptDetailslist = new List<GeneratePdf>();
            using (var context = new MMSContext())
            {
                receiptDetailslist = context.Database.SqlQuery<GeneratePdf>("EXEC GeneratePdf @GateEntryNo={0}", GateEntryNo).ToList();

            }
            return receiptDetailslist;
        }
        public List<GeneratePdf> GeneratePdf_Jobwork(string GateEntryNo)
        {
            List<GeneratePdf> receiptDetailslist = new List<GeneratePdf>();
            try
            {
                using (var context = new MMSContext())
                {
                    receiptDetailslist = context.Database.SqlQuery<GeneratePdf>("EXEC GeneratePdf_JobWork @GateEntryNo={0}", GateEntryNo).ToList();

                }
                return receiptDetailslist;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return receiptDetailslist;
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public List<MMS.Data.StoredProcedureModel.OutwardGateEntryCheck> GetOutwardGateEntry(string IssueSlipNo)
        {
            List<MMS.Data.StoredProcedureModel.OutwardGateEntryCheck> checkList = new List<MMS.Data.StoredProcedureModel.OutwardGateEntryCheck>();
            try
            {
                checkList = gateEntry_OutwardCheck.GetOutwardGateEntry(IssueSlipNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return checkList;
        }


        public string GetOutwardSizeRange(int IssueSlipId)
        {
            var size = "";
            try
            {
                size = gateEntry_OutwardCheck.GetOutwardSizeRange(IssueSlipId);
                return size;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int GateDetailsChecked(int IssueSlipNo,bool isGateChecked,string GateEntryNo)
        {
            int result;
            try
            {
                result= gateEntry_OutwardCheck.GetOutwardGateEntryCheck(IssueSlipNo, isGateChecked, GateEntryNo);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
            
        }
    }
}
