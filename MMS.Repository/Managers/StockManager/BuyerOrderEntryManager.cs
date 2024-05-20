using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class BuyerOrderEntryManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<OrderEntry> BuyerOrdeEntryrRepository;
        private Repository<OePackingDetails> OePackingDetailsRepository;
        private Repository<OeShipmentDetails> OeShipmentDetailsRepository;
        private Repository<OeOtherDetails> OeOtherDetailsRepository;
        //private Repository<InvoiceDetails> InvoiceDetailsRepository;

        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();

        #region Helper Method
        public BuyerOrderEntryManager()
        {
            BuyerOrdeEntryrRepository = unitOfWork.Repository<OrderEntry>();
            OeOtherDetailsRepository = unitOfWork.Repository<OeOtherDetails>();
            OePackingDetailsRepository = unitOfWork.Repository<OePackingDetails>();
            OeShipmentDetailsRepository = unitOfWork.Repository<OeShipmentDetails>();
            //InvoiceDetailsRepository = unitOfWork.Repository<InvoiceDetails>();
        }
        public PurchaseOrder Get(int id)
        {
            return null;
        }
        public List<OrderEntry> GetInternalIO()
        {
            List<OrderEntry> BuyerOrderEntryList = new List<OrderEntry>();
            try
            {

                BuyerOrderEntryList = BuyerOrdeEntryrRepository.Table.ToList<OrderEntry>().Where(x => x.IsDeleted == false && x.IsInternal == true).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BuyerOrderEntryList;
        }
        public List<SizeRangeQtyRate> GetAllOrderSize()
        {
            List<SizeRangeQtyRate> SizeRangeQtyRatelist = new List<SizeRangeQtyRate>();
            try
            {
                SizeRangeQtyRatelist = BuyerOrdeEntryrRepository.GetAllOrderSize();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return SizeRangeQtyRatelist;
        }
        public List<SizeRangeQtyRate> GetAllOrderSize_OrderEntryId(string OrderEntryId)
        {
            List<SizeRangeQtyRate> SizeRangeQtyRatelist = new List<SizeRangeQtyRate>();
            try
            {
                SizeRangeQtyRatelist = BuyerOrdeEntryrRepository.GetAllOrderSize_ProductionJob(OrderEntryId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return SizeRangeQtyRatelist;
        }
        public List<OrderEntry> GetInternalIOWeek(string WeekNo)
        {
            List<OrderEntry> BuyerOrderEntryList = new List<OrderEntry>();
            try
            {
                BuyerOrderEntryList = BuyerOrdeEntryrRepository.Table.ToList<OrderEntry>().Where(x => x.IsDeleted == false && x.IsInternal == true && x.WeekNo == WeekNo).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BuyerOrderEntryList;
        }
        public List<OrderEntry> GetInternalIOSeason(string WeekNo)
        {
            List<OrderEntry> BuyerOrderEntryList = new List<OrderEntry>();
            try
            {
                BuyerOrderEntryList = BuyerOrdeEntryrRepository.Table.ToList<OrderEntry>().Where(x => x.IsDeleted == false && x.IsInternal == true && x.WeekNo == WeekNo).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BuyerOrderEntryList;
        }
        public List<OrderEntry> Get()
        {
            List<OrderEntry> BuyerOrderEntryList = new List<OrderEntry>();
            try
            {
                BuyerOrderEntryList = BuyerOrdeEntryrRepository.Table.ToList<OrderEntry>().Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return BuyerOrderEntryList;
        }

        public OrderEntry GetOrderEntryId(int OrderEntryId)
        {
            OrderEntry model = new OrderEntry();
            if (OrderEntryId != 0)
            {
                model = BuyerOrdeEntryrRepository.GetOrderEntryId(OrderEntryId);
            }
            return model;
        }
        public OrderEntry GetOrderEntryIdbyFilter(int OrderEntryId, int processstatus)
        {
            OrderEntry model = new OrderEntry();
            if (OrderEntryId != 0)
            {
                model = BuyerOrdeEntryrRepository.GetOrderEntryIdbyFilter(OrderEntryId, processstatus);
            }
            return model;
        }

        //public InternalOrderForm GetInteranlOrderEntryId(int OrderEntryId)
        //{
        //    InternalOrderForm model = new InternalOrderForm();
        //    if (OrderEntryId != 0)
        //    {
        //        model = BuyerorderIntenalDetailsRepository.Table.Where(x => x.OrderEntryId == OrderEntryId && x.IsDeleted == false && x.IsInternal == true).FirstOrDefault();
        //    }
        //    return model;
        //}
        //public InternalOrderForm GetIntenalBuyerOderSlNo(string BuyerPONo)
        //{
        //    InternalOrderForm model = new InternalOrderForm();
        //    if (BuyerPONo != "" && BuyerPONo != null)
        //    {
        //        model = BuyerorderIntenalDetailsRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerPONo && x.IsInternal == true && x.IsDeleted == false).FirstOrDefault();
        //    }
        //    return model;
        //}
        public OrderEntry GetBuyerOrderSlNo(string BuyerOrderSlNo)
        {
            OrderEntry model = new OrderEntry();
            if (BuyerOrderSlNo != "")
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerOrderSlNo && x.IsBuyer == true && x.IsDeleted == false).FirstOrDefault();
            }
            return model;
        }
        public OrderEntry GetBuyerOrderSlNoWihLot(string BuyerOrderSlNo, string LotNo)
        {
            OrderEntry model = new OrderEntry();
            if (BuyerOrderSlNo != "")
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerOrderSlNo && x.IsBuyer == true && x.IsDeleted == false && x.LotNo == LotNo).FirstOrDefault();
            }
            return model;
        }
        public OrderEntry GetBuyerOrderDetails(string BuyerOrderSlNo, int? Season, int? Buyername)
        {
            OrderEntry model = new OrderEntry();
            if (BuyerOrderSlNo != "")
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerOrderSlNo && x.IsBuyer == true && x.IsDeleted == false && x.BuyerSeason == Season && x.BuyerName == Buyername).FirstOrDefault();
            }
            return model;
        }
        public OrderEntry BuyerOrderExitNo(string BuyerOrderSlNo, string Season, string Buyer)
        {
            OrderEntry model_ = new OrderEntry();
            if (BuyerOrderSlNo != "")
            {
                model_ = BuyerOrdeEntryrRepository.Table.ToList().Where(x => x.BuyerOrderSlNo == BuyerOrderSlNo && x.IsDeleted == false && x.IsBuyer == true && x.BuyerSeason == Convert.ToInt32(Season) && x.BuyerName == Convert.ToInt32(Buyer)).FirstOrDefault();
            }
            return model_;
        }
        public OrderEntry InternalOrderExitNo(string BuyerOrderSlNo, string Season, string Buyer)
        {
            OrderEntry model_ = new OrderEntry();
            if (BuyerOrderSlNo != "")
            {
                model_ = BuyerOrdeEntryrRepository.Table.ToList().Where(x => x.BuyerOrderSlNo == BuyerOrderSlNo && x.IsDeleted == false && x.IsInternal == true && x.BuyerSeason == Convert.ToInt32(Season) && x.BuyerName == Convert.ToInt32(Buyer)).FirstOrDefault();
            }
            return model_;
        }
        public List<OrderEntry> isExistBuyerOrderSlNo(string BuyerOrderSlNo)
        {
            List<OrderEntry> model_ = new List<OrderEntry>();
            if (BuyerOrderSlNo != "")
            {

                model_ = BuyerOrdeEntryrRepository.Table.ToList().Where(x => x.BuyerOrderSlNo == BuyerOrderSlNo && x.IsDeleted == false && x.IsBuyer == true).ToList();
            }
            return model_;
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
        public OrderEntry GetBuyerOderPoNo(string BuyerPONo)
        {
            OrderEntry model = new OrderEntry();
            if (BuyerPONo != "")
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerPONo).FirstOrDefault();
            }
            return model;
        }
        public OrderEntry GetBuyerOderSlNo(string BuyerPONo)
        {
            OrderEntry model = new OrderEntry();
            if (BuyerPONo != "" && BuyerPONo != null)
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerPONo && x.IsInternal == true && x.IsDeleted == false).FirstOrDefault();
            }
            return model;
        }

        public List<OrderEntry> GetBuyer_Production_style()
        {

            List<OrderEntry> model = new List<OrderEntry>();
            try
            {

                model = BuyerOrdeEntryrRepository.Table.Where(x => x.IsBuyer == true && x.IsInternal == true && x.IsDeleted == false).ToList();

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return model;
        }
        public OrderEntry GetBuyerOderSlNo_withSeason(string BuyerPONo, string Lotno, int? season)
        {
            OrderEntry model = new OrderEntry();
            if (BuyerPONo != "" && BuyerPONo != null)
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerPONo && x.IsInternal == true && x.BuyerSeason == season && x.LotNo == Lotno && x.IsDeleted == false).FirstOrDefault();
            }
            return model;
        }
        public List<OrderEntry> LotNumberWithOrder(string LotNo, string Season)
        {
            List<OrderEntry> model_ = new List<OrderEntry>();
            if (LotNo != "")
            {

                model_ = BuyerOrdeEntryrRepository.Table.ToList().Where(x => x.LotNo == LotNo && x.BuyerSeason == Convert.ToInt32(Season) && x.IsDeleted == false).ToList();
            }
            return model_;
        }
        public List<OrderEntry> GetLotNumberWithBuyer(string LotNo)
        {
            List<OrderEntry> model_ = new List<OrderEntry>();
            if (LotNo != "")
            {

                model_ = BuyerOrdeEntryrRepository.Table.ToList().Where(x => x.LotNo == LotNo && x.IsDeleted == false && x.IsBuyer == true).ToList();
            }
            return model_;
        }
        public List<OrderEntry> GetLotNumberWithSeason(string LotNo, string Season)
        {
            List<OrderEntry> model_ = new List<OrderEntry>();
            if (LotNo != "")
            {

                model_ = BuyerOrdeEntryrRepository.Table.ToList().Where(x => x.LotNo == LotNo && x.BuyerSeason == Convert.ToInt32(Season) && x.IsDeleted == false && x.IsBuyer == true).ToList();
            }
            return model_;
        }
        public OrderEntry GetBuyerOderSlNoWithmaterial(string BuyerPONo, int Material)
        {
            OrderEntry model = new OrderEntry();
            if (BuyerPONo != "")
            {
                model = BuyerOrdeEntryrRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerPONo && x.IsInternal == true && x.IsDeleted == false && x.LeatherDescription == Material.ToString()).SingleOrDefault();
            }
            return model;
        }
        #endregion
        public OrderEntry GetBuyersino(int Buyersino)
        {
            OrderEntry buyerMaster = new OrderEntry();
            if (Buyersino > 0)
            {
                buyerMaster = BuyerOrdeEntryrRepository.Table.Where(x => x.OrderEntryId == Buyersino).FirstOrDefault();
            }
            return buyerMaster;
        }

        public List<OrderEntry> GetIntenalOrderGrid(string BuyerSlipperNo)
        {
            List<OrderEntry> contactlist = new List<OrderEntry>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.SearchInternalOrderList(BuyerSlipperNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }

        //public InternalOrderForm GetIntenalBuyerOrderDetails(string BuyerOrderSlNo, int? Season, int? Buyername)
        //{
        //    InternalOrderForm model = new InternalOrderForm();
        //    if (BuyerOrderSlNo != "")
        //    {
        //        model = BuyerorderIntenalDetailsRepository.Table.Where(x => x.BuyerOrderSlNo == BuyerOrderSlNo && x.IsBuyer == true && x.IsDeleted == false && x.BuyerSeason == Season && x.BuyerName == Buyername).FirstOrDefault();
        //    }
        //    return model;
        //}

        public List<OrderEntry> GetBuyerOrderGrid(string BuyerSlipperNo)
        {
            List<OrderEntry> contactlist = new List<OrderEntry>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.SearchBuyerOrderList(BuyerSlipperNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }

        public List<GetBuyerOrderlist> GetBuyerOrderlist(int buyername)
        {
            List<GetBuyerOrderlist> contactlist = new List<GetBuyerOrderlist>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.getbuyOrderList(buyername);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        public List<GetBuyerOrderlist> getbuyOrderListdropdown()
        {
            List<GetBuyerOrderlist> contactlist = new List<GetBuyerOrderlist>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.getbuyOrderListdropdown();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        public List<getbuyerorderaddressdetails> getbuyerorderaddressdetails(int orderid)
        {
            List<getbuyerorderaddressdetails> contactlist = new List<getbuyerorderaddressdetails>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.getbuyerorderaddressdetails(orderid);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        #region Curd Operation

        public int Post(OrderEntry arg)
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
                    OrderEntry model = BuyerOrdeEntryrRepository.Table.Where(m => m.OrderEntryId == arg.OrderEntryId).FirstOrDefault();
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
                    if (arg.ExFactoryDate.ToString() == "1/1/0001 12:00:00 AM")
                    {
                        model.ExFactoryDate = null;
                    }
                    else if (arg.ExFactoryDate.ToString() == "01-01-0001 00:00:00")
                    {
                        model.ExFactoryDate = null;

                    }
                    else
                    {
                        model.ExFactoryDate = arg.ExFactoryDate;
                    }

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
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    // ItesmaterialName = GetMaterial(model.MaterialMasterId);
                    EmailTemplate emailTemplate = new EmailTemplate();
                    CompanyManager companyManager = new CompanyManager();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    List<Company> listCompany = new List<Company>();
                    listCompany = companyManager.Get();
                    //emailTemplate = emailTemplateManager.GetTemplateName("Buyer Order Update");
                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;
                    //    MaterialManager materialManager = new MaterialManager();
                    //    contents = contents.Replace("[[OrderNo]]", !string.IsNullOrEmpty(model.BuyerOrderSlNo) ? model.BuyerOrderSlNo : string.Empty);
                    //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    //    emailTemplate.Body = contents;
                    //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = 0;
            }
            return result;
        }
        //public int InternalPost(InternalOrderForm arg)
        //{
        //    int result = 0;
        //    try
        //    {
        //        if (arg.OrderEntryId == 0)
        //        {
        //            string username = HttpContext.Current.Session["UserName"].ToString();
        //            arg.CreatedBy = username;
        //            //arg.UpdatedBy = "";                    
        //            BuyerorderIntenalDetailsRepository.Insert(arg);

        //            result = arg.OrderEntryId;
        //        }
        //        else
        //        {
        //            InternalOrderForm model = BuyerorderIntenalDetailsRepository.Table.Where(m => m.OrderEntryId == arg.OrderEntryId).FirstOrDefault();
        //            model.OrderEntryId = arg.OrderEntryId;
        //            model.BuyerOrderSlNo = arg.BuyerOrderSlNo;
        //            model.LotNo = arg.LotNo;
        //            model.Count = arg.Count;
        //            //model.FreightAmtCurrency = arg.FreightAmtCurrency;
        //            model.WeekNo = arg.WeekNo;
        //            model.Date = arg.Date;
        //            model.BuyerSeason = arg.BuyerSeason;
        //            model.BuyerName = arg.BuyerName;
        //            model.OrderProjectionNo = arg.OrderProjectionNo;
        //            model.BuyerPoNo = arg.BuyerPoNo;
        //            model.OurStyle = arg.OurStyle;
        //            model.LeatherDescription = arg.LeatherDescription;
        //            model.DiscountPer = arg.DiscountPer;
        //            model.QuoteNo = arg.QuoteNo;
        //            model.CountryStamp = arg.CountryStamp;
        //            model.CustomerPlan = arg.CustomerPlan;
        //            model.CustomerDate = arg.CustomerDate;
        //            model.AgentMasterId = arg.AgentMasterId;
        //            model.CommPer = arg.CommPer;
        //            if (arg.ExFactoryDate.ToString() == "1/1/0001 12:00:00 AM")
        //            {
        //                model.ExFactoryDate = null;
        //            }
        //            else if (arg.ExFactoryDate.ToString() == "01-01-0001 00:00:00")
        //            {
        //                model.ExFactoryDate = null;

        //            }
        //            else
        //            {
        //                model.ExFactoryDate = arg.ExFactoryDate;
        //            }

        //            model.ShipmentMode = arg.ShipmentMode;
        //            model.SampleReqNo = arg.SampleReqNo;
        //            model.Brand = arg.Brand;
        //            model.BuyerStyleNo = arg.BuyerStyleNo;
        //            model.BarCodeNo = arg.BarCodeNo;
        //            model.BomNo = arg.BomNo;
        //            model.Last = arg.Last;
        //            model.ColorMasterId = arg.ColorMasterId;
        //            model.FinishedProdType = arg.FinishedProdType;
        //            model.ProductTypeId = arg.ProductTypeId;
        //            model.AmendmentNoWithDate = arg.AmendmentNoWithDate;
        //            model.TotalOrderForWeek = arg.TotalOrderForWeek;
        //            model.OrderType = arg.OrderType;
        //            model.Currency = arg.Currency;
        //            model.Parties = arg.Parties;
        //            model.GradeMasterId = arg.GradeMasterId;
        //            model.SizeRangeMasterId = arg.SizeRangeMasterId;
        //            model.Remarks1 = arg.Remarks1;
        //            model.Remarks2 = arg.Remarks2;
        //            model.LineNo_1 = arg.LineNo_1;
        //            model.PartiesAmount1 = arg.PartiesAmount1;
        //            model.ShortUnitID = arg.ShortUnitID;
        //            model.PartiesAmount2 = arg.PartiesAmount2;
        //            model.LongUnitID = arg.LongUnitID;
        //            model.IsBuyer = arg.IsBuyer;
        //            model.IsInternal = arg.IsInternal;
        //            model.CreatedDate = arg.CreatedDate;
        //            model.UpdatedDate = DateTime.Now;
        //            model.TotalAmount = arg.TotalAmount;
        //            string username = HttpContext.Current.Session["UserName"].ToString();
        //            model.UpdatedBy = username;
        //            BuyerorderIntenalDetailsRepository.Update(model);
        //            result = arg.OrderEntryId;
        //            MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
        //            // ItesmaterialName = GetMaterial(model.MaterialMasterId);
        //            EmailTemplate emailTemplate = new EmailTemplate();
        //            CompanyManager companyManager = new CompanyManager();
        //            StoreMasterManager storeManager = new StoreMasterManager();
        //            StoreMaster storeMaster = new StoreMaster();
        //            List<Company> listCompany = new List<Company>();
        //            listCompany = companyManager.Get();
        //            //emailTemplate = emailTemplateManager.GetTemplateName("Buyer Order Update");
        //            //if (emailTemplate != null)
        //            //{
        //            //    string contents = emailTemplate.Body;
        //            //    MaterialManager materialManager = new MaterialManager();
        //            //    contents = contents.Replace("[[OrderNo]]", !string.IsNullOrEmpty(model.BuyerOrderSlNo) ? model.BuyerOrderSlNo : string.Empty);
        //            //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
        //            //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
        //            //    emailTemplate.Body = contents;
        //            //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = 0;
        //    }
        //    return result;
        //}

        public bool Delete(int OrderEntryId)
        {
            bool result = false;
            try
            {
                CompanyManager companyManager = new CompanyManager();
                StoreMasterManager storeManager = new StoreMasterManager();
                StoreMaster storeMaster = new StoreMaster();
                List<Company> listCompany = new List<Company>();
                listCompany = companyManager.Get();

                OrderEntry model = BuyerOrdeEntryrRepository.GetById(OrderEntryId);
                model.IsDeleted = true;
                BuyerOrdeEntryrRepository.Update(model);
                MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                // ItesmaterialName = GetMaterial(model.MaterialMasterId);
                EmailTemplate emailTemplate = new EmailTemplate();
                emailTemplate = emailTemplateManager.GetTemplateName("Buyer Order Delete");
                if (emailTemplate != null)
                {
                    string contents = emailTemplate.Body;
                    MaterialManager materialManager = new MaterialManager();
                    contents = contents.Replace("[[OrderNo]]", !string.IsNullOrEmpty(model.BuyerOrderSlNo) ? model.BuyerOrderSlNo : string.Empty);
                    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    //contents = contents.Replace("[[StoreName]]", storeMaster != null ? storeMaster.StoreName != null ? storeMaster.StoreName : string.Empty : string.Empty);
                    // contents = contents.Replace("[[MaterialName]]", "All the material has been deleted");
                    emailTemplate.Body = contents;
                    MMS.Common.EmailHelper.SendEmail(emailTemplate);
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
        //public List<InvoiceDetails> GetnvoiceDetailslist()
        //{
        //    List<InvoiceDetails> InvoiceDetailslist = new List<InvoiceDetails>();
        //    try
        //    {
        //        InvoiceDetailslist = InvoiceDetailsRepository.Table.Where(x => x.IsActive == true).ToList<InvoiceDetails>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    return InvoiceDetailslist;
        //}

        public List<OrderEntry> BuyerOrderforInvoiceList()
        {
            List<OrderEntry> contactlist = new List<OrderEntry>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.BuyerOrderforInvoiceList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        public List<OrderEntry> BuyerOrderforBuyerList(int buyerid)
        {
            List<OrderEntry> contactlist = new List<OrderEntry>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.BuyerOrderforBuyerList(buyerid);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        public List<OrderEntry> BuyerOrderforProcessList(int process)
        {
            List<OrderEntry> contactlist = new List<OrderEntry>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.BuyerOrderforProcessList(process);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        public List<OrderEntry> BuyerOrderforbuyerandProcessList(int buyerid, int process)
        {
            List<OrderEntry> contactlist = new List<OrderEntry>();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.BuyerOrderforbuyerandProcessList(buyerid, process);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        public List<OrderEntry> GetOrderSingleEntryId(int OrderEntryId)
        {
            List<OrderEntry> model = new List<OrderEntry>();
            if (OrderEntryId != 0)
            {
                model = BuyerOrdeEntryrRepository.GetOrderSingleEntryId(OrderEntryId);
            }
            return model;
        }
        public List<OrderEntry> GetbuyerOrderprocessEntryList(int buerid, int Process, int OrderEntryId, DateTime? from_date, DateTime? to_date)
        {
            List<OrderEntry> model = new List<OrderEntry>();
            try
            {
                model = BuyerOrdeEntryrRepository.GetbuyerOrderprocessEntryList(buerid, Process, OrderEntryId, from_date, to_date);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return model;
        }
        public List<OrderEntry> GetOrderListEntryId(int OrderEntryId)
        {
            List<OrderEntry> model = new List<OrderEntry>();
            if (OrderEntryId != 0)
            {
                model = BuyerOrdeEntryrRepository.GetOrderListEntryId(OrderEntryId);
            }
            return model;
        }
        public OrderEntry BuyerOrderstyleList()
        {
            OrderEntry contactlist = new OrderEntry();
            try
            {
                contactlist = BuyerOrdeEntryrRepository.BuyerOrderstyleList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }
        #endregion
    }
}
