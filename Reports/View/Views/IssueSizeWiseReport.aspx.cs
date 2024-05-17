using CrystalDecisions.CrystalReports.Engine;
using Reports.Common;
using Reports.DataSet_;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Reports.View.Views
{
    public partial class IssueSizeWiseReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string IssueSlipNumber = "";
        string StoreNo = "", From = "", To = "", Group = "", Category = "", url = "", materialType = "", material = "", customer = "", conveyor = "", issue = "", ionumber = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
              
                Session["Material"] = "";               
                Session["Group"] = "";
                Session["StoreNo"] = "";
                Session["Category"] = "";
                Session["MaterialType"] = "";
                Session["Conveyor"] = "";
                Session["Customer"] = "";
                Session["Issue"] = "";
                Session["IONumber"] = "";
                Session["IssueSlipNo"] = "";
                Session["IssueSlipNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IssueSlipNo"]));
                Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));              
                Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
                Session["Customer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Customer"]));
                Session["Conveyor"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Conveyor"])); ;
                Session["Issue"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Issue"])); ;
                Session["IONumber"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IONumber"])); ;
                //}
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }               
                Group = Session["Group"].ToString();
                StoreNo = Session["StoreNo"].ToString();
                Category = Session["Category"].ToString();
                materialType = Session["MaterialType"].ToString();
                material = Session["Material"].ToString();
                customer = Session["Customer"].ToString();
                conveyor = Session["Conveyor"].ToString();
                issue = Session["Issue"].ToString();
                ionumber = Session["IONumber"].ToString();
                IssueSlipNumber = Session["IssueSlipNo"].ToString(); 
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
                cmd.CommandText = "MULTIPLEISSUESIZEWISEREPORT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STOREID", StoreNo);
                cmd.Parameters.AddWithValue("@ISSUETYPE", "");
                cmd.Parameters.AddWithValue("@CATEGORY", Category);
                cmd.Parameters.AddWithValue("@MATERIALTYPE", materialType);
                cmd.Parameters.AddWithValue("@CUSTOMER", customer);
                cmd.Parameters.AddWithValue("@CONVEYOR", conveyor);
                cmd.Parameters.AddWithValue("@ISSUSLIPNO", IssueSlipNumber);
                cmd.Parameters.AddWithValue("@LOTNO", "");
                cmd.Parameters.AddWithValue("@SEASON", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.SizewiseIssueSet dsRpt = new SizewiseIssueSet();
                da.Fill(dsRpt, "MULTIPLEISSUESIZEWISEREPORT");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[1];
                crystalReport.Load(Server.MapPath("~/ReportPage/MultipleIssueSizewise.rpt"));
                string orderingfor = "";
                string ConveyorName = "";
                string MaterialType = "";
                string CUSTOMERNAME = "";
                string CategoryName = "";
                string STORENAME = "";
                string ISSUETYPE = "";
                string ORDERNO = "";
                string LOTNO = "";
                string STYLE = "";
                orderingfor = dt.Rows[0]["ORDERINGFOR"].ToString();
                ConveyorName = dt.Rows[0]["ConveyorName"].ToString();
                MaterialType = dt.Rows[0]["MATERIALTYPE"].ToString();
                CUSTOMERNAME = dt.Rows[0]["CUSTOMERNAME"].ToString();
                CategoryName = dt.Rows[0]["CategoryName"].ToString();
                STORENAME = dt.Rows[0]["STORENAME"].ToString();             
                ISSUETYPE = dt.Rows[0]["ISSUETYPE"].ToString();
                ORDERNO = dt.Rows[0]["ORDERNO"].ToString();
                LOTNO = dt.Rows[0]["LOTNO"].ToString();
                STYLE = dt.Rows[0]["STYLE"].ToString();
                TextObject issuslip = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text27"];
                issuslip.Text = IssueSlipNumber;
                TextObject storename_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text30"];
                storename_.Text = STORENAME;
                TextObject CategoryName_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text31"];
                CategoryName_.Text = CategoryName;
                TextObject issuetype_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text32"];
                issuetype_.Text = ISSUETYPE;
                TextObject orderingfor_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text33"];
                orderingfor_.Text = orderingfor;
                TextObject lotno_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text38"];
                lotno_.Text = LOTNO;
                TextObject style_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text39"];
                style_.Text = STYLE;
                TextObject ConveyorName_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text41"]; 
                ConveyorName_.Text = ConveyorName;
                TextObject MaterialType_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text36"];
                MaterialType_.Text = MaterialType;
                TextObject CUSTOMERNAME_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text34"];
                CUSTOMERNAME_.Text = CUSTOMERNAME;
                TextObject issuedate_ = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text29"];
                issuedate_.Text = dt.Rows[0]["ISSUEDATE"].ToString();
                crystalReport.SetDataSource(dt);
                Email.GetCompany(url, crystalReport);
                SizewiseIssueRegisterReportViewer.ReportSource = crystalReport;
                SizewiseIssueRegisterReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "Issue Register Report", "Issue Register Report");
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