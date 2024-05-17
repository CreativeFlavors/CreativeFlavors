using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System.Web;   
namespace MMS.Repository.Managers
{
  public  class Product_BuyerStyleManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Product_BuyerStyleMaster> ProductBuyerStyleRepository;

        public Product_BuyerStyleManager()
        {
            ProductBuyerStyleRepository = unitOfWork.Repository<Product_BuyerStyleMaster>();
        }

        public Product_BuyerStyleMaster Post(Product_BuyerStyleMaster arg)
        {
            bool result = false;

            Product_BuyerStyleMaster product_BuyerStyleMaster = new Product_BuyerStyleMaster();
            try
            {
                if(arg.ProductOrBuyerStyleId==0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    ProductBuyerStyleRepository.Insert(arg);
                    result = true;
                    product_BuyerStyleMaster = arg;
                }
                else if(arg.ProductOrBuyerStyleId!=0)
                {
                    Product_BuyerStyleMaster model = ProductBuyerStyleRepository.Table.Where(p => p.ProductOrBuyerStyleId == arg.ProductOrBuyerStyleId).FirstOrDefault();
                    if (model != null)
                    {
                        model.ProductOrBuyerStyleId = arg.ProductOrBuyerStyleId;
                        model.BuyerName_ProductGroup = arg.BuyerName_ProductGroup;
                        model.BuyerModel_ProductType = arg.BuyerModel_ProductType;
                        model.BuyerStyle = arg.BuyerStyle;

                        model.Last = arg.Last;
                        model.ProductColour = arg.ProductColour;
                        model.OurStyle = arg.OurStyle;
                        model.SizeRange = arg.SizeRange;
                        model.BomNo = arg.BomNo;
                        model.LeatherName_1 = arg.LeatherName_1;
                        model.LeatherName_2 = arg.LeatherName_2;
                        model.LeatherName_3 = arg.LeatherName_3;
                        model.LeatherName_4 = arg.LeatherName_4;
                        model.UOM = arg.UOM;
                        model.Width_Fit = arg.Width_Fit;
                        model.FinishedProductType = arg.FinishedProductType;
                        model.StandardPrice = arg.StandardPrice;
                        model.Product_Image = arg.Product_Image;
                        model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        model.Weight = arg.Weight;
                        model.Destination = arg.Destination;
                        //model.CreatedBy = "";
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                        ProductBuyerStyleRepository.Update(model);
                        product_BuyerStyleMaster = model;
                        result = true;
                    }
                }               
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return product_BuyerStyleMaster;
        }

        public Product_BuyerStyleMaster GetOurStyle(string OurStyle)
        {
            Product_BuyerStyleMaster product_BuyerStyleMaster = new Product_BuyerStyleMaster();
            if (OurStyle != "")
            {
                product_BuyerStyleMaster = ProductBuyerStyleRepository.Table.Where(x => x.OurStyle.ToLower() == OurStyle.ToLower().Trim()).FirstOrDefault();
            }
            return product_BuyerStyleMaster;
        }

        public Product_BuyerStyleMaster GetProductOrBuyerStyleId(int ProductOrBuyerStyleId)
        {
            Product_BuyerStyleMaster product_BuyerStyleMaster = new Product_BuyerStyleMaster();
            if (ProductOrBuyerStyleId != 0)
            {
                product_BuyerStyleMaster = ProductBuyerStyleRepository.Table.Where(x => x.ProductOrBuyerStyleId == ProductOrBuyerStyleId).SingleOrDefault();
            }
            return product_BuyerStyleMaster;
        }

        public Product_BuyerStyleMaster Put(Product_BuyerStyleMaster arg)
        {
            bool result = false;
            Product_BuyerStyleMaster product_BuyerStyleMaster = new Product_BuyerStyleMaster();
            try
            {

                Product_BuyerStyleMaster model = ProductBuyerStyleRepository.Table.Where(p => p.ProductOrBuyerStyleId == arg.ProductOrBuyerStyleId).FirstOrDefault();
                if (model != null)
                {
                    model.ProductOrBuyerStyleId = arg.ProductOrBuyerStyleId;
                    model.BuyerName_ProductGroup = arg.BuyerName_ProductGroup;
                    model.BuyerModel_ProductType = arg.BuyerModel_ProductType;
                    model.BuyerStyle = arg.BuyerStyle;

                    model.Last = arg.Last;
                    model.ProductColour = arg.ProductColour;
                    model.OurStyle = arg.OurStyle;
                    model.SizeRange = arg.SizeRange;
                    model.BomNo = arg.BomNo;
                    model.LeatherName_1 = arg.LeatherName_1;
                    model.LeatherName_2 = arg.LeatherName_2;
                    model.LeatherName_3 = arg.LeatherName_3;
                    model.LeatherName_4 = arg.LeatherName_4;
                    model.UOM = arg.UOM;
                    model.Width_Fit = arg.Width_Fit;
                    model.FinishedProductType = arg.FinishedProductType;
                    model.StandardPrice = arg.StandardPrice;
                    model.Product_Image = arg.Product_Image;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    ProductBuyerStyleRepository.Update(model);
                    product_BuyerStyleMaster = model;
                    model.Weight = arg.Weight;
                    model.Destination = arg.Destination;
                    result = true;
                }
                else
                {
                    return product_BuyerStyleMaster;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return product_BuyerStyleMaster;
        }

        public List<Product_BuyerStyleMaster> Get()
        {
            List<Product_BuyerStyleMaster> ProductList = new List<Product_BuyerStyleMaster>();
            try
            {
                ProductList = ProductBuyerStyleRepository.Table.ToList<Product_BuyerStyleMaster>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ProductList;
        }        

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Product_BuyerStyleMaster model = ProductBuyerStyleRepository.GetById(id);
                ProductBuyerStyleRepository.Delete(model);
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
