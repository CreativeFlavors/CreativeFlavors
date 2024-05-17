using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
namespace Reports.View.Views
{
    public partial class CuttingSlip : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();        
        string Buyer = "";
        string Group = "";
        string Category = "";
        string Season = "";
        string LotNo = "";
        string url = "";
        string IoNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["Season"])  && !string.IsNullOrEmpty(Request.QueryString["LotNo"])  && !string.IsNullOrEmpty(Request.QueryString["Buyer"])   && !string.IsNullOrEmpty(Request.QueryString["Group"]) && !string.IsNullOrEmpty(Request.QueryString["Category"]))
                {
                    Session["Season"] = "";
                    Session["Buyer"]   = "";
                    Session["Group"]   = "";
                    Session["LotNo"]   = "";
                    Session["Category"] = "";
                    Session["IoNo"] = "";
                    Session["IoNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IoNo"]));
                    Session["Season"]  =  Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Season"]));
                    Session["LotNo"]     = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["LotNo"]));
                    Session["Buyer"]    = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));
                    Session["Group"]     = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["Category"]  = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));                  
                }                
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                if (!string.IsNullOrEmpty(Session["Season"].ToString()) && !string.IsNullOrEmpty(Session["LotNo"].ToString()) && !string.IsNullOrEmpty(Session["Buyer"].ToString()) && !string.IsNullOrEmpty(Session["Group"].ToString()) && !string.IsNullOrEmpty(Session["Category"].ToString()))
                {
                    Season = Session["Season"].ToString();
                    Buyer = Session["Buyer"].ToString();
                    Group = Session["Group"].ToString();
                    LotNo = Session["LotNo"].ToString();
                    Category = Session["Category"].ToString();
                    IoNo = Session["IoNo"].ToString();
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
                cmd.CommandText = "spCuttingSlip";
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@LotNo",(LotNo));
                cmd.Parameters.AddWithValue("@BuyerName", Convert.ToInt32(Buyer));
                cmd.Parameters.AddWithValue("@BuyerSeason", Convert.ToInt32(Season));
                cmd.Parameters.AddWithValue("@MaterialCategoryMasterId", Convert.ToInt32(Category));
                cmd.Parameters.AddWithValue("@MaterialGroupMasterId", (Group));
                cmd.Parameters.AddWithValue("@IoNo", IoNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd); 
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.CuttingSlip dsRpt  = new DataSet_.CuttingSlip(); 
                da.Fill(dsRpt, "spCuttingSlip");
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                dt = dsRpt.Tables[0];
                //dt1 = dsRpt.Tables[1];
               // string GroupName = dt.Rows[0]["GroupName"].ToString();
                crystalReport.Load(Server.MapPath("~/ReportPage/CuttingSlipReport.rpt"));
                //TextObject GroupName_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["GroupName"];
                //GroupName_.Text = "Group Name     " + GroupName.ToString();
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
              
                CuttingSlipReportViewer.ReportSource = crystalReport;
                CuttingSlipReportViewer.DataBind();
               // Email.SendEmail(crystalReport, "Cutting Slip Report", "Cutting Slip Report");
            }
            catch (Exception Ex)
            {
                Response.Write(Ex.Message);
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
    }
}