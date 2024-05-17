using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using Newtonsoft.Json;
using MMS.Common;
using System.Globalization;
using static MMS.Web.ExtensionMethod.HtmlHelper;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class PurchaseOrderController : Controller
    {
        #region Curd Operation
        [HttpPost]
        public ActionResult PurchaseOrderSerialize(FormCollection collection, string IndentNo)
        {
            bool Result = false;
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            IndentMaterials indentMaterials_ = new IndentMaterials();
            PurchaseOrder GetpurchaseOrderID = new PurchaseOrder();
            PurchaseOrder purchaseOrder_ = new PurchaseOrder();
            string Message = "";
            IndentMaterials indentMaterials = new IndentMaterials();
            if (!string.IsNullOrEmpty(collection["IndentMaterialID"].ToString()))
            {
                indentMaterials = indentMaterialManager.GetIndentMaterialId(Convert.ToInt32(collection["IndentMaterialID"]));
            }
            if (indentMaterials != null && indentMaterials.IndentMaterialID != 0 && indentMaterials.IsPo == true && collection["PoOrderId"] == null)
            {
                return Json(new { Response = "This material is already PO is raised." }, JsonRequestBehavior.AllowGet);
            }
            var mat = collection["Material"];
            var pn = collection["PoNo"];
            purchaseOrder_ = purchaseOrderManager.GetPoExist(collection["PoNo"], Int32.Parse(collection["Material"]));
            if (purchaseOrder_ != null && purchaseOrder_.PoOrderId != 0 && collection["PoOrderId"] == null)
            {
                Message = "Alreday Exist";
                return Json(new { Response = Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (collection["PoOrderId"] != null && collection["PoOrderId"] != "" && collection["PoOrderId"] != "0")
                {
                    purchaseOrder.PoOrderId = Convert.ToInt32(collection["PoOrderId"]);
                    if (purchaseOrder.PoOrderId != 0)
                    {
                        GetpurchaseOrderID = purchaseOrderManager.GetPoOrderId(purchaseOrder.PoOrderId);
                    }

                }
                int indentMaterialID = 0;
                if (purchaseOrder.PoOrderId != 0)
                {
                    if (GetpurchaseOrderID != null && GetpurchaseOrderID.IndentMaterialID != null && GetpurchaseOrderID.IndentMaterialID != 0)
                    {
                        indentMaterialID = GetpurchaseOrderID.IndentMaterialID.Value;
                        purchaseOrder.IndentMaterialID = indentMaterialID;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(collection["IndentNoDirectPO"]))
                    {
                        if (collection["IndentMaterialID"] != null && collection["IndentMaterialID"] != "")
                        {
                            indentMaterialID = Convert.ToInt32(collection["IndentMaterialID"]);
                        }
                        else
                        {
                            indentMaterialID = 0;
                        }

                        purchaseOrder.IndentMaterialID = indentMaterialID;
                    }
                }
                purchaseOrder.PoNo = collection["PoNo"];
                if (!string.IsNullOrEmpty(collection["Material"].ToString()))
                {
                    purchaseOrder.Material = Int32.Parse(collection["Material"]);
                }
                else
                {
                    purchaseOrder.Material = 0;
                }
                if (!string.IsNullOrEmpty(collection["ServiceTaxPer"].ToString()))
                {
                    purchaseOrder.ServiceTaxPer = Convert.ToDecimal(collection["ServiceTaxPer"]);
                }
                else
                {
                    purchaseOrder.ServiceTaxPer = 0;
                }
                if (!string.IsNullOrEmpty(collection["UOMValue1"].ToString()))
                {
                    purchaseOrder.UOMValue1 = Convert.ToDecimal(collection["UOMValue1"]);
                }
                else
                {
                    purchaseOrder.UOMValue1 = 0;
                }
                if (!string.IsNullOrEmpty(collection["FreightCostTotal"].ToString()))
                {
                    purchaseOrder.FreightCostTotal = Convert.ToDecimal(collection["FreightCostTotal"]);
                }
                else
                {
                    purchaseOrder.FreightCostTotal = 0;
                }
                purchaseOrder.Unit = new Guid(collection["UnitId"]);
                if (collection["FreightAmt"] != "")
                {
                    purchaseOrder.FreightAmt = collection["FreightAmt"];
                }
                if (collection["FreightType"] != "")
                {
                    purchaseOrder.FreightType = Convert.ToInt32(collection["FreightType"]);
                }
                if (collection["Category"] != "")
                {
                    purchaseOrder.Category = Convert.ToInt32(collection["Category"]);
                }
                if (collection["Groupname"] != "")
                {
                    purchaseOrder.Groupname = Convert.ToInt32(collection["Groupname"]);
                }
                purchaseOrder.LastAmendmentNo = collection["LastAmendmentNo"];
                if (collection["MaterialType"] != "" && collection["MaterialType"] != null)
                {
                    purchaseOrder.MaterialType = Convert.ToInt32(collection["MaterialType"]);
                }
                if (collection["LastAmendmentDate"] != "")
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    string[] LastAmendmentDateString = collection["LastAmendmentDate"].ToString().Split('/');
                    DateTime? LastAmendmentDateString_date = Convert.ToDateTime(LastAmendmentDateString[1] + "/" + LastAmendmentDateString[0] + "/" + LastAmendmentDateString[2], culture);
                    purchaseOrder.LastAmendmentDate = LastAmendmentDateString_date;
                }
                else
                {
                    purchaseOrder.LastAmendmentDate = null;
                }
                if (collection["PoDate"] != "")
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    string[] PoDateString = collection["PoDate"].ToString().Split('/');
                    string PoDateStr = PoDateString[1] + "/" + PoDateString[0] + "/" + PoDateString[2];
                    purchaseOrder.PoDate = Convert.ToDateTime(PoDateStr.Trim(), culture);
                }
                else
                {
                    purchaseOrder.PoDate = null;
                }
                if (collection["StockValue"] != "")
                {
                    purchaseOrder.StockValue = Convert.ToDecimal(collection["StockValue"]);
                }
                else
                {
                    purchaseOrder.StockValue = 0;
                }
                if (collection["FrightType"] != "")
                {
                    purchaseOrder.FrightType = Convert.ToInt32(Convert.ToDecimal(collection["FrightType"]));
                }
                else
                {
                    purchaseOrder.FrightType = null;
                }
                purchaseOrder.PoType = collection["PoType"];
                if (collection["InsuranceAmount"] != "")
                {
                    purchaseOrder.InsuranceAmount = collection["InsuranceAmount"];
                }
                else
                {
                    purchaseOrder.InsuranceAmount = null;
                }
                if (collection["InsureanceCurrency"] != "")
                {
                    purchaseOrder.InsureanceCurrency = Convert.ToInt32(collection["InsureanceCurrency"]);
                }
                if (collection["RefInfo"] != "")
                {
                    purchaseOrder.RefInfo = collection["RefInfo"];
                }
                purchaseOrder.OrderType = collection["OrderType"];
                if (collection["Supplier"] != "")
                {
                    purchaseOrder.Supplier = Convert.ToInt32(collection["Supplier"]);
                }

                else
                {
                    purchaseOrder.Supplier = null;
                }
                if (collection["CustomsDuty"] != "")
                {
                    purchaseOrder.CustomsDuty = Convert.ToInt32(collection["CustomsDuty"]);
                }
                else
                {
                    purchaseOrder.CustomsDuty = null;
                }
                if (collection["CustomsDutyType"] != "")
                {
                    purchaseOrder.CustomsDutyType = Convert.ToString(collection["CustomsDutyType"]);
                }
                if (collection["Form"] != "")
                {
                    purchaseOrder.Form = Convert.ToInt32(collection["Form"]);
                }
                if (collection["Currency"] != "")
                {
                    purchaseOrder.Currency = Convert.ToInt32(collection["Currency"]);
                }
                else
                {
                    purchaseOrder.Currency = null;
                }
                if (collection["ExRate"] != "")
                {
                    purchaseOrder.ExRate = Convert.ToDecimal(collection["ExRate"]);
                }
                else
                {
                    purchaseOrder.ExRate = 0;
                }
                if (collection["PackForwardtype"] != "")
                {
                    purchaseOrder.PackForwardtype = Convert.ToInt32(collection["PackForwardtype"]);
                }
                else
                {
                    purchaseOrder.PackForwardtype = null;
                }
                if (collection["PackForward"] != "")
                {
                    purchaseOrder.PackForward = collection["PackForward"];
                }
                purchaseOrder.FormName = collection["FormName"];
                purchaseOrder.PoQty = Convert.ToDecimal(collection["PoQty"]);
                purchaseOrder.PendingQty = !string.IsNullOrEmpty(collection["PendingQty"].ToString()) ? Convert.ToDecimal(collection["PendingQty"]) : 0;
                purchaseOrder.InsuranceDetails = collection["InsuranceDetails"];
                if (collection["ServiceTax"] != "")
                {
                    purchaseOrder.ServiceTax = collection["ServiceTax"];
                }
                else
                {
                    purchaseOrder.ServiceTax = null;
                }
                purchaseOrder.ServiceTaxType = Convert.ToInt32(collection["ServiceTaxType"]);
                string[] indentno = null;


                purchaseOrder.ServiceTaxNumbner = Convert.ToDecimal(collection["ServiceTaxNumbner"].ToString());
                string b = collection["TickToCloseOrder"];
                purchaseOrder.TickToCloseOrder = (collection["TickToCloseOrder"] ?? "").Equals("true", StringComparison.CurrentCultureIgnoreCase);
                purchaseOrder.TaxType = Convert.ToInt32(collection["TaxType"]);
                purchaseOrder.OrderClosedOn = null;
                if (string.IsNullOrEmpty(collection["IndentNoDirectPO"]))
                {
                    string indent_id = (collection["IndentNo"]);
                    indentno = indent_id.TrimEnd(',').Split(',');
                    indentno = indentno.ToArray().Distinct().ToArray();
                    string ClubIndentid = "";
                    foreach (var item in indentno)
                    {
                        ClubIndentid += item + ",";
                    }
                    purchaseOrder.IndentNo = ClubIndentid.TrimEnd(',');
                }
                if (collection["MachinaryMaterialID"] != null && collection["MachinaryMaterialID"] != "")
                {
                    purchaseOrder.Machinename = Convert.ToInt32(collection["MachinaryMaterialID"]);
                }
                else
                {
                    purchaseOrder.Machinename = 0;
                }
                if (collection["UOM"] != null && collection["UOM"] != "")
                {
                    purchaseOrder.UOM = Convert.ToInt32(collection["UOM"]);
                }
                if (collection["UOMValue"] != null && collection["UOMValue"] != "")
                {
                    purchaseOrder.UOMValue = Convert.ToDecimal(collection["UOMValue"]);
                }
                else
                {
                    purchaseOrder.UOMValue = 0;
                }
                if (collection["UOMType"] != "")
                {
                    purchaseOrder.UOMType = Convert.ToInt32(collection["UOMType"]);
                }
                else
                {
                    purchaseOrder.UOMType = null;
                }

                if (collection["Color"] == "")
                {
                    purchaseOrder.Color = null;
                }
                else
                {
                    purchaseOrder.Color = Convert.ToInt32(collection["Color"]);
                }
                if (collection["Qty"] == "")
                {
                    purchaseOrder.Qty = null;
                }
                else
                {
                    purchaseOrder.Qty = Convert.ToDecimal(collection["Qty"]);
                }
                if (collection["PaymentTerms"] == "" && collection["PaymentTerms"] == null)
                {
                    purchaseOrder.PaymentTerms = null;
                }
                else
                {
                    purchaseOrder.PaymentTerms = collection["PaymentTerms"];
                }

                if (collection["DeliverTerms"] == "" && collection["DeliverTerms"] == null)
                {
                    purchaseOrder.DeliverTerms = null;
                }
                else
                {
                    purchaseOrder.DeliverTerms = collection["DeliverTerms"];
                }


                if (collection["OtherTerms"] == "" && collection["OtherTerms"] == null)
                {
                    purchaseOrder.OtherTerms = null;
                }
                else
                {
                    purchaseOrder.OtherTerms = collection["OtherTerms"];
                }
                if (collection["TermsConditions"] == "" && collection["TermsConditions"] == null)
                {
                    purchaseOrder.TermsConditions = null;
                }
                else
                {
                    purchaseOrder.TermsConditions = collection["TermsConditions"];
                }

                if (collection["SupplierName"] == "" && collection["SupplierName"] == null)
                {
                    purchaseOrder.SupplierName = null;
                }
                else
                {
                    purchaseOrder.SupplierName = collection["SupplierName"];
                }

                if (collection["CompanyName"] == "" && collection["CompanyName"] == null)
                {
                    purchaseOrder.CompanyName = null;
                }
                else
                {
                    purchaseOrder.CompanyName = collection["CompanyName"];
                }

                if (collection["Address"] == "" && collection["Address"] == null)
                {
                    purchaseOrder.Address = null;
                }
                else
                {
                    purchaseOrder.Address = collection["Address"];
                }

                if (collection["City"] == "" && collection["City"] == null)
                {
                    purchaseOrder.City = null;
                }
                else
                {
                    purchaseOrder.City = collection["City"];
                }


                if (collection["Phone"] == "" && collection["Phone"] == null)
                {
                    purchaseOrder.Phone = null;
                }
                else
                {
                    purchaseOrder.Phone = collection["Phone"];
                }




                if (collection["Substance"] != "")
                {
                    purchaseOrder.Substance = Convert.ToInt32(collection["Substance"]);
                }
                purchaseOrder.Weight = Convert.ToDecimal(collection["Weight"]);
                if (string.IsNullOrEmpty(collection["OrderType"]))
                {
                    purchaseOrder.IONO = Convert.ToInt32(collection["IONO"]);
                }

                purchaseOrder.OtherSpecification = collection["OtherSpecification"];
                if (collection["ReqdDate"] != "")
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    string[] dateString = collection["ReqdDate"].ToString().Split('/');
                    DateTime? enter_date = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2], culture);
                    purchaseOrder.ReqdDate = enter_date;
                }
                else
                {
                    purchaseOrder.ReqdDate = null;
                }
                if (collection["ETD"] != "")
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    string[] ETDString = collection["ETD"].ToString().Split('/');
                    DateTime? ETDString_date = Convert.ToDateTime(ETDString[1] + "/" + ETDString[0] + "/" + ETDString[2], culture);
                    purchaseOrder.ETD = ETDString_date;
                }
                else
                {
                    purchaseOrder.ETD = null;
                }
                if (collection["ETA"] != "")
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    string[] ETAString = collection["ETA"].ToString().Split('/');
                    DateTime? ETAStringString_date = Convert.ToDateTime(ETAString[1] + "/" + ETAString[0] + "/" + ETAString[2], culture);
                    purchaseOrder.ETA = ETAStringString_date;
                }
                else
                {
                    purchaseOrder.ETA = null;
                }




                if (collection["Rate_"] != null && collection["Rate_"] != "")
                {
                    purchaseOrder.Rate = collection["Rate_"];
                }
                else
                {
                    purchaseOrder.Rate = "";
                }

                purchaseOrder.Dis = collection["Dis"];
                if (collection["DisAmount"] != "")
                {
                    purchaseOrder.DisAmount = Decimal.Parse(collection["DisAmount"]);
                }
                else
                {
                    purchaseOrder.DisAmount = 0;
                }
                if (collection["ExciseDutyAmount"] != "")
                {
                    purchaseOrder.ExciseDutyAmount = Decimal.Parse(collection["ExciseDutyAmount"]);
                }
                else
                {
                    purchaseOrder.ExciseDutyAmount = 0;
                }
                if (collection["ExciseDuty"] != "")
                {
                    purchaseOrder.ExciseDuty = Decimal.Parse(collection["ExciseDuty"]);
                }
                else
                {
                    purchaseOrder.ExciseDuty = null;
                }
                if (collection["Exess"] != "")
                {
                    purchaseOrder.Exess = Convert.ToString(collection["Exess"]);
                }
                else
                {
                    purchaseOrder.Exess = null;
                }
                if (collection["SHExess"] != "")
                {
                    purchaseOrder.SHExess = Convert.ToString(collection["SHExess"]);
                }
                else
                {
                    purchaseOrder.SHExess = null;
                }
                if (collection["ExessAmount"] != "")
                {
                    purchaseOrder.ExessAmount = Decimal.Parse(collection["ExessAmount"]);

                }
                else
                {
                    purchaseOrder.ExessAmount = null;
                }
                purchaseOrder.TaxType = Convert.ToInt32(collection["TaxType"]);

                if (collection["SHExessAmount"] != "")
                {
                    purchaseOrder.SHExessAmount = Convert.ToDecimal(collection["SHExessAmount"]);
                }
                else
                {
                    purchaseOrder.SHExessAmount = null;
                }
                if (collection["ExciseDutyAmount"] != "")
                {
                    purchaseOrder.VAT = collection["VAT"];
                }
                else
                {
                    purchaseOrder.VAT = null;
                }
                if (collection["VATAmt"] != "")
                {
                    purchaseOrder.VATAmt = Convert.ToDecimal(collection["VATAmt"]);
                }
                else
                {
                    purchaseOrder.VATAmt = null;
                }
                if (collection["SurCharge"] != "")
                {
                    purchaseOrder.SurCharge = collection["SurCharge"];
                }
                else
                {
                    purchaseOrder.SurCharge = null;
                }
                if (collection["SurChargeAmt"] != "")
                {
                    purchaseOrder.SurChargeAmt = Decimal.Parse(collection["SurChargeAmt"]);
                }
                else
                {
                    purchaseOrder.SurChargeAmt = 0;
                }
                if (!string.IsNullOrEmpty(collection["AmountTax"]))
                {
                    purchaseOrder.AmountTax = Convert.ToDecimal(collection["AmountTax"]);
                }
                if (!string.IsNullOrEmpty(collection["Remarks"]))
                {
                    purchaseOrder.Remarks = collection["Remarks"];
                }
                if (collection["MRPMargin"] != "")
                {
                    purchaseOrder.MRPMargin = collection["MRPMargin"];
                }
                if (collection["MRPPrice"] != "")
                {
                    purchaseOrder.MRPPrice = Convert.ToDecimal(collection["MRPPrice"]);
                }
                if (collection["IndentNoDirectPO"] != "")
                {
                    purchaseOrder.IndentNoDirectPO = Convert.ToInt32(collection["IndentNoDirectPO"]);
                }
                purchaseOrder.AccessibleValue = collection["AccessibleValue"];
                purchaseOrder.UpdatedDate = DateTime.Now;
                string a = collection["ISPO_cancelled"];
                purchaseOrder.ISPO_cancelled = Convert.ToBoolean(collection["ISPO_cancelled"].Split(',')[0]);
                purchaseOrder.ISPO_cancelled_Reson = collection["ISPO_cancelled_Reson"];
                if (string.IsNullOrEmpty(collection["OrderType"]))
                {
                    purchaseOrder.IndentMaterialID = indentMaterialID;
                }
                Logger.Log("sucess","","");
                Result = purchaseOrderManager.Post(purchaseOrder);
                Logger.Log(Result.ToString(), "", "");
                if (Result == true)
                {
                    Message = "Saved Successfully";
                    List<PurchaseOrder> listPurchaseOrder = new List<Core.Entities.Stock.PurchaseOrder>();
                    if (purchaseOrder.IndentMaterialID != null)
                    {
                        listPurchaseOrder = purchaseOrderManager.GetISPoIndentMaterialList(purchaseOrder.IndentNo, purchaseOrder.Material);
                    }
                    if (!string.IsNullOrEmpty(collection["OrderType"]))
                    {
                        if (string.IsNullOrEmpty(collection["IndentNoDirectPO"]))
                        {
                            foreach (var iteration in indentno)
                            {
                                indentMaterials_ = new IndentMaterials();
                                if (iteration != "")
                                {
                                    indentMaterials_ = indentMaterialManager.GetIsPOUpdate(Convert.ToInt32(iteration), purchaseOrder.Material);
                                    if (indentMaterials_ != null)
                                    {
                                        var value = listPurchaseOrder.Sum(X => X.PoQty);
                                        if (purchaseOrder.PoQty != purchaseOrder.Qty && value != purchaseOrder.Qty)
                                        {
                                            indentMaterials_.IsPo = false;
                                        }
                                        else if (value == purchaseOrder.Qty) 
                                        {
                                            indentMaterials_.IsPo = true;
                                        }
                                        else
                                        {
                                            indentMaterials_.IsPo = true;
                                        }

                                        indentMaterialManager.Post(indentMaterials_);
                                    }
                                }

                            }

                        }

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(collection["IndentNoDirectPO"]))
                        {
                            indentMaterials_ = indentMaterialManager.GetIndentMaterialId(indentMaterialID);
                            if (indentMaterials_ != null)
                            {
                                indentMaterials_.IsPo = true;
                                indentMaterialManager.Post(indentMaterials_);
                            }
                        }
                    }

                }
                if (collection["SizeRangeDeliery"] != null)
                {
                    var SizeDelivery = JsonConvert.DeserializeObject<List<DelieryScheduleObject>>(collection["SizeRangeDeliery"]);
                    int count = 1;

                    foreach (var item in SizeDelivery)
                    {
                        PurchaseOrderDelierySchedule delieryScheduleObject = new PurchaseOrderDelierySchedule();
                        delieryScheduleObject.Material = item.Material;
                        delieryScheduleObject.Quantity = Convert.ToInt32(item.Quantity);
                        delieryScheduleObject.Date = Convert.ToDateTime(item.Date);
                        delieryScheduleObject.PoOrderID = purchaseOrder.PoOrderId;
                        delieryScheduleObject.IO = 0;
                        List<PurchaseOrderDelierySchedule> listOrderDeliverySchedule = new List<PurchaseOrderDelierySchedule>();
                        listOrderDeliverySchedule = purchaseOrderManager.PurchaseOrderDelieryScheduleGet().Where(x => x.PoOrderID == purchaseOrder.PoOrderId).ToList();
                        if (count == 1)
                        {
                            purchaseOrderManager.PurchaseOrderDelieryScheduleDelete(purchaseOrder.PoOrderId);
                            count++;
                        }

                        purchaseOrderManager.SizeRangeDeliveryPost(delieryScheduleObject);
                    }
                }
                if (collection["SizeRangeDetails"] != null)
                {
                    var SizeDetails = JsonConvert.DeserializeObject<List<SizeQuantityObject>>(collection["SizeRangeDetails"]);
                    int count1 = 1;
                    foreach (var item in SizeDetails)
                    {
                        PurchaseOrderSizeRangeQuantity purchaseOrderSizeRangeQuantity = new PurchaseOrderSizeRangeQuantity();
                        purchaseOrderSizeRangeQuantity.Size = item.Size;
                        purchaseOrderSizeRangeQuantity.Quantity = Convert.ToInt32(item.Quantity);
                        purchaseOrderSizeRangeQuantity.Rate = item.Rate;
                        purchaseOrderSizeRangeQuantity.PurchaseSizeRangeID = 0;
                        purchaseOrderSizeRangeQuantity.PoOrderID = purchaseOrder.PoOrderId;
                        List<PurchaseOrderSizeRangeQuantity> listPurchaseOrderSizeRangeQuantity = new List<PurchaseOrderSizeRangeQuantity>();
                        listPurchaseOrderSizeRangeQuantity = purchaseOrderManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == purchaseOrder.PoOrderId).ToList();
                        if (count1 == 1)
                        {
                            purchaseOrderManager.PurchaseOrderSizeRangeQuantityDelete(purchaseOrder.PoOrderId);
                            count1++;
                        }
                        purchaseOrderManager.SizeRangeDetailsPost(purchaseOrderSizeRangeQuantity);
                    }
                }
                List<SizeRangeQtyRate> listSizeRangeQtyRateList = new List<SizeRangeQtyRate>();
                List<IndentMaterial> listIndentMaterial = new List<IndentMaterial>();
                if (Result == true)
                {
                    MaterialGroupManager materialGroupManager = new MaterialGroupManager();
                    materialgroupmaster materialGroupMaster = new materialgroupmaster();
                    if (!string.IsNullOrEmpty(collection["OrderType"]))
                    {
                        GRNTypeManager grnTypemanager = new GRNTypeManager();
                        GrnTypeMaster grnTypeMaster = new GrnTypeMaster();
                        grnTypeMaster = grnTypemanager.GetIssueTypeMasterId(Convert.ToInt32(collection["OrderType"]));
                        if (grnTypeMaster != null && grnTypeMaster.IssueType != "Direct Po")
                        {
                            if (indentMaterials_ != null && indentMaterials_.IndentMaterialID != 0)
                            {
                                materialGroupMaster = materialGroupManager.GetmaterialgroupmasterId(indentMaterials_.MaterialGroupMasterId);
                                if (materialGroupMaster != null)
                                {
                                    if (materialGroupMaster.IsSize == true)
                                    {
                                        MaterialMaster materialMaster = new MaterialMaster();
                                        MaterialManager Materialmanager = new MaterialManager();
                                        SizeRangeQtyRateManager sizeRangeQtyRateManager = new SizeRangeQtyRateManager();
                                        materialMaster = Materialmanager.GetMaterialName_(indentMaterials_.MaterialMasterID.Value);
                                        listSizeRangeQtyRateList = sizeRangeQtyRateManager.Get().Where(x => x.OrderEntryId == indentMaterials_.OrderEntryId).ToList();
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(collection["IndentNoDirectPO"]) && purchaseOrder.Supplier != null && grnTypeMaster.IssueType != "General")
                            {
                                listIndentMaterial = indentMaterialManager.GetIndentIDWithSupplierPurchaseOrder(purchaseOrder.Supplier.Value, purchaseOrder.IndentNo);
                            }
                            else if (string.IsNullOrEmpty(collection["IndentNoDirectPO"]) && purchaseOrder.Supplier != null && grnTypeMaster.IssueType == "General")
                            {
                                listIndentMaterial = indentMaterialManager.GetIndentIDWithSupplierPurchaseOrder(purchaseOrder.Supplier.Value, purchaseOrder.IndentNo);
                            }
                            else if (!string.IsNullOrEmpty(collection["IndentNoDirectPO"]) && purchaseOrder.Supplier != null && grnTypeMaster.IssueType == "Order")
                            {
                                listIndentMaterial = indentMaterialManager.GetIndentIDWithSupplierPurchaseOrder(purchaseOrder.Supplier.Value, purchaseOrder.IndentNoDirectPO.Value.ToString());
                            }
                            else
                            {
                                listIndentMaterial = indentMaterialManager.GetIndentIDWithSupplierPurchaseOrder(0, purchaseOrder.IndentNo);
                            }
                        }
                        else
                        {
                            MaterialMaster materialMaster = new MaterialMaster();
                            MaterialManager Materialmanager = new MaterialManager();
                            materialMaster = Materialmanager.GetMaterialMasterId(purchaseOrder.Material);
                            List<SizeItemMaterial> listSizeItemMaterial = new List<SizeItemMaterial>();
                            listSizeItemMaterial = Materialmanager.GetSizeItemMaterial(purchaseOrder.Material.Value);
                            MaterialNameManager materialNameManager = new MaterialNameManager();
                            tbl_materialnamemaster materialNameMaster = new tbl_materialnamemaster();
                            if (materialMaster != null)
                            {
                                materialNameMaster = materialNameManager.GetMaterialNameMasterId(materialMaster.MaterialName);

                                poList = indentMaterialManager.PurchaseOrderGrid1(purchaseOrder.PoNo);

                            }
                            return Json(new { IndentMaterial = poList, SizeRangeQtyRate = listSizeItemMaterial, purchaseOrder = purchaseOrder, Response = Message, orderType = "Direct Po", materialNameMaster = materialNameMaster }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { IndentMaterial = listIndentMaterial, SizeRangeQtyRate = listSizeRangeQtyRateList, Response = Message, orderType = "General", purchaseOrder = purchaseOrder }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { IndentMaterial = purchaseOrder, orderType = "General" }, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(new { IndentMaterial = listIndentMaterial, SizeRangeQtyRate = listSizeRangeQtyRateList, Response = Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PurchaseOrderPost(PurchaseOrderModel model)
        {
            bool Result = false;
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder.PoOrderId = model.PoOrderId;
            purchaseOrder.PoNo = model.PoNo;
            purchaseOrder.Unit = model.UnitId;
            purchaseOrder.FreightAmt = model.FreightAmt;
            purchaseOrder.FreightType = model.FreightType;
            purchaseOrder.LastAmendmentNo = model.LastAmendmentNo;
            DateTime purLastAmendmentDate = DateTime.Parse(model.LastAmendmentDate, new CultureInfo("en-CA"));
            DateTime purPoDate = DateTime.Parse(model.PoDate, new CultureInfo("en-CA"));
            purchaseOrder.LastAmendmentDate = purLastAmendmentDate.Date;
            purchaseOrder.PoDate = purPoDate.Date;
            purchaseOrder.PoType = model.PoType;
            purchaseOrder.InsuranceAmount = model.InsuranceAmount;
            purchaseOrder.InsureanceCurrency = model.InsureanceCurrency;
            purchaseOrder.RefInfo = model.RefInfo;
            purchaseOrder.Supplier = model.Supplier;
            purchaseOrder.CustomsDuty = model.CustomsDuty;
            purchaseOrder.CustomsDutyType = model.CustomsDutyType;
            purchaseOrder.Form = model.Form;
            purchaseOrder.Currency = model.Currency;
            purchaseOrder.ExRate = model.ExRate;
            purchaseOrder.PackForwardtype = model.PackForwardtype;
            purchaseOrder.PackForward = model.PackForward;
            purchaseOrder.FrightType = model.FrightType;
            purchaseOrder.StockValue = model.StockValue;
            purchaseOrder.FormName = model.FormName;
            purchaseOrder.InsuranceDetails = model.InsuranceDetails;
            purchaseOrder.ServiceTax = model.ServiceTax;
            purchaseOrder.ServiceTaxType = model.ServiceTaxType;
            purchaseOrder.ServiceTaxNumbner = model.ServiceTaxNumbner;
            purchaseOrder.TickToCloseOrder = model.TickToCloseOrder;
            purchaseOrder.TaxType = model.TaxType;
            purchaseOrder.FreightCostTotal = model.FreightCostTotal;
            purchaseOrder.UOMValue1 = model.UOMValue1;
            purchaseOrder.ServiceTaxPer = model.ServiceTaxPer;

            if (model.TickToCloseOrder == false)
            {
                DateTime? purOrderClosedOn = null;
                purchaseOrder.OrderClosedOn = purOrderClosedOn;
            }
            else
            {
                DateTime purOrderClosedOn = DateTime.Parse(model.OrderClosedOn, new CultureInfo("en-CA"));
                purchaseOrder.OrderClosedOn = purOrderClosedOn.Date;
            }


            purchaseOrder.IndentNo = model.IndentNo;
            purchaseOrder.Category = model.Category;
            purchaseOrder.Groupname = model.Groupname;
            purchaseOrder.Material = model.Material;
            purchaseOrder.Description = model.Description;
            purchaseOrder.Machinename = model.Machinename;
            purchaseOrder.UOM = model.UOM;
            purchaseOrder.UOMValue = model.UOMValue;
            purchaseOrder.UOMType = model.UOMType;
            purchaseOrder.Color = model.Color;
            purchaseOrder.Qty = model.Qty;
            purchaseOrder.Substance = model.Substance;
            purchaseOrder.Weight = model.Weight;
            purchaseOrder.IONO = model.IONO;
            purchaseOrder.OtherSpecification = model.OtherSpecification;
            DateTime purReqdDate = DateTime.Parse(model.ReqdDate, new CultureInfo("en-CA"));
            DateTime purETD = DateTime.Parse(model.ETD, new CultureInfo("en-CA"));
            DateTime purETA = DateTime.Parse(model.ETA, new CultureInfo("en-CA"));
            purchaseOrder.ReqdDate = purReqdDate.Date;
            purchaseOrder.ETD = purETD.Date;
            purchaseOrder.ETA = purETA.Date;
            purchaseOrder.Rate = model.Rate;
            purchaseOrder.Dis = model.Dis;
            purchaseOrder.DisAmount = model.DisAmount;
            purchaseOrder.ExciseDutyAmount = model.ExciseDutyAmount;
            purchaseOrder.ExciseDuty = model.ExciseDuty;
            purchaseOrder.Exess = model.Exess;
            purchaseOrder.SHExess = model.SHExess;
            purchaseOrder.ExessAmount = model.ExessAmount;
            purchaseOrder.TaxType = model.TaxType;
            purchaseOrder.SHExessAmount = model.SHExessAmount;
            purchaseOrder.VAT = model.VAT;
            purchaseOrder.VATAmt = model.VATAmt;
            purchaseOrder.SurCharge = model.SurCharge;
            purchaseOrder.SurChargeAmt = model.SurChargeAmt;
            purchaseOrder.AmountTax = model.AmountTax;
            purchaseOrder.Remarks = model.Remarks;
            purchaseOrder.MRPMargin = model.MRPMargin;
            purchaseOrder.MRPPrice = model.MRPPrice;
            purchaseOrder.AccessibleValue = model.AccessibleValue;
            purchaseOrder.UpdatedDate = DateTime.Now;

            purchaseOrder.SupplierName = model.SupplierName;
            purchaseOrder.CompanyName = model.CompanyName;
            purchaseOrder.Address = model.Address;
            purchaseOrder.Phone = model.Phone;
            purchaseOrder.City = model.City;
            purchaseOrder.PaymentTerms = model.PaymentTerms;
            purchaseOrder.OtherTerms = model.OtherTerms;
            purchaseOrder.DeliverTerms = model.DeliverTerms;
            purchaseOrder.TermsConditions = model.TermsConditions;
            Result = purchaseOrderManager.Post(purchaseOrder);
            if (model.SizeRangeDeliery != null)
            {
                var SizeDelivery = JsonConvert.DeserializeObject<List<DelieryScheduleObject>>(model.SizeRangeDeliery);
                int count = 1;

                foreach (var item in SizeDelivery)
                {
                    PurchaseOrderDelierySchedule delieryScheduleObject = new PurchaseOrderDelierySchedule();
                    delieryScheduleObject.Material = item.Material;
                    delieryScheduleObject.Quantity = Convert.ToInt32(item.Quantity);
                    delieryScheduleObject.Date = Convert.ToDateTime(item.Date);
                    delieryScheduleObject.PoOrderID = purchaseOrder.PoOrderId;
                    delieryScheduleObject.IO = 0;
                    List<PurchaseOrderDelierySchedule> listOrderDeliverySchedule = new List<PurchaseOrderDelierySchedule>();
                    listOrderDeliverySchedule = purchaseOrderManager.PurchaseOrderDelieryScheduleGet().Where(x => x.PoOrderID == purchaseOrder.PoOrderId).ToList();
                    if (count == 1)
                    {
                        purchaseOrderManager.PurchaseOrderDelieryScheduleDelete(purchaseOrder.PoOrderId);
                        count++;
                    }

                    purchaseOrderManager.SizeRangeDeliveryPost(delieryScheduleObject);
                }
            }
            if (model.SizeRangeDetails != null)
            {
                var SizeDetails = JsonConvert.DeserializeObject<List<SizeQuantityObject>>(model.SizeRangeDetails);
                int count1 = 1;
                foreach (var item in SizeDetails)
                {
                    PurchaseOrderSizeRangeQuantity purchaseOrderSizeRangeQuantity = new PurchaseOrderSizeRangeQuantity();
                    purchaseOrderSizeRangeQuantity.Size = item.Size;
                    purchaseOrderSizeRangeQuantity.Quantity = Convert.ToInt32(item.Quantity);
                    purchaseOrderSizeRangeQuantity.Rate = item.Rate;
                    purchaseOrderSizeRangeQuantity.PurchaseSizeRangeID = 0;
                    purchaseOrderSizeRangeQuantity.PoOrderID = purchaseOrder.PoOrderId;
                    List<PurchaseOrderSizeRangeQuantity> listPurchaseOrderSizeRangeQuantity = new List<PurchaseOrderSizeRangeQuantity>();
                    listPurchaseOrderSizeRangeQuantity = purchaseOrderManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == purchaseOrder.PoOrderId).ToList();
                    if (count1 == 1)
                    {
                        purchaseOrderManager.PurchaseOrderSizeRangeQuantityDelete(purchaseOrder.PoOrderId);
                        count1++;
                    }
                    purchaseOrderManager.SizeRangeDetailsPost(purchaseOrderSizeRangeQuantity);
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(PurchaseOrderModel model)
        {
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            IndentMaterialManager intendMaterialManager = new IndentMaterialManager();
            IndentMaterials indentMaterial = new IndentMaterials();
            string status = "";
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            purchaseOrderList = purchaseOrderManager.GetPoNOList(model.PoNo);
            if (purchaseOrderList != null && purchaseOrderList.Count > 0)
            {
                status = "Success";
                foreach (var purchaseitem in purchaseOrderList)
                {
                    List<PurchaseOrderSizeRangeQuantity> listPurchaseOrderSizeRangeQuantity = new List<PurchaseOrderSizeRangeQuantity>();
                    listPurchaseOrderSizeRangeQuantity = purchaseOrderManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == purchaseitem.PoOrderId).ToList();

                    List<PurchaseOrderDelierySchedule> listPurchaseOrderDelierySchedule = new List<PurchaseOrderDelierySchedule>();
                    listPurchaseOrderDelierySchedule = purchaseOrderManager.PurchaseOrderDelieryScheduleGet().Where(x => x.PoOrderID == purchaseitem.PoOrderId).ToList();

                    foreach (var items in listPurchaseOrderDelierySchedule)
                    {
                        purchaseOrderManager.PurchaseOrderDelieryScheduleDelete(items.PoOrderID);
                    }
                    foreach (var deleteItem in listPurchaseOrderSizeRangeQuantity)
                    {
                        purchaseOrderManager.PurchaseOrderSizeRangeQuantityDelete(deleteItem.PoOrderID);
                    }

                    if (purchaseitem.IndentNo != null && purchaseitem.IndentNo != "")
                    {
                        if (purchaseitem.IndentNo.Contains(","))
                        {
                            string[] indentarray = purchaseitem.IndentNo.Split(',');
                            foreach (var item in indentarray)
                            {
                                indentMaterial = intendMaterialManager.GetIsPOUpdate(Convert.ToInt32(item), purchaseitem.Material.Value);
                                if (indentMaterial != null)
                                {
                                    indentMaterial.IsPo = false;
                                    intendMaterialManager.Post(indentMaterial);
                                }
                                purchaseOrderManager.Delete(purchaseitem.PoOrderId);
                            }
                        }
                        else
                        {
                            indentMaterial = intendMaterialManager.GetIsPOUpdate(Convert.ToInt32(purchaseitem.IndentNo), purchaseitem.Material.Value);
                            if (indentMaterial != null)
                            {
                                indentMaterial.IsPo = false;
                                intendMaterialManager.Post(indentMaterial);
                            }
                            purchaseOrderManager.Delete(purchaseitem.PoOrderId);
                        }

                    }
                    else
                    {
                        purchaseOrderManager.Delete(purchaseitem.PoOrderId);
                    }

                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DirectPoDelete(PurchaseOrderModel model)
        {
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            IndentMaterialManager intendMaterialManager = new IndentMaterialManager();
            IndentMaterials indentMaterial = new IndentMaterials();
            string status = "";
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            List<PurchaseOrder> purchaseOrderList = new List<PurchaseOrder>();
            GrnManagerNew grnManager = new GrnManagerNew();
            purchaseOrderList = purchaseOrderManager.GetPoOrderIdList(model.PoOrderId);
            string poOrderId = purchaseOrderList.FirstOrDefault().PoNo;
            var polist = grnManager.GetGRN_PoDelete(model.PoOrderId);
            if (polist == null || polist.PoNO == 0 || polist?.PoNO == null)
            {
                if (purchaseOrderList != null && purchaseOrderList.Count > 0)
                {
                    status = "Success";
                    foreach (var purchaseitem in purchaseOrderList)
                    {
                        List<PurchaseOrderSizeRangeQuantity> listPurchaseOrderSizeRangeQuantity = new List<PurchaseOrderSizeRangeQuantity>();
                        listPurchaseOrderSizeRangeQuantity = purchaseOrderManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == purchaseitem.PoOrderId).ToList();

                        List<PurchaseOrderDelierySchedule> listPurchaseOrderDelierySchedule = new List<PurchaseOrderDelierySchedule>();
                        listPurchaseOrderDelierySchedule = purchaseOrderManager.PurchaseOrderDelieryScheduleGet().Where(x => x.PoOrderID == purchaseitem.PoOrderId).ToList();

                        foreach (var items in listPurchaseOrderDelierySchedule)
                        {
                            purchaseOrderManager.PurchaseOrderDelieryScheduleDelete(items.PoOrderID);
                        }
                        foreach (var deleteItem in listPurchaseOrderSizeRangeQuantity)
                        {
                            purchaseOrderManager.PurchaseOrderSizeRangeQuantityDelete(deleteItem.PoOrderID);
                        }
                        purchaseOrderManager.Delete(purchaseitem.PoOrderId);
                        if (purchaseitem.IndentNo != null && purchaseitem.IndentNo != "0")
                        {
                            indentMaterial = intendMaterialManager.GetIsPOUpdate(Convert.ToInt32(purchaseitem.IndentNo), Convert.ToInt32( purchaseitem.Material));
                            if (indentMaterial != null)
                            {
                                indentMaterial.IsPo = false;
                                intendMaterialManager.Post(indentMaterial);
                            }
                        }
                    }
                }
                var Result = new { Status = status, Id = poOrderId };
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Result = new { Status = "Grn Created", Id = 0 };
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion

        #region Helper Method

        public ActionResult PurchaseOrderDetails()
        {

            IndentManager indentManager = new IndentManager();
            SelectList lstobj = null;
        var items = indentManager.Get().OrderBy(x => x.IndentNo).Select(
                                    item => new System.Web.Mvc.SelectListItem()
                                    {
                                        Text = item.IndentNo,
                                        Value = item.IndentId.ToString()
                                    }
                                    ).ToList();
            var ShotName = new System.Web.Mvc.SelectListItem()
            {
                Value = "",
                Text = "Please Select"
            };
            // Setting.  
            lstobj = new SelectList(items, "Value", "Text");          
            PurchaseOrderModel model = new PurchaseOrderModel();
            // Initialization.  
            //  DropdownViewModel models = new DropdownViewModel();
            // Settings.  
            model.DrpIndentNo = 0;
            // Loading drop down lists.  
            this.ViewBag.CountryList =lstobj;            
          
            string PoNo = "";
            List<tbl_Company> list = new List<tbl_Company>();
            int id = Convert.ToInt32(Request.QueryString["id"]);
            if (!string.IsNullOrEmpty(Request.QueryString["PoOrderId"]))
            {
                PoNo = Request.QueryString["PoOrderId"];
            }
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            CompanyManager companyManager = new CompanyManager();
            PurchaseOrder arg = new PurchaseOrder();
            List<SizeRangeDetails> sizeRangeDetais = new List<SizeRangeDetails>();
            SizeRangeDetailsManager sizeRangeManager = new SizeRangeDetailsManager();
            sizeRangeDetais = sizeRangeManager.Get();
            model.SizeRangeDetailslist = sizeRangeDetais;
            List<PurchaseOrderSizeRangeQuantity> purchaseOrderSizeRange = new List<PurchaseOrderSizeRangeQuantity>();
            List<PurchaseOrderDelierySchedule> purchaserOrderDeliveryList = new List<PurchaseOrderDelierySchedule>();
            AutoGenPo autoPo = new AutoGenPo();
            AutoGenPo autoPoNo = new AutoGenPo();
            AutoGenPoManager autoManager = new AutoGenPoManager();
            Company DeliverTo = companyManager.Get().FirstOrDefault();
            if (DeliverTo != null)
            {
                model.SupplierName = DeliverTo.SupplierName;
                model.CompanyName = DeliverTo.CompanyName;
                model.Phone = DeliverTo.Phone;
                model.City = DeliverTo.City;
                model.Address = DeliverTo.Address;
                model.PaymentTerms = DeliverTo.PaymentTerms;
                model.DeliverTerms = DeliverTo.DeliverTerms;
                model.OtherTerms = DeliverTo.OtherTerms;
                model.TermsConditions = DeliverTo.TermsConditions;
            }
            if (id == 0)
            {

                model.OrderType = "7";

                autoPo = autoManager.Get().LastOrDefault();
                if (autoPo != null)
                {
                    var autoId = Convert.ToInt32(autoPo.PoId);
                    autoId++;
                    autoPoNo.PoId = autoId.ToString();
                    autoManager.Post(autoPoNo);
                }
                else
                {

                    autoPoNo.PoId = "1";
                    autoManager.Post(autoPoNo);
                }
            }
            autoPo = autoManager.Get().LastOrDefault();
            int ID = 0;
            if (autoPo == null)
            {
                ID = Convert.ToInt32("1");
            }
            else
            {
                ID = Convert.ToInt32(autoPo.PoId);
            }
            if (id == 0 && string.IsNullOrEmpty(PoNo))
            {
                model.PurchaserOrderList = null;
                string pono = GetPONo();
                model.PoNo = ID.ToString();
                model.MRPMargin = "0";
                model.MRPPrice = 0;
                model.AccessibleValue = "0";
                model.StockValue = 0;
                model.Dis = "0";
                model.ExciseDuty = 0;
                model.Exess = "0";
                model.DisAmount = 0;
                model.ExciseDutyAmount = 0;
                model.Exess = "0";
                model.ExessAmount = 0;
                model.SHExess = "0";
                model.SHExessAmount = 0;
                model.VAT = "0";
                model.VATAmt = 0;
                model.SurCharge = "0";
                model.SurChargeAmt = 0;
                model.AmountTax = 0;
                model.Weight = 0;
                model.Rate = "0";
                model.FreightCostTotal = 0;
                model.FreightAmt = "0";
                model.InsuranceAmount = "0";
                model.CustomsDutyType = "0";
                model.PackForward = "0";
                model.MaterialType = "";
                model.ServiceTaxPer = 0;
                model.ServiceTaxNumbner = 0;
            }
            else
            {
                if (!string.IsNullOrEmpty(PoNo))
                {
                    model.PoNo = PoNo;
                    arg = purchaseOrderManager.Get().Where(x => x.PoNo == PoNo).FirstOrDefault();
                    if (arg != null && arg.PoOrderId != 0)
                    {
                        purchaseOrderSizeRange = purchaseOrderManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == arg.PoOrderId).ToList();
                        model.PurchaseOrderDelierySchedulelist = purchaserOrderDeliveryList;
                        model.PurchaseOrderSizeRangeQuantityList = purchaseOrderSizeRange;
                    }
                    List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
                    IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                    StoreMaster storeMaster = new StoreMaster();
                    StoreMasterManager storeMasterManager = new StoreMasterManager();

                    if (arg != null && arg.IndentMaterialID != null && arg.IndentMaterialID != 0)
                    {
                        if (arg.Supplier != 0 && arg.Supplier != null)
                        {
                            indentMaterials = indentMaterialManager.GetEditPOIndentIDWithSupplier(arg.IndentNo, arg.Supplier.Value);
                            model.ListIndentMaterials = indentMaterials;
                        }
                    }
                    GRNTypeManager manager = new GRNTypeManager();
                    var Order = manager.Get().Where(x => x.GrnTypeMasterId == Convert.ToInt32(arg.OrderType)).FirstOrDefault();
                    model.PoNo = arg.PoNo;
                    if (Order.IssueType.ToLower().Trim() == "direct po")
                    {
                        List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();
                        poList = indentMaterialManager.PurchaseOrderGrid1(arg.PoNo);
                        model.poList = poList;
                    }
                    else
                    {
                        List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();
                        poList = indentMaterialManager.PurchaesOrderedwithNeedToIndent(arg.Supplier, arg.IndentNo, arg.PoNo);
                        model.poList = poList;
                    }
                    model.MRPMargin = "0";
                    model.MRPPrice = 0;
                    model.AccessibleValue = "0";
                    model.StockValue = 0;
                    model.Dis = "0";
                    model.ExciseDuty = 0;
                    model.Exess = "0";
                    model.DisAmount = 0;
                    model.ExciseDutyAmount = 0;
                    model.Exess = "0";
                    model.ExessAmount = 0;
                    model.SHExess = "0";
                    model.SHExessAmount = 0;
                    model.VAT = "0";
                    model.VATAmt = 0;
                    model.SurCharge = "0";
                    model.SurChargeAmt = 0;
                    model.AmountTax = 0;
                    model.Weight = 0;
                    model.Rate = "0";
                    model.FreightCostTotal = 0;
                    model.FreightAmt = "0";
                    model.InsuranceAmount = "0";
                    model.CustomsDutyType = "0";
                    model.PackForward = "0";
                    model.MaterialType = "";
                    model.ServiceTaxPer = 0;
                    model.ServiceTaxNumbner = 0;
                    model.OrderType = arg.OrderType;
                }
            }

            if (id != 0)
            {
                model.PoNo = id.ToString();
                arg = purchaseOrderManager.GetPoNO(model.PoNo);
                string[] IndentArray = arg.IndentNo != null ? arg.IndentNo.Split(',') : null;
                List<Indent> listIndent = new List<Indent>();
                listIndent = indentManager.Get();
                model.ListIndent = listIndent;
                model.Supplier = arg.Supplier;
                if (arg != null && arg.PoOrderId != 0)
                {
                    purchaseOrderSizeRange = purchaseOrderManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == arg.PoOrderId).ToList();
                    model.PurchaseOrderDelierySchedulelist = purchaserOrderDeliveryList;
                    model.PurchaseOrderSizeRangeQuantityList = purchaseOrderSizeRange;
                }
                List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
                IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                StoreMaster storeMaster = new StoreMaster();
                StoreMasterManager storeMasterManager = new StoreMasterManager();

                if (arg != null && arg.IndentMaterialID != null && arg.IndentMaterialID != 0)
                {
                    if (arg.Supplier != 0 && arg.Supplier != null)
                    {
                        indentMaterials = indentMaterialManager.GetEditPOIndentIDWithSupplier(arg.IndentNo, arg.Supplier.Value);
                        model.ListIndentMaterials = indentMaterials;
                    }
                }
                GRNTypeManager manager = new GRNTypeManager();
                var Order = manager.Get().Where(x => x.GrnTypeMasterId == Convert.ToInt32(arg.OrderType)).FirstOrDefault();
                model.PoNo = arg.PoNo;
                if (Order.IssueType.ToLower().Trim() == "direct po")
                {
                    List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();
                    poList = indentMaterialManager.PurchaseOrderGrid1(arg.PoNo);
                    model.poList = poList;
                }
                else
                {

                    List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();

                    poList = indentMaterialManager.PurchaesOrderedwithNeedToIndent(arg.Supplier, arg.IndentNo, arg.PoNo);
                    model.poList = poList;
                }


                model.MRPMargin = "0";
                model.MRPPrice = 0;
                model.AccessibleValue = "0";
                model.StockValue = 0;
                model.Dis = "0";
                model.ExciseDuty = 0;
                model.Exess = "0";
                model.DisAmount = 0;
                model.ExciseDutyAmount = 0;
                model.Exess = "0";
                model.ExessAmount = 0;
                model.SHExess = "0";
                model.SHExessAmount = 0;
                model.VAT = "0";
                model.VATAmt = 0;
                model.SurCharge = "0";
                model.SurChargeAmt = 0;
                model.AmountTax = 0;
                model.Weight = 0;
                model.Rate = "0";
                model.FreightCostTotal = 0;
                model.FreightAmt = "0";
                model.InsuranceAmount = "0";
                model.CustomsDutyType = "0";
                model.PackForward = "0";
                model.MaterialType = "";
                model.ServiceTaxPer = 0;
                model.ServiceTaxNumbner = 0;
                model.OrderType = arg.OrderType;
                model.IndentNoDirectPO = arg.IndentNoDirectPO;
                model.IndentNo = arg.IndentNo;
                model.IndentValue = arg.IndentValue;
                model.UnitId = arg.Unit;
                model.ISPO_cancelled = arg.ISPO_cancelled;
                model.ISPO_cancelled_Reson = arg.ISPO_cancelled_Reson;
            }
            return PartialView("~/Views/Stock/PurchaseOrder/Partial/PurchaseOrderDetails.cshtml", model);
        }
        [HttpGet]
        public ActionResult GetPurchaseOrderDetails(int? PoOrderID)
        {
            PurchaseOrderModel model = new PurchaseOrderModel();
            Company company = new Company();
            CompanyManager companyManager = new CompanyManager();
            int id = PoOrderID.Value;
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            IndentMaterials indentMaterial = new IndentMaterials();
            PurchaseOrder arg = new PurchaseOrder();
            List<SizeRangeDetails> sizeRangeDetais = new List<SizeRangeDetails>();
            SizeRangeDetailsManager sizeRangeManager = new SizeRangeDetailsManager();
            MaterialManager materialManager = new MaterialManager();
            MaterialMaster materialMaster = new MaterialMaster();
            StoreMaster storeMaster = new StoreMaster();
            sizeRangeDetais = sizeRangeManager.Get();
            model.SizeRangeDetailslist = sizeRangeDetais;
            List<PurchaseOrderSizeRangeQuantity> purchaseOrderSizeRange = new List<PurchaseOrderSizeRangeQuantity>();
            List<PurchaseOrderDelierySchedule> purchaserOrderDeliveryList = new List<PurchaseOrderDelierySchedule>();


            if (id == 0)
            {
                model.PurchaserOrderList = null;
                string pono = GetPONo();
                model.PoNo = (pono);
                model.MRPMargin = "0";
                model.MRPPrice = 0;
                model.AccessibleValue = "0";
                model.StockValue = 0;
                model.Dis = "0";
                model.ExciseDuty = 0;
                model.Exess = "0";
                model.DisAmount = 0;
                model.ExciseDutyAmount = 0;
                model.Exess = "0";
                model.ExessAmount = 0;
                model.SHExess = "0";
                model.SHExessAmount = 0;
                model.VAT = "0";
                model.VATAmt = 0;
                model.SurCharge = "0";
                model.SurChargeAmt = 0;
                model.AmountTax = 0;
                model.Weight = 0;
                model.Rate = "0";
                model.MaterialType = "";
                model.FreightCostTotal = 0;
                model.FreightAmt = "0";
                model.InsuranceAmount = "0";
                model.CustomsDutyType = "0";
                model.PackForward = "0";
                model.ServiceTaxPer = 0;
                model.ServiceTaxNumbner = 0;
                PurchaseOrder DeliverTo = purchaseOrderManager.Get().FirstOrDefault();
                if (DeliverTo != null)
                {
                    model.SupplierName = DeliverTo.SupplierName;
                    model.CompanyName = DeliverTo.CompanyName;
                    model.Phone = DeliverTo.Phone;
                    model.City = DeliverTo.City;
                    model.Address = DeliverTo.Address;
                    model.PaymentTerms = DeliverTo.PaymentTerms;
                    model.DeliverTerms = DeliverTo.DeliverTerms;
                    model.OtherTerms = DeliverTo.OtherTerms;
                    model.TermsConditions = DeliverTo.TermsConditions;
                }
            }

            if (id != 0)
            {
                arg = purchaseOrderManager.GetPoOrderId(id);
                if (arg != null && arg.PoOrderId != 0)
                {
                    purchaseOrderSizeRange = purchaseOrderManager.PurchaseOrderSizeRangeQuantityGet().Where(x => x.PoOrderID == arg.PoOrderId).ToList();
                    model.PurchaseOrderDelierySchedulelist = purchaserOrderDeliveryList;
                    model.PurchaseOrderSizeRangeQuantityList = purchaseOrderSizeRange;
                }
                List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
                IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                StoreMasterManager storeMasterManager = new StoreMasterManager();
                if (arg != null && arg.IndentMaterialID != null && arg.IndentMaterialID != 0)
                {
                    if (arg.Supplier != 0 && arg.Supplier != null)
                    {
                        indentMaterials = indentMaterialManager.GetEditPOIndentIDWithSupplier(arg.IndentNo, arg.Supplier.Value);
                        model.ListIndentMaterials = indentMaterials;
                    }
                }
                model.PoOrderId = arg.PoOrderId;
                model.PoNo = arg.PoNo;
                List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel> poList = new List<MMS.Data.StoredProcedureModel.PurchaseOrderGridModel>();
                GRNTypeManager manager = new GRNTypeManager();
                var Order = manager.Get().Where(x => x.GrnTypeMasterId == Convert.ToInt32(arg.OrderType)).FirstOrDefault();
                model.PoNo = arg.PoNo;
                if (Order.IssueType.ToLower().Trim() == "direct po")
                {
                    poList = indentMaterialManager.PurchaseOrderGrid1(arg.PoNo);
                    model.poList = poList;
                }
                else
                {
                    poList = indentMaterialManager.PurchaseOrderGrid(arg.Supplier, arg.IndentNo, arg.PoNo);
                    model.poList = poList;
                }
                model.poList = poList;
                model.UnitId = arg.Unit;
                model.PoQty = arg.PoQty;
                model.PendingQty = arg.PendingQty;
                model.FreightAmt = arg.FreightAmt;
                model.FreightCostTotal = arg.FreightCostTotal;
                model.UOMValue1 = arg.UOMValue1;
                model.ServiceTaxPer = arg.ServiceTaxPer;
                model.FreightType = arg.FreightType;
                model.LastAmendmentNo = arg.LastAmendmentNo;
                model.LastAmendmentDate = Convert.ToDateTime(arg.LastAmendmentDate).Date.ToString("dd/MM/yyyy");
                model.PoDate = Convert.ToDateTime(arg.PoDate).Date.ToString("dd/MM/yyyy");
                model.PoType = arg.PoType;
                model.InsuranceAmount = arg.InsuranceAmount;
                model.InsureanceCurrency = arg.InsureanceCurrency;
                model.RefInfo = arg.RefInfo;
                model.Supplier = arg.Supplier;
                model.OrderType = arg.OrderType;
                model.IndentValue = arg.IndentValue;
                model.CustomsDuty = arg.CustomsDuty;
                model.CustomsDutyType = arg.CustomsDutyType;
                model.Form = arg.Form;
                model.Currency = arg.Currency;
                model.ExRate = arg.ExRate;
                model.PackForwardtype = arg.PackForwardtype;
                model.PackForward = arg.PackForward;
                model.IndentNoDirectPO = arg.IndentNoDirectPO;
                model.IndentMaterialID = arg.IndentMaterialID;
                model.FormName = arg.FormName;
                model.InsuranceDetails = arg.InsuranceDetails;
                model.ServiceTax = arg.ServiceTax;
                model.ServiceTaxType = arg.ServiceTaxType;
                model.ServiceTaxNumbner = arg.ServiceTaxNumbner;
                model.TickToCloseOrder = arg.TickToCloseOrder;
                model.TaxType = arg.TaxType;
                model.OrderClosedOn = Convert.ToDateTime(arg.OrderClosedOn).Date.ToString("dd/MM/yyyy");
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
                model.ReqdDate = Convert.ToDateTime(arg.ReqdDate).Date.ToString("dd/MM/yyyy");
                model.ETD = Convert.ToDateTime(arg.ETD).Date.ToString("dd/MM/yyyy");
                model.ETA = Convert.ToDateTime(arg.ETA).Date.ToString("dd/MM/yyyy");
                SupplierMasterManager supplierMasterManager = new SupplierMasterManager();
                ApprovedPriceListManager approvedPriceListManager = new ApprovedPriceListManager();
                List<ApprovedPriceList> approvedPriceList = new List<ApprovedPriceList>();


                var supplierItems = (from x in approvedPriceListManager.Get()
                                     join y in supplierMasterManager.Get()
                                     on x.SupplierId equals y.SupplierMasterId
                                     where x.MaterialID == arg.Material && x.SupplierId == arg.Supplier
                                     select new { x.SupplierId, y.SupplierName, x.Date, x.PriceRs }).OrderByDescending(t => t.Date)
                                .ToList();
                var supplierItems_ = supplierItems.Where(x => Convert.ToDateTime(x.Date.Value) <= Convert.ToDateTime(arg.PoDate.Value)).ToList().OrderByDescending(x => x.Date).Take(1);
                if (arg != null && arg.OrderType != null && arg.OrderType != "")
                {
                    model.Rate_ = Convert.ToDecimal(supplierItems_.FirstOrDefault().PriceRs);
                }
                else
                {
                    model.Rate = (supplierItems_.FirstOrDefault().PriceRs.ToString());
                }
                model.Dis = arg.Dis;
                model.DisAmount = arg.DisAmount;
                model.ExciseDutyAmount = arg.ExciseDutyAmount;
                model.ExciseDuty = arg.ExciseDuty;
                model.Exess = arg.Exess;
                model.ExessAmount = arg.ExessAmount;
                model.SHExess = arg.SHExess;
                model.SHExessAmount = arg.SHExessAmount;
                model.VAT = arg.VAT;
                model.VATAmt = arg.VATAmt;
                model.MaterialType = arg.MaterialType.ToString();
                model.SurCharge = arg.SurCharge;
                model.SurChargeAmt = arg.SurChargeAmt;
                model.AmountTax = arg.AmountTax;
                model.Remarks = arg.Remarks;
                model.MRPMargin = arg.MRPMargin;
                model.MRPPrice = arg.MRPPrice;
                model.AccessibleValue = arg.AccessibleValue;
                model.FrightType = arg.FrightType;
                model.StockValue = arg.StockValue;
                model.SupplierName = arg.SupplierName;
                model.CompanyName = arg.CompanyName;
                model.Address = arg.Address;
                model.Phone = arg.Phone;

                model.City = arg.City;
                model.PaymentTerms = arg.PaymentTerms;
                model.OtherTerms = arg.OtherTerms;
                model.DeliverTerms = arg.DeliverTerms;
                model.TermsConditions = arg.TermsConditions;
                model.Edit = true;
                if (model.IndentMaterialID != null && model.IndentMaterialID != 0)
                {
                    indentMaterial = indentMaterialManager.GetIndentMaterialId(model.IndentMaterialID.Value);
                    if (indentMaterial != null && indentMaterial.StoreId != null)
                    {
                        storeMaster = storeMasterManager.GetStoreMasterId(indentMaterial.StoreId.Value);
                    }
                    materialMaster = materialManager.GetMaterialMasterId(indentMaterial.MaterialMasterID);
                }
                else
                {
                    materialMaster = materialManager.GetMaterialMasterId(model.Material);
                    storeMaster = storeMasterManager.GetStoreMasterId(materialMaster.StoreMasterId);
                }
                if (storeMaster != null && storeMaster.StoreMasterId != 0)
                {
                    company = companyManager.GetCompanyCode(storeMaster.StoreMasterId);
                }

            }
            return Json(new { PurchaseOrder = model, indentMaterial = indentMaterial, Material = materialMaster, store = storeMaster, company = company }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PurchaseOrderDetailsView(string PoNo)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrder arg = new PurchaseOrder();
            PurchaseOrderModel model = new PurchaseOrderModel();
            List<PurchaseOrderSizeRangeQuantity> purchaseOrderSizeRange = new List<PurchaseOrderSizeRangeQuantity>();
            List<PurchaseOrderDelierySchedule> purchaserOrderDeliveryList = new List<PurchaseOrderDelierySchedule>();

            model.PurchaseOrderDelierySchedulelist = purchaserOrderDeliveryList;
            model.PurchaseOrderSizeRangeQuantityList = purchaseOrderSizeRange;
            if (PoNo == "" || PoNo == null)
            {
                model.PurchaserOrderList = null;
                model.Edit = true;
                string pono = GetPONo();
                model.PoNo = (pono);
            }
            if (PoNo != "" && PoNo != null)
            {
                arg = purchaseOrderManager.GetPoOrderId(Convert.ToInt32(PoNo));
                List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
                IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
                indentMaterials = indentMaterialManager.GetEditPOIndentIDWithSupplier(arg.IndentNo, arg.Supplier.Value);
                model.ListIndentMaterials = indentMaterials;
                model.PoOrderId = arg.PoOrderId;
                model.PoNo = arg.PoNo;
                model.UnitId = arg.Unit;
                model.IndentValue = arg.IndentValue;
                model.FreightAmt = arg.FreightAmt;
                model.FreightType = arg.FreightType;
                model.LastAmendmentNo = arg.LastAmendmentNo;
                model.LastAmendmentDate = Convert.ToDateTime(arg.LastAmendmentDate).Date.ToString("dd/MM/yyyy");
                model.PoDate = Convert.ToDateTime(arg.PoDate).Date.ToString("dd/MM/yyyy");
                model.PoType = arg.PoType;
                model.FreightCostTotal = arg.FreightCostTotal;
                model.UOMValue1 = arg.UOMValue1;
                model.ServiceTaxPer = arg.ServiceTaxPer;
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
                model.OrderClosedOn = Convert.ToDateTime(arg.OrderClosedOn).Date.ToString("dd/MM/yyyy");
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
                model.ReqdDate = Convert.ToDateTime(arg.ReqdDate).Date.ToString("dd/MM/yyyy");
                model.ETD = Convert.ToDateTime(arg.ETD).Date.ToString("dd/MM/yyyy");
                model.ETA = Convert.ToDateTime(arg.ETA).Date.ToString("dd/MM/yyyy");
                model.Rate = arg.Rate;
                model.Dis = arg.Dis;
                model.DisAmount = arg.DisAmount;
                model.ExciseDutyAmount = arg.ExciseDutyAmount;
                model.ExciseDuty = arg.ExciseDuty;
                model.Exess = arg.Exess;
                model.SHExess = arg.SHExess;
                model.ExessAmount = arg.ExessAmount;
                model.SHExessAmount = arg.SHExessAmount;
                model.VAT = arg.VAT;
                model.VATAmt = arg.VATAmt;
                model.SurCharge = arg.SurCharge;
                model.SurChargeAmt = arg.SurChargeAmt;
                model.AmountTax = arg.AmountTax;
                model.Remarks = arg.Remarks;
                model.MRPMargin = arg.MRPMargin;
                model.MRPPrice = arg.MRPPrice;
                model.AccessibleValue = arg.AccessibleValue;
                model.SupplierName = arg.SupplierName;
                model.CompanyName = arg.CompanyName;
                model.Address = arg.Address;
                model.Phone = arg.Phone;
                model.City = arg.City;
                model.PaymentTerms = arg.PaymentTerms;
                model.OtherTerms = arg.OtherTerms;
                model.DeliverTerms = arg.DeliverTerms;
                model.TermsConditions = arg.TermsConditions;
                model.Edit = true;
            }

            return PartialView("~/Views/Stock/PurchaseOrder/Partial/AddPurchaseOrder.cshtml", model);
        }
        public string GetPONo()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGeneratePoNo();
            ID++;
            return ID.ToString();
        }
        [HttpGet]
        public ActionResult GetLastPO()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGeneratePoNo();
            ID++;

            return Json(ID.ToString(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PurchaseOrder(int PoOrderId = 0)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrder arg = new PurchaseOrder();
            PurchaseOrderModel model = new PurchaseOrderModel();
            List<SizeRangeDetails> sizeRangeDetais = new List<SizeRangeDetails>();
            SizeRangeDetailsManager sizeRangeManager = new SizeRangeDetailsManager();
            sizeRangeDetais = sizeRangeManager.Get();
            model.SizeRangeDetailslist = sizeRangeDetais;
            if (PoOrderId == 0)
            {
                model.PurchaserOrderList = null;
                model.Edit = true;
            }
            if (PoOrderId != 0)
            {
                arg = purchaseOrderManager.GetPoOrderId(PoOrderId);
                model.PoOrderId = arg.PoOrderId;
                model.PoNo = arg.PoNo;
                model.UnitId = arg.Unit;
                model.IndentValue = arg.IndentValue;
                model.OrderType = arg.OrderType;
                model.FreightAmt = arg.FreightAmt;
                model.FreightType = arg.FreightType;
                model.LastAmendmentNo = arg.LastAmendmentNo;
                model.LastAmendmentDate = Convert.ToDateTime(arg.LastAmendmentDate).Date.ToString("dd/MM/yyyy");
                model.PoDate = Convert.ToDateTime(arg.PoDate).Date.ToString("dd/MM/yyyy");
                model.PoType = arg.PoType;
                model.InsuranceAmount = arg.InsuranceAmount;
                model.InsureanceCurrency = arg.InsureanceCurrency;
                model.RefInfo = arg.RefInfo;
                model.MaterialType = arg.MaterialType.ToString();
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
                model.OrderClosedOn = Convert.ToDateTime(arg.OrderClosedOn).Date.ToString("dd/MM/yyyy");
                model.IndentNo = arg.IndentNo;
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
                model.ReqdDate = Convert.ToDateTime(arg.ReqdDate).Date.ToString("dd/MM/yyyy");
                model.ETD = Convert.ToDateTime(arg.ETD).Date.ToString("dd/MM/yyyy");
                model.ETA = Convert.ToDateTime(arg.ETA).Date.ToString("dd/MM/yyyy");
                model.Rate = arg.Rate;
                model.Dis = arg.Dis;
                model.DisAmount = arg.DisAmount;
                model.ExciseDutyAmount = arg.ExciseDutyAmount;
                model.ExciseDuty = arg.ExciseDuty;
                model.Exess = arg.Exess;
                model.SHExess = arg.SHExess;
                model.ExessAmount = arg.ExessAmount;
                model.SHExessAmount = arg.SHExessAmount;
                model.VAT = arg.VAT;
                model.VATAmt = arg.VATAmt;
                model.SurCharge = arg.SurCharge;
                model.SurChargeAmt = arg.SurChargeAmt;
                model.AmountTax = arg.AmountTax;
                model.Remarks = arg.Remarks;
                model.MRPMargin = arg.MRPMargin;
                model.MRPPrice = arg.MRPPrice;
                model.AccessibleValue = arg.AccessibleValue;
                model.FrightType = arg.FrightType;
                model.StockValue = arg.StockValue;
                model.SupplierName = arg.SupplierName;
                model.CompanyName = arg.CompanyName;
                model.Address = arg.Address;
                model.Phone = arg.Phone;
                model.City = arg.City;
                model.PaymentTerms = arg.PaymentTerms;
                model.OtherTerms = arg.OtherTerms;
                model.DeliverTerms = arg.DeliverTerms;
                model.TermsConditions = arg.TermsConditions;
                model.Edit = true;
            }
            return PartialView("~/Views/Stock/PurchaseOrder/Partial/PurchaseOrderDetails.cshtml", model);

        }

        public ActionResult PurchaseOrderGrid()
        {
            PurchaseOrderModel vm = new PurchaseOrderModel();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            vm.purchaseOrderGrid = purchaseOrderManager.PurchaseOrderGrid();
            List<SizeRangeDetails> sizeRangeDetais = new List<SizeRangeDetails>();
            SizeRangeDetailsManager sizeRangeManager = new SizeRangeDetailsManager();
            sizeRangeDetais = sizeRangeManager.Get();
            vm.SizeRangeDetailslist = sizeRangeDetais;
            return PartialView("~/Views/Stock/PurchaseOrder/Partial/PurchaseOrderGrid.cshtml", vm);
        }

        public ActionResult PurchaseOrder()
        {
            PurchaseOrderModel vm = new PurchaseOrderModel();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            return View("~/Views/Stock/PurchaseOrder/PurchaseOrder.cshtml", vm);
        }
        public ActionResult GetIndentDetails(int IndentNo)
        {
            IndentManager indentManager = new IndentManager();
            MaterialManager materialManager = new MaterialManager();
            Indent indent = new Indent();
            indent = indentManager.GetIndentNO(IndentNo);
            return Json(indent, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIndentMaterials_(string Indentid, int Supplierid)
        {
            string pono = "";
            List<IndentMaterial> indentMaterials = new List<IndentMaterial>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            string indentNo = Indentid.TrimEnd(',');
            var indentMaterials_ = indentMaterialManager.GetIndentIDWithSupplierPurchaseOrder(Supplierid, indentNo).ToList();
            foreach (var item in indentMaterials_)
            {
                indentMaterials.Add(item);
            }
            var jsonResult = Json(new { IndentMaterials = indentMaterials, pono = pono }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           
        }
        [HttpGet] 
        public ActionResult GetIndentPOQty(string Indentid, string MaterialID)
        {
            List<IndentMaterials> indentMaterials = new List<IndentMaterials>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            List<PurchaseOrder> purchaseOrder = new List<PurchaseOrder>();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            purchaseOrder = purchaseOrderManager.GetIndentPoPendingQty(Convert.ToInt32(MaterialID), Indentid);
            decimal? PendingQty = 0;
            bool isExist = false;
            if (purchaseOrder != null && purchaseOrder.Count > 0)
            {
                decimal? IndentQty = purchaseOrder.FirstOrDefault().Qty;
                decimal? PoQty = purchaseOrder.Sum(x => x.PoQty);
                PendingQty = IndentQty - PoQty;
                isExist = true;
            }

            return Json(new { PoPending = PendingQty, isExist = isExist }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCategoryWithMaterials(string Indentid, int Supplierid, int Category, string StoreID)
        {
            string pono = "";
            List<IndentMaterial> indentMaterials = new List<IndentMaterial>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            purchaseOrder = purchaseOrderManager.GetSupplierWithIndentID(Supplierid, Indentid);
            if (purchaseOrder == null)
            {
                pono = GetPONo();
            }
            indentMaterials = indentMaterialManager.GetIndentIDWithSupplier(Supplierid, Indentid);
            if (!string.IsNullOrEmpty(StoreID))
            {
                indentMaterials = indentMaterials.Where(x => x.StoreId == (StoreID)).ToList();
            }
            if (Category != 0)
            {
                indentMaterials = indentMaterials.Where(x => x.MaterialCategoryMasterId == Category).ToList();
            }
            return Json(new { indent = indentMaterials, pono = pono }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGroupWithMaterials(string Indentid, int Supplierid, int MaterialGroupMasterId)
        {
            string pono = "";
            List<IndentMaterial> indentMaterials = new List<IndentMaterial>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            purchaseOrder = purchaseOrderManager.GetSupplierWithIndentID(Supplierid, Indentid);
            if (purchaseOrder == null)
            {
                pono = GetPONo();
            }
            indentMaterials = indentMaterialManager.GetIndentIDWithSupplier(Supplierid, Indentid);
            indentMaterials = indentMaterials.Where(x => x.MaterialGroupMasterId == MaterialGroupMasterId).ToList();
            return Json(new { indent = indentMaterials, pono = pono }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetIndentSupplierid(int Supplierid, string Indentno)
        {
            IndentManager indentManager = new IndentManager();
            PurchaseOrderManager PurchaseOrderManager = new PurchaseOrderManager();
            List<IndentMaterial> indentMaterials = new List<IndentMaterial>();
            IndentMaterialManager indentMaterialManager = new IndentMaterialManager();
            indentMaterials = indentMaterialManager.GetIndentIDWithSupplier(Supplierid, Indentno);
            return Json(indentMaterials, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string filter)
        {
            PurchaseOrderModel model = new PurchaseOrderModel();
            List<PurchaseOrder> purchaseOrder = new List<PurchaseOrder>();
            PurchaseOrderManager purchaseOrderManager = new PurchaseOrderManager();
            PurchaseOrderManager PurchaseOrderManager = new PurchaseOrderManager();
            model.purchaseOrderGrid = purchaseOrderManager.PurchaseOrderGrid();
            model.purchaseOrderGrid = model.purchaseOrderGrid.Where(x => x.PoNo.ToLower().Contains(filter.ToLower())).ToList();
            


            return PartialView("~/Views/Stock/PurchaseOrder/Partial/PurchaseOrderGrid.cshtml", model);
        }
        #endregion
    }
}
