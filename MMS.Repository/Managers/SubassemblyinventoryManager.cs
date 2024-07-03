using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class SubassemblyinventoryManager : ISubassemblyinventoryServices, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Subassemblyinventory> subassemblyinventoryrep;
        public SubassemblyinventoryManager()
        {
            subassemblyinventoryrep = unitOfWork.Repository<Subassemblyinventory>();

        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        public bool Post(Subassemblyinventory arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                subassemblyinventoryrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Put(Subassemblyinventory arg)
        {
            bool result = false;
            try
            {
                Subassemblyinventory model = subassemblyinventoryrep.Table.Where(f => f.Id == arg.Id).FirstOrDefault();
                if (model != null)
                {
                    model.Id = arg.Id;
                    model.ProductCode = arg.ProductCode;
                    model.Price = arg.Price;
                    model.Cost = arg.Cost;
                    model.StoreCode = arg.StoreCode;
                    model.Quantity = arg.Quantity;
                    model.QuantityLock = arg.QuantityLock;
                    model.QuantityLockRefTime = arg.QuantityLockRefTime;
                    model.QuantityLockReleaseAt = arg.QuantityLockReleaseAt;
                    model.PurchaseUOM = arg.PurchaseUOM;
                    model.SaleUOM = arg.SaleUOM;
                    model.LastTransNo = arg.LastTransNo;
                    model.LastTransQty = arg.LastTransQty;
                    model.LastTransDate = arg.LastTransDate;
                    model.Batchcode = arg.Batchcode;
                    model.ProductType = arg.ProductType;
                    model.ProductId = arg.ProductId;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"]?.ToString();
                    if (username != null)
                    {
                        model.UpdatedBy = username;
                    }
                    //model.UpdatedBy = username;
                    subassemblyinventoryrep.Update(model);
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
        public Subassemblyinventory GetByProductCode(string productcode)
        {
            Subassemblyinventory subassemblyproductionlist = new Subassemblyinventory();
            if (productcode != null)
            {
                try
                {
                    subassemblyproductionlist = subassemblyinventoryrep.Table.Where(x => x.ProductCode == productcode).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return subassemblyproductionlist;
        }


    }
}
