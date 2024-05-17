using CrystalDecisions.CrystalReports.Engine;
using Reports.Common;
using Reports.DataSet_;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reports.View.Views
{
    public partial class Costing : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string Buyer = "";
        string BomNo = "";
        string Group = "";
        string Store = "";
        string Category = "", url = "", lotNo = "", material = "", season = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                Session["Buyer"] = "";
                Session["BomNo"] = "";
                Session["Group"] = "";
                Session["Store"] = "";
                Session["LotNo"] = "";
                Session["Material"] = "";
                Session["Season"] = "";
                Session["Category"] = "";
                Session["Buyer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));
                Session["BomNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Bom"]));
                Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));
                Session["LotNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["LotNo"]));
                Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
                Session["Season"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Season"]));

            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                // if (!string.IsNullOrEmpty(Session["LotNo"].ToString()))
                //{
                lotNo = Session["LotNo"].ToString();
                season = Session["Season"].ToString();
                material = Session["Material"].ToString();
                Buyer = Session["Buyer"].ToString();
                BomNo = Session["BomNo"].ToString();
                Group = Session["Group"].ToString();
                Category = Session["Category"].ToString();
                Store = Session["Store"].ToString();
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
                cmd.CommandText = "spBomCosting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BuyerName ", Buyer);
                cmd.Parameters.AddWithValue("@BomNo", BomNo);
                cmd.Parameters.AddWithValue("@Group ", Group);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Store ", Store);
                cmd.Parameters.AddWithValue("@LotNo ", lotNo);
                cmd.Parameters.AddWithValue("@SeasonName ", season);
                cmd.Parameters.AddWithValue("@MaterialMaster", material);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                BomCosting dsRpt = new BomCosting();
                da.Fill(dsRpt, "spBomCosting");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[1];
                crystalReport.Load(Server.MapPath("~/ReportPage/BomCosting.rpt"));
                crystalReport.SetDataSource(dt);
                Email.GetCompany(url, crystalReport);
                CostingReportViewer.ReportSource = crystalReport;
                CostingReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "BomCosting Report", "BomCosting Report");
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