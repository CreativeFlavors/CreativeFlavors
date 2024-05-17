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
    public class BuyerManager : IBuyerService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BuyerMaster> buyerMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(BuyerMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                arg.CreatedDate = DateTime.Now;
                //arg.UpdatedDate = DateTime.Now;
                buyerMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(BuyerMaster arg)
        {
            bool result = false;
            try
            {

                BuyerMaster model = buyerMasterRepository.Table.Where(p => p.BuyerMasterId == arg.BuyerMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.BuyerCode = arg.BuyerCode;
                    model.BuyerFullName = arg.BuyerFullName;
                    model.BuyerShortName = arg.BuyerShortName;
                    model.Currency = arg.Currency;
                    model.BuyerAddress1 = arg.BuyerAddress1;
                    model.BuyerAddress2 = arg.BuyerAddress2;
                    model.BuyerPincode = arg.BuyerPincode;
                    model.Country = arg.Country;
                    model.ContactPersion = arg.ContactPersion;
                    model.Designation = arg.Designation;
                    model.ContactNoo = arg.ContactNoo;
                    model.EmailID = arg.EmailID;
                    model.STNoHead = arg.STNoHead;
                    model.CGTNoHead = arg.CGTNoHead;
                    model.DeliverAddress1 = arg.DeliverAddress1;
                    model.DeliverAddress2 = arg.DeliverAddress2;
                    model.Pincode = arg.Pincode;
                    model.AgentName = arg.AgentName;
                    model.AgentAddress1 = arg.AgentAddress1;
                    model.AgentAddress2 = arg.AgentAddress2;
                    model.AgentPincode = arg.AgentPincode;
                    model.AgentCountry = arg.AgentCountry;
                    model.AgentCurrency = arg.AgentCurrency;
                    model.PaymentsTerms = arg.PaymentsTerms;
                    model.DeliveryTerms = arg.DeliveryTerms;
                    model.Pincode = arg.Pincode;
                    model.Insurance = arg.Insurance;
                    model.DelierTo = arg.DelierTo;
                    model.Brand = arg.Brand;
                    model.ShipmentTo = arg.ShipmentTo;
                    model.ShimentMode = arg.ShimentMode;
                    model.CountryStamp = arg.CountryStamp;
                    //model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    //model.CreatedBy = username;
                    buyerMasterRepository.Update(model);
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
                BuyerMaster model = buyerMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                buyerMasterRepository.Update(model);
                //buyerMasterRepository.Delete(model);
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

        public BuyerManager()
        {
            buyerMasterRepository = unitOfWork.Repository<BuyerMaster>();
        }

        public BuyerMaster GetBuyerFullName(string BuyerFullName)
        {
            BuyerMaster buyerMaster = new BuyerMaster();
            if (BuyerFullName != "" && BuyerFullName != null)
            {
                buyerMaster = buyerMasterRepository.Table.Where(x => x.BuyerFullName == BuyerFullName.ToLower().Trim() && x.IsDeleted==false).FirstOrDefault();
            }
            return buyerMaster;
        }

        public BuyerMaster GetBuyerMasterId(int BuyerMasterId)
        {
            BuyerMaster buyerMaster = new BuyerMaster();
            if (BuyerMasterId != 0)
            {
                buyerMaster = buyerMasterRepository.Table.Where(x => x.BuyerMasterId == BuyerMasterId).SingleOrDefault();
            }
            return buyerMaster;
        }







        public BuyerMaster Get(int id)
        {
            return null;
        }

        public List<BuyerMaster> Get()
        {
            List<BuyerMaster> buyerMaster = new List<BuyerMaster>();

            try
            {
                buyerMaster = buyerMasterRepository.Table.Where(x => x.IsDeleted == false || x.IsDeleted == null).ToList<BuyerMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return buyerMaster;
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
