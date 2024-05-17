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
    public class SupplierMasterManager 
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SupplierMaster> supplierMasterRepository;
        private Repository<SupplierSubMaster> supplierSubMasterRepository;

        #region Add/Update/Delete Operation

        public SupplierMaster Post(SupplierMaster arg)
        {
            bool result = false;
            SupplierMaster supplierMaster = new SupplierMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                supplierMasterRepository.Insert(arg);
                supplierMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return supplierMaster;

        }
        public SupplierSubMaster SupplierSubMasterSave(SupplierSubMaster arg)
        {
            bool result = false;
            SupplierSubMaster supplierSubMaster = new SupplierSubMaster();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                supplierSubMasterRepository.Insert(arg);
                supplierSubMaster = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return supplierSubMaster;

        }
        public SupplierSubMaster SupplierSubMasterUpdate(SupplierSubMaster arg)
        {
            SupplierSubMaster supplierSubMaster = new SupplierSubMaster();
            try
            {
                if (arg != null)
                {
                    supplierSubMasterRepository.Update(arg);
                    supplierSubMaster = arg;
                }
                else
                {
                    return supplierSubMaster;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
               // result = supplierSubMaster;
            }

            return supplierSubMaster;

        }
        public bool Put(SupplierMaster arg)
        {
            bool result = false;
            try
            {
                if(arg != null)
                {
                    supplierMasterRepository.Update(arg);
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
                SupplierMaster model = supplierMasterRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                supplierMasterRepository.Update(model);
                //supplierMasterRepository.Delete(model);
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

        public SupplierMasterManager()
        {
            supplierMasterRepository = unitOfWork.Repository<SupplierMaster>();
            supplierSubMasterRepository= unitOfWork.Repository<SupplierSubMaster>();
        }
       
        public SupplierMaster GetSupplierName(string SupplierName)
        {
            SupplierMaster supplierMaster = new SupplierMaster();
            if (SupplierName != "" && SupplierName != null)
            {
                supplierMaster = supplierMasterRepository.Table.Where(x => x.SupplierName == SupplierName).SingleOrDefault();
            }
            return supplierMaster;
        }

        public SupplierMaster GetSupplierMasterId(int SupplierMasterId)
        {
            SupplierMaster supplierMaster = new SupplierMaster();
            if (SupplierMasterId != 0)
            {
                supplierMaster = supplierMasterRepository.Table.Where(x => x.SupplierMasterId == SupplierMasterId).SingleOrDefault();
            }
            return supplierMaster;
        }
        public SupplierMaster GetSupplierMasterId(int? SupplierMasterId)
        {
            SupplierMaster supplierMaster = new SupplierMaster();
            if (SupplierMasterId != 0)
            {
                supplierMaster = supplierMasterRepository.Table.Where(x => x.SupplierMasterId == SupplierMasterId).SingleOrDefault();
            }
            return supplierMaster;
        }

        public SupplierMaster Get(int id)
        {
            return null;
        }

        public List<SupplierMaster> Get()
        {
            List<SupplierMaster> supplierMasterlist = new List<SupplierMaster>();

            try
            {
                supplierMasterlist = supplierMasterRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<SupplierMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return supplierMasterlist;
        }

        public List<SupplierMaster> GetSupplierMasterGrid(string SupplierName)
        {
            List<SupplierMaster> supplierlist = new List<SupplierMaster>();
            try
            {
                supplierlist = supplierMasterRepository.SearchSupplierList(SupplierName);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return supplierlist;
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
