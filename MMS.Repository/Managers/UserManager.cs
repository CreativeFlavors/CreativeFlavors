using MMS.Common;
using MMS.Core.Entities;
using MMS.Data;
using MMS.Data.Context;
using MMS.Repository.Service;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MMS.Repository.Managers
{
    public class UserManager : IUserService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Users> userRepository;

        public UserManager()
        {
            userRepository = unitOfWork.Repository<Users>();
        }


        public bool Post(Users arg)
        {
            bool result = false;
            try
            {
                string username = HttpContext.Current.Session["UserName"].ToString();
                arg.CreatedBy = username;
                arg.UpdatedBy = username;
                userRepository.Insert(arg);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;

        }
        public string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        public string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public bool IsValidateUser(string UserName, string Password)
        {
            bool result = false;
            try
            {
                List<Users> user = new List<Users>();
                string PasswordSalt = Encryptdata(Password);
                string Password_ = Decryptdata(PasswordSalt);

                user = userRepository.Table.Where(P => P.Email.ToLower() == UserName.ToLower() && P.Password == Password_).ToList<Users>();              
                if (user.Count > 0)
                {
                    HttpContext.Current.Session["UserEmail"] = user.FirstOrDefault().Email;
                    HttpContext.Current.Session["UserName"] = (user.FirstOrDefault().FirstName + " " + user.FirstOrDefault().LastName).ToString();
                    HttpContext.Current.Session["UserType"] = user[0].UserType;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }
            return result;
        }
        public Users GetUserName(string Email)
        {
            Users user = new Users();
            user = userRepository.Table.Where(x => x.Email == Email).SingleOrDefault();
            return user;
        }
        public Users GetUserID(int ID)
        {
            Users user = new Users();
            user = userRepository.Table.Where(x => x.UserID == ID).SingleOrDefault();
            return user;
        }
        public bool Put(Users arg)
        {
            bool result = false;
            try
            {

                Users model = userRepository.Table.Where(p => p.Email == arg.Email).FirstOrDefault();
                if (model != null)
                {
                    if (HttpContext.Current.Session["UserName"] != null)
                    {
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                    }
                    if (arg.Password == "" || arg.Password == null)
                    {
                        model.Password = Cryptography.CreateRandomPassword(6);
                    }
                    else
                    {
                        model.Password = arg.Password;
                    }

                    userRepository.Update(arg);
                    result = true;
                    if (arg.Password == "" || arg.Password == null)
                    {
                        string urername = model.FirstName + model.LastName;
                        string host = HttpContext.Current.Request.Url.Host;
                        EmailHelper.SendResetPasswordEmailMsg("test@gmail.com", "New Password from Enco Shoes", model.Password, urername, host);
                    }
                    //EmailHelper.SendMessageWithAttachment("", "Karthikpalanisamy90@gmail.com","New Password from Enco Shoes", "Password : " + model.Password + "","","","");

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }




        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                Users model = userRepository.GetById(id);
                userRepository.Delete(model);
                result = true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
                result = false;
            }

            return result;
        }

        public Users Get(int id)
        {
            return null;
        }

        public List<Users> Get()
        {
            List<Users> user = new List<Users>();

            try
            {
                user = userRepository.Table.ToList<Users>();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return user;
        }

        public void Dispose()
        {
            using (MMSContext context = new MMSContext())
            {
                context.Dispose();
            }
        }

        public bool ChangetheForpasswordPut(Users arg)
        {
            bool result = false;
            try
            {
                Users model = userRepository.Table.Where(p => p.Email == arg.Email).FirstOrDefault();
                if (model != null)
                {
                    if (HttpContext.Current.Session["UserName"] != null)
                    {
                        string username = HttpContext.Current.Session["UserName"].ToString();
                        model.UpdatedBy = username;
                    }
                    if (arg.Password == "" || arg.Password == null)
                    {
                        //model.Password = Cryptography.CreateRandomPassword(6);
                        model.Password = arg.Password;
                    }
                    else
                    {
                        model.Password = arg.Password;
                    }

                    userRepository.Update(arg);
                    result = true;
                    //if (arg.Password == "" || arg.Password == null)
                    //{
                    //    string urername = model.FirstName + model.LastName;
                    //    string host = HttpContext.Current.Request.Url.Host;
                    //    EmailHelper.SendResetPasswordEmailMsg("test@gmail.com", "New Password from Enco Shoes", model.Password, urername, host);
                    //}
                    //EmailHelper.SendMessageWithAttachment("", "Karthikpalanisamy90@gmail.com","New Password from Enco Shoes", "Password : " + model.Password + "","","","");

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return result;
        }

    }
}
