using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class ProductManager : Iproductservices, IDisposable
    {

        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<product> productrep;
        public ProductManager()
        {
            productrep = unitOfWork.Repository<product>();

        }
        #region Helper Method



        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool Post(product arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                productrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(product arg)
        {
            bool result = false;
            try
            {

                product model = productrep.Table.Where(p => p.ProductId == arg.ProductId).FirstOrDefault();
                if (model != null)
                {
                    model.ProductCode = arg.ProductCode;
                    model.ProductName = arg.ProductName;
                    model.ProductDesc = arg.ProductDesc;
                    model.TaxMasterId = arg.TaxMasterId;
                    model.UomMasterId = arg.UomMasterId;
                    model.Price = arg.Price;
                    model.BomNo = arg.BomNo;
                    model.IsActive = arg.IsActive;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedBy = "admin";
                    productrep.Update(model);
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

        public bool Delete(int productid)
        {
            bool result = false;
            try
            {
                product model = productrep.GetById(productid);
                //model.IsActive = false;

                productrep.Delete(model);
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

        public List<product> Get()
        {
            List<product> products = new List<product>();

            try
            {
                products = productrep.Table.ToList<product>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return products;
        }

        public product GetId(int productid)
        {
            product productlist = new product();
            if (productid != 0)
            {
                try
                {
                    productlist = productrep.Table.Where(x => x.ProductId == productid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return productlist;
        }
        public decimal GetProductqty(string bomno)
        {
            decimal val = 0;
            val = productrep.GetProductqty(bomno);
            return val;
        }
        public decimal GetsingleProductqty(int bomid)
        {
            decimal val = 0;
            val = productrep.GetsingleProductqty(bomid);
            return val;
        }
        public bool subassemblyPost(product arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                productrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        //List<product> Iproductservices.Get()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
