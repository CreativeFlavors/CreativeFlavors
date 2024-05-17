using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Reports
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        public void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
               Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
           Log(ex.Message.ToString(), this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public static void Log(string Message, string ClassName, string FunctionName)
        {
            string ERROR_MESSAGE = string.Empty;

            ERROR_MESSAGE = System.Environment.NewLine;
            ERROR_MESSAGE += "[" + DateTime.Now.ToString() + "] : " + Message + ".    [" + ClassName + "]     [" + FunctionName + "]";



            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";

                logFilePath += DateTime.Now.ToString("MM-dd-yyyy") + "_Logs" + "." + "txt";

                if (logFilePath.Equals(""))
                {
                    return;
                }
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists)
                {
                    logDirInfo.Create();
                }
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(ERROR_MESSAGE);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
    }
}