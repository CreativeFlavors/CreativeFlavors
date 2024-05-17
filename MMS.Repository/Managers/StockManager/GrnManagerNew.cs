using MMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMS.Common;
using System.Web;
using MMS.Core.Entities.Stock;
using MMS.Data.StoredProcedureModel;

namespace MMS.Repository.Managers.StockManager
{
    public class GrnManagerNew
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<GRN_EntityModelNew> GrnRepository;
        private Repository<IssueSlip_MaterialDetails> IssueRepository;
        private Repository<GRNSizeQuantityObjectNew> GRNSizeRangeRepository;
        #region Helper Method

        public GrnManagerNew()
        {
            GrnRepository = unitOfWork.Repository<GRN_EntityModelNew>();
            GRNSizeRangeRepository = unitOfWork.Repository<GRNSizeQuantityObjectNew>();
        }
        public List<GRNSizeQuantityObjectNew> GRNSizeQuantityGet()
        {
            List<GRNSizeQuantityObjectNew> gRNSizeQuantityObject = new List<GRNSizeQuantityObjectNew>();
            try
            {
                gRNSizeQuantityObject = GRNSizeRangeRepository.Table.ToList<GRNSizeQuantityObjectNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gRNSizeQuantityObject;
        }
        public List<GRNSizeQuantityObjectNew> GetGRNSizeQuantity(int GrnID)
        {
            List<GRNSizeQuantityObjectNew> gRNSizeQuantityObject = new List<GRNSizeQuantityObjectNew>();
            try
            {
                gRNSizeQuantityObject = GRNSizeRangeRepository.Table.Where(x => x.Grnid_FK == GrnID).ToList<GRNSizeQuantityObjectNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return gRNSizeQuantityObject;
        }
        public GRN_EntityModelNew CheckDuplicate_GRN(int PONO, int Material)
        {
            GRN_EntityModelNew GrnObj = new GRN_EntityModelNew();
            if (Material != 0 && PONO != 0)
            {
                GrnObj = GrnRepository.Table.Where(x => x.MaterialId == PONO && x.Grn_MaterialID == Material).FirstOrDefault();
            }
            return GrnObj;
        }
        public bool GRNSizeRangeQuantityDelete(int GrnID)
        {
            bool result = false;
            try
            {
                List<GRNSizeQuantityObjectNew> sizeRangeQuantityList = new List<GRNSizeQuantityObjectNew>();
                GrnManagerNew grnManager = new GrnManagerNew();
                sizeRangeQuantityList = grnManager.GRNSizeQuantityGet().Where(x => x.Grnid_FK == GrnID).ToList();
                foreach (var deleteItem in sizeRangeQuantityList)
                {
                    GRNSizeQuantityObjectNew model = GRNSizeRangeRepository.GetById(deleteItem.GRNeSizeRangeID);
                    GRNSizeRangeRepository.Delete(model);
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
        public bool GRNSizeRangeDetailsPost(GRNSizeQuantityObjectNew arg)
        {
            bool result = false;
            try
            {
                if (arg.GRNeSizeRangeID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedDate = DateTime.Now;
                    arg.UpdatedDate = null;
                    GRNSizeRangeRepository.Insert(arg);
                    result = true;
                }
                else
                {
                    GRNSizeQuantityObjectNew model = GRNSizeRangeRepository.Table.Where(m => m.GRNeSizeRangeID == arg.GRNeSizeRangeID).FirstOrDefault();
                    model.Grnid_FK = arg.Grnid_FK;
                    model.Size = arg.Size;
                    model.quantity = arg.quantity;
                    model.Rate = arg.Rate;
                    model.UpdatedDate = DateTime.Now;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    GRNSizeRangeRepository.Update(model);
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
        public List<GRN_EntityModelNew> Get()
        {
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            try
            {
                GrnObjList = GrnRepository.Table.ToList<GRN_EntityModelNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }

        public List<GRN_EntityModelNew> GetGRNNO(int GRNNo)
        {
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.GrnNo == GRNNo).ToList<GRN_EntityModelNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public GRN_EntityModelNew GetGRNNewNO(int GRNNo)
        {
          GRN_EntityModelNew GrnObjList = new GRN_EntityModelNew();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.GrnNo == GRNNo).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModelNew> GetGRNPOQty(int PoOrderID)
        {
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.MaterialId == PoOrderID && x.PoNO != 0).OrderByDescending(x => x.CreatedDate).ToList<GRN_EntityModelNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public GRN_EntityModelNew GetGRN_PoDelete(int PoOrderID)
        {
            GRN_EntityModelNew GrnObjList = new GRN_EntityModelNew();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.PoNO == PoOrderID && x.PoNO != 0).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModelNew> isExistGRNNOWithMaterial(int MaterialID, int GRNNo)
        {
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.GrnNo == GRNNo && x.MaterialId == MaterialID).ToList<GRN_EntityModelNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public GRN_EntityModelNew GetGRNSelectedRow(int GrnID)
        {
            GRN_EntityModelNew GrnObj = new GRN_EntityModelNew();
            if (GrnID != 0)
            {
                GrnObj = GrnRepository.Table.Where(x => x.GrnID == GrnID).FirstOrDefault();
            }
            return GrnObj;
        }
        public GRN_EntityModelNew IsSupplierWithInvoice(string INVoiceNo, int SupplierNameId, int MaterialId, string DC_No)
        {
            GRN_EntityModelNew GrnObj = new GRN_EntityModelNew();
            GrnObj = GrnRepository.Table.Where(x => x.INVoiceNo == INVoiceNo && x.SupplierNameId == SupplierNameId && x.MaterialId == MaterialId && x.DC_No == DC_No).FirstOrDefault();

            return GrnObj;
        }
        public GRN_EntityModelNew IsSupplierWithInvoice_production(string INVoiceNo,  string DC_No)
        {
            GRN_EntityModelNew GrnObj = new GRN_EntityModelNew();
            GrnObj = GrnRepository.Table.Where(x => x.INVoiceNo == INVoiceNo   && x.DC_No == DC_No).FirstOrDefault();

            return GrnObj;
        }
        #endregion
        public List<ApprovedPrice> getApprovedPrice(string SupplierID, string MaterialID)
        {
            List<ApprovedPrice> list = new List<ApprovedPrice>();
            list = GrnRepository.getApprovedPrice(SupplierID, MaterialID);
            return list;
        }
        public List<GRN_EntityModelNew> GetPOQty(int PoOrderID, int materialID)
        {
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.MaterialId == PoOrderID && x.Grn_MaterialID == materialID && x.PoNO != 0).OrderByDescending(x => x.CreatedDate).ToList<GRN_EntityModelNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModelNew> GetGSTPOQty(int PoOrderID, int materialID)
        {
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.MaterialId == PoOrderID && x.Grn_MaterialID == materialID && x.PoNO != 0).OrderByDescending(x => x.CreatedDate).ToList<GRN_EntityModelNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModelNew> GetAutomaticPOQty(string PoOrderID, int materialID)
        {
            List<GRN_EntityModelNew> GrnObjList = new List<GRN_EntityModelNew>();
            try
            {
                GrnObjList = GrnRepository.Table.Where(x => x.AutomaticPONumber == PoOrderID && x.MaterialId == materialID && x.PoNO != 0).OrderByDescending(x => x.CreatedDate).ToList<GRN_EntityModelNew>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return GrnObjList;
        }
        public List<GRN_EntityModelNew> GetGrnGrid(string GrnNo)
        {
            List<GRN_EntityModelNew> contactlist = new List<GRN_EntityModelNew>();
            try
            {
               // contactlist = GrnRepository.SearchGrnList(GrnNo);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return contactlist;
        }


        public GRN_EntityModelNew Post(GRN_EntityModelNew arg)
        {
            bool result = false;
            GRN_EntityModelNew grn_EntityModel = new GRN_EntityModelNew();
            try
            {
                if (arg.GrnID == 0)
                {
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    arg.CreatedBy = username;
                    arg.UpdatedDate = null;
                    arg.UpdatedBy = string.Empty;
                    GrnRepository.Insert(arg);
                    grn_EntityModel = arg;
                }
                else
                {
                    GRN_EntityModelNew model = GrnRepository.Table.Where(m => m.GrnID == arg.GrnID).FirstOrDefault();
                    model.GrnID = arg.GrnID;
                    model.AmountplusTax = arg.AmountplusTax;
                    model.IONo = arg.IONo;
                    model.PoQty = arg.PoQty;
                    model.BarCodeNo = arg.BarCodeNo;
                    model.Transporter = arg.Transporter;
                    model.GrnNo = arg.GrnNo;
                  //  model.GateEntryNo = arg.GateEntryNo;
                    model.Grn_MaterialID = arg.Grn_MaterialID;
                    model.FreightAmount = arg.FreightAmount;
                    model.IndentMaterialId = arg.IndentMaterialId;
                    model.GrnDate = arg.GrnDate;
                    model.MaterialType = arg.MaterialType;
                    model.GEDate = arg.GEDate;
                    model.ServiceTaxPer = arg.ServiceTaxPer;
                    model.IndentNo = arg.IndentNo;
                    model.GETime = arg.GETime;
                    model.InsuranceAmount = arg.InsuranceAmount;
                    model.GrnRefNo = arg.GrnRefNo;
                    model.AutomaticPONumber = arg.AutomaticPONumber;
                    model.GateRefNo = arg.GateRefNo;
                    model.ShortageQty = arg.ShortageQty;
                    model.ShortageSQFT = arg.ShortageSQFT;
                    model.ShortagePCS = arg.ShortagePCS;
                    model.ShortageType = arg.ShortageType;
                    model.CustomsDuty = arg.CustomsDuty;
                    model.SupplierNameId = arg.SupplierNameId;
                    model.GrnType = arg.GrnType;
                    model.BENo = arg.BENo;
                    model.Pack_Forward = arg.Pack_Forward;
                    model.DC_No = arg.DC_No;
                    model.ST_VATcredit = arg.ST_VATcredit;
                    model.ServiceTax = arg.ServiceTax;
                    model.DCDate = arg.DCDate;
                    model.ImportClearance = arg.ImportClearance;
                    model.OtherCharges = arg.OtherCharges;
                    model.INVoiceNo = arg.INVoiceNo;
                    model.ExchangeRate = arg.ExchangeRate;
                    model.Transporter = arg.Transporter;
                    model.INVoiceDate = arg.INVoiceDate;
                    model.ShipmentMode = arg.ShipmentMode;
                    model.GeneralRemarks1 = arg.GeneralRemarks1;
                    model.PoNO = arg.PoNO;
                    model.LOTNo = arg.LOTNo;
                    model.IndentNo = arg.IndentNo;
                    model.Stores = arg.Stores;
                    model.MaterialId = arg.MaterialId;
                    model.Grade = arg.Grade;
                    model.GroupID = arg.GroupID;
                    model.CategoryId = arg.CategoryId;
                    model.IfGroupIsMaintenance = arg.IfGroupIsMaintenance;
                    model.ColourId = arg.ColourId;
                    model.Substance = arg.Substance;
                    model.QtyAsPerDc = arg.QtyAsPerDc;
                    model.QtyExcess = arg.QtyExcess;
                    model.UOMId = arg.UOMId;
                    model.Weight = arg.Weight;
                    model.ReceivedQty = arg.ReceivedQty;
                    model.DOM = arg.DOM;
                    model.AcceptedQty = arg.AcceptedQty;
                    model.PendingQty = arg.PendingQty;
                    model.StoreLocation = arg.StoreLocation;
                    model.Disp_SelectedMatOfPO = arg.Disp_SelectedMatOfPO;
                    model.Disp_AllPOBasedOnSelecMat = arg.Disp_AllPOBasedOnSelecMat;
                    model.GeneralRemarks = arg.GeneralRemarks;
                    model.Disp_AllMatOfPO = arg.Disp_AllMatOfPO;
                    model.Rate = arg.Rate;
                    model.Value = arg.Value;
                    model.MRPMargin = arg.MRPMargin;
                    model.MRPPrice = arg.MRPPrice;
                    model.AccessibleValue = arg.AccessibleValue;
                    model.Discount_Per = arg.Discount_Per;
                    model.Discount_Val = arg.Discount_Val;
                    model.Ecess_Per = arg.Ecess_Per;
                    model.Ecess_Val = arg.Ecess_Val;
                    model.MRPMargin = arg.MRPMargin;
                    model.ExciseDuty_Per = arg.ExciseDuty_Per;
                    model.ExciseDuty_Val = arg.ExciseDuty_Val;
                    model.SH_Ecess_Per = arg.SH_Ecess_Per;
                    model.SH_Ecess_Val = arg.SH_Ecess_Val;
                    model.ST_VAT_CST_Per = arg.ST_VAT_CST_Per;
                    model.ST_VAT_CST_Val = arg.ST_VAT_CST_Val;
                    model.Surcharge_Per = arg.Surcharge_Per;
                    model.Surcharge_Val = arg.Surcharge_Val;
                    model.ExcessApproval = arg.ExcessApproval;
                    model.RejectedQty = arg.RejectedQty;
                    model.RejectedPCS = arg.RejectedPCS;
                    model.RejectedSQFT = arg.RejectedSQFT;
                    model.RejectedType = arg.RejectedType;
                    model.TagsPiecesDiscount_Per = arg.TagsPiecesDiscount_Per;
                    model.TagsPiecesDiscount_Val = arg.TagsPiecesDiscount_Val;
                    model.UpdatedDate = DateTime.Now;
                    model.CreatedDate = arg.CreatedDate;
                    model.CreatedBy = arg.CreatedBy;
                    model.BuyerMasterId = arg.BuyerMasterId;
                    model.BuyerSeasonID = arg.BuyerSeasonID;
                    string username = HttpContext.Current.Session["UserName"].ToString();
                    model.UpdatedBy = username;

                    model.PoDate = arg.PoDate;
                    model.BEDate = arg.BEDate;
                    model.ModeofTransport = arg.ModeofTransport;
                    model.MachineryName = arg.MachineryName;
                    model.PoReceivedQty = arg.PoReceivedQty;
                    model.GST = arg.GST;
                    model.SGST = arg.SGST;
                    model.CGST = arg.CGST;
                    model.IGST = arg.IGST;
                    model.ImportIGST = arg.ImportIGST;
                    model.Surcharges = arg.Surcharges;
                    model.GSTValue = arg.GSTValue;
                    model.SGSTValue = arg.SGSTValue;
                    model.CGSTValue = arg.CGSTValue;
                    model.IGSTValue = arg.IGSTValue;
                    model.TCS = arg.TCS;
                    model.TCSValue = arg.TCSValue;
                    model.ImportIGSTValue = arg.ImportIGSTValue;
                    model.SurchargesValue = arg.SurchargesValue;
                    model.FreightAmtPercent = arg.FreightAmtPercent;
                    model.InsuranceAmtPercent = arg.InsuranceAmtPercent;
                    model.CustomDutyPercent = arg.CustomDutyPercent;
                    model.ClearingChargePercent = arg.ClearingChargePercent;
                    model.PackforwardPercent = arg.PackforwardPercent;
                    model.FreightTaxAmt = arg.FreightTaxAmt;
                    model.InsuranceTaxAmt = arg.InsuranceTaxAmt;
                    model.CustomDutyTax = arg.CustomDutyTax;
                    model.ClearingChargeTax = arg.ClearingChargeTax;
                    model.PackforwardTax = arg.PackforwardTax;
                    model.ValueInRps = arg.ValueInRps;
                    model.Currencykey = arg.Currencykey;
                    model.TotalAmount = arg.TotalAmount;
                    model.NetTotal = arg.NetTotal;
                    model.TotalCost = arg.TotalCost;
                    model.TagsPiecesDiscount = arg.TagsPiecesDiscount;
                    model.ServiceChargeTax = arg.ServiceChargeTax;

                    model.ReceivedQtysqf = arg.ReceivedQtysqf;
                    model.ReceivedQtyPsc = arg.ReceivedQtyPsc;
                    model.ShortageQtysqf = arg.ShortageQtysqf;
                    model.ShortageQtyPsc = arg.ShortageQtyPsc;
                    model.RejectedQtysqf = arg.RejectedQtysqf;
                    model.RejectedQtyPsc = arg.RejectedQtyPsc;
                    model.AcceptedQtysqf = arg.AcceptedQtysqf;
                    model.AcceptedQtyPsc = arg.AcceptedQtyPsc;
                    model.byprocuct_kg = arg.byprocuct_kg;
                    model.byprocuct_sqf = arg.byprocuct_sqf;
                    model.byprocuct_psc = arg.byprocuct_psc;
                    model.cuttable_per = arg.cuttable_per;

                    GrnRepository.Update(model);
                    grn_EntityModel = arg;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return grn_EntityModel;
        }

        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                GRN_EntityModelNew model = GrnRepository.GetById(id);
                GrnRepository.Delete(model);
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
