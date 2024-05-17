using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers.StockManager;
using MMS.Core.Entities.Stock;
using MMS.Web.Models.StockModel;
using MMS.Common;

namespace MMS.Web.Controllers.Stock
{
      [CustomFilter]
    public class IssueSlipController : Controller
    {
        //
        // GET: /IssueSlip/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IssueSlipDetails()
        {
            string SearchFilter = Request.QueryString["SearchFilter"];
            IssueSlipManager ObjManager = new IssueSlipManager();
            IssueSlipModel ObjModel = new IssueSlipModel();

            if (SearchFilter == null)
            {
            ObjModel.IssueSlipList = ObjManager.Get();
            }
            else
            {
                 ObjModel.IssueSlipList = ObjManager.Get().Where(x=>x.Style.Contains(SearchFilter)).ToList();
            }

            return View(ObjModel);
        }

        public JsonResult SaveDetails(IssueSlipModel Model)
        {
            IssueSlip_MaterialDetails EntModel = new IssueSlip_MaterialDetails();
            IssueSlipManager ObjManager = new IssueSlipManager();
            bool Result = false;
            EntModel.IssueSlipId = Model.IssueSlipId;
            EntModel.IssueSlipFK = Model.IssueSlipFK;
            EntModel.OrderNo = Model.OrderNo;
            EntModel.Style = Model.Style;
            EntModel.IssueType = Model.IssueType;
            EntModel.NoOfSets = Model.NoOfSets;
            EntModel.StoreName = Model.StoreName;
            EntModel.MaterialName = Model.MaterialName;
            EntModel.ColourId = Model.ColourId;
            EntModel.CategoryId = Model.CategoryId;
            EntModel.GroupId = Model.GroupId;
            EntModel.RequiredQty = Model.RequiredQty;
            EntModel.AlredayIssued = Model.AlredayIssued;
            EntModel.CurrentIssue = Model.CurrentIssue;
            EntModel.Rate = Model.Rate;

            EntModel.CreatedBy = Session["UserName"].ToString();
            EntModel.NoOfSets = Model.NoOfSets;

            EntModel = ObjManager.Post(EntModel);
            return Json(EntModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIssueDetails(int IssueSlipId)
        {
            IssueSlip_MaterialDetails EntModel = new IssueSlip_MaterialDetails();
            IssueSlipManager ObjManager = new IssueSlipManager();

            EntModel = ObjManager.Get().Where(x => x.IssueSlipId == IssueSlipId).SingleOrDefault();


            return Json(EntModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteIssueDetails(int IssueSlipId)
        {
            IssueSlip_MaterialDetails EntModel = new IssueSlip_MaterialDetails();
            IssueSlipManager ObjManager = new IssueSlipManager();

            bool result = false;
           result= ObjManager.Delete(IssueSlipId);


           return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
