using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMS.Web.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/User/Index.cshtml");
        }

        public ActionResult UserGrid()
        {
            UserManager userManager = new UserManager();
            UserModel model = new UserModel();
            model.UserList = userManager.Get().ToList();
            return PartialView("~/Views/User/Partial/UserGrid.cshtml", model);
        }

        [HttpPost]
        public ActionResult Save(UserModel model)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users();
                UserManager userManager = new UserManager();
                users = userManager.GetUserName(model.Email);
                if (users == null)
                {
                    string PasswordSalt = userManager.Encryptdata(model.Password);
                    Users user_ = new Users();
                    Session["Email"] = model.Email;
                    Session["UserName"] = model.FirstName + "" + model.LastName;
                    user_.Email = model.Email;
                    user_.Password = model.Password;
                    user_.FirstName = model.FirstName;
                    user_.LastName = model.LastName;
                    user_.UserType = model.UserType;
                    user_.CreatedDate = DateTime.Now;
                    user_.UpdatedDate = DateTime.Now;
                    user_.IsActive = true;
                    user_.PasswordSalt = PasswordSalt;
                    user_.CreatedBy = Session["UserName"] as string;
                    user_.UpdatedBy = Session["UserName"] as string;
                    userManager.Post(user_);
                }
                else
                {
                    model.EmailExist = true;
                    return Json(model, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(UserModel model)
        {
            Users users = new Users();
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                Users users_ = new Users();
                users_ = userManager.GetUserID(model.UserID);
                if (users_ != null)
                {
                    string PasswordSalt = userManager.Encryptdata(model.Password);
                    users_.FirstName = model.FirstName;
                    users_.LastName = model.LastName;
                    users_.Email = model.Email;
                    users_.UserType = model.UserType;
                    users_.Password = model.Password;
                    users_.PasswordSalt = PasswordSalt;
                    users_.UpdatedDate = DateTime.Now;
                    users_.CreatedDate = users_.CreatedDate;
                    users_.CreatedBy = Session["UserName"] as string;
                    users_.UpdatedBy = Session["UserName"] as string;
                    userManager.Put(users_);
                    model.EmailExist = true;
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(model, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int UserID)
        {
            Users users = new Users();
            string status = "";
            UserManager userManager = new UserManager();
            users = userManager.GetUserID(UserID);
            if (users != null)
            {
                status = "Success";
                userManager.Delete(users.UserID);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UserDetails(int UserId)
        {
            UserManager userManager = new UserManager();
            Users users = new Users();
            UserModel model = new UserModel();
            users = userManager.GetUserID(UserId);
            if (users != null)
            {
                model.FirstName = users.FirstName;
                model.LastName = users.LastName;
                model.Email = users.Email;
                model.IsActive = users.IsActive;
                model.Password = users.Password;
                model.UserType = users.UserType;
                model.UserID = users.UserID;
            }
            return PartialView("~/Views/User/Partial/UserDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult Search(string filter)
        {
            UserManager userManager = new UserManager();
            List<Users> userslist = new List<Users>();
            UserModel model = new UserModel();

            userslist = userManager.Get();
            if (userslist != null)
            {
                userslist = userslist.Where(x => x.FirstName.ToLower().Contains(filter.ToLower()) || x.LastName.ToLower().Contains(filter.ToLower()) || x.Email.ToLower().Contains(filter.ToLower()) || x.UserType.ToLower().Contains(filter.ToLower())).ToList();
            }
            
            model.UserList = userslist;
            return PartialView("~/Views/User/Partial/UserGrid.cshtml", model);
        }

    }
}