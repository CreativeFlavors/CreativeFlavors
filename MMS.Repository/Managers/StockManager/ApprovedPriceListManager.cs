using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using MMS.Repository.Service.StockService;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MMS.Repository;
using System.IO;
using MMS.Data.StoredProcedureModel;
using System.Web.Hosting;
using System.Globalization;

namespace MMS.Repository.Managers.StockManager
{
    public class ApprovedPriceListManager : IApprovedPriceListService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<ApprovedPriceList> ApprovedPriceListRepository;
        private Repository<ApprovedPriceListHistory> ApprovedPriceListHistoryRepository;
        private Repository<MaterialMaster> MaterialMasterRepository;

        #region Add/Update/Delete Operation

        public bool Post(ApprovedPriceList arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.CreatedDate = DateTime.Now;
                ApprovedPriceListRepository.Insert(arg);
                //ApprovedPriceHistoryPrice(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public bool Put(ApprovedPriceList arg)
        {
            bool result = false;
            try
            {
                ApprovedPriceList model = ApprovedPriceListRepository.Table.Where(p => p.ApprovedPriceID == arg.ApprovedPriceID).FirstOrDefault();
                if (model != null)
                {
                    model.ApprovedPriceID = arg.ApprovedPriceID;
                    model.SupplierId = arg.SupplierId;
                    model.Date = arg.Date;
                    model.PriceRs = arg.PriceRs;
                    model.Uom = arg.Uom;
                    model.CategoryID = arg.CategoryID;
                    model.TaxDetails = arg.TaxDetails;
                    model.IsApproved = arg.IsApproved;
                    model.GroupID = arg.GroupID;
                    model.MaterialID = arg.MaterialID;
                    model.POExcessPercentage = arg.POExcessPercentage;
                    model.ColorID = arg.ColorID;
                    model.LeadTime = arg.LeadTime;
                    model.MRPMargin = arg.MRPMargin;
                    model.MRPPrice = arg.MRPPrice;
                    model.AccessibleValue = arg.AccessibleValue;
                    model.SupplierDescription = arg.SupplierDescription;
                    model.CreatedDate = arg.CreatedDate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.ApprovedBy = username;
                    //model.CreatedBy = username;
                    model.UpdatedBY = username;
                    ApprovedPriceListRepository.Update(model);
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
        public ApprocedPriceList Email(string MaterialID, string SupplierID)
        {
            try
            {
                List<ApprovedPriceMail> listApprovePriceMail = new List<ApprovedPriceMail>();
                listApprovePriceMail = ApprovedPriceListRepository.ChangepriceinApprovedPrice(MaterialID, SupplierID);
                string invoiceDynamicRow = "";
                string fileName = "";
                string contents = string.Empty;
                string HtmlText = string.Empty;
                fileName = HostingEnvironment.MapPath("~/App_Data/PdfTemplate/RateComparison.html");
                contents = System.IO.File.ReadAllText(fileName);
                HtmlText = contents;
                int count = 1;
                foreach (var item in listApprovePriceMail)
                {
                    decimal? PreviousPrice = 0;
                    PreviousPrice = listApprovePriceMail.Where(x => x.RankOrder == 2).Select(x => x.PriceRs).FirstOrDefault();
                    if (count == 1) 
                    {
                        invoiceDynamicRow += "<tr>" +
                                            "<td width='180px' style='border-right:1px solid #fff;border-left:1px solid #ddd;'><h3 style ='color:#4e4e4e; font-size:14px; line-height:30px; padding:5px 0 5px 20px;'>" + count + "</ h3 ></td>" +
                                            "<td width='300px' style='border-right:1px solid #ddd'><h3 style ='color:#4e4e4e; font-size:14px; line-height:30px; padding:5px 0 5px 20px;'>" + item.MaterialDescription + "</ h3 ></td>" +
                                            "<td width='300px' style='border-right:1px solid #ddd'><h3 style ='color:#4e4e4e; font-size:14px; line-height:30px; padding:5px 0 5px 20px;'>" + item.SupplierName + "</ h3 ></td>" +
                                            "<td style='border-right:1px solid #ddd'><h3 style ='color:#4e4e4e; font-size:14px; line-height:30px; padding:5px 0 5px 20px;'>" + PreviousPrice + "</ h3 ></td>" +
                                            "<td  style='border-right:1px solid #ddd'><h3 style ='color:#4e4e4e; font-size:14px; line-height:30px; padding:5px 0 5px 20px;'>" + item.PriceRs + "</ h3 ></td>" +
                                            "<td width='300px' style='border-right:1px solid #ddd'><h3 style ='color:#4e4e4e; font-size:14px; line-height:30px; padding:5px 0 5px 20px;'>" + item.UpdatedBY + "</ h3 ></td>" +
                                         "<td  width='300px' style='border-right:1px solid #ddd'><h3 style ='color:#4e4e4e; font-size:14px; line-height:30px; padding:5px 0 5px 20px;'>" + item.approvedby + "</ h3 ></td>" +
                                            "<td colspan = '6'; style = 'border-bottom:1px solid #ddd' ></td>" + "</tr>";
                    }

                    count++;
                }
                HtmlText = HtmlText.Replace("[[DynamicAppend]]", invoiceDynamicRow);
                HtmlText = HtmlText.Replace("[[Company]]", HttpContext.Current.Request.Url.Host.ToLower());
                HtmlText = HtmlText.Replace("[[Store]]", listApprovePriceMail.FirstOrDefault().StoreName);
                HtmlText = HtmlText.Replace("[[Date]]", DateTime.Now.ToString("dd/MM/yyyy"));
                MMS.Common.EmailHelper.RateComparisonEmail(HtmlText, "Rate Comparison");

            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                ApprovedPriceList model = ApprovedPriceListRepository.GetById(id);
                ApprovedPriceListRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }
        public bool PutPrice(ApprovedPriceList arg)
        {
            bool result = false;
            try
            {
                MaterialMaster model = MaterialMasterRepository.Table.Where(p => p.MaterialName == arg.MaterialID).FirstOrDefault();
                if (model != null)
                {

                    model.Price = arg.PriceRs.ToString();
                    MaterialMasterRepository.Update(model);
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
        public bool ApprovedPriceHistoryPrice(ApprovedPriceList arg)
        {
            bool result = false;
            try
            {
                ApprovedPriceListHistory approvedPriceHistoryPrice = new ApprovedPriceListHistory();
                if (arg != null)
                {
                    approvedPriceHistoryPrice.ApprovedPriceID = arg.ApprovedPriceID;
                    approvedPriceHistoryPrice.SupplierId = arg.SupplierId;
                   // var format = "dd/MM/yyyy";
                   // DateTime pricDate = DateTime.ParseExact(arg.Date.Value.ToString("dd/MM/yyyy"), format, CultureInfo.InvariantCulture);
                    DateTime pricDate = Convert.ToDateTime(Convert.ToDateTime(arg.Date).ToString("dd/MM/yyyy HH:mm:ss"));
                    approvedPriceHistoryPrice.Date = pricDate.Date;
                    approvedPriceHistoryPrice.PriceRs = arg.PriceRs;
                    approvedPriceHistoryPrice.CategoryID = arg.CategoryID;
                    approvedPriceHistoryPrice.TaxDetails = arg.TaxDetails;
                    approvedPriceHistoryPrice.GroupID = arg.GroupID;
                    approvedPriceHistoryPrice.IsApproved = arg.IsApproved;
                    approvedPriceHistoryPrice.MaterialID = arg.MaterialID;
                    approvedPriceHistoryPrice.ColorID = arg.ColorID;
                    approvedPriceHistoryPrice.LeadTime = arg.LeadTime;
                    approvedPriceHistoryPrice.MRPMargin = arg.MRPMargin;
                    approvedPriceHistoryPrice.CurrencyID = arg.CurrencyID;
                    approvedPriceHistoryPrice.POExcessPercentage = arg.POExcessPercentage;
                    approvedPriceHistoryPrice.MRPPrice = arg.MRPPrice;
                    approvedPriceHistoryPrice.AccessibleValue = arg.AccessibleValue;
                    approvedPriceHistoryPrice.IsApproved = arg.IsApproved;
                    approvedPriceHistoryPrice.SupplierDescription = arg.SupplierDescription;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    approvedPriceHistoryPrice.ApprovedBy = username;
                    approvedPriceHistoryPrice.UpdatedBY = username;
                    approvedPriceHistoryPrice.CreatedDate = DateTime.Now;
                    ApprovedPriceListHistoryRepository.Insert(approvedPriceHistoryPrice);
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

        #endregion

        #region Helper Method

        public ApprovedPriceListManager()
        {
            ApprovedPriceListRepository = unitOfWork.Repository<ApprovedPriceList>();
            MaterialMasterRepository = unitOfWork.Repository<MaterialMaster>();
            ApprovedPriceListHistoryRepository = unitOfWork.Repository<ApprovedPriceListHistory>();
        }

        public ApprovedPriceList GetApprovedPriceID(int ApprovedPriceID)
        {
            ApprovedPriceList spprocedPriceList = new ApprovedPriceList();
            if (ApprovedPriceID != 0)
            {
                spprocedPriceList = ApprovedPriceListRepository.Table.Where(x => x.ApprovedPriceID == ApprovedPriceID).SingleOrDefault();
            }
            return spprocedPriceList;
        }
        public ApprovedPriceList GetMaterialPrice(int MaterialID)
        {
            ApprovedPriceList spprocedPriceList = new ApprovedPriceList();
            if (MaterialID != 0)
            {

                spprocedPriceList = ApprovedPriceListRepository.Table.Where(x => x.MaterialID == MaterialID).FirstOrDefault();
            }
            return spprocedPriceList;
        }
        public List<SpResult> GetApprovedMasterId(int ApprovedMasterId)
        {
            List<SpResult> spprocedPriceList = new List<SpResult>();
            if (ApprovedMasterId != 0)
            {
                spprocedPriceList = ApprovedPriceListRepository.ExecWithStoreProcedure("", ApprovedMasterId);

            }
            return spprocedPriceList;
        }

        public List<ApprovedPriceListHistory> GetID(int ApprovedMasterId)
        {
            List<ApprovedPriceListHistory> approvedPriceListHistory = new List<ApprovedPriceListHistory>();
            if (ApprovedMasterId != 0)
            {
                approvedPriceListHistory = ApprovedPriceListHistoryRepository.Table.Where(x => x.ApprovedPriceID == ApprovedMasterId).ToList();

            }
            return approvedPriceListHistory;
        }

        public ApprovedPriceList Get(int id)
        {
            return null;
        }

        public List<ApprovedPriceList> Get()
        {
            List<ApprovedPriceList> ApprovedPriceList = new List<ApprovedPriceList>();

            try
            {
                ApprovedPriceList = ApprovedPriceListRepository.Table.ToList<ApprovedPriceList>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }

        public List<ApprovedPriceList> GetMaterialList(int MaterialID)
        {
            List<ApprovedPriceList> ApprovedPriceList = new List<ApprovedPriceList>();

            try
            {
                ApprovedPriceList = ApprovedPriceListRepository.Table.Where(x => x.MaterialID == MaterialID).ToList<ApprovedPriceList>().OrderByDescending(x => x.CreatedDate).Take(1).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }
        public List<ApprovedPriceList> GetMaterialList_Po(int MaterialID)
        {
            List<ApprovedPriceList> ApprovedPriceList = new List<ApprovedPriceList>();

            try
            {
                ApprovedPriceList = ApprovedPriceListRepository.Table.Where(x => x.MaterialID == MaterialID).ToList<ApprovedPriceList>().OrderByDescending(x => x.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }
        public List<ApprovedPriceList> ApprovedPriceListGridBasedOnSupplierId(int? SupplierId)
        {
            List<ApprovedPriceList> ApprovedPriceList = new List<ApprovedPriceList>();

            try
            {
                ApprovedPriceList = ApprovedPriceListRepository.Table.Where(x => x.SupplierId == SupplierId).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return ApprovedPriceList;
        }



        public List<ApprovedPriceList> GetRate(int MaterialNameID, int? SupplierNameId)
        {
            List<ApprovedPriceList> approvedPriceListHistory = new List<ApprovedPriceList>();

            try
            {
                approvedPriceListHistory = ApprovedPriceListRepository.Table.Where(x => x.SupplierId == SupplierNameId && x.MaterialID == MaterialNameID).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedPriceListHistory;
        }
        public ApprovedPriceList GetApprovedPriceList(int MaterialNameID, int? SupplierNameId)
        {
            ApprovedPriceList approvedPriceListHistory = new ApprovedPriceList();

            try
            {
                approvedPriceListHistory = ApprovedPriceListRepository.Table.Where(x => x.SupplierId == SupplierNameId && x.MaterialID == MaterialNameID).OrderByDescending(x => x.CreatedDate).Take(1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedPriceListHistory;
        }
        public ApprovedPriceList GetRateForDirectPO(int MaterialNameID)
        {
            ApprovedPriceList approvedPriceListHistory = new ApprovedPriceList();
            try
            {
                approvedPriceListHistory = ApprovedPriceListRepository.Table.Where(x => x.MaterialID == MaterialNameID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedPriceListHistory;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }


        public List<SupplierSuppliedMaterialPrice> SupplierSuppliedMaterialPrice(int MaterialmasterId, int SupplierId)
        {
            List<SupplierSuppliedMaterialPrice> supplierMaterialPrice = new List<SupplierSuppliedMaterialPrice>();
            try
            {
                supplierMaterialPrice = ApprovedPriceListHistoryRepository.SupplierSuppliedMaterialPrice(MaterialmasterId, SupplierId);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return supplierMaterialPrice;
        }


        #endregion

        public List<ApprovedPriceListMasterGrid> GetApprovedPriceGrid(string SupplierName)
        {
            List<ApprovedPriceListMasterGrid> approvedlist = new List<ApprovedPriceListMasterGrid>();
            try
            {
                approvedlist = ApprovedPriceListRepository.SearchApprovedPriceList(SupplierName);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return approvedlist;
        }
    }
}
