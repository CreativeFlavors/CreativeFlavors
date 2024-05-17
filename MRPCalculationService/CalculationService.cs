using log4net;
using MMS.Repository.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using MMS.Core.Entities;
using MMS.Repository.Managers.StockManager;
using MMS.Core.Entities.Stock;
using System.Configuration;
using System.Net.Mail;

namespace MRPCalculationService
{
    public class CalculationService : IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger("RollingFileAppender");

        public bool CalculateMRP()
        {
            log.Info("MRP Calculation Start at : " + DateTime.Now.ToString());

            try
            {
                SimpleMRPManager simpleMRPManager = new SimpleMRPManager();

                var MRPLists = simpleMRPManager.GetMRPForCalculation();

                if (MRPLists == null || MRPLists.Count == 0)
                {
                    log.Info("No MRPs Available For Calculation");
                    return true;
                }

                foreach (var simpleMRP in MRPLists)
                {
                    var IsCalculated = PerformCalculation(simpleMRP);

                    if (IsCalculated)
                    {
                        simpleMRP.IsCalculated = true;
                        simpleMRPManager.CalculateMRPPut(simpleMRP);

                        // NEED TO SEND NOTIFICATION TO THE USERS HERE
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error on CalculateMRP : " + ex.Message);
                return true;
            }

            log.Info("MRP Calculation Compeleted at : " + DateTime.Now.ToString());

            return true;
        }

        private bool PerformCalculation(CalculateMRP simpleMRP)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            InternalOrderForm order = new InternalOrderForm();
            BillOfMaterialManager billOfMaterialManager = new BillOfMaterialManager();
            BOMMaterialListManager bomMaterialListManager = new BOMMaterialListManager();
            Bom billOfMaterial = new Bom();
            List<bomgriddetail> listMaterial = new List<bomgriddetail>();
            List<BOMMaterial> bomMaterialList = new List<BOMMaterial>();
            BOMMaterial bomMaterial = new BOMMaterial();
            string Message = "";
            string OrderMessage = "";
            string BOMMessage = "";
            string SaveBOMMessage = "";
            SimpleMRPManager simpleMRPManager = new SimpleMRPManager();
            BuyerOrderCreationManager buyerOrderCreationManager = new BuyerOrderCreationManager();
            try
            {
                var SelectedValues = "" + simpleMRPManager.GetMRPSelectedValuesBy(simpleMRP.MRPIdToCalculate);

                foreach (var selectedValue in SelectedValues)
                {
                    MRPSelectedValues selectedValues = new MRPSelectedValues();
                    BuyerOrderCreation buyerOrderCreation = new BuyerOrderCreation();

                    order = buyerOrderEntryManager.GetBuyerOderSlNo(selectedValue.IONumberID);
                    if (order != null && order.OrderEntryId != 0)
                    {
                        billOfMaterial = billOfMaterialManager.getLinkBomNumber(order.OurStyle);
                    }
                    MRPSelectedValues mrpselectedValues = new MRPSelectedValues();
                    mrpselectedValues = simpleMRPManager.checkOrderNumber(selectedValue.IONumberID);
                    if (billOfMaterial != null)
                    {
                        billOfMaterialManager.GetBomMaterialBOMID(billOfMaterial.BomId);
                        bomMaterialList = billOfMaterialManager.GetBomMaterialBOMID(billOfMaterial.BomId);
                    }
                    SizeScheduleMasterManager sizeScheduleMasterManager = new SizeScheduleMasterManager();
                    MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                    if (order != null && billOfMaterial != null)
                    {
                        foreach (var each in bomMaterialList)
                        {
                            if (each.MaterialName == 551)
                            {
                                string message = "";
                            }
                            MRPRequirement mrpRequirement = new MRPRequirement();
                            mrpRequirement.RequiredQty = 0;
                            decimal? Amount = 0;
                            decimal? qty = 0;
                            BOMMaterial bomMaterial_ = new BOMMaterial();
                            materialgroupmaster materialGroupMaster = new materialgroupmaster();
                            SizeScheduleRange sizeScheduleRange = new SizeScheduleRange();
                            MaterialManager materialManager = new MaterialManager();
                            MaterialMaster materialMaster = new MaterialMaster();
                            materialMaster = materialManager.GetMaterialMasterId(each.MaterialName);
                            SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                            materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(each.MaterialGroupMasterId);
                            List<SizeRangeQtyRate> sizeRangeQtyRateList = new List<SizeRangeQtyRate>();

                            if (materialGroupMaster.IsSize == true)
                            {
                                if (((each.SizeScheduleMasterId != null && each.SizeScheduleMasterId != 0) && (each.SizeRangeMasterID == null || each.SizeRangeMasterID == 0)))
                                {
                                    List<SizeScheduleRange> listSizeScheduleRange = new List<SizeScheduleRange>();
                                    List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();
                                    listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(each.BOMMaterialID);
                                    sizeScheduleRange = sizeScheduleMasterManager.GetSizeScheduleID(each.SizeScheduleMasterId.Value);
                                    listSizeScheduleRange = sizeScheduleMasterManager.GetSizeRange(each.SizeScheduleMasterId.Value);

                                    if (sizeScheduleRange != null)
                                    {
                                        sizeRangeQtyRateList = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(order.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                                        var OrderEntryList = (from w1 in sizeRangeQtyRateList
                                                              join w2 in listSizeScheduleRange on Convert.ToDecimal(w1.SizeRange) equals w2.Size
                                                              select new { w1, w2 }).ToList();


                                        var result = sizeRangeQtyRateList.Where(p => !listSizeScheduleRange.Any(p2 => p2.Size == Convert.ToDecimal(p.SizeRange)));


                                        foreach (var iteration_ in result)
                                        {

                                            List<SizeRangeQtyRate> rangeQtyRateList = new List<SizeRangeQtyRate>();
                                            BomSizeMatching bomSizeMatching = new BomSizeMatching();
                                            bomSizeMatching.Size = Convert.ToDecimal(iteration_.SizeRange);
                                            bomSizeMatching.BomMaterialID = each.BOMMaterialID;
                                            bomSizeMatching.Frame = Convert.ToDecimal(iteration_.SizeRange);
                                            bomSizeMatching.Qty = iteration_.Qty;
                                            bomSizeMatching.Rate = each.Rate;
                                            bomSizeMatching.SizeScheduleMasterID = sizeScheduleRange.SizeScheduleMasterID;
                                            billOfMaterialManager.BOMSizeMatchingPost(bomSizeMatching);
                                        }
                                        foreach (var iteration in OrderEntryList)
                                        {
                                            List<SizeRangeQtyRate> rangeQtyRateList = new List<SizeRangeQtyRate>();
                                            BomSizeMatching bomSizeMatching = new BomSizeMatching();
                                            string[] FrameArray = iteration.w2.Frame.Split('-');
                                            var FrameQTYList = sizeRangeQtyRateList.Where(x => Convert.ToDecimal(x.SizeRange) >= Convert.ToDecimal(FrameArray[0]) && Convert.ToDecimal(x.SizeRange) <= Convert.ToDecimal(FrameArray[1])).ToList();
                                            decimal? TotalQTY = FrameQTYList.Sum(x => x.Qty);
                                            bomSizeMatching.Size = Convert.ToDecimal(iteration.w1.SizeRange);
                                            bomSizeMatching.BomMaterialID = each.BOMMaterialID;
                                            bomSizeMatching.Frame = Convert.ToDecimal(iteration.w1.SizeRange);
                                            bomSizeMatching.Qty = TotalQTY;
                                            bomSizeMatching.Rate = each.Rate;
                                            bomSizeMatching.SizeScheduleMasterID = sizeScheduleRange.SizeScheduleMasterID;
                                            billOfMaterialManager.BOMSizeMatchingPost(bomSizeMatching);
                                        }
                                        mrpRequirement.Size = sizeScheduleRange.Size.ToString();
                                        mrpRequirement.RequiredQty = sizeRangeQtyRateList.Sum(x => x.Qty);
                                        if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                                        {
                                            if (materialMaster.IsSecondaryPackage == true)
                                            {
                                                qty = mrpRequirement.RequiredQty;
                                                materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                                                if (materialMaster.PurchasePacket != 0)
                                                {
                                                    mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                                                }
                                                else
                                                {
                                                    mrpRequirement.RequiredQty = qty;
                                                }
                                                mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                                            }

                                        }

                                    }
                                    else if (listDisplaySizeMaterial != null && listDisplaySizeMaterial.Count > 0)
                                    {
                                        var displaySize = sizeRangeQtyRateList.Where(p => !listDisplaySizeMaterial.Any(p2 => Convert.ToDecimal(p2.SizeRange) == Convert.ToDecimal(p.SizeRange)));
                                    }
                                }
                                else if (((each.SizeScheduleMasterId != null && each.SizeScheduleMasterId != 0) && (each.SizeRangeMasterID != null && each.SizeRangeMasterID != 0)))
                                {
                                    List<SizeScheduleRange> listSizeScheduleRange = new List<SizeScheduleRange>();
                                    sizeRangeQtyRateList = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(order.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();
                                    listSizeScheduleRange = sizeScheduleMasterManager.GetSizeRange(each.SizeScheduleMasterId.Value);


                                    mrpRequirement.RequiredQty = sizeRangeQtyRateList.Sum(x => x.Qty);
                                    if (materialMaster != null && materialMaster.MaterialMasterId != 0)
                                    {
                                        if (materialMaster.IsSecondaryPackage == true)
                                        {
                                            qty = mrpRequirement.RequiredQty;
                                            materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                                            if (materialMaster.PurchasePacket != 0)
                                            {
                                                mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                                            }
                                            else
                                            {
                                                mrpRequirement.RequiredQty = qty;
                                            }
                                            mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                                        }

                                    }
                                }
                                else if (each.SizeRangeMasterID != null && each.SizeRangeMasterID != 0)
                                {
                                    List<SizeScheduleRange> listSizeScheduleRange = new List<SizeScheduleRange>();
                                    List<DisplaySizeMaterial> listDisplaySizeMaterial = new List<DisplaySizeMaterial>();

                                    listDisplaySizeMaterial = bomMaterialListManager.DisplaySizeMaterialGet(each.BOMMaterialID);
                                    listDisplaySizeMaterial = listDisplaySizeMaterial.Where(x => x.SizeIsChecked == true).ToList();
                                    sizeRangeQtyRateList = sizeRangeQtyRateManager.GetSizeRangeByOrderEntryId(order.OrderEntryId).OrderBy(x => Convert.ToDecimal(x.SizeRange)).ToList();

                                    if (listDisplaySizeMaterial != null && listDisplaySizeMaterial.Count > 0)
                                    {
                                        var displaySize = sizeRangeQtyRateList.Where(p => listDisplaySizeMaterial.Any(p2 => Convert.ToDecimal(p2.SizeRange) == Convert.ToDecimal(p.SizeRange)));
                                        string endurea = System.Configuration.ConfigurationManager.AppSettings["EnduraURL"].ToString();
                                        string host = simpleMRP.HostName;//System.Web.HttpContext.Current.Request.Url.Host.ToString();
                                        string Hostname = "http://" + host;
                                        if (!string.IsNullOrEmpty(endurea) && Hostname == endurea)
                                        {
                                            mrpRequirement.RequiredQty = displaySize.Sum(x => x.Qty) * Convert.ToDecimal(each.BuyerNorms);
                                        }
                                        else
                                        {
                                            mrpRequirement.RequiredQty = displaySize.Sum(x => x.Qty) * Convert.ToDecimal(each.TotalNorms);
                                        }

                                    }
                                }
                                else
                                {
                                    string endurea = System.Configuration.ConfigurationManager.AppSettings["EnduraURL"].ToString();
                                    string host = simpleMRP.HostName;
                                    string Hostname = "http://" + host;
                                    if (!string.IsNullOrEmpty(endurea) && Hostname == endurea)
                                    {
                                        Amount = (Convert.ToDecimal(each.BuyerNorms) * Convert.ToDecimal(order.TotalAmount));
                                    }
                                    else
                                    {
                                        Amount = (Convert.ToDecimal(each.TotalNorms) * Convert.ToDecimal(order.TotalAmount));
                                    }

                                    qty = Amount;
                                    materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                                    if (materialMaster.PurchasePacket != 0)
                                    {
                                        mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                                    }
                                    else
                                    {
                                        mrpRequirement.RequiredQty = qty;
                                    }
                                    mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                                }

                            }
                            else
                            {

                                string endurea = System.Configuration.ConfigurationManager.AppSettings["EnduraURL"].ToString();
                                string host = simpleMRP.HostName;
                                string Hostname = "http://" + host;
                                if (!string.IsNullOrEmpty(endurea) && Hostname == endurea)
                                {
                                    Amount = Convert.ToDecimal(each.BuyerNorms) * order.TotalAmount;
                                }
                                else
                                {
                                    Amount = Convert.ToDecimal(each.TotalNorms) * order.TotalAmount;
                                }

                                qty = Amount;
                                materialMaster.PurchasePacket = materialMaster.PurchasePacket != null ? materialMaster.PurchasePacket : 0;
                                if (materialMaster.PurchasePacket != 0)
                                {
                                    mrpRequirement.RequiredQty = qty / materialMaster.PurchasePacket;
                                }
                                else
                                {
                                    mrpRequirement.RequiredQty = qty;
                                }
                                mrpRequirement.RequiredQty = Math.Round(mrpRequirement.RequiredQty.Value, 2, MidpointRounding.AwayFromZero);
                            }
                            mrpRequirement.IsMRP = true;
                            mrpRequirement.ParentCommonBOMID = each.ParentCommonBOMID;
                            mrpRequirement.ParentBOMID = each.ParentBOMID;
                            mrpRequirement.BOMID = each.BOMID;
                            mrpRequirement.MaterialCategoryMasterId = each.MaterialCategoryMasterId;
                            mrpRequirement.MaterialGroupMasterId = each.MaterialGroupMasterId;
                            mrpRequirement.SubstanceMasterId = each.SubstanceMasterId;
                            mrpRequirement.MaterialName = each.MaterialName;
                            mrpRequirement.Conversion = each.Conversion;
                            mrpRequirement.ColorMasterId = each.ColorMasterId;
                            mrpRequirement.SizeScheduleMasterId = each.SizeScheduleMasterId;
                            mrpRequirement.SampleNorms = each.SampleNorms;
                            mrpRequirement.Wastage = each.Wastage;
                            mrpRequirement.SupplierMasterID = each.SupplierMasterID;
                            mrpRequirement.WastageQty = each.WastageQty;
                            mrpRequirement.TotalNorms = each.TotalNorms;
                            mrpRequirement.BuyerNorms = each.BuyerNorms;
                            mrpRequirement.Rate = Convert.ToDecimal(materialMaster.Price);
                            mrpRequirement.SizeRangeMasterID = each.SizeRangeMasterID;
                            mrpRequirement.NoofSets = each.NoofSets;
                            mrpRequirement.BuyerNorms = each.BuyerNorms;
                            mrpRequirement.WastageQtyUOM = each.WastageQtyUOM;
                            mrpRequirement.TotalNormsUOM = each.TotalNormsUOM;
                            mrpRequirement.SimpleMRPID = selectedValue.SimpleMRPID;
                            mrpRequirement.OrderNo = order.BuyerOrderSlNo;
                            mrpRequirement.CreatedBy = simpleMRP.CreatedBy;
                            bomMaterialListManager.MRPRequirementPost(mrpRequirement);
                            SaveBOMMessage = "Saved Successfully";
                        }
                    }
                    // }
                }
                string SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
                string SMTPPort = ConfigurationManager.AppSettings["SMTPPort"];
                string smtpUserName = ConfigurationManager.AppSettings["SMTPUserName"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];
                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress(smtpUserName);    // Sender e-mail address. 
                Msg.To.Add("selvarj100@gmail.com");                     // Recipient e-mail address.
                Msg.Subject = "Simple MRP Calculation";
                Msg.Body = "MRP no:" + simpleMRP.MRPCalculateID + " Calculation is completed";
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = Convert.ToInt32(SMTPPort);
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                smtp.EnableSsl = true;
                smtp.Send(Msg);


            }
            catch (Exception ex)
            {
                log.Error("Error on Perform Calculation for Calculate ID : " + simpleMRP.MRPCalculateID.ToString() + " " + ex.Message);
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
