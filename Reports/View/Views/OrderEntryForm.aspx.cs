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
    public partial class OrderEntryForm : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string LotNo = "";
        string Buyer = "";
        string OrderEntryId = "";
        string PoNo = "",url="";      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;
     
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["Buyer"] != null && Request.QueryString["LotNo"] != null && Request.QueryString["OrderEntryId"] != null && Request.QueryString["PoNo"] != null)
                {
                    Session["LotNo"] = "";
                    Session["Buyer"] = "";
                    Session["OrderEntryId"] = "";
                    Session["PoNo"] = ""; 
                    Session["LotNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["LotNo"]));
                    Session["Buyer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));
                    Session["OrderEntryId"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["OrderEntryId"]));
                    Session["PoNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["PoNo"]));                   

                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                if (!string.IsNullOrEmpty(Session["LotNo"].ToString()) && !string.IsNullOrEmpty(Session["Buyer"].ToString()) && !string.IsNullOrEmpty(Session["OrderEntryId"].ToString()) && !string.IsNullOrEmpty(Session["PoNo"].ToString()))
                {
                    LotNo = Session["LotNo"].ToString();
                    Buyer = Session["Buyer"].ToString();
                    OrderEntryId = Session["OrderEntryId"].ToString();
                    PoNo = Session["PoNo"].ToString();              
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
                cmd.CommandText = "spBuyerOrderInternal";
                cmd.CommandType = CommandType.StoredProcedure;   
                cmd.Parameters.AddWithValue("@LotNo", LotNo);
                cmd.Parameters.AddWithValue("@Buyer", Buyer);
                cmd.Parameters.AddWithValue("@OrderEntryId", OrderEntryId);
                cmd.Parameters.AddWithValue("@PoNo",  PoNo);
         
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.OrderInternalForm dsRpt = new DataSet_.OrderInternalForm();
                //try
                //{
                   
                    da.Fill(dsRpt, "spBuyerOrderInternal");
                //}
                //catch(Exception ex)
                //{

                //}
                
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/OrderEntryReport.rpt"));  
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                crystalReport.SetDatabaseLogon("sa", "Colan123");
                OrderEntryFormReportViewer.ReportSource = crystalReport;
                OrderEntryFormReportViewer.DataBind();
                //ToDo need to check local working fine but server is not working when we enable below line
                //Email.SendEmail(crystalReport, "Order Entry Form Report", "Order Entry Form Report");


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