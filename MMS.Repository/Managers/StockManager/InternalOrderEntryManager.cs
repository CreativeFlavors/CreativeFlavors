using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
   //public class InternalOrderEntryManager
   // {
   //    private UnitOfWork unitOfWork = new UnitOfWork();
   //    private Repository<InternalOrderEntryForm> InternalOrderEntryRepository;

   //    #region Helper Method
   //    public InternalOrderEntryManager()
   //     {
   //         InternalOrderEntryRepository = unitOfWork.Repository<InternalOrderEntryForm>();
   //     }
   //    public InternalOrderEntryForm Get(int id)
   //     {
   //         return null;
   //     }
   //    public List<InternalOrderEntryForm> Get()
   //     {
   //         List<InternalOrderEntryForm> internalOrderEntryForm = new List<InternalOrderEntryForm>();
   //         try
   //         {
   //             internalOrderEntryForm = InternalOrderEntryRepository.Table.ToList<InternalOrderEntryForm>();
   //         }
   //         catch (Exception ex)
   //         {
   //             Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
   //         }
   //         return internalOrderEntryForm;
   //     }
   //    public InternalOrderEntryForm GetInternalEntryOrderId(int OrderEntryId)
   //     {
   //         InternalOrderEntryForm internalOrderEntryForm = new InternalOrderEntryForm();
   //         if (OrderEntryId != 0)
   //         {
   //             internalOrderEntryForm = InternalOrderEntryRepository.Table.Where(x => x.OrderEntryId == OrderEntryId).SingleOrDefault();
   //         }
   //         return internalOrderEntryForm;
   //     }
   //     #endregion

   //    #region Crud Operation
   //    public bool Post(InternalOrderEntryForm arg)
   //    {
   //        bool result = false;
   //        try
   //        {
   //            if (arg.OrderEntryId == 0)
   //            {
   //                //string username = HttpContext.Current.Session["UserName"].ToString();
   //                arg.UpdatedBy = "";
   //                arg.CreatedBy = "";
   //                InternalOrderEntryRepository.Insert(arg);
   //                result = true;
   //            }
   //            else
   //            {
   //                InternalOrderEntryForm model = InternalOrderEntryRepository.Table.Where(m => m.OrderEntryId == arg.OrderEntryId).FirstOrDefault();
   //                model.OrderEntryId = arg.OrderEntryId;
   //                model.BuyerOrderSlNo = arg.BuyerOrderSlNo;
   //                model.LotNo = arg.LotNo;
   //                model.Count = arg.Count;
   //                //model.FreightAmtCurrency = arg.FreightAmtCurrency;
   //                model.WeekNo = arg.WeekNo;
   //                model.Date = arg.Date;
   //                model.BuyerSeason = arg.BuyerSeason;
   //                model.BuyerName = arg.BuyerName;
   //                model.OrderProjectionNo = arg.OrderProjectionNo;
   //                model.BuyerPoNo = arg.BuyerPoNo;
   //                model.OurStyle = arg.OurStyle;
   //                model.LeatherDescription = arg.LeatherDescription;
   //                model.DiscountPer = arg.DiscountPer;
   //                model.QuoteNo = arg.QuoteNo;
   //                model.CountryStamp = arg.CountryStamp;
   //                model.CustomerPlan = arg.CustomerPlan;
   //                model.CustomerDate = arg.CustomerDate;
   //                model.AgentMasterId = arg.AgentMasterId;
   //                model.CommPer = arg.CommPer;
   //                model.ExFactoryDate = arg.ExFactoryDate;
   //                model.ShipmentMode = arg.ShipmentMode;
   //                model.SampleReqNo = arg.SampleReqNo;
   //                model.Brand = arg.Brand;
   //                model.BuyerStyleNo = arg.BuyerStyleNo;
   //                model.BarCodeNo = arg.BarCodeNo;
   //                model.BomNo = arg.BomNo;
   //                model.Last = arg.Last;
   //                model.ColorMasterId = arg.ColorMasterId;
   //                model.FinishedProdType = arg.FinishedProdType;
   //                model.ProductType_BuyerModel = arg.ProductType_BuyerModel;
   //                model.AmendmentNoWithDate = arg.AmendmentNoWithDate;
   //                model.TotalOrderForWeek = arg.TotalOrderForWeek;
   //                model.OrderType = arg.OrderType;
   //                model.Currency = arg.Currency;
   //                model.Parties = arg.Parties;
   //                model.GradeMasterId = arg.GradeMasterId;
   //                model.SizeRangeMasterId = arg.SizeRangeMasterId;
   //                model.Remarks1 = arg.Remarks1;
   //                model.Remarks2 = model.Remarks2;
   //                //model.OtherSpecifications = arg.OtherSpecifications;
   //                model.LineNo_1 = arg.LineNo_1;
   //                model.IsBuyer = arg.IsBuyer;
   //                model.IsInternal = arg.IsInternal;
   //                model.CreatedDate = arg.CreatedDate;
   //                model.UpdatedDate = DateTime.Now;
   //                model.CreatedBy = arg.CreatedBy;
   //                model.UpdatedBy = arg.UpdatedBy;
   //                //model.UpdatedDate = DateTime.Now;
   //                //string username = HttpContext.Current.Session["UserName"].ToString();
   //                //model.UpdatedBy = username;
   //                InternalOrderEntryRepository.Update(model);
   //                result = true;
   //            }
   //        }
   //        catch (Exception ex)
   //        {
   //            Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
   //            result = false;
   //        }
   //        return result;
   //    }

   //    public bool Delete(int InternalEntryOrderId)
   //    {
   //        bool result = false;
   //        try
   //        {
   //            InternalOrderEntryForm model = InternalOrderEntryRepository.GetById(InternalEntryOrderId);
   //            InternalOrderEntryRepository.Delete(model);
   //            result = true;
   //        }
   //        catch (Exception ex)
   //        {
   //            Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
   //            result = false;
   //        }

   //        return result;
   //    }
   //    #endregion
   // }
}
