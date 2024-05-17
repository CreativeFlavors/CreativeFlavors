using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.AttendanceIncentiveModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class AttendanceIncentiveController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult AttendanceIncentive()
        {
            AttendanceIncentiveModel vm = new AttendanceIncentiveModel();
            return View(vm);
        }
        public ActionResult AttendanceIncentiveGrid()
        {
            AttendanceIncentiveModel vm = new AttendanceIncentiveModel();
            AttendanceIncentiveManager attendanceIncentiveManager = new AttendanceIncentiveManager();
            vm.AttendanceIncentiveCalculationList = attendanceIncentiveManager.Get();

            return PartialView("Partial/AttendanceIncentiveGrid", vm);
        }
        [HttpPost]
        public ActionResult AttendanceIncentiveDetails(int AttendanceIncentiveCalcualtionId)
        {
            AttendanceIncentiveManager attendanceIncentiveManager = new AttendanceIncentiveManager();
            AttendanceIncentiveCalculation attendanceIncentiveCalculation = new AttendanceIncentiveCalculation();
            AttendanceIncentiveModel model = new AttendanceIncentiveModel();
            attendanceIncentiveCalculation = attendanceIncentiveManager.GetAttendanceIncentiveCalcualtionId(AttendanceIncentiveCalcualtionId);
            if (attendanceIncentiveCalculation != null)
            {
                model.AttendanceIncentiveCalcualtionId = attendanceIncentiveCalculation.AttendanceIncentiveCalcualtionId;
                model.Leave = attendanceIncentiveCalculation.Leave;
                model.Absent = attendanceIncentiveCalculation.Absent;
                model.Amount = attendanceIncentiveCalculation.Amount;
                model.CreatedDate = attendanceIncentiveCalculation.CreatedDate;
                model.UpdatedDate = attendanceIncentiveCalculation.UpdatedDate;
                model.CreatedBy = attendanceIncentiveCalculation.CreatedBy;
                model.UpdatedBy = attendanceIncentiveCalculation.UpdatedBy;
            }
            return PartialView("Partial/AttendanceIncentiveDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult AttendanceIncentive(AttendanceIncentiveModel model)
        {
            AttendanceIncentiveCalculation attendanceIncentiveCalculations = new AttendanceIncentiveCalculation();
            AttendanceIncentiveCalculation attendanceIncentiveCalculation = new AttendanceIncentiveCalculation();
            AttendanceIncentiveManager attendanceIncentiveManager = new AttendanceIncentiveManager();
            attendanceIncentiveCalculations.AttendanceIncentiveCalcualtionId = model.AttendanceIncentiveCalcualtionId;
            attendanceIncentiveCalculations.Leave = model.Leave;
            attendanceIncentiveCalculations.Absent = model.Absent;
            attendanceIncentiveCalculations.Amount = model.Amount;
            attendanceIncentiveCalculations.CreatedDate = DateTime.Now;
            attendanceIncentiveCalculations.UpdatedDate = DateTime.Now;
            attendanceIncentiveManager.Post(attendanceIncentiveCalculations);
            return Json(attendanceIncentiveCalculations, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(AttendanceIncentiveModel model)
        {
            AttendanceIncentiveCalculation attendanceIncentiveCalculations = new AttendanceIncentiveCalculation();
            if (ModelState.IsValid)
            {
                AttendanceIncentiveCalculation attendanceIncentiveCalculation = new AttendanceIncentiveCalculation();
                AttendanceIncentiveManager attendanceIncentiveManager = new AttendanceIncentiveManager();
                attendanceIncentiveCalculation = attendanceIncentiveManager.GetAttendanceIncentiveCalcualtionId(model.AttendanceIncentiveCalcualtionId);
                if (attendanceIncentiveCalculation != null)
                {
                    attendanceIncentiveCalculations.AttendanceIncentiveCalcualtionId = model.AttendanceIncentiveCalcualtionId;
                    attendanceIncentiveCalculations.Leave = model.Leave;
                    attendanceIncentiveCalculations.Absent = model.Absent;
                    attendanceIncentiveCalculations.Amount = model.Amount;
                    attendanceIncentiveCalculations.CreatedDate = attendanceIncentiveCalculation.CreatedDate;
                    attendanceIncentiveCalculations.UpdatedDate = DateTime.Now;
                    attendanceIncentiveCalculations.CreatedBy = attendanceIncentiveCalculation.CreatedBy;
                    attendanceIncentiveCalculations.UpdatedBy = attendanceIncentiveCalculation.UpdatedBy;
                    attendanceIncentiveManager.Put(attendanceIncentiveCalculations);
                }
                else
                {
                    return Json(attendanceIncentiveCalculations, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(attendanceIncentiveCalculations, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int AttendanceIncentiveCalcualtionId)
        {
            AttendanceIncentiveCalculation attendanceIncentiveCalculation = new AttendanceIncentiveCalculation();
            string status = "";
            AttendanceIncentiveManager attendanceIncentiveManager = new AttendanceIncentiveManager();
            attendanceIncentiveCalculation = attendanceIncentiveManager.GetAttendanceIncentiveCalcualtionId(AttendanceIncentiveCalcualtionId);
            if (attendanceIncentiveCalculation != null)
            {
                status = "Success";
                attendanceIncentiveManager.Delete(attendanceIncentiveCalculation.AttendanceIncentiveCalcualtionId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<AttendanceIncentiveCalculation> attendanceIncentiveCalculationList = new List<AttendanceIncentiveCalculation>();
            AttendanceIncentiveManager attendanceIncentiveManager = new AttendanceIncentiveManager();
            attendanceIncentiveCalculationList = attendanceIncentiveManager.Get();
            AttendanceIncentiveModel model = new AttendanceIncentiveModel();
            model.AttendanceIncentiveCalculationList = attendanceIncentiveCalculationList;
            return PartialView("Partial/AttendanceIncentiveGrid", model);
        }
        #endregion

    }
}
