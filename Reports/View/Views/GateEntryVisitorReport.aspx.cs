using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
using System.Globalization;
using System.Web.UI;

namespace Reports.View.Views
{
    public partial class GateEntryVisitorReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string GateEntryNo = "", VisitorName = "", visitorType = "", From = "", To = "", url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                if (Request.QueryString["FromDate"] != null || Request.QueryString["ToDate"] != null || Request.QueryString["GateEntryNo"] != null || Request.QueryString["VisitorName"] != null || Request.QueryString["VisitorType"] != null)
                {
                    Session["FromDate"] = "";
                    Session["ToDate"] = "";
                    Session["GateEntryNo"] = "";
                    Session["VisitorName"] = "";
                    Session["VisitorType"] = "";

                    Session["FromDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["ToDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["GateEntryNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["GateEntryNo"]));
                    Session["VisitorName"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["VisitorName"]));
                    Session["VisitorType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["VisitorType"]));

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
                VisitorName = Session["VisitorName"].ToString();
                visitorType = Session["VisitorType"].ToString();


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
                cmd.CommandText = "spGateEntryVisitor";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);

                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@GateEntryNo", GateEntryNo);
                cmd.Parameters.AddWithValue("@VisitorName", VisitorName);
                cmd.Parameters.AddWithValue("@VisitorType", visitorType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.GateEntryVisitor rpt = new Reports.DataSet_.GateEntryVisitor();
                da.Fill(rpt, "spGateEntryVisitor");
                DataTable dt = new DataTable();
                dt = rpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/GateEntryVisitorReport.rpt"));

                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["VisitorDate"];
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                crystalReport.SetDataSource(rpt.Tables[0]);
                Email.GetCompany(url, crystalReport);
                GateEntryVisitorReportViewer.ReportSource = crystalReport;
                GateEntryVisitorReportViewer.DataBind();
               // Email.SendEmail(crystalReport, "Visitor Report", "Visitor Report");
            }
            catch (Exception Ex)
            {

            }

        }

        //protected void ExportPDF(object sender, ImageClickEventArgs e)
        //{
        //    string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
        //    SqlConnection Con = new SqlConnection(ConStr);
        //    SqlCommand cmd = new SqlCommand("Usp_getPersonRecords", Con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = cmd;
        //    DataTable datatable = new DataTable();
        //    da.Fill(datatable); // getting value according to imageID and fill dataset

        //    ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
        //    crystalReport.Load(Server.MapPath("~/CrystalPersonInfo.rpt")); // path of report 
        //    crystalReport.SetDataSource(datatable); // binding datatable
        //    GateEntryVisitorReportViewer.ReportSource = crystalReport;

        //    crystalReport.ExportToHttpResponse
        //    (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");
        //    //here i have use [ CrystalDecisions.Shared.ExportFormatType.PortableDocFormat ] to Export in PDF

        //}
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
    }
}