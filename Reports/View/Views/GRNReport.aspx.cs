using System; 
using System.Web; 
using Reports.Common;
using System.Data.SqlClient; 
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
using Reports.Common;
namespace Reports.View.Views
{
    public partial class GRNReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string GrnNo="", Group ="",Category="",Store ="",Supplier ="",url="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["Grn"] != null && !string.IsNullOrEmpty(Request.QueryString["Group"]) && !string.IsNullOrEmpty(Request.QueryString["Category"]) && !string.IsNullOrEmpty(Request.QueryString["Store"])&& !string.IsNullOrEmpty(Request.QueryString["Supplier"]))
                {
                    Session["Group"] = "";
                    Session["Store"] = "";                
                    Session["Category"] = "";
                    Session["Grn"] = "";
                    Session["Supplier"] = "";
                    Session["Grn"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Grn"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Supplier"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Supplier"]));

                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                if (!string.IsNullOrEmpty(Session["Grn"].ToString()) || !string.IsNullOrEmpty(Session["Supplier"].ToString()) ||   !string.IsNullOrEmpty(Session["Category"].ToString()) || !string.IsNullOrEmpty(Session["Store"].ToString()))
                    {
                    GrnNo =    Session["Grn"].ToString();
                    Group = Session["Group"].ToString();
                    Store    = Session["Store"].ToString();
                    Category = Session["Category"].ToString(); 
                    Supplier = Session["Supplier"].ToString();
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
                cmd.CommandText = "spGoodRecieptNote";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GRNNO", Convert.ToInt32(GrnNo));
                cmd.Parameters.AddWithValue("@Store", Store);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                cmd.Parameters.AddWithValue("@Group", Group);
               
        
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                GoodReceiptNote dsRpt = new GoodReceiptNote();
                da.Fill(dsRpt, "spGoodRecieptNote");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/GRNReport.rpt"));
                         
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                GRNReportViewer.ReportSource = crystalReport;
                GRNReportViewer.DataBind();
               // Email.SendEmail(crystalReport  ,"GRN Report","GRN Report");  
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