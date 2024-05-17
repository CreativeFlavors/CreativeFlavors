using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class SizeRangeQtyRateManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<SizeRangeQtyRate> SizeRangeQtyRaterRepository;

        public SizeRangeQtyRateManager()
        {
            SizeRangeQtyRaterRepository = unitOfWork.Repository<SizeRangeQtyRate>();
        }

        public List<SizeRangeQtyRate> Get()
        {
            List<SizeRangeQtyRate> sizeRangeQtyRate = new List<SizeRangeQtyRate>();
            try
            {
                sizeRangeQtyRate = SizeRangeQtyRaterRepository.Table.ToList<SizeRangeQtyRate>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return sizeRangeQtyRate;
        }

        public List<SizeRangeQtyRate> GetSizeRangeByOrderEntryId(int OrderEntryId) 
        {
            List<SizeRangeQtyRate> model = new List<SizeRangeQtyRate>();
            if (OrderEntryId != 0)
            {
                model = SizeRangeQtyRaterRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).ToList();
            }
            return model;
        }
        public decimal? GetSizeRange(string sizerange, int OrderEnterID)
        {
            SizeRangeQtyRate model = new SizeRangeQtyRate();
            if (sizerange != "")
            {
                model = SizeRangeQtyRaterRepository.Table.Where(x => x.SizeRange == sizerange && x.OrderEntryId == OrderEnterID).FirstOrDefault();
            }
            decimal? qty = 0;
            if (model != null)
            {
                qty = model.Qty;
            }
            else
            {
                qty = 0;
            }
            return qty;
        }
        public bool Post_(SizeRangeQtyRate arg)
        {
            bool result = false;
            try
            {
                if (arg.SizeQRId == 0)
                {
                    SizeRangeQtyRate SizeRangeQtyRate = new SizeRangeQtyRate();
                    SizeRangeQtyRate = arg;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                   // arg.UpdatedBy = username;
                    SizeRangeQtyRaterRepository.Insert(arg);
                    result = true;   
                }
               
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public bool Post(SizeRangeQtyRate arg)
        {
            bool result = false;
            try
            {
                if (arg.SizeQRId == 0)
                {
                    SizeRangeQtyRate sizeRangeQtyrate = new SizeRangeQtyRate();
                    sizeRangeQtyrate = arg;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;               
                    SizeRangeQtyRaterRepository.Insert(sizeRangeQtyrate);
                    result = true;
                }
                else
                {
                    SizeRangeQtyRate model = SizeRangeQtyRaterRepository.Table.Where(m => m.SizeQRId == arg.SizeQRId).FirstOrDefault();
                    model.SizeQRId = arg.SizeQRId;
                    model.Qty = arg.Qty;
                    model.SizeRange = arg.SizeRange;
                    model.OrderEntryId = arg.OrderEntryId;                   
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    SizeRangeQtyRaterRepository.Update(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Delete(int OrderEntryId)
        {
            bool result = false;
            try
            {
                List<SizeRangeQtyRate> model = GetSizeRangeByOrderEntryId(OrderEntryId);
                foreach (var item in model)
                {
                    SizeRangeQtyRaterRepository.Delete(item);
                }
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
