using MMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMS.Common
{
    public static class EmailHelper
    {

        public static bool SendResetPasswordEmailMsg(string EmailId, string Subject, string NewPassword,string urername,string host)
        {
           
            string SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            string SMTPHmailTo = ConfigurationManager.AppSettings["SMTP_To"]; 
            string SMTPPort = ConfigurationManager.AppSettings["SMTPPort"];
            string smtpUserName = ConfigurationManager.AppSettings["SMTPUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(smtpUserName);    // Sender e-mail address. 
            string[] toAddress = SMTPHmailTo.Split(';');
            foreach (var item in toAddress)
            {
                Msg.To.Add(item);
            }
            // Msg.To.Add("karthikpalanisamy790@gmail.com");         // Recipient e-mail address.
            Msg.Subject = Subject;
            Msg.Body = "Dear "+urername+ " SignIn in " + host + "  using this new password and change the password by clicking Change Password tab in Permission" + " : " + NewPassword;
            Msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = Convert.ToInt32(SMTPPort);
            smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            return true;
        }
        public static bool SendEmail(EmailTempate emailTemplate)
        {
            string SMTPHost = ConfigurationManager.AppSettings["SMTPHost"];
            string SMTPPort = ConfigurationManager.AppSettings["SMTPPort"];
            string smtpUserName = ConfigurationManager.AppSettings["SMTPUserName"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            MailMessage Msg = new MailMessage();
            Msg.From = new MailAddress(smtpUserName);    
            Msg.To.Add(emailTemplate.ToAddress);                    
            Msg.Subject = emailTemplate.Subject;
             
            Msg.Body = emailTemplate.Body;
            Msg.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
         //   smtp.EnableSsl = true;
            smtp.Port = Convert.ToInt32(SMTPPort);
            smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            return true;
        }

        public static bool SendMessageWithAttachment(string fromAddress, string toAddresses, string subjectText, string bodyText, string smtpServerAddress, string smtpUserName, string smtpPassword)
        {

            smtpServerAddress = ConfigurationManager.AppSettings["SMTPPort"];
            smtpUserName = ConfigurationManager.AppSettings["SMTPUserName"];
            smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];

            bool result = false;
            Attachment ATTACHEMENT_DATA = null;
            try
            {
                MailMessage MAIL_MESSAGE = new MailMessage();
                MAIL_MESSAGE.IsBodyHtml = true;
                MAIL_MESSAGE.From = new MailAddress(fromAddress);
                MAIL_MESSAGE.To.Clear();
                if (toAddresses != null) //&& toAddresses.Count > 0
                {
                    MAIL_MESSAGE.To.Add(new MailAddress(toAddresses));

                }
                MAIL_MESSAGE.CC.Clear();
                MAIL_MESSAGE.Subject = subjectText;
                MAIL_MESSAGE.Body = bodyText;
                int SMTPPORT = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpClient SMTP_CLIENT = new SmtpClient(smtpServerAddress, SMTPPORT)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpUserName, smtpPassword),
                    EnableSsl = true
                };

                SMTP_CLIENT.Send(MAIL_MESSAGE);
                result = true;
            }
            catch (SmtpException ex)
            {
                result = false;
            }
            finally
            {
                if (ATTACHEMENT_DATA != null)
                {
                    ATTACHEMENT_DATA.Dispose();
                }
            }
            return result;
        }
        #region Email
        public static bool RateComparisonEmail(string body,string Subject)
        {
            bool result = false;
            try
            {
                string SMTPPort = ConfigurationManager.AppSettings["SMTP_Port"];
                string SMTPHost = ConfigurationManager.AppSettings["SMTP_ServerAddress"];
                string smtpUserName = ConfigurationManager.AppSettings["SMTP_UserName"];
                string SMTPCc = ConfigurationManager.AppSettings["SMTP_Cc"];
                string SMTPTo = ConfigurationManager.AppSettings["SMTP_To"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTP_Password"];
                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress(smtpUserName);
                string[] toAddress = SMTPTo.Split(';');
                foreach (var item in toAddress)
                {
                    Msg.To.Add(item);
                }

                Msg.CC.Add(SMTPCc);
                Msg.Subject = Subject;
                Msg.Body = body;
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTPHost;
                smtp.Port = Convert.ToInt32(SMTPPort);
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                smtp.EnableSsl = true;
                smtp.Send(Msg);
                return result = true;
            }
            catch (Exception ex)
            {
                // Library.WriteErrorLog(DateTime.Now.ToString() + ex.Message.ToString());
                return result = false;
            }

        }
        #endregion
    }
}
