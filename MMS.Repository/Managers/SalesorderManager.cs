﻿using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace MMS.Repository.Managers
{
    public class SalesorderManager : ISalesorder, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<salesordercart> salesorderrep;

        public SalesorderManager()
        {
            salesorderrep = unitOfWork.Repository<salesordercart>();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public Data.StoredProcedureModel.currencycunversion Getcurrencyconversion(string p_primarycurrency, string p_secondarycurrency, string todaydate)
        {
            currencycunversion currencycunversion = new currencycunversion();
            currencycunversion = salesorderrep.Getcurrencyconversion(p_primarycurrency, p_secondarycurrency, todaydate);
            return currencycunversion;
        }
        public List<MRP_Details> GetmrpList()
        {
            List<MRP_Details> MRP_Details = new List<MRP_Details>();
            try
            {
                MRP_Details = salesorderrep.MRP_DetailsList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MRP_Details;
        }
      
        public List<mrp_material_list> GetmrpmaterialList(int productid)
        {
            List<mrp_material_list> MRP_Details = new List<mrp_material_list>();
            try
            {
                MRP_Details = salesorderrep.mrpMaterialList1(productid);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MRP_Details;
        }
        public List<mrp_subassembly_list> GetmrpsubassemblyList(int? productid)
        {
            List<mrp_subassembly_list> MRP_Details = new List<mrp_subassembly_list>();
            try
            {
                MRP_Details = salesorderrep.get_mrp_subassembly_list(productid);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MRP_Details;
        }
        public salesordercart Post(salesordercart arg)
        {
            salesordercart salesorder  = new salesordercart();
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.createdby = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = 1;
                salesorderrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public int? GetNextsoNumberFromDatabase()
        {
            int? latestNumber = 0; 
            try
            {
                latestNumber = salesorderrep.Table.Max(p => p.salesordernumber);
                latestNumber++;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return latestNumber;
        }
        public List<SalesorderCart> GetsalesorderList(int customerid, int salesid)
        {
            List<SalesorderCart> salesorder = new List<SalesorderCart>();
            try
            {
                salesorder = salesorderrep.SearchSalesordercart(customerid, salesid);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<salesordercart> GetsalesorderCartList(int customerid, int salesordernumber)
        {
            List<salesordercart> salesorder = new List<salesordercart>();
                 salesorder = salesorderrep.Table.Where(x => x.customerid== customerid && x.Status == 1 && x.isdeleted == true && x.salesordernumber == salesordernumber).ToList();
            return salesorder;
        }
        public bool Putstatus()
        {
            bool result = false;

                List<salesordercart> models = salesorderrep.Table.Where(x => x.Status == 1 && x.isdeleted == true).ToList();

                if (models != null && models.Count > 0)
                {
                    foreach (var model in models)
                    {
                        model.Status = 0;
                        salesorderrep.Update(model);
                    }
                    result = true;
                }
                else
                {
                    return false;
                }

            return result;
        }
        public bool Putstatussuccess()
        {
            bool result = false;

            List<salesordercart> models = salesorderrep.Table.Where(x => x.Status == 1 && x.isdeleted == true).ToList();

            if (models != null && models.Count > 0)
            {
                foreach (var model in models)
                {
                    model.Status = 2;
                    salesorderrep.Update(model);
                }
                result = true;
            }
            else
            {
                return false;
            }

            return result;
        }

        public List<salesordercart> Get()
        {
            List<salesordercart> obj = new List<salesordercart>();
            obj = salesorderrep.Table.ToList<salesordercart>();
            return obj;
        }
        public salesordercart GetSO(int? id)
        {
            salesordercart salesorder = new salesordercart();
            if (id != null)
            {
                salesorder = salesorderrep.Table.Where(x => x.SalesorderId == id && x.isdeleted == true).FirstOrDefault();
            }
            return salesorder;
        }

        public salesordercart Getproductqty(int productid)
        {
            salesordercart salesorder = new salesordercart();
            if (productid != null)
            {
                salesorder = salesorderrep.Table.Where(x => x.ProductNameid == productid).FirstOrDefault();
            }
            return salesorder;
        }
        public salesordercart GettypeId(int id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                salesordercart model = salesorderrep.GetById(id);
                model.isdeleted = false;
                model.Status = 0;
                model.Deletedby = HttpContext.Current.Session["UserName"].ToString();
                model.Deleteddate = DateTime.Now;
                salesorderrep.Update(model);
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
