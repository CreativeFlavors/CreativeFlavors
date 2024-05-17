using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.MachineryMasterModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class MachineryMasterController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult MachineryMaster()
        {
            MachineryMasterModel vm = new MachineryMasterModel();
            return View(vm);
        }
        public ActionResult MachineryMasterGrid()
        {
            MachineryMasterModel vm = new MachineryMasterModel();
            MachineryMasterManager machineryMasterManager = new MachineryMasterManager();
            vm.MachineryMasterList = machineryMasterManager.Get();

            return PartialView("Partial/MachineryMasterGrid", vm);
        }
        [HttpPost]
        public ActionResult MachineryMasterDetails(int MachineryMasterId)
        {
            MachineryMasterManager machineryMasterManager = new MachineryMasterManager();
            MachineryMaster machineryMaster = new MachineryMaster();
            MachineryMasterModel model = new MachineryMasterModel();
            int id_;
            machineryMaster = machineryMasterManager.GetMachineryMasterId(MachineryMasterId);
            string id = MMS.Web.ExtensionMethod.HtmlHelper.getAutoGenerateMachinaryID();
            if (id == "")
            {
                id = "0";
            }
            id_ = Convert.ToInt32(id);
            id_++;
            if (MachineryMasterId == 0)
            {
                model.MachineCode = "MAC" + id_;
            }
            else
            {
                model.MachineCode = machineryMaster.MachineCode;
            }
            if (machineryMaster != null)
            {
                model.MachineryMasterId = machineryMaster.MachineryMasterId;
                model.MachineName = machineryMaster.MachineName;
                model.Make = machineryMaster.Make;
                model.Model = machineryMaster.Model;
                model.Image = machineryMaster.Image;
                model.AssesListNo = machineryMaster.AssesListNo;
                model.MachineSerialNo = machineryMaster.MachineSerialNo;
                model.Kilowatt = machineryMaster.Kilowatt;
                model.HorsePower = machineryMaster.HorsePower;
                model.Volt = machineryMaster.Volt;
                model.OperationUsedFor = machineryMaster.OperationUsedFor;
                model.Specification = machineryMaster.Specification;
                model.CreatedDate = machineryMaster.CreatedDate;
                model.UpdatedDate = machineryMaster.UpdatedDate;
                model.UpdatedBy = machineryMaster.UpdatedBy;
            }
            return PartialView("Partial/MachineryMasterDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult MachineryMaster(MachineryMasterModel machineryMasterModel)
        {
            MachineryMaster machineryMasters = new MachineryMaster();
            MachineryMaster machineryMaster = new MachineryMaster();
            MachineryMasterManager machineryMasterManager = new MachineryMasterManager();

            if (Session["UploadImage"] != null && Session["UploadImage"].ToString() != "")
            {
                machineryMasterModel.Image = Session["UploadImage"].ToString();
            }

            machineryMaster = machineryMasterManager.GetMachineName(machineryMasterModel.MachineName);
            machineryMasterModel.MachineCode = machineryMasterModel.MachineCode.Substring(3);
            if (machineryMaster == null)
            {
                machineryMasters.MachineCode = machineryMasterModel.MachineCode;
                machineryMasters.MachineName = machineryMasterModel.MachineName;
                machineryMasters.Make = machineryMasterModel.Make;
                machineryMasters.Model = machineryMasterModel.Model;
                machineryMasters.Image = machineryMasterModel.Image;
                machineryMasters.AssesListNo = machineryMasterModel.AssesListNo;
                machineryMasters.MachineSerialNo = machineryMasterModel.MachineSerialNo;
                machineryMasters.Kilowatt = machineryMasterModel.Kilowatt;
                machineryMasters.HorsePower = machineryMasterModel.HorsePower;
                machineryMasters.Volt = machineryMasterModel.Volt;
                machineryMasters.OperationUsedFor = machineryMasterModel.OperationUsedFor;
                machineryMasters.Specification = machineryMasterModel.Specification;
                machineryMasters.CreatedDate = DateTime.Now;
                machineryMasterManager.Post(machineryMasters);
            }
            else if (machineryMaster != null && machineryMasterModel.MachineName == machineryMaster.MachineName && machineryMasterModel.MachineryMasterId == 0)
            {
                machineryMasters.MachineryMasterId = 0;
                return Json(machineryMasters, JsonRequestBehavior.AllowGet);
            }
            return Json(machineryMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(MachineryMasterModel machineryMasterModel)
        {
            MachineryMaster machineryMasters = new MachineryMaster();
            if (ModelState.IsValid)
            {
                MachineryMaster machineryMaster = new MachineryMaster();
                MachineryMasterManager machineryMasterManager = new MachineryMasterManager();
                machineryMaster = machineryMasterManager.GetMachineryMasterId(machineryMasterModel.MachineryMasterId);
                if (machineryMasterModel.Image != null)
                {
                    machineryMasters.Image = machineryMasterModel.Image;
                }
                else
                {
                    machineryMasters.Image = machineryMaster.Image;
                }
                if (machineryMaster != null)
                {
                    machineryMasters.MachineryMasterId = machineryMasterModel.MachineryMasterId;
                    machineryMasters.MachineCode = machineryMasterModel.MachineCode;
                    machineryMasters.MachineName = machineryMasterModel.MachineName;
                    machineryMasters.Make = machineryMasterModel.Make;
                    machineryMasters.Model = machineryMasterModel.Model;
                    machineryMasters.AssesListNo = machineryMasterModel.AssesListNo;
                    machineryMasters.MachineSerialNo = machineryMasterModel.MachineSerialNo;
                    machineryMasters.Kilowatt = machineryMasterModel.Kilowatt;
                    machineryMasters.HorsePower = machineryMasterModel.HorsePower;
                    machineryMasters.Volt = machineryMasterModel.Volt;
                    machineryMasters.OperationUsedFor = machineryMasterModel.OperationUsedFor;
                    machineryMasters.Specification = machineryMasterModel.Specification;
                    machineryMasters.UpdatedDate = DateTime.Now;
                    machineryMasters.UpdatedBy = machineryMaster.UpdatedBy;
                    machineryMasterManager.Put(machineryMasters);
                }
                else
                {
                    return Json(machineryMasters, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(machineryMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int MachineryMasterId)
        {
            MachineryMaster machineryMaster = new MachineryMaster();
            string status = "";
            MachineryMasterManager machineryMasterManager = new MachineryMasterManager();
            machineryMaster = machineryMasterManager.GetMachineryMasterId(MachineryMasterId);
            if (machineryMaster != null)
            {
                status = "Success";
                machineryMasterManager.Delete(machineryMaster.MachineryMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("~/Images/MachinaryImage/") + _imgname + _ext;
                    _imgname = _imgname + _ext;
                    Session["UploadImage"] = _imgname;
                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string filter)
        {
            List<MachineryMaster> machineryMasterlsit = new List<MachineryMaster>();
            MachineryMasterManager commisionCritiriaManager = new MachineryMasterManager();
            machineryMasterlsit = commisionCritiriaManager.Get();
            machineryMasterlsit = machineryMasterlsit.Where(x => x.MachineName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.MachineCode.Trim().Contains(filter.ToString().Trim())).ToList();
            MachineryMasterModel model = new MachineryMasterModel();
            model.MachineryMasterList = machineryMasterlsit;
            return PartialView("Partial/MachineryMasterGrid", model);
        }
        #endregion

    }
}
