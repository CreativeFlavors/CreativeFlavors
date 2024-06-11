using Excel.Log;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Repository.Service;
using MMS.Web.Models;
using MMS.Web.Models.Product;
using MMS.Web.Models.Production;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class MRPController : Controller
    {
        #region views

        public ActionResult MRP_Master()
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            Salesorders salesorders = new Salesorders();
            var mrpdetails = salesorderManager.GetmrpList();
            salesorders.MRP_Detail= mrpdetails;

            return View(salesorders);
        }


        public ActionResult MRP_Production()
        {
            return PartialView("MRP_Production");
        }
        #endregion
    }
}
