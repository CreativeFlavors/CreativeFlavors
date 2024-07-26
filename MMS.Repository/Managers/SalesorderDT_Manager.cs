using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
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
    public class SalesorderDT_Manager : ISalesorderDT_Services, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Salesorder_dt> salesorderhdrep;
        public SalesorderDT_Manager()
        {
            salesorderhdrep = unitOfWork.Repository<Salesorder_dt>();

        }
        public List<Salesorder_Grid> salesorder_Grid()
        {
            List<Salesorder_Grid> Salesorder_Grid = new List<Salesorder_Grid>();
            Salesorder_Grid = salesorderhdrep.Get_Salesorder_Grid();
            return Salesorder_Grid;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Salesorder_dt> Get()
        {
            List<Salesorder_dt> obj = new List<Salesorder_dt>();
            obj = salesorderhdrep.Table.ToList<Salesorder_dt>();
            return obj;
        }

        public Salesorder_dt GettypeId(int id)
        {
            throw new NotImplementedException();
        }
        public Salesorder_dt Post(Salesorder_dt arg)
        {
            Salesorder_dt parentbom_Material = new Salesorder_dt();
            bool result = false;
            try
            {
                string username = "admin";
                arg.createdby = username;
                arg.CreatedDate = DateTime.Now;
                salesorderhdrep.Insert(arg);
                parentbom_Material = arg;
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return parentbom_Material;
        }
        public List<mrp_material_list> GetmrpmaterialList(int productid)
        {
            List<mrp_material_list> MRP_Details = new List<mrp_material_list>();
            try
            {
                MRP_Details = salesorderhdrep.mrpMaterialList1(productid);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return MRP_Details;
        }
        public Salesorder_dt GetSO(int? id)
        {
            Salesorder_dt salesorder = new Salesorder_dt();
            if (id != null)
            {
                salesorder = salesorderhdrep.Table.Where(x => x.Salesorderid_dt == id).FirstOrDefault();
            }
            return salesorder;
        } 
        public Salesorder_dt Getproductid(int? id)
        {
            Salesorder_dt salesorder = new Salesorder_dt();
            if (id != null)
            {
                salesorder = salesorderhdrep.Table.Where(x => x.productid == id).FirstOrDefault();
            }
            return salesorder;
        }
        public bool Put(Salesorder_dt arg)
        {
            bool result = false;
            try
            {
                Salesorder_dt model = salesorderhdrep.Table.Where(p => p.Salesorderid_dt == arg.Salesorderid_dt).FirstOrDefault();
                if (model != null)
                {
                    model.dc_qty = arg.dc_qty;
                    model.UpdatedDate = DateTime.Now;
                    string username = "admin";
                    model.Updatedby = username;
                    salesorderhdrep.Update(model);
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
        public bool update(Salesorder_dt arg)
        {
            bool result = false;
            try
            {
                Salesorder_dt model = salesorderhdrep.Table.Where(p => p.Salesorderid_dt == arg.Salesorderid_dt).FirstOrDefault();
                if (model != null)
                {
                    model.Invoice_qty = arg.Invoice_qty;
                    model.UpdatedDate = DateTime.Now;
                    string username = "admin";
                    model.Updatedby = username;
                    salesorderhdrep.Update(model);
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
        public List<Salesorder_dt> GetSOIdS(int id)
        {
            List<Salesorder_dt> salesorders = new List<Salesorder_dt>();
            salesorders = salesorderhdrep.Table.Where(x => x.Salesorderid_hd == id).ToList();
            return salesorders;
        }
    }
}
