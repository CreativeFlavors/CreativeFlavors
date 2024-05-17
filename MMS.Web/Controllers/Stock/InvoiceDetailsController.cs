using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Common;
using MMS.Core.Entities;
using MMS.Web.Models.InvoiceDetails;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.Addressdetails;
using MMS.Repository.Managers;
using static iTextSharp.text.pdf.AcroFields;

namespace MMS.Web.Controllers.Stock
{
    [CustomFilter]
    public class InvoiceDetailsController : Controller
    {
        #region Accounts View
        [HttpGet]
        public ActionResult InvoiceMaster()
        {
            InvoiceDetails model = new InvoiceDetails();
            return View(model);
        }
        [HttpGet]
        public ActionResult InvoiceDetailsGrid()
        {
            InvoiceDetails model = new InvoiceDetails();
            InvoiceDetailsManages InvoiceDetailsManagesManager = new InvoiceDetailsManages();
            //model.Invoicedetailslist = InvoiceDetailsManagesManager.Get();
            CustAddressMangers oCustAddressMangers = new CustAddressMangers();
            var custaddlist = oCustAddressMangers.Get();
            model.Invoicedetailslist = InvoiceDetailsManagesManager.Get()
               .Select(item => new InvoiceDetails
               {
                   Id = item.Id,
                   OrderId = item.OrderId,
                   CCode = item.CCode,
                   OrderDate = item.OrderDate,
                   OrderFullFillDate = item.OrderFullFillDate,
                   Notes = item.Notes,
                   AddCode = item.AddCode,
                   ShipAddCode = item.ShipAddCode,
                   BillAddCode = item.BillAddCode,
                   TotalAmount = item.TotalAmount,
                   DiscAmount = item.DiscAmount,
                   TaxAmount = item.TaxAmount,
                   BillAmount = item.BillAmount,
                   FreightAmount = item.FreightAmount,
                   IsQuote = item.IsQuote,
                   IsActive = item.IsActive,
                   //CreatedBy = item.CreatedBy,
                   //CreatedDate = item.CreatedDate,
                   TotalQty = item.TotalQty,
                   TotalItems = item.TotalItems,
                   ShipCode = item.ShipCode,
                   ExpectedDeliveryDate = item.ExpectedDeliveryDate
               })
                    .ToList();
            foreach (var item1 in model.Invoicedetailslist)
            {
                foreach (var item2 in custaddlist)
                {
                    if (item1.AddCode == item2.AddressType)
                    {
                        item1.add1 = item2.Add1;
                    }
                    if (item1.ShipAddCode == item2.AddressType)
                    {
                        item1.add2 = item2.Add2;
                    }
                    if (item1.BillAddCode == item2.AddressType)
                    {
                        item1.add3 = item2.Add3;
                    }
                }
            }

            return PartialView("Partial/InvoiceDetailsGrid", model);
        }
        [HttpGet]
        public ActionResult InvoiceDetails()
        {
            InvoiceDetails model = new InvoiceDetails();

            return PartialView("Partial/InvoiceDetails", model);
        }
       
        [HttpGet]
        public ActionResult GetInvoiceAddressDetailbind(int orderid)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<Core.Entities.Stock.getbuyerorderaddressdetails> result = new List<Core.Entities.Stock.getbuyerorderaddressdetails>();
            result = buyerOrderEntryManager.getbuyerorderaddressdetails(orderid);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetBuyerOrderlist(string buyername)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<Core.Entities.Stock.GetBuyerOrderlist> result = new List<Core.Entities.Stock.GetBuyerOrderlist>();
            result = buyerOrderEntryManager.GetBuyerOrderlist(Convert.ToInt32(buyername));

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
