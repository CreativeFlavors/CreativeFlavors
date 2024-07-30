using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class SupplierMaterialManager : ISupplierMaterialService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SupplierMaterial> suppliermaterialRepository;
        public SupplierMaterialManager()
        {
            suppliermaterialRepository = unitOfWork.Repository<SupplierMaterial>();
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        public List<SupplierMaterialgrid> SupplierMaterialgrid()
        {
            List<SupplierMaterialgrid> FineshedgoodsReport = new List<SupplierMaterialgrid>();
            FineshedgoodsReport = suppliermaterialRepository.suppliermaterial();
            return FineshedgoodsReport;
        }
        public bool IsMaterialSuppliedBySupplier(int? productId, int supplierId)
        {
            bool isSupplied = false;

            try
            {
                isSupplied = suppliermaterialRepository.Table
                                .Any(x => x.ProductId == productId && x.SupplierId == supplierId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return isSupplied;
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                SupplierMaterial model = suppliermaterialRepository.GetById(id);
                model.IsActive = false;
                model.UpdatedBy = "admin";//HttpContext.Current.Session["UserName"].ToString();
                model.UpdatedDate = DateTime.Now;
                suppliermaterialRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool Post(SupplierMaterial arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                //arg.UpdatedBy = username;
                arg.CreatedDate = DateTime.Now;
                suppliermaterialRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Put(SupplierMaterial arg)
        {
            bool result = false;
            try
            {
                SupplierMaterial model = suppliermaterialRepository.Table.Where(p => p.SupplierMaterialId == arg.SupplierMaterialId).FirstOrDefault();
                if (model != null)
                {
                    model.SupplierId = arg.SupplierId;
                    model.ProductId = arg.ProductId;
                    model.UomId = arg.UomId;
                    model.Categoryid = arg.Categoryid;
                    model.TaxId = arg.TaxId;
                    model.UpdatedDate = DateTime.Now;
                    string username = "admin";

                    model.UpdatedBy = username;
                    suppliermaterialRepository.Update(model);
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
        public List<SupplierMaterial> Get()
        {
            List<SupplierMaterial> SupplierMaterial = new List<SupplierMaterial>();
            try
            {
                SupplierMaterial = suppliermaterialRepository.Table.Where(x => x.IsActive == true).ToList<SupplierMaterial>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return SupplierMaterial;
        }
        public SupplierMaterial GetCustAddressId(int SupplierMaterialId)
        {
            SupplierMaterial oCustAddress = new SupplierMaterial();
            if (SupplierMaterialId != 0)
            {
                oCustAddress = suppliermaterialRepository.Table.Where(x => x.SupplierMaterialId == SupplierMaterialId).FirstOrDefault();
            }
            return oCustAddress;
        }
    }
}
