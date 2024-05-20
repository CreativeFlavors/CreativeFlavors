using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class ProductionController : Controller
    {
        public ActionResult ProductionMaster()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProductionGrid()
        {
            

            return PartialView("partial/ProductionGrid");
        }

        [HttpGet]
        public ActionResult ProductionDetails()
        {
            return View();
        }

    }
}
