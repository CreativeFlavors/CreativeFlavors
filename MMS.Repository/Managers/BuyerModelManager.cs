using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class BuyerModelManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<BuyerModel> buyerModelRepository;

        public BuyerModelManager()
        {
            buyerModelRepository = unitOfWork.Repository<BuyerModel>();
        }


        public bool Post(BuyerModel arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                buyerModelRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public BuyerModel GetBuyerModelID(int BuyerModelID)
        {
            BuyerModel buyerModel = new BuyerModel();
            buyerModel = buyerModelRepository.Table.Where(x => x.BuyerModelID == BuyerModelID).SingleOrDefault();
            return buyerModel;
        }
        public BuyerModel GetBuyerModel(string BuyerModel)
        {
            BuyerModel buyerModel = new BuyerModel();
            buyerModel = buyerModelRepository.Table.Where(x => x.BuyerModelName == BuyerModel).SingleOrDefault();
            return buyerModel;
        }


        public bool Put(BuyerModel arg)
        {
            bool result = false;
            try
            {
                BuyerModel model = buyerModelRepository.Table.Where(p => p.BuyerModelID == arg.BuyerModelID).FirstOrDefault();
                if (model != null)
                {
                    model.BuyerModelID = model.BuyerModelID;
                    model.Remarks = arg.Remarks;
                    model.BuyerModelName = arg.BuyerModelName;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    //model.CreatedBy = arg.CreatedBy;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    model.CreatedBy = username;
                    buyerModelRepository.Update(model);
                    result = true;
                }

                else
                {
                    result = false;
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
                BuyerModel model = buyerModelRepository.GetById(id);
                model.IsDeleted = true;
                model.DeletedBy = HttpContext.Current.Session["UserName"].ToString();
                model.DeletedDate = DateTime.Now;
                buyerModelRepository.Update(model);
                //  buyerModelRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public ColorMaster Get(int id)
        {
            return null;
        }

        public List<BuyerModel> Get()
        {
            List<BuyerModel> buyerModelList = new List<BuyerModel>();
            try
            {
                buyerModelList = buyerModelRepository.Table.Where(X => X.IsDeleted == false || X.IsDeleted == null).ToList<BuyerModel>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return buyerModelList;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }
    }
}
