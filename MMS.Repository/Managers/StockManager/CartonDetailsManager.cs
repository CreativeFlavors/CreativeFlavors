using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class CartonDetailsManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<CartonDetails> CartonDetailsRepository;

        public CartonDetailsManager()
        {
            CartonDetailsRepository = unitOfWork.Repository<CartonDetails>();
        }

        public CartonDetails GetCartonDetailsByOrderEntryId(int OrderEntryId)
        {
            CartonDetails model = new CartonDetails();
            if (OrderEntryId != 0)
            {
                model = CartonDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).FirstOrDefault();
            }
            return model;
        }

        public bool Post(CartonDetails arg)
        {
            bool result = false;
            try
            {
                if (arg.CartonDetailsId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.UpdatedBy = username;
                    CartonDetailsRepository.Insert(arg);

                    result = true;
                }
                else
                {
                    CartonDetails model = CartonDetailsRepository.Table.Where(m => m.CartonDetailsId == arg.CartonDetailsId).FirstOrDefault();
                    model.CartonDetailsId = arg.CartonDetailsId;
                    model.PackType = arg.PackType;
                    model.NoOfCarton = arg.NoOfCarton;
                    model.OrderEntryId = arg.OrderEntryId;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    CartonDetailsRepository.Update(model);
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
                CartonDetails model = GetCartonDetailsByOrderEntryId(OrderEntryId);
                if (model != null)
                {
                    CartonDetailsRepository.Delete(model);
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
