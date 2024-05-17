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
    public class InternalOrderFormManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<OrderEntryForm> BuyerOrdeEntryrRepository;
        private Repository<OePackingDetails> OePackingDetailsRepository;
        private Repository<OeShipmentDetails> OeShipmentDetailsRepository;
        private Repository<OeOtherDetails> OeOtherDetailsRepository;

        #region Helper Method
        public InternalOrderFormManager()
        {
            BuyerOrdeEntryrRepository = unitOfWork.Repository<OrderEntryForm>();
            OeOtherDetailsRepository = unitOfWork.Repository<OeOtherDetails>();
            OePackingDetailsRepository = unitOfWork.Repository<OePackingDetails>();
            OeShipmentDetailsRepository = unitOfWork.Repository<OeShipmentDetails>();
        }
        public PurchaseOrder Get(int id)
        {
            return null;
        }
        public List<OrderEntryForm> Get()
        {
            List<OrderEntryForm> BuyerOrderEntryList = new List<OrderEntryForm>();
            try
            {
                BuyerOrderEntryList = BuyerOrdeEntryrRepository.Table.ToList<OrderEntryForm>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BuyerOrderEntryList;
        }

        public OrderEntryForm GetOrderEntryId(int OrderEntryId)
        {
            OrderEntryForm model = new OrderEntryForm();
            if (OrderEntryId != 0)
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).SingleOrDefault();
            }
            return model;
        }

        public List<OePackingDetails> GetOePackingDetails(int OrderEntryId)
        {
            List<OePackingDetails> model = new List<OePackingDetails>();
            if (OrderEntryId != 0)
            {
                model = OePackingDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).ToList();
            }
            return model;
        }

        public OeShipmentDetails GetOeShipmentDetails(int OrderEntryId)
        {
            OeShipmentDetails model = new OeShipmentDetails();
            if (OrderEntryId != 0)
            {
                model = OeShipmentDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).SingleOrDefault();
            }
            return model;
        }

        public OeOtherDetails GetOeOtherDetails(int OrderEntryId)
        {
            OeOtherDetails model = new OeOtherDetails();
            if (OrderEntryId != 0)
            {
                model = OeOtherDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).SingleOrDefault();
            }
            return model;
        }
        public OrderEntryForm GetBuyerOderPoNo(string BuyerPONo)
        {
            OrderEntryForm model = new OrderEntryForm();
            if (BuyerPONo != "")
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerPoNo == BuyerPONo).SingleOrDefault();
            }
            return model;
        }
        #endregion

        #region Curd Operation
        public int Post(OrderEntryForm arg)
        {
            int result = 0;
            try
            {
                if (arg.OrderEntryId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    //arg.UpdatedBy = "";                    
                    BuyerOrdeEntryrRepository.Insert(arg);

                    result = arg.OrderEntryId;
                }
                else
                {
                    OrderEntryForm model = BuyerOrdeEntryrRepository.Table.Where(m => m.OrderEntryId == arg.OrderEntryId).FirstOrDefault();
                    model.OrderEntryId = arg.OrderEntryId;
                    model.BuyerOrderSlNo = arg.BuyerOrderSlNo;
                    model.LotNo = arg.LotNo;
                    model.Count = arg.Count;
                    //model.FreightAmtCurrency = arg.FreightAmtCurrency;
                    model.WeekNo = arg.WeekNo;
                    model.Date = arg.Date;
                    model.BuyerSeason = arg.BuyerSeason;
                    model.BuyerName = arg.BuyerName;
                    model.OrderProjectionNo = arg.OrderProjectionNo;
                    model.BuyerPoNo = arg.BuyerPoNo;
                    model.OurStyle = arg.OurStyle;
                    model.LeatherDescription = arg.LeatherDescription;
                    model.DiscountPer = arg.DiscountPer;
                    model.QuoteNo = arg.QuoteNo;
                    model.CountryStamp = arg.CountryStamp;
                    model.CustomerPlan = arg.CustomerPlan;
                    model.CustomerDate = arg.CustomerDate;
                    model.AgentMasterId = arg.AgentMasterId;
                    model.CommPer = arg.CommPer;
                    model.ExFactoryDate = arg.ExFactoryDate;
                    model.ShipmentMode = arg.ShipmentMode;
                    model.SampleReqNo = arg.SampleReqNo;
                    model.Brand = arg.Brand;
                    model.BuyerStyleNo = arg.BuyerStyleNo;
                    model.BarCodeNo = arg.BarCodeNo;
                    model.BomNo = arg.BomNo;
                    model.Last = arg.Last;
                    model.ColorMasterId = arg.ColorMasterId;
                    model.FinishedProdType = arg.FinishedProdType;
                    model.ProductTypeId = arg.ProductTypeId;
                    model.AmendmentNoWithDate = arg.AmendmentNoWithDate;
                    model.TotalOrderForWeek = arg.TotalOrderForWeek;
                    model.OrderType = arg.OrderType;
                    model.Currency = arg.Currency;
                    model.Parties = arg.Parties;
                    model.GradeMasterId = arg.GradeMasterId;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.Remarks1 = arg.Remarks1;
                    model.Remarks2 = arg.Remarks2;
                    model.LineNo_1 = arg.LineNo_1;
                    model.PartiesAmount1 = arg.PartiesAmount1;
                    model.ShortUnitID = arg.ShortUnitID;
                    model.PartiesAmount2 = arg.PartiesAmount2;
                    model.LongUnitID = arg.LongUnitID;
                    model.IsBuyer = arg.IsBuyer;
                    model.IsInternal = arg.IsInternal;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    model.TotalAmount = arg.TotalAmount;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    BuyerOrdeEntryrRepository.Update(model);
                    result = arg.OrderEntryId;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;
        }

        public bool Delete(int OrderEntryId)
        {
            bool result = false;
            try
            {
                OrderEntryForm model = BuyerOrdeEntryrRepository.GetById(OrderEntryId);
                model.IsDeleted = true;
                BuyerOrdeEntryrRepository.Update(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public bool Post_OePackingDetails(OePackingDetails arg)
        {
            bool result = false;
            try
            {
                if (arg.OePackingDetailsId == 0)
                {
                    //string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedDate = DateTime.Now;
                    OePackingDetailsRepository.Insert(arg);

                    result = true;
                }
                else
                {
                    OePackingDetails model = OePackingDetailsRepository.Table.Where(m => m.OePackingDetailsId == arg.OePackingDetailsId).FirstOrDefault();
                    model.OePackingDetailsId = arg.OePackingDetailsId;
                    model.PackingType = arg.PackingType;
                    model.SizeRangeMasterId = arg.SizeRangeMasterId;
                    model.Size = arg.Size;
                    model.OrderEntryId = arg.OrderEntryId;
                    model.UpdatedDate = DateTime.Now;
                    OePackingDetailsRepository.Update(model);
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

        public bool Delete_OePackingDetails(int OrderEntryId)
        {
            bool result = false;
            try
            {
                List<OePackingDetails> model = OePackingDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).ToList();
                foreach (var item in model)
                {
                    OePackingDetailsRepository.Delete(item);
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

        public bool Post_OeShipmentDetails(OeShipmentDetails arg)
        {
            bool result = false;
            try
            {
                if (arg.OeShipmentDetailsId == 0)
                {
                    arg.CreatedDate = DateTime.Now;
                    OeShipmentDetailsRepository.Insert(arg);

                    result = true;
                }
                else
                {
                    OeShipmentDetails model = OeShipmentDetailsRepository.Table.Where(m => m.OeShipmentDetailsId == arg.OeShipmentDetailsId).FirstOrDefault();
                    model.OeShipmentDetailsId = arg.OeShipmentDetailsId;
                    model.CountryStamp = arg.CountryStamp;
                    model.ShipmentFrom = arg.ShipmentFrom;
                    model.ShipmentTo = arg.ShipmentTo;
                    model.OtherInstruction = arg.OtherInstruction;
                    model.OrderEntryId = arg.OrderEntryId;
                    model.UpdatedDate = DateTime.Now;
                    OeShipmentDetailsRepository.Update(model);
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

        public bool Delete_OeShipmentDetails(int OrderEntryId)
        {
            bool result = false;
            try
            {
                OeShipmentDetails model = OeShipmentDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).FirstOrDefault();
                OeShipmentDetailsRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public bool Post_OeOtherDetails(OeOtherDetails arg)
        {
            bool result = false;
            try
            {
                if (arg.OeOtherDetailsId == 0)
                {
                    // string username = HttpContext.Current.Session["UserName"].ToString();               
                    arg.CreatedDate = DateTime.Now;
                    OeOtherDetailsRepository.Insert(arg);

                    result = true;
                }
                else
                {
                    OeOtherDetails model = OeOtherDetailsRepository.Table.Where(m => m.OeOtherDetailsId == arg.OeOtherDetailsId).FirstOrDefault();
                    model.OeOtherDetailsId = arg.OeOtherDetailsId;
                    model.PaymentTerms = arg.PaymentTerms;
                    model.Insurance = arg.Insurance;
                    model.DeliverTo = arg.DeliverTo;
                    //model.FreightAmtCurrency = arg.FreightAmtCurrency;
                    model.SpecialInstructions = arg.SpecialInstructions;
                    model.PackingOrLabelling = arg.PackingOrLabelling;
                    model.OrderEntryId = arg.OrderEntryId;
                    model.UpdatedDate = DateTime.Now;
                    OeOtherDetailsRepository.Update(model);
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

        public bool Delete_OeOtherDetails(int OrderEntryId)
        {
            bool result = false;
            try
            {
                OeOtherDetails model = OeOtherDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).FirstOrDefault();
                OeOtherDetailsRepository.Delete(model);
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

    }
}
