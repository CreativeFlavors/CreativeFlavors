using MMS.Common;
using MMS.Core.Entities.Stock;
using MMS.Repository.Managers.StockManager;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.InvoiceDetails;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class InvoiceDetailsController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult InvoiceDetails()
        {
            InvoiceDetails model = new InvoiceDetails();

            return PartialView("Partial/InvoiceDetails", model);
        }
        [HttpGet]
        public ActionResult InvoiceDetailsGrid()
        {
            InvoiceDetails model = new InvoiceDetails();
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            model.Invoicedetailslist = buyerOrderEntryManager.GetnvoiceDetailslist();

            return PartialView("Partial/InvoiceDetailsGrid", model);
        }
        [HttpGet]
        public ActionResult GetInvoiceAddressDetailbind(int orderid)
        {
            BuyerOrderEntryManager buyerOrderEntryManager = new BuyerOrderEntryManager();
            List<Core.Entities.Stock.getbuyerorderaddressdetails> result = new List<Core.Entities.Stock.getbuyerorderaddressdetails>();
            result = buyerOrderEntryManager.getbuyerorderaddressdetails(orderid);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
