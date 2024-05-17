using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
   public class OpeningStockPinCardManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<OpeningStockPinCard> OpeningStockPinCardManagerRepository;

        #region Add/Update/Delete Operation

        public bool Post(OpeningStockPinCard arg)
        {
            bool result = false;
            try
            {
                arg.CreatedDate = DateTime.Now;
                OpeningStockPinCardManagerRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(OpeningStockPinCard arg)
        {
            bool result = false;
            try
            {
                OpeningStockPinCard model = OpeningStockPinCardManagerRepository.Table.Where(p => p.MaterialOpeningStockID == arg.MaterialOpeningStockID).FirstOrDefault();
                if (model != null)
                {                   
                    model.materialmasterid = arg.materialmasterid;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.MaterialDescription = arg.MaterialDescription;
                    model.shortunitname = arg.shortunitname;
                    model.categoryname = arg.categoryname;
                    model.storename = arg.storename;
                    model.groupname = arg.groupname;
                    model.OpeningStockDate = arg.OpeningStockDate;
                    model.Rate = arg.Rate;
                    model.materialpcs = arg.materialpcs;
                    model.OpeningAsOnStock = arg.OpeningAsOnStock;
                    model.Issues = arg.Issues;
                    model.Receipt = arg.Receipt;
                    model.ClosingStock = arg.ClosingStock;
                    model.MaterialType = arg.MaterialType;
                    model.ClosingStockValue = arg.ClosingStockValue;
                    OpeningStockPinCardManagerRepository.Update(model);
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
                OpeningStockPinCard model = OpeningStockPinCardManagerRepository.GetById(id);              
                OpeningStockPinCardManagerRepository.Update(model);               
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

        public OpeningStockPinCardManager()
        {
            OpeningStockPinCardManagerRepository = unitOfWork.Repository<OpeningStockPinCard>();
        }       

        public List<OpeningStockPinCard> Get()
        {
            List<OpeningStockPinCard> openingStockPinCardlist = new List<OpeningStockPinCard>();
            try
            {
                openingStockPinCardlist = OpeningStockPinCardManagerRepository.Table.ToList<OpeningStockPinCard>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return openingStockPinCardlist;
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
