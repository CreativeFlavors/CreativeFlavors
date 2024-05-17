using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration; 
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace Reports.View.Views
{
    public partial class AllStoresGRNReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string From = "";
        string To = "";
        string Supplier = "";
        string Group = "";
        string Store = "";
        string Category = "",url="", grnoption = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }


                if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null )
                {
                    Session["FromDate"] = "";
                    Session["ToDate"] = "";
                    Session["Group"] = "";
                    Session["Supplier"] = "";
                    Session["Store"] = "";
                    Session["Category"] = "";
                    Session["FromDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["ToDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["Supplier"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Supplier"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));

                    Session["GRNoption"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["GRNoption"]));

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
                    Group = Session["Group"].ToString();
                    Supplier = Session["Supplier"].ToString();
                    Store = Session["Store"].ToString();
                    Category = Session["Category"].ToString();
                grnoption = Session["GRNoption"].ToString();


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
                cmd.CommandText = "spAllStoresGRNReport";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                cmd.Parameters.AddWithValue("@From", FromDate);
                cmd.Parameters.AddWithValue("@To", ToDate);
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Store", Store);
                cmd.Parameters.AddWithValue("@Group", Group);
              
                 cmd.Parameters.AddWithValue("@grnOption", grnoption);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.AllStoresGRNRep dsRpt = new DataSet_.AllStoresGRNRep();
                da.Fill(dsRpt, "spAllStoresGRNReport");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/AllStoresGRNReport.rpt"));
                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["GrnRegisterDate"];
                Division1.Text = "From:" + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To :" + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                AllStoresGRNReportViewer.ReportSource = crystalReport;           
                AllStoresGRNReportViewer.DataBind();
                //Email.SendEmail(crystalReport , "All Stores Grn Report", "All Stores Grn Report");
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