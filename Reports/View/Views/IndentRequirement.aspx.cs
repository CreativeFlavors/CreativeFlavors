using System;  
using System.Web; 
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Reports.DataSet_;
using Reports.Common;
namespace Reports.View.Views
{
    public partial class IndentRequirement : System.Web.UI.Page
    {        
        ReportDocument crystalReport = new ReportDocument();
        string IndentNo = "",IndentType="",Buyer="",Store ="", Group = "", Category = "",url="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;
              
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["IndentNo"] !=null  && Request.QueryString["Store"] != null && Request.QueryString["Category"] != null)
                {      
                       
                    Session["IndentNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IndentNo"]));
                    Session["IndentType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IndentType"]));
                    Session["Buyer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                }

            }
            if (!string.IsNullOrEmpty(Session["Url"].ToString()))
            {
                url = Session["Url"].ToString();
            }
            if (!string.IsNullOrEmpty(Session["IndentNo"].ToString()))
            {
                IndentNo = Session["IndentNo"].ToString();
              
              
                if(Session["Group"].ToString().ToLower()!= "please select")
                {
                    Group = Session["Group"].ToString();
                }
                if (Session["IndentType"].ToString().ToLower() != "please select")
                {
                    IndentType = Session["IndentType"].ToString();
                }
                if (Session["Store"].ToString().ToLower() != "please select")
                {
                    Store = Session["Store"].ToString();
                }
                if (Session["Category"].ToString().ToLower() != "please select")
                {
                    Category = Session["Category"].ToString();
                }
                if (Session["Buyer"].ToString().ToLower() != "please select")
                {
                    Buyer = Session["Buyer"].ToString();
                }
               
            }
                if (crystalReport == null)
                crystalReport.Close();
            crystalReport = new ReportDocument();
            GC.Collect();
            LoadReport();
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
                cmd.CommandText = "spIndentRequirement";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IndentNo", IndentNo);
                cmd.Parameters.AddWithValue("@BuyerMasterId", Buyer);
                cmd.Parameters.AddWithValue("@IndentType", IndentType);
                cmd.Parameters.AddWithValue("@Store", Store);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Group", Group);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                IndentReport dsRpt = new IndentReport();
                da.Fill(dsRpt, "spIndentRequirement");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];                    
                crystalReport.Load(Server.MapPath("~/ReportPage/IndentRequirement.rpt"));
                string totalPairs = "";
                totalPairs = dt.Rows[0]["TotalPairs"].ToString();
                TextObject TotalPairs = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text16"];
                TotalPairs.Text = totalPairs;
                string seasonName = "";
                seasonName = dt.Rows[0]["SeasonName"].ToString();
                TextObject SeasonName = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text39"];
                SeasonName.Text = seasonName;
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                IndentRequirementViewer.ReportSource = crystalReport;
                IndentRequirementViewer.DataBind();
                //Email.SendEmail(crystalReport,  "Indent Requirement Report", "Indent Requirement Report");
 
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