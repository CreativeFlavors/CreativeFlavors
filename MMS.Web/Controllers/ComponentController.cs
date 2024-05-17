using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models;
using MMS.Web.Models.Component;
using MMS.Repository.Managers;
using MMS.Core.Entities;
using System.IO;
using System.Web.Helpers;
using MMS.Common;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class ComponentController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult Component()
        {
            ComponentModel vm = new ComponentModel();
            return View(vm);
        }
        public ActionResult ComponentGrid()
        {
            ComponentModel vm = new ComponentModel();
            ComponentManager componentManager = new ComponentManager();
            vm.ComponentMasterList = componentManager.Get();

            return PartialView("Partial/ComponentGrid", vm);
        }
        [HttpPost]
        public ActionResult ComponentDetails(int ComponentMasterId)
        {
            ComponentManager componentManager = new ComponentManager();
            ComponentMaster componentMaster = new ComponentMaster();
            ComponentModel model = new ComponentModel();
            componentMaster = componentManager.GetComponentMasterId(ComponentMasterId);
            if (componentMaster != null)
            {
                model.ComponentName = componentMaster.ComponentName;
                model.Image = componentMaster.Image;
                model.UsedIn = componentMaster.UsedIn;
                model.ComponentMasterId = componentMaster.ComponentMasterId; 
            }
            return PartialView("Partial/ComponentDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult Component(ComponentModel componentModel)
        {
            ComponentMaster componentMasters = new ComponentMaster();
            if (ModelState.IsValid)
            {
                ComponentMaster componentMaster = new ComponentMaster();
                ComponentManager componentManager = new ComponentManager();

                if (Session["UploadImage"]!=null&&Session["UploadImage"].ToString() != "")
                {
                    componentModel.Image = Session["UploadImage"].ToString();
                }

                componentMaster = componentManager.GetComponetName(componentModel.ComponentName);
                if (componentMaster == null)
                {
                    componentMasters.ComponentName = componentModel.ComponentName;
                    componentMasters.Image = componentModel.Image;
                    componentMasters.CreatedDate = DateTime.Now;
                    componentMasters.UsedIn = componentModel.UsedIn;
                    componentMasters.UpdatedDate = DateTime.Now;
                    componentManager.Post(componentMasters);
                }
                else
                {
                    return Json(componentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(componentMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(ComponentModel componentModel)
        {
            ComponentMaster ComponentMasters = new ComponentMaster();

            if (Session["UploadImage"]!=null&&Session["UploadImage"].ToString() != "")
            {
                componentModel.Image = Session["UploadImage"].ToString();
            }

            if (ModelState.IsValid)
            {
                ComponentMaster componentMaster = new ComponentMaster();
                ComponentManager componentManager = new ComponentManager();               
                componentMaster = componentManager.GetComponentMasterId(componentModel.ComponentMasterId);
                if (componentMaster != null)
                {
                    ComponentMasters.ComponentName = componentModel.ComponentName;
                    if (componentModel.Image != null)
                    {
                        ComponentMasters.Image = componentModel.Image;
                    }
                    else
                    {
                        ComponentMasters.Image = componentMaster.Image;
                    }
                    ComponentMasters.UsedIn = componentModel.UsedIn;
                    ComponentMasters.ComponentMasterId = componentModel.ComponentMasterId; ;
                    ComponentMasters.CreatedDate = componentMaster.CreatedDate;
                    ComponentMasters.UpdatedDate = DateTime.Now;
                    ComponentMasters.CreatedBy = "";
                    ComponentMasters.UpdatedBy = "";
                    componentManager.Put(ComponentMasters);
                }
                else
                {
                    return Json(ComponentMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(ComponentMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int ComponentMasterId)
        {
            ComponentMaster componentMaster = new ComponentMaster();
            string status = "";
            ComponentManager componentManager = new ComponentManager();
            componentMaster = componentManager.GetComponentMasterId(ComponentMasterId);
            if (componentMaster != null)
            {
                status = "Success";
                componentManager.Delete(componentMaster.ComponentMasterId);
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
                    Session["UploadImage"] = _imgname + _ext;
                    var _comPath = Server.MapPath("~/ColorUpload/CompenentImages/") + _imgname ;
                    _imgname = _imgname + _ext;

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
            List<ComponentMaster> componentMasterlist = new List<ComponentMaster>();
            ComponentManager componentManager = new ComponentManager();
            componentMasterlist = componentManager.Get();
            componentMasterlist = componentMasterlist.Where(x => x.ComponentName.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.UsedIn.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();          
            ComponentModel model = new ComponentModel();
            model.ComponentMasterList = componentMasterlist;
            return PartialView("Partial/ComponentGrid", model);
        }
        #endregion
        public ActionResult ComponentGrid_Get()
        {
            ComponentModel vm = new ComponentModel();
            ComponentManager componentManager = new ComponentManager();
            var component = componentManager.Get();

            return Json(new { component = component }, JsonRequestBehavior.AllowGet);
        }

    }
}
