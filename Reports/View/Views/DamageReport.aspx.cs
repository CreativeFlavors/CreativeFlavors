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
using System.Globalization;
using CrystalDecisions.Shared;


namespace Reports.View.Views
{
    public partial class DamageReport : System.Web.UI.Page
    {
        #region GlobalVaraiables
        ReportDocument crystalReport = new ReportDocument();
        string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
        string From = "";
        string To = "";
        string Order = "", material = "", Buyer = "", Store = "", Group = "", Category = "", url = "";
        string season = ""; string lotno = "";
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                Session["From"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["From"]));
                Session["To"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["To"]));
                Session["Order"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Order"]));
                Session["Buyer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Buyer"]));
                Session["group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["group"]));
                Session["store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["store"]));
                Session["category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["category"]));
                Session["lotNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["lotNo"]));
                Session["season"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["season"]));
                Session["material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["material"]));


            }
            if (!string.IsNullOrEmpty(Session["Url"].ToString()))
            {
                url = Session["Url"].ToString();
            }
            From = Session["From"].ToString();
            To = Session["To"].ToString();
            Order = Session["Order"].ToString();
            material = Session["material"].ToString();
            Buyer = Session["Buyer"].ToString();
            if (Session["Order"].ToString().ToLower() != "please select")
            {
                Order = Session["Order"].ToString();
            }
            if (Session["group"].ToString().ToLower() != "please select")
            {
                Group = Session["group"].ToString();
            }
            if (Session["store"].ToString().ToLower() != "please select")
            {
                Store = Session["store"].ToString();
            }
            if (Session["category"].ToString().ToLower() != "please select")
            {
                Category = Session["category"].ToString();
            }
            if (Session["season"].ToString().ToLower() != "please select")
            {
                season = Session["season"].ToString();
            }
            if (Session["lotNo"].ToString().ToLower() != "please select")
            {
                lotno = Session["lotNo"].ToString();
            }
            //}
            if (crystalReport == null)
                crystalReport.Close();
            crystalReport = new ReportDocument();
            GC.Collect();
            LoadReport();
        }
        #endregion

        #region Load Report

        public void LoadReport()
        {
            string ConStr = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
            SqlConnection Con = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = Con;
                Con.Open();
                cmd.CommandText = "SpDamage_Details";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                cmd.Parameters.AddWithValue("@From", FromDate);
                cmd.Parameters.AddWithValue("@To", ToDate);
                cmd.Parameters.AddWithValue("@BuyerMasterId", Buyer);
                cmd.Parameters.AddWithValue("@Store", Store);
                cmd.Parameters.AddWithValue("@Lotno", lotno);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Orderno", Order);
                cmd.Parameters.AddWithValue("@MaterialType", material);
                cmd.Parameters.AddWithValue("@SeasonID", season);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                DataSet_.SpDamage_Details dsRpt = new DataSet_.SpDamage_Details();
                da.Fill(dsRpt, "SpDamage_Details");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                var rowColl = dsRpt.Tables[0].AsEnumerable();

                crystalReport.Load(Server.MapPath("~/ReportPage/SpDamage_Details.rpt"));
                string Categoryname = "";
                string GroupName = "";
                if(!string.IsNullOrEmpty(Category))
                {
                    Categoryname = Email.getCategoryName(Convert.ToInt32(Category));
                }
                if(!string.IsNullOrEmpty(Group))
                {
                    GroupName = Email.getGroupName(Convert.ToInt32(Group));
                }
                TextObject categoryname_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text17"];
                categoryname_.Text = Categoryname;

                TextObject groupname_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text18"];
                groupname_.Text = GroupName;
                
                TextObject materialType_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text20"];
              
                if (material == "1")
                {
                    materialType_.Text = ": Local";
                }
                if (material == "2")
                {
                    materialType_.Text = ": Customer Import";
                }
                if (material == "3")
                {
                    materialType_.Text = ": Direct Import";
                }
                TextObject fromdate = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text15"];
                fromdate.Text = "From Date:" + Convert.ToDateTime(FromDate).ToString("dd/MM/yyyy") + " " + "To Date:" + Convert.ToDateTime(ToDate).ToString("dd/MM/yyyy");
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                DamageReportViewer.ReportSource = crystalReport;
                DamageReportViewer.DataBind();
                // Email.SendEmail(crystalReport, "Damage Report", "Damage Report");
            }

            catch (Exception Ex)
            {

            }

        }

        #endregion

        #region Helper method
        public string getBuyerName(int buyerMasterid)
        {
            string TotalPairs = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select BuyerFullName from BuyerMaster where BuyerMasterId = '" + buyerMasterid + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                TotalPairs = Rdr[0].ToString();
            }
            return TotalPairs;
        }

        public string getStoreName(int storeMasterid)
        {
            string TotalPairs = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select StoreName from StoreMaster where StoreMasterId = '" + storeMasterid + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                TotalPairs = Rdr[0].ToString();
            }
            return TotalPairs;
        }
        #endregion

        #region PageUnload
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
        #endregion
    }
}