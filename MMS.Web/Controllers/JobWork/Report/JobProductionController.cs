using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers.JobWork.Report
{
    public class JobProductionController : Controller
    {
        //
        // GET: /JobProduction/

        public ActionResult Job_ProductionOrder()
        {
            return View("/Views/JobworkReport/Job_ProductionOrder.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToBinCardAspx( string To, string Type, string Buyer, string Season, string LOtNo, string Bom, string Io,string stage)
        {

            //string IssueType_ = IssueType.Trim();
            //string Jobworktype_Id_ = Jobworktype_Id.Trim();
            string to = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(To.Trim()));
            string type = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Type.Trim()));
            string buyer = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Buyer.Trim()));
            string season = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Season.Trim()));
            string lOtNo = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(LOtNo.Trim()));
            string bom = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Bom.Trim()));
            string io = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(Io.Trim()));
            string Stage = HttpUtility.UrlEncode(MMS.Web.ExtensionMethod.HtmlHelper.Encrypt(stage.Trim()));

            var result = new {  to = to, type= type, buyer = buyer , season = season, lOtNo= lOtNo, bom= bom, io= io, Stage= Stage };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
