using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
using System.Globalization;
namespace Reports.View.Views
{
    public partial class IssueRegister : System.Web.UI.Page
    {

        ReportDocument crystalReport = new ReportDocument();
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

                if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null && Request.QueryString["StoreNo"] != null)
                {
                    Session["Material"] = "";
                    Session["From"] = "";
                    Session["To"] = "";
                    Session["Group"] = "";
                    Session["StoreNo"] = "";
                    Session["Category"] = "";
                    Session["MaterialType"] = "";
                    Session["Conveyor"] = "";
                    Session["Customer"] = "";
                    Session["Issue"] = "";
                    Session["IONumber"] = "";
                    Session["MaterialType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialType"]));
                    Session["From"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["To"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["StoreNo"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["StoreNo"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Material"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Material"]));
                    Session["Customer"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Customer"]));
                    Session["Conveyor"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Conveyor"])); ;
                    Session["Issue"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Issue"])); ;
                    Session["IONumber"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["IONumber"])); ;
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                //if (!string.IsNullOrEmpty(Session["From"].ToString()) && !string.IsNullOrEmpty(Session["To"].ToString()) && !string.IsNullOrEmpty(Session["StoreNo"].ToString()))
                //{
                From = Session["From"].ToString();
                To = Session["To"].ToString();
                Group = Session["Group"].ToString();
                StoreNo = Session["StoreNo"].ToString();
                Category = Session["Category"].ToString();
                materialType = Session["MaterialType"].ToString();
                material = Session["Material"].ToString();
                customer = Session["Customer"].ToString();
                conveyor = Session["Conveyor"].ToString();
                issue = Session["Issue"].ToString();
                ionumber = Session["IONumber"].ToString();
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
                cmd.CommandText = "spIssueSlipRegister";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);
                cmd.Parameters.AddWithValue("@StoreNo", StoreNo);
                cmd.Parameters.AddWithValue("@From", FromDate);
                cmd.Parameters.AddWithValue("@To", ToDate);
                cmd.Parameters.AddWithValue("@Group", Group);
                cmd.Parameters.AddWithValue("@Category", Category);
                cmd.Parameters.AddWithValue("@MaterialType", materialType);
                cmd.Parameters.AddWithValue("@MaterialMaster", material);
                cmd.Parameters.AddWithValue("@CustomerName", customer);
                cmd.Parameters.AddWithValue("@ConveyorName", conveyor);
                cmd.Parameters.AddWithValue("@Issue", issue);
                cmd.Parameters.AddWithValue("@IONumber", ionumber);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.IssueSlipRegister dsRpt = new IssueSlipRegister();
                da.Fill(dsRpt, "spIssueSlipRegister");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/IssueSlipRegisterReport.rpt"));

                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["IssueDate"];
                TextObject MaterialType = (TextObject)crystalReport.ReportDefinition.ReportObjects["MaterialType"];
                TextObject IssueFor = (TextObject)crystalReport.ReportDefinition.ReportObjects["IssueFor"];
                if (issue == "1")
                {
                    IssueFor.Text = "Issuing For : Rework";
                }
                if (issue == "2")
                {
                    IssueFor.Text = "Issuing For : Recut";
                }
                if (issue == "3")
                {
                    IssueFor.Text = "Issuing For : Excess Issues";
                }
                if (issue == "4")
                {
                    IssueFor.Text = "Issuing For : Sample";
                }
                if (issue == "5")
                {
                    IssueFor.Text = "Issuing For : Order";
                }
                if (materialType == "1")
                {
                    MaterialType.Text = "Material Type : Local";
                }
                if (materialType == "2")
                {
                    MaterialType.Text = "Material Type : Customer Import";
                }
                if (materialType == "3")
                {
                    MaterialType.Text = "Material Type : Direct Import";
                }
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                crystalReport.SetDataSource(dsRpt.Tables[0]);
                Email.GetCompany(url, crystalReport);
                IssueRegisterReportViewer.ReportSource = crystalReport;
                IssueRegisterReportViewer.DataBind();
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