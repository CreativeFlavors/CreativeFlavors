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
    public class FinishedGoodManager : IFinishedGoodService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<FinishedGood> finishedgoodrep;
        public FinishedGoodManager()
        {
            finishedgoodrep = unitOfWork.Repository<FinishedGood>();

        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
        public bool Post(FinishedGood arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                finishedgoodrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(FinishedGood arg)
        {
            bool result = false;
            try
            {
                FinishedGood model = finishedgoodrep.Table.Where(f => f.Id == arg.Id).FirstOrDefault();
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
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"]?.ToString();
                    if (username != null)
                    {
                        model.UpdatedBy = username;
                    }
                    //model.UpdatedBy = username;
                    finishedgoodrep.Update(model);
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

        public FinishedGood GetByProductCode(string productcode)
        {
            FinishedGood finishedgoodlist = new FinishedGood();
            if (productcode != null)
            {
                try
                {
                    finishedgoodlist = finishedgoodrep.Table.Where(x => x.ProductCode == productcode).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return finishedgoodlist;
        }

    }
}
