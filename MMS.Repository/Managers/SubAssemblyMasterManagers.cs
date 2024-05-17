using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class SubAssemblyMasterManagers : IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SubAssemblyMaster> SubAssemblyrep;
        public SubAssemblyMasterManagers()
        {
            SubAssemblyrep = unitOfWork.Repository<SubAssemblyMaster>();
        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        public List<SubAssemblyMaster> Get()
        {
            List<SubAssemblyMaster> Sub = new List<SubAssemblyMaster>();

            try
            {
                Sub = SubAssemblyrep.Table.ToList<SubAssemblyMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Sub;
        }
        public List<SubAssemblyMaster> GetSubAssemblyMaster()
        {
            List<SubAssemblyMaster> Sub = new List<SubAssemblyMaster>();

            try
            {
                Sub = SubAssemblyrep.GetSubAssemblyMaster();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return Sub;
        }
        public bool Post(SubAssemblyMaster arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                SubAssemblyrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        //public bool Put(SubAssemblyMaster arg)
        //{
        //    bool result = false;
        //    try
        //    {

        //        SubAssemblyMaster model = SubAssemblyrep.Table.Where(p => p.ProductId == arg.ProductId).FirstOrDefault();
        //        if (model != null)
        //        {
        //            model.ProductCode = arg.ProductCode;
        //            model.ProductName = arg.ProductName;
        //            model.ProductDesc = arg.ProductDesc;
        //            model.TaxMasterId = arg.TaxMasterId;
        //            model.UomMasterId = arg.UomMasterId;
        //            model.Price = arg.Price;
        //            model.BomNo = arg.BomNo;
        //            model.IsActive = arg.IsActive;
        //            model.UpdatedDate = DateTime.Now;
        //            model.UpdatedBy = "admin";
        //            SubAssemblyrep.Update(model);
        //            result = true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }

        //    return result;
        //}

        public bool Delete(int productid)
        {
            bool result = false;
            try
            {
                SubAssemblyMaster model = SubAssemblyrep.GetById(productid);
                //model.IsActive = false;

                SubAssemblyrep.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
    }
}
