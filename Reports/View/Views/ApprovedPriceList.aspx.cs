using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Globalization;

using System.IO;
using CrystalDecisions.Shared;
using Reports.DataSet_;

namespace Reports.View.Views
{
    public partial class ApprovedPriceList : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string From = "";
        string To = "";
        string IndentNo = "";
        string Season = "", url = ""; string Store = ""; string Supplier = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
             
                   
                    Session["Supplier"] = "";
                    Session["Store"] = "";

                    Session["Supplier"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Supplier"]));
                    Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));

            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
             
                  

                    Supplier = Session["Supplier"].ToString();
                    Store = Session["Store"].ToString();
               
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
                cmd.CommandText = "AprovedPriceList_WithDate";
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                cmd.Parameters.AddWithValue("@Store", Store);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                SpApList dsRpt = new SpApList();
                // DataSet_.PoList dsRpt = new DataSet_.PoList();
                DataTable dt = new DataTable();
                da.Fill(dsRpt, "SpApList");
                dt = dsRpt.Tables["SpApList"];

                



                crystalReport.Load(Server.MapPath("~/ReportPage/ApList.rpt"));
                //E:\Job Work\Production Jobwork\Reports\ReportPage\ApList.rpt
                //ReportDocument crystalReport1 = new ReportDocument();E:\Job Work\Production Jobwork\Reports\ReportPage\Approvedprocelist.rpt
                //crystalReport.Load(Server.MapPath("../Models/ApList.rpt"));E:\Job Work\Production Jobwork\Reports\Models\ApList.rpt
                //crystalReport.Load("~/ReportPage/ApList.rpt");
                //TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["PoDate"];     
                //Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + "  " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                crystalReport.SetDataSource(dsRpt.Tables[0]);
                Email.GetCompany(url, crystalReport);
                ApListReportViewer.ReportSource = crystalReport;
                ApListReportViewer.DataBind();
                Email.SendEmail(crystalReport, "Po List Report", "Po List Report");
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