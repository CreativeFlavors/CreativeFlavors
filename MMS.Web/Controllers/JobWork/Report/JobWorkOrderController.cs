using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.JobWork.Report
{
    public class JobWorkOrderController : Controller
    {
        //
        // GET: /JobWorkOrder/

        public ActionResult JobWorkOrder()
        {
            return View("/Views/JobworkReport/JobworkOrderReport.cshtml");
        }
       
        [HttpPost]
        public ActionResult RedirectToBinCardAspx(string IssueType, string Jobworktype_Id)
        {

            //string IssueType_ = IssueType.Trim();
            //string Jobworktype_Id_ = Jobworktype_Id.Trim();
            string IssueType_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(IssueType.Trim()));
            string Jobworktype_Id_ = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Jobworktype_Id.Trim()));

            var result = new { IssueType = IssueType_, Jobworktype_Id = Jobworktype_Id_ };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
