using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
using System.Globalization;
using CrystalDecisions.Shared;

namespace Reports.View.Views
{
    public partial class BomReport : System.Web.UI.Page
    {

        ReportDocument crystalReport = new ReportDocument();
        string Buyer = "";
        string BomNo = "";
        string Group = "";
        string Store = "";
        string bomid = "";
        string Category = "", url = "", lotNo = "", material = "", season = "";
        string fileName = "";
        string cookievalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Name"] != null)
            {
                cookievalue = Request.Cookies["Name"].Value;
            }
            else
            {
                Response.Cookies["Name"].Value = "";
                Response.Cookies["Name"].Expires = DateTime.Now.AddMinutes(1); // add expiry time
            }
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
                Session["BomNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(cookievalue));
                Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));
                Session["LotNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["LotNo"]));
                Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
                Session["Season"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Season"]));
                // }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                lotNo = Session["LotNo"].ToString();
                season = Session["Season"].ToString();
                material = Session["Material"].ToString();
                Buyer = Session["Buyer"].ToString();
                BomNo = Session["BomNo"].ToString();
                if (!string.IsNullOrEmpty(BomNo))
                {
                    bomid = Email.getBOMID(BomNo);
                    bomid = bomid.TrimEnd();
                }

                Group = Session["Group"].ToString();
                Category = Session["Category"].ToString();
                Store = Session["Store"].ToString();
                if (crystalReport == null)
                    crystalReport.Close();
                crystalReport = new ReportDocument();


                if (!string.IsNullOrEmpty(season))
                {
                    fileName = "";
                    fileName = "Season_" + Email.getSeasonName(season.ToString());
                }
                if (!string.IsNullOrEmpty(lotNo))
                {
                    fileName = "";
                    fileName = "LotNo_" + lotNo;
                }
                if (!string.IsNullOrEmpty(BomNo))
                {
                    fileName = "";
                    string bomno = "StyleNo_" + BomNo;
                    string[] bomaryys = bomno.Split(' ', '\t');
                    string bomnumber = "";
                    foreach (var item in bomaryys)
                    {
                        bomnumber += item + "_";
                    }
                    fileName = bomnumber.TrimEnd();
                }
                BomReportViewer.ID = fileName;
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
                cmd.CommandText = "spBom";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BuyerName ", Buyer);
                cmd.Parameters.AddWithValue("@BomNo", bomid);
                cmd.Parameters.AddWithValue("@Group ", Group);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Store ", Store);
                cmd.Parameters.AddWithValue("@LotNo ", lotNo);
                cmd.Parameters.AddWithValue("@SeasonName ", season);        
                cmd.Parameters.AddWithValue("@MaterialMaster", material);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.Bom dsRpt = new DataSet_.Bom();
                da.Fill(dsRpt, "spBom");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/Bom.rpt"));
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                BomReportViewer.ReportSource = crystalReport;
                BomReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "Bom Report", "Bom Report");
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