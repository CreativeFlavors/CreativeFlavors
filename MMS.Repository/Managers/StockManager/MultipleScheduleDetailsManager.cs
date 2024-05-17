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
    public class MultipleScheduleDetailsManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<MultipleScheduleDetails> MultipleScheduleDetailsRepository;

        public MultipleScheduleDetailsManager()
        {
            MultipleScheduleDetailsRepository = unitOfWork.Repository<MultipleScheduleDetails>();
        }

        public List<MultipleScheduleDetails> GetMultipleScheduleDetailsByOrderEntryId(int OrderEntryId)  
        {
            List<MultipleScheduleDetails> model = new List<MultipleScheduleDetails>();
            if (OrderEntryId != 0)
            {
                model = MultipleScheduleDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).ToList();
            }
            return model;
        }

        public bool Post(MultipleScheduleDetails arg)
        {
            bool result = false;
            try
            {
                if (arg.MultipleScheduleDetailsId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.UpdatedBy = username;
                    MultipleScheduleDetailsRepository.Insert(arg);

                    result = true;
                }
                else
                {
                    MultipleScheduleDetails model = MultipleScheduleDetailsRepository.Table.Where(m => m.MultipleScheduleDetailsId == arg.MultipleScheduleDetailsId).FirstOrDefault();
                    model.MultipleScheduleDetailsId = arg.MultipleScheduleDetailsId;                   
                    model.Io = arg.Io;
                    model.Size = arg.Size;
                    model.Qty = arg.Qty;
                    model.ExFDt = arg.ExFDt;
                    model.OrderEntryId = arg.OrderEntryId;                   
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    MultipleScheduleDetailsRepository.Update(model);
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
                List<MultipleScheduleDetails> model = GetMultipleScheduleDetailsByOrderEntryId(OrderEntryId);
                foreach (var item in model)
                {
                    MultipleScheduleDetailsRepository.Delete(item);
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
