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
    public class Product_TypeManger : IProduct_TypeService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Product_Type> product_TypeRepository;
        public Product_TypeManger()
        {
            product_TypeRepository = unitOfWork.Repository<Product_Type>();
        }
        public Product_Type Post(Product_Type arg)
        {
            bool result = false;
            Product_Type productType = new Product_Type();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();//HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                // arg.UpdatedBy = username;
                arg.CreatedDate = DateTime.Now;

                product_TypeRepository.Insert(arg);
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
        public Product_Type GetProductTypeID(int? ProductTypeID)
        {
            Product_Type productType = new Product_Type();
            productType = product_TypeRepository.Table.Where(x => x.ProductTypeID == ProductTypeID).SingleOrDefault();
            return productType;
        }
        public Product_Type GetProductName(string ProductTypeName)
        {
            Product_Type colorMaster = new Product_Type();
            colorMaster = product_TypeRepository.Table.Where(x => x.ProductTypeName == ProductTypeName).SingleOrDefault();
            return colorMaster;
        }
        public bool Put(Product_Type arg)
        {
            bool result = false;
            try
            {
                Product_Type model = product_TypeRepository.Table.Where(p => p.ProductTypeID == arg.ProductTypeID).FirstOrDefault();
                if (model != null)
                {
                    model.ProductTypeName = arg.ProductTypeName;
                    model.ProductTypeID = arg.ProductTypeID;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    product_TypeRepository.Update(model);
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
                Product_Type model = product_TypeRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                product_TypeRepository.Update(model);
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
        public List<Product_Type> Get()
        {
            List<Product_Type> productType = new List<Product_Type>();
            try
            {
                productType = product_TypeRepository.Table.Where(X => X.IsDeleted == true || X.IsDeleted == null).ToList<Product_Type>();
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
       
            Product_Type IProduct_TypeService.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
