using MMS.Common;
using MMS.Data.StoredProcedureModel;
using MMS.Repository.Managers.GateEntryManager;
using MMS.Web.Models.GateEntryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Repository.Managers.StockManager;

namespace MMS.Web.Controllers.JobWork
{
    [CustomFilter]
    public class JobWorkOutwardController : Controller
    {
        //
        // GET: /JobWorkOutward/
        GateEntryOutwardCheck gateEntryOutwardCheck = new GateEntryOutwardCheck();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JobWorkOutward()
        {
            try
            {                
                return View("~/Views/Jobwork/Job_Master/JobWorkGateOutward/JobWorkOutwardCheckMaster.cshtml", gateEntryOutwardCheck);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult JobWorkOutwardGrid(string IssueSlipNo)
        {
            try
            {
                IssueSlip_SingleManager IssueSlip_SingleManager = new IssueSlip_SingleManager();
                   GateEntryOutwardManager outwardManager = new GateEntryOutwardManager();
                List<OutwardGateEntryCheck> gateEntryOutwardCheckList = new List<OutwardGateEntryCheck>();
                gateEntryOutwardCheckList = outwardManager.GetOutwardGateEntry(IssueSlipNo);
                string year = DateTime.Now.Year.ToString();
                int Issue_No = IssueSlip_SingleManager.GETJob_IssueCount();
                string GateEntryNo = "GEIM " + Issue_No + "/" + year;
                ViewBag.GateOutNo = GateEntryNo;
                return PartialView("~/Views/Jobwork/Job_Master/JobWorkGateOutward/Partial/JobWorkOutwardCheckGrid.cshtml", gateEntryOutwardCheckList);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult JobWorkOutwardGridSearch(string IssueSlipNo)
        {
            try
            {
                IssueSlip_SingleManager IssueSlip_SingleManager = new IssueSlip_SingleManager();
                GateEntryOutwardManager outwardManager = new GateEntryOutwardManager();
                List<OutwardGateEntryCheck> gateEntryOutwardCheckList = new List<OutwardGateEntryCheck>();                
                gateEntryOutwardCheckList = outwardManager.GetOutwardGateEntry(IssueSlipNo);
                var Issueis = IssueSlip_SingleManager.GetMultipleIssueSlip(IssueSlipNo).FirstOrDefault();

                string year = DateTime.Now.Year.ToString();
                int Issue_No = IssueSlip_SingleManager.GETJob_IssueCount();
                string GateEntryNo = "GEIM " + (Issue_No+1) + "/" + year;

                foreach (var item in gateEntryOutwardCheckList)
                {
                    var size = outwardManager.GetOutwardSizeRange(item.IssueSlipId);
                    item.sizerange = size;
                }
                //return PartialView("~/Views/Jobwork/Job_Master/JobWorkGateOutward/Partial/JobWorkOutwardCheckGrid.cshtml", gateEntryOutwardCheckList);
                return Json(new { gateEntryOutwardCheckList = gateEntryOutwardCheckList, Issue= Issueis, GateEntryNo= GateEntryNo },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult JobWorkOutwardChecked(int IssueSlipId,bool isGatechecked,string GateEntryNo)
        {
            try
            {
                GateEntryOutwardManager outwardManager = new GateEntryOutwardManager();
                var result=outwardManager.GateDetailsChecked(IssueSlipId, isGatechecked, GateEntryNo);
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
