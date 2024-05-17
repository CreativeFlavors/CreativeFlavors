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
    public partial class StockReportSizeRange : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string StoreNo = "", From = "", To = "", Group = "",Category ="" ,url="",materialType="",material="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null  && Request.QueryString["StoreNo"] != null)
                {
                    Session["Material"] = "";
                    Session["From"]      = "";
                    Session["To"]        = "";
                    Session["Group"]     = "";
                    Session["StoreNo"]   = "";
                    Session["Category"] = "";
                    Session["MaterialType"] = "";
                    Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));
                    Session["From"]      = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["To"]        = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"])); 
                    Session["Group"]     = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
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
                cmd.CommandText = "SizeRangeMaster_stock";
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
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.StockSizeRangeDataSet dsRpt = new StockSizeRangeDataSet();
                da.Fill(dsRpt, "SizeRangeMaster_stock");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[1];
                crystalReport.Load(Server.MapPath("~/ReportPage/StockReportSizeRange.rpt"));               
                crystalReport.SetDataSource(dsRpt.Tables[1]);
                Email.GetCompany(url, crystalReport);
                StockReportSizeRangeReportViewer.ReportSource = crystalReport;
                StockReportSizeRangeReportViewer.DataBind();

               // Email.SendEmail(crystalReport , "Stock Statement Report", "Stock Statement Report");
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