using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class RejectionReturnManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<RejectionReturnSup> RejectionReturnSupRepository;

        #region Add/Update/Delete Operation

        public bool Post(RejectionReturnSup arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                RejectionReturnSupRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(RejectionReturnSup arg)
        {
            bool result = false;
            try
            {
                RejectionReturnSup model = RejectionReturnSupRepository.Table.Where(m => m.RejectionReturnId == arg.RejectionReturnId).FirstOrDefault();
                if (model != null)
                {
                    model.RejectionDcNo = arg.RejectionDcNo;
                    model.GrnNo = arg.GrnNo;
                    model.Date = arg.Date;
                    model.PoNo = arg.PoNo;
                    model.SupplierMasterId = arg.SupplierMasterId;
                    model.IMIRNo = arg.IMIRNo;
                    model.MaterialGroupId = arg.MaterialGroupId;
                    model.Uom = arg.Uom;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.Quantity = arg.Quantity;
                    model.ColourMasterId = arg.ColourMasterId;
                    model.Rate = arg.Rate;
                    model.Remarks = arg.Remarks;
                    model.Pcs = arg.Pcs;
                    model.AmountTotal = arg.AmountTotal;
                    model.GatePassDc = arg.GatePassDc;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    RejectionReturnSupRepository.Update(model);
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

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                RejectionReturnSup model = RejectionReturnSupRepository.GetById(id);
                RejectionReturnSupRepository.Delete(model);
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

        #region Helper Method
        public RejectionReturnManager()
        {
            RejectionReturnSupRepository = unitOfWork.Repository<RejectionReturnSup>();
        }

        public RejectionReturnSup GetRejectionReturnId(int RejectionReturnId)
        {
            RejectionReturnSup rejectionReturnSup = new RejectionReturnSup();
            if (RejectionReturnId != 0)
            {
                rejectionReturnSup = RejectionReturnSupRepository.Table.Where(x => x.RejectionReturnId == RejectionReturnId).SingleOrDefault();

            }
            return rejectionReturnSup;
        }

        public RejectionReturnSup Get(int id)
        {
            return null;
        }

        public List<RejectionReturnSup> Get()
        {
            List<RejectionReturnSup> rejectionReturnSup = new List<RejectionReturnSup>();
            try
            {
                rejectionReturnSup = RejectionReturnSupRepository.Table.ToList<RejectionReturnSup>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return rejectionReturnSup;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        #endregion
    }
}
