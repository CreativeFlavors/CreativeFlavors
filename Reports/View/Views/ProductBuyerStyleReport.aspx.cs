using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
using System.Globalization;

namespace Reports.View.Views
{
    public partial class ProductBuyerStyleReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string BuyerName = "";
        string BuyerModel = "";
        string SeasonName = "",url="";
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["Buyer"] != null && Request.QueryString["BuyerModel"] != null && Request.QueryString["Season"] != null)
                {
                    Session["Season"] = "";
                    Session["Buyer"] = "";
                    Session["BuyerModel"] = "";                    
                    Session["Buyer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));
                    Session["BuyerModel"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["BuyerModel"]));
                    Session["Season"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Season"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }


                if (!string.IsNullOrEmpty(Session["Buyer"].ToString()) && !string.IsNullOrEmpty(Session["BuyerModel"].ToString()) && !string.IsNullOrEmpty(Session["Season"].ToString()))
                {
                    SeasonName = Session["Season"].ToString();
                    BuyerName = Session["Buyer"].ToString();
                    BuyerModel = Session["BuyerModel"].ToString();
                    
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
        public void LoadReport()
        {
            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "spProductBuyerStyleRepot";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BuyerName ", BuyerName);
                cmd.Parameters.AddWithValue("@BuyerModel", BuyerModel);
                cmd.Parameters.AddWithValue("@Season", SeasonName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.ProductBuyerStyleReport dsRpt = new DataSet_.ProductBuyerStyleReport();
                da.Fill(dsRpt, "spProductBuyerStyleRepot");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/ProductBuyerStyle.rpt"));                
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                ProductBuyerStyleReportViewer.ReportSource = crystalReport;
                ProductBuyerStyleReportViewer.DataBind();
               // Email.SendEmail(crystalReport, "Purchaser Order Report", "Purchaser Order Report");

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