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
    public partial class GateEntryInwardMaterial : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string From = "", To = "", url = "", ReturnType = "", SupplierId = "", PoNoId = "", MaterialNameId = "";
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
                    Session["ReturnType"] = "";
                    Session["SupplierId"] = "";
                    Session["PoNoId"] = "";
                    Session["MaterialNameId"] = "";


                    Session["FromDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["FromDate"]));
                    Session["ToDate"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ToDate"]));
                    Session["ReturnType"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ReturnType"]));
                    Session["SupplierId"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["SupplierId"]));
                    Session["PoNoId"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["PoNoId"]));
                    Session["MaterialNameId"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["MaterialNameId"]));
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
                ReturnType = Session["ReturnType"].ToString();
                SupplierId = Session["SupplierId"].ToString();
                PoNoId = Session["PoNoId"].ToString();
                MaterialNameId = Session["MaterialNameId"].ToString();

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
                cmd.CommandText = "spGateEntryInward";
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime FromDate = DateTime.ParseExact(From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToDate = DateTime.ParseExact(To, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);

                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.Parameters.AddWithValue("@ReturnType", ReturnType);
                cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                cmd.Parameters.AddWithValue("@PoNoId", PoNoId);
                cmd.Parameters.AddWithValue("@MaterialNameId", MaterialNameId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                Reports.DataSet_.GateEntryInwardMaterial rpt = new Reports.DataSet_.GateEntryInwardMaterial();
                da.Fill(rpt, "spGateEntryInward");
                DataTable dt = new DataTable();
                dt = rpt.Tables[0];
                crystalReport.Load(Server.MapPath("~/ReportPage/GateEntryInwardMaterialReport.rpt"));

                TextObject Division1 = (TextObject)crystalReport.ReportDefinition.ReportObjects["EntryDateTime"];
                Division1.Text = "From : " + FromDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " " + "To : " + ToDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                crystalReport.SetDataSource(rpt.Tables[0]);
                Email.GetCompany(url, crystalReport);
                GateEntryInwardMaterialReportViewer.ReportSource = crystalReport;
                GateEntryInwardMaterialReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "GateEntry Inward Report", "GateEntry Inward Report");
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