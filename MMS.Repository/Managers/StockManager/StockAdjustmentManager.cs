using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service.StockService;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class StockAdjustmentManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<StockAdjustmentForm> stockadjustmentFormRepository;
        private Repository<StockAdjustmentGridDetails> stockadjustmentGridDetailsRepository;
        private Repository<SizeItemsStockAdjustment> stockadjustmentSizeQtyRateRepository;

        #region Add/Update/Delete Operation

        public bool Post(StockAdjustmentForm arg)
        {
            bool result = false;
            try
            {
                if (arg.StockAdjustmentId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    //arg.UpdatedBy = username;
                    stockadjustmentFormRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    StockAdjustmentForm model = stockadjustmentFormRepository.Table.Where(m => m.StockAdjustmentId == arg.StockAdjustmentId).FirstOrDefault();
                    model.StockAdjustmentId = arg.StockAdjustmentId;
                    model.AsOnDate = arg.AsOnDate;
                    model.StoreId = arg.StoreId;
                    model.CategoryId = arg.CategoryId;
                    model.GroupId = arg.GroupId;
                    model.SubGroupId = arg.SubGroupId;
                    model.StockNo = arg.StockNo;
                    model.MaterialType = arg.MaterialType;
                    model.IsApproved = arg.IsApproved;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    stockadjustmentFormRepository.Update(model);
                    result = Convert.ToBoolean(arg.StockAdjustmentId);
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool Put(StockAdjustmentForm arg)
        {
            bool result = false;
            try
            {
                StockAdjustmentForm model = stockadjustmentFormRepository.Table.Where(p => p.StockAdjustmentId == arg.StockAdjustmentId).FirstOrDefault();
                if (model != null)
                {
                    model.StockAdjustmentId = arg.StockAdjustmentId;
                    model.AsOnDate = arg.AsOnDate;
                    model.StoreId = arg.StoreId;
                    model.CategoryId = arg.CategoryId;
                    model.GroupId = arg.GroupId;
                    model.SubGroupId = arg.SubGroupId;
                    model.Rate = arg.Rate;
                    model.StockNo = arg.StockNo;
                    model.MaterialMasterId = arg.MaterialMasterId;
                    model.UomId = arg.UomId;
                    model.MaterialType = arg.MaterialType;
                    //model.BookStock = arg.BookStock;
                    //model.PhysicalStock = arg.PhysicalStock;
                    //model.VariationStock = arg.VariationStock;
                    //model.PlusStock = arg.PlusStock;
                    //model.MinusStock = arg.MinusStock;
                    //model.Size = arg.Size;
                    //model.QTY = arg.QTY;
                    model.CreatedDate = model.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    stockadjustmentFormRepository.Update(model);
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
                StockAdjustmentForm model = stockadjustmentFormRepository.GetById(id);
                stockadjustmentFormRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }

        public bool PostStockAdjustmentSizeQtyRate(SizeItemsStockAdjustment arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBY = username;
                if (arg.SizeMaterialD == 0 || arg.SizeMaterialD == null)
                {
                    stockadjustmentSizeQtyRateRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    // arg.UpdatedBy = username;
                    SizeItemsStockAdjustment model = stockadjustmentSizeQtyRateRepository.Table.Where(p => p.SizeMaterialD == arg.SizeMaterialD).FirstOrDefault();
                    if (model != null)
                    {
                        model.MaterialDescription = arg.MaterialDescription;
                        model.Size = arg.Size;
                        model.Quantity = arg.Quantity;
                        model.MaterialMasterId = arg.MaterialMasterId;
                        model.ShortUnitName = arg.ShortUnitName;
                        model.Rate = arg.Rate;
                        if (arg.BookStock == null)
                            model.BookStock = 0;
                        else
                            model.BookStock = arg.BookStock;
                        if (arg.PhysicalStock == null)
                            model.PhysicalStock = 0;
                        else
                            model.PhysicalStock = arg.PhysicalStock;
                        if (arg.VariationStock == null)
                            model.VariationStock = 0;
                        else
                            model.VariationStock = arg.VariationStock;
                        if (arg.PlusStock == null)
                            model.PlusStock = 0;
                        else
                            model.PlusStock = arg.PlusStock;
                        if (arg.MinusStock == null)
                            model.MinusStock = 0;
                        else
                            model.MinusStock = arg.MinusStock;
                        if (arg.BookStockValue == null)
                            model.BookStockValue = 0;
                        else
                            model.BookStockValue = arg.BookStockValue;
                        if (arg.PhysicalStockValue == null)
                            model.PhysicalStockValue = 0;
                        else
                            model.PhysicalStockValue = arg.PhysicalStockValue;
                        if (arg.VariationStockValue == null)
                            model.VariationStockValue = 0;
                        else
                            model.VariationStockValue = arg.VariationStockValue;
                        if (arg.PlusStockValue == null)
                            model.PlusStockValue = 0;
                        else
                            model.PlusStockValue = arg.PlusStockValue;
                        if (arg.MinusStockValue == null)
                            model.MinusStockValue = 0;
                        else
                            model.MinusStockValue = arg.MinusStockValue;
                        model.StockAdjustmentFk = arg.StockAdjustmentFk;
                        //  model.CreatedDate = arg.CreatedDate;
                        model.UpdatedDate = DateTime.Now;
                        string username_ = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username_;
                        stockadjustmentSizeQtyRateRepository.Update(model);
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }

        public bool DeleteStockAdjustmentSizeQtyRate(int StockAdjustmentId)
        {
            bool result = false;
            try
            {
                List<SizeItemsStockAdjustment> model = GetSizeQuantityRangeById(StockAdjustmentId);
                foreach (var item in model)
                {
                    stockadjustmentSizeQtyRateRepository.Delete(item);
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



        #endregion

        #region Helper Method

        public StockAdjustmentManager()
        {
            stockadjustmentFormRepository = unitOfWork.Repository<StockAdjustmentForm>();
            stockadjustmentGridDetailsRepository = unitOfWork.Repository<StockAdjustmentGridDetails>();
            stockadjustmentSizeQtyRateRepository = unitOfWork.Repository<SizeItemsStockAdjustment>();
        }

        public StockAdjustmentForm GetStockAdjustmentId(int StockAdjustmentId)
        {
            StockAdjustmentForm stockadjustmentForm = new StockAdjustmentForm();
            if (StockAdjustmentId != 0)
            {
                stockadjustmentForm = stockadjustmentFormRepository.Table.Where(x => x.StockAdjustmentId == StockAdjustmentId).SingleOrDefault();
            }
            return stockadjustmentForm;
        }
        public StockAdjustmentForm isExists(DateTime? AsOnDate,int CategoryID,int StoreID,int GroupID,int MaterialType)
        {
            StockAdjustmentForm stockadjustmentForm = new StockAdjustmentForm();
            if (AsOnDate != null)
            {
                stockadjustmentForm = stockadjustmentFormRepository.Table.Where(x => x.AsOnDate == AsOnDate &&x.CategoryId==CategoryID&&x.StoreId==StoreID&&x.GroupId==GroupID&&x.MaterialType==MaterialType).FirstOrDefault();
            }
            return stockadjustmentForm;
        }
        public List<StockAdjustMent> isExistsStockAdjustment(DateTime? AsOnDate, int CategoryID, int StoreID, int GroupID, int MaterialType,int MaterialID)
        {
            List<StockAdjustMent> stockadjustmentForm = new List<StockAdjustMent>();
            if (AsOnDate != null)
            {
                stockadjustmentForm = stockadjustmentSizeQtyRateRepository.isExistsStockAdjustment(StoreID,AsOnDate,CategoryID,GroupID,MaterialType, MaterialID);
            }
            return stockadjustmentForm;
        }

        public StockAdjustmentForm Get(int id)
        {
            return null;
        }

        public List<StockAdjustmentForm> Get()
        {
            List<StockAdjustmentForm> stockadjustmentForm = new List<StockAdjustmentForm>();
            try
            {
                stockadjustmentForm = stockadjustmentFormRepository.Table.ToList<StockAdjustmentForm>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return stockadjustmentForm;
        }

        public List<StockAdjustmentGridDetails> GetList(int Store, DateTime From, int Category, int Group, int MaterialType)
        {
            List<StockAdjustmentGridDetails> stockadjustment = new List<StockAdjustmentGridDetails>();
            try
            {

                stockadjustment = stockadjustmentFormRepository.SearchBookStockList(Store, From, Category, Group, MaterialType);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return stockadjustment;
        }


        public List<SizeItemsStockAdjustment> GetSizeQuantityRangeById(int StockAdjustmentId)
        {
            List<SizeItemsStockAdjustment> model = new List<SizeItemsStockAdjustment>();
            if (StockAdjustmentId != 0)
            {
                model = stockadjustmentSizeQtyRateRepository.Table.Where(x => x.StockAdjustmentFk == StockAdjustmentId).ToList();
            }
            return model;
        }
        public List<SizeItemsStockAdjustment> DeleteSizeQuantityRangeById(int StockAdjustmentId)
        {
            List<SizeItemsStockAdjustment> model = new List<SizeItemsStockAdjustment>();
            string consString = ConfigurationManager.ConnectionStrings["MMSConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM  SizeItemsStockAdjustment WHERE StockAdjustmentFk='" + StockAdjustmentId + "'", con))
                {
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
            return model;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        #endregion

        #region StockAdjustmentGrid

        //public bool Post(StockAdjustmentGridDetails arg)
        //{
        //    bool result = false;
        //    try
        //    {
        //        if (arg.ImirGridId == 0)
        //        {
        //            stockadjustmentGridDetailsRepository.Insert(arg);
        //            result = true;
        //        }
        //        else if (arg.ImirGridId != 0)
        //        {
        //            StockAdjustmentGridDetails model = stockadjustmentGridDetailsRepository.Table.Where(x => x.ImirGridId == arg.ImirGridId).FirstOrDefault();
        //            model.ImirGridId = arg.ImirGridId;
        //            model.SlNo = arg.SlNo;
        //            model.ParameterId = arg.ParameterId;
        //            model.Parameter = arg.Parameter;
        //            model.InspectionFrequency = arg.InspectionFrequency;
        //            model.RejectionQty = arg.RejectionQty;
        //            model.GridRemarks = arg.GridRemarks;
        //            model.ImirId = arg.ImirId;
        //            model.CreatedDate = arg.CreatedDate;
        //            model.UpdatedDate = DateTime.Now;
        //            stockadjustmentGridDetailsRepository.Update(arg);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }
        //    return result;
        //}

        //public ImirGridDetails GetImirGridId(int ImirGridId)
        //{
        //    ImirGridDetails imirGridDetails = new ImirGridDetails();
        //    if (ImirGridId != 0)
        //    {
        //        imirGridDetails = imirGridDetailsRepository.Table.Where(x => x.ImirGridId == ImirGridId).SingleOrDefault();
        //    }
        //    return imirGridDetails;
        //}

        //public List<ImirGridDetails> GridList()
        //{
        //    List<ImirGridDetails> imirGridDetails = new List<ImirGridDetails>();
        //    try
        //    {
        //        imirGridDetails = imirGridDetailsRepository.Table.ToList<ImirGridDetails>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //    }
        //    return imirGridDetails;
        //}


        //public bool ImirGridDelete(int id)
        //{
        //    bool result = false;
        //    try
        //    {
        //        ImirGridDetails model = imirGridDetailsRepository.GetById(id);
        //        imirGridDetailsRepository.Delete(model);
        //        result = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        result = false;
        //    }
        //    return result;
        //}

        #endregion
    }
}
