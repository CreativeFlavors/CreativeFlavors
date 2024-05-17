using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models;
using MMS.Web.Models.ColorMasterModel;
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
    public class ColorController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult Color()
        {
            MMS.Web.Models.ColorMasterModel.ColorMasterModel vm = new ColorMasterModel();
            return View(vm);
        }
        public ActionResult ColorGrid()
        {
            ColorMasterModel vm = new ColorMasterModel();
            ColorManager colorManager = new ColorManager();
            vm.ColorMasterList = colorManager.Get();

            return PartialView("Partial/ColorGrid", vm);
        }
        [HttpPost]
        public ActionResult EditColorList(int ColorMasterId)
        {
            ColorManager colorManager = new ColorManager();
            ColorMaster colorMaster = new ColorMaster();
            ColorMasterModel model = new ColorMasterModel();
            colorMaster = colorManager.GetColorMasterID(ColorMasterId);
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getColorCodeId();
            

            if (colorMaster != null)
            {
                model.Color = colorMaster.Color;
                model.ColorImages = colorMaster.ColorImages;
                model.ColorMasterId = colorMaster.ColorMasterId;
                model.CreatedBy = colorMaster.CreatedBy;
                model.UpdatedBy = colorMaster.UpdatedBy;
                model.CreatedDate = colorMaster.CreatedDate.Value;
                model.UpdatedDate = colorMaster.UpdatedDate.Value;

            }
            if (model.ColorMasterId != 0)
            {
                model.BuyerColorCode = colorMaster.BuyerColorCode;
            }
            else
            {
                ID++;
                model.BuyerColorCode = "CLR" + ID;
            }
            return PartialView("Partial/EditColorList", model);
        }


        [HttpPost]
        public JsonResult GetColourStatus(string ColourCode)
        {
            ColorManager colorManager = new ColorManager();
            string Message = string.Empty;
            ColorMaster colorMaster = new ColorMaster();
            colorMaster = colorManager.GetColorCode(ColourCode);
            if (colorMaster != null)
            {
                Message = "Exist";
            }
            else
            {
                Message = "Failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult Color(ColorMasterModel ColorMasterModel)
        {
            ColorMaster ColorMasters = new ColorMaster();
            
            if (ModelState.IsValid)
            {
                ColorMaster colorMaster = new ColorMaster();
                ColorManager colorManager = new ColorManager();
                colorMaster = colorManager.GetColor(ColorMasterModel.Color);
                int ID = MMS.Web.ExtensionMethod.HtmlHelper.getColorCodeId();
                ID++;
                if (colorMaster == null)
                {
                    ColorMasters.Color = ColorMasterModel.Color;                 
                    ColorMasters.BuyerColorCode = ColorMasterModel.BuyerColorCode;
                    ColorMasters.ColorImages = ColorMasterModel.ColorImages;
                    ColorMasters.ColorMasterId = ColorMasterModel.ColorMasterId;
                    ColorMasters.CreatedDate = DateTime.Now;  
                    colorManager.Post(ColorMasters);
                  
                }
                else if (colorMaster != null && colorMaster.Color == ColorMasterModel.Color && ColorMasterModel.ColorMasterId == 0)
                {
                    colorMaster.ColorMasterId = 0;
                    return Json(colorMaster, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(ColorMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(ColorMasters, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateColor(ColorMasterModel colorMasterModel)
        {
            ColorMaster ColorMasters = new ColorMaster();
            if (ModelState.IsValid)
            {
                ColorMaster colorMaster = new ColorMaster();
                ColorManager colorManager = new ColorManager();                
                colorMaster = colorManager.GetcolorID(colorMasterModel.ColorMasterId);
                if (colorMaster != null)
                {
                    ColorMasters.Color = colorMasterModel.Color;
                    ColorMasters.ColorMasterId = colorMasterModel.ColorMasterId;
                    ColorMasters.BuyerColorCode = colorMasterModel.BuyerColorCode;
                    if (colorMasterModel.ColorImages != null)
                    {
                        ColorMasters.ColorImages = colorMasterModel.ColorImages; 
                    }
                    else
                    {
                        ColorMasters.ColorImages = colorMaster.ColorImages; 
                    }                                                   
                    ColorMasters.CreatedDate = colorMaster.CreatedDate;
                    ColorMasters.UpdatedDate = DateTime.Now;                    
                    colorManager.Put(ColorMasters);
                }
                else
                {
                    return Json(ColorMasters, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(ColorMasters, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteColor(int ColorMasterId)
        {
            ColorMaster users = new ColorMaster();
            string status = "";
            ColorManager colorManager = new ColorManager();
            users = colorManager.GetColorMasterID(ColorMasterId);
            if (users != null)
            {
                status = "Success";
                colorManager.Delete(users.ColorMasterId);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
       
        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<ColorMaster> users = new List<ColorMaster>();            
            ColorManager colorManager = new ColorManager();
            users = colorManager.Get();
            users = users.Where(x => x.Color.ToLower().Contains(filter.ToLower().Trim()) || x.BuyerColorCode.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            Session["ColorList"] = users;
            ColorMasterModel model = new ColorMasterModel();
            model.ColorMasterList = users;
            return PartialView("Partial/ColorGrid", model);
        }
        #endregion
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
                    var _comPath = Server.MapPath("/ColorUpload/") + _imgname + _ext;
                    _imgname = _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;
                    
                    pic.SaveAs(path);
                    
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

    }
}
