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
    public class TaxTypeManager:ITaxTypeService,IDisposable
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<TaxTypeMaster> taxMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(TaxTypeMaster arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                taxMasterRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(TaxTypeMaster arg)
        {
            bool result = false;
            try
            {
                TaxTypeMaster model = taxMasterRepository.Table.Where(p => p.TaxMasterID == arg.TaxMasterID).FirstOrDefault();
                if (model != null)
                {
                    model.TaxMasterID = arg.TaxMasterID;
                    model.TaxName = arg.TaxName;
                    model.TaxValue = arg.TaxValue;
                    //model.TaxMode = arg.TaxMode;
                    model.TaxValueMode = arg.TaxValueMode;
                    model.TaxOn = arg.TaxOn;
                    model.TaxCode = arg.TaxCode;
                    model.TaxRef = arg.TaxRef;
                    model.Form = arg.Form;
                    model.TaxOn = arg.TaxOn;
                    //model.CostOfTheProduct = arg.CostOfTheProduct;
                    model.TaxValueMode = arg.TaxValueMode;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedBy = model.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();                    
                    model.UpdatedBy = username;
                    taxMasterRepository.Update(model);
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
                TaxTypeMaster model = taxMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                taxMasterRepository.Update(model);
                //taxMasterRepository.Delete(model);
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

        public TaxTypeManager()
        {
            taxMasterRepository = unitOfWork.Repository<TaxTypeMaster>();
        }

        public TaxTypeMaster GetTaxName(string TaxName)
        {
            TaxTypeMaster taxMaster = new TaxTypeMaster();
            if (TaxName != "" && TaxName != null)
            {
                taxMaster = taxMasterRepository.Table.Where(x => x.TaxName == TaxName).SingleOrDefault();
            }
            return taxMaster;
        }

        public TaxTypeMaster GetTaxMasterId(int? TaxMasterID)
        {
            TaxTypeMaster taxMaster = new TaxTypeMaster();
            if (TaxMasterID != 0)
            {
                taxMaster = taxMasterRepository.Table.Where(x => x.TaxMasterID == TaxMasterID).SingleOrDefault();
            }
            return taxMaster;
        }

        public TaxTypeMaster Get(int id)
        {
            return null;
        }

        public List<TaxTypeMaster> Get()
        {
            List<TaxTypeMaster> taxMasterlist = new List<TaxTypeMaster>();
            try
            {
                taxMasterlist = taxMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<TaxTypeMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return taxMasterlist;
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
