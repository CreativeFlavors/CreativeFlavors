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
    public class ProductTypeManager : IProductTypeService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ProductType> productTypeRepository;

        public ProductTypeManager()
        {
            productTypeRepository = unitOfWork.Repository<ProductType>();
        }
        public ProductType Post(ProductType arg)
        {
            bool result = false;
            ProductType productType = new ProductType();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                // arg.UpdatedBy = username;
                arg.CreatedDate = DateTime.Now;

                productTypeRepository.Insert(arg);
                productType = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return productType;
        }
        public ProductType GetProductTypeID(int? ProductTypeID)
        {
            ProductType productType = new ProductType();
            productType = productTypeRepository.Table.Where(x => x.ProductTypeID == ProductTypeID).SingleOrDefault();
            return productType;
        }
        public ProductType GetProductName(string ProductTypeName)
        {
            ProductType colorMaster = new ProductType();
            colorMaster = productTypeRepository.Table.Where(x => x.ProductTypeName == ProductTypeName).SingleOrDefault();
            return colorMaster;
        }
        public bool Put(ProductType arg)
        {
            bool result = false;
            try
            {
                ProductType model = productTypeRepository.Table.Where(p => p.ProductTypeID == arg.ProductTypeID).FirstOrDefault();
                if (model != null)
                {
                    model.ProductTypeName = arg.ProductTypeName;
                    model.ProductTypeID = arg.ProductTypeID;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    productTypeRepository.Update(model);
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
                ProductType model = productTypeRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                productTypeRepository.Update(model);
                //  productTypeRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public ProductType Get(int id)
        {
            return null;
        }
        public List<ProductType> Get()
        {
            List<ProductType> productType = new List<ProductType>();
            try
            {
                productType = productTypeRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<ProductType>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return productType;
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
