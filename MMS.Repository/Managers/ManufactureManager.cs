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
    public class ManufactureManager : IManufactureService,IDisposable
    {
        
         private UnitOfWork unitOfWork = new UnitOfWork();
         private Repository<ManufacturerMaster> ManufacturerMasterRepository;

        #region Add/Update/Delete Operation

         public ManufacturerMaster Post(ManufacturerMaster arg)
        {
            bool result = false;
            ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;             
                ManufacturerMasterRepository.Insert(arg);
                manufacturerMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return manufacturerMaster;

        }
         public bool Put(ManufacturerMaster arg)
        {
            bool result = false;
            try
            {
                ManufacturerMaster model = ManufacturerMasterRepository.Table.Where(p => p.ManufacturerMasterId == arg.ManufacturerMasterId).FirstOrDefault();
                if (model != null)
                {
                    model.ManufacturerMasterId = arg.ManufacturerMasterId;
                    model.ManufacturerCode = arg.ManufacturerCode;
                    model.ManufacturerName = arg.ManufacturerName;                    
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();                  
                    model.UpdatedBy = username;
                    ManufacturerMasterRepository.Update(model);
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
                ManufacturerMaster model = ManufacturerMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                ManufacturerMasterRepository.Update(model);
                // ManufacturerMasterRepository.Delete(model);
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

        public ManufactureManager()
        {
            ManufacturerMasterRepository = unitOfWork.Repository<ManufacturerMaster>();
        }

        public ManufacturerMaster GetManufacturerName(string ManufacturerName)
        {
            ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
            if (ManufacturerName != "" && ManufacturerName != null)
            {
                manufacturerMaster = ManufacturerMasterRepository.Table.Where(x => x.ManufacturerName == ManufacturerName).SingleOrDefault();
            }
            return manufacturerMaster;
        }

        public ManufacturerMaster GetManufacturerMasterId(int ManufacturerMasterId)
        {
            ManufacturerMaster manufacturerMaster = new ManufacturerMaster();
            if (ManufacturerMasterId != 0)
            {
                manufacturerMaster = ManufacturerMasterRepository.Table.Where(x => x.ManufacturerMasterId == ManufacturerMasterId).SingleOrDefault();
            }
            return manufacturerMaster;
        }
        public ManufacturerMaster Get(int id)
        {
            return null;
        }

        public List<ManufacturerMaster> Get()
        {
            List<ManufacturerMaster> manufacturerMaster = new List<ManufacturerMaster>();
            try
            {
                manufacturerMaster = ManufacturerMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<ManufacturerMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return manufacturerMaster;
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
