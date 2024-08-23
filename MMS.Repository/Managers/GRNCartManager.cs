using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Mapping;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Repository.Managers
{
    public class GRNCartManager : IGRNCartServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GRNCart> GRNCartrep;
        public GRNCartManager()
        {
            GRNCartrep = unitOfWork.Repository<GRNCart>();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public List<GRNCarlist> GetGRNCsrtList(int poheader)
        {
            List<GRNCarlist> GRNCarlist = new List<GRNCarlist>();
            try
            {
                GRNCarlist = GRNCartrep.GRNCartlist(poheader);

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GRNCarlist;
        }
        public GRNCart POST(GRNCart arg)
        {
            GRNCart salesorder = new GRNCart();
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                arg.Status = 1;
                GRNCartrep.Insert(arg);
                salesorder = arg;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return salesorder;
        }
        public List<GRNCart> Get()
        {
            List<GRNCart> obj = new List<GRNCart>();
            obj = GRNCartrep.Table.ToList<GRNCart>();
            return obj;
        }
        public GRNCart GetSO(int? id)
        {
            try
            {
                GRNCart salesorder = new GRNCart();
                if (id != null)
                {
                    salesorder = GRNCartrep.Table.Where(x => x.grncartid == id && x.IsDeleted == true).FirstOrDefault();
                }
                return salesorder;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public GRNCart Getgrncartdt(int id)
        {
            try
            {
                GRNCart GRNCart = new GRNCart();
                if (id != null)
                {
                    GRNCart = GRNCartrep.Table.Where(x => x.podetailid == id && x.IsDeleted == true && x.Status == 1).FirstOrDefault();
                }
                return GRNCart;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<GRNCart> GetgrncartHD(int id,int grnno)
        {
            try
            {
                List<GRNCart> GRNCart = new List<GRNCart>();
                if (id != null)
                {
                    GRNCart = GRNCartrep.Table.Where(x => x.poheaderid == id && x.IsDeleted == true && x.Status ==1 && x.GRNNumber == grnno).ToList();
                }
                return GRNCart;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool Putstatussuccess(int GRNN0)
        {
            bool result = false;

            List<GRNCart> models = GRNCartrep.Table.Where(x => x.Status == 1 && x.IsDeleted == true && x.GRNNumber == GRNN0).ToList();

            if (models != null && models.Count > 0)
            {
                foreach (var model in models)
                {
                    model.Status = 2;
                    GRNCartrep.Update(model);
                }
                result = true;
            }
            else
            {
                return false;
            }

            return result;
        }
        public bool Putcancelsuccess()
        {
            bool result = false;

            List<GRNCart> models = GRNCartrep.Table.Where(x => x.Status == 1 && x.IsDeleted == true).ToList();

            if (models != null && models.Count > 0)
            {
                foreach (var model in models)
                {
                    model.Status = 0;
                    GRNCartrep.Update(model);
                }
                result = true;
            }
            else
            {
                return false;
            }

            return result;
        }
        public GRNCart Put(GRNCart arg)
        {
            GRNCart GRNCart = new GRNCart();
            GRNCart model = GRNCartrep.Table.Where(p => p.podetailid == arg.podetailid && p.Status == 1 && p.IsDeleted == true).FirstOrDefault();
            if (model != null)
            {
                model.Quantity = arg.Quantity;
                model.BatchCode = arg.BatchCode;
                model.Subtotal = arg.Subtotal;
                model.Grandtotal = arg.Grandtotal;
                model.TaxValue = arg.TaxValue;
                model.DiscountValue = arg.DiscountValue;
                model.ExpiryDate = arg.ExpiryDate;
                model.UpdatedDate = DateTime.Now;
                string username = "admin";

                model.UpdatedBy = username;
                GRNCartrep.Update(model);
                GRNCart = model;
            }
            return GRNCart;
        }

        public GRNCart Delete(int id)
        {
            GRNCart GRNCart = new GRNCart();
            try
            {
                GRNCart model = GRNCartrep.GetById(id);
                model.IsDeleted = false;
                model.DeletedBy = "admin";
                model.DeletedDate = DateTime.Now;
                GRNCartrep.Update(model);
                GRNCart = model;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return GRNCart;
        }
    }
}
