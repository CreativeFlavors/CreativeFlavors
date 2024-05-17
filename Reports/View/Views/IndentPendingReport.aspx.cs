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
    public partial class IndentPendingReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string From = "";
        string To = "";
        string IndentNo = "";
        string supplierID = "";
        string Season = "", url = "";
        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["From"] != null && Request.QueryString["To"] != null)
                {

                    Session["From"] = "";
                    Session["To"] = "";
                    Session["IndentNo"] = "";
                    Session["Season"] = "";
                    Session["Supplierid"] = "";
                    Session["Supplierid"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["SupplierID"]));
                    Session["From"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["From"]));
                    Session["To"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["To"]));
                    Session["IndentNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IndentNo"]));
                    Session["Season"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Season"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                if (!string.IsNullOrEmpty(Session["From"].ToString()) && !string.IsNullOrEmpty(Session["To"].ToString()))
                {
                    From = Session["From"].ToString();
                    To = Session["To"].ToString();
                    IndentNo = Session["IndentNo"].ToString();
                    Season = Session["Season"].ToString();
                    supplierID = Session["Supplierid"].ToString();
                }
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
        #endregion

        #region LoadReport
        public void LoadReport()
        {
            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "IndentPending";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                cmd.Parameters.AddWithValue("@From", FromDate);
                cmd.Parameters.AddWithValue("@To", ToDate);
                cmd.Parameters.AddWithValue("@IndentNo", IndentNo);
                cmd.Parameters.AddWithValue("@Season", Season);
                cmd.Parameters.AddWithValue("@Supplier", supplierID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.IndentPendingSet dsRpt = new DataSet_.IndentPendingSet();
                da.Fill(dsRpt, "IndentPending");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/IndentPending.rpt"));
                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["PoDate"];

                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + "  " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                IndentPendingReportViewer.ReportSource = crystalReport;
                IndentPendingReportViewer.DataBind();
                // Email.SendEmail(crystalReport, "PO Pending Report", "PO Pending Report");

            }
            catch (Exception Ex)
            {

            }

        }
        #endregion

        #region PageUnload
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
        #endregion
    }
}