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
    public class GatePassManager : IGateService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GatePassMaster> gatePassMasterRepository;

        public GatePassManager()
        {
            gatePassMasterRepository = unitOfWork.Repository<GatePassMaster>();
        }

        public bool Post(GatePassMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                gatePassMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public GatePassMaster GetGatePassID(int GatePassId)
        {
            GatePassMaster gatePassMaster  = new GatePassMaster();
            gatePassMaster = gatePassMasterRepository.Table.Where(x => x.GatePassId == GatePassId).SingleOrDefault();
            return gatePassMaster;
        }
        public GatePassMaster GetMaterialName(int Material)
        {
            GatePassMaster gateMaster = new GatePassMaster();
            gateMaster = gatePassMasterRepository.Table.Where(x => x.Material == Material).FirstOrDefault();
            return gateMaster;
        }
        public bool Put(GatePassMaster arg)
        {
            bool result = false;
            try
            {
                GatePassMaster model = gatePassMasterRepository.Table.Where(p => p.GatePassId == arg.GatePassId).FirstOrDefault();
                if (model != null)
                {
                    model.GatePassId = arg.GatePassId;
                    model.GatePassNo = arg.GatePassNo;
                    model.IsSupplier = arg.IsSupplier;
                    model.Date = arg.Date;
                    model.PartyName = arg.PartyName;
                    model.DeliveryAddress = arg.DeliveryAddress;
                    model.Material = arg.Material;
                    model.Quantity = arg.Quantity;
                    model.UOM = arg.UOM;
                    model.Rate = arg.Rate;
                    model.Amount = arg.Amount;
                    model.IfReturnable = arg.IfReturnable;
                    model.Remarks = arg.Remarks;
                    model.IsPrintWithRateValue = arg.IsPrintWithRateValue;
                    model.IsPrintWithoutCompanyAddress = arg.IsPrintWithoutCompanyAddress;           
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();           
                    model.UpdatedBy = username;

                    gatePassMasterRepository.Update(model);
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
                GatePassMaster model = gatePassMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                gatePassMasterRepository.Update(model);
                //gatePassMasterRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public GatePassMaster Get(int id)
        {
            return null;
        }
        public List<GatePassMaster> Get()
        {
            List<GatePassMaster> gradeMaster = new List<GatePassMaster>();
            try
            {
                gradeMaster = gatePassMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<GatePassMaster>();
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
