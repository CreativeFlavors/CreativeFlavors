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
    public partial class GrnStyle : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string Style="",url="";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
    
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["Style"] != null)
                {
                    Session["Style"] = "";
                    
                    Session["Style"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Style"]));
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                       url = Session["Url"].ToString();
                }


                if (!string.IsNullOrEmpty(Session["Style"].ToString()))
                {
                  Style =  Session["Style"].ToString();
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
                cmd.CommandText = "spGRNvsStyle";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Style", Style);
                //cmd.Parameters.AddWithValue("@Style", Convert.ToInt32(Style));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                GRNvsStyle dsRpt = new GRNvsStyle();
                da.Fill(dsRpt, "spGRNvsStyle");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/GRNvsStyle.rpt"));
                //crystalReport.Load(Server.MapPath("~/ReportPage/Testgrnvsgst1.rpt"));                
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                GrnStyleReportViewer.ReportSource = crystalReport;
                GrnStyleReportViewer.DataBind();
               // Email.SendEmail(crystalReport, "Grn Vs Style Report", "Grn Vs Style Report");
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