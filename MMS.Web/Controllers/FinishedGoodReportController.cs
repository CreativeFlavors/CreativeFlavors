using MMS.Repository.Managers.StockManager;
using MMS.Repository.Managers;
using MMS.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class FinishedGoodReportController : Controller
    {
       public ActionResult FinishedGoodMaster()
        {
            return View();
        }
        public ActionResult FinishedGoodGrid()
        {
         

                return PartialView("~/Views/FinishedGoodReport/Partial/FinishedGoodGrid.cshtml");
            
          
        }
    }
}
