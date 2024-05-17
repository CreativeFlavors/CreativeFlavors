using MMS.Common;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using MMS.Data;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Service.StockService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers.StockManager
{
    public class PurchaseOrderManager
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<PurchaseOrder> PurchaseOrderRepository;
        private Repository<PurchaseOrderDelierySchedule> PurchaseOrderDelieryScheduleRepository;
        private Repository<PurchaseOrderSizeRangeQuantity> PurchaseOrderSizeRangeQuantityRepository;
        BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
        EmailTemplateManager emailTemplateManager = new EmailTemplateManager();
        #region Helper Method
        public PurchaseOrderManager()
        {
            PurchaseOrderRepository = unitOfWork.Repository<PurchaseOrder>();
            PurchaseOrderDelieryScheduleRepository = unitOfWork.Repository<PurchaseOrderDelierySchedule>();
            PurchaseOrderSizeRangeQuantityRepository = unitOfWork.Repository<PurchaseOrderSizeRangeQuantity>();
        }

        public PurchaseOrder GetPoNO(string POno)
        {
            PurchaseOrder IndentMaster = new PurchaseOrder();
            if (POno != "")
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.PoNo == POno).FirstOrDefault();
            }
            return IndentMaster;
        }
        public List<MMS.Web.Models.PendingQty> MaterialOpeningStockValue(int materialMasterID)
        {
            List<MMS.Web.Models.PendingQty> bomMaterialList = new List<MMS.Web.Models.PendingQty>();
            if (materialMasterID != 0)
            {
                bomMaterialList = PurchaseOrderRepository.MaterialOpeningStockValue(materialMasterID);

            }
            return bomMaterialList;
        }
        public List<PurchaseOrder> GetPoNOList(string POno)
        {
            List<PurchaseOrder> IndentMaster = new List<PurchaseOrder>();
            if (POno != "")
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.PoNo == POno).ToList();
            }
            return IndentMaster;
        }
        public List<PurchaseOrder> GetPoOrderIdList(int PoOrderId)
        {
            List<PurchaseOrder> IndentMaster = new List<PurchaseOrder>();
            if (PoOrderId != null && PoOrderId != 0)
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.PoOrderId == PoOrderId).ToList();
            }
            return IndentMaster;
        }
        public PurchaseOrder Get(int id)
        {
            return null;
        }
        public List<PurchaseOrder> Get()
        {
            List<PurchaseOrder> IndentList = new List<PurchaseOrder>();
            try
            {
                IndentList = PurchaseOrderRepository.Table.ToList<PurchaseOrder>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<PurchaseOrderGrid> PurchaseOrderGrid()
        {
            List<PurchaseOrderGrid> purchaseOrderGridList = new List<PurchaseOrderGrid>();
            purchaseOrderGridList = PurchaseOrderRepository.GetPurchaseOrderGrid();
            return purchaseOrderGridList;
        }
        public List<PurchaseOrderDelierySchedule> PurchaseOrderDelieryScheduleGet()
        {
            List<PurchaseOrderDelierySchedule> IndentList = new List<PurchaseOrderDelierySchedule>();
            try
            {
                IndentList = PurchaseOrderDelieryScheduleRepository.Table.ToList<PurchaseOrderDelierySchedule>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public List<PurchaseOrderSizeRangeQuantity> PurchaseOrderSizeRangeQuantityGet()
        {
            List<PurchaseOrderSizeRangeQuantity> IndentList = new List<PurchaseOrderSizeRangeQuantity>();
            try
            {
                IndentList = PurchaseOrderSizeRangeQuantityRepository.Table.ToList<PurchaseOrderSizeRangeQuantity>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return IndentList;
        }
        public PurchaseOrder GetPoOrderId(int PoOrderId)
        {
            PurchaseOrder IndentMaster = new PurchaseOrder();
            if (PoOrderId != 0)
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.PoOrderId == PoOrderId).FirstOrDefault();
            }
            return IndentMaster;
        }
        public PurchaseOrder GetPoIndentOrderId(int IndentMaterialID)
        {
            PurchaseOrder IndentMaster = new PurchaseOrder();
            if (IndentMaterialID != 0)
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.IndentMaterialID == IndentMaterialID).FirstOrDefault();
            }
            return IndentMaster;
        }
        public PurchaseOrder GetIndentClubMaterial(int? MaterialID, string IndentNo)
        {
            PurchaseOrder IndentMaster = new PurchaseOrder(); 
            if (IndentNo != "")
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x =>  x.Material == MaterialID && x.IndentNo.Contains(IndentNo)).FirstOrDefault();
            }
            return IndentMaster;
        }
        public List<PurchaseOrder> GetPoIndentMaterialList(int IndentMaterialID)
        {
            List<PurchaseOrder> IndentMaster = new List<PurchaseOrder>();
            if (IndentMaterialID != 0)
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.IndentMaterialID == IndentMaterialID).ToList();
            }
            return IndentMaster;
        }
        public List<PurchaseOrder> GetIndentClubPoMaterialList(int? MaterialID, string IndentNo)
        {
            List<PurchaseOrder> IndentMaster = new List<PurchaseOrder>();
            if (IndentNo != "")
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.Material == MaterialID && x.IndentNo.Contains(IndentNo) && x.PendingQty != 0).ToList();
            }
            return IndentMaster;
        }
        public List<PurchaseOrder> GetISPoIndentMaterialList(string IndentID, int? MaterialID)
        {
            List<PurchaseOrder> IndentMaster = new List<PurchaseOrder>();
            if (IndentID != "")
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.IndentNo.Contains(IndentID) && x.Material == MaterialID).ToList();
            }
            return IndentMaster;
        }
        public PurchaseOrder GetPoExist(string PoNo, int Material)
        {
            PurchaseOrder IndentMaster = new PurchaseOrder();
            if (Material != 0 && PoNo != null)
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.PoNo == PoNo && x.Material == Material).FirstOrDefault();
            }
            return IndentMaster;
        }
        public PurchaseOrder GetSupplierWithIndentID(int Supplierid, string IndentID)
        {
            PurchaseOrder IndentMaster = new PurchaseOrder();
            if (Supplierid != 0 && IndentID != "")
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.Supplier == Supplierid && x.IndentNo == IndentID).FirstOrDefault();
            }
            return IndentMaster;
        }
        public List<PurchaseOrder> GetIndentPoPendingQty(int MaterialID, string IndentID)
        {
            List<PurchaseOrder> PurchaseOrderList = new List<PurchaseOrder>();
            if (MaterialID != 0 && IndentID != null)
            {
                PurchaseOrderList = PurchaseOrderRepository.Table.Where(x => x.Material == MaterialID && x.IndentNo.Contains(IndentID)).ToList();
            }
            return PurchaseOrderList;
        }
        public List<PurchaseOrderDelierySchedule> GetPurchaseOrderDelierySchedulePoOrderId(int PoOrderId)
        {
            List<PurchaseOrderDelierySchedule> purchaseOrderDelierySchedulelist = new List<PurchaseOrderDelierySchedule>();
            if (PoOrderId != 0)
            {
                purchaseOrderDelierySchedulelist = PurchaseOrderDelieryScheduleRepository.Table.Where(x => x.PoOrderID == PoOrderId).ToList();
            }
            return purchaseOrderDelierySchedulelist;
        }
        public List<PurchaseOrderSizeRangeQuantity> GetPurchaseOrderSizeRangeQuantityPoOrderId(int PoOrderId)
        {
            List<PurchaseOrderSizeRangeQuantity> purchaseOrderSizeRangeQuantityList = new List<PurchaseOrderSizeRangeQuantity>();
            if (PoOrderId != 0)
            {
                purchaseOrderSizeRangeQuantityList = PurchaseOrderSizeRangeQuantityRepository.Table.Where(x => x.PoOrderID == PoOrderId).ToList();
            }
            return purchaseOrderSizeRangeQuantityList;
        }

        public PurchaseOrder GetPoNOWithMaterial(int IndentMaterialID)
        {
            PurchaseOrder IndentMaster = new PurchaseOrder();
            if (IndentMaterialID != 0 && IndentMaterialID != null)
            {
                IndentMaster = PurchaseOrderRepository.Table.Where(x => x.IndentMaterialID == IndentMaterialID).FirstOrDefault();
            }
            return IndentMaster;
        }
        #endregion
        #region Curd Operation
        public bool Post(PurchaseOrder arg)
        {
            bool result = false;
            try
            {
                if (arg.PoOrderId == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    // arg.UpdatedBy = username;
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    PurchaseOrderRepository.Insert(arg);
                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    EmailTemplate emailTemplate = new EmailTemplate();
                    emailTemplate = emailTemplateManager.GetTemplateName("Purchase order Update");
                    ItesmaterialName = bomMaterialListManager.GetMaterial(arg.Material.Value);
                    CompanyManager companyManager = new CompanyManager();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    List<Company> listCompany = new List<Company>();
                    listCompany = companyManager.Get();

                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;
                    //    MaterialManager materialManager = new MaterialManager();
                    //    contents = contents.Replace("[[PONo]]", !string.IsNullOrEmpty(arg.PoNo) ? arg.PoNo : string.Empty);
                    //    contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) ? ItesmaterialName.materialdescription : string.Empty : string.Empty);

                    //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    //    emailTemplate.Body = contents;
                    //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    //}
                    result = true;
                }
                else
                {
                    PurchaseOrder model = PurchaseOrderRepository.Table.Where(m => m.PoOrderId == arg.PoOrderId).FirstOrDefault();
                    model.PoOrderId = arg.PoOrderId;
                    model.PoNo = arg.PoNo;
                    model.Unit = arg.Unit;
                    model.FreightAmt = arg.FreightAmt;
                    model.MaterialType = arg.MaterialType;
                    model.FreightType = arg.FreightType;
                    model.LastAmendmentNo = arg.LastAmendmentNo;
                    model.LastAmendmentDate = DateTime.Now;
                    model.PoDate = arg.PoDate;
                    model.FrightType = arg.FrightType;
                    model.PoQty = arg.PoQty;
                    model.PendingQty = arg.PendingQty;
                    model.StockValue = arg.StockValue;
                    model.ServiceTaxPer = arg.ServiceTaxPer;
                    model.FreightCostTotal = arg.FreightCostTotal;
                    model.UOMValue1 = arg.UOMValue1;
                    model.OrderType = arg.OrderType;
                    model.IndentValue = arg.IndentValue;
                    model.PoType = arg.PoType;
                    model.IndentMaterialID = arg.IndentMaterialID;
                    model.InsuranceAmount = arg.InsuranceAmount;
                    model.InsureanceCurrency = arg.InsureanceCurrency;
                    model.RefInfo = arg.RefInfo;
                    model.Supplier = arg.Supplier;
                    model.CustomsDuty = arg.CustomsDuty;
                    model.CustomsDutyType = arg.CustomsDutyType;
                    model.Form = arg.Form;
                    model.Currency = arg.Currency;
                    model.ExRate = arg.ExRate;
                    model.PackForwardtype = arg.PackForwardtype;
                    model.PackForward = arg.PackForward;
                    model.FormName = arg.FormName;
                    model.InsuranceDetails = arg.InsuranceDetails;
                    model.ServiceTax = arg.ServiceTax;
                    model.ServiceTaxType = arg.ServiceTaxType;
                    model.ServiceTaxNumbner = arg.ServiceTaxNumbner;
                    model.TickToCloseOrder = arg.TickToCloseOrder;
                    model.TaxType = arg.TaxType;
                    model.OrderClosedOn = arg.OrderClosedOn;

                    model.ISPO_cancelled = arg.ISPO_cancelled;
                    model.ISPO_cancelled_Reson = arg.ISPO_cancelled_Reson;
                    if (arg.IndentNoDirectPO != null && arg.IndentNoDirectPO != 0)
                    {
                        model.IndentNoDirectPO = arg.IndentNoDirectPO;
                    }
                    else
                    {
                        model.IndentNo = arg.IndentNo;
                    }
                    model.Category = arg.Category;
                    model.Groupname = arg.Groupname;
                    model.Material = arg.Material;
                    model.Description = arg.Description;
                    model.Machinename = arg.Machinename;
                    model.UOM = arg.UOM;
                    model.UOMValue = arg.UOMValue;
                    model.UOMType = arg.UOMType;
                    model.Color = arg.Color;
                    model.Qty = arg.Qty;
                    model.Substance = arg.Substance;
                    model.Weight = arg.Weight;
                    model.IONO = arg.IONO;
                    model.OtherSpecification = arg.OtherSpecification;
                    model.ReqdDate = arg.ReqdDate;
                    model.ETD = arg.ETD;
                    model.ETA = arg.ETA;
                    model.Rate = arg.Rate;
                    model.Dis = arg.Dis;
                    model.DisAmount = arg.DisAmount;
                    model.ExciseDutyAmount = arg.ExciseDutyAmount;
                    model.ExciseDuty = arg.ExciseDuty;
                    model.Exess = arg.Exess;
                    model.SHExess = arg.SHExess;
                    model.SHExessAmount = arg.SHExessAmount;
                    model.VAT = arg.VAT;
                    model.VATAmt = arg.VATAmt;
                    model.SurCharge = arg.SurCharge;
                    model.SurChargeAmt = arg.SurChargeAmt;
                    model.AmountTax = arg.AmountTax;
                    model.Remarks = arg.Remarks;
                    model.MRPMargin = arg.MRPMargin;
                    model.MRPPrice = arg.MRPPrice;
                    model.SupplierName = arg.SupplierName;
                    model.CompanyName = arg.CompanyName;
                    model.Address = arg.Address;
                    model.Phone = arg.Phone;
                    model.City = arg.City;
                    model.PaymentTerms = arg.PaymentTerms;
                    model.OtherTerms = arg.OtherTerms;
                    model.DeliverTerms = arg.DeliverTerms;
                    model.TermsConditions = arg.TermsConditions;
                    model.AccessibleValue = arg.AccessibleValue;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    PurchaseOrderRepository.Update(model);

                    MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                    EmailTemplate emailTemplate = new EmailTemplate();
                    emailTemplate = emailTemplateManager.GetTemplateName("Purchase order Update");
                    ItesmaterialName = bomMaterialListManager.GetMaterial(arg.Material.Value);
                    CompanyManager companyManager = new CompanyManager();
                    StoreMasterManager storeManager = new StoreMasterManager();
                    StoreMaster storeMaster = new StoreMaster();
                    List<Company> listCompany = new List<Company>();
                    listCompany = companyManager.Get();
                    
                    //if (emailTemplate != null)
                    //{
                    //    string contents = emailTemplate.Body;
                    //    MaterialManager materialManager = new MaterialManager();
                    //    contents = contents.Replace("[[PONo]]", !string.IsNullOrEmpty(arg.PoNo) ? arg.PoNo : string.Empty);
                    //    contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) ? ItesmaterialName.materialdescription : string.Empty : string.Empty);
                       
                    //    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    //    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
                    //    emailTemplate.Body = contents;
                    //    MMS.Common.EmailHelper.SendEmail(emailTemplate);
                    //}
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

        public bool SizeRangeDeliveryPost(PurchaseOrderDelierySchedule arg)
        {
            bool result = false;
            try
            {
                if (arg.IO == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    //arg.UpdatedBy = username;
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    PurchaseOrderDelieryScheduleRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    PurchaseOrderDelierySchedule model = PurchaseOrderDelieryScheduleRepository.Table.Where(m => m.IO == arg.IO).FirstOrDefault();
                    model.PoOrderID = arg.PoOrderID;
                    model.Material = arg.Material;
                    model.Quantity = arg.Quantity;
                    model.Date = arg.Date;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    PurchaseOrderDelieryScheduleRepository.Update(model);
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


        public bool SizeRangeDetailsPost(PurchaseOrderSizeRangeQuantity arg)
        {
            bool result = false;
            try
            {
                if (arg.PurchaseSizeRangeID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    //arg.UpdatedBy = username;
                    arg.CreatedBy = username;
                    arg.CreatedDate = DateTime.Now;
                    PurchaseOrderSizeRangeQuantityRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    PurchaseOrderSizeRangeQuantity model = PurchaseOrderSizeRangeQuantityRepository.Table.Where(m => m.PurchaseSizeRangeID == arg.PurchaseSizeRangeID).FirstOrDefault();
                    model.PoOrderID = arg.PoOrderID;
                    model.Size = arg.Size;
                    model.Quantity = arg.Quantity;
                    model.Rate = arg.Rate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;
                    PurchaseOrderSizeRangeQuantityRepository.Update(model);
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

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                PurchaseOrder model = PurchaseOrderRepository.GetById(id);
                PurchaseOrderRepository.Delete(model);
                MMS.Data.StoredProcedureModel.ItemMaterial ItesmaterialName = new MMS.Data.StoredProcedureModel.ItemMaterial();
                EmailTemplate emailTemplate = new EmailTemplate();
                emailTemplate = emailTemplateManager.GetTemplateName("Purchase order Delete");
                ItesmaterialName = bomMaterialListManager.GetMaterial(model.Material.Value);
                CompanyManager companyManager = new CompanyManager();               
                List<Company> listCompany = new List<Company>();
                listCompany = companyManager.Get();
                if (emailTemplate != null)
                {
                    string contents = emailTemplate.Body;
                    MaterialManager materialManager = new MaterialManager();
                    contents = contents.Replace("[[PONo]]", !string.IsNullOrEmpty(model.PoNo) ? model.PoNo : string.Empty);
                    contents = contents.Replace("[[MaterialName]]", ItesmaterialName != null ? !string.IsNullOrEmpty(ItesmaterialName.materialdescription) ? ItesmaterialName.materialdescription : string.Empty : string.Empty);
                    contents = contents.Replace("[[CompanyName]]", listCompany != null ? listCompany.Count > 0 ? listCompany.LastOrDefault().CompanyName.ToString() : string.Empty : string.Empty);
                    contents = contents.Replace("[[UserName]]", HttpContext.Current.Session["UserName"].ToString());
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

        public bool PurchaseOrderDelieryScheduleDelete(int PoOrderID)
        {
            bool result = false;
            try
            {
                List<PurchaseOrderDelierySchedule> DeliveryList = new List<PurchaseOrderDelierySchedule>();
                PurchaseOrderManager purchaserManager = new PurchaseOrderManager();
                DeliveryList = purchaserManager.PurchaseOrderDelieryScheduleGet().Where(x => x.PoOrderID == PoOrderID).ToList();
                foreach (var deleteItem in DeliveryList)
                {
                    PurchaseOrderDelierySchedule model = PurchaseOrderDelieryScheduleRepository.GetById(deleteItem.IO);
                    PurchaseOrderDelieryScheduleRepository.Delete(model);
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

        public bool PurchaseOrderSizeRangeQuantityDelete(int PoOrderID)
        {
            bool result = false;
            try
            {
                List<PurchaseOrderSizeRangeQuantity> sizeRangeQuantityList = new List<PurchaseOrderSizeRangeQuantity>();
                PurchaseOrderManager purchaserManager = new PurchaseOrderManager();
                sizeRangeQuantityList = purchaserManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == PoOrderID).ToList();
                foreach (var deleteItem in sizeRangeQuantityList)
                {
                    PurchaseOrderSizeRangeQuantity model = PurchaseOrderSizeRangeQuantityRepository.GetById(deleteItem.PurchaseSizeRangeID);
                    PurchaseOrderSizeRangeQuantityRepository.Delete(model);
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
    }
}
