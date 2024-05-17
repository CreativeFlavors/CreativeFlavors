using System;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Reports.DataSet_;
using Reports.Common;
using System.Collections.Generic;

namespace Reports.View.Views
{
    public partial class MrpRequirement : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string strcon = ConfigurationManager.ConnectionStrings["EnsysConnectionString"].ConnectionString;
        string MrpNo = "", IndentType = "", Buyer = "", Store = "", Group = "", Category = "", url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["MrpNo"] != null && Request.QueryString["Store"] != null && Request.QueryString["Category"] != null)
                {

                    Session["MrpNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MrpNo"]));
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
            if (!string.IsNullOrEmpty(Session["MrpNo"].ToString()))
            {
                MrpNo = Session["MrpNo"].ToString();
                IndentType = Session["IndentType"].ToString();
                Buyer = Session["Buyer"].ToString();
                if (Session["Group"].ToString().ToLower() != "please select")
                {
                    Group = Session["Group"].ToString();
                }
                if (Session["Store"].ToString().ToLower() != "please select")
                {
                    Store = Session["Store"].ToString();
                }
                if (Session["Category"].ToString().ToLower() != "please select")
                {
                    Category = Session["Category"].ToString();
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
                cmd.CommandText = "MRPRequirementMateriallist_Details";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Store", Store);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@MRPNO", MrpNo);
                cmd.Parameters.AddWithValue("@BuyerMasterId", Buyer);
                cmd.Parameters.AddWithValue("@IndentType", IndentType);
                cmd.Parameters.AddWithValue("@Group", Group);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                MrpReport dsRpt = new MrpReport();

                da.Fill(dsRpt, "MRPRequirementMateriallist_Details");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                var rowColl = dsRpt.Tables[0].AsEnumerable();                
                decimal? sum = 0;
                string Total = "";
                string totalPairs = "";
                //if (!string.IsNullOrEmpty(Store) || !string.IsNullOrEmpty(Group) || !string.IsNullOrEmpty(Category) || !string.IsNullOrEmpty(Buyer))
                //{
                //    if(dt.Select().Length>0)
                //    {
                //        sum = (decimal)dt.Compute("Sum(TotalPairs)", "");
                //        totalPairs = sum.ToString();
                //    }

                //}
                //else
                //{
                //    Total = getTotalPairs(MrpNo);
                //    totalPairs = Total.ToString();
                //}
                Total = getTotalPairs(MrpNo);
                 totalPairs = Total.ToString();
                string storeName_ = "";
                if (!string.IsNullOrEmpty(Store))
                {
                    storeName_ = getStoreName(Convert.ToInt32(Store));
                }
                crystalReport.Load(Server.MapPath("~/ReportPage/MrpRequirement.rpt"));
                string LotNO = dt.Rows[0]["LotNO"].ToString();
                string WeekNO = dt.Rows[0]["WeekNO"].ToString();
                TextObject TotalPairs = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text16"];
                TotalPairs.Text = totalPairs;
                TextObject Groupname = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text38"];
                Groupname.Text = Group;
                TextObject CategoryName = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text39"];
                CategoryName.Text = Category;
                TextObject storeName = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text40"];
                storeName.Text = storeName_;
                TextObject Mrp = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text8"];
                Mrp.Text = MrpNo;
                TextObject Week = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text42"];
                Week.Text = WeekNO;
                TextObject lot = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text15"];
                lot.Text = LotNO;
                string Buyer_ = "";
                if (!string.IsNullOrEmpty(Buyer))
                {
                    Buyer_ = getBuyerName(Convert.ToInt32(Buyer));
                }
                TextObject buyername = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text25"];
                buyername.Text = Buyer_;

                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                MrpRequirementViewer.ReportSource = crystalReport;
                MrpRequirementViewer.DataBind();
               // Email.SendEmail(crystalReport, "Mrp Requirement Report", "Mrp Requirement Report");
            }

            catch (Exception Ex)
            {

            }

        }
        public string getTotalPairs(string StrAtdCCode)
        {
            string TotalPairs = "";

            SqlConnection con = null;
            SqlDataReader Rdr = null;
            using (con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand Cmd = new SqlCommand("select SUM(TotalAmount) from orderentry oe join bom bom on oe.ourstyle = bom.linkbomno join mrpselectedvalues mrpbo on oe.BuyerOrderSlNo = mrpbo.ionumberid join SimpleMRP mrp on mrp.SimpleMRPID = mrpbo.SimpleMRPID  where oe.isinternal = 1 and bom.isdeleted = 0  and oe.isdeleted=0 and mrp.isdeleted=0 and mrp.MRPNO = '" + MrpNo + "'", con);
                Rdr = Cmd.ExecuteReader();
                Rdr.Read();
                TotalPairs = Rdr[0].ToString();
            }
            return TotalPairs;
        }
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
        protected void Page_Unload(object sender, EventArgs e)
        {
            crystalReport.Close();
            crystalReport.Dispose();
        }
    }
}