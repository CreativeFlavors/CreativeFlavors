using System;
using System.Web;
using Reports.Common;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using Reports.DataSet_;
using Reports.Common;
namespace Reports.View.Views
{
    public partial class GrnGstReport : System.Web.UI.Page
    {
        ReportDocument crystalReport = new ReportDocument();
        string GrnNo = "", Group = "", Category = "", Store = "", Supplier = "", url = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                url = Request.Url.AbsoluteUri;
                if (!string.IsNullOrEmpty(url))
                {
                    Session["Url"] = url;
                }
                if (Request.QueryString["Grn"] != null && !string.IsNullOrEmpty(Request.QueryString["Group"]) && !string.IsNullOrEmpty(Request.QueryString["Category"]) && !string.IsNullOrEmpty(Request.QueryString["Store"]) && !string.IsNullOrEmpty(Request.QueryString["Supplier"]))
                {
                    Session["Group"] = "";
                    Session["Store"] = "";
                    Session["Category"] = "";
                    Session["Grn"] = "";
                    Session["Supplier"] = "";
                    Session["Grn"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Grn"]));
                    Session["Group"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Group"]));
                    Session["Store"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Store"]));
                    Session["Category"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Category"]));
                    Session["Supplier"] = Decryption.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Supplier"]));

                }
            }
            try
            {
                if (!string.IsNullOrEmpty(Session["Url"].ToString()))
                {
                    url = Session["Url"].ToString();
                }
                if (!string.IsNullOrEmpty(Session["Grn"].ToString()) || !string.IsNullOrEmpty(Session["Supplier"].ToString()) || !string.IsNullOrEmpty(Session["Category"].ToString()) || !string.IsNullOrEmpty(Session["Store"].ToString()))
                {
                    GrnNo = Session["Grn"].ToString();
                    Group = Session["Group"].ToString();
                    Store = Session["Store"].ToString();
                    Category = Session["Category"].ToString();
                    Supplier = Session["Supplier"].ToString();
                }
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
                cmd.CommandText = "spGrnGst";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GRNNO", Convert.ToInt32(GrnNo));
                cmd.Parameters.AddWithValue("@Store", Store);
                cmd.Parameters.AddWithValue("@Category", Category); 
                cmd.Parameters.AddWithValue("@Supplier", Supplier);
                cmd.Parameters.AddWithValue("@Group", Group);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                GrnGst dsRpt = new GrnGst();
                da.Fill(dsRpt, "spGrnGst");
                DataTable dt = new DataTable();
                dt = dsRpt.Tables[0];

                DataTable payabledt = new DataTable();
                payabledt = dsRpt.Tables[1];
                string payableValues = "";
                string TotalFreightValues = "";
                string TotalTaxAmoutValues = "";
                string TotalAmountValues = "";
                string TotalDiscountValues = "";
                string TCSValue = "";
                payableValues = payabledt.Rows[0]["payable"].ToString();
                TotalFreightValues = payabledt.Rows[0]["TotalFreight"].ToString();
                TotalTaxAmoutValues = payabledt.Rows[0]["TotalTaxAmout"].ToString();
                TotalAmountValues = payabledt.Rows[0]["TotalAmount"].ToString();
                TotalDiscountValues = payabledt.Rows[0]["TotalDiscount"].ToString();
                TCSValue = payabledt.Rows[0]["TCSValue"].ToString();
                //double x = 1.1;
                double rounding =Math.Round(Convert.ToDouble(payableValues), 0);
                //double TotalFreight = Math.Round(Convert.ToDouble(TotalFreightValues), 0);
                //double TotalTaxAmout = Math.Round(Convert.ToDouble(TotalTaxAmoutValues), 0);
                //double TotalAmount = Math.Round(Convert.ToDouble(TotalAmountValues), 0);
                //double TotalDiscount = Math.Round(Convert.ToDouble(TotalDiscountValues), 0);

                crystalReport.Load(Server.MapPath("~/ReportPage/GrnGstReport.rpt"));

                TextObject TCSValue_val = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text59"];
                TextObject paytableTotal = (TextObject)crystalReport.ReportDefinition.ReportObjects["Text48"];
                TextObject Totaltaxamt_val = (TextObject)crystalReport.ReportDefinition.ReportObjects["Totaltaxamt"];
                TextObject Totalfreightcost_val = (TextObject)crystalReport.ReportDefinition.ReportObjects["Totalfreightcost"];
                TextObject Totalnetamount_val = (TextObject)crystalReport.ReportDefinition.ReportObjects["Totalnetamount"];
                TextObject TotalDiscount_val = (TextObject)crystalReport.ReportDefinition.ReportObjects["TotalDiscount"];
                TCSValue_val.Text = TCSValue.ToString();
                paytableTotal.Text = rounding.ToString();
                Totaltaxamt_val.Text = TotalTaxAmoutValues.ToString();
                Totalfreightcost_val.Text = TotalFreightValues.ToString();
                Totalnetamount_val.Text = TotalAmountValues.ToString();
                TotalDiscount_val.Text = TotalDiscountValues.ToString();
                crystalReport.SetDataSource(dsRpt);
                Email.GetCompany(url, crystalReport);
                GrnGstReportViewer.ReportSource = crystalReport;
                GrnGstReportViewer.DataBind();
                //Email.SendEmail(crystalReport, "GRN GST Report", "GRN GST Report");
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