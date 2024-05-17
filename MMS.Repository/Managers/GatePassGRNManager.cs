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
    public class GatePassGRNManager : IGatePassGRNService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GatePassGRNMaster> GatePassGRNMasterRepository;

        public GatePassGRNManager()
        {
            GatePassGRNMasterRepository = unitOfWork.Repository<GatePassGRNMaster>();
        }

        public bool Post(GatePassGRNMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                GatePassGRNMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public GatePassGRNMaster GetGatePassGRNID(int GatePassInvwardId)
        {
            GatePassGRNMaster GatePassGRNMaster = new GatePassGRNMaster();
            GatePassGRNMaster = GatePassGRNMasterRepository.Table.Where(x => x.GatePassInvwardId == GatePassInvwardId).SingleOrDefault();
            return GatePassGRNMaster;
        }
        public GatePassGRNMaster GetMaterialName(int Material)
        {
            GatePassGRNMaster gateMaster = new GatePassGRNMaster();
            gateMaster = GatePassGRNMasterRepository.Table.Where(x => x.Material == Material).SingleOrDefault();
            return gateMaster;
        }
        public bool Put(GatePassGRNMaster arg)
        {
            bool result = false;
            try
            {
                GatePassGRNMaster model = GatePassGRNMasterRepository.Table.Where(p => p.GatePassInvwardId == arg.GatePassInvwardId).FirstOrDefault();
                if (model != null)
                {
                    model.GatePassInvwardId = arg.GatePassInvwardId;
                    model.ReceiptNo = arg.ReceiptNo;
                    model.IsSupplier = arg.IsSupplier;
                    model.Date = arg.Date;
                    model.PartyName = arg.PartyName;                   
                    model.Material = arg.Material;
                    model.Quantity = arg.Quantity;
                    model.UOM = arg.UOM;
                    model.Rate = arg.Rate;
                    model.Amount = arg.Amount;
                    model.Instructions = arg.Instructions;
                    model.IsPrintWithRateValue = arg.IsPrintWithRateValue;
                    model.IsPrintWithoutCompanyAddress = arg.IsPrintWithoutCompanyAddress;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();                  
                    model.UpdatedBy = username;
                    GatePassGRNMasterRepository.Update(model);
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
                GatePassGRNMaster model = GatePassGRNMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                GatePassGRNMasterRepository.Update(model);
                //  GatePassGRNMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public GatePassGRNMaster Get(int id)
        {
            return null;
        }
        public List<GatePassGRNMaster> Get()
        {
            List<GatePassGRNMaster> gradeMaster = new List<GatePassGRNMaster>();
            try
            {
                gradeMaster = GatePassGRNMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<GatePassGRNMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gradeMaster;
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
