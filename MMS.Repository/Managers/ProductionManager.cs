using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.JobWork;
using MMS.Data;
using MMS.Data.Context;
using MMS.Data.Mapping;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class ProductionManager : IProductionServices,IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Production> productionrep;
        public ProductionManager()
        {
            productionrep = unitOfWork.Repository<Production>();

        }
        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool Post(Production arg)
        {
            bool result = false;
            try
            {
                string username = "admin";
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                productionrep.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(Production arg)
        {
            bool result = false;
            try
            {
                Production model = productionrep.Table.Where(p => p.ProductionId == arg.ProductionId).FirstOrDefault();
                if (model != null)
                {
                    model.ProductionId = arg.ProductionId;
                    model.ProductionDate = arg.ProductionDate;
                    model.ProductCode = arg.ProductCode;
                    model.ProductionCode = arg.ProductionCode;
                    model.ProductionQty = arg.ProductionQty;
                    model.ProductionStatus = arg.ProductionStatus;
                    model.ProductId = arg.ProductId;
                    model.MinQty = arg.MinQty;
                    model.MaxQty = arg.MaxQty;
                    model.RequiredQty = arg.RequiredQty;
                    model.StoreCode = arg.StoreCode;
                    model.ProductionPerDay = arg.ProductionPerDay;
                    model.ProductionDueDate = arg.ProductionDueDate;
                    model.ProductionFullfillDate = arg.ProductionFullfillDate;
                    model.RefDocNo = arg.RefDocNo;
                    model.RefDocDate = arg.RefDocDate;
                    model.Status1Code = arg.Status1Code;
                    model.Status1Date = arg.Status1Date;
                    model.Status1By = arg.Status1By;
                    model.Status2Code = arg.Status2Code;
                    model.Status2Date = arg.Status2Date;
                    model.Status2By = arg.Status2By;
                    model.Status3Code = arg.Status3Code;
                    model.Status3Date = arg.Status3Date;
                    model.Status3By = arg.Status3By;
                    model.Productions = arg.Productions;
                    model.SubAssembly = arg.SubAssembly;
                    model.Inprogress = arg.Inprogress;

                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = "";
                    string username = HttpContext.Current.Session["UserName"]?.ToString();
                    if (username != null)
                    {
                        model.UpdatedBy = username;
                    }
                    //model.UpdatedBy = username;
                    productionrep.Update(model);
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


        public Production Getproductionid(int? productionid)
        {
            Production productionlist = new Production();
            if (productionid != 0)
            {
                try
                {
                    productionlist = productionrep.Table.Where(x => x.ProductionId == productionid).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return productionlist;
        }

        public List<Production> Get_byCodes(int ProductionId)
        {
            List<Production> productions = new List<Production>();
            try
            {
                productions = productionrep.Table.Where(X => X.ProductionId== ProductionId).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return productions;
        }

        public List<Production> GetProductions()
        {
            List<Production> productions = new List<Production>();

            try
            {
                productions = productionrep.Table.ToList<Production>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return productions;
        }

      
        public string GetLatestBatchNumberFromDatabase()
        {
            string latestBatchNumber = null; // Initialize with a default value
            try
            {
                latestBatchNumber = productionrep.Table.Max(p => p.ProductionCode);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return latestBatchNumber;
        }
    }
}
