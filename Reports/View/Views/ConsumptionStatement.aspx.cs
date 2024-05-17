using System;
using System.Web;

using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Reports.DataSet_;
using Reports.Common;
using System.Globalization;

namespace Reports.View.Views
{
    public partial class ConsumptionStatement : System.Web.UI.Page
    {
        #region Global varaiables
        ReportDocument crystalReport = new ReportDocument();
        string StoreNo = "", From = "", To = "", Group = "", Category = "", url = "", Buyer = "", IoNo = "", Conveyor = "", MaterialType = "", Season = "", Style = "", LotNo = "";
        #endregion

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
                if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null && Request.QueryString["Group"] != null && Request.QueryString["StoreNo"] != null && Request.QueryString["Category"] != null)
                {
                    Session["FromDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["ToDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Buyer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));
                    Session["IoNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IoNo"]));
                    Session["Conveyor"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Conveyor"]));
                    Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));
                    Session["Season"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Season"]));
                    Session["Style"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Style"]));
                    Session["LotNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["LotNo"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                if (!string.IsNullOrEmpty(Session["FromDate"].ToString()) && !string.IsNullOrEmpty(Session["ToDate"].ToString()) && !string.IsNullOrEmpty(Session["StoreNo"].ToString()) && !string.IsNullOrEmpty(Session["Buyer"].ToString()))
                {
                    From = Session["FromDate"].ToString();
                    To = Session["ToDate"].ToString();
                    Group = Session["Group"].ToString();
                    StoreNo = Session["StoreNo"].ToString();
                    Category = Session["Category"].ToString();
                    Buyer = Session["Buyer"].ToString();
                    IoNo = Session["IoNo"].ToString();
                    Conveyor = Session["Conveyor"].ToString();
                    MaterialType = Session["MaterialType"].ToString();
                    Season = Session["Season"].ToString();
                    Style = Session["Style"].ToString();
                    LotNo = Session["LotNo"].ToString();
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
                cmd.CommandText = "spLeatherConsumption";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                cmd.Parameters.AddWithValue("@From", FromDate);
                cmd.Parameters.AddWithValue("@To", ToDate);
                cmd.Parameters.AddWithValue("@StoreNo", StoreNo);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Category", Category);

                cmd.Parameters.AddWithValue("@BuyerName", Buyer);
                cmd.Parameters.AddWithValue("@InterOrderNo", IoNo);
                cmd.Parameters.AddWithValue("@Style", Style);
                cmd.Parameters.AddWithValue("@Season", Season);
                cmd.Parameters.AddWithValue("@ConveyorId", Conveyor);

                cmd.Parameters.AddWithValue("@LotNo", LotNo);
                cmd.Parameters.AddWithValue("@MaterialType", MaterialType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                StockStatement dsRpt = new StockStatement();
                da.Fill(dsRpt, "spLeatherConsumption");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/ConsumptionStatement.rpt"));
                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["StockDates"];
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                ConsumptionStatementReportViewer.ReportSource = crystalReport;
                ConsumptionStatementReportViewer.DataBind();
                // Email.SendEmail(crystalReport, "Consumption Statement Report", "Consumption Statement Report");
            }
            catch (Exception Ex)
            {
                //  ErrorLog error = new ErrorLog();
                // error.LogError(Ex);
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