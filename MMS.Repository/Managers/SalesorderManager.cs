using MMS.Common;
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
using System.Web.UI.WebControls;

namespace MMS.Repository.Managers
{
    public class SalesorderManager : ISalesorder, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<salesorder> salesorderrep;

        public SalesorderManager()
        {
            salesorderrep = unitOfWork.Repository<salesorder>();

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public decimal Getcurrencyconversion(string p_primarycurrency, string p_secondarycurrency, string todaydate)
        {
            decimal val = 0;
            val = salesorderrep.Getcurrencyconversion(p_primarycurrency, p_secondarycurrency, todaydate);
            return val;
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
        public salesorder Post(salesorder arg)
        {
            salesorder salesorder  = new salesorder();
            try
            {
                string username = "admin";
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
    
        public List<SalesorderCart> GetsalesorderList(int customerid)
        {
            List<SalesorderCart> salesorder = new List<SalesorderCart>();
            try
            {
                salesorder = salesorderrep.SearchSalesordercart(customerid);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<salesorder> GetsalesorderCartList(int customerid)
        {
            List<salesorder> salesorder = new List<salesorder>();
                 salesorder = salesorderrep.Table.Where(x => x.customerid== customerid && x.Status == 1 && x.isdeleted == true).ToList();
            return salesorder;
        }
        public bool Putstatus()
        {
            bool result = false;

                List<salesorder> models = salesorderrep.Table.Where(x => x.Status == 1 && x.isdeleted == true).ToList();

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

            List<salesorder> models = salesorderrep.Table.Where(x => x.Status == 1 && x.isdeleted == true).ToList();

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

        public List<salesorder> Get()
        {
            List<salesorder> obj = new List<salesorder>();
            obj = salesorderrep.Table.ToList<salesorder>();
            return obj;
        }
        public salesorder GetSO(int? id)
        {
            salesorder salesorder = new salesorder();
            if (id != null)
            {
                salesorder = salesorderrep.Table.Where(x => x.SalesorderId == id && x.isdeleted == true).FirstOrDefault();
            }
            return salesorder;
        }

        public salesorder Getproductqty(int productid)
        {
            salesorder salesorder = new salesorder();
            if (productid != null)
            {
                salesorder = salesorderrep.Table.Where(x => x.ProductNameid == productid).FirstOrDefault();
            }
            return salesorder;
        }
        public salesorder GettypeId(int id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                salesorder model = salesorderrep.GetById(id);
                model.isdeleted = false;
                model.Deletedby = "admin";
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
