using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Globalization;
using CrystalDecisions.Shared;
using Reports.DataSet_;
namespace Reports.View.Views
{
    public partial class Orderentry : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string OrderType = "";
        string BuyerName = "";
        string Season = "";
        string Lot = "";
        string Order = "";
       
        string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["OrderType"] != null && Request.QueryString["BuyerName"] != null && Request.QueryString["Season"] != null && Request.QueryString["Order"] != null)
                {
                    Session["OrderType"] = "";
                    Session["BuyerName"] = "";
                    Session["Season"] = "";
                    Session["Lot"] = "";
                    Session["Order"] = "";
                    Session["OrderType"] = Request.QueryString["OrderType"];
                    Session["BuyerName"] = Request.QueryString["BuyerName"];
                    Session["Season"] = Request.QueryString["Season"];
                    Session["Lot"] =Request.QueryString["Lot"];
                    Session["Order"] = Request.QueryString["Order"];

                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                //if (!string.IsNullOrEmpty(Session["OrderType"].ToString()) && !string.IsNullOrEmpty(Session["BuyerName"].ToString()) && !string.IsNullOrEmpty(Session["Season"].ToString()) && !string.IsNullOrEmpty(Session["Lot"].ToString()) && !string.IsNullOrEmpty(Session["Order"].ToString()))
                //{
                    OrderType = Session["OrderType"].ToString();
                    BuyerName = Session["BuyerName"].ToString();
                    Season = Session["Season"].ToString();
                    Lot = Session["Lot"].ToString();
                    Order = Session["Order"].ToString();
               // }

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
             
                cmd.CommandText = "spOrderEntryReport";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderType", OrderType);
                cmd.Parameters.AddWithValue("@BuyerName", BuyerName);
                cmd.Parameters.AddWithValue("@Season", Season);
                cmd.Parameters.AddWithValue("@Lot", Lot);
                cmd.Parameters.AddWithValue("@Order", Order);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.Orderentry dsRpt = new DataSet_.Orderentry();

                da.Fill(dsRpt, "spOrderEntryReport");
                DataTable dt = new DataTable();

                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/Orderentry.rpt"));
              
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                //crystalReport.SetDatabaseLogon("sa", "Colan123");
                OrderentryReportViewer.ReportSource = crystalReport;
                OrderentryReportViewer.DataBind();
               //Email.SendEmail(crystalReport, "Order Entry Report", "Order Entry Report");
            }
            catch (Exception Ex)
            {
            }
        }
        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    crystalReport.Close();
        //    crystalReport.Dispose();
        //}
    }
}
