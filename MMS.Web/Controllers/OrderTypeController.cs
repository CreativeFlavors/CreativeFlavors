using MMS.Common;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.OrderTypeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    public class OrderTypeController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult OrderType()
        {
            OrderTypeModel vm = new OrderTypeModel();
            return View(vm);
        }
        public ActionResult OrderTypeGrid()
        {
            OrderTypeModel vm = new OrderTypeModel();
            OrderTypeManager orderTypeManager = new OrderTypeManager();
            vm.OrderTypeList = orderTypeManager.Get();

            return PartialView("Partial/OrderTypeGrid", vm);
        }
        [HttpPost]
        public ActionResult OrderTypeDetails(int OrderTypeID)
        {
            OrderTypeManager orderTypeManager = new OrderTypeManager();
            OrderType orderType = new OrderType();
            OrderTypeModel model = new OrderTypeModel();
            orderType = orderTypeManager.GetOrderTypeID(OrderTypeID);
            string autoId = GetAutoid();
            if (orderType != null)
            {
                model.OrderTypeID = orderType.OrderTypeID;
                model.OrderTypeCode = orderType.OrderTypeCode;
                model.OrderTypeName = orderType.OrderTypeName;
                model.CreatedDate = orderType.CreatedDate;
                model.UpdatedDate = orderType.UpdatedDate;
                model.CreatedBy = orderType.CreatedBy;
                model.UpdatedBy = orderType.UpdatedBy;
            }
            if (OrderTypeID == 0)
            {
                model.OrderTypeCode = "ORT" + autoId;
            }
            else
            {
                model.OrderTypeCode = "ORT" + orderType.OrderTypeCode;
            }
            return PartialView("Partial/OrderTypeDetails", model);
        }
        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult OrderType(OrderTypeModel model)
        {
            OrderType orderTypes = new OrderType();
            if (ModelState.IsValid)
            {
                OrderType orderType = new OrderType();
                OrderTypeManager orderTypeManager = new OrderTypeManager();
                orderType = orderTypeManager.GetOrderTypeName(model.OrderTypeName);
                if (orderType == null)
                {
                    orderTypes.OrderTypeName = model.OrderTypeName;
                    orderTypes.OrderTypeCode = model.OrderTypeCode;
                    orderTypes.CreatedDate = DateTime.Now;
                    orderTypes.UpdatedDate = DateTime.Now;
                    string username = Session["UserName"].ToString();
                    orderTypes.CreatedBy = username;
                    orderTypeManager.Post(orderTypes);
                }
                else if (orderType != null && orderType.OrderTypeName == model.OrderTypeName && model.OrderTypeID == 0)
                {
                    orderTypes.OrderTypeID = 0;
                    return Json(orderTypes, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(orderTypes, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(orderTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(OrderTypeModel model)
        {
            OrderType orderTypes = new OrderType();
            if (ModelState.IsValid)
            {
                OrderType orderType = new OrderType();
                OrderTypeManager orderTypeManager = new OrderTypeManager();
                orderType = orderTypeManager.GetOrderTypeID(model.OrderTypeID);
                if (orderType != null)
                {
                    orderTypes.OrderTypeID = orderType.OrderTypeID;
                    orderTypes.OrderTypeCode = model.OrderTypeCode;
                    orderTypes.OrderTypeName = model.OrderTypeName;
                    orderTypes.CreatedDate = model.CreatedDate;
                    orderTypes.UpdatedDate = DateTime.Now;
                    string username = Session["UserName"].ToString();
                    orderTypes.CreatedBy = username;
                    orderTypes.UpdatedBy = username;
                    orderTypeManager.Put(orderTypes);
                }
                else
                {
                    return Json(orderTypes, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(orderTypes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int OrderTypeID)
        {
            OrderType orderType = new OrderType();
            string status = "";
            OrderTypeManager orderTypeManager = new OrderTypeManager();
            orderType = orderTypeManager.GetOrderTypeID(OrderTypeID);
            if (orderType != null)
            {
                status = "Success";
                orderTypeManager.Delete(orderType.OrderTypeID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Helper Method
        public ActionResult Search(string filter)
        {
            List<OrderType> orderTypeList = new List<OrderType>();
            OrderTypeManager orderTypeManager = new OrderTypeManager();
            orderTypeList = orderTypeManager.Get();
            orderTypeList = orderTypeList.Where(x => x.OrderTypeCode.ToLower().Trim().Contains(filter.ToLower().Trim()) || x.OrderTypeName.ToLower().Trim().Contains(filter.ToLower().Trim())).ToList();
            OrderTypeModel model = new OrderTypeModel();
            model.OrderTypeList = orderTypeList;
            return PartialView("Partial/OrderTypeGrid", model);
        }
        public string GetAutoid()
        {
            int ID = MMS.Web.ExtensionMethod.HtmlHelper.getAutoOrderTypeD();
            ID++;
            return ID.ToString();
        }
        #endregion


    }
}
