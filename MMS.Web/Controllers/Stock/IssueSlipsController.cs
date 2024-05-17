using MMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.Stock
{
      [CustomFilter]
    public class IssueSlipsController : Controller
    {


        public ActionResult SingleIssueSlip()
        {
            return View("~/Views/Stock/IssueSlips/SingleIssueSlip.cshtml");

        }
        public ActionResult SingleIssueSlipGrid()
        {
          
            return PartialView("~/Views/Stock/IssueSlips/Partial/SingleIssueSlipGrid.cshtml");
        }

    }
}
