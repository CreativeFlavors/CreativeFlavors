using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class BinCardController : Controller
    {
        //
        // GET: /BinCard/

       public ActionResult BinCard()
        {
            return View("/Views/Report/BinCard.cshtml");
        }
        public ActionResult BinCardReport()
        {

            return View("/Views/Report/BinCard.cshtml");
        }
        [HttpPost]
        public ActionResult RedirectToBinCardAspx(string materialType, string group, string storeNo, string from, string to, string category, string MaterialName)
        {
            
            string materialName = MaterialName.Trim();
            string MaterialType = materialType.Trim();
            string Group = group.Trim();
            string StoreNo = storeNo.Trim();
            string From = from.Trim();
            string To = to.Trim();
            string Category = category.Trim();
            var result = new { materialName = materialName, MaterialType = MaterialType, Group = Group, StoreNo = StoreNo, From = From, To = To, Category = Category };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}