using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMS.Web.Models.AccountModel;
using System.Web.Security;
using MMS.Core.Entities;
using MMS.Repository.Managers;
using MMS.Web.Models.ForgetPassword;
using MMS.Web.Models.UserModel;
using MMS.Web.Models.Permission;
using MMS.Common;
using MMS.Web.ExtensionMethod;

namespace MMS.Web.Controllers
{
    [CustomFilter]
    //  [SessionExpire]
    public class AccountsController : Controller
    {
        #region Accounts View

        [HttpGet]
        public ActionResult Registration()
        {
            MMS.Web.Models.AccountModel.AccountModel vm = new Models.AccountModel.AccountModel();
            return View(vm);
        }

       public ActionResult DashBoard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.AccountModel.AccountModel user)
        {
            bool result = false;
            string SuccessMsg = "";
            if (ModelState.IsValid)
            {
                Users users = new Users();
                UserManager userManager = new UserManager();
                if(user.Forgotpassword== "Forgotpassword")
                {
                    users.Email = user.Email;
                    users.Password = user.Password;
                    result = userManager.ChangetheForpasswordPut(users);
                    SuccessMsg = "Password Updated Successfully !";
                }
                //if (user.Password == "resetpwd")
                //{
                //    users.Email = user.Email;
                //    result = userManager.Put(users);
                //    SuccessMsg = "Password Resetted Successfully & New Password sent to your EmailId !";
                //}
                else
                {
                    result = userManager.IsValidateUser(user.Email, user.Password);
                    SuccessMsg = "Entered Username and Password is Invalid !";
                }
            }
            if (result == true)
            {
                if (user.Password == "resetpwd")
                {
                    TempData["msg"] = "<script>alert('" + SuccessMsg + "')</script>";
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('" + SuccessMsg + "')</script>";
                return View();
            }
        }

        public ActionResult LoginValidation(Models.AccountModel.AccountModel user)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users();
                UserManager userManager = new UserManager();
                bool result = userManager.IsValidateUser(user.Email, user.Password);
                if (result == true)
                {
                    return Json(user, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    user.EmailExist = true;
                    return Json(user, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }



        #endregion

        #region Curd Operation
        [HttpPost]
        public ActionResult Registration(Models.AccountModel.AccountModel user)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users();
                UserManager userManager = new UserManager();
                users = userManager.GetUserName(user.Email);
                if (users == null)
                {
                    string PasswordSalt = userManager.Encryptdata(user.Password);
                    Users user_ = new Users();
                    Session["Email"] = user.Email;
                    Session["UserName"] = user.FirstName + "" + user.LastName;
                    Session["UserType"] = user.UserType;
                    user_.Email = user.Email;
                    user_.Password = user.Password;
                    user_.FirstName = user.FirstName;
                    user_.LastName = user.LastName;
                    user_.UserType = user.UserType;
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
                    user.EmailExist = true;
                    return Json(user, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UserUpdate(AccountModel accountModel)
        {
            Users users = new Users();
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                Users users_ = new Users();
                users_ = userManager.GetUserID(accountModel.UserID);
                if (users_ != null)
                {
                    
                    users_.FirstName = accountModel.FirstName;
                    users_.LastName = accountModel.LastName;
                    users_.Email = accountModel.Email;
                    users_.UserType = accountModel.UserType;
                    users_.UpdatedDate = DateTime.Now;
                    users_.CreatedDate = users_.CreatedDate;
                    users_.CreatedBy = Session["UserName"] as string;
                    users_.UpdatedBy = Session["UserName"] as string;
                    userManager.Put(users_);
                    accountModel.EmailExist = true;
                    return Json(accountModel, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(accountModel, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(accountModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUser(int UserID)
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
        #endregion

        #region Home Page
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Forgot Password

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ForgotPasswordModel vm = new ForgotPasswordModel();
            vm.IsExist = null;
            return View(vm);
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                UserManager userManager = new UserManager();
                Users users = new Users();
                users = userManager.GetUserName(model.Email.ToLower());
                if (users != null)
                {
                    model.IsExist = true;
                }
                else
                {
                    model.IsExist = false;
                }
            }
            return View(model);
        }
        #endregion

        #region User grid

        public ActionResult UserGrid()
        {
            UserManager userManager = new UserManager();
            List<Users> users = userManager.Get().ToList();
            return View(users);
        }
        [HttpPost]
        public ActionResult AddViewUserPopup_(int ID)
        {
            UserManager userManager = new UserManager();
            Users users = new Users();
            AccountModel accountModel = new AccountModel();
            users = userManager.GetUserID(ID);
            return PartialView("AddViewUserPopup", users);
        }
        public ActionResult UserAdd()
        {
            UserManager userManager = new UserManager();
            Users users = new Users();
            AccountModel accountModel = new AccountModel();
            return PartialView("AddViewUserPopup");
        }
        #endregion

        #region UserType

        public ActionResult UserType()
        {
            UserTypeManager UserTypeManager = new UserTypeManager();
            UserTypeModel model = new UserTypeModel();
            model.UserTypeModelList = UserTypeManager.Get().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult UserType(FormCollection data)
        {
            bool Result = false;

            UserTypeManager UserTypeManager = new UserTypeManager();
            UserTypeMaster Model = new UserTypeMaster();
            Model.UserTypeID = Convert.ToInt32(data["UserTypeID"]);
            Model.UserType = data["UserType"];
            Model.UserTypeDesc = data["UserTypeDesc"];
            Result = UserTypeManager.Post(Model);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteUser(FormCollection data)
        {
            bool Result = false;
            UserTypeManager UserTypeManager = new UserTypeManager();
            Result = UserTypeManager.Delete(Convert.ToInt32(data["UserTypeID"]));
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Permission

        public ActionResult Permission()
        {
            PermissionManager PermissionManager = new PermissionManager();
            PermissionModel model = new PermissionModel();
            model.PermissionModelList = PermissionManager.Get().ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Permission(FormCollection data)
        {
            bool Result = false;

            PermissionManager PermissionManager = new PermissionManager();
            tbl_Permission Model = new tbl_Permission();
            Model.PermissionID = Convert.ToInt32(data["PermissionID"]);
            Model.PermissionName = data["PermissionName"];
            Model.PermissionDesc = data["PermissionDesc"];
            Result = PermissionManager.Post(Model);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PermissionSetting

        public ActionResult PermissionSetting()
        {
            PermissionManager PermissionManager = new PermissionManager();
            PermissionModel model = new PermissionModel();
            model.PermissionModelList = PermissionManager.Get().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult PermissionSetting(string PermissionIDs, int UserTypeId)
        {
            bool Result = false;
            PermissionSettingManager PermissionSetManager = new PermissionSettingManager();
            tbl_PermissionSetting Model = new tbl_PermissionSetting();
            Model.PermissionID = PermissionIDs;
            Model.UserTypeID = UserTypeId;
            Result = PermissionSetManager.Post(Model);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPermissionSettingIDs(int UserTypeId)
        {
            PermissionSettingManager PermissionSetManager = new PermissionSettingManager();
            tbl_PermissionSetting Model = PermissionSetManager.GetByID(UserTypeId);
            if (Model != null)
            {
                return Json(Model.PermissionID, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        #endregion


        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Accounts");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(AccountModel Model)
        {
            bool Result = false;
            UserManager userManager = new UserManager();
            Users users = new Users();
            users.Email = Session["UserEmail"].ToString();
            users.Password = Model.Password;
            Result = userManager.Put(users);
            if (Result == true)
            {
                TempData["msg"] = "<script>alert('PassWord Changed Successfully !')</script>";

            }
            else
            {
                TempData["msg"] = "<script>alert('PassWord Not Changed !')</script>";
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult Forgotpasswordnewformate(string email,string password)
        //{
        //    bool result = false;
           
        //    if (ModelState.IsValid)
        //    {
        //        UserManager userManager = new UserManager();
        //         result =userManager.ChangetheForpasswordPut(email, password);
                
        //    }

        //    return Json(result,JsonRequestBehavior.AllowGet);
        //}
    }
}
