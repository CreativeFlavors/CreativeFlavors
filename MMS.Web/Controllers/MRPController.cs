using Excel.Log;
using iTextSharp.text;
using MMS.Core.Entities;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers;
using MMS.Repository.Managers.StockManager;
using MMS.Repository.Service;
using MMS.Web.Models;
using MMS.Web.Models.CustomerTransaction;
using MMS.Web.Models.Product;
using MMS.Web.Models.Production;
using MMS.Web.Models.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMS.Web.Controllers
{
    public class MRPController : Controller
    {
        #region views

        public ActionResult MRP_Master(int page = 1, int pageSize = 6)
        {
            SalesorderManager salesorderManager = new SalesorderManager();
            Salesorders salesorders = new Salesorders();
            var mrpdetails = salesorderManager.GetmrpList();
            List<Salesorders> totalList = new List<Salesorders>();

            foreach (var item in mrpdetails)
            {
                Salesorders model = new Salesorders();
                model.ProductName = item.ProductName;
                model.SalesQuantity = item.salesorderquantity;
                model.AvailableStock = item.AvailableStock;
                model.ProductionDueDate = item.ProductionDueDate;
                model.ProductionQty = item.ProductionQty;
                model.Status = item.Status;
                model.ProductID = item.Product_id;
                model.subassemblyqty = item.Sub_quantity;
                model.mrp_Material_Lists = salesorderManager.GetmrpmaterialList(item.Product_id);
                model.mrp_subassembly_Lists = salesorderManager.GetmrpsubassemblyList(item.Subassemblyproid);
                totalList.Add(model);
            }
            var totalCount = totalList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            int startIndex = (page - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);
            totalList = totalList.OrderByDescending(d => d.ProductID)
                         .Skip(startIndex)
                         .Take(pageSize)
                         .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;


            return View(totalList);
        }


        public ActionResult MRP_Production()
        {
            return PartialView("MRP_Production");
        }
        #endregion
    }
}
