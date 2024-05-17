using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Globalization;

namespace Reports.View.Views
{
    public partial class GateEntryVehicle : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string GateEntryNo = "", VehicleId = "",Purpose= "",DriverName= "",From= "",To="", url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                if (Request.QueryString["FromDate"] != null || Request.QueryString["ToDate"] != null || Request.QueryString["GateEntryNo"] != null || Request.QueryString["VehicleId"] != null || Request.QueryString["Purpose"] != null || Request.QueryString["DriverName"] != null)
                {
                    Session["FromDate"] = "";
                    Session["ToDate"] = "";
                    Session["GateEntryNo"] = "";
                    Session["VehicleId"] = "";
                    Session["Purpose"] = "";
                    Session["DriverName"] = "";
                   

                    Session["FromDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["ToDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["GateEntryNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["GateEntryNo"]));
                    Session["VehicleId"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["VehicleId"]));
                    Session["Purpose"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Purpose"]));
                    Session["DriverName"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["DriverName"]));
                }
              
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                From = Session["FromDate"].ToString();
                To = Session["ToDate"].ToString();
                GateEntryNo = Session["GateEntryNo"].ToString();
                VehicleId = Session["VehicleId"].ToString();
                Purpose = Session["Purpose"].ToString();
                DriverName = Session["DriverName"].ToString();
             
                if (crystalReport == null)
                    crystalReport.Close();
                crystalReport = new ReportDocument();
                GC.Collect();

                LoadReport();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void LoadReport()
        {

            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "spGateEntryVehicle";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);

                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@GateEntryNo", GateEntryNo);
                cmd.Parameters.AddWithValue("@VehicleId", VehicleId);
                cmd.Parameters.AddWithValue("@Purpose", Purpose);
                cmd.Parameters.AddWithValue("@DriverName", DriverName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.GateEntryVehicle rpt = new Reports.DataSet_.GateEntryVehicle();
                da.Fill(rpt, "spGateEntryVehicle");
                DataTable dt = new DataTable();
                dt = rpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/GateEntryVehicleReport.rpt"));

                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["EntryDateTime"];
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                crystalReport.SetDataSource(rpt.Tables[0]);
                Email.GetCompany(url, crystalReport);
                GateEntryVehicleReportViewer.ReportSource = crystalReport;
                GateEntryVehicleReportViewer.DataBind();
               // Email.SendEmail(crystalReport, "GateEntry Vehicle Report", "GateEntry Vehicle Report");
            }
            catch (Exception Ex)
            {

            }

        }

       
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
    }
}