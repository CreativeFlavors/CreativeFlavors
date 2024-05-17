using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Globalization;

namespace Reports.View.Views
{
    public partial class GateEntryInwardDocument : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string From = "", To = "", url = "", FromWhere = "", AddressToWhom = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                url = Request.Url.AbsoluteUri;

                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }

                if (Request.QueryString["FromDate"] != null && Request.QueryString["ToDate"] != null)
                {
                    Session["FromDate"] = "";
                    Session["ToDate"] = "";
                    Session["FromWhere"] = "";
                    Session["AddressToWhom"] = "";
                   
                    Session["FromDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["ToDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["FromWhere"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromWhere"]));
                    Session["AddressToWhom"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["AddressToWhom"]));
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
                FromWhere = Session["FromWhere"].ToString();
                AddressToWhom = Session["AddressToWhom"].ToString();

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
                cmd.CommandText = "spGateEntryInwardDocument";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);

                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@FromWhere", FromWhere);
                cmd.Parameters.AddWithValue("@AddressToWhom", AddressToWhom);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.GateEntryInwardDocument rpt = new Reports.DataSet_.GateEntryInwardDocument();
                da.Fill(rpt, "spGateEntryInwardDocument");
                DataTable dt = new DataTable();
                dt = rpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/GateEntryInwardDocumentReport.rpt"));

                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["EntryDateTime"];
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                crystalReport.SetDataSource(rpt.Tables[0]);
                Email.GetCompany(url, crystalReport);
                GateEntryInwardDocumentReportViewer.ReportSource = crystalReport;
                GateEntryInwardDocumentReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "GateEntry Inward Document Report", "GateEntry Inward Document  Report");
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