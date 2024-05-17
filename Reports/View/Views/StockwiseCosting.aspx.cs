using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Reports.DataSet_;
using Reports.Common;
using System.Globalization;

namespace Reports.View.Views
{
    
    public partial class StockwiseCosting : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string StoreNo = "", From = "", To = "", Group = "", Category = "", url = "", materialType = "", material = "", stockstatement = "",Season_="";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null && Request.QueryString["StoreNo"] != null)
                {
                    Session["Material"] = "";
                    Session["From"] = "";
                    Session["To"] = "";
                    Session["Group"] = "";
                    Session["StoreNo"] = "";
                    Session["Category"] = "";
                    Session["MaterialType"] = "";
                    Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));
                    Session["From"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["To"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
                    Session["Stockstatement"] = (HttpUtility.UrlDecode(Request.QueryString["Stockstatement"]));
                    Session["Season"] = (HttpUtility.UrlDecode(Request.QueryString["Season"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                //if (!string.IsNullOrEmpty(Session["From"].ToString()) && !string.IsNullOrEmpty(Session["To"].ToString()) && !string.IsNullOrEmpty(Session["StoreNo"].ToString()) )
                //{
                From = Session["From"].ToString();
                To = Session["To"].ToString();
                Group = Session["Group"].ToString();
                StoreNo = Session["StoreNo"].ToString();
                Category = Session["Category"].ToString();
                materialType = Session["MaterialType"].ToString();
                material = Session["Material"].ToString();
                stockstatement = Session["Stockstatement"].ToString();
                Season_ = Session["Season"].ToString();
                //}

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
                cmd.CommandText = "spStockwiseCosting";
                cmd.CommandType = CommandType.StoredProcedure;

                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                cmd.Parameters.AddWithValue("@From", FromDate);
                cmd.Parameters.AddWithValue("@To", ToDate);
                cmd.Parameters.AddWithValue("@StoreNo", StoreNo);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@MaterialType", materialType);
                cmd.Parameters.AddWithValue("@MaterialMaster", material);
                cmd.Parameters.AddWithValue("@Season", Season_);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                StockStatement dsRpt = new StockStatement();
                da.Fill(dsRpt, "spStockwiseCosting");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                if (stockstatement == "0")
                {
                    crystalReport.Load(Server.MapPath("~/ReportPage/Seasonwisecosting.rpt"));
                }
                else
                {
                    crystalReport.Load(Server.MapPath("~/ReportPage/StockManagementweight.rpt"));
                }
                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["StockDate"];
                TextObject MaterialType = (TextObject)crystalReport.ReportDefinition.ReportObjects["MaterialType"];
                if (materialType == "1")
                {
                    MaterialType.Text = "Material Type : Local";

                }
                if (materialType == "2")
                {
                    MaterialType.Text = "Material Type : Customer Import";
                }
                if (materialType == "3")
                {
                    MaterialType.Text = "Material Type : Direct Import";
                }
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                SeasonWiseCostingReportViewer.ReportSource = crystalReport;
                SeasonWiseCostingReportViewer.DataBind();
                //Email.SendEmail(crystalReport , "Stock Statement Report", "Stock Statement Report");
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